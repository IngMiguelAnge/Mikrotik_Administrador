using Mikrotik_Administrador.Class;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador
{
    public partial class InfoMikrotik : Form
    {
        public int IdMikrotik = 0;
        public bool limite_alcanzado = false;
        MK mikrotik;// = new MK("172.18.1.254", 8728);
        public InfoMikrotik()
        {
            InitializeComponent();
        }

        private async void btnProbar_Click(object sender, EventArgs e)
        {
            btnProbar.Enabled = false; // Bloqueamos el botón para evitar múltiples clics
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            try
            {
                mikrotik = new MK(txtIP.Text.ToString(), Convert.ToInt32(this.txtPort.Text));
                // Usamos Task.Run para que la conexión no detenga la ventana
                bool login = await Task.Run(() => {
                    // Aquí dentro va la lógica pesada que antes congelaba todo
                    return mikrotik.ConectarYLogin(txtUsuario.Text, txtPassword.Text);
                });
                if (login == true)
                {
                    lblProbar.Text = "Conexión exitosa";
                    MessageBox.Show("Conexión exitosa", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lblProbar.Text = "Error en la conexión";
                    MessageBox.Show("Error en conexión, revisar que el firewall y nat no esten bloqueando los puertos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                lblProbar.Text = "Error en la conexión";
                MessageBox.Show("No se pudo establecer la conexión. Verifique IP y Puertos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 0;
                btnProbar.Enabled = true; // Rehabilitamos el botón
            }
        }

        private void InfoMikrotik_Load(object sender, EventArgs e)
        {
            lblProbar.Text = "Sin conexión";
            if (IdMikrotik != 0)
            {
                AppRepository obj = new AppRepository();
                var mikrotik = obj.GetMikrotikById(IdMikrotik).Result;
                txtNombre.Text = mikrotik.Nombre;
                txtIP.Text = mikrotik.IP;
                txtPort.Text = mikrotik.Port;
                txtUsuario.Text = mikrotik.Usuario;
                txtPassword.Text = mikrotik.Password;
                limite_alcanzado = mikrotik.Limite_Alcanzado;
                if (mikrotik.Estatus == true)
                    lblProbar.Text = "Conexión exitosa";
                else
                    lblProbar.Text = "Sin conexión";
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!System.Net.IPAddress.TryParse(txtIP.Text, out _))
            {
                MessageBox.Show("Por favor, corrige la dirección IP antes de guardar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtIP.Focus();
                return;
            }
            if (txtIP.Text.Trim() == "..." || txtPassword.Text == string.Empty
              || txtPort.Text == string.Empty || txtUsuario.Text == string.Empty)
            {
                MessageBox.Show("Se requiere probar la conexión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool Estatus = true;
            if (lblProbar.Text != "Conexión exitosa")
            {
                DialogResult resultado = MessageBox.Show("¿Deseas guardar los cambios, la conexión no se encuentra como exitosa?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    Estatus = false;
                }
                else
                {
                    return;
                }
            }
            AppRepository obj = new AppRepository();
            MikrotikModel mikrotik = new MikrotikModel();
            mikrotik.Nombre = txtNombre.Text;
            mikrotik.IP = txtIP.Text.Replace(" ", ""); ;
            mikrotik.Port = txtPort.Text.Replace(" ", ""); ;
            mikrotik.Usuario = txtUsuario.Text;
            mikrotik.Password = txtPassword.Text;
            mikrotik.Id = IdMikrotik;
            mikrotik.Estatus = Estatus;
            mikrotik.Limite_Alcanzado = limite_alcanzado;
            if (obj.InsertandUpdateMikrotik(mikrotik).Result == true)
            {
                MessageBox.Show("Guardado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }

            MessageBox.Show("Error al guardar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void txtPort_TextChanged(object sender, EventArgs e)
        {
            lblProbar.Text = "Sin conexión";
        }

        private void txtIP_TextChanged(object sender, EventArgs e)
        {
            lblProbar.Text = "Sin conexión";
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            lblProbar.Text = "Sin conexión";
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            lblProbar.Text = "Sin conexión";
        }

        private void txtIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permite números, puntos y la tecla Backspace (borrar)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // No permite un punto si ya hay 3 puntos en la IP
            if (e.KeyChar == '.' && (sender as TextBox).Text.Count(c => c == '.') >= 3)
            {
                e.Handled = true;
            }
        }

        private void txtIP_Validating(object sender, CancelEventArgs e)
        {
            string input = txtIP.Text.Trim();

            // Si el campo es obligatorio y está vacío
            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("La dirección IP es obligatoria.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true; // No deja salir del TextBox
                return;
            }

            // Validar si el formato es de una IP real (0.0.0.0 a 255.255.255.255)
            if (!System.Net.IPAddress.TryParse(input, out System.Net.IPAddress address) || address.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
            {
                MessageBox.Show("El formato de IP no es válido. Ejemplo: 192.168.1.1", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true; // No deja salir hasta que se corrija
            }
        }
    }
}