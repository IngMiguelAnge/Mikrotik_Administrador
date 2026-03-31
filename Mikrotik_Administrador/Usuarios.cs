using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Devices;
using Mikrotik_Administrador.Class;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador
{
    public partial class Usuarios : Form
    {
        MK mikrotik;
        public Usuarios()
        {
            InitializeComponent();
        }
        public void BuscarUsuarios()
        {
            BtnAsignar.Visible = true;
            cbTodos.Visible = true;
            CBAsignar.Visible = true;
            BtnAsignar.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            BtnBuscar.Enabled = false;
            BtnAsignar.Enabled = false;
            btnClientesSin.Enabled = false;
            dgvUsuarios.DataSource = null;
            dgvUsuarios.Columns.Clear(); // Limpiar columnas anteriores
            int IdMikrotik = CBTodosMikrotiks.Checked == true ? 0 : (int)CBMikrotiks.SelectedValue;
            try
            {
                AppRepository obj = new AppRepository();
                var lista = obj.GetUsuariosMikrotiksByName(txtNombre.Text, IdMikrotik, txtCliente.Text).Result;

                if (lista != null && lista.Count > 0)
                {
                    dgvUsuarios.DataSource = lista;
                    AgregarBotones();
                    MessageBox.Show("Carga completa", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se encontraron usuarios en el Mikrotik seleccionado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
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
            BuscarUsuarios();
        }
        private void AgregarBotones()
        {
            // Botón Checket
            DataGridViewCheckBoxColumn chkSeleccionar = new DataGridViewCheckBoxColumn();
            chkSeleccionar.Name = "cbSeleccionar";
            chkSeleccionar.HeaderText = "Asignar";
            dgvUsuarios.Columns.Add(chkSeleccionar);

            // Botón Ubicacion
            DataGridViewButtonColumn btnUbicacion = new DataGridViewButtonColumn();
            btnUbicacion.Name = "btnUbicacion";
            btnUbicacion.HeaderText = "Acción";
            btnUbicacion.Text = "Ubicación";
            btnUbicacion.UseColumnTextForButtonValue = true;
            dgvUsuarios.Columns.Add(btnUbicacion);

            // Botón Checket
            DataGridViewButtonColumn BtnEstatus = new DataGridViewButtonColumn();
            BtnEstatus.Name = "btnEstatus";
            BtnEstatus.HeaderText = "Acción";
            BtnEstatus.Text = "Cambio Estatus";
            BtnEstatus.UseColumnTextForButtonValue = true;
            dgvUsuarios.Columns.Add(BtnEstatus);

            DataGridViewButtonColumn BtnPlan = new DataGridViewButtonColumn();
            BtnPlan.Name = "btnPlan";
            BtnPlan.HeaderText = "Acción";
            BtnPlan.Text = "Cambio Plan";
            BtnPlan.UseColumnTextForButtonValue = true;
            dgvUsuarios.Columns.Add(BtnPlan);
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

        private void migracionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Migracion m = new Migracion();
            m.Show();
            this.Hide();
        }

        private async void BtnAsignar_Click(object sender, EventArgs e)
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
            try
            {
                AppRepository obj = new AppRepository();

                List<UsuariosModel> Seleccionados = new List<UsuariosModel>();
                Seleccionados = dgvUsuarios.Rows.Cast<DataGridViewRow>()
                 .Where(r => Convert.ToBoolean(r.Cells["cbSeleccionar"].Value))
                  .Select(r => new UsuariosModel
                  {
                      id = Convert.ToString(r.Cells["Id"].Value),
                      name = Convert.ToString(r.Cells["Usuario"].Value),
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
                foreach (UsuariosModel item in Seleccionados)
                {
                    NombreAsignado = CBAsignar.Checked == false ? NombreAsignado : Regex.Replace(item.name, @"[-<>]", " ").Trim().ToUpper();
                    Insert = obj.SaveClienteInGeneral(Convert.ToInt32(item.id), NombreAsignado.ToUpper()).Result;
                    if (Insert == false)
                    {
                        MessageBox.Show("Error al asignar el cliente: " + NombreAsignado, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                dgvUsuarios.DataSource = null;
                dgvUsuarios.Columns.Clear(); // Limpiar columnas anteriores
                int IdMikrotik = CBTodosMikrotiks.Checked == true ? 0 : (int)CBMikrotiks.SelectedValue;
                IdMikrotik = CBTodosMikrotiks.Checked ? 0 : IdMikrotik;
                var lista = obj.GetUsuariosMikrotiksByName(txtNombre.Text, IdMikrotik, txtCliente.Text).Result;

                if (lista != null && lista.Count > 0)
                {
                    dgvUsuarios.DataSource = lista;
                    AgregarBotones();
                }
                lblMensaje4.Text = "Clientes sin servicios: " + await obj.GetClientesSinServicios().ContinueWith(t => t.Result.Count.ToString());
                MessageBox.Show("Clientes asignados correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                progressBar1.Style = ProgressBarStyle.Blocks; // Detenemos el movimiento
                progressBar1.Value = 100;
            }
        }

        public void CargarClientesSin()
        {
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            BtnBuscar.Enabled = false;
            btnClientesSin.Enabled = false;
            BtnAsignar.Visible = false;
            cbTodos.Visible = false;
            CBAsignar.Visible = false;
            BtnAsignar.Visible = false;
            dgvUsuarios.DataSource = null;
            dgvUsuarios.Columns.Clear(); // Limpiar columnas anteriores
            AppRepository obj = new AppRepository();
            try
            {
                var lista = obj.GetClientesSinServicios().Result;

                if (lista != null && lista.Count > 0)
                {
                    dgvUsuarios.DataSource = lista;
                    AgregarBotones2();
                    MessageBox.Show("Carga completa", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se encontraron clientes sin servicios en el Mikrotik seleccionado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
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
            }
        }

        private void btnClientesSin_Click(object sender, EventArgs e)
        {
            CargarClientesSin();
        }

        private void AgregarBotones2()
        {
            // Botón cambio de estatus
            DataGridViewButtonColumn btnDesactivar = new DataGridViewButtonColumn();
            btnDesactivar.Name = "btnDesactivar";
            btnDesactivar.HeaderText = "Acción";
            btnDesactivar.Text = "Cambiar Estatus";
            btnDesactivar.UseColumnTextForButtonValue = true;
            dgvUsuarios.Columns.Add(btnDesactivar);
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
                        CargarClientesSin();
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
                case "btnEstatus":
                    ListUsuariosGeneralModel objUsuario = new ListUsuariosGeneralModel();
                    objUsuario.Id = Convert.ToInt32(Id);
                    objUsuario.IdMikrotik = (int)dgvUsuarios.Rows[e.RowIndex].Cells["IdMikrotik"].Value;
                    objUsuario.IdInterno = (string)dgvUsuarios.Rows[e.RowIndex].Cells["IdInterno"].Value;
                    objUsuario.Usuario = (string)dgvUsuarios.Rows[e.RowIndex].Cells["Usuario"].Value;
                    objUsuario.Estatus = (string)dgvUsuarios.Rows[e.RowIndex].Cells["Estatus"].Value;
                    objUsuario.Tipo = (string)dgvUsuarios.Rows[e.RowIndex].Cells["Tipo"].Value;

                    await CambiarEstatus(objUsuario);
                    break;
                case "btnPlan":
                    Planes p = new Planes();
                    p.IdUsuario = Convert.ToInt32(Id);
                    p.Tipo = (string)dgvUsuarios.Rows[e.RowIndex].Cells["Tipo"].Value;

                    if (p.ShowDialog() == DialogResult.OK)
                    {
                        int idRecibido = p.IdSeleccionado;
                        ListUsuariosGeneralModel objUsuario2 = new ListUsuariosGeneralModel();
                        objUsuario2.Id = Convert.ToInt32(Id);
                        objUsuario2.IdMikrotik = (int)dgvUsuarios.Rows[e.RowIndex].Cells["IdMikrotik"].Value;
                        objUsuario2.IdInterno = (string)dgvUsuarios.Rows[e.RowIndex].Cells["IdInterno"].Value;
                        objUsuario2.Usuario = (string)dgvUsuarios.Rows[e.RowIndex].Cells["Usuario"].Value;
                        objUsuario2.Tipo = (string)dgvUsuarios.Rows[e.RowIndex].Cells["Tipo"].Value;
                        objUsuario2.IdPlan = idRecibido;
                        await CambiarPlan(objUsuario2);
                    }
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
                    var Res = await obj.UpdateEstatusGeneral(objUsuario.Id, nuevoEstatus);
                    BuscarUsuarios();
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
        public async Task CambiarPlan(ListUsuariosGeneralModel objUsuario)
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
                var plan = obj.GetPlanById(objUsuario.IdPlan).Result;
                if (objUsuario.Tipo == "Antena")
                    Result1 = mikrotik.ActualizarVelocidadQueue(objUsuario.Usuario, plan.Velocidad);
                else
                    Result1 = mikrotik.ActualizarUsuarioPPP(objUsuario.IdInterno, plan.Nombre, plan.Velocidad);
                if (Result1 == true)
                {
                   var Res = await obj.UpdatePlanGeneral(objUsuario.Id, plan.Id);
                    BuscarUsuarios();
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
                    row.Cells["cbSeleccionar"].Value = isChecked;
                }
            }
        }
    }
}
