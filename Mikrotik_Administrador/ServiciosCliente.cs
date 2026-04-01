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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador
{
    public partial class ServiciosCliente : Form
    {
        MK mikrotik;
        public int IdCliente { get; set; }
        public ServiciosCliente()
        {
            InitializeComponent();
        }

        private void ServiciosCliente_Load(object sender, EventArgs e)
        {
            BuscarServicios();
        }
        public void BuscarServicios()
        {
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            btnPlan.Enabled = false;
            DGVServicios.DataSource = null;
            DGVServicios.Columns.Clear();
            try
            {
                AppRepository obj = new AppRepository();
                var lista = obj.GetUsuariosMikrotiksByIdCliente(IdCliente).Result;
                if (lista != null && lista.Count > 0)
                {
                    DGVServicios.DataSource = lista;
                    AgregarBotones();
                    MessageBox.Show("Carga completa", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se encontraron usuarios en el Mikrotik seleccionado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 0;
                btnPlan.Enabled = true; 
            }
        }
        private void AgregarBotones()
        {
            // Botón Checket
            DataGridViewCheckBoxColumn chkSeleccionar = new DataGridViewCheckBoxColumn();
            chkSeleccionar.Name = "cbSeleccionar";
            chkSeleccionar.HeaderText = "Asignar";
            DGVServicios.Columns.Add(chkSeleccionar);

            // Botón Ubicacion
            DataGridViewButtonColumn btnUbicacion = new DataGridViewButtonColumn();
            btnUbicacion.Name = "btnUbicacion";
            btnUbicacion.HeaderText = "Acción";
            btnUbicacion.Text = "Ubicación";
            btnUbicacion.UseColumnTextForButtonValue = true;
            DGVServicios.Columns.Add(btnUbicacion);

            // Botón Checket
            DataGridViewButtonColumn BtnEstatus = new DataGridViewButtonColumn();
            BtnEstatus.Name = "btnEstatus";
            BtnEstatus.HeaderText = "Acción";
            BtnEstatus.Text = "Cambio Estatus";
            BtnEstatus.UseColumnTextForButtonValue = true;
            DGVServicios.Columns.Add(BtnEstatus);
        }
        private void cbTodos_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = cbTodos.Checked;

            foreach (DataGridViewRow row in DGVServicios.Rows)
            {
                if (!row.IsNewRow)
                {
                    row.Cells["cbSeleccionar"].Value = isChecked;
                }
            }
        }

        private async void DGVServicios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Evitar errores si hacen click en el encabezado
            if (e.RowIndex < 0) return;
            var Id = DGVServicios.Rows[e.RowIndex].Cells["Id"].Value;

            switch (DGVServicios.Columns[e.ColumnIndex].Name)
            {
                case "btnUbicacion":
                    var IdMikrotik = DGVServicios.Rows[e.RowIndex].Cells["IdMikrotik"].Value;

                    Ubicacion u = new Ubicacion();
                    u.IdUsuario = Convert.ToInt32(Id);
                    u.IdMikrotik = Convert.ToInt32(IdMikrotik);
                    u.Show();
                    break;
                case "btnEstatus":
                    ListUsuariosGeneralModel objUsuario = new ListUsuariosGeneralModel();
                    objUsuario.Id = Convert.ToInt32(Id);
                    objUsuario.IdMikrotik = (int)DGVServicios.Rows[e.RowIndex].Cells["IdMikrotik"].Value;
                    objUsuario.IdInterno = (string)DGVServicios.Rows[e.RowIndex].Cells["IdInterno"].Value;
                    objUsuario.Usuario = (string)DGVServicios.Rows[e.RowIndex].Cells["Usuario"].Value;
                    objUsuario.Estatus = (string)DGVServicios.Rows[e.RowIndex].Cells["Estatus"].Value;
                    objUsuario.Tipo = (string)DGVServicios.Rows[e.RowIndex].Cells["Tipo"].Value;

                    await CambiarEstatus(objUsuario);
                    break;
            }
        }
        public async Task CambiarEstatus(ListUsuariosGeneralModel objUsuario)
        {
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            btnPlan.Enabled = false;
            AppRepository obj = new AppRepository();
            try
            {
                MikrotikModel mikro = new MikrotikModel();
                mikro = obj.GetMikrotikById(objUsuario.IdMikrotik).Result;
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
                bool Result1 = false;
                bool Result2 = false;
                if (objUsuario.Tipo == "Antena")
                {
                    Result1 = mikrotik.CambiarEstatusAntena(objUsuario.IdInterno, objUsuario.Estatus);
                    Result2 = mikrotik.CambiarEstatusQueues(objUsuario.Usuario, objUsuario.Estatus);
                }
                else
                {
                    Result1 = mikrotik.CambiarEstatusFibra(objUsuario.IdInterno, objUsuario.Estatus);
                    Result2 = true;
                }
                if (Result1 == true && Result2 == true)
                {
                    string nuevoEstatus = objUsuario.Estatus == "Activo" ? "Inactivo" : "Activo";
                    var Res = await obj.UpdateEstatusGeneral(objUsuario.Id, nuevoEstatus);
                    BuscarServicios();
                }
                else
                    MessageBox.Show("Error al actualizar el estatus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                btnPlan.Enabled = true;
            }
        }

        private async void btnPlan_Click(object sender, EventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            btnPlan.Enabled = false;
            try
            {
                List<UsuariosModel> Seleccionados = new List<UsuariosModel>();
                Seleccionados = DGVServicios.Rows.Cast<DataGridViewRow>()
                 .Where(r => Convert.ToBoolean(r.Cells["cbSeleccionar"].Value))
                  .Select(r => new UsuariosModel
                  {
                      id = Convert.ToInt32(r.Cells["Id"].Value),
                      idmikrotik = Convert.ToInt32(r.Cells["IdMikrotik"].Value),
                      idinterno = Convert.ToString(r.Cells["IdInterno"].Value),
                      name = Convert.ToString(r.Cells["Usuario"].Value),
                      tipo = Convert.ToString(r.Cells["Tipo"].Value),
                  })
                  .OrderBy(u => u.idmikrotik)
                  .ToList();


                if (Seleccionados.Count() == 0)
                {
                    MessageBox.Show("No hay usuarios seleccionados");
                    return;
                }


                string primerTipo = Seleccionados[0].tipo;
                foreach (UsuariosModel item in Seleccionados)
                {
                    if (item.tipo != primerTipo)
                    {
                        MessageBox.Show("No se pueden asignar planes a usuarios que no sean " + primerTipo + "," +
                            "por favor seleccione usuarios del mismo tipo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                Planes p = new Planes();
                p.PorUsuarios = true;
                p.Tipo = primerTipo;

                if (p.ShowDialog() == DialogResult.OK)
                {
                    AppRepository obj = new AppRepository();
                    var plan = obj.GetPlanById(p.IdSeleccionado).Result;
                    int contadorfallos = 0;
                    int contadorcorrecto = 0;
                    int MikrotikActual = 0;
                    MikrotikModel mikro;
                    bool F = false;
                    foreach (UsuariosModel item in Seleccionados)
                    {
                        if (MikrotikActual != item.idmikrotik)
                        {
                            F = false;
                            mikro = new MikrotikModel();
                            mikro = obj.GetMikrotikById(item.idmikrotik).Result;
                            MikrotikActual = item.idmikrotik;
                            if (mikro.Estatus == false)
                            {
                                contadorfallos++;
                                F = true;
                                continue;
                            }
                            if (mikrotik != null)
                            {
                                await Task.Run(() => mikrotik.Close());
                            }
                            mikrotik = new MK(mikro.IP, Convert.ToInt32(mikro.Port));
                            bool login = await Task.Run(() =>
                            {
                                return mikrotik.ConectarYLogin(mikro.Usuario, mikro.Password);
                            });
                            if (login == false)
                            {
                                contadorfallos++;
                                F = true;
                                continue;
                            }
                        }
                        else
                        {
                            if (F == true)
                            {
                                contadorfallos++;
                                continue;
                            }
                        }
                        bool Result1 = false;
                        if (item.tipo == "Antena")
                            Result1 = mikrotik.ActualizarVelocidadQueue(item.name, plan.Velocidad);
                        else
                            Result1 = mikrotik.ActualizarUsuarioPPP(item.idinterno, plan.Nombre, plan.Velocidad);
                        if (Result1 == true)
                        {
                            var Res = await obj.UpdatePlanGeneral(item.id, plan.Id);
                            contadorcorrecto++;
                        }
                        else
                            contadorfallos++;
                    }
                    BuscarServicios();
                    MessageBox.Show("Usuarios que cambiaron plan: " + contadorcorrecto.ToString() +
                        "\nUsuarios no cambiaron plan: " + contadorfallos.ToString(), "Resultado de Exportación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnPlan.Enabled = true;
                progressBar1.Style = ProgressBarStyle.Blocks; // Detenemos el movimiento
                progressBar1.Value = 100;
                if (mikrotik != null)
                {
                    await Task.Run(() => mikrotik.Close());
                }
            }
        }
    }
}
