using DocumentFormat.OpenXml.Office2010.Excel;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Items;
using Mikrotik_Administrador.Model;
using Mikrotik_Administrador.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador.Catalogos
{
    public partial class Mensualidades : Form
    {
        public int IdResponsable { get; set; }
        public int IdUsuarioM { get; set; }
        public string Cliente { get; set; }
        public string UsuarioM { get; set; }
        public Mensualidades()
        {
            InitializeComponent();
        }

        private void Mensualidades_Load(object sender, EventArgs e)
        {
            BuscarExtras();
            BuscarProrrogas();
            Buscar();
        }
        public void BuscarExtras()
        {
            AppRepository obj = new AppRepository();
            var ListExtras = obj.GetCostoExtra(IdUsuarioM,0).Result;
            lblServicioExtra.Text = ListExtras != null ?
                "Pago pendiente de los servicios $" + ListExtras.Where(x=> x.Tipo == "Servicio").Sum(x => x.TotalPendiente) :
               "Pago pendiente de los servicios $0.00";
            lblVentaExtra.Text = ListExtras != null ?
                "Pago pendiente de las ventas $" + ListExtras.Where(x => x.Tipo == "Venta").Sum(x => x.TotalPendiente) :
               "Pago pendiente de las ventas $0.00";
            lblPrestamoExtra.Text = ListExtras != null ?
                "Pago pendiente de los prestamos $" + ListExtras.Where(x => x.Tipo == "Prestamo").Sum(x => x.TotalPendiente) :
               "Pago pendiente de los prestamos $0.00";
        }
        public void BuscarProrrogas()
        {
            AppRepository obj = new AppRepository();
            var Prorroga = obj.GetUltimaProrroga(IdUsuarioM).Result;
            lblFechaProrroga.Text = Prorroga != null ? Prorroga.Fecha.ToShortDateString() :
                "Sin prorrogas activas";
        }
        public async void Buscar()
        {
            btnBuscar.Enabled = false;
            CrearGridView();
            AppRepository obj = new AppRepository();
            try
            {
                var Mensualidades = await obj.GetMensualidades(IdUsuarioM);
                var listaFinal = Mensualidades?.ToList() ?? new List<ListMensualidadesModel>();
                dgvMensualidades.DataSource = new SortableBindingList<ListMensualidadesModel>(listaFinal);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnBuscar.Enabled = true;
            }
        }
        public void CrearGridView()
        {
            dgvMensualidades.Columns.Clear();
            dgvMensualidades.AutoGenerateColumns = false;
            dgvMensualidades.EnableHeadersVisualStyles = false;
            // --- ESTILO DE LOS TÍTULOS (HEADERS) CON TU AZUL LOGO ---
            dgvMensualidades.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            dgvMensualidades.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvMensualidades.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            dgvMensualidades.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dgvMensualidades.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            dgvMensualidades.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);


            dgvMensualidades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvMensualidades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DiaCorte",
                HeaderText = "Día Corte",
                DataPropertyName = "DiaCorte",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dgvMensualidades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaInicio",
                HeaderText = "Inicia Mes",
                DataPropertyName = "FechaInicio",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvMensualidades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaLimite",
                HeaderText = "Fecha Limite",
                DataPropertyName = "FechaLimite",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvMensualidades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Responsable",
                HeaderText = "Responsable",
                DataPropertyName = "Responsable",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvMensualidades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Mensualidad",
                HeaderText = "Mensualidad",
                DataPropertyName = "Mensualidad",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2", // Aplica formato de moneda local (ej: $120.00 o $120.50)
                    FormatProvider = new System.Globalization.CultureInfo("es-MX") // Forzado a pesos mexicanos
                }
            });
            dgvMensualidades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Recibido",
                HeaderText = "Recibido",
                DataPropertyName = "Recibido",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2", // Aplica formato de moneda local (ej: $120.00 o $120.50)
                    FormatProvider = new System.Globalization.CultureInfo("es-MX") // Forzado a pesos mexicanos
                }
            });
            dgvMensualidades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Faltante",
                HeaderText = "Faltante",
                DataPropertyName = "Faltante",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2", // Aplica formato de moneda local (ej: $120.00 o $120.50)
                    FormatProvider = new System.Globalization.CultureInfo("es-MX") // Forzado a pesos mexicanos
                }
            });
            dgvMensualidades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                HeaderText = "Estatus",
                DataPropertyName = "Estatus",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DataGridViewButtonColumn btnStatus = new DataGridViewButtonColumn
            {
                Name = "btnStatus",
                HeaderText = "Acción",
                Text = "Cambiar Estatus",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            dgvMensualidades.Columns.Add(btnStatus);
            DataGridViewButtonColumn btnModificar = new DataGridViewButtonColumn
            {
                Name = "btnModificar",
                HeaderText = "Acción",
                Text = "Modificar",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            dgvMensualidades.Columns.Add(btnModificar);
            DataGridViewButtonColumn btnHistorial = new DataGridViewButtonColumn
            {
                Name = "btnHistorial",
                HeaderText = "Acción",
                Text = "Historial de pagos",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            dgvMensualidades.Columns.Add(btnHistorial);
            DataGridViewButtonColumn btnVerDetalles = new DataGridViewButtonColumn
            {
                Name = "btnVerDetalles",
                HeaderText = "Acción",
                Text = "Detalles",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            dgvMensualidades.Columns.Add(btnVerDetalles);
            dgvMensualidades.AllowUserToAddRows = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void dgvMensualidades_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                dgvMensualidades.Enabled = false;
                AppRepository r = new AppRepository();
                var IdMensualidad = (int)dgvMensualidades.Rows[e.RowIndex].Cells["Id"].Value;
                switch (dgvMensualidades.Columns[e.ColumnIndex].Name)
                {
                    case "btnStatus":                        
                        var result = r.UpdateEstatusMensualidad(IdMensualidad).Result;
                        Buscar();
                        break;
                    case "btnModificar":
                        IniciarPagos ini = new IniciarPagos();
                        ini.IdMensualidad = (int)dgvMensualidades.Rows[e.RowIndex].Cells["Id"].Value;
                        ini.IdUsuarioM = IdUsuarioM;
                        ini.IdResponsable = IdResponsable;
                        ini.ShowDialog();
                        Buscar();
                        break;
                    case "btnVerDetalles":
                        DetallesMensualidad detalles = new DetallesMensualidad();
                        detalles.IdUsuarioM = IdUsuarioM;
                        detalles.Desde = (DateTime)dgvMensualidades.Rows[e.RowIndex].Cells["FechaInicio"].Value;
                        detalles.Hasta = (DateTime)dgvMensualidades.Rows[e.RowIndex].Cells["FechaLimite"].Value;
                        detalles.Mensualidad = Convert.ToDecimal(dgvMensualidades.Rows[e.RowIndex].Cells["Mensualidad"].Value);
                        detalles.ShowDialog();
                        break;
                    case "btnHistorial":
                        HistorialPagos Hi = new HistorialPagos();
                        Hi.IdExtra = 0;
                        Hi.IdMensualidad = (int)dgvMensualidades.Rows[e.RowIndex].Cells["Id"].Value;
                        Hi.Faltante = (decimal)dgvMensualidades.Rows[e.RowIndex].Cells["Faltante"].Value;
                        Hi.Mensualidad = (decimal)dgvMensualidades.Rows[e.RowIndex].Cells["Mensualidad"].Value;
                        Hi.IdResponsable = IdResponsable;
                        Hi.Cliente = Cliente;
                        Hi.IdUsuarioM = IdUsuarioM;
                        Hi.UsuarioM = UsuarioM;                     
                        Hi.ShowDialog();
                        Buscar();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}");
            }
            finally
            {
                dgvMensualidades.Enabled = true;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            IniciarPagos ini = new IniciarPagos();
            ini.IdMensualidad = 0;
            ini.IdUsuarioM = IdUsuarioM;
            ini.IdResponsable = IdResponsable;
            if (ini.ShowDialog() != DialogResult.OK)
            { return; }
            Buscar();
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            AppRepository obj = new AppRepository();
            var RevisarMensualidades = obj.GetRevisionMensualidades(IdUsuarioM).Result.ToList();
            if(RevisarMensualidades.Count > 0)
            {
                MessageBox.Show("No se puede realizar el pago, ya que existen mensualidades pendientes de revisión. Por favor, revisa el historial de pagos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Pagar iniP = new Pagar();
            iniP.IdExtra = 0;
            iniP.IdHistorial = 0;
            iniP.IdMensualidadRecibida = 0;
            iniP.IdResponsable = IdResponsable;
            iniP.Cliente = Cliente;
            iniP.IdUsuarioM = IdUsuarioM;
            iniP.UsuarioM = UsuarioM;
            iniP.ShowDialog();
            BuscarExtras();
            Buscar();
        }

        private void btnVerProrrogas_Click(object sender, EventArgs e)
        {
            Prorrogas pr = new Prorrogas();
            pr.IdUsuarioM = IdUsuarioM;
            pr.ShowDialog();
            BuscarProrrogas();
        }

        private void btnExtras_Click(object sender, EventArgs e)
        {
            Extras ext = new Extras();
            ext.IdResponsable = IdResponsable;
            ext.IdUsuarioM = IdUsuarioM;
            ext.Ocultar = false;
            ext.ShowDialog();
            BuscarExtras();
        }
    }
}
