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
        public int IdResponsable { get; set; }
        public int IdTipoUsuario { get; set; }
        public Menu()
        {
            InitializeComponent();
        }
        //Menu configuracion
        private void OcultarSubMenuConfiguracion()
        {
            if (panelSubmenuConfiguracion.Visible == true)
                panelSubmenuConfiguracion.Visible = false;
        }

        private void MostrarSubMenuConfiguracion(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                OcultarSubMenuConfiguracion();
                submenu.Visible = true;
            }
            else
            {
                submenu.Visible = false;
            }
        }

        private void btnSideConfiguracion_Click(object sender, EventArgs e)
        {
            MostrarSubMenuConfiguracion(panelSubmenuConfiguracion);
        }
        private void btnSubUsuarios_Click(object sender, EventArgs e)
        {
            OcultarSubMenuConfiguracion();
        }

        private void btnSubMikrotik_Click(object sender, EventArgs e)
        {
            OcultarSubMenuConfiguracion();
        }

        private void btnSubComment_Click(object sender, EventArgs e)
        {
            OcultarSubMenuConfiguracion();
        }

        private void btnSubPlanes_Click(object sender, EventArgs e)
        {
            OcultarSubMenuConfiguracion();
        }
        private void btnSubBancos_Click(object sender, EventArgs e)
        {
            OcultarSubMenuConfiguracion();
        }


        //Menu OutSistema
        private void OcultarSubMenuOut()
        {
            if (panelSubmenuOut.Visible == true)
                panelSubmenuOut.Visible = false;
        }

        private void MostrarSubMenuOut(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                OcultarSubMenuOut();
                submenu.Visible = true;
            }
            else
            {
                submenu.Visible = false;
            }
        }

        private void btnSideOut_Click(object sender, EventArgs e)
        {
            MostrarSubMenuOut(panelSubmenuOut);
        }

        private void btnSubMigracion_Click(object sender, EventArgs e)
        {
            OcultarSubMenuOut();
        }

        private void btnSubAsignaciones_Click(object sender, EventArgs e)
        {
            OcultarSubMenuOut();
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
            m.IdResponsable = IdResponsable;
            m.Show();
        }

        private void btnPlanes_Click(object sender, EventArgs e)
        {
            Planes m = new Planes();
            m.PorUsuarios = false;
            m.IdResponsable = IdResponsable;
            m.Tipo = string.Empty;
            m.Show();
        }

        private void btnAsignacion_Click(object sender, EventArgs e)
        {
            Usuarios m = new Usuarios();
            m.IdResponsable = IdResponsable;
            m.Show();
        }

        private void btnInformacion_Click(object sender, EventArgs e)
        {
            InfoClientes m = new InfoClientes();
            m.IdResponsable = IdResponsable;
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
            pagos.IdResponsable = IdResponsable;
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
            {
                btnMikrotiks.Visible = false;
                BtnComments.Visible = false;
                btnMigracion.Visible = false;
                btnPlanes.Visible = false;
                btnAsignacion.Visible = false;
                btnBancos.Visible = false;
                btnHistorial.Visible = false;
                btnUsuarios.Visible = false;
                return;
            }
               
            AppRepository obj = new AppRepository();
            var lista = await Task.Run(() => obj.GetHistorialMovimientosUrgentes());
            var listaFinal = lista?.ToList() ?? new List<ListHistorialMovimientosModel>();
            if(listaFinal.Count() > 0)
            {
                MessageBox.Show("Se encontraron situaciónes urgentes a revisar, favor de ir a historial y pulsar en el boton de urgentes.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            UsuariosSistema us = new UsuariosSistema();
            us.Show();
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            MensualidadesMultiples mm = new MensualidadesMultiples();
            mm.Show();
        }

    }
}
