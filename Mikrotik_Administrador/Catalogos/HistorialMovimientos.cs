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
    public partial class HistorialMovimientos : Form
    {
        public HistorialMovimientos()
        {
            InitializeComponent();
        }
        public void CrearGridView()
        {
            dgvHistorial.Columns.Clear();
            dgvHistorial.AutoGenerateColumns = false;
            dgvHistorial.EnableHeadersVisualStyles = false;
            // --- ESTILO DE LOS TÍTULOS (HEADERS) CON TU AZUL LOGO ---
            dgvHistorial.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            dgvHistorial.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvHistorial.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            dgvHistorial.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dgvHistorial.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            dgvHistorial.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Visible = false,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Usuario",
                HeaderText = "Responsable",
                DataPropertyName = "Usuario",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
           
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Pagina",
                HeaderText = "Página",
                DataPropertyName = "Pagina",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaCreacion",
                HeaderText = "Fecha",
                DataPropertyName = "FechaCreacion",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descripcion",
                HeaderText = "Descripción",
                DataPropertyName = "Descripcion",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvHistorial.AllowUserToAddRows = false;
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            CrearGridView();
            btnBuscar.Enabled = false;
            try
            {
                AppRepository obj = new AppRepository();
                var lista = await Task.Run(() => obj.GetHistorialMovimientos(dtpFechaInicio.Value, dtpFechaFinal.Value));
                var listaFinal = lista?.ToList() ?? new List<ListHistorialMovimientosModel>();
                dgvHistorial.DataSource = new SortableBindingList<ListHistorialMovimientosModel>(listaFinal);
                if (dgvHistorial.Columns["Id"] != null)
                    dgvHistorial.Columns["Id"].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnBuscar.Enabled = true;
            }
        }
    }
}
