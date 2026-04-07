using Mikrotik_Administrador.Class;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador
{
    public partial class WirelessMikrotik : Form
    {
        public int IdMikrotik;
        MK mikrotik;
        List<Address> listaaddress = new List<Address>();
        public WirelessMikrotik()
        {
            InitializeComponent();
        }

        private void WirelessMikrotik_Load(object sender, EventArgs e)
        {

        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            if(dgvWireless.DataSource == null)
            {
                MessageBox.Show("No hay datos para actualizar. Por favor extraiga los datos primero.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            BtnExtraer.Enabled = false; // Deshabilitar el botón para evitar múltiples clics
            BtnActualizar.Enabled = false;
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            try
            {
                AppRepository obj = new AppRepository();
                //if (obj.DesactivarWireless(IdMikrotik).Result == false)
                //{
                //    MessageBox.Show("Error con la comunicacion con wireless.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}
                List<Address> Seleccionados = new List<Address>();
                Seleccionados = dgvWireless.Rows.Cast<DataGridViewRow>()
                 .Where(r => Convert.ToBoolean(r.Cells["cbSeleccionar"].Value))
                  .Select(r => new Address
                  {
                      id = Convert.ToString(r.Cells["id"].Value),
                      comment = Convert.ToString(r.Cells["comment"].Value),
                      address = Convert.ToString(r.Cells["address"].Value),
                      estatus = Convert.ToString(r.Cells["estatus"].Value)
                  })
                   .ToList();

                foreach (var item in Seleccionados)
                {
                    InsertListWirelessModel model = new InsertListWirelessModel
                    {
                        IdMikrotik = IdMikrotik,
                        Address = item.address,
                        Comment = item.comment,
                        Estatus = item.estatus,
                        IdInterno = item.id
                    };
                    if (obj.SaveWireless(model).Result == false)
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
            dgvWireless.Columns.Clear();
            try
            {
                AppRepository obj = new AppRepository();
                MikrotikModel mikro = new MikrotikModel();
                mikro = obj.GetMikrotikById(IdMikrotik).Result;
                if (mikro.Estatus == false)
                {                          
                    MessageBox.Show("El Mikrotik seleccionado está desactivado, por favor activelo para continuar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                mikrotik = new MK(mikro.IP, Convert.ToInt32(mikro.Port));
                // Usamos Task.Run para que la conexión no detenga la ventana
                bool login = await Task.Run(() =>
                {
                    return mikrotik.ConectarYLogin(mikro.Usuario, mikro.Password);
                });
                if (login == false)
                {
                    MessageBox.Show("Error en conexión, revisar que el firewall y nat no esten bloqueando los puertos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var lista = await Task.Run(() => mikrotik.VerAddres());
                if (lista != null && lista.Count > 0)
                {
                    dgvWireless.DataSource = lista;
                    listaaddress = lista;
                    AgregarBotones();
                    BtnActualizar.Enabled = true;
                }

                MessageBox.Show("Carga completa", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo establecer la conexión. Verifique IP y Puertos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (mikrotik != null)
                {
                    await Task.Run(() => mikrotik.Close());
                }
                progressBar1.Style = ProgressBarStyle.Blocks; // Detenemos el movimiento
                progressBar1.Value = 100;
                BtnExtraer.Enabled = true;         
            }
        }

        private void AgregarBotones()
        {
            // Botón Checket
            DataGridViewCheckBoxColumn chkSeleccionar = new DataGridViewCheckBoxColumn();
            chkSeleccionar.Name = "cbSeleccionar";
            chkSeleccionar.HeaderText = "Copiar a Base";
            dgvWireless.Columns.Add(chkSeleccionar);
        }

    }
}