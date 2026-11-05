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
    public partial class Deuda : Form
    {
        public int Id { get; set; }
        public int IdDeudor { get; set; }
        public Deuda()
        {
            InitializeComponent();
        }

        private void Deuda_Load(object sender, EventArgs e)
        {
            if (Id != 0)
            {
                AppRepository obj = new AppRepository();
                var data = obj.GetDeudabyId(Id).Result;
                if (data != null)
                {
                    NUDCantidad.Value = data.Cantidad;
                    NUDYear.Value = data.Year;
                    CBMes.SelectedItem = data.Mes;
                }
            }
            else
                CBMes.SelectedIndex = 0;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (CBMes.SelectedIndex == 0)
            {
                MessageBox.Show("Por favor, seleccione un mes.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DeudaModel model = new DeudaModel
            {
                Id = this.Id,
                Cantidad = NUDCantidad.Value,
                Year = (int)NUDYear.Value,
                Mes = CBMes.SelectedItem.ToString(),
                Estatus = true,
                IdDeudor = IdDeudor
            };
            AppRepository obj = new AppRepository();
            var result = obj.SaveDeduda(model).Result;
            if (result > 0)
            {
                MessageBox.Show("Guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Ocurrió un error al guardar la deuda.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
