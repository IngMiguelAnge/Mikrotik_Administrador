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
    public partial class PagarDeudores : Form
    {
        public int IdHistorial { get; set; }
        public int IdResponsable { get; set; }
        public string Cliente { get; set; }
        public int IdDeudor { get; set; }
        public PagarDeudores()
        {
            InitializeComponent();
        }

        private void PagarDeudores_Load(object sender, EventArgs e)
        {

        }
    }
}
