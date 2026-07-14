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
    public partial class CambiosPlan : Form
    {
        public CambiosPlan()
        {
            InitializeComponent();
        }

        private void CambiosPlan_Load(object sender, EventArgs e)
        {
        }
        public void CrearGridView()
        {
            DGVCambios.Columns.Clear();
            DGVCambios.AutoGenerateColumns = false;
            DGVCambios.EnableHeadersVisualStyles = false;
            // --- ESTILO DE LOS TÍTULOS (HEADERS) CON TU AZUL LOGO ---
            DGVCambios.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            DGVCambios.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            DGVCambios.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            DGVCambios.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            DGVCambios.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            DGVCambios.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);

            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Visible = false,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Usuario",
                HeaderText = "Usuario ha afectar",
                DataPropertyName = "Usuario",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Dias",
                HeaderText = "Días que durará",
                DataPropertyName = "Dias",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Horas",
                HeaderText = "Horas que durará",
                DataPropertyName = "Horas",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaInicio",
                HeaderText = "Fecha que se iniciara",
                DataPropertyName = "FechaInicio",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaFin",
                HeaderText = "Fecha que terminara",
                DataPropertyName = "FechaFin",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Modo",
                HeaderText = "Modo",
                DataPropertyName = "Modo",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdUsuarioM",
                HeaderText = "IdUsuarioM",
                DataPropertyName = "IdUsuarioM",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdPlan",
                HeaderText = "IdPlan",
                DataPropertyName = "IdPlan",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PlanNuevo",
                HeaderText = "Se cambiara por el plan",
                DataPropertyName = "PlanNuevo",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                HeaderText = "Estatus",
                DataPropertyName = "Estatus",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nota",
                HeaderText = "Nota",
                DataPropertyName = "Nota",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVCambios.AllowUserToAddRows = false;
        }
        public void Buscar()
        {
            CrearGridView();
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación

            try
            {
                AppRepository obj = new AppRepository();
                var lista = obj.GetTiempoCambio(dtpFechaInicio.Value, dtpFechaFinal.Value).Result;
                var listaFinal = lista?.ToList() ?? new List<ListTiempoCambioModel>();
                DGVCambios.DataSource = new SortableBindingList<ListTiempoCambioModel>(listaFinal);
                if (DGVCambios.Columns["Id"] != null)
                {
                    DGVCambios.Columns["Id"].Visible = false;
                }
                if (DGVCambios.Columns["IdPlan"] != null)
                {
                    DGVCambios.Columns["IdPlan"].Visible = false;
                }
                if (DGVCambios.Columns["IdUsuarioM"] != null)
                {
                    DGVCambios.Columns["IdUsuarioM"].Visible = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 0;
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }
    }
}
