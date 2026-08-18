using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Mikrotik_Administrador.Items
{
    public partial class IniciarPagos : Form
    {
        public int IdMensualidad { get; set; }
        public int IdUsuarioM { get; set; }
        public int IdResponsable { get; set; }
        public IniciarPagos()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            AppRepository obj = new AppRepository();
            obj.GetExistMensualidadProxima(IdUsuarioM, IdMensualidad, dtpFechaInicio.Value, Convert.ToDateTime(lblFechaCorte.Text)).ContinueWith(task =>
            {
                if (task.Result != null)
                {
                    if (task.Result.Count>0)
                    {
                        MessageBox.Show("No se puede ingresar esta fecha, ya hay un registro aproximado a la fecha establecida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            });
            MensualidadModel mensualidad = new MensualidadModel
            {
                Id = IdMensualidad,
                Pagado = false,
                IdUsuarioM = IdUsuarioM,
                FechaInicio = dtpFechaInicio.Value,
                DiaCorte = (int)NUDCorte.Value,
                FechaLimite = Convert.ToDateTime(lblFechaCorte.Text),
                IdUsuario = IdResponsable
            };

            if (obj.SaveMensualidad(mensualidad).Result)
            {
                MessageBox.Show("Mensualidad guardada");
                DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("Error al guardar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
