using DocumentFormat.OpenXml.Office2010.Excel;
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
    public partial class HistorialPagosDeudores : Form
    {
        public int IdDeudor { get; set; }
        public int IdDeuda { get; set; }
        public int IdResponsable { get; set; }
        public decimal Faltante { get; set; }
        public string Cliente { get; set; }
        public HistorialPagosDeudores()
        {
            InitializeComponent();
        }

        private void HistorialPagosDeudores_Load(object sender, EventArgs e)
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
            Buscar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (Faltante <= 0)
            {
                MessageBox.Show("No se pueden introducir mas pagos, la mensualidad esta completa", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PagarDeudores iniP = new PagarDeudores();
            iniP.IdHistorial = 0;
            iniP.IdResponsable = IdResponsable;
            iniP.Cliente = Cliente;
            iniP.IdDeudor = IdDeudor;
            iniP.ShowDialog();
            CalcularFaltante();
            Buscar();
        }
        public void CalcularFaltante()
        {
            AppRepository obj = new AppRepository();
            //if (IdExtra == 0)
            //{
                var Deudasfaltantes = obj.GetFaltantesPagarbyIdDeudor(IdDeudor).Result.ToList();
                if (IdDeuda != 0)
                {
                    Deudasfaltantes = Deudasfaltantes.Where(x => x.Id == IdDeuda).ToList();
                }
                Faltante = 0;
                foreach (var item in Deudasfaltantes)
                {
                    var Pagos = obj.GetHistorialPagosDeudas(item.Id, string.Empty, 0, 0);
                    decimal TotalHistorial = Pagos.Result.Where(x => x.Estatus == "Activo").Sum(x => x.Cantidad);
                    Faltante += item.Cantidad - TotalHistorial;
                }
            //}
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
        public async void Buscar()
        {
            btnBuscar.Enabled = false;
            CrearGridView();
            AppRepository obj = new AppRepository();
            try
            {
                //if (IdExtra == 0)
                //{
                    var Pagos = await obj.GetHistorialPagosDeudas(IdDeuda, txtReferencia.Text.Trim(), (int)NUDTicket.Value, (int)CBBanco.SelectedValue);
                    var listaFinal = Pagos?.ToList() ?? new List<ListHistorialPagosModel>();
                    dgvHistorialPagos.DataSource = new SortableBindingList<ListHistorialPagosModel>(listaFinal);
                //}
                //else
                //{
                //    var Pagos = await obj.GetHistorialPagosExtras(IdExtra, txtReferencia.Text.Trim(), (int)NUDTicket.Value, (int)CBBanco.SelectedValue);
                //    var listaFinal = Pagos?.ToList() ?? new List<ListHistorialPagosExtrasModel>();
                //    dgvHistorialPagos.DataSource = new SortableBindingList<ListHistorialPagosExtrasModel>(listaFinal);
                //}
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
                        if ((string)dgvHistorialPagos.Rows[e.RowIndex].Cells["Estatus"].Value == "Inactivo"
                             && (decimal)dgvHistorialPagos.Rows[e.RowIndex].Cells["Cantidad"].Value > Faltante)
                        {
                            MessageBox.Show("No se puede reactivar este pago, sobre pasa el faltante a pagar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        AppRepository obj = new AppRepository();
                        bool IsExtra = false;// IdExtra == 0 ? false : true;
                        await obj.UpdateEstatusHistorialPagosDeudas(Id, IsExtra);

                        CalcularFaltante();
                        Buscar();
                        break;
                    case "btnEditar":
                        PagarDeudores iniP = new PagarDeudores();
                        //iniP.IdExtra = IdExtra;
                        iniP.IdHistorial = Id;
                        iniP.IdDeuda = IdDeuda;
                        iniP.IdResponsable = IdResponsable;
                        iniP.Cliente = Cliente;
                        iniP.IdDeudor = IdDeudor;
                        iniP.ShowDialog();
                        CalcularFaltante();
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
