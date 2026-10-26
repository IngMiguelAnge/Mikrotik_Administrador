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
    public partial class Programar : Form
    {
        public List<ListCommentsModel> listComments { get; set; }
        public string SePrograma {  get; set; }
        public string Excel { get; set; } = string.Empty;
        public Programar()
        {
            InitializeComponent();
        }

        private void Programar_Load(object sender, EventArgs e)
        {
            if(listComments != null && listComments.Count > 0)
            {
                listComments.Insert(0, new ListCommentsModel { Id = 0, Nombre = "Selecciona un comment", Estatus = "Activo" });

                // Configuramos el ComboBox
                CBAccion.DisplayMember = "Nombre"; // Lo que el usuario VE
                CBAccion.ValueMember = "Id";      // El dato que procesas por DETRÁS
                CBAccion.DataSource = listComments.ToList();
            }
            if (Excel != string.Empty)
            {
                CBAccion.Items.Clear();
                CBAccion.Items.Add("Seleccione");
                CBAccion.Items.Add("Mensualidades");
                CBAccion.Items.Add("Deudores");
            }
            CBAccion.SelectedIndex = 0;
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if (CBAccion.SelectedIndex == 0)
            {
                MessageBox.Show("Debe seleccionar una opción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SePrograma = CBAccion.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
