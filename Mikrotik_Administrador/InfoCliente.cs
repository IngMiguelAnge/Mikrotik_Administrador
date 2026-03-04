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
    public partial class InfoCliente : Form
    {
        public int IdCliente { get; set; }
        public InfoCliente()
        {
            InitializeComponent();
        }

        private void InfoCliente_Load(object sender, EventArgs e)
        {
            if (IdCliente != 0)
            {
                AppRepository obj = new AppRepository();
                var cliente = obj.GetClienteById(IdCliente).Result;
                txtNombre.Text = cliente.Nombre;
                txtCorreo.Text = cliente.Correo;
                txtTelefono1.Text = cliente.Telefono1;
                txtTelefono2.Text = cliente.Telefono2;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Se requiere un nombre para el cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AppRepository obj = new AppRepository();
            ClienteModel cliente = new ClienteModel();
            cliente.Id = IdCliente;
            cliente.Nombre = txtNombre.Text.Trim();
            cliente.Correo = txtCorreo.Text.Trim() ;
            cliente.Telefono1 = txtTelefono1.Text.Trim();
            cliente.Telefono2 = txtTelefono2.Text.Trim();
            if (obj.InsertAndUpdateCliente(cliente).Result == true)
            {
                MessageBox.Show("Guardado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }

            MessageBox.Show("Error al guardar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
