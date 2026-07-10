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
    public partial class TiempoDefinido : Form
    {
        public string Plan { get; set; }
        public bool Test { get; set; }
        public int Horas { get; set; }
        public int Dias { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public TiempoDefinido()
        {
            InitializeComponent();
        }

        private void TiempoDefinido_Load(object sender, EventArgs e)
        {
            lblTiempo.Text = "Tiempo que desea que dure el plan " + Plan;
            NUDDias.Value = 8;
            dtpFechaInicio.Value = DateTime.Now;
            CambiarFinal();
        }

        private void btnTEST_Click(object sender, EventArgs e)
        {
            Test = true;
            if (!checarfechas())
            {
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCobro_Click(object sender, EventArgs e)
        {
            Test = false;
            if (!checarfechas())
            {
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        public bool checarfechas()
        {
            if (NUDDias.Value == 0 && NUDHoras.Value == 0)
            {
                MessageBox.Show("Debe seleccionar al menos un día o una hora para el plan de prueba.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (dtpFechaInicio.Value >= FechaInicio && dtpFechaInicio.Value <= FechaFin)
            {
                MessageBox.Show("La fecha de inicio seleccionada no es válida. No debe estar entre " + FechaInicio?.ToString("dd/MM/yyyy HH:mm:ss") + " y " + FechaFin?.ToString("dd/MM/yyyy HH:mm:ss"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (Convert.ToDateTime(lblFechaFin.Text) >= FechaInicio && Convert.ToDateTime(lblFechaFin.Text) <= FechaFin)
            {
                MessageBox.Show("La fecha que termina es válida. No debe estar entre " + FechaInicio?.ToString("dd/MM/yyyy HH:mm:ss") + " y " + FechaFin?.ToString("dd/MM/yyyy HH:mm:ss"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            Dias = (int)NUDDias.Value;
            Horas = (int)NUDHoras.Value;       
            return true;
        }
        public void CambiarFinal()
        {
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
    }
}
