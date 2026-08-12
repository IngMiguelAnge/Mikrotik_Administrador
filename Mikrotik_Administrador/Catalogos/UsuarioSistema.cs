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
using System.Xml.Linq;

namespace Mikrotik_Administrador.Catalogos
{
    public partial class UsuarioSistema : Form
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public int IdTipo { get; set; }
        public UsuarioSistema()
        {
            InitializeComponent();
        }

        private async void UsuarioSistema_Load(object sender, EventArgs e)
        {
            txtNombre.Text = Nombre;
            txtPassword.Text = Password;
            txtUsuario.Text = Usuario;
            AppRepository obj = new AppRepository();
            var listaTipo = await obj.GetTipos();

            // Insertamos un objeto "fantasma" al inicio para el placeholder
            listaTipo.Insert(0, new ListTiposUsuarioModel { Id = 0, TipoUsuario = "Selecciona un tipo" });

            // Configuramos el ComboBox
            CBTipo.DisplayMember = "TipoUsuario"; // Lo que el usuario VE
            CBTipo.ValueMember = "Id";      // El dato que procesas por DETRÁS
            CBTipo.DataSource = listaTipo;
            CBTipo.SelectedIndex = 0;
            if (Id != 0)
            {
                CBTipo.SelectedValue = IdTipo;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(txtNombre.Text.Trim() == string.Empty || txtPassword.Text.Trim() == string.Empty || txtUsuario.Text.Trim() == string.Empty || CBTipo.SelectedIndex == 0)
            {
                MessageBox.Show("Debe llenar todos los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            UserModel us = new UserModel
            {
                Id=Id,
                Usuario = txtUsuario.Text,
                Password = txtPassword.Text,
                IdTipoUsuario = (int)CBTipo.SelectedValue,
                Estatus = true
            };
            AppRepository m = new AppRepository();
            var result = m.SaveUsuario(us,txtNombre.Text.Trim());
            if (result.Result)
            {
                MessageBox.Show("Usuario guardado correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al guardar el usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
