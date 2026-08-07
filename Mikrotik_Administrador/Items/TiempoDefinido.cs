using Microsoft.VisualBasic;
using Mikrotik_Administrador.Class;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Mikrotik_Administrador.Items
{
    public partial class TiempoDefinido : Form
    {
        public string Password { get; set; }
        public int IdPlan { get; set; }
        public string Modo { get; set; }
        public int Horas { get; set; }
        public int Dias { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        private bool primera = true;
        public int IdMikrotik { get; set; }
        public string Programacion { get; set; }
        public string NombrePlan { get; set; }
        MK mikrotik;
        public TiempoDefinido()
        {
            InitializeComponent();
        }

        private async void TiempoDefinido_Load(object sender, EventArgs e)
        {
            
            CBModo.SelectedIndex = 0;
            lblTiempo.Text = "Tiempo que desea que dure:";
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
            if (Programacion != "Cambio de plan")
            {
                CBMikrotiks.Visible = false;
                lblMikrotik.Visible = false;
                CBMikrotiks.SelectedValue = IdMikrotik;
            }
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
            if(CBModo.SelectedIndex == 3 && Programacion == "Suspensión")
            {
                DialogResult resultado = MessageBox.Show("Si selecciona permanente, el usuario sera eliminado del mikrotik. ¿Quiere continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.No)
                {
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

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            Password = string.Empty;
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
            if (IdMikrotik != (int)CBMikrotiks.SelectedValue)
            {
                DialogResult resultado = MessageBox.Show("Esta intentado mover un usuario de un mikrotik a otro. ¿Quiere continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.No)
                {
                    return;
                }
            }
            AppRepository obj = new AppRepository();
            var plan = obj.GetPlanById(IdPlan).Result;
            IdMikrotik = (int)CBMikrotiks.SelectedValue;
            if(plan.IsAntena ==  true)
            {
                var listwireles = await obj.GetWirelessbyIdMikrotik(IdMikrotik);
                if (listwireles.Where(x => x.Estatus == "Activo").ToList().Count() == 0)
                {
                    MessageBox.Show("El mikrotik seleccionado no contiene wireless agregados, favor de completar la informacion del mikrotik antes de continuar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var listacomments = await Task.Run(() => obj.GetCommentsActivos(IdMikrotik));
                if(listacomments.ToList().Count() == 0)
                {
                    MessageBox.Show("El mikrotik seleccionado no contiene comentarios agregados, favor de completar la informacion del mikrotik antes de continuar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                var listPool = await obj.GetPoolsbyIdMikrotik(IdMikrotik);
                if (listPool.Where(x => x.Estatus == "Activo").ToList().Count() == 0)
                {
                    MessageBox.Show("El mikrotik seleccionado no contiene pools agregados, favor de completar la informacion del mikrotik antes de continuar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                PasswordFibra pf = new PasswordFibra();
                pf.ShowDialog();
                Password = pf.Password;

                progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
                progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
                btnGuardar.Enabled = false;
                try
                {
                    //NombrePlan
                    if (mikrotik != null)
                    {
                        await Task.Run(() => mikrotik.Close());
                        mikrotik = null;
                    }
                    MikrotikModel mikro = new MikrotikModel();
                    mikro = obj.GetMikrotikById(IdMikrotik).Result;
                    if (mikro.Estatus == false)
                    {
                        MessageBox.Show("El Mikrotik seleccionado está desactivado, por favor activelo para continuar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    mikrotik = new MK(mikro.IP, Convert.ToInt32(mikro.Port));

                    bool login = await Task.Run(() =>
                    {
                        return mikrotik.ConectarYLogin(mikro.Usuario, mikro.Password);
                    });
                    if (login == false)
                    {
                        MessageBox.Show("Error en conexión, revisar que el firewall y nat no esten bloqueando los puertos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string IdInterno = mikrotik.BuscarPerfil(NombrePlan);
                    if(IdInterno == string.Empty)
                    {
                        MessageBox.Show("Error este plan fue eliminado del mikrotik de manera interna y no fue informado el sistema, favor de revisar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("El mikrotik seleccionado no responde, favor de revisar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    if (mikrotik != null)
                    {
                        await Task.Run(() => mikrotik.Close());
                    }
                    btnGuardar.Enabled = true;
                    progressBar1.Style = ProgressBarStyle.Blocks; // Detenemos el movimiento
                    progressBar1.Value = 100;
                }
               
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }        
    }
}