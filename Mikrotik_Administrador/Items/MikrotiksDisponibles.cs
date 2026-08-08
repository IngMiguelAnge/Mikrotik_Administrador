using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using System;
using System.Windows.Forms;

namespace Mikrotik_Administrador.Items
{
    public partial class MikrotiksDisponibles : Form
    {
        public int IdPlan { get; set; }
        public int IdMikrotik { get; set; }
        public string Nombre { get; set; }
        public MikrotiksDisponibles()
        {
            InitializeComponent();
        }

        private async void MikrotiksDisponibles_Load(object sender, EventArgs e)
        {
            AppRepository obj = new AppRepository();
            var listaMikrotiks = await obj.GetMikrotiksByIdPlan(IdPlan);
            // Insertamos un objeto "fantasma" al inicio para el placeholder
            listaMikrotiks.Insert(0, new ListMikrotikModel { Id = 0, Nombre = "Selecciona un Mikrotik" });

            // Configuramos el ComboBox
            CBMikrotiks.DisplayMember = "Nombre"; // Lo que el usuario VE
            CBMikrotiks.ValueMember = "Id";      // El dato que procesas por DETRÁS
            CBMikrotiks.DataSource = listaMikrotiks;
            CBMikrotiks.SelectedIndex = 0;
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if(CBMikrotiks.SelectedIndex == 0)
            {
                MessageBox.Show("Por favor, selecciona un Mikrotik antes de continuar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            IdMikrotik = (int)CBMikrotiks.SelectedValue;
            Nombre = CBMikrotiks.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
