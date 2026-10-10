using GMap.NET.MapProviders;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Mikrotik_Administrador.Class;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Items;
using Mikrotik_Administrador.Model;
using Mikrotik_Administrador.Settings;
using Org.BouncyCastle.Tls;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador.Catalogos
{
    public partial class CambiosPlan : Form
    {
        MK mikrotik;
        public int IdResponsable { get; set; }
        public CambiosPlan()
        {
            InitializeComponent();
        }

        private void CambiosPlan_Load(object sender, EventArgs e)
        {
        }
        public void CrearGridView()
        {
            DGVCambios.Columns.Clear();
            DGVCambios.AutoGenerateColumns = false;
            DGVCambios.EnableHeadersVisualStyles = false;

            // --- AJUSTES PARA SALTO DE LÍNEA Y ALTO AUTOMÁTICO ---
            DGVCambios.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // --- ESPACIADO E INTERLINEADO (EVITA QUE SE VEA AMONTONADO) ---
            // Agrega 6px arriba/abajo y 8px a los lados de margen interno en cada celda
            DGVCambios.DefaultCellStyle.Padding = new Padding(8, 6, 8, 6);
            DGVCambios.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // --- ESTILO DE LOS TÍTULOS (HEADERS) ---
            DGVCambios.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            DGVCambios.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            DGVCambios.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            DGVCambios.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            DGVCambios.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            DGVCambios.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            DGVCambios.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            estiloBotones.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Visible = false,
                ReadOnly = true
            });
            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdUsuarioM",
                HeaderText = "IdUsuarioM",
                DataPropertyName = "IdUsuarioM",
                ReadOnly = true,
                Visible = false
            });

            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Usuario",
                HeaderText = "Servicio",
                DataPropertyName = "Usuario",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdPlanNuevo",
                HeaderText = "IdPlanNuevo",
                DataPropertyName = "IdPlanNuevo",
                ReadOnly = true,
                Visible = false,
            });
            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Modo",
                HeaderText = "Tipo de cambio",
                DataPropertyName = "Modo",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PlanNuevo",
                HeaderText = "Plan ha utilizar",
                DataPropertyName = "PlanNuevo",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdMikrotikReceptor",
                HeaderText = "IdMikrotikReceptor",
                DataPropertyName = "IdMikrotikReceptor",
                ReadOnly = true,
                Visible = false,
            });
            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MikrotikNuevo",
                HeaderText = "Mikrotik ha afectar",
                DataPropertyName = "MikrotikNuevo",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaInicio",
                HeaderText = "Comenzara el",
                DataPropertyName = "FechaInicio",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Dias",
                HeaderText = "Días",
                DataPropertyName = "Dias",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Horas",
                HeaderText = "Horas",
                DataPropertyName = "Horas",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaFin",
                HeaderText = "Fecha que terminara",
                DataPropertyName = "FechaFin",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdPlanRetorno",
                HeaderText = "IdPlanRetorno",
                DataPropertyName = "IdPlanRetorno",
                ReadOnly = true,
                Visible = false,
            });
            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PlanRetorno",
                HeaderText = "Plan que se tendra al terminar",
                DataPropertyName = "PlanRetorno",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdMikrotikOriginal",
                HeaderText = "IdMikrotikOriginal",
                DataPropertyName = "IdMikrotikOriginal",
                ReadOnly = true,
                Visible = false,
            });
            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MikrotikOriginal",
                HeaderText = "Mikrotik al terminar",
                DataPropertyName = "MikrotikOriginal",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                HeaderText = "Estatus",
                DataPropertyName = "Estatus",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            // --- COLUMNA NOTA OPTIMIZADA ---
            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nota",
                HeaderText = "Nota",
                DataPropertyName = "Nota",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 250, // Garantiza un espacio horizontal mínimo holgado
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn
            {
                Name = "btnEditar",
                HeaderText = "Acción",
                Text = "Editar",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };

            DGVCambios.Columns.Add(btnEditar);
            DataGridViewButtonColumn btnEstatus = new DataGridViewButtonColumn
            {
                Name = "btnEstatus",
                HeaderText = "Acción",
                Text = "Estatus",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };

            DGVCambios.Columns.Add(btnEstatus);
            DGVCambios.AllowUserToAddRows = false;
        }
        public void Buscar()
        {
            CrearGridView();
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación

            try
            {
                AppRepository obj = new AppRepository();
                var lista = obj.GetTiempoCambio(0, dtpFechaInicio.Value, dtpFechaFinal.Value).Result;
                var listaFinal = lista?.ToList() ?? new List<ListTiempoCambioModel>();
                DGVCambios.DataSource = new SortableBindingList<ListTiempoCambioModel>(listaFinal);
                if (DGVCambios.Columns["Id"] != null)
                {
                    DGVCambios.Columns["Id"].Visible = false;
                }
                if (DGVCambios.Columns["IdPlanNuevo"] != null)
                {
                    DGVCambios.Columns["IdPlanNuevo"].Visible = false;
                }
                if (DGVCambios.Columns["IdMikrotikReceptor"] != null)
                {
                    DGVCambios.Columns["IdMikrotikReceptor"].Visible = false;
                }
                if (DGVCambios.Columns["IdPlanRetorno"] != null)
                {
                    DGVCambios.Columns["IdPlanRetorno"].Visible = false;
                }
                if (DGVCambios.Columns["IdMikrotikOriginal"] != null)
                {
                    DGVCambios.Columns["IdMikrotikOriginal"].Visible = false;
                }
                if (DGVCambios.Columns["IdUsuarioM"] != null)
                {
                    DGVCambios.Columns["IdUsuarioM"].Visible = false;
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
            }
        }
        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }
        private async void DGVCambios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string nombreColumna = DGVCambios.Columns[e.ColumnIndex].Name;
            if (nombreColumna != "btnEditar" && nombreColumna != "btnEstatus") return;
            var Id = DGVCambios.Rows[e.RowIndex].Cells["Id"].Value;
            var IdUsuarioM = Convert.ToInt32(DGVCambios.Rows[e.RowIndex].Cells["IdUsuarioM"].Value);
            var FechaInicioCambio = Convert.ToDateTime(DGVCambios.Rows[e.RowIndex].Cells["FechaInicio"].Value);
            var FechaFinCambio = Convert.ToDateTime(DGVCambios.Rows[e.RowIndex].Cells["FechaFin"].Value);
            AppRepository obj = new AppRepository();
            switch (DGVCambios.Columns[e.ColumnIndex].Name)
            {
                case "btnEditar":
                    TiempoDefinido TD = new TiempoDefinido();
                    TD.Id= Convert.ToInt32(Id);
                    TD.ShowDialog();
                    Buscar();
                    break;
                case "btnEstatus":
                    var EstatusActual = DGVCambios.Rows[e.RowIndex].Cells["Estatus"].Value.ToString();
                    var PlanRetorno = DGVCambios.Rows[e.RowIndex].Cells["PlanRetorno"].Value.ToString();
                    var Servicio = DGVCambios.Rows[e.RowIndex].Cells["Usuario"].Value.ToString().Trim();
                    Programar pr = new Programar();
                    pr.ShowDialog();
                    if (EstatusActual == pr.SePrograma)
                    {
                        MessageBox.Show("Este estatus ya esta aplicado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if ((pr.SePrograma == "Cancelado" && EstatusActual == "Completado")
                        || (pr.SePrograma == "Completado" && EstatusActual == "Cancelado"))
                    {
                        goto Fin;
                    }
                    if (FechaFinCambio < DateTime.Now && pr.SePrograma == "Ejecutando")
                    {
                        MessageBox.Show("Este cambio ya termino, no se puede cambiar a estado Ejecutando.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if ((pr.SePrograma == "Cancelado" || pr.SePrograma == "Completado") && EstatusActual == "Ejecutando")
                    {
                        DialogResult resultado = MessageBox.Show("Actualmente este cambio se esta ejecutando, al cancelar o completar se devolvera el servicio al plan " + PlanRetorno + " ¿Quiere continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                        if (resultado == DialogResult.No)
                        {
                            return;
                        }
                    }
                    bool primeravuelta = true;
                //Primera vuelta insertamos o activamos
                //segun vuelta desactivamos el origen
                Vuelta2:
                    int IdMikrotik = Convert.ToInt32(DGVCambios.Rows[e.RowIndex].Cells["IdMikrotikReceptor"].Value);
                    int IdPlan = Convert.ToInt32(DGVCambios.Rows[e.RowIndex].Cells["IdPlanNuevo"].Value);
                    if (pr.SePrograma != "Ejecutando" && primeravuelta)
                    {
                        IdMikrotik = Convert.ToInt32(DGVCambios.Rows[e.RowIndex].Cells["IdMikrotikOriginal"].Value);
                        IdPlan = Convert.ToInt32(DGVCambios.Rows[e.RowIndex].Cells["IdPlanRetorno"].Value);
                    }

                    var plan = obj.GetPlanById(IdPlan).Result;
                    string comment = string.Empty;
                    if (plan.IsAntena == true)//es antena si sale false
                    {
                        var listacomments = await Task.Run(() => obj.GetCommentsActivos(IdMikrotik));
                        if (listacomments.Count == 0)
                        {
                            MessageBox.Show("No se encontraron comments activos en el mikrotik seleccionado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        Programar pro = new Programar();
                        pro.listComments = listacomments.ToList();
                        pro.ShowDialog();
                        if (pro.SePrograma == string.Empty)
                        {
                            MessageBox.Show("No se selecciono un comment para el servicio a crear, favor de seleccionar uno.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        comment = pro.SePrograma;
                    }
                    if (mikrotik != null)
                    {
                        await Task.Run(() => mikrotik.Close());
                        mikrotik = null;
                    }
                    MikrotikModel mikro = new MikrotikModel();
                    mikro = obj.GetMikrotikById(IdMikrotik).Result;
                    if (mikro.Estatus == false)
                    {
                        MessageBox.Show("El Mikrotik " + mikro.Nombre + " está desactivado, por favor activelo para continuar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (plan.IsAntena == true)
                    {
                        string ExisteEnQueue = mikrotik.VerIdQueue(Servicio.Trim());
                        List<Antenas> Antenas = mikrotik.VerAntenasbyComment(Servicio.Trim());
                        if (Antenas.ToList().Count() > 0 && ExisteEnQueue == string.Empty)
                        {
                            MessageBox.Show("Se encontro el servicio en firewall " + Servicio +
                                " pero en queues no existe. Favor de revisar"
                                , "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (Antenas.ToList().Count() == 0 && ExisteEnQueue != string.Empty)
                        {
                            MessageBox.Show("Se encontro el servicio queues " + Servicio +
                                " pero en firewall no existe. Favor de revisar"
                                , "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (Antenas.ToList().Count() > 0 && ExisteEnQueue != string.Empty)
                        {
                            if (primeravuelta == false)
                            {
                                var Result4 = mikrotik.CambiarEstatusQueues(Servicio, "Activo");
                            }
                            else
                            {
                                MessageBox.Show("Ya existe un servicio con el nombre " + Servicio + " en el mikrotik "
                            + mikro.Nombre + ", se procedera solo ha activarlo y otorgar el plan correspondiente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                var Result1 = mikrotik.ActualizarVelocidadQueue(Servicio, plan.Velocidad);
                                var Result2 = mikrotik.CambiarEstatusQueues(Servicio, "Inactivo");
                                var Result3 = mikrotik.ActualizalistdeQueue(Antenas.First().id, comment);
                                UsuariosGeneralModel ug = new UsuariosGeneralModel()
                                {
                                    Id = Convert.ToInt32(DGVCambios.Rows[e.RowIndex].Cells["IdUsuarioM"].Value),
                                    IdMikrotik = mikro.Id,
                                    IdPlan = plan.Id,
                                    Nombre = Servicio,
                                    Address = Antenas.First().address,
                                    IdInterno = Antenas.First().id,
                                    Estatus = "Activo"
                                };
                                var result = obj.SaveUsuariosGeneral(ug, IdResponsable).Result;
                                HistorialMovimientosModel hm = new HistorialMovimientosModel()
                                {
                                    Descripcion = "Se actualizo el servicio " + Servicio + " con la ip " + Antenas.First().address + " y la velocidad " + plan.Velocidad + " en el mikrotik " + mikro.Nombre,
                                    Pagina = "CambiosPlan",
                                    IdUsuario = IdResponsable,
                                    Fecha = DateTime.Now,
                                    Estatus = false,
                                    Address = Antenas.First().address,
                                    Comment = Servicio,
                                    IsAntena = true,
                                    IdMikrotik = mikro.Id
                                };
                                await obj.SaveHistorialMovimientos(hm);
                                MessageBox.Show("El servicio " + Servicio +
                            " se actualizo con la ip " + Antenas.First().address + " y la velocidad " + plan.Velocidad +
                            " en el mikrotik " + mikro.Nombre + " y se"
                            , "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                        }
                        else
                        {
                            if (primeravuelta)
                            {
                                DialogResult resultado = MessageBox.Show("No se encontro al servicio " + Servicio + " se procedera ha agregarlo con una ip nueva en antena ¿Quiere continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                                if (resultado == DialogResult.No)
                                {
                                    return;
                                }
                                //No existe en el mikrotik hay que agregarlo
                                string IPDisponible = string.Empty;
                                var listwiriless = obj.GetWirelessbyIdMikrotik(IdMikrotik, true).Result;
                                if (listwiriless.Count() == 0)
                                {
                                    MessageBox.Show("Se han acompletado todos los addresslist disponibles para antena, crear más para confirmar el cambio en el mikrotik " + mikro.Nombre + " favor de revisar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            buscaotraipAntena:
                                IPDisponible = obj.GetIPDisponible(IdMikrotik, true, IPDisponible).Result;
                                if (IPDisponible != string.Empty)
                                {
                                    ExisteEnQueue = mikrotik.VerIdQueuebyAddress(IPDisponible);//Se extrae el id del queues
                                    var ExisteEnAntenas = mikrotik.VerAntenasbyAddress(IPDisponible);
                                    if (ExisteEnAntenas.Count() > 0 || ExisteEnQueue != string.Empty)
                                    {
                                        HistorialMovimientosModel H = new HistorialMovimientosModel
                                        {
                                            Id = 0,
                                            Descripcion = "Ya se encuentra registrado el ip " + IPDisponible + " para antena, en el mikrotik " +
                                            mikro.Nombre + " y no esta informado el sistema favor de revisar",
                                            Pagina = "CambiosPlan",
                                            IdUsuario = IdResponsable,
                                            Estatus = true
                                        };
                                        await obj.SaveHistorialMovimientos(H);
                                        goto buscaotraipAntena;
                                    }
                                    //Insertamos en mikrotik
                                    bool re = mikrotik.CrearSimpleQueue(Servicio, IPDisponible, plan.Velocidad, comment);
                                    bool re2 = mikrotik.AgregarAntena(comment, IPDisponible, Servicio, true);
                                    ExisteEnAntenas = new List<Antenas>();
                                    ExisteEnAntenas = mikrotik.VerAntenasbyAddress(IPDisponible);
                                    mikrotik.CambiarEstatusAntena(ExisteEnAntenas.First().id, "Activo");
                                    mikrotik.CambiarEstatusQueues(Servicio, "Inactivo");
                                    UsuariosGeneralModel objuser = new UsuariosGeneralModel();
                                    objuser.IdMikrotik = IdMikrotik;
                                    objuser.Nombre = Servicio;
                                    objuser.Address = IPDisponible;
                                    objuser.IdInterno = ExisteEnAntenas.First().id;
                                    objuser.Estatus = "Activo";
                                    objuser.Id = IdUsuarioM;
                                    objuser.IdPlan = IdPlan;
                                    var result = obj.SaveUsuariosGeneral(objuser, IdResponsable).Result;

                                }
                                else
                                {
                                    listwiriless = obj.GetWirelessbyIdMikrotik(IdMikrotik, true).Result;
                                    if (listwiriless.Count() == 0)
                                    {
                                        MessageBox.Show("Se han acompletado todos los addresslist disponibles para antena, crear más para confirmar el cambio en el mikrotik " + mikro.Nombre + " favor de revisar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Se continuara con la serie " + listwiriless.First().Address + " en el mikrotik " + mikro.Nombre + " favor de revisar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        goto buscaotraipAntena;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        string IdPlanInterno = mikrotik.BuscarPerfil(plan.Nombre);
                        if (IdPlanInterno == string.Empty)
                        {
                            MessageBox.Show("No se logro extraer el perfil del plan " +
                                plan.Nombre +
                                "para la solicitud asignada en el mikrotik "
                                + mikro.Nombre +
                                ", es posible que lo hayan borrado fuera del sistema. Favor de revisar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        List<Fibra> ExisteEnFibra = mikrotik.VerFibra(Servicio.Trim());
                        if (ExisteEnFibra.Count() > 0)
                        {
                            if (primeravuelta == false)
                            {
                                mikrotik.CambiarEstatusFibra(ExisteEnFibra.First().id, "Activo");
                            }
                            else
                            {
                                MessageBox.Show("Ya existe un servicio con el nombre " + Servicio + " en el mikrotik "
                           + mikro.Nombre + ", se procedera solo ha activarlo y otorgar el plan correspondiente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                var Result1 = mikrotik.ActualizarUsuarioPPP(ExisteEnFibra.First().id, plan.Nombre, plan.Velocidad);
                                var Result2 = mikrotik.DeleteInterfacebyName(Servicio);
                                UsuariosGeneralModel ug = new UsuariosGeneralModel()
                                {
                                    Id = Convert.ToInt32(DGVCambios.Rows[e.RowIndex].Cells["IdUsuarioM"].Value),
                                    IdMikrotik = mikro.Id,
                                    IdPlan = plan.Id,
                                    Nombre = Servicio,
                                    Address = ExisteEnFibra.First().address,
                                    IdInterno = ExisteEnFibra.First().id,
                                    Estatus = "Activo"
                                };
                                var result = obj.SaveUsuariosGeneral(ug, IdResponsable).Result;
                                HistorialMovimientosModel hm = new HistorialMovimientosModel()
                                {
                                    Descripcion = "Se actualizo el servicio " + Servicio + " con la ip " + ExisteEnFibra.First().address + " y la velocidad " + plan.Velocidad + " en el mikrotik " + mikro.Nombre,
                                    Pagina = "CambiosPlan",
                                    IdUsuario = IdResponsable,
                                    Fecha = DateTime.Now,
                                    Estatus = false,
                                    Address = ExisteEnFibra.First().address,
                                    Comment = Servicio,
                                    IsAntena = false,
                                    IdMikrotik = mikro.Id
                                };
                                await obj.SaveHistorialMovimientos(hm);
                                MessageBox.Show("El servicio " + Servicio +
                            " se actualizo con la ip " + ExisteEnFibra.First().address + " y la velocidad " + plan.Velocidad +
                            " en el mikrotik " + mikro.Nombre + " y se"
                            , "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                        }
                        else
                        {
                            if (primeravuelta == true)
                            {
                                DialogResult resultado = MessageBox.Show("No se encontro al servicio " + Servicio + " se procedera ha agregarlo con una ip nueva en fibra ¿Quiere continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                                if (resultado == DialogResult.No)
                                {
                                    return;
                                }
                                //No existe en el mikrotik hay que agregarlo
                                string IPDisponibleFibra = string.Empty;
                            buscaotraipFibra:
                                IPDisponibleFibra = obj.GetIPDisponible(IdMikrotik, false, IPDisponibleFibra).Result;
                                if (IPDisponibleFibra != string.Empty)
                                {
                                    ExisteEnFibra = mikrotik.VerFibrabyAddress(IPDisponibleFibra);
                                    if (ExisteEnFibra.Count() > 0) //Ya existe en secret
                                    {
                                        HistorialMovimientosModel H = new HistorialMovimientosModel
                                        {
                                            Id = 0,
                                            Descripcion = "Ya se encuentra registrado el ip " + IPDisponibleFibra + " para fibra, en el mikrotik " + mikro.Nombre + " y no esta informado el sistema favor de revisar",
                                            Pagina = "CambiosPlan",
                                            IdUsuario = IdResponsable,
                                            Estatus = true
                                        };
                                        await obj.SaveHistorialMovimientos(H);
                                        goto buscaotraipFibra;
                                    }
                                    string idCreado = mikrotik.CrearFibra(Servicio, IPDisponibleFibra, plan.Nombre, "1234");
                                    mikrotik.CambiarEstatusFibra(idCreado, "Inactivo");
                                    UsuariosGeneralModel objuser = new UsuariosGeneralModel();
                                    objuser.IdMikrotik = IdMikrotik;
                                    objuser.Nombre = Servicio;
                                    objuser.Address = IPDisponibleFibra;
                                    objuser.IdInterno = idCreado;
                                    objuser.Estatus = "Activo";
                                    objuser.Id = IdUsuarioM;
                                    objuser.IdPlan = IdPlan;
                                    var result = obj.SaveUsuariosGeneral(objuser, IdResponsable).Result;
                                }
                                else
                                {
                                    var listPools = obj.GetPoolsbyIdMikrotik(IdMikrotik, true).Result;
                                    if (listPools.Count() == 0)
                                    {
                                        MessageBox.Show("Se han acompletado todos los pools disponibles para fibra, crear más para confirmar el cambio en el mikrotik " + mikro.Nombre + " favor de revisar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Se continuara con la serie " + listPools.First().IP + " en el mikrotik " + mikro.Nombre + " favor de revisar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        goto buscaotraipFibra;
                                    }
                                }
                            }

                        }
                    }
                    if (IdMikrotik != Convert.ToInt32(DGVCambios.Rows[e.RowIndex].Cells["IdMikrotikOriginal"].Value))
                    {
                        primeravuelta = false;
                        goto Vuelta2;
                    }

                Fin:
                    // 1. Actualizamos primero el estatus del cambio (ej. a Cancelado)
                    await obj.UpdateEstatusTiempoCambio(Convert.ToInt32(Id), pr.SePrograma);
      
                    var mensualidadesAfectadas = await obj.GetMensualidadesAfectadas(IdUsuarioM, FechaInicioCambio, FechaFinCambio);
                    foreach (var mens in mensualidadesAfectadas)
                    {
                        // Para CADA mensualidad afectada (ej. su propio FechaInicio y FechaLimite), 
                        // recalculamos sus tramos limpios
                        var tramosCalculados = await CalcularTramosMensualidadAsync(IdUsuarioM, mens.FechaInicio, mens.FechaLimite);

                        // Sumamos el costo total de los tramos que cayeron dentro de esta mensualidad
                        decimal costoTotalMensualidad = tramosCalculados.Sum(t => t.Costo);

                        // Actualizamos el costo final de ESTA mensualidad en la base de datos
                        mens.Mensualidad = costoTotalMensualidad;
                        await obj.SaveMensualidad(mens);
                    }
                    Buscar();
                    break;
            }

        }
        public async Task<List<ListDetallesMensualidadModel>> CalcularTramosMensualidadAsync(int idUsuarioM, DateTime desde, DateTime hasta)
        {
            AppRepository obj = new AppRepository();
            var Detalles = await obj.GetTiempoCambioforDetalles(idUsuarioM, desde, hasta);
            if (Detalles == null || !Detalles.Any())
            {
                return new List<ListDetallesMensualidadModel>();
            }

            List<ListDetallesMensualidadModel> ListDestalles = new List<ListDetallesMensualidadModel>();

            var usuarioMikrotik = await obj.GetUsuariosMikrotiksById(idUsuarioM);
            var planBasePeriodo = await obj.GetPlanById(usuarioMikrotik.IdPlanOriginal);
            string nombrePlanActual = planBasePeriodo != null ? planBasePeriodo.Nombre : "Plan Base";
            decimal precioPlanActual = planBasePeriodo != null ? planBasePeriodo.Precio : 0m;

            var cambioAnterior = Detalles
                .Where(x => x.FechaFin < desde && x.Estatus != "Cancelado")
                .OrderByDescending(x => x.FechaFin)
                .FirstOrDefault();

            if (cambioAnterior != null)
            {
                var planAnt = await obj.GetPlanById(cambioAnterior.IdPlanOriginal);
                if (planAnt != null)
                {
                    nombrePlanActual = planAnt.Nombre;
                    precioPlanActual = planAnt.Precio;
                }
            }

            var cambiosOrdenados = Detalles
                .Where(x => x.FechaInicio <= hasta && x.FechaFin >= desde && x.Estatus != "Cancelado")
                .OrderBy(x => x.FechaInicio)
                .ToList();

            DateTime cursor = desde;

            foreach (var cambio in cambiosOrdenados)
            {
                if (cursor < cambio.FechaInicio)
                {
                    DateTime finTramoBase = cambio.FechaInicio.AddDays(-1);
                    if (finTramoBase > hasta) finTramoBase = hasta;

                    if (cursor <= finTramoBase)
                    {
                        int diasBase = (int)(finTramoBase - cursor).TotalDays + 1;
                        decimal costoBase = RedondearMontoFinanciero(diasBase * (precioPlanActual / 30.0m));

                        ListDestalles.Add(new ListDetallesMensualidadModel
                        {
                            Id = 0, // O el Id de la mensualidad base correspondiente si lo manejas
                            FechaInicio = cursor,
                            FechaFin = finTramoBase,
                            Estatus = "Activo",
                            Plan = nombrePlanActual,
                            Costo = costoBase
                        });
                    }
                    cursor = cambio.FechaInicio;
                }

                DateTime inicioCambioEfectivo = cursor > cambio.FechaInicio ? cursor : cambio.FechaInicio;
                DateTime finCambioEfectivo = hasta < cambio.FechaFin ? hasta : cambio.FechaFin;

                if (inicioCambioEfectivo <= finCambioEfectivo)
                {
                    int diasCambio = (int)(finCambioEfectivo - inicioCambioEfectivo).TotalDays + 1;
                    var planNuevo = await obj.GetPlanById(cambio.IdPlan);
                    decimal precioPlanNuevo = planNuevo != null ? planNuevo.Precio : 0m;
                    decimal costoCambio = RedondearMontoFinanciero(diasCambio * (precioPlanNuevo / 30.0m));

                    ListDestalles.Add(new ListDetallesMensualidadModel
                    {
                        Id = cambio.Id, // ID del cambio / mensualidad a afectar
                        FechaInicio = inicioCambioEfectivo,
                        FechaFin = finCambioEfectivo,
                        Estatus = cambio.Estatus,
                        Plan = cambio.Plan,
                        Costo = costoCambio
                    });

                    cursor = finCambioEfectivo.AddDays(1);
                }

                var planOrigCambio = await obj.GetPlanById(cambio.IdPlanOriginal);
                if (planOrigCambio != null)
                {
                    nombrePlanActual = planOrigCambio.Nombre;
                    precioPlanActual = planOrigCambio.Precio;
                }
            }

            if (cursor <= hasta)
            {
                DateTime fechaFinTramoFinal = hasta;
                if (cursor <= fechaFinTramoFinal)
                {
                    int diasFinales = (int)(fechaFinTramoFinal - cursor).TotalDays + 1;
                    decimal costoFinal = RedondearMontoFinanciero(diasFinales * (precioPlanActual / 30.0m));

                    ListDestalles.Add(new ListDetallesMensualidadModel
                    {
                        Id = 0,
                        FechaInicio = cursor,
                        FechaFin = fechaFinTramoFinal,
                        Estatus = "Activo",
                        Plan = nombrePlanActual,
                        Costo = costoFinal
                    });
                }
            }

            return ListDestalles;
        }
        private decimal RedondearMontoFinanciero(decimal monto)
        {
            decimal parteEntera = Math.Floor(monto);
            decimal parteDecimal = monto - parteEntera;

            if (parteDecimal > 0.00m && parteDecimal < 0.30m)
                return parteEntera;
            else if (parteDecimal >= 0.30m && parteDecimal <= 0.50m)
                return parteEntera + 0.50m;
            else if (parteDecimal > 0.50m)
                return parteEntera + 1.00m;

            return parteEntera;
        }
    }
}
