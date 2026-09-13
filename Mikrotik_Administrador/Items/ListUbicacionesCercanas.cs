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

namespace Mikrotik_Administrador.Items
{
    public partial class ListUbicacionesCercanas : Form
    {
        public List<UbicacionesCercanas> listUbicaciones{ get; set; }
        public ListUbicacionesCercanas()
        {
            InitializeComponent();
        }

        private void UbibcacionesCercanas_Load(object sender, EventArgs e)
        {
            CrearGridView();
            var listaFinal = listUbicaciones?.ToList() ?? new List<UbicacionesCercanas>();
            dgvUbicaciones.DataSource = new SortableBindingList<UbicacionesCercanas>(listaFinal);
        }
        public void CrearGridView()
        {
            dgvUbicaciones.Columns.Clear();
            dgvUbicaciones.AutoGenerateColumns = false;
            dgvUbicaciones.EnableHeadersVisualStyles = false;
            // --- ESTILO DE LOS TÍTULOS (HEADERS) CON TU AZUL LOGO ---
            dgvUbicaciones.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            dgvUbicaciones.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvUbicaciones.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            dgvUbicaciones.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dgvUbicaciones.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            dgvUbicaciones.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);

            dgvUbicaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUbicaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Servicio",
                HeaderText = "Servicio",
                DataPropertyName = "Servicio",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUbicaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Address",
                HeaderText = "Address",
                DataPropertyName = "Address",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUbicaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                HeaderText = "Estatus del servicio",
                DataPropertyName = "Estatus",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUbicaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Mikrotik",
                HeaderText = "Registrado en",
                DataPropertyName = "Mikrotik",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUbicaciones.AllowUserToAddRows = false;
        }
        private void btnCancelar_Click(object sender, EventArgs e) => this.Close();

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
