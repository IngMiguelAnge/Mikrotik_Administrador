using Mikrotik_Administrador.Catalogos;
using System;
using System.IO;
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
      
        private void btnMikrotiks_Click(object sender, EventArgs e)
        {
            Mikrotiks m = new Mikrotiks();
            m.Show();
        }

        private void BtnComments_Click(object sender, EventArgs e)
        {
            Comments c = new Comments();
            c.Show();
        }

        private void btnMigracion_Click(object sender, EventArgs e)
        {
            Migracion m = new Migracion();
            m.Show();
        }

        private void btnPlanes_Click(object sender, EventArgs e)
        {
            Planes m = new Planes();
            m.PorUsuarios = false;
            m.IdMikrotik = 0;
            m.Tipo = string.Empty;
            m.Show();
        }

        private void btnAsignacion_Click(object sender, EventArgs e)
        {
            Usuarios m = new Usuarios();
            m.Show();
        }

        private void btnInformacion_Click(object sender, EventArgs e)
        {
            InfoClientes m = new InfoClientes();
            m.Show();
        }

        private void btnBancos_Click(object sender, EventArgs e)
        {
            Bancos b = new Bancos();
            b.Show();
        }

        private void btnPagos_Click(object sender, EventArgs e)
        {
            Pagos pagos = new Pagos();
            pagos.Show();
        }

        private void btnCambios_Click(object sender, EventArgs e)
        {
            CambiosPlan cp = new CambiosPlan();
            cp.Show();
        }
    }
}
