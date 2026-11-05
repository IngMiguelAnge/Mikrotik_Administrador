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

namespace Mikrotik_Administrador.Catalogos
{
    public partial class NoRegistrado : Form
    {
        public int Id { get; set; }
        public NoRegistrado()
        {
            InitializeComponent();
        }

        private void NoRegistrado_Load(object sender, EventArgs e)
        {
            if (Id != 0)
            {
                AppRepository obj = new AppRepository();
                var data = obj.GetDeudoresbyId(Id).Result;
                if (data != null)
                {
                    txtNombre.Text = data.Nombre;
                    txtLatitud.Text = data.Latitud;
                    txtLongitud.Text = data.Longitud;
                    txtNota.Text = data.Nota;
                    CBRecodigo.Checked = data.Recogido;
                }
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if(txtNombre.Text.Trim() == string.Empty )
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DeudoresModel model = new DeudoresModel
            {
                Id = this.Id,
                Nombre = txtNombre.Text.Trim(),
                Latitud = txtLatitud.Text.Trim(),
                Longitud = txtLongitud.Text.Trim(),
                Nota = txtNota.Text.Trim(),
                Recogido = CBRecodigo.Checked
            };
            AppRepository obj = new AppRepository();
            var result = obj.SaveDeudoresbyName(model).Result;
            if(result > 0)
            {
                MessageBox.Show("Guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Ocurrió un error al guardar el registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}