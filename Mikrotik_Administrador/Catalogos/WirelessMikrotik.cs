using Mikrotik_Administrador.Class;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador
{
    public partial class WirelessMikrotik : Form
    {
        public int IdMikrotik;
        MK mikrotik;
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
                List<Address> Seleccionados = new List<Address>();
                Seleccionados = dgvWireless.Rows.Cast<DataGridViewRow>()
                 .Where(r => Convert.ToBoolean(r.Cells["chkSeleccionar"].Value))
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

        public void CrearGridView()
        {
            dgvWireless.Columns.Clear();
            dgvWireless.AutoGenerateColumns = false;
            dgvWireless.EnableHeadersVisualStyles = false;
            // --- ESTILO DE LOS TÍTULOS (HEADERS) CON TU AZUL LOGO ---
            dgvWireless.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            dgvWireless.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvWireless.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            dgvWireless.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dgvWireless.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            dgvWireless.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);

            dgvWireless.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "id",
                HeaderText = "Id",
                DataPropertyName = "id",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvWireless.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "address",
                HeaderText = "IP",
                DataPropertyName = "address",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvWireless.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "comment",
                HeaderText = "Comment",
                DataPropertyName = "comment",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvWireless.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "estatus",
                HeaderText = "Estatus",
                DataPropertyName = "estatus",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DataGridViewCheckBoxColumn chkSeleccionar = new DataGridViewCheckBoxColumn
            {
                Name = "chkSeleccionar",
                HeaderText = "Acción",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                TrueValue = true,
                FalseValue = false,
                IndeterminateValue = false
            };
            dgvWireless.Columns.Add(chkSeleccionar);
            dgvWireless.AllowUserToAddRows = false;
        }
        private async void BtnExtraer_Click(object sender, EventArgs e)
        {
            CrearGridView();
            BtnExtraer.Enabled = false; // Deshabilitar el botón para evitar múltiples clics
            BtnActualizar.Enabled = false;
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
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
                var listaFinal = lista?.ToList() ?? new List<Address>();
                dgvWireless.DataSource = new BindingList<Address>(listaFinal);
                if (lista != null && lista.Count > 0)
                {
                    BtnActualizar.Enabled = true;
                }
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

    }
}