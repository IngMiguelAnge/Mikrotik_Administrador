using GMap.NET.MapProviders;
using Mikrotik_Administrador.Class;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador
{
    public partial class Plan : Form
    {
        public int Id { get; set; }
        MK mikrotik;
        public Plan()
        {
            InitializeComponent();
        }

        private async void Plan_Load(object sender, EventArgs e)
        {
            AppRepository obj = new AppRepository();
            var lista = await obj.GetMikrotiksActivos(string.Empty);
            if (lista.Count() == 0)
            {
                MessageBox.Show("No hay Mikrotiks activos, por favor registre uno antes de crear un plan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            if (Id != 0)
            {
                var conteo = await obj.GetCountUsuariosByPlan(Id);
                if (conteo > 0)
                {
                    MessageBox.Show("Este plan cuenta con usuarios: \n " +
                        "Al guardar se demorara en asignar la información a todos los usuarios", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CBPerteneceA.Enabled = false;
                    lblCantidadenPlan.Text = conteo.ToString();
                }
                var Plan = obj.GetPlanById(Id).Result;
                txtNombre.Text = Plan.Nombre;
                NUDPrecio.Text = Convert.ToString(Plan.Precio);
                string[] Velocidad = Plan.Velocidad.Split('/');
                // Primera parte (ej. 12M)
                string num1 = Regex.Match(Velocidad[0], @"\d+").Value;
                string uni1 = Regex.Match(Velocidad[0], @"[a-zA-Z]+").Value;

                // Segunda parte (ej. 1k)
                string num2 = Regex.Match(Velocidad[1], @"\d+").Value;
                string uni2 = Regex.Match(Velocidad[1], @"[a-zA-Z]+").Value;

                NUDSubida.Text = num1;
                cbSubida.SelectedItem = uni1;
                NUDDescarga.Text = num2;
                CBDescarga.SelectedItem = uni2;
                if (Plan.IsAntena)
                {

                    CBPerteneceA.SelectedItem = "Antena";
                }
                else
                    CBPerteneceA.SelectedItem = "Fibra";
            }
        }

        private async void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Trim() == string.Empty || Convert.ToString(CBPerteneceA.SelectedItem) == string.Empty ||
                Convert.ToString(cbSubida.SelectedItem) == string.Empty ||
                Convert.ToString(CBDescarga.SelectedItem) == string.Empty)
            {
                MessageBox.Show("Datos incompletos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // 1. Convertimos la subida a una unidad base (kB)
            double totalSubida = (double)NUDSubida.Value;
            if (Convert.ToString(cbSubida.SelectedItem) == "M")
            {
                totalSubida *= 1024; // Convertimos MB a kB
            }

            // 2. Convertimos la descarga a la misma unidad base (kB)
            double totaldescarga = (double)NUDDescarga.Value;
            if (Convert.ToString(CBDescarga.SelectedItem) == "M")
            {
                totaldescarga *= 1024; // Convertimos MB a kB
            }

            // 3. Ahora sí, comparamos "manzanas con manzanas"
            if (totaldescarga < totalSubida)
            {
                MessageBox.Show("La descarga no puede ser inferior a la subiuda", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            AppRepository obj = new AppRepository();
            var lista = await obj.GetMikrotiksActivos(Convert.ToString(CBPerteneceA.SelectedItem));
            if (lista.Count() == 0)
            {
                MessageBox.Show("No hay Mikrotiks que permita este plan, por favor registre uno antes de crear un plan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PlanModel plan = new PlanModel();
            plan.Id = Id;
            plan.Nombre = txtNombre.Text.Trim();
            plan.IsAntena = (string)CBPerteneceA.SelectedItem == "Antena" ? true : false;
            plan.Precio = NUDPrecio.Value;
            plan.Velocidad = Convert.ToString(NUDSubida.Value) + cbSubida.SelectedItem +
                "/" + Convert.ToString(NUDDescarga.Value) + CBDescarga.SelectedItem;
            plan.Id = obj.SavePlan(plan).Result;
            if (plan.Id != 0)
            {
                string Mensaje = await EmparejarPlan(lista, plan);
                if (Mensaje == string.Empty)
                {
                    MessageBox.Show("Guardado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }
                else
                {
                    MessageBox.Show(Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            MessageBox.Show("Error al guardar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        public async Task<string> EmparejarPlan(List<MikrotikModel> lista, PlanModel Plan)
        {
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            BtnGuardar.Enabled = false;
            string MensajeError = string.Empty;
            AppRepository obj = new AppRepository();
            foreach (var Fila in lista)
            {
                try
                {
                    mikrotik = new MK(Fila.IP, Convert.ToInt32(Fila.Port));
                    bool login = await Task.Run(() =>
                    {
                        return mikrotik.ConectarYLogin(Fila.Usuario, Fila.Password);
                    });
                    if (login == true)
                    {
                        if ((string)CBPerteneceA.SelectedItem == "Fibra")
                        {
                            PlanesAnidadosModel PlanInstroducir = new PlanesAnidadosModel();
                            PlanInstroducir.IdPlan = Plan.Id;
                            PlanInstroducir.IdMikrotik = Fila.Id;
                            PlanInstroducir.IsAntena = false;
                            var Anidado = obj.GetPlanesAnidadosbyParametros(PlanInstroducir).Result;
                            if (Anidado is null)
                            {
                                Anidado = new PlanesAnidadosModel();
                                Anidado.IdPlan = Plan.Id;
                                Anidado.IdMikrotik = Fila.Id;
                                Anidado.IsAntena = false;
                                Anidado.IdPlanInterno = string.Empty;
                            }
                            var Result1 = await Task.Run(() =>
                            {
                                return mikrotik.SavePerfil(Plan, Anidado);
                            });
                            if (Result1 != string.Empty)
                            {
                                MensajeError += $"{Result1} {Fila.Nombre}\n";
                            }
                            else
                            {
                                var Result2 = await Task.Run(() =>
                                {
                                    return mikrotik.DeleteInterface(Plan);
                                });
                            }
                        }
                        else
                        {
                            PlanAnidadoModel plansave = new PlanAnidadoModel();
                            plansave.IdPlan = Plan.Id;
                            plansave.IdPlanInterno = string.Empty;
                            plansave.IsAntena = Plan.IsAntena;
                            plansave.IdMikrotik = Fila.Id;
                            int guardado = obj.SavePlanAnidadoByMigracion(plansave).Result;

                            if (Convert.ToInt32(lblCantidadenPlan.Text) > 0)
                            {
                                var listausuario = obj.GetUsuariosMikrotiksByPlan(Fila.Id, Plan.Id).Result;
                                foreach (var Filausuario in listausuario)
                                {
                                    var  Result1 = mikrotik.ActualizarVelocidadQueue(Filausuario.Nombre, Plan.Velocidad);
                                }
                            }
                        }                           
                    }
                }
                catch (Exception ex)
                {
                    MensajeError += "Mikrotik: " + Fila.Nombre + " " + ex.Message;
                }
                finally
                {
                    if (mikrotik != null)
                    {
                        await Task.Run(() => mikrotik.Close());
                    }
                }
            }
            BtnGuardar.Enabled = true;
            progressBar1.Style = ProgressBarStyle.Blocks; // Detenemos el movimiento
            progressBar1.Value = 100;
            return MensajeError;
        }
    }
}