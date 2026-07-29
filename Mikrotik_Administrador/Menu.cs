using Mikrotik_Administrador.Catalogos;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador
{
    public partial class Menu : Form
    {
        public int IdUsuario { get; set; }
        public int IdTipoUsuario { get; set; }
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
            m.IdUsuario = IdUsuario;
            m.Show();
        }

        private void btnPlanes_Click(object sender, EventArgs e)
        {
            Planes m = new Planes();
            m.PorUsuarios = false;
            m.IdUsuario = IdUsuario;
            m.Tipo = string.Empty;
            m.Show();
        }

        private void btnAsignacion_Click(object sender, EventArgs e)
        {
            Usuarios m = new Usuarios();
            m.IdUsuario = IdUsuario;
            m.Show();
        }

        private void btnInformacion_Click(object sender, EventArgs e)
        {
            InfoClientes m = new InfoClientes();
            m.IdUsuario = IdUsuario;
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

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            HistorialMovimientos H = new HistorialMovimientos();
            H.Show();
        }

        private async void Menu_Load(object sender, EventArgs e)
        {
            if (IdTipoUsuario != 1) //1:Administrador
                return;
            AppRepository obj = new AppRepository();
            var lista = await Task.Run(() => obj.GetHistorialMovimientosUrgentes());
            var listaFinal = lista?.ToList() ?? new List<ListHistorialMovimientosModel>();
            if(listaFinal.Count() > 0)
            {
                MessageBox.Show("Se encontraron situaciónes urgentes a revisar, favor de ir a historial y pulsar en el boton de urgentes.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
