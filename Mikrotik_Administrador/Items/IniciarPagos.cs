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
        public bool Guardar = false;
        public IniciarPagos()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            FechaInicio = dtpFechaInicio.Value;
            Guardar = true;
            this.Close();
        }

        private void IniciarPagos_Load(object sender, EventArgs e)
        {
            dtpFechaInicio.Value = DateTime.Now;
            DateTime fechaCorte = new DateTime(DateTime.Now.AddMonths(1).Year,
                DateTime.Now.AddMonths(1).Month, 1);
            lblFechaCorte.Text = fechaCorte.ToString("dd/MM/yyyy");
        }

        private void NUDCorte_ValueChanged(object sender, EventArgs e)
        {
            CambiarFecha();
        }

        private void dtpFechaInicio_ValueChanged(object sender, EventArgs e)
        {
            CambiarFecha();
        }
        public void CambiarFecha()
        {
            int diaDeseado = (int)NUDCorte.Value;

            // 2. Obtén la fecha del próximo mes
            DateTime proximoMes = dtpFechaInicio.Value.AddMonths(1);

            // 3. Obtén el máximo de días que tiene ese próximo mes
            int diasEnElMes = DateTime.DaysInMonth(proximoMes.Year, proximoMes.Month);

            // 4. Si el día deseado es mayor que los días del mes, usa el último día disponible
            int diaFinal = Math.Min(diaDeseado, diasEnElMes);

            // 5. Crea la fecha final y asígnala al Label
            DateTime fechaCorte = new DateTime(proximoMes.Year, proximoMes.Month, diaFinal);
            lblFechaCorte.Text = fechaCorte.ToString("dd/MM/yyyy");
        }
    }
}
