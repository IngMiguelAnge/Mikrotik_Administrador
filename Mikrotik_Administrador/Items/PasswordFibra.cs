using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador.Items
{
    public partial class PasswordFibra : Form
    {
        public string Password { get; set; }
        public PasswordFibra()
        {
            InitializeComponent();
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            Password = txtContraseña.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void PasswordFibra_Load(object sender, EventArgs e)
        {
            txtContraseña.Text = "1234";
        }
    }
}
