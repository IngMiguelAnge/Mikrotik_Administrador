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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void mikrotiksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mikrotiks m = new Mikrotiks();
            m.Show();
        }

        private void migracionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Migracion m = new Migracion();
            m.Show();
            //this.Hide();
        }

        private void asignacionClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Usuarios m = new Usuarios();
            m.Show();
        }

        private void informacionClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InfoClientes m = new InfoClientes();
            m.Show();
        }

        private void planesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Planes m = new Planes();
            m.PorUsuarios = false;
            m.Tipo = string.Empty;
            m.Show();
        }
    }
}
