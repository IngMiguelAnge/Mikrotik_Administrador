using Microsoft.VisualBasic;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador.Catalogos
{
    public partial class Pagos : Form
    {
        private int IdCliente { get; set; } = 0;
        private int IdUsuario { get; set; } = 0;
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
            if(txtIdentificador.Text.Trim() != string.Empty)
            {
                DialogResult resultado = MessageBox.Show("El campo identificador tiene información, esto omitira todos los demas campos para busqueda. ¿Quiere continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.No)
                {
                    return;
                }
                ValidarIdentificador();
            }
            Buscar();
        }
        private void ValidarIdentificador()
        {
            // Patrón para la estructura "Cli" + número + "Us" + número
            string patron = @"^CLI(\d+)US(\d+)$";

            Match match = Regex.Match(txtIdentificador.Text.Trim().ToUpper(), patron);

            if (match.Success)
            {
                // Extraer los valores capturados en los grupos de la expresión regular
                IdCliente = int.Parse(match.Groups[1].Value);
                IdUsuario = int.Parse(match.Groups[2].Value);
            }
            else
            {
                MessageBox.Show("La estructura del texto ingresado no es válida. Debe ser tipo 'CliXUsY' (ej. Cli1Us1).", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        public async void Buscar()
        {
            btnBuscar.Enabled = false;
            CrearGridView();
            AppRepository obj = new AppRepository();
            try
            {

                int IdPlan = CBPlan.SelectedIndex <= 0  ? 0 : (int)CBPlan.SelectedValue;
                int IdMikrotik = CBMikrotik.SelectedIndex <= 0 ? 0 : (int)CBMikrotik.SelectedValue;
                string Cliente = txtCliente.Text.Trim();
                string Usuario = txtUsuario.Text.Trim();
                if (IdCliente != 0 && IdUsuario != 0)
                {
                    IdPlan = 0;
                    IdMikrotik = 0;
                    Cliente = string.Empty;
                    Usuario = string.Empty;
                }
                var Servicios = await obj.GetUsuariosandPlanes(IdCliente, IdUsuario, Cliente, Usuario, IdPlan, IdMikrotik);
                var listaFinal = Servicios?.ToList() ?? new List<UsuariosandPlanesModel>();
                dgvClientes.DataSource = new SortableBindingList<UsuariosandPlanesModel>(listaFinal);
                if (dgvClientes.Columns["IdCliente"] != null)
                    dgvClientes.Columns["IdCliente"].Visible = false;
                if (dgvClientes.Columns["IdUser"] != null)
                    dgvClientes.Columns["IdUser"].Visible = false;
                if (dgvClientes.Columns["IdPlan"] != null)
                    dgvClientes.Columns["IdPlan"].Visible = false;
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
            dgvClientes.Columns.Clear();
            dgvClientes.AutoGenerateColumns = false;
            dgvClientes.EnableHeadersVisualStyles = false;
            // --- ESTILO DE LOS TÍTULOS (HEADERS) CON TU AZUL LOGO ---
            dgvClientes.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            dgvClientes.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvClientes.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            dgvClientes.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dgvClientes.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            dgvClientes.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);


            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Identificador",
                HeaderText = "Identificador",
                DataPropertyName = "Identificador",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdCliente",
                HeaderText = "IdCliente",
                DataPropertyName = "IdCliente",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cliente",
                HeaderText = "Cliente",
                DataPropertyName = "Cliente",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdUser",
                HeaderText = "IdUser",
                DataPropertyName = "IdUser",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Usuario",
                HeaderText = "Usuario",
                DataPropertyName = "Usuario",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdPlan",
                HeaderText = "IdPlan",
                DataPropertyName = "IdPlan",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Plan Actual",
                HeaderText = "Plan",
                DataPropertyName = "Plan",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                HeaderText = "Estatus del servicio",
                DataPropertyName = "Estatus",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Mikrotik",
                HeaderText = "Mikrotik",
                DataPropertyName = "Mikrotik",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DataGridViewButtonColumn btnMensualidad = new DataGridViewButtonColumn
            {
                Name = "btnMensualidad",
                HeaderText = "Acción",
                Text = "Mensualidad",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            dgvClientes.Columns.Add(btnMensualidad);
           

            dgvClientes.AllowUserToAddRows = false;
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                int IdPlan = (int)dgvClientes.Rows[e.RowIndex].Cells["IdPlan"].Value;
                int IdUser = (int)dgvClientes.Rows[e.RowIndex].Cells["IdUser"].Value;
                string IdCliente = (string)dgvClientes.Rows[e.RowIndex].Cells["IdCliente"].Value;
                dgvClientes.Enabled = false;
                AppRepository r = new AppRepository();
                switch (dgvClientes.Columns[e.ColumnIndex].Name)
                {
                    case "btnMensualidad":
                        //if (IdMes != 0)
                        //{
                        //    MessageBox.Show("Este servicio ya tiene una fecha limite", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    return;
                        //}
                        //IniciarPagos ini = new IniciarPagos();

                        //ini.ShowDialog();
                        //if (!ini.Guardar) return;
                        //MensualidadModel model = new MensualidadModel
                        //{
                        //    Id = 0,
                        //    FechaLimite = ini.FechaInicio,
                        //    Pagado = false,
                        //    IdPlan = IdPlan,
                        //    PlanCerrado = string.Empty,
                        //    CantidadCerrada = 0,
                        //    IdUsuarioM = IdUser
                        //};
                        //AppRepository obj = new AppRepository();
                        //bool result = obj.SaveMensualidad(model).Result;
                        //if (result)
                        //{
                        //    MessageBox.Show("Guardado correctamente.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    Buscar();
                        //}
                        //else
                        //    MessageBox.Show("Error al guardar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
