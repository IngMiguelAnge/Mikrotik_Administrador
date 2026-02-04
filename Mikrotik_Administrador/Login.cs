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
    public partial class Login : Form
    {

        public Login()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            AppRepository obj = new AppRepository();
            var usuario = obj.GetUser(txtUser.Text, txtPassword.Text).Result;
            if (usuario is null)
            {
                MessageBox.Show("Usuario o contraseña incorrectos");
            }
            else
            {
                Mikrotiks m = new Mikrotiks();
                m.Show();
                this.Hide();
            }
        }
    }
}
