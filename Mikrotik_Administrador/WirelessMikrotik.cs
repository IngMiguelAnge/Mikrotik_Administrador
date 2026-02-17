using Mikrotik_Administrador.Class;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador
{
    public partial class WirelessMikrotik : Form
    {
        public int Id_Mikrotik;
        MK mikrotik;
        List<Address> lista = new List<Address>();
        public WirelessMikrotik()
        {
            InitializeComponent();
        }

        private void WirelessMikrotik_Load(object sender, EventArgs e)
        {

        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            BtnExtraer.Enabled = false; // Deshabilitar el botón para evitar múltiples clics
            BtnActualizar.Enabled = false;
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            try
            {
                AppRepository obj = new AppRepository();
                if (obj.DesactivarWireless(Id_Mikrotik).Result == false)
                {
                    MessageBox.Show("Error con la comunicacion con wireless.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                foreach (var item in lista)
                {
                    InsertListWirelessModel model = new InsertListWirelessModel
                    {
                        Id_Mikrotik = Id_Mikrotik,
                        Address = item.address,
                        Network = item.network,
                        Interface = item.@interface,
                        Actual_Interface = item.actual_interface,
                        Comment = item.comment,
                        Disabled = item.disabled,
                        Id_Interno = item.id
                    };
                    if (obj.InsertandUpdateWireless(model).Result == false)
                    {
                        MessageBox.Show("Error al actualizar wireless. id: " + item.id, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                MessageBox.Show("Guardado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo establecer la conexión. Verifique IP y Puertos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar1.Style = ProgressBarStyle.Blocks; // Detenemos el movimiento
                progressBar1.Value = 100;
                BtnExtraer.Enabled = true; // Rehabilitamos el botón
                BtnActualizar.Enabled = true;
            }
        }

        private async void BtnExtraer_Click(object sender, EventArgs e)
        {
            BtnExtraer.Enabled = false; // Deshabilitar el botón para evitar múltiples clics
            BtnActualizar.Enabled = false;
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            dgvWireless.DataSource = null;
            try
            {
                AppRepository obj = new AppRepository();
                MikrotikModel mikro = new MikrotikModel();
                mikro = obj.GetMikrotikById(Id_Mikrotik).Result;
                if (mikro.Estatus == false)
                {                          
                    MessageBox.Show("El Mikrotik seleccionado está desactivado, por favor activelo para continuar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                mikrotik = new MK(mikro.IP, Convert.ToInt32(mikro.Port));
                // Usamos Task.Run para que la conexión no detenga la ventana
                bool login = await Task.Run(() =>
                {
                    // Aquí dentro va la lógica pesada que antes congelaba todo
                    return mikrotik.ConectarYLogin(mikro.Usuario, mikro.Password);
                });
                if (login == false)
                {
                    MessageBox.Show("Error en conexión, revisar que el firewall y nat no esten bloqueando los puertos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                lista = mikrotik.VerAddres();
                if (lista != null && lista.Count > 0)
                {
                    dgvWireless.DataSource = lista;
                    BtnActualizar.Enabled = true;
                }
                else BtnActualizar.Enabled = false;

                MessageBox.Show("Carga completa correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo establecer la conexión. Verifique IP y Puertos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar1.Style = ProgressBarStyle.Blocks; // Detenemos el movimiento
                progressBar1.Value = 100;
                BtnExtraer.Enabled = true; // Rehabilitamos el botón
            }
        }
    }
}