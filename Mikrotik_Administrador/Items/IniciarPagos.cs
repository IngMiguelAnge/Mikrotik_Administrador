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
    public partial class IniciarPagos : Form
    {
        public DateTime FechaInicio = DateTime.Now;
        public IniciarPagos()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            FechaInicio = dtpFecha.Value;
            this.Close();
        }
    }
}
