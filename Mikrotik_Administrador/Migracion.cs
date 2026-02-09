using Mikrotik_Administrador.Class;
using Mikrotik_Administrador.Data;
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

namespace Mikrotik_Administrador
{
    public partial class Migracion : Form
    {
        MK mikrotik;
        public Migracion()
        {
            InitializeComponent();
        }

        private void mikrotiksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mikrotiks m = new Mikrotiks();
            m.Show();
            this.Close();
        }

        private async void Migracion_Load(object sender, EventArgs e)
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
        }

        private async void BtnBuscar_Click(object sender, EventArgs e)
        {
            if (CBMikrotiks.SelectedValue == null && CBTodosMikrotiks.Checked == false)
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
            BtnBuscar.Enabled = false; // Deshabilitar el botón para evitar múltiples clics
            dgvUsuarios.DataSource = null;
            try
            {
                int Id = 0;
                if (CBTodosMikrotiks.Checked == false)
                {
                    int Id_Mikrotik = (int)CBMikrotiks.SelectedValue;
                    if (Id_Mikrotik == 0)
                    {
                        progressBar1.Style = ProgressBarStyle.Blocks; // Detenemos el movimiento
                        progressBar1.Value = 100;
                        MessageBox.Show("Selecciona un Mikrotik válido de la lista.");
                        return;
                    }
                    Id = Id_Mikrotik;
                    AppRepository obj = new AppRepository();
                    MikrotikModel mikro = new MikrotikModel();
                    mikro = obj.GetMikrotikById(Id_Mikrotik).Result;
                    if (mikro.Estatus == false)
                    {
                        progressBar1.Style = ProgressBarStyle.Blocks; // Detenemos el movimiento
                        progressBar1.Value = 100;
                        MessageBox.Show("El Mikrotik seleccionado está desactivado, por favor activelo para continuar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }
                    if (RBMikrotik.Checked)
                    {
                        mikrotik = new MK(mikro.IP, Convert.ToInt32(mikro.Port));
                        // Usamos Task.Run para que la conexión no detenga la ventana
                        bool login = await Task.Run(() =>
                        {
                            // Aquí dentro va la lógica pesada que antes congelaba todo
                            return mikrotik.ConectarYLogin(mikro.Usuario, mikro.Password);
                        });
                        if (login == false)
                        {
                            progressBar1.Style = ProgressBarStyle.Blocks; // Detenemos el movimiento
                            progressBar1.Value = 100;
                            MessageBox.Show("Error en conexión, revisar que el firewall y nat no esten bloqueando los puertos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        var lista = mikrotik.VerUsuarios(txtNombre.Text).ToList();

                        if (lista != null && lista.Count > 0)
                        {
                            dgvUsuarios.DataSource = lista;
                            if (dgvUsuarios.Columns["chSeleccionar"] == null)
                            {
                                AgregarBotones();
                            }
                        }
                        else
                        {
                            progressBar1.Style = ProgressBarStyle.Blocks; // Detenemos el movimiento
                            progressBar1.Value = 100;
                            MessageBox.Show("No se encontraron usuarios en el Mikrotik seleccionado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }

                if (RBBase.Checked)
                {
                    AppRepository Basse = new AppRepository();

                    var lista = Basse.GetUsuariosMikrotiksByName(txtNombre.Text, Id).Result;

                    if (lista != null && lista.Count > 0)
                    {
                        dgvUsuarios.DataSource = lista;
                    }
                    else
                    {
                        progressBar1.Style = ProgressBarStyle.Blocks; // Detenemos el movimiento
                        progressBar1.Value = 100;
                        MessageBox.Show("No se encontraron usuarios en el Mikrotik seleccionado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                progressBar1.Style = ProgressBarStyle.Blocks; // Detenemos el movimiento
                progressBar1.Value = 100;
            }
            catch (Exception ex)
            {
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 0;
                MessageBox.Show("No se pudo establecer la conexión. Verifique IP y Puertos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                BtnBuscar.Enabled = true; // Rehabilitamos el botón
            }
        }
        private void AgregarBotones()
        {
            // Botón Checket
            DataGridViewCheckBoxColumn chkSeleccionar = new DataGridViewCheckBoxColumn();
            chkSeleccionar.Name = "cbSeleccionar";
            chkSeleccionar.HeaderText = "Copiar a Base";
            dgvUsuarios.Columns.Add(chkSeleccionar);
        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void RBBase_CheckedChanged(object sender, EventArgs e)
        {
            CBTodosMikrotiks.Visible = true;
            CBTodosMikrotiks.Checked = false;
            cbExportar.Visible = false;
            cbExportar.Checked = false;
            btnExportar.Visible = false;
        }

        private void RBMikrotik_CheckedChanged(object sender, EventArgs e)
        {
            CBTodosMikrotiks.Visible = false;
            CBTodosMikrotiks.Checked = false;
            cbExportar.Visible = true;
            cbExportar.Checked = false;
            btnExportar.Visible = true;
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            int cantidadExportada = 0;
            int cantidadNoExportada = 0;
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            btnExportar.Enabled = false; // Bloqueamos el botón para evitar múltiples clics
            try
            {
                List<string> Seleccionados = new List<string>();
                if (cbExportar.Checked)
                {
                    Seleccionados = dgvUsuarios.Rows.Cast<DataGridViewRow>()
                    .Where(r => Convert.ToBoolean(r.Cells["cbSeleccionar"].Value))
                    .Select(r => Convert.ToString(r.Cells["Nombre"].Value)) // Cambia "Id" por tu columna clave
                    .ToList();
                }
                else
                {
                    Seleccionados = dgvUsuarios.Rows.Cast<DataGridViewRow>()
                   .Select(r => Convert.ToString(r.Cells["Nombre"].Value)) // Cambia "Id" por tu columna clave
                   .ToList();
                }
                if (Seleccionados.Count == 0)
                {
                    progressBar1.Style = ProgressBarStyle.Blocks; // Detenemos el movimiento
                    progressBar1.Value = 100;
                    MessageBox.Show("No has seleccionado ningún usuario para exportar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    int Id_Mikrotik = (int)CBMikrotiks.SelectedValue;
                    if (Id_Mikrotik == 0)
                    {
                        progressBar1.Style = ProgressBarStyle.Blocks; // Detenemos el movimiento
                        progressBar1.Value = 100;
                        MessageBox.Show("Selecciona un Mikrotik válido de la lista.");
                        return;
                    }
                    foreach (string item in Seleccionados)
                    {
                        UsuariosGeneralModel objuser = new UsuariosGeneralModel();
                        objuser.Id_Mikrotik = Id_Mikrotik;
                        objuser.Nombre = item;
                        objuser.Id = 0;
                        AppRepository obj = new AppRepository();
                        var res = obj.InsertandUpdateUsuariosGeneral(objuser).Result;
                        if (res)
                        {
                            cantidadExportada++;
                        }
                        else
                        {
                            cantidadNoExportada++;
                        }
                    }
                }
                progressBar1.Style = ProgressBarStyle.Blocks; // Detenemos el movimiento
                progressBar1.Value = 100;
                MessageBox.Show("Usuarios exportados: " + cantidadExportada.ToString() + "\nUsuarios no exportados: " + cantidadNoExportada.ToString(), "Resultado de Exportación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 0;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnExportar.Enabled = true; // Rehabilitamos el botón
            }
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
    }
}
