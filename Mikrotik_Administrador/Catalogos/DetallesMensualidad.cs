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
    public partial class DetallesMensualidad : Form
    {
        public int IdUsuarioM { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public DetallesMensualidad()
        {
            InitializeComponent();
        }

        private async void DetallesMensualidad_Load(object sender, EventArgs e)
        {
            CrearGridView();
            AppRepository obj = new AppRepository();
            try
            {
                var Detalles = await obj.GetDetallesMensualidad(IdUsuarioM, Desde, Hasta);
                if(Detalles.Count() == 0)
                {
                    MessageBox.Show("Este plan transcurrio con normalidad, sin cambios encontrados.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                var listaFinal = Detalles?.ToList() ?? new List<ListDetallesMensualidadModel>();
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
                HeaderText = "Empezo",
                DataPropertyName = "FechaInicio",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaFin",
                HeaderText = "Termino",
                DataPropertyName = "FechaFin",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
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
                Name = "Programacion",
                HeaderText = "Acción",
                DataPropertyName = "Programacion",
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
          
            dgvDetalles.AllowUserToAddRows = false;
        }

    }
}
