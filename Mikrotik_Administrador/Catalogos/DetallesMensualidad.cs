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
                if(Detalles.Count() == 0)
                {
                    MessageBox.Show("Este período transcurrió con normalidad. No hay cambios que detallar.",
                                          "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }
                int diasTotalesMensualidad = 30;
                CrearGridView();
                List<ListDetallesMensualidadModel> ListDestalles = new List<ListDetallesMensualidadModel>();

                int diasOcupadosPorCambios = 0;
                decimal costoAcumuladoDetalles = 0m;
                int idPlanOriginalPeriodo = 0;
                DateTime fechaProcesadaHasta = Desde;

                // 2. Agregar tramos de Cambios / Suspensiones
                foreach (var item in Detalles)
                {
                    if (idPlanOriginalPeriodo == 0 && item.IdPlanOriginal > 0)
                    {
                        idPlanOriginalPeriodo = item.IdPlanOriginal;
                    }

                    // Acotar rango de fechas efectivo dentro de la mensualidad
                    DateTime fInicioEfectiva = Desde > item.FechaInicio ? Desde : item.FechaInicio;
                    DateTime fFinEfectiva = Hasta < item.FechaFin ? Hasta : item.FechaFin;

                    // Días del evento de cambio
                    int diasEfectivos = item.Dias;

                    if (diasEfectivos > 0)
                    {
                        if (diasEfectivos > diasTotalesMensualidad)
                            diasEfectivos = diasTotalesMensualidad;

                        var planNuevo = await obj.GetPlanById(item.IdPlan);
                        decimal precioPlan = planNuevo != null ? planNuevo.Precio : 0m;

                        decimal costoCalculado = 0m;
                        if (diasEfectivos >= diasTotalesMensualidad)
                        {
                            costoCalculado = precioPlan;
                            diasOcupadosPorCambios = 30;
                        }
                        else
                        {
                            diasOcupadosPorCambios += diasEfectivos;
                            decimal costoBruto = diasEfectivos * (precioPlan / 30.0m);
                            costoCalculado = RedondearMontoFinanciero(costoBruto);
                        }

                        costoAcumuladoDetalles += costoCalculado;

                        // Definición coherente de la fecha de término del cambio
                        // Si el evento inicia en fInicioEfectiva y dura N días, la fecha fin inclusiva es fInicioEfectiva + (Dias - 1)
                        DateTime fFinVisual = fInicioEfectiva.AddDays(diasEfectivos - 1);

                        ListDestalles.Add(new ListDetallesMensualidadModel
                        {
                            Id = item.Id,
                            FechaInicio = fInicioEfectiva,
                            FechaFin = fFinVisual,
                            Estatus = item.Estatus,
                            Plan = item.Plan,
                            Costo = costoCalculado
                        });

                        // La fecha de inicio del siguiente tramo será el día posterior al término del cambio
                        fechaProcesadaHasta = fFinVisual.AddDays(1);
                    }
                }

                // 3. Agregar el tramo restante con el Plan Original / Base
                int diasRestantesPlanBase = 30 - diasOcupadosPorCambios;
                if (diasRestantesPlanBase > 0 && fechaProcesadaHasta < Hasta)
                {
                    var planOriginal = idPlanOriginalPeriodo > 0
                        ? await obj.GetPlanById(idPlanOriginalPeriodo)
                        : await obj.GetPlanByIdUsuarioM(IdUsuarioM);

                    string nombrePlanBase = planOriginal != null ? planOriginal.Nombre : "Plan Original";

                    decimal costoBaseFinal = Mensualidad - costoAcumuladoDetalles;
                    if (costoBaseFinal < 0) costoBaseFinal = 0m;

                    // Fecha fin visual del tramo base (un día antes de la fecha límite del mes o la fecha límite exacta)
                    DateTime fFinOriginalVisual = (Hasta.Day == 1) ? Hasta.AddDays(-1) : Hasta;

                    ListDestalles.Add(new ListDetallesMensualidadModel
                    {
                        Id = 0,
                        FechaInicio = fechaProcesadaHasta, // Comienza exactamente al día siguiente de finalizar el cambio (ej. 11/01/2026)
                        FechaFin = fFinOriginalVisual,     // Finaliza en el último día del período (ej. 31/01/2026)
                        Estatus = "Activo",
                        Plan = nombrePlanBase,
                        Costo = costoBaseFinal
                    });
                }
                var listaFinal = ListDestalles?.ToList() ?? new List<ListDetallesMensualidadModel>();
                dgvDetalles.DataSource = new SortableBindingList<ListDetallesMensualidadModel>(listaFinal);
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
