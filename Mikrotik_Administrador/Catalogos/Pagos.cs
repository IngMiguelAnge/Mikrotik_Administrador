using Microsoft.VisualBasic;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Items;
using Mikrotik_Administrador.Model;
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
    public partial class Pagos : Form
    {
        public Pagos()
        {
            InitializeComponent();
        }

        private void Pagos_Load(object sender, EventArgs e)
        {
            CBTipo.SelectedIndex = 0;
            AppRepository obj = new AppRepository();
            var ListMikrotiks = obj.GetMikrotiks().Result.OrderBy(x => x.Nombre).ToList();
            // Insertamos un objeto "fantasma" al inicio para el placeholder
            ListMikrotiks.Insert(0, new ListMikrotikModel { Id = 0, Nombre = "Seleccione" });
            CBMikrotik.DataSource = null;
            CBMikrotik.DisplayMember = "Nombre";
            CBMikrotik.ValueMember = "Id";
            CBMikrotik.DataSource = ListMikrotiks;
            CBMikrotik.SelectedIndex = 0;
        }

        private void CBTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CBPlan.DataSource = null;
            if (CBTipo.SelectedIndex != 0)
            {
                AppRepository obj = new AppRepository();
                bool IsAntena = CBTipo.Text == "Antena" ? true : false;
                var ListPlanes = obj.GetPlanes(IsAntena).Result.OrderBy(x => x.Nombre).ToList();
                ListPlanes.Insert(0, new PlanesModel { Id = 0, Nombre = "Seleccione" });
                CBPlan.DisplayMember = "Nombre";
                CBPlan.ValueMember = "Id";
                CBPlan.DataSource = ListPlanes;
                CBPlan.SelectedIndex = 0;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {       
            Buscar();
        }
        public async void Buscar()
        {
            CrearGridView();
            AppRepository obj = new AppRepository();
            try
            {
                int IdPlan = CBPlan.SelectedIndex <= 0 ? 0 : (int)CBPlan.SelectedValue;
                int IdMikrotik = CBMikrotik.SelectedIndex <= 0 ? 0 : (int)CBMikrotik.SelectedValue;
                var Servicios = await obj.GetUsuariosandPlanes(txtCliente.Text.Trim(), txtUsuario.Text.Trim(), IdPlan, IdMikrotik);
                var listaFinal = Servicios?.ToList() ?? new List<UsuariosandPlanesModel>();
                dgvClientes.DataSource = new BindingList<UsuariosandPlanesModel>(listaFinal);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void CrearGridView()
        {
            dgvClientes.Columns.Clear();
            dgvClientes.AutoGenerateColumns = false;
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cliente",
                HeaderText = "Cliente",
                DataPropertyName = "Cliente",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdUser",
                HeaderText = "IdUser",
                DataPropertyName = "IdUser",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Usuario",
                HeaderText = "Usuario",
                DataPropertyName = "Usuario",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdPlan",
                HeaderText = "IdPlan",
                DataPropertyName = "IdPlan",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Plan",
                HeaderText = "Plan",
                DataPropertyName = "Plan",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Precio",
                HeaderText = "Precio",
                DataPropertyName = "Precio",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Velocidad",
                HeaderText = "Velocidad",
                DataPropertyName = "Velocidad",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdMes",
                HeaderText = "IdMes",
                DataPropertyName = "IdMes",
                Visible = false,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaLimite",
                HeaderText = "Fecha Limite",
                DataPropertyName = "FechaLimite",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EstatusServicio",
                HeaderText = "Estatus del servicio",
                DataPropertyName = "EstatusServicio",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Mikrotik",
                HeaderText = "Mikrotik",
                DataPropertyName = "Mikrotik",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells//Width = 80
            });
            DataGridViewButtonColumn btnIniciar = new DataGridViewButtonColumn
            {
                Name = "Iniciar",
                HeaderText = "Acción",
                Text = "Iniciar",
                UseColumnTextForButtonValue = true,
                Width = 90,
                FlatStyle = FlatStyle.Flat
            };
            dgvClientes.Columns.Add(btnIniciar);
            DataGridViewButtonColumn btnPagar = new DataGridViewButtonColumn
            {
                Name = "Pagar",
                HeaderText = "Acción",
                Text = "Pagar",
                UseColumnTextForButtonValue = true,
                Width = 90,
                FlatStyle = FlatStyle.Flat
            };
            dgvClientes.Columns.Add(btnPagar);
            DataGridViewButtonColumn btnProrroga = new DataGridViewButtonColumn
            {
                Name = "Prorroga",
                HeaderText = "Acción",
                Text = "Prorroga",
                UseColumnTextForButtonValue = true,
                Width = 90,
                FlatStyle = FlatStyle.Flat
            };
            dgvClientes.Columns.Add(btnProrroga);
            DataGridViewButtonColumn btnHistorial = new DataGridViewButtonColumn
            {
                Name = "Historial",
                HeaderText = "Acción",
                Text = "Historial",
                UseColumnTextForButtonValue = true,
                Width = 90,
                FlatStyle = FlatStyle.Flat
            };
            dgvClientes.Columns.Add(btnHistorial);

            dgvClientes.AllowUserToAddRows = false;
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                int IdMes = (int)dgvClientes.Rows[e.RowIndex].Cells["IdMes"].Value;
                int IdPlan = (int)dgvClientes.Rows[e.RowIndex].Cells["IdPlan"].Value;
                int IdUser = (int)dgvClientes.Rows[e.RowIndex].Cells["IdUser"].Value;
                string Cliente = (string)dgvClientes.Rows[e.RowIndex].Cells["Cliente"].Value;
                dgvClientes.Enabled = false;
                AppRepository r = new AppRepository();
                switch (dgvClientes.Columns[e.ColumnIndex].Name)
                {
                    case "Iniciar":
                        if(IdMes != 0)
                        {
                            MessageBox.Show("Este servicio ya tiene una fecha limite", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        IniciarPagos ini = new IniciarPagos();
                        ini.ShowDialog();
                        MensualidadModel model = new MensualidadModel
                        {
                            Id = 0,
                            FechaLimite = ini.FechaInicio,
                            Pagado = false,
                            IdPlan = IdPlan,
                            PlanCerrado = string.Empty,
                            CantidadCerrada = 0,
                            IdUsuarioM = IdUser
                        };
                        AppRepository obj = new AppRepository();
                        bool result = obj.SaveMensualidad(model).Result;
                        if(result)
                        {
                            MessageBox.Show("Guardado correctamente.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Buscar();
                        }
                        else
                            MessageBox.Show("Error al guardar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        break;
                    case "Pagar":
                        if (IdMes == 0)
                        {
                            MessageBox.Show("Este servicio no cuenta con una fecha limite", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        Pagar Pa = new Pagar();
                        Pa.IdUsuarioM = IdUser;
                        Pa.Cliente = Cliente;
                        Pa.ShowDialog();
                        break;
                    case "Prorroga":
                        Prorroga Pr = new Prorroga();
                        Pr.ShowDialog();
                        break;
                    case "Historial":
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
                dgvClientes.Enabled = true;
            }
        }
    }
}
