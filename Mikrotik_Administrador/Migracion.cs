using Microsoft.IdentityModel.Tokens;
using Mikrotik_Administrador.Class;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador
{
    public partial class Migracion : Form
    {
        MK mikrotik;
        bool IsAntena = false;
        public Migracion()
        {
            InitializeComponent();
        }

        private async void Migracion_Load(object sender, EventArgs e)
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
        }

        private async void BtnBuscar_Click(object sender, EventArgs e)
        {
            if (CBMikrotiks.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Por favor, selecciona un Mikrotik.");
                return;
            }
            if (txtNombre.Text.Trim() == "")
            {
                DialogResult resultado = MessageBox.Show("Ha dejado el campo vacio, esto buscara a todos los usuarios pero puede demorar ¿Quiere continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.No)
                {
                    return;
                }
            }
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            BtnBuscar.Enabled = false; // Deshabilitar el botón para evitar múltiples clics
            btnExportar.Enabled = false; 
            dgvUsuarios.DataSource = null;
            dgvUsuarios.Columns.Clear(); // Limpiar columnas anteriores
            int IdMikrotik = (int)CBMikrotiks.SelectedValue;
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
                    // Aquí dentro va la lógica pesada que antes congelaba todo
                    return mikrotik.ConectarYLogin(mikro.Usuario, mikro.Password);
                });
                if (login == false)
                {
                    MessageBox.Show("Error en conexión, revisar que el firewall y nat no esten bloqueando los puertos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (cbAntenas.Checked == true)
                {
                    IsAntena = true;
                    var listaddress = await obj.GetWirelessbyIdMikrotik(IdMikrotik);

                    if (listaddress == null || listaddress.Count == 0)
                    {
                        MessageBox.Show("No se ha guardado el Wireless del Mikrotik seleccionado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    var lista = await Task.Run(() => mikrotik.VerAntenas(txtNombre.Text, listaddress).ToList());
                    dgvUsuarios.DataSource = lista != null && lista.Count > 0 ? lista : null;
                    if (lista == null || lista.Count == 0)
                    {
                        MessageBox.Show("No se encontraron usuarios en el Mikrotik seleccionado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        AgregarBotones();
                        MessageBox.Show("Carga completa", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    IsAntena = false;
                    var lista = await Task.Run(() => mikrotik.VerFibra(txtNombre.Text).ToList());
                    dgvUsuarios.DataSource = lista != null && lista.Count > 0
                      ? lista : null;
                    if (lista == null || lista.Count == 0)
                    {
                        MessageBox.Show("No se encontraron usuarios en el Mikrotik seleccionado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        AgregarBotones();
                        MessageBox.Show("Carga completa", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (mikrotik != null)
                {
                    await Task.Run(() => mikrotik.Close());
                }
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 0;
                BtnBuscar.Enabled = true;
                btnExportar.Enabled = true;
            }
        }
        private void AgregarBotones()
        {
            // Botón Checket
            DataGridViewCheckBoxColumn chkSeleccionar = new DataGridViewCheckBoxColumn();
            chkSeleccionar.Name = "cbSeleccionar";
            chkSeleccionar.HeaderText = "Copiar a Base";
            dgvUsuarios.Columns.Add(chkSeleccionar);
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            int cantidadExportada = 0;
            int cantidadNoExportada = 0;
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            btnExportar.Enabled = false; 
            BtnBuscar.Enabled = false; 
            try
            {
                List<UsuariosExtraidosModel> Seleccionados = new List<UsuariosExtraidosModel>();
                Seleccionados = dgvUsuarios.Rows.Cast<DataGridViewRow>()
                 .Where(r => cbExportar.Checked || Convert.ToBoolean(r.Cells["cbSeleccionar"].Value))
                  .Select(r => new UsuariosExtraidosModel
                  {
                      id = Convert.ToString(r.Cells["id"].Value),
                      comment = Convert.ToString(r.Cells["comment"].Value),
                      address = Convert.ToString(r.Cells["address"].Value),
                      estatus = Convert.ToString(r.Cells["estatus"].Value),
                      velocidad = Convert.ToString(r.Cells["velocidad"].Value)
                  })
                   .ToList();

                if (Seleccionados.Count == 0)
                {
                    MessageBox.Show("No has seleccionado ningún usuario para exportar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    int IdMikrotik = (int)CBMikrotiks.SelectedValue;
                    if (IdMikrotik == 0)
                    {
                        MessageBox.Show("Selecciona un Mikrotik válido de la lista.");
                        return;
                    }
                    AppRepository obj = new AppRepository();
                    foreach (UsuariosExtraidosModel item in Seleccionados)
                    {
                        if(string.IsNullOrEmpty(item.velocidad))
                        {
                            cantidadNoExportada++;
                            continue; // Saltamos este usuario y pasamos al siguiente
                        }
                        PlanModel objPlan = new PlanModel();
                        objPlan.Velocidad = item.velocidad.Trim();
                        objPlan.IsAntena = cbAntenas.Checked;
                        var result = obj.SavePlanByMigracion(objPlan);
                        if(result.Result == 0)
                        {
                            MessageBox.Show("Error al guardar los planes","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                        UsuariosGeneralModel objuser = new UsuariosGeneralModel();
                        objuser.IdMikrotik = IdMikrotik;
                        objuser.Nombre = item.comment;
                        objuser.Address = item.address;
                        objuser.Antena = IsAntena;
                        objuser.IdInterno = item.id;
                        objuser.Estatus = item.estatus;
                        objuser.Id = 0;
                        objuser.IdPlan = result.Result;
                        var res = obj.SaveUsuariosGeneral(objuser).Result;
                        if (res)
                            cantidadExportada++;
                        else
                            cantidadNoExportada++;
                    }
                }
                MessageBox.Show("Usuarios exportados: " + cantidadExportada.ToString() + "\nUsuarios no exportados (Revisar que tengan una velocidad): " + cantidadNoExportada.ToString(), "Resultado de Exportación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar1.Style = ProgressBarStyle.Blocks; 
                progressBar1.Value = 100;
                BtnBuscar.Enabled = true;
                btnExportar.Enabled = true;
            }
        }
    }
}
