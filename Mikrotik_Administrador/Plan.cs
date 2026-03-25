using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador
{
    public partial class Plan : Form
    {
        public int Id { get; set; }
        public Plan()
        {
            InitializeComponent();
        }

        private void Plan_Load(object sender, EventArgs e)
        {
            if (Id != 0)
            {
                AppRepository obj = new AppRepository();
                var Plan = obj.GetPlanById(Id).Result;
                txtNombre.Text = Plan.Nombre;
                NUDPrecio.Text = Convert.ToString(Plan.Precio);
                string[] Velocidad = Plan.Velocidad.Split('/');
                // Primera parte (ej. 12M)
                string num1 = Regex.Match(Velocidad[0], @"\d+").Value;
                string uni1 = Regex.Match(Velocidad[0], @"[a-zA-Z]+").Value;

                // Segunda parte (ej. 1k)
                string num2 = Regex.Match(Velocidad[1], @"\d+").Value;
                string uni2 = Regex.Match(Velocidad[1], @"[a-zA-Z]+").Value;

                NUDSubida.Text = num1;
                cbSubida.SelectedItem = uni1;
                NUDDescarga.Text = num2;
                CBDescarga.SelectedItem = uni2;
                if (Plan.IsAntena)
                {

                    CBPerteneceA.SelectedItem = "Antena";
                }
                else
                    CBPerteneceA.SelectedItem = "Fibra";
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Trim() == string.Empty || Convert.ToString(CBPerteneceA.SelectedItem) == string.Empty ||
                Convert.ToString(cbSubida.SelectedItem) == string.Empty ||
                Convert.ToString(CBDescarga.SelectedItem) == string.Empty)
            {
                MessageBox.Show("Datos incompletos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AppRepository obj = new AppRepository();
            PlanModel plan = new PlanModel();
            plan.Id = Id;
            plan.Nombre = txtNombre.Text.Trim();
            plan.IsAntena = (string)CBPerteneceA.SelectedItem == "Antena"? true:false;
            plan.Precio = NUDPrecio.Value;
            plan.Velocidad = Convert.ToString(NUDSubida.Value) + cbSubida.SelectedItem + 
                "/" + Convert.ToString(NUDDescarga.Value) + CBDescarga.SelectedItem;
            if (obj.SavePlan(plan).Result == true)
            {
                MessageBox.Show("Guardado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }

            MessageBox.Show("Error al guardar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
