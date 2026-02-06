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
        MK mikrotik = new MK("172.18.1.254", 8728);
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

        private void BtnBuscar_Click(object sender, EventArgs e)
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
            Timer1.Interval = 2500 * 10;// 60 seg
            Timer1.Start();
            dgvUsuarios.DataSource = null;
            int Id = 0;
            if (CBTodosMikrotiks.Checked == false)
            {
                int Id_Mikrotik = (int)CBMikrotiks.SelectedValue;
                if (Id_Mikrotik == 0)
                {
                    MessageBox.Show("Selecciona un Mikrotik válido de la lista.");
                    progressBar1.Value = 100;
                    Timer1.Stop();
                    return;
                }
                Id = Id_Mikrotik;
                AppRepository obj = new AppRepository();
                MikrotikModel mikro = new MikrotikModel();
                mikro = obj.GetMikrotikById(Id_Mikrotik).Result;
                if (mikro.Estatus == false)
                {
                    MessageBox.Show("El Mikrotik seleccionado está desactivado, por favor activelo para continuar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    progressBar1.Value = 100;
                    Timer1.Stop();
                    return;
                }
                if (RBMikrotik.Checked)
                {
                    if (mikro.IP != "172.18.1.254")
                        mikrotik = new MK(mikro.IP, Convert.ToInt32(mikro.Port));
                    var login = mikrotik.Login(mikro.Usuario, mikro.Password);
                    if (login == false)
                    {
                        MessageBox.Show("Error en conexión, revisar que el firewall y nat no esten bloqueando los puertos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        progressBar1.Value = 100;
                        Timer1.Stop();
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
            }
            progressBar1.Value = 100;
            Timer1.Stop();
        }
        private void AgregarBotones()
        {
            // Botón Checket
            DataGridViewCheckBoxColumn chkSeleccionar = new DataGridViewCheckBoxColumn();
            chkSeleccionar.Name = "cbSeleccionar";
            chkSeleccionar.HeaderText = "Copiar a Base";
            dgvUsuarios.Columns.Add(chkSeleccionar);
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            ProcessBar();
        }
        public void ProcessBar()
        {
            if (progressBar1.Value < 100)
                progressBar1.Value = progressBar1.Value + 10;
            else
                progressBar1.Value = 0;
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
            Timer1.Interval = 2500 * 10;// 60 seg
            Timer1.Start();
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
            if(Seleccionados.Count == 0)
            {
                MessageBox.Show("No has seleccionado ningún usuario para exportar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressBar1.Value = 100;
                Timer1.Stop();
                return;
            }
            else
            {
                int Id_Mikrotik = (int)CBMikrotiks.SelectedValue;
                if (Id_Mikrotik == 0)
                {
                    MessageBox.Show("Selecciona un Mikrotik válido de la lista.");
                    progressBar1.Value = 100;
                    Timer1.Stop();
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
                    if(res)
                    {
                        cantidadExportada++;
                    }
                    else
                    {
                        cantidadNoExportada++;
                    }
                }               
            }
            progressBar1.Value = 100;
            Timer1.Stop();
            MessageBox.Show("Usuarios exportados: " + cantidadExportada.ToString() + "\nUsuarios no exportados: " + cantidadNoExportada.ToString(), "Resultado de Exportación", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
