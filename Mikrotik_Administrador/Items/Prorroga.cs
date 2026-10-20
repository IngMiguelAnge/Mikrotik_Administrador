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

namespace Mikrotik_Administrador.Items
{
    public partial class Prorroga : Form
    {
        public int Id { get; set; }
        public int IdUsuarioM { get; set; }
        public Prorroga()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ProrrogaModel model = new ProrrogaModel()
            {
                Id= Id,
                Fecha = dtpFechaInicio.Value,
                IdUsuarioM = IdUsuarioM,
            };
            AppRepository obj = new AppRepository();
            var r = obj.SaveProrroga(model);
            MessageBox.Show("Prorroga guardada");
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Prorroga_Load(object sender, EventArgs e)
        {
            if(Id != 0)
            {
                AppRepository obj = new AppRepository();
                var prorroga = obj.GetProrrogasbyId(Id).Result;
                dtpFechaInicio.Value = prorroga.Fecha;
            }               
        }
    }
}
