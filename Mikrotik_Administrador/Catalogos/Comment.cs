using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using System.Windows.Forms;

namespace Mikrotik_Administrador.Items
{
    public partial class Comment : Form
    {
        public int Id { get; set; }
        public string CommitText { get; set; }
        public int IdMikrotik { get; set; }
        public string Permitido { get; set; }
        public Comment()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, System.EventArgs e)
        {
            if(CBMikrotiks.SelectedIndex == 0)
            {
                MessageBox.Show("Debe seleccionar un Mikrotik", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cbPermitidos.SelectedIndex == 0)
            {
                MessageBox.Show("Debe seleccionar el tipo de comment", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            AppRepository m = new AppRepository();
            bool Permitido = cbPermitidos.Text == "Permitido" ? true : false;

            if (m.SaveComment(Id, txtComment.Text, (int)CBMikrotiks.SelectedValue, Permitido).Result != 0)
            {
                MessageBox.Show("Comment guardado");
                this.Close();
            }
            else
                MessageBox.Show("Error al actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private async void Comment_Load(object sender, System.EventArgs e)
        {
            AppRepository obj = new AppRepository();
            var listaMikrotiks = await obj.GetMikrotiks();

            // Insertamos un objeto "fantasma" al inicio para el placeholder
            listaMikrotiks.Insert(0, new ListMikrotikModel { Id = 0, Nombre = "Selecciona un Mikrotik" });

            // Configuramos el ComboBox
            CBMikrotiks.DisplayMember = "Nombre"; // Lo que el usuario VE
            CBMikrotiks.ValueMember = "Id";      // El dato que procesas por DETRÁS
            CBMikrotiks.DataSource = listaMikrotiks;
            CBMikrotiks.SelectedIndex = 0;
            cbPermitidos.SelectedIndex = 0;
            if(IdMikrotik != 0)
            {
                CBMikrotiks.SelectedValue = IdMikrotik;
                txtComment.Text = CommitText;
                cbPermitidos.Text = Permitido;
            }
        }
    }
}
