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
    public partial class Prorrogas : Form
    {
        public int IdUsuarioM { get; set; }
        public Prorrogas()
        {
            InitializeComponent();
        }

        private void Prorrogas_Load(object sender, EventArgs e)
        {
            Buscar(); 
        }
        public async void Buscar()
        {
            CrearGridView();
            AppRepository obj = new AppRepository();
            try
            {
                var ListProrrogas = await obj.GetProrrogasbyIdUsuarioM(IdUsuarioM);
                var listaFinal = ListProrrogas?.ToList() ?? new List<ListProrrogasModel>();
                DGVProrrogas.DataSource = new SortableBindingList<ListProrrogasModel>(listaFinal);
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
            DGVProrrogas.Columns.Clear();
            DGVProrrogas.AutoGenerateColumns = false;
            DGVProrrogas.EnableHeadersVisualStyles = false;
            // --- ESTILO DE LOS TÍTULOS (HEADERS) CON TU AZUL LOGO ---
            DGVProrrogas.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            DGVProrrogas.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            DGVProrrogas.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            DGVProrrogas.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            DGVProrrogas.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            DGVProrrogas.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);


            DGVProrrogas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVProrrogas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Fecha",
                HeaderText = "Prorroga hasta el día",
                DataPropertyName = "Fecha",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVProrrogas.Columns.Add(new DataGridViewTextBoxColumn
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
            DGVProrrogas.Columns.Add(btnStatus);
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
            DGVProrrogas.Columns.Add(btnModificar);
            DGVProrrogas.AllowUserToAddRows = false;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Prorroga pr = new Prorroga();
            pr.Id = 0;
            pr.IdUsuarioM = IdUsuarioM;
            pr.ShowDialog();
            Buscar();
        }

        private void DGVProrrogas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                DGVProrrogas.Enabled = false;
                AppRepository r = new AppRepository();
                var Id = (int)DGVProrrogas.Rows[e.RowIndex].Cells["Id"].Value;

                switch (DGVProrrogas.Columns[e.ColumnIndex].Name)
                {
                    case "btnStatus":
                        var result = r.UpdateEstatusProrroga(Id).Result;
                        Buscar();
                        break;
                    case "btnModificar":
                        Prorroga ini = new Prorroga();
                        ini.Id = Id;
                        ini.IdUsuarioM = IdUsuarioM;
                        ini.ShowDialog();
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
                DGVProrrogas.Enabled = true;
            }
        }
    }
}
