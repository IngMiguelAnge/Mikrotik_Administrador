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
    public partial class Banco : Form
    {
        public int Id {  get; set; }
        public Banco()
        {
            InitializeComponent();
        }

        private void Banco_Load(object sender, EventArgs e)
        {
            cbTipo.SelectedIndex = 0;
            if (Id == 0) return;
            AppRepository obj = new AppRepository();
            var Banco = obj.GetBancobyId(Id).Result;
            txtNombre.Text = Banco.Nombre;
            cbTipo.Text = Banco.Tipo;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || cbTipo.SelectedIndex <= 0)
            {
                MessageBox.Show("Datos incompletos revise la información", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            AppRepository obj = new AppRepository();
            List<ListBancosModel> exist = obj.GetBancos(txtNombre.Text.Trim(),string.Empty).Result;
            if (exist.Where(x => x.Id != Id).Count() > 0)
            {
                MessageBox.Show("Ya existe un banco con ese nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (obj.SaveBanco(Id, txtNombre.Text.Trim(),cbTipo.Text).Result)
            {
                MessageBox.Show("Banco guardado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else MessageBox.Show("Error al guardar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
