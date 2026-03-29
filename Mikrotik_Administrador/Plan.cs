using GMap.NET.MapProviders;
using Mikrotik_Administrador.Class;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            var lista = await obj.GetMikrotiksActivos();
            if (lista.Count() == 0)
            {
                MessageBox.Show("No hay Mikrotiks activos, por favor registre uno antes de crear un plan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            if (Id != 0)
            {
                var conteo = await obj.GetCountUsuariosByPlan(Id);
                if(conteo > 0)
                {
                    MessageBox.Show("Este plan cuenta con usuarios: \n Al guardar se demorara en asignar la información a todos los usuarios", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CBPerteneceA.Enabled = false;
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
            AppRepository obj = new AppRepository();
            var lista = await obj.GetMikrotiksActivos();
            if (lista.Count() == 0)
            {
                MessageBox.Show("No hay Mikrotiks activos, por favor registre uno antes de crear un plan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
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
            if (obj.SavePlan(plan).Result != 0)
            {
                //progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
                //progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
                //BtnGuardar.Enabled = false;
                //string MensajeError = string.Empty;
                //foreach (var Fila in lista)
                //{
                //    try
                //    {
                //        var listUsuarios = obj.GetUsuariosMikrotiksByPlan(Fila.Id, plan.Id).Result;
                //        if(listUsuarios.Count() == 0)
                //        {
                //            continue; // Si no hay usuarios para este Mikrotik, pasamos al siguiente
                //        }
                //        mikrotik = new MK(Fila.IP, Convert.ToInt32(Fila.Port));
                //        bool login = await Task.Run(() =>
                //        {
                //            return mikrotik.ConectarYLogin(Fila.Usuario, Fila.Password);
                //        });
                //        if (login == true)
                //        {//Aqui tiene que actualizar a la lista de planes de mikrotik y a los usuarios anidados
                //            //if (objUsuario.Tipo == "Antena")
                //            //    Result1 = mikrotik.ActualizarVelocidadQueue(objUsuario.Usuario, plan.Velocidad);
                //            //else
                //            //    Result1 = mikrotik.ActualizarUsuarioPPP(objUsuario.IdInterno, plan.Nombre, plan.Velocidad);

                //        }
                //        else
                //            MensajeError += $"No se pudo conectar al Mikrotik {Fila.Nombre} con IP {Fila.IP}\n";
                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        break;
                //    }
                //    finally
                //    {
                //        if (mikrotik != null)
                //        {
                //            await Task.Run(() => mikrotik.Close());
                //        }
                //    }
                //}
                //BtnGuardar.Enabled = true;
                //progressBar1.Style = ProgressBarStyle.Blocks; // Detenemos el movimiento
                //progressBar1.Value = 100;
                //if (MensajeError != string.Empty)
                //{
                //    MessageBox.Show(MensajeError, "Errores de conexión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
                //else
                //{
                    MessageBox.Show("Guardado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                //}

            }

            MessageBox.Show("Error al guardar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
