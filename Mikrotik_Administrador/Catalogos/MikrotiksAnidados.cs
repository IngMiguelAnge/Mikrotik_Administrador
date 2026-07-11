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
    public partial class MikrotiksAnidados : Form
    {
        public int IdPlan { get; set; }
        public MikrotiksAnidados()
        {
            InitializeComponent();
        }
        public void CrearGridView()
        {
            dgvMikrotiks.Columns.Clear();
            dgvMikrotiks.AutoGenerateColumns = false;
            dgvMikrotiks.EnableHeadersVisualStyles = false;
            // --- ESTILO DE LOS TÍTULOS (HEADERS) CON TU AZUL LOGO ---
            dgvMikrotiks.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            dgvMikrotiks.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvMikrotiks.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            dgvMikrotiks.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dgvMikrotiks.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            dgvMikrotiks.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);

            dgvMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Visible = false,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Mikrotik",
                HeaderText = "Mikrotik",
                DataPropertyName = "Mikrotik",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IP",
                HeaderText = "IP",
                DataPropertyName = "IP",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                HeaderText = "Estatus",
                DataPropertyName = "Estatus",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvMikrotiks.AllowUserToAddRows = false;
        }

        private void MikrotiksAnidados_Load(object sender, EventArgs e)
        {
            CrearGridView();
            try
            {
                AppRepository obj = new AppRepository();
                var lista = obj.GetPlanesAnidadosDetalles(IdPlan).Result;
                var listaFinal = lista?.ToList() ?? new List<ListPlanesAnidadosDetalleModel>();
                dgvMikrotiks.DataSource = new SortableBindingList<ListPlanesAnidadosDetalleModel>(listaFinal);
                if (dgvMikrotiks.Columns["Id"] != null)
                    dgvMikrotiks.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }
        }
    }
}
