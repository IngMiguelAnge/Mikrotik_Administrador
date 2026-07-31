using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador.Items
{
    public partial class Programar : Form
    {
        public string SePrograma {  get; set; }
        public Programar()
        {
            InitializeComponent();
        }

        private void Programar_Load(object sender, EventArgs e)
        {
            CBAccion.SelectedIndex = 0;
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if (CBAccion.SelectedIndex == 0)
            {
                MessageBox.Show("Seleccione una acción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SePrograma = CBAccion.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
