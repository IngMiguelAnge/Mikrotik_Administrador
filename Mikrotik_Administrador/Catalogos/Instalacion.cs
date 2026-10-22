using DocumentFormat.OpenXml.Office2010.Excel;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Items;
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

namespace Mikrotik_Administrador.Catalogos
{
    public partial class Instalacion : Form
    {
        public int IdUsuarioM {  get; set; }
        public Instalacion()
        {
            InitializeComponent();
        }

        private async void Instalacion_Load(object sender, EventArgs e)
        {
            AppRepository obj = new AppRepository();
            var listaUsuarios = await obj.GetUsuarios(string.Empty, string.Empty);
            // Insertamos un objeto "fantasma" al inicio para el placeholder
            listaUsuarios.Insert(0, new ListUsuariosModel { Id = 0, Nombre = "Selecciona un tecnico", IdTipo = 3 });

            // Configuramos el ComboBox
            CBInstalador.DisplayMember = "Nombre"; // Lo que el usuario VE
            CBInstalador.ValueMember = "Id";      // El dato que procesas por DETRÁS
            CBInstalador.DataSource = listaUsuarios.Where(x=> x.IdTipo == 3).ToList();
            CBInstalador.SelectedIndex = 0;
            dtpFecha.Value = DateTime.Now;
            var RInstalacion = obj.GetInstalacionbyIdUsuarioM(IdUsuarioM).Result;
            if (RInstalacion != null)
            {
                CBInstalador.SelectedValue = RInstalacion.IdUsuario;
                NUDPuerto.Value = RInstalacion.Puerto;
                txtCaja.Text = RInstalacion.Caja;
                txtSpliter.Text = RInstalacion.Spliter;
                NUDPuertoInterno.Value = RInstalacion.Puerto_Interno;
                dtpFecha.Value = RInstalacion.Fecha_Instalacion;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if((int)CBInstalador.SelectedValue == 0 || 
                txtCaja.Text.Trim() == string.Empty ||
                txtSpliter.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Datos incompletos, favor de llenar toda la información necesaria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            InstalacionModel Ins = new InstalacionModel()
            {
                Id = 0,
                IdUsuarioM = IdUsuarioM,
                IdUsuario = (int)CBInstalador.SelectedValue,
                Caja = txtCaja.Text.Trim(),
                Spliter = txtSpliter.Text.Trim(),
                Puerto = (int)NUDPuerto.Value,
                Puerto_Interno = (int)NUDPuertoInterno.Value,
                Fecha_Instalacion = dtpFecha.Value,
            };
            AppRepository ap = new AppRepository();
            var r = ap.SaveInstalacion(Ins).Result;
            if (r > 0)
            {
                MessageBox.Show("Guardado correctamante", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al guardar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
