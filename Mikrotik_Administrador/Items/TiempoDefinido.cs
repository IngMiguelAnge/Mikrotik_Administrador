using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using System;
using System.Windows.Forms;
namespace Mikrotik_Administrador.Items
{
    public partial class TiempoDefinido : Form
    {
        public int IdPlan { get; set; }
        public string Modo { get; set; }
        public int Horas { get; set; }
        public int Dias { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        private bool primera = true;
        public int IdMikrotik { get; set; }
        public TiempoDefinido()
        {
            InitializeComponent();
        }

        private async void TiempoDefinido_Load(object sender, EventArgs e)
        {
            CBModo.SelectedIndex = 0;
            lblTiempo.Text = "Tiempo que desea que dure el plan";
            NUDDias.Value = 8;
            dtpFechaInicio.Value = DateTime.Now;
            dtpFechaInicio.MinDate = DateTime.Now;
            lblFechaFin.Text = string.Empty;
            CambiarFinal();
            AppRepository obj = new AppRepository();
            var listaMikrotiks = await obj.GetMikrotiksByIdPlan(IdPlan);

            // Insertamos un objeto "fantasma" al inicio para el placeholder
            listaMikrotiks.Insert(0, new ListMikrotikModel { Id = 0, Nombre = "Selecciona un Mikrotik" });

            // Configuramos el ComboBox
            CBMikrotiks.DisplayMember = "Nombre"; // Lo que el usuario VE
            CBMikrotiks.ValueMember = "Id";      // El dato que procesas por DETRÁS
            CBMikrotiks.DataSource = listaMikrotiks;
            CBMikrotiks.SelectedIndex = 0;
            primera = false;
        }

        public bool checarfechas()
        {
            if (CBModo.SelectedIndex == 0)
            {
                MessageBox.Show("Debe seleccionar de que forma aplicara el cambio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (CBModo.SelectedIndex != 3 && (NUDDias.Value == 0 && NUDHoras.Value == 0))
            {
                MessageBox.Show("Debe seleccionar al menos un día o una hora para el plan de prueba.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(FechaInicio != null && FechaFin != null)
            {
                if (dtpFechaInicio.Value >= FechaInicio && dtpFechaInicio.Value <= FechaFin)
                {
                    MessageBox.Show("La fecha de inicio seleccionada no es válida. No debe estar entre " + FechaInicio?.ToString("dd/MM/yyyy HH:mm:ss") + " y " + FechaFin?.ToString("dd/MM/yyyy HH:mm:ss"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (Convert.ToDateTime(lblFechaFin.Text) >= FechaInicio && Convert.ToDateTime(lblFechaFin.Text) <= FechaFin)
                {
                    MessageBox.Show("La fecha que termina no es válida. No debe estar entre " + FechaInicio?.ToString("dd/MM/yyyy HH:mm:ss") + " y " + FechaFin?.ToString("dd/MM/yyyy HH:mm:ss"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
       
            Dias = (int)NUDDias.Value;
            Horas = (int)NUDHoras.Value;
            return true;
        }
        public void CambiarFinal()
        {
            if (primera || CBModo.SelectedIndex == 0)
            {
                lblFechaFin.Text = string.Empty;
                return;
            }
            lblFechaFin.Text = dtpFechaInicio.Value.AddDays((int)NUDDias.Value).AddHours((int)NUDHoras.Value).ToString("dd/MM/yyyy HH:mm:ss");
        }
        private void dtpFechaInicio_ValueChanged(object sender, EventArgs e)
        {
            CambiarFinal();
        }

        private void NUDDias_ValueChanged(object sender, EventArgs e)
        {
            CambiarFinal();
        }

        private void NUDHoras_ValueChanged(object sender, EventArgs e)
        {
            CambiarFinal();
        }

        private void CBModo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBModo.SelectedIndex == 0)
            {
                NUDDias.Enabled = false;
                NUDHoras.Enabled = false;
                dtpFechaInicio.Enabled = false;
                lblFechaFin.Text = string.Empty;
            }
            else
            { 
                dtpFechaInicio.Enabled = true;
                if(CBModo.SelectedIndex == 3)
                {
                    NUDDias.Enabled = false;
                    NUDHoras.Enabled = false;
                    lblFechaFin.Text = "Es para plan mensual";
                }
                else
                {
                    NUDDias.Enabled = true;
                    NUDHoras.Enabled = true;
                    CambiarFinal();
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (CBMikrotiks.SelectedIndex == 0)
            {
                MessageBox.Show("Debe seleccionar un Mikrotik", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Modo = CBModo.Text;
            if (!checarfechas())
            {
                return;
            }
            IdMikrotik = (int)CBMikrotiks.SelectedValue;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}