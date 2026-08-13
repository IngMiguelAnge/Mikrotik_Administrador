using Microsoft.VisualBasic;
using Mikrotik_Administrador.Class;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using Mikrotik_Administrador.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador
{
    public partial class Usuarios : Form
    {
        MK mikrotik;
        public int IdCliente { get; set; }
        public int IdResponsable {  get; set; }
        public Usuarios()
        {
            InitializeComponent();
            //this.dgvUsuarios.ColumnHeaderMouseClick += dgvUsuarios_ColumnHeaderMouseClick;
        }
        public void CrearGridViewUsuarios()
        {
            dgvUsuarios.Columns.Clear();
            dgvUsuarios.AutoGenerateColumns = false;
            dgvUsuarios.EnableHeadersVisualStyles = false;
            // --- ESTILO DE LOS TÍTULOS (HEADERS) CON TU AZUL LOGO ---
            dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            dgvUsuarios.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dgvUsuarios.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            dgvUsuarios.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdInterno",
                HeaderText = "IdInterno",
                DataPropertyName = "IdInterno",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Usuario",
                HeaderText = "Usuario",
                DataPropertyName = "Usuario",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Address",
                HeaderText = "IP",
                DataPropertyName = "Address",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                HeaderText = "Estatus",
                DataPropertyName = "Estatus",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdPlan",
                HeaderText = "IdPlan",
                DataPropertyName = "IdPlan",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdPlanOriginal",
                HeaderText = "IdPlanOriginal",
                DataPropertyName = "IdPlanOriginal",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Plan",
                HeaderText = "Plan",
                DataPropertyName = "Plan",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UploadDownload",
                HeaderText = "UploadDownload",
                DataPropertyName = "UploadDownload",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdMikrotik",
                HeaderText = "IdMikrotik",
                DataPropertyName = "IdMikrotik",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Mikrotik",
                HeaderText = "Mikrotik",
                DataPropertyName = "Mikrotik",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdCliente",
                HeaderText = "IdCliente",
                DataPropertyName = "IdCliente",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cliente",
                HeaderText = "Cliente",
                DataPropertyName = "Cliente",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tipo",
                HeaderText = "Tipo",
                DataPropertyName = "Tipo",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MinFechaInicio",
                HeaderText = "Cambio de plan inicia",
                DataPropertyName = "MinFechaInicio",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaxFechaFin",
                HeaderText = "Cambio de plan termina",
                DataPropertyName = "MaxFechaFin",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DataGridViewCheckBoxColumn chkSeleccionar = new DataGridViewCheckBoxColumn
            {
                Name = "chkSeleccionar",
                HeaderText = "Asignar",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                TrueValue = true,
                FalseValue = false,
                IndeterminateValue = false
            };
            dgvUsuarios.Columns.Add(chkSeleccionar);
            DataGridViewButtonColumn btnUbicacion = new DataGridViewButtonColumn
            {
                Name = "btnUbicacion",
                HeaderText = "Acción",
                Text = "Ubicación",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            dgvUsuarios.Columns.Add(btnUbicacion);
            DataGridViewButtonColumn BtnEstatus = new DataGridViewButtonColumn
            {
                Name = "BtnEstatus",
                HeaderText = "Acción",
                Text = "Cambiar Estatus",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            dgvUsuarios.Columns.Add(BtnEstatus);
            dgvUsuarios.AllowUserToAddRows = false;
        }
        public async void BuscarUsuarios(bool sinervicios)
        {
            CrearGridViewUsuarios();
            BtnAsignar.Visible = true;
            cbTodos.Visible = true;
            CBAsignar.Visible = true;
            BtnAsignar.Visible = true;
            BtnEliminar.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            BtnBuscar.Enabled = false;
            BtnAsignar.Enabled = false;
            btnClientesSin.Enabled = false;
            BtnEliminar.Enabled = false;
            int IdMikrotik = CBTodosMikrotiks.Checked == true ? 0 : (int)CBMikrotiks.SelectedValue;
            try
            {
                AppRepository obj = new AppRepository();
                var lista = await obj.GetUsuariosMikrotiksByName(txtNombre.Text, IdMikrotik, txtCliente.Text);
                lblMensaje4.Text = "Clientes sin servicios: " + await obj.GetClientesSinServicios().ContinueWith(t => t.Result.Count.ToString());
                lblServiciossin.Text = "Usuarios sin servicios: " + lista.Where(x => x.IdCliente == null).Count();

                var listaFinal = lista?.ToList() ?? new List<ListUsuariosGeneralModel>();
                dgvUsuarios.DataSource = new SortableBindingList<ListUsuariosGeneralModel>(listaFinal);

                if (dgvUsuarios.Columns["Id"] != null)
                {
                    dgvUsuarios.Columns["Id"].Visible = false;
                }
                if (dgvUsuarios.Columns["IdPlan"] != null)
                {
                    dgvUsuarios.Columns["IdPlan"].Visible = false;
                }
                if (dgvUsuarios.Columns["IdPlanOriginal"] != null)
                {
                    dgvUsuarios.Columns["IdPlanOriginal"].Visible = false;
                }
                if (dgvUsuarios.Columns["IdMikrotik"] != null)
                {
                    dgvUsuarios.Columns["IdMikrotik"].Visible = false;
                }
                if (dgvUsuarios.Columns["IdCliente"] != null)
                {
                    dgvUsuarios.Columns["IdCliente"].Visible = false;
                }
                if (dgvUsuarios.Columns["MinFechaInicio"] != null)
                {
                    dgvUsuarios.Columns["MinFechaInicio"].Visible = false;
                }
                if (dgvUsuarios.Columns["MaxFechaFin"] != null)
                {
                    dgvUsuarios.Columns["MaxFechaFin"].Visible = false;
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
                BtnBuscar.Enabled = true; // Rehabilitamos el botón
                BtnAsignar.Enabled = true;
                btnClientesSin.Enabled = true;
                BtnEliminar.Enabled = true;
            }
        }
        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            if (CBMikrotiks.SelectedValue.ToString() == "0" && CBTodosMikrotiks.Checked == false)
            {
                MessageBox.Show("Por favor, selecciona un Mikrotik.");
                return;
            }
            if (txtNombre.Text.Trim() == "")
            {
                DialogResult resultado = MessageBox.Show("Ha dejado el campo vacio, esto buscara a todos los usuarios pero puede demorar ¿Quiere continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.No)
                {
                    return;
                }
            }
            BtnEliminar.Visible = true;
            BuscarUsuarios(false);
        }
        private void CBTodosMikrotiks_CheckedChanged(object sender, EventArgs e)
        {
            if (CBTodosMikrotiks.Checked)
            {
                CBMikrotiks.Enabled = false;
            }
            else
            {
                CBMikrotiks.Enabled = true;
            }
        }
        private async void Clientes_Load(object sender, EventArgs e)
        {
            AppRepository obj = new AppRepository();
            var listaMikrotiks = await obj.GetMikrotiks();

            // Insertamos un objeto "fantasma" al inicio para el placeholder
            listaMikrotiks.Insert(0, new ListMikrotikModel { Id = 0, Nombre = "Selecciona un Mikrotik" });

            // Configuramos el ComboBox
            CBMikrotiks.DisplayMember = "Nombre"; // Lo que el usuario VE
            CBMikrotiks.ValueMember = "Id";      // El dato que procesas por DETRÁS
            CBMikrotiks.DataSource = listaMikrotiks;
            CBMikrotiks.SelectedIndex = 0;
            lblMensaje4.Text = "Clientes sin servicios: " + await obj.GetClientesSinServicios().ContinueWith(t => t.Result.Count.ToString());
        }

        private void mikrotiksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mikrotiks m = new Mikrotiks();
            m.Show();
            this.Hide();
        }

        private void BtnAsignar_Click(object sender, EventArgs e)
        {
            if (CBMikrotiks.SelectedValue.ToString() == "0" && CBTodosMikrotiks.Checked == false)
            {
                MessageBox.Show("Por favor, selecciona un Mikrotik.");
                return;
            }

            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            btnClientesSin.Enabled = false;
            BtnAsignar.Enabled = false;
            BtnBuscar.Enabled = false;
            BtnEliminar.Enabled = false;
            try
            {
                List<UsuariosModel> Seleccionados = new List<UsuariosModel>();
                Seleccionados = dgvUsuarios.Rows.Cast<DataGridViewRow>()
                 .Where(r => Convert.ToBoolean(r.Cells["chkSeleccionar"].Value))
                  .Select(r => new UsuariosModel
                  {
                      id = Convert.ToInt32(r.Cells["Id"].Value),
                      idmikrotik = Convert.ToInt32(r.Cells["IdMikrotik"].Value),
                      idinterno = Convert.ToString(r.Cells["IdInterno"].Value),
                      name = Convert.ToString(r.Cells["Usuario"].Value),
                      tipo = Convert.ToString(r.Cells["Tipo"].Value),
                  })
                  .ToList();
                if (Seleccionados.Count() == 0)
                {
                    MessageBox.Show("No hay usuarios seleccionados");
                    return;
                }
                string NombreAsignado = string.Empty;
                if (CBAsignar.Checked == false)
                {
                    NombreAsignado = Interaction.InputBox("Escriba el nombre del cliente:", "Registro de Cliente", "");
                    if (NombreAsignado.Trim() == string.Empty)
                    {
                        MessageBox.Show("Se necesita un cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                bool Insert = false;
                AppRepository obj = new AppRepository();

                foreach (UsuariosModel item in Seleccionados)
                {
                    NombreAsignado = CBAsignar.Checked == false ? NombreAsignado : Regex.Replace(item.name, @"[-<>]", " ").Trim().ToUpper();
                    Insert = obj.SaveClienteInGeneral(item.id, NombreAsignado.ToUpper()).Result;
                }
                BuscarUsuarios(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnClientesSin.Enabled = true;
                BtnAsignar.Enabled = true;
                BtnBuscar.Enabled = true;
                BtnEliminar.Enabled = true;
                progressBar1.Style = ProgressBarStyle.Blocks; // Detenemos el movimiento
                progressBar1.Value = 100;
            }
        }
        public void CrearGridViewClientes()
        {
            dgvUsuarios.Columns.Clear();
            dgvUsuarios.AutoGenerateColumns = false;
            dgvUsuarios.EnableHeadersVisualStyles = false;
            // --- ESTILO DE LOS TÍTULOS (HEADERS) CON TU AZUL LOGO ---
            dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            dgvUsuarios.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dgvUsuarios.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            dgvUsuarios.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                HeaderText = "Estatus",
                DataPropertyName = "Estatus",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalServicios",
                HeaderText = "Total de Servicios",
                DataPropertyName = "TotalServicios",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });           
            DataGridViewButtonColumn btnDesactivar = new DataGridViewButtonColumn
            {
                Name = "btnDesactivar",
                HeaderText = "Acción",
                Text = "Cambiar Estatus",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            dgvUsuarios.Columns.Add(btnDesactivar);
            dgvUsuarios.AllowUserToAddRows = false;
        }
        public async Task CargarClientesSin()
        {
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            BtnBuscar.Enabled = false;
            btnClientesSin.Enabled = false;
            BtnAsignar.Visible = false;
            cbTodos.Visible = false;
            CBAsignar.Visible = false;
            BtnAsignar.Visible = false;
            BtnEliminar.Visible = false;
            AppRepository obj = new AppRepository();
            try
            {
                var lista = await obj.GetClientesSinServicios();
                var listaFinal = lista?.ToList() ?? new List<ListClientesModel>();
                dgvUsuarios.DataSource = new SortableBindingList<ListClientesModel>(listaFinal);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 0;
                BtnBuscar.Enabled = true; // Rehabilitamos el botón
                BtnAsignar.Enabled = true;
                btnClientesSin.Enabled = true;
            }
        }

        private async void btnClientesSin_Click(object sender, EventArgs e)
        {
            BtnEliminar.Visible = false;
            await CargarClientesSin();
        }
        private async void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Evitar errores si hacen click en el encabezado
            if (e.RowIndex < 0) return;
            var Id = dgvUsuarios.Rows[e.RowIndex].Cells["Id"].Value;

            switch (dgvUsuarios.Columns[e.ColumnIndex].Name)
            {
                case "btnDesactivar":
                    AppRepository obj = new AppRepository();
                    bool result = await obj.UpdateEstatusCliente(Convert.ToInt32(Id));
                    if (result == true)
                    {
                        MessageBox.Show("Estatus cambiado");
                        await CargarClientesSin();
                    }
                    else
                        MessageBox.Show("Error al desactivar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    var lista = await obj.GetClientesSinServicios();
                    lblMensaje4.Text = "Clientes sin servicios: " + lista.Count.ToString();

                    break;
                case "btnUbicacion":
                    var IdMikrotik = dgvUsuarios.Rows[e.RowIndex].Cells["IdMikrotik"].Value;

                    Ubicacion u = new Ubicacion();
                    u.IdUsuario = Convert.ToInt32(Id);
                    u.IdMikrotik = Convert.ToInt32(IdMikrotik);
                    u.Show();
                    break;
                case "BtnEstatus":
                    ListUsuariosGeneralModel objUsuario = new ListUsuariosGeneralModel();
                    objUsuario.Id = Convert.ToInt32(Id);
                    objUsuario.IdMikrotik = (int)dgvUsuarios.Rows[e.RowIndex].Cells["IdMikrotik"].Value;
                    objUsuario.IdInterno = (string)dgvUsuarios.Rows[e.RowIndex].Cells["IdInterno"].Value;
                    objUsuario.Usuario = (string)dgvUsuarios.Rows[e.RowIndex].Cells["Usuario"].Value;
                    objUsuario.Estatus = (string)dgvUsuarios.Rows[e.RowIndex].Cells["Estatus"].Value;
                    objUsuario.Tipo = (string)dgvUsuarios.Rows[e.RowIndex].Cells["Tipo"].Value;

                    await CambiarEstatus(objUsuario);
                    break;
            }
        }

        public async Task CambiarEstatus(ListUsuariosGeneralModel objUsuario)
        {
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            BtnBuscar.Enabled = false;
            BtnAsignar.Enabled = false;
            btnClientesSin.Enabled = false;
            AppRepository obj = new AppRepository();
            try
            {
                MikrotikModel mikro = new MikrotikModel();
                mikro = obj.GetMikrotikById(objUsuario.IdMikrotik).Result;
                if (mikro.Estatus == false)
                {
                    MessageBox.Show("El Mikrotik seleccionado está desactivado, por favor activelo para continuar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (mikrotik != null)
                {
                    await Task.Run(() => mikrotik.Close());
                    mikrotik = null;
                }
                mikrotik = new MK(mikro.IP, Convert.ToInt32(mikro.Port));

                bool login = await Task.Run(() =>
                {
                    return mikrotik.ConectarYLogin(mikro.Usuario, mikro.Password);
                });
                if (login == false)
                {
                    MessageBox.Show("Error en conexión, revisar que el firewall y nat no esten bloqueando los puertos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                bool Result1 = false;
                bool Result2 = false;
                if (objUsuario.Tipo == "Antena")
                {
                    Result1 = mikrotik.CambiarEstatusAntena(objUsuario.IdInterno, objUsuario.Estatus);
                    Result2 = mikrotik.CambiarEstatusQueues(objUsuario.Usuario, objUsuario.Estatus);
                }
                else
                {
                    Result1 = mikrotik.CambiarEstatusFibra(objUsuario.IdInterno, objUsuario.Estatus);
                    Result2 = true;
                }
                if (Result1 == true && Result2 == true)
                {
                    string nuevoEstatus = objUsuario.Estatus == "Activo" ? "Inactivo" : "Activo";
                    var Res = await obj.UpdateEstatusGeneral(objUsuario.Id, nuevoEstatus, IdResponsable);
                    HistorialMovimientosModel H = new HistorialMovimientosModel
                    {
                        Id = 0,
                        Descripcion = "Se " + nuevoEstatus + " el usuario " + objUsuario.Usuario + " por desición del usuario",
                        Pagina = "En la página de asignaciones",
                        IdUsuario = IdResponsable,
                        Estatus = false
                    };
                    var r = obj.SaveHistorialMovimientos(H);
                    BuscarUsuarios(false);
                }
                else
                    MessageBox.Show("Error al actualizar el estatus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (mikrotik != null)
                {
                    await Task.Run(() => mikrotik.Close());
                }
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 0;
                BtnBuscar.Enabled = true;
                BtnAsignar.Enabled = true;
                btnClientesSin.Enabled = true;
            }
        }

        private void cbTodos_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = cbTodos.Checked;

            foreach (DataGridViewRow row in dgvUsuarios.Rows)
            {
                if (!row.IsNewRow)
                {
                    row.Cells["chkSeleccionar"].Value = isChecked;
                }
            }
        }

        private void CBMikrotiks_SelectedIndexChanged(object sender, EventArgs e)
        {
            CrearGridViewUsuarios();
        }

        private void btnServiciosSin_Click(object sender, EventArgs e)
        {
            if (CBMikrotiks.SelectedValue.ToString() == "0" && CBTodosMikrotiks.Checked == false)
            {
                MessageBox.Show("Por favor, selecciona un Mikrotik.");
                return;
            }
            if (txtNombre.Text.Trim() == "")
            {
                DialogResult resultado = MessageBox.Show("Ha dejado el campo vacio, esto buscara a todos los usuarios pero puede demorar ¿Quiere continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.No)
                {
                    return;
                }
            }
            BtnEliminar.Visible = false;
            BuscarUsuarios(true);
        }

        private async void BtnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Estas por eliminar de forma permanente a los usuarios del mikrotik ¿Quiere continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.No)
            {
                return;
            }
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            BtnBuscar.Enabled = false;
            BtnAsignar.Enabled = false;
            btnClientesSin.Enabled = false;
            BtnEliminar.Enabled = false;
            try
            {
                List<UsuariosModel> Seleccionados = new List<UsuariosModel>();
                Seleccionados = dgvUsuarios.Rows.Cast<DataGridViewRow>()
                 .Where(r => Convert.ToBoolean(r.Cells["chkSeleccionar"].Value))
                  .Select(r => new UsuariosModel
                  {
                      id = Convert.ToInt32(r.Cells["Id"].Value),
                      idmikrotik = Convert.ToInt32(r.Cells["IdMikrotik"].Value),
                      mikrotik = Convert.ToString(r.Cells["Mikrotik"].Value),
                      idinterno = Convert.ToString(r.Cells["IdInterno"].Value),
                      name = Convert.ToString(r.Cells["Usuario"].Value),
                      tipo = Convert.ToString(r.Cells["Tipo"].Value),
                  })
                  .ToList();

                if (Seleccionados.Count == 0)
                {
                    MessageBox.Show("No has seleccionado ningún usuario para exportar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int revisaridmikrotik = 0;
                AppRepository obj = new AppRepository();
                MikrotikModel mikro = new MikrotikModel();
                foreach (UsuariosModel item in Seleccionados.OrderBy(c=> c.idmikrotik))
                {
                    if(revisaridmikrotik != item.idmikrotik)
                    {
                        mikro = obj.GetMikrotikById(item.idmikrotik).Result;
                        if (mikro.Estatus == false)
                        {
                            MessageBox.Show("El Mikrotik "+ item.mikrotik +" está desactivado, por favor activelo para continuar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            BuscarUsuarios(false);
                            return;
                        }
                        if (mikrotik != null)
                        {
                            await Task.Run(() => mikrotik.Close());
                            mikrotik = null;
                        }
                        mikrotik = new MK(mikro.IP, Convert.ToInt32(mikro.Port));
                        // Usamos Task.Run para que la conexión no detenga la ventana
                        bool login = await Task.Run(() =>
                        {
                            return mikrotik.ConectarYLogin(mikro.Usuario, mikro.Password);
                        });
                        if (login == false)
                        {
                            MessageBox.Show("Error en conexión, revisar que el firewall y nat no esten bloqueando los puertos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        revisaridmikrotik = item.idmikrotik;
                    }                   
                    
                    if (item.tipo.ToUpper() == "ANTENA")
                    {
                        mikrotik.EliminarQueuePorNombre(item.name);
                        mikrotik.EliminarAntena(item.idinterno);
                    }
                    else
                    {
                        mikrotik.EliminarFibra(item.idinterno);
                        mikrotik.DeleteInterfacebyName(item.name);
                    }
                    obj.UpdateEstatusGeneral(item.id, "Eliminado", IdResponsable).Wait();
                }
                MessageBox.Show("Usuarios eliminados del Mikrotik correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BuscarUsuarios(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (mikrotik != null)
                {
                    await Task.Run(() => mikrotik.Close());
                }
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 100;
                BtnBuscar.Enabled = true;
                BtnAsignar.Enabled = true;
                btnClientesSin.Enabled = true;
                BtnEliminar.Enabled = true;
            }
        }
    }
}