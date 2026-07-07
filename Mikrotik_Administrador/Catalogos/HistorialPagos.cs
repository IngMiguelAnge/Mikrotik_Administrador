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
    public partial class HistorialPagos : Form
    {
        public int IdUser { get; set; }
        public HistorialPagos()
        {
            InitializeComponent();
        }

        private void HistorialPagos_Load(object sender, EventArgs e)
        {
            CBBanco.DataSource = null;
            AppRepository obj = new AppRepository();
            var ListBancos = obj.GetBancos(string.Empty, string.Empty).Result.OrderBy(x => x.Nombre).ToList();
            // Insertamos un objeto "fantasma" al inicio para el placeholder
            ListBancos.Insert(0, new ListBancosModel { Id = 0, Nombre = "Seleccione" });
            CBBanco.DataSource = null;
            CBBanco.DisplayMember = "Nombre";
            CBBanco.ValueMember = "Id";
            CBBanco.DataSource = ListBancos;
            CBBanco.SelectedIndex = 0;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }
        public async void Buscar()
        {
            btnBuscar.Enabled = false;
            CrearGridView();
            AppRepository obj = new AppRepository();
            try
            {
                var Pagos = await obj.GetHistorialPagos(IdUser, txtReferencia.Text.Trim(), (int)NUDTicket.Value, (int)CBBanco.SelectedValue);
                var listaFinal = Pagos?.ToList() ?? new List<ListHistorialPagosModel>();
                dgvPagos.DataSource = new SortableBindingList<ListHistorialPagosModel>(listaFinal);
                if (dgvPagos.Columns["Imagen"] != null)
                    dgvPagos.Columns["Imagen"].Visible = false;
                if (dgvPagos.Columns["Comentario"] != null)
                    dgvPagos.Columns["Comentario"].Visible = false;
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
            dgvPagos.Columns.Clear();
            dgvPagos.AutoGenerateColumns = false;
            dgvPagos.EnableHeadersVisualStyles = false;
            // --- ESTILO DE LOS TÍTULOS (HEADERS) CON TU AZUL LOGO ---
            dgvPagos.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            dgvPagos.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvPagos.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            dgvPagos.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dgvPagos.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            dgvPagos.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);

            dgvPagos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "NoTicket",
                DataPropertyName = "Id",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvPagos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaRecibido",
                HeaderText = "Fecha en que se recibe",
                DataPropertyName = "FechaRecibido",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvPagos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cantidad",
                HeaderText = "Cantidad",
                DataPropertyName = "Cantidad",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvPagos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                HeaderText = "Estatus",
                DataPropertyName = "Estatus",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvPagos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Comentario",
                HeaderText = "Comentario",
                DataPropertyName = "Comentario",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgvPagos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Banco",
                HeaderText = "Banco",
                DataPropertyName = "Banco",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvPagos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Referencia",
                HeaderText = "Referencia",
                DataPropertyName = "Referencia",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgvPagos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaLimite",
                HeaderText = "Fecha Límite",
                DataPropertyName = "FechaLimite",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvPagos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Plan",
                HeaderText = "Plan",
                DataPropertyName = "Plan",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvPagos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Precio",
                HeaderText = "Precio",
                DataPropertyName = "Precio",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvPagos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Imagen",
                HeaderText = "Imagen",
                DataPropertyName = "Imagen",
                Visible = false,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DataGridViewButtonColumn btnVerComentario = new DataGridViewButtonColumn
            {
                Name = "VerComentario",
                HeaderText = "Acción",
                Text = "Ver Comentario",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            dgvPagos.Columns.Add(btnVerComentario);
            DataGridViewButtonColumn btnVerImagen = new DataGridViewButtonColumn
            {
                Name = "VerImagen",
                HeaderText = "Acción",
                Text = "Ver Imagen",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            dgvPagos.Columns.Add(btnVerImagen);

            dgvPagos.AllowUserToAddRows = false;
        }

        private void dgvPagos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                dgvPagos.Enabled = false;
                string Comentario = (string)dgvPagos.Rows[e.RowIndex].Cells["Comentario"].Value;
                byte[] Imagen = (byte[])dgvPagos.Rows[e.RowIndex].Cells["Imagen"].Value;
                switch (dgvPagos.Columns[e.ColumnIndex].Name)
                {
                    case "VerComentario":
                        VerComentario vc = new VerComentario();
                        vc.Comentario = Comentario;
                        vc.ShowDialog();
                        break;
                    case "VerImagen":
                        VerImagen vi = new VerImagen();
                        vi.Imagen = Imagen;
                        vi.ShowDialog();
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
                dgvPagos.Enabled = true;
            }
        }
    }
}
