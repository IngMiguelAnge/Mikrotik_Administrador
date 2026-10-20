using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using System;
using System.Windows.Forms;

namespace Mikrotik_Administrador.Items
{
    public partial class VentanaCombo : Form
    {
        public int IdPlan { get; set; }
        public int IdMikrotik { get; set; }
        public string Nombre { get; set; }
        public bool IsExtra { get; set; } = false;
        public VentanaCombo()
        {
            InitializeComponent();
        }

        private async void MikrotiksDisponibles_Load(object sender, EventArgs e)
        {
            AppRepository obj = new AppRepository();   
            if (IsExtra == false)
            {
                CBOpciones.Items.Clear();
                var listaMikrotiks = await obj.GetMikrotiksByIdPlan(IdPlan, false);
                // Insertamos un objeto "fantasma" al inicio para el placeholder
                listaMikrotiks.Insert(0, new ListMikrotikModel { Id = 0, Nombre = "Selecciona un Mikrotik" });

                // Configuramos el ComboBox
                CBOpciones.DisplayMember = "Nombre"; // Lo que el usuario VE
                CBOpciones.ValueMember = "Id";      // El dato que procesas por DETRÁS
                CBOpciones.DataSource = listaMikrotiks;
                CBOpciones.SelectedIndex = 0;
            }
            else
            {
                lblMensaje.Text = "Seleccione un estatus";
                CBOpciones.SelectedIndex = 0;
            }
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if(CBOpciones.SelectedIndex == 0)
            {
                MessageBox.Show("Por favor, selecciona un Mikrotik antes de continuar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            IdMikrotik = IsExtra == false ? (int)CBOpciones.SelectedValue:0;
            Nombre = CBOpciones.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
