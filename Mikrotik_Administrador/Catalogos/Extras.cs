using DocumentFormat.OpenXml.Vml.Office;
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
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador.Catalogos
{
    public partial class Extras : Form
    {
        public int IdUsuarioM { get; set; }
        public int IdResponsable {  get; set; }
        public bool Ocultar { get; set; }
        public Extras()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void Extras_Load(object sender, EventArgs e)
        {
            Buscar();
        }
        private async void Buscar()
        {
            CrearGridView();
            AppRepository obj = new AppRepository();
            try
            {
                var ListExtras = await obj.GetExtrasbyIdUsuarioM(IdUsuarioM);
                var listaFinal = ListExtras?.ToList() ?? new List<ExtrasModel>();
                DGVExtras.DataSource = new SortableBindingList<ExtrasModel>(listaFinal);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }
        }
        public void CrearGridView()
        {
            DGVExtras.Columns.Clear();
            DGVExtras.AutoGenerateColumns = false;
            DGVExtras.EnableHeadersVisualStyles = false;
            // --- ESTILO DE LOS TÍTULOS (HEADERS) CON TU AZUL LOGO ---
            DGVExtras.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            DGVExtras.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            DGVExtras.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            DGVExtras.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            DGVExtras.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            DGVExtras.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);

            DGVExtras.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVExtras.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVExtras.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Costo",
                HeaderText = "Costo",
                DataPropertyName = "Costo",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2", // Aplica formato de moneda local (ej: $120.00 o $120.50)
                    FormatProvider = new System.Globalization.CultureInfo("es-MX") // Forzado a pesos mexicanos
                }
            });            
            DGVExtras.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Fecha_Recibido",
                HeaderText = "Fecha que se recibio",
                DataPropertyName = "Fecha_Recibido",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVExtras.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Fecha_Entregado",
                HeaderText = "Fecha en que se entrego",
                DataPropertyName = "Fecha_Entregado",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVExtras.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                HeaderText = "Estatus",
                DataPropertyName = "Estatus",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            if (Ocultar == false)
            {

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
                DGVExtras.Columns.Add(btnStatus);
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
                DGVExtras.Columns.Add(btnModificar);
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
                DGVExtras.Columns.Add(btnHistorial);
            }
            DGVExtras.AllowUserToAddRows = false;
        }

        private void DGVExtras_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                DGVExtras.Enabled = false;
                AppRepository r = new AppRepository();
                var Id = (int)DGVExtras.Rows[e.RowIndex].Cells["Id"].Value;
                switch (DGVExtras.Columns[e.ColumnIndex].Name)
                {
                    case "btnStatus":
                        VentanaCombo vc = new VentanaCombo();
                        vc.IsExtra = true;
                        if (vc.ShowDialog() == DialogResult.OK)
                        {
                            var result = r.UpdateEstatusExtra(Id, vc.Nombre).Result;
                            Buscar();
                        }                      
                        break;
                    case "btnModificar":
                        Extra ini = new Extra();
                        ini.Id = Id;
                        ini.IdUsuarioM = IdUsuarioM;
                        ini.ShowDialog();
                        Buscar();
                        break;
                    case "btnHistorial":
                        AppRepository obj = new AppRepository();
                        var Historial = obj.GetHistorialPagosExtras(Id, string.Empty, 0, 0).Result;
                        decimal suma = Historial?.Where(x => x.Estatus == "Activo").Sum(x => x.Cantidad) ?? 0m;
                        decimal Costo = (decimal)DGVExtras.Rows[e.RowIndex].Cells["Costo"].Value;
                        HistorialPagos Hi = new HistorialPagos();
                        Hi.IdExtra = Id;
                        Hi.IdMensualidad = 0;
                        Hi.Faltante = Costo - suma < 0 ? 0 : Costo - suma;
                        Hi.Mensualidad = Costo;
                        Hi.IdResponsable = IdResponsable;
                        Hi.Cliente = "";
                        Hi.IdUsuarioM = IdUsuarioM;
                        Hi.UsuarioM = "";
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
                DGVExtras.Enabled = true;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Extra extras = new Extra();
            extras.Id = 0;
            extras.IdUsuarioM = IdUsuarioM;
            extras.ShowDialog();
            Buscar();
        }
    }
}