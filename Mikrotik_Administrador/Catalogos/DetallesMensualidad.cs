using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using Mikrotik_Administrador.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador.Catalogos
{
    public partial class DetallesMensualidad : Form
    {
        public int IdUsuarioM { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public decimal Mensualidad {  get; set; }
        public DetallesMensualidad()
        {
            InitializeComponent();
        }

        private async void DetallesMensualidad_Load(object sender, EventArgs e)
        {
            AppRepository obj = new AppRepository();
            try
            {
                var Detalles = await obj.GetTiempoCambioforDetalles(IdUsuarioM, Desde, Hasta);
                if (Detalles == null || !Detalles.Any())
                {
                    MessageBox.Show("Este período transcurrió con normalidad. No hay cambios que detallar.",
                                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }

                CrearGridView();
                List<ListDetallesMensualidadModel> ListDestalles = new List<ListDetallesMensualidadModel>();

                // Obtener el plan base vigente al inicio de este período
                var usuarioMikrotik = obj.GetUsuariosMikrotiksById(IdUsuarioM).Result;
                var planBasePeriodo = await obj.GetPlanById(usuarioMikrotik.IdPlanOriginal);
                int idPlanActual = planBasePeriodo != null ? planBasePeriodo.Id : 0;
                string nombrePlanActual = planBasePeriodo != null ? planBasePeriodo.Nombre : "Plan Base";
                decimal precioPlanActual = planBasePeriodo != null ? planBasePeriodo.Precio : 0m;

                // Si hay un cambio previo que afecte el inicio, buscamos su plan original
                var cambioAnterior = Detalles
                    .Where(x => x.FechaFin < Desde && x.Estatus != "Cancelado")
                    .OrderByDescending(x => x.FechaFin)
                    .FirstOrDefault();

                if (cambioAnterior != null)
                {
                    var planAnt = await obj.GetPlanById(cambioAnterior.IdPlanOriginal);
                    if (planAnt != null)
                    {
                        nombrePlanActual = planAnt.Nombre;
                        precioPlanActual = planAnt.Precio;
                    }
                }

                // Ordenar los eventos de cambio cronológicamente dentro del rango relevante
                var cambiosOrdenados = Detalles
                    .Where(x => x.FechaInicio <= Hasta && x.FechaFin >= Desde && x.Estatus != "Cancelado")
                    .OrderBy(x => x.FechaInicio)
                    .ToList();

                DateTime cursor = Desde;

                // Recorrer de forma continua desde la fecha 'Desde' hasta la fecha 'Hasta'
                foreach (var cambio in cambiosOrdenados)
                {
                    // Si hay un espacio libre antes de que empiece este cambio, pertenece al plan base actual
                    if (cursor < cambio.FechaInicio)
                    {
                        DateTime finTramoBase = cambio.FechaInicio.AddDays(-1);
                        if (finTramoBase > Hasta) finTramoBase = Hasta;

                        if (cursor <= finTramoBase)
                        {
                            int diasBase = (int)(finTramoBase - cursor).TotalDays + 1;
                            decimal costoBase = RedondearMontoFinanciero(diasBase * (precioPlanActual / 30.0m));

                            ListDestalles.Add(new ListDetallesMensualidadModel
                            {
                                Id = 0,
                                FechaInicio = cursor,
                                FechaFin = finTramoBase,
                                Estatus = "Activo",
                                Plan = nombrePlanActual,
                                Costo = costoBase
                            });
                        }
                        cursor = cambio.FechaInicio;
                    }

                    // Acotar el rango del cambio dentro de los límites del período
                    DateTime inicioCambioEfectivo = cursor > cambio.FechaInicio ? cursor : cambio.FechaInicio;
                    DateTime finCambioEfectivo = Hasta < cambio.FechaFin ? Hasta : cambio.FechaFin;

                    if (inicioCambioEfectivo <= finCambioEfectivo)
                    {
                        int diasCambio = (int)(finCambioEfectivo - inicioCambioEfectivo).TotalDays + 1;
                        var planNuevo = await obj.GetPlanById(cambio.IdPlan);
                        decimal precioPlanNuevo = planNuevo != null ? planNuevo.Precio : 0m;
                        decimal costoCambio = RedondearMontoFinanciero(diasCambio * (precioPlanNuevo / 30.0m));

                        ListDestalles.Add(new ListDetallesMensualidadModel
                        {
                            Id = cambio.Id,
                            FechaInicio = inicioCambioEfectivo,
                            FechaFin = finCambioEfectivo,
                            Estatus = cambio.Estatus,
                            Plan = cambio.Plan,
                            Costo = costoCambio
                        });

                        cursor = finCambioEfectivo.AddDays(1);
                    }

                    // Actualizar el plan base posterior según el IdPlanOriginal de este cambio
                    var planOrigCambio = await obj.GetPlanById(cambio.IdPlanOriginal);
                    if (planOrigCambio != null)
                    {
                        nombrePlanActual = planOrigCambio.Nombre;
                        precioPlanActual = planOrigCambio.Precio;
                    }
                }

                // Si queda tiempo después del último cambio hasta llegar a la fecha límite ('Hasta')
                if (cursor <= Hasta)
                {
                    DateTime fechaFinTramoFinal = Hasta;

                    // Opcional si manejas el cierre un día antes cuando es el inicio del mes siguiente:
                    // if (fechaFinTramoFinal.Day == 1 && ListDestalles.Count > 0) fechaFinTramoFinal = fechaFinTramoFinal.AddDays(-1);

                    if (cursor <= fechaFinTramoFinal)
                    {
                        int diasFinales = (int)(fechaFinTramoFinal - cursor).TotalDays + 1;
                        decimal costoFinal = RedondearMontoFinanciero(diasFinales * (precioPlanActual / 30.0m));

                        ListDestalles.Add(new ListDetallesMensualidadModel
                        {
                            Id = 0,
                            FechaInicio = cursor,
                            FechaFin = fechaFinTramoFinal,
                            Estatus = "Activo",
                            Plan = nombrePlanActual,
                            Costo = costoFinal
                        });
                    }
                }

                dgvDetalles.DataSource = new SortableBindingList<ListDetallesMensualidadModel>(ListDestalles);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }
        }
        private decimal RedondearMontoFinanciero(decimal monto)
        {
            decimal parteEntera = Math.Floor(monto);
            decimal parteDecimal = monto - parteEntera;

            if (parteDecimal > 0.00m && parteDecimal < 0.30m)
                return parteEntera;
            else if (parteDecimal >= 0.30m && parteDecimal <= 0.50m)
                return parteEntera + 0.50m;
            else if (parteDecimal > 0.50m)
                return parteEntera + 1.00m;

            return parteEntera;
        }
        public void CrearGridView()
        {
            dgvDetalles.Columns.Clear();
            dgvDetalles.AutoGenerateColumns = false;
            dgvDetalles.EnableHeadersVisualStyles = false;

            // --- ESTILO DE LOS TÍTULOS (HEADERS) CON TU AZUL LOGO ---
            dgvDetalles.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            dgvDetalles.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvDetalles.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            dgvDetalles.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dgvDetalles.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            dgvDetalles.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);

            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaInicio",
                HeaderText = "Empezó",
                DataPropertyName = "FechaInicio",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaFin",
                HeaderText = "Terminó",
                DataPropertyName = "FechaFin",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                HeaderText = "Estatus",
                DataPropertyName = "Estatus",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Plan",
                HeaderText = "Plan",
                DataPropertyName = "Plan",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic,
            });

            // Formato de Moneda ($MXN)
            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Costo",
                HeaderText = "Costo",
                DataPropertyName = "Costo",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    FormatProvider = new System.Globalization.CultureInfo("es-MX")
                }
            });

            dgvDetalles.AllowUserToAddRows = false;
        }

    }
}
