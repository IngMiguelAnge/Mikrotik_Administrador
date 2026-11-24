using Mikrotik_Administrador.Data;
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
    public partial class Instalaciones : Form
    {
        public int IdUsuarioM { get; set; }
        public string IP {  get; set; }
        public Instalaciones()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void Instalaciones_Load(object sender, EventArgs e)
        {
            Buscar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Instalacion Ins = new Instalacion();
            Ins.Id = 0;
            Ins.IdUsuarioM = IdUsuarioM;
            Ins.IP = IP;
            Ins.ShowDialog();
            Buscar();
        }
        private async void Buscar()
        {
            CrearGridView();
            AppRepository obj = new AppRepository();
            try
            {
                var ListInstalaciones = await obj.GetInstalacionesbyIdUsuarioM(IdUsuarioM);
                var listaFinal = ListInstalaciones?.ToList() ?? new List<ListInstalacionesModel>();
                DGVInstalaciones.DataSource = new SortableBindingList<ListInstalacionesModel>(listaFinal);
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
            DGVInstalaciones.Columns.Clear();
            DGVInstalaciones.AutoGenerateColumns = false;
            DGVInstalaciones.EnableHeadersVisualStyles = false;
            // --- ESTILO DE LOS TÍTULOS (HEADERS) CON TU AZUL LOGO ---
            DGVInstalaciones.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            DGVInstalaciones.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            DGVInstalaciones.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            DGVInstalaciones.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            DGVInstalaciones.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            DGVInstalaciones.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);

            DGVInstalaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVInstalaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Folio",
                HeaderText = "Folio",
                DataPropertyName = "Folio",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVInstalaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Fecha_Instalacion",
                HeaderText = "Fecha de instalación",
                DataPropertyName = "Fecha_Instalacion",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVInstalaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Modalidad",
                HeaderText = "Modalidad",
                DataPropertyName = "Modalidad",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVInstalaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Latitud",
                HeaderText = "Latitud",
                DataPropertyName = "Latitud",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVInstalaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Latitud",
                HeaderText = "Latitud",
                DataPropertyName = "Latitud",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVInstalaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Longitud",
                HeaderText = "Longitud",
                DataPropertyName = "Longitud",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVInstalaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                HeaderText = "Total",
                DataPropertyName = "Total",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2", // Aplica formato de moneda local (ej: $120.00 o $120.50)
                    FormatProvider = new System.Globalization.CultureInfo("es-MX") // Forzado a pesos mexicanos
                }
            });
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
            DGVInstalaciones.Columns.Add(btnModificar);
            DGVInstalaciones.AllowUserToAddRows = false;
        }

    }
}
