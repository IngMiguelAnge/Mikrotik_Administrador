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
        public int IdMensualidad { get; set; }
        public decimal Faltante { get; set; }
        public decimal Mensualidad { get; set; }
        public int IdResponsable {  get; set; }
        public string Cliente { get; set; }
        public string UsuarioM {  get; set; }
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
                var Pagos = await obj.GetHistorialPagos(IdMensualidad, txtReferencia.Text.Trim(), (int)NUDTicket.Value, (int)CBBanco.SelectedValue);
                var listaFinal = Pagos?.ToList() ?? new List<ListHistorialPagosModel>();
                dgvHistorialPagos.DataSource = new SortableBindingList<ListHistorialPagosModel>(listaFinal);
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
            dgvHistorialPagos.Columns.Clear();
            dgvHistorialPagos.AutoGenerateColumns = false;
            dgvHistorialPagos.EnableHeadersVisualStyles = false;
            // --- ESTILO DE LOS TÍTULOS (HEADERS) CON TU AZUL LOGO ---
            dgvHistorialPagos.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            dgvHistorialPagos.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvHistorialPagos.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            dgvHistorialPagos.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dgvHistorialPagos.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            dgvHistorialPagos.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);

            dgvHistorialPagos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "NoTicket",
                DataPropertyName = "Id",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvHistorialPagos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaRecibido",
                HeaderText = "Fecha en que se recibe",
                DataPropertyName = "FechaRecibido",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvHistorialPagos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cantidad",
                HeaderText = "Cantidad",
                DataPropertyName = "Cantidad",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvHistorialPagos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                HeaderText = "Estatus",
                DataPropertyName = "Estatus",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvHistorialPagos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Banco",
                HeaderText = "Banco",
                DataPropertyName = "Banco",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvHistorialPagos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Referencia",
                HeaderText = "Referencia",
                DataPropertyName = "Referencia",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgvHistorialPagos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Responsable",
                HeaderText = "Responsable",
                DataPropertyName = "Responsable",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DataGridViewButtonColumn btnCambiarStatus = new DataGridViewButtonColumn
            {
                Name = "btnCambiarStatus",
                HeaderText = "Acción",
                Text = "Cambio Estatus",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            dgvHistorialPagos.Columns.Add(btnCambiarStatus);
            DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn
            {
                Name = "btnEditar",
                HeaderText = "Acción",
                Text = "Editar",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            dgvHistorialPagos.Columns.Add(btnEditar);
            dgvHistorialPagos.AllowUserToAddRows = false;
        }

        private async void dgvHistorialPagos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            dgvHistorialPagos.Enabled = false;
            try
            {
                int Id = (int)dgvHistorialPagos.Rows[e.RowIndex].Cells["Id"].Value;
                switch (dgvHistorialPagos.Columns[e.ColumnIndex].Name)
                {
                    case "btnCambiarStatus":
                        if((string)dgvHistorialPagos.Rows[e.RowIndex].Cells["Estatus"].Value == "Inactivo"
                            && (decimal)dgvHistorialPagos.Rows[e.RowIndex].Cells["Estatus"].Value > Faltante)
                        {
                            MessageBox.Show("No se puede reactivar este pago, sobre pasa el faltante a pagar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                      
                        AppRepository obj = new AppRepository();
                        await obj.UpdateStatusHistorialPagos(Id);
                        Buscar();
                        break;
                    case "btnEditar":
                        Pagar iniP = new Pagar();
                        iniP.Id = Id;
                        iniP.IdMensualidad = IdMensualidad;
                        iniP.Mensualidad = Mensualidad;
                        iniP.IdResponsable = IdResponsable;
                        iniP.Cliente = Cliente;
                        iniP.UsuarioM = UsuarioM;
                        iniP.Faltante = Faltante + (decimal)dgvHistorialPagos.Rows[e.RowIndex].Cells["Cantidad"].Value;
                        iniP.ShowDialog();
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
                dgvHistorialPagos.Enabled = true;
            }
        }
    }
}
