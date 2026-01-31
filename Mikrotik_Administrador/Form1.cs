using Mikrotik_Administrador.Class;
using Mikrotik_Administrador.Data;
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
    public partial class Form1 : Form
    {
        MK mikrotik = new MK("172.18.1.254", 8728);
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnLogin1_Click(object sender, EventArgs e)
        {
            if (txtEthernet.Text.ToString() != "172.18.1.254")
                mikrotik = new MK(this.txtEthernet.Text.ToString(), Convert.ToInt32(this.txtPort.Text));
            var login = mikrotik.Login(this.txtUsuario1.Text.ToString(), this.txtPassword1.Text.ToString());
            if (login == true)
            {
                lblConexion.Text = "Conexión exitosa";
            }
            else
                lblConexion.Text = "Error en conexión, revisar que el firewall y nat no esten bloqueando los puertos";
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            AppRepository obj = new AppRepository();
            var usuario = obj.GetUser(txtUser.Text, txtPassword.Text).Result;
            if (usuario is null)
            {
                MessageBox.Show("Usuario o contraseña incorrectos");
                return;
            }
            else
            {
                MessageBox.Show("Usuario logeado");
                return;
            }
        }
    }
}
