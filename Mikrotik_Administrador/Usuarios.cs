using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Devices;
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
        public Usuarios()
        {
            InitializeComponent();
        }

        private void Clientes_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
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
                var lista = obj.GetUsuariosMikrotiksByName(txtNombre.Text, IdMikrotik,txtCliente.Text).Result;

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
                 .Where(r => cbTodos.Checked || Convert.ToBoolean(r.Cells["cbSeleccionar"].Value))
                  .Select(r => new UsuariosModel
                  {
                      id = Convert.ToString(r.Cells["Id"].Value),
                      name = Convert.ToString(r.Cells["Nombre"].Value),
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
                    Insert = obj.SaveClienteInGeneral(Convert.ToInt32(item.id), NombreAsignado).Result;
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
                MessageBox.Show("No hay usuarios seleccionados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public void CargarClientesSin() {
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            BtnBuscar.Enabled = false;
            BtnAsignar.Enabled = false;
            btnClientesSin.Enabled = false;
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

        private void informaciónDeClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InfoClientes m = new InfoClientes();
            m.Show();
            this.Hide();
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
                    bool result = obj.UpdateEstatusCliente(Convert.ToInt32(Id)).Result;
                    if (result == true)
                    {
                        MessageBox.Show("Estatus cambiado");
                        CargarClientesSin();
                    }
                    else
                        MessageBox.Show("Error al desactivar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMensaje4.Text = "Clientes sin servicios: " + await obj.GetClientesSinServicios().ContinueWith(t => t.Result.Count.ToString());

                    break;
                case "btnUbicacion":
                    var IdMikrotik = dgvUsuarios.Rows[e.RowIndex].Cells["IdMikrotik"].Value;

                    Ubicacion u = new Ubicacion();
                    u.IdUsuario = Convert.ToInt32(Id);
                    u.IdMikrotik = Convert.ToInt32(IdMikrotik);
                    u.Show();
                    break;
            }
        }
    }
}
