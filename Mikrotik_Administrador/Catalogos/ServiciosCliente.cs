using GMap.NET.MapProviders;
using Mikrotik_Administrador.Class;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Items;
using Mikrotik_Administrador.Model;
using Mikrotik_Administrador.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace Mikrotik_Administrador
{
    public partial class ServiciosCliente : Form
    {
        MK mikrotik;
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public string NombreCliente { get; set; }
        public ServiciosCliente()
        {
            InitializeComponent();
        }

        private void ServiciosCliente_Load(object sender, EventArgs e)
        {
            BuscarServicios();
        }
        public void CrearGridView()
        {
            DGVServicios.Columns.Clear();
            DGVServicios.AutoGenerateColumns = false;
            DGVServicios.EnableHeadersVisualStyles = false;
            // --- ESTILO DE LOS TÍTULOS (HEADERS) CON TU AZUL LOGO ---
            DGVServicios.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            DGVServicios.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            DGVServicios.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            DGVServicios.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            DGVServicios.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            DGVServicios.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);

            DGVServicios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Visible = false,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVServicios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdInterno",
                HeaderText = "IdInterno",
                DataPropertyName = "IdInterno",
                Visible = false,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVServicios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Usuario",
                HeaderText = "Usuario",
                DataPropertyName = "Usuario",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVServicios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Address",
                HeaderText = "IP",
                DataPropertyName = "Address",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVServicios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                HeaderText = "Estatus",
                DataPropertyName = "Estatus",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVServicios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdPlan",
                HeaderText = "IdPlan",
                DataPropertyName = "IdPlan",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVServicios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdPlanOriginal",
                HeaderText = "IdPlanOriginal",
                DataPropertyName = "IdPlanOriginal",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVServicios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Plan",
                HeaderText = "Plan",
                DataPropertyName = "Plan",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVServicios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UploadDownload",
                HeaderText = "UploadDownload",
                DataPropertyName = "UploadDownload",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVServicios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdMikrotik",
                HeaderText = "IdMikrotik",
                DataPropertyName = "IdMikrotik",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVServicios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Mikrotik",
                HeaderText = "Mikrotik",
                DataPropertyName = "Mikrotik",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVServicios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdCliente",
                HeaderText = "IdCliente",
                DataPropertyName = "IdCliente",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVServicios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cliente",
                HeaderText = "Cliente",
                DataPropertyName = "Cliente",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVServicios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tipo",
                HeaderText = "Tipo",
                DataPropertyName = "Tipo",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVServicios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MinFechaInicio",
                HeaderText = "Cambio de plan inicia",
                DataPropertyName = "MinFechaInicio",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVServicios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaxFechaFin",
                HeaderText = "Cambio de plan termina",
                DataPropertyName = "MaxFechaFin",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DataGridViewButtonColumn btnProgramar = new DataGridViewButtonColumn
            {
                Name = "btnProgramar",
                HeaderText = "Acción",
                Text = "Programar",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            DGVServicios.Columns.Add(btnProgramar);
            DataGridViewButtonColumn btnUbicacion = new DataGridViewButtonColumn
            {
                Name = "btnUbicacion",
                HeaderText = "Acción",
                Text = "Ubicación",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            DGVServicios.Columns.Add(btnUbicacion);
            DataGridViewButtonColumn BtnEstatus = new DataGridViewButtonColumn
            {
                Name = "btnEstatus",
                HeaderText = "Acción",
                Text = "Cambio Estatus",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            DGVServicios.Columns.Add(BtnEstatus);
            DGVServicios.AllowUserToAddRows = false;
        }
        public void BuscarServicios()
        {
            CrearGridView();
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación

            try
            {
                AppRepository obj = new AppRepository();
                var lista = obj.GetUsuariosMikrotiksByIdCliente(IdCliente).Result;
                var listaFinal = lista?.ToList() ?? new List<ListUsuariosGeneralModel>();
                DGVServicios.DataSource = new SortableBindingList<ListUsuariosGeneralModel>(listaFinal);
                if (DGVServicios.Columns["Id"] != null)
                {
                    DGVServicios.Columns["Id"].Visible = false;
                }
                if (DGVServicios.Columns["IdPlan"] != null)
                {
                    DGVServicios.Columns["IdPlan"].Visible = false;
                }
                if (DGVServicios.Columns["IdPlanOriginal"] != null)
                {
                    DGVServicios.Columns["IdPlanOriginal"].Visible = false;
                }
                if (DGVServicios.Columns["IdMikrotik"] != null)
                {
                    DGVServicios.Columns["IdMikrotik"].Visible = false;
                }
                if (DGVServicios.Columns["IdCliente"] != null)
                {
                    DGVServicios.Columns["IdCliente"].Visible = false;
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

        private async void DGVServicios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Evitar errores si hacen click en el encabezado
            if (e.RowIndex < 0) return;
            int Id = (int)DGVServicios.Rows[e.RowIndex].Cells["Id"].Value;
            string Estatus = (string)DGVServicios.Rows[e.RowIndex].Cells["Estatus"].Value;
            ListUsuariosGeneralModel objUsuario = new ListUsuariosGeneralModel();
            objUsuario.Id = Id;
            objUsuario.IdMikrotik = (int)DGVServicios.Rows[e.RowIndex].Cells["IdMikrotik"].Value;
            objUsuario.IdInterno = (string)DGVServicios.Rows[e.RowIndex].Cells["IdInterno"].Value;
            objUsuario.Usuario = (string)DGVServicios.Rows[e.RowIndex].Cells["Usuario"].Value;
            objUsuario.Estatus = (string)DGVServicios.Rows[e.RowIndex].Cells["Estatus"].Value;
            objUsuario.Tipo = (string)DGVServicios.Rows[e.RowIndex].Cells["Tipo"].Value;

            switch (DGVServicios.Columns[e.ColumnIndex].Name)
            {
                case "btnUbicacion":
                    var IdMikrotik = DGVServicios.Rows[e.RowIndex].Cells["IdMikrotik"].Value;

                    Ubicacion u = new Ubicacion();
                    u.IdUsuario = Id;
                    u.IdMikrotik = Convert.ToInt32(IdMikrotik);
                    u.Show();
                    break;
                case "btnEstatus":
                    if (Estatus == "Eliminado")
                    {
                        MessageBox.Show("Este servicio se encuentra ya eliminado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    await CambiarEstatus(objUsuario);
                    break;

                case "btnProgramar":
                    if (Estatus == "Eliminado")
                    {
                        MessageBox.Show("Este servicio se encuentra ya eliminado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    bool checar = await ChecarUsuario(objUsuario);
                    if (checar == false)
                    {
                        return;
                    }
                    Programar pr = new Programar();
                    if (pr.ShowDialog() != DialogResult.OK)
                        return;
                    int IdPlan = (int)DGVServicios.Rows[e.RowIndex].Cells["IdPlan"].Value;
                    int IdPlanActual = (int)DGVServicios.Rows[e.RowIndex].Cells["IdPlanOriginal"].Value;
                    int IdPlanSeccionado = (int)DGVServicios.Rows[e.RowIndex].Cells["IdPlanOriginal"].Value;
                    string NombrePlan = string.Empty;
                    if (pr.SePrograma == "Cambio de plan")
                    {
                        Planes p = new Planes();
                        p.IdUsuario = IdUsuario;
                        p.PorUsuarios = true;
                        p.Tipo = string.Empty;
                        if (p.ShowDialog()!= DialogResult.OK)
                        {
                            return;
                        }
                        IdPlanSeccionado = p.IdSeleccionado;
                        NombrePlan = p.NombrePlan;
                    }
                    if (checar == false)
                    {
                        return;
                    }
                    TiempoDefinido td = new TiempoDefinido();
                    td.FechaInicio = DGVServicios.Rows[e.RowIndex].Cells["MinFechaInicio"].Value == DBNull.Value || DGVServicios.Rows[e.RowIndex].Cells["MinFechaInicio"].Value == null
                        ? (DateTime?)null : Convert.ToDateTime(DGVServicios.Rows[e.RowIndex].Cells["MinFechaInicio"].Value);
                    td.FechaFin = DGVServicios.Rows[e.RowIndex].Cells["MaxFechaFin"].Value == DBNull.Value || DGVServicios.Rows[e.RowIndex].Cells["MaxFechaFin"].Value == null
                        ? (DateTime?)null : Convert.ToDateTime(DGVServicios.Rows[e.RowIndex].Cells["MaxFechaFin"].Value);
                    td.IdPlan = IdPlanSeccionado;
                    td.Programacion = pr.SePrograma;
                    td.IdMikrotik = objUsuario.IdMikrotik;
                    td.NombrePlan = NombrePlan;
               
                    if (td.ShowDialog() == DialogResult.Cancel)
                    {
                        MessageBox.Show("Se cancelo el cambio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (IdPlanActual == IdPlanSeccionado && td.IdMikrotik == objUsuario.IdMikrotik && pr.SePrograma == "Cambio de plan") //No tiene caso designar el mismo plan
                    {
                        MessageBox.Show("Esta plan ya se encuentra funcionando actualmente en el mikrotik seleccionado, por favor seleccione otro plan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    TiempoDefinidosModel TD = new TiempoDefinidosModel
                    {
                        Dias = td.Dias,
                        Horas = td.Horas,
                        FechaInicio = td.FechaInicio ?? DateTime.Now,
                        FechaFin = td.FechaFin ?? DateTime.Now.AddDays(td.Dias).AddHours(td.Horas),
                        Modo = td.Modo,
                        IdUsuarioM = Convert.ToInt32(DGVServicios.Rows[e.RowIndex].Cells["Id"].Value),
                        Estatus = "Pendiente",
                        IdPlan = IdPlanSeccionado,
                        IdMikrotikReceptor = td.IdMikrotik,
                        Programacion = pr.SePrograma,
                        Password = td.Password
                    };
                    HistorialMovimientosModel H = new HistorialMovimientosModel
                    {
                        Id = 0,
                        Descripcion = "Se a solicitado " + pr.SePrograma + " para el usuario " + objUsuario.Usuario + " plan seleccionado: " + NombrePlan,
                        Pagina = "Servicio cliente",
                        IdUsuario = IdUsuario,
                        Estatus = false
                    };
                    
                    AppRepository obj = new AppRepository();
                    await obj.SaveHistorialMovimientos(H);
                    var result = obj.SaveTiempoCambio(TD);
                    MessageBox.Show("Se ha enviado la solicitud de cambio de plan satisfactoriamente.", "Resultado de cambio de plan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BuscarServicios();
                    break;
            }
        }
        public async Task<bool> ChecarUsuario(ListUsuariosGeneralModel objUsuario)
        {
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            AppRepository obj = new AppRepository();
            try
            {
                MikrotikModel mikro = new MikrotikModel();
                mikro = obj.GetMikrotikById(objUsuario.IdMikrotik).Result;
                if (mikro.Estatus == false)
                {
                    MessageBox.Show("El Mikrotik seleccionado está desactivado, por favor activelo para continuar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (mikrotik != null)
                {
                    await Task.Run(() => mikrotik.Close());
                    mikrotik = null;
                }
                mikrotik = new MK(mikro.IP, Convert.ToInt32(mikro.Port));

                bool login = await Task.Run(() =>
                {
                    return mikrotik.ConectarYLogin(mikro.Usuario, mikro.Password);
                });
                if (login == false)
                {
                    MessageBox.Show("Error en conexión, revisar que el firewall y nat no esten bloqueando los puertos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (objUsuario.Tipo == "Antena")
                {
                    //Primero revisamos si el servicio aun existe en el mikrotik
                    string Queue = await Task.Run(() => mikrotik.VerVelocidadQueue(objUsuario.Usuario));
                    if (Queue == string.Empty)
                    {
                        obj.UpdateEstatusGeneral(objUsuario.Id, "Eliminado", 1).Wait();

                        MessageBox.Show("No se encontro el usuario en el Mikrotik seleccionado, es posible que haya sido eliminado previamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BuscarServicios();
                        return false;
                    }
                    return true;
                }
                else
                {
                    //Primero revisamos si el servicio aun existe en el mikrotik
                    var lista = await Task.Run(() => mikrotik.VerFibra(objUsuario.Usuario)
                              .OrderBy(x => x.comment)
                              .ToList());
                    if (lista == null || lista.Count == 0)
                    {
                        obj.UpdateEstatusGeneral(objUsuario.Id, "Eliminado", 1).Wait();

                        MessageBox.Show("No se encontro el usuario en el Mikrotik seleccionado, es posible que haya sido eliminado previamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BuscarServicios();
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (mikrotik != null)
                {
                    await Task.Run(() => mikrotik.Close());
                }
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 0;
            }
        }
        public async Task CambiarEstatus(ListUsuariosGeneralModel objUsuario)
        {
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
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
                if (mikrotik != null)
                {
                    await Task.Run(() => mikrotik.Close());
                    mikrotik = null;
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
                    //Primero revisamos si el servicio aun existe en el mikrotik
                    string Queue = await Task.Run(() => mikrotik.VerVelocidadQueue(objUsuario.Usuario));
                    if (Queue == string.Empty)
                    {
                        obj.UpdateEstatusGeneral(objUsuario.Id, "Eliminado", 1).Wait();

                        MessageBox.Show("No se encontro el usuario en el Mikrotik seleccionado, es posible que haya sido eliminado previamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BuscarServicios();
                        return;
                    }
                    Result1 = mikrotik.CambiarEstatusAntena(objUsuario.IdInterno, objUsuario.Estatus);
                    Result2 = mikrotik.CambiarEstatusQueues(objUsuario.Usuario, objUsuario.Estatus);
                }
                else
                {
                    //Primero revisamos si el servicio aun existe en el mikrotik
                    var lista = await Task.Run(() => mikrotik.VerFibra(objUsuario.Usuario)
                              .OrderBy(x => x.comment)
                              .ToList());
                    if (lista == null || lista.Count == 0)
                    {
                        obj.UpdateEstatusGeneral(objUsuario.Id, "Eliminado", 1).Wait();

                        MessageBox.Show("No se encontro el usuario en el Mikrotik seleccionado, es posible que haya sido eliminado previamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BuscarServicios();
                        return;
                    }
                    Result1 = mikrotik.CambiarEstatusFibra(objUsuario.IdInterno, objUsuario.Estatus);
                    Result2 = true;
                }
                if (Result1 == true && Result2 == true)
                {
                    string nuevoEstatus = objUsuario.Estatus == "Activo" ? "Inactivo" : "Activo";
                    var Res = await obj.UpdateEstatusGeneral(objUsuario.Id, nuevoEstatus, IdUsuario);
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
            }
        }

        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            string nombrePlan = string.Empty;
            Planes p = new Planes();
            p.IdUsuario = IdUsuario;
            p.PorUsuarios = true;
            p.Tipo = string.Empty;
            if (p.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            AppRepository obj = new AppRepository();
            var plan = obj.GetPlanById(p.IdSeleccionado).Result;

            MikrotiksDisponibles m = new MikrotiksDisponibles();
            m.IdPlan = p.IdSeleccionado;
            if (m.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string comment = string.Empty;
            if (plan.IsAntena)
            {
                var listacomments = await Task.Run(() => obj.GetCommentsActivos(m.IdMikrotik));
                if (listacomments.Count == 0)
                {
                    MessageBox.Show("No se encontraron commments activos en el mikrotik seleccionado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                comment = listacomments.First().Nombre;
            }
            Ubicacion ub = new Ubicacion();
            ub.NuevoServicio = true;
            ub.IdUsuario = 0;
            ub.IdMikrotik = m.IdMikrotik;
            if (ub.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            try
            {
                btnNuevo.Enabled= false;
                if (mikrotik != null)
                {
                    await Task.Run(() => mikrotik.Close());
                    mikrotik = null;
                }
                MikrotikModel mikro = new MikrotikModel();
                mikro = obj.GetMikrotikById(m.IdMikrotik).Result;
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
                int contador = DGVServicios.Rows.Count + 1;
                int IdUsuarioN = 0;
                if (plan.IsAntena == true)
                {
                reintentarQueue:
                    string ExisteEnQueue = string.Empty;
                    ExisteEnQueue = mikrotik.VerIdQueue(NombreCliente + contador.ToString());
                    if (ExisteEnQueue != string.Empty)
                    {
                        contador += 1;
                        goto reintentarQueue;
                    }
                    List<Antenas> ExisteEnAntenas = new List<Antenas>();
                    ExisteEnAntenas = mikrotik.VerAntenasbyComment(NombreCliente + contador.ToString());
                    if (ExisteEnAntenas.Count() > 0)
                    {
                        contador += 1;
                        goto reintentarQueue;
                    }
                buscaotraipAntena:
                    var IPDisponible = obj.GetIPDisponible(m.IdMikrotik, true);
                    if (IPDisponible.Result != string.Empty)
                    {
                        //Checamos que no exista el ip que continua, si existe mandaremos una mensaje para que lo revisen
                        ExisteEnQueue = mikrotik.VerIdQueuebyAddress(IPDisponible.Result);//Se extrae el id del queues
                        ExisteEnAntenas = mikrotik.VerAntenasbyAddress(IPDisponible.Result);
                        if (ExisteEnAntenas.Count() == 0 && ExisteEnQueue != string.Empty)//No existe en firewall pero si en queue
                        {
                            MessageBox.Show("En el recorrido de las ips se encontro un error logico, en quest existe la ip " + IPDisponible.Result + " pero en firewall no se encontro cohincidencia, perteneciente al mikrotik " + m.Nombre + ", se cancela la solicitud", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (ExisteEnAntenas.Count() > 0) //Si existe en firewall
                        {
                            HistorialMovimientosModel H = new HistorialMovimientosModel
                            {
                                Id = 0,
                                Descripcion = "Ya se encuentra registrado el ip " + IPDisponible.Result + " para antena, en el mikrotik " + m.Nombre + " y no esta informado el sistema favor de actualizar, se procedera a guardarlo en el sistema, favor de revisar",
                                Pagina = "Servicio cliente",
                                IdUsuario = 1,
                                Estatus = true
                            };
                            await obj.SaveHistorialMovimientos(H);
                            //Insertamos el encontrado para que mas tarde lo revise el administrador y tambien para que no cuente para nuestra busqueda
                            if (ExisteEnAntenas.First().velocidad == string.Empty)
                            {
                                H = new HistorialMovimientosModel
                                {
                                    Id = 0,
                                    Descripcion = "La ip " + IPDisponible.Result + " no se encuentra registrada en el sistema, y no se encontro velocidad designada, se procedera a guardarlo en el sistema con velocidad de 1k/1k, favor de revisar",
                                    Pagina = "Servicio cliente",
                                    IdUsuario = 1,
                                    Estatus = true
                                };    //solo quedara registrado en el sistema mas no afectara a mikrotik
                                await obj.SaveHistorialMovimientos(H);
                            }
                            PlanModel objPlan = new PlanModel();
                            objPlan.Velocidad = ExisteEnAntenas.First().velocidad == string.Empty ? "1k/1k" : ExisteEnAntenas.First().velocidad;
                            objPlan.IsAntena = true;
                            var result = obj.SavePlanByMigracion(objPlan);
                            if (result.Result == 0)
                            {
                                MessageBox.Show("No se logro guardar el plan para la solicitud asignada en la base de datos favor de revisar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            objPlan.Id = result.Result;
                            PlanAnidadoModel objAnidado = new PlanAnidadoModel();
                            objAnidado.IdMikrotik = m.IdMikrotik;
                            objAnidado.IdPlanInterno = string.Empty;
                            objAnidado.IdPlan = objPlan.Id;
                            objAnidado.IsAntena = true;
                            objAnidado.Id = 0;
                            var ress = obj.SavePlanAnidadoByMigracion(objAnidado);
                            UsuariosGeneralModel objuser = new UsuariosGeneralModel();
                            objuser.IdMikrotik = m.IdMikrotik;
                            objuser.Nombre = ExisteEnAntenas.First().comment;
                            objuser.Address = IPDisponible.Result;
                            objuser.IdInterno = ExisteEnAntenas.First().id;
                            objuser.Estatus = ExisteEnAntenas.First().estatus;
                            objuser.Id = 0;
                            objuser.IdPlan = objPlan.Id;
                            var res = obj.SaveUsuariosGeneral(objuser, 1).Result;

                            goto buscaotraipAntena;
                        }
                        else
                        {
                            PlanAnidadoModel objAnidado = new PlanAnidadoModel();
                            objAnidado.IdMikrotik = m.IdMikrotik;
                            objAnidado.IdPlanInterno = string.Empty;
                            objAnidado.IdPlan = plan.Id;
                            objAnidado.IsAntena = true;
                            objAnidado.Id = 0;
                            var ress = obj.SavePlanAnidadoByMigracion(objAnidado);

                            //Insertamos en mikrotik
                            bool r = mikrotik.CrearSimpleQueue(NombreCliente + contador.ToString(), IPDisponible.Result, plan.Velocidad, comment);
                            bool r2 = mikrotik.AgregarAntena(comment, IPDisponible.Result, NombreCliente + contador.ToString(), true);
                            ExisteEnAntenas = new List<Antenas>();
                            ExisteEnAntenas = mikrotik.VerAntenasbyAddress(IPDisponible.Result);
                            UsuariosGeneralModel objuser = new UsuariosGeneralModel();
                            objuser.IdMikrotik = m.IdMikrotik;
                            objuser.Nombre = NombreCliente + contador.ToString();
                            objuser.Address = IPDisponible.Result;
                            objuser.IdInterno = ExisteEnAntenas.First().id;
                            objuser.Estatus ="Inactivo";
                            objuser.Id = 0;
                            objuser.IdPlan = plan.Id;
                            IdUsuarioN = obj.SaveUsuariosNuevo(objuser, IdUsuario, IdCliente).Result;
                            mikrotik.CambiarEstatusAntena(objuser.IdInterno, "Activo");
                            mikrotik.CambiarEstatusQueues(objuser.Nombre, "Activo");
                            HistorialMovimientosModel H = new HistorialMovimientosModel
                            {
                                Id = 0,
                                Descripcion = "Se creo el usuario " + objuser.Nombre + " en antenas",
                                Pagina = "Servicio cliente",
                                IdUsuario = IdUsuario,
                                Estatus = false
                            };
                        }
                    }
                    else
                    {
                    NuevaIpAddres:
                        //Se acabaron las ips disponibles de esa serie 
                        var IPDisponibleAddress = obj.GetIPDisponibleAdresslist(m.IdMikrotik, true);
                        var ExisteAddresList = mikrotik.VerAddresbyAddress(IPDisponibleAddress.Result);
                        string IpExist = obj.GetIPExist(m.IdMikrotik, true, IPDisponibleAddress.Result).Result;
                        if (IpExist == string.Empty && ExisteAddresList.ToList().Count() > 0)
                        {
                            //No existe en la base pero si en el mikrotik
                            //Lo introduciremos para que lo saltemos y no recorreremos su serie
                            InsertListWirelessModel model = new InsertListWirelessModel
                            {
                                IdMikrotik = m.IdMikrotik,
                                Address = IPDisponibleAddress.Result,
                                Comment = ExisteAddresList.First().comment,
                                Estatus = ExisteAddresList.First().estatus,
                                IdInterno = ExisteAddresList.First().id,
                                Completado = true
                            };
                            await obj.SaveWireless(model);
                            HistorialMovimientosModel H = new HistorialMovimientosModel
                            {
                                Id = 0,
                                Descripcion = "La ip " + IPDisponibleAddress.Result + " se encontro en el addres list del mikrotik " + m.Nombre + " pero no esta registrado en la base, se agregara a la base de forma automatica",
                                Pagina = "Servicio cliente",
                                IdUsuario = 1,
                                Estatus = false
                            };
                            await obj.SaveHistorialMovimientos(H);
                            goto NuevaIpAddres;
                        }
                        if (ExisteAddresList.ToList().Count() == 0)
                        {
                            //No existe en el mikrotik se procede a instroducirlo
                            var result = mikrotik.AgregarIPAddress(IPDisponibleAddress.Result, "LAN_ServiciosCliente" + IdCliente.ToString(), "LAN_ServiciosCliente" + IdCliente.ToString());
                            string text = result == true ? "La ip " + IPDisponibleAddress.Result + " no se encontro en el addres list del mikrotik " + m.Nombre + ", se agregara a la base e introducira en el mikrotik de forma automatica" :
                                "La ip " + IPDisponibleAddress.Result + " no se logro introducir en el addres list del mikrotik " + m.Nombre;
                            bool Estatushistory = result == true ? false : true;
                            HistorialMovimientosModel H = new HistorialMovimientosModel
                            {
                                Id = 0,
                                Descripcion = text,
                                Pagina = "Servicio cliente",
                                IdUsuario = 1,
                                Estatus = Estatushistory
                            };
                            await obj.SaveHistorialMovimientos(H);
                            if (Estatushistory == true)
                            {
                                MessageBox.Show("No se logro introducir la ip en el addres list del mikrotik " + m.Nombre + ", se cancela la solicitud", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            else
                            {
                                goto buscaotraipAntena;
                            }
                        }
                        if (IpExist != string.Empty && ExisteAddresList.ToList().Count() > 0)
                        {
                            //Existe en el mikrotik y tambien en la base
                            goto buscaotraipAntena;
                        }
                    }
                }
                else
                {
                    PasswordFibra pass = new PasswordFibra();
                    if (pass.ShowDialog() != DialogResult.OK)
                    {
                        MessageBox.Show("Cancelado por el usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                reintentarfibra:
                    List<Fibra> ExisteEnFibra = mikrotik.VerFibra(NombreCliente + contador.ToString());
                    if (ExisteEnFibra.Count() > 0)
                    {
                        contador += 1;
                        goto reintentarfibra;
                    }
                buscaotraipFibra:
                    var IPDisponibleFibra = obj.GetIPDisponible(m.IdMikrotik, false);

                    if (IPDisponibleFibra.Result != string.Empty)
                    {
                        ExisteEnFibra = mikrotik.VerFibrabyAddress(IPDisponibleFibra.Result);//Se extrae el id del queues
                        if (ExisteEnFibra.Count() > 0) //Ya existe en secret
                        {
                            HistorialMovimientosModel H = new HistorialMovimientosModel
                            {
                                Id = 0,
                                Descripcion = "Ya se encuentra registrado el ip " + IPDisponibleFibra.Result + " para fibra, en el mikrotik " + m.Nombre + " y no esta informado el sistema favor de actualizar, se procedera a guardarlo en el sistema, favor de revisar",
                                Pagina = "Servicio automatico de planes",
                                IdUsuario = 1,
                                Estatus = true
                            };
                            await obj.SaveHistorialMovimientos(H);

                            PlanModel objPlan = new PlanModel();
                            objPlan.Velocidad = ExisteEnFibra.First().velocidad == string.Empty ? "1k/1k" : ExisteEnFibra.First().velocidad;
                            objPlan.IsAntena = false;
                            var result = obj.SavePlanByMigracion(objPlan);
                            if (result.Result == 0)
                            {
                                MessageBox.Show("No se logro guardar el plan para la solicitud asignada en la base de datos favor de revisar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            objPlan.Id = result.Result;
                            PlanAnidadoModel objAnidado = new PlanAnidadoModel();
                            objAnidado.IdMikrotik = m.IdMikrotik;
                            objAnidado.IdPlanInterno = ExisteEnFibra.First().idplan;
                            objAnidado.IdPlan = objPlan.Id;
                            objAnidado.IsAntena = false;
                            objAnidado.Id = 0;
                            var ress = obj.SavePlanAnidadoByMigracion(objAnidado);
                            UsuariosGeneralModel objuser = new UsuariosGeneralModel();
                            objuser.IdMikrotik = m.IdMikrotik;
                            objuser.Nombre = ExisteEnFibra.First().comment;
                            objuser.Address = IPDisponibleFibra.Result;
                            objuser.IdInterno = ExisteEnFibra.First().id;
                            objuser.Estatus = ExisteEnFibra.First().estatus;
                            objuser.Id = 0;
                            objuser.IdPlan = objPlan.Id;
                            var res = obj.SaveUsuariosGeneral(objuser, 1).Result;

                            goto buscaotraipFibra;
                        }
                        else
                        {
                            //No existe en el mikrotik ahora si podemos meter el nuevo ip
                            //Insertamos en mikrotik
                            string idCreado = mikrotik.CrearFibra(NombreCliente + contador.ToString(), IPDisponibleFibra.Result, plan.Nombre, pass.Password);

                            string IdPlanInterno = mikrotik.BuscarPerfil(plan.Nombre);
                            if (IdPlanInterno == string.Empty)
                            {
                                MessageBox.Show("No se logro extraer el perfil del plan para la solicitud asignada en el mikrotik, es posible que lo hayan borrado fuera del sistema. Favor de revisar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            UsuariosGeneralModel objuser = new UsuariosGeneralModel();
                            objuser.IdMikrotik = m.IdMikrotik;
                            objuser.Nombre = NombreCliente + contador.ToString();
                            objuser.Address = IPDisponibleFibra.Result;
                            objuser.IdInterno = idCreado;
                            objuser.Estatus = "Inactivo";
                            objuser.Id = 0;
                            objuser.IdPlan = plan.Id;
                            IdUsuarioN = obj.SaveUsuariosNuevo(objuser, IdUsuario, IdCliente).Result;
                            mikrotik.CambiarEstatusFibra(objuser.IdInterno, "Activo");
                            HistorialMovimientosModel H = new HistorialMovimientosModel
                            {
                                Id = 0,
                                Descripcion = "Se creo el usuario " + objuser.Nombre + " en fibras",
                                Pagina = "Servicio cliente",
                                IdUsuario = IdUsuario,
                                Estatus = false
                            };
                        }
                    }
                    else
                    {
                    NuevaIpAddressFibra:
                        //Se acabaron las ips disponibles de esa serie 
                        var IPDisponibleAddress = obj.GetIPDisponibleAdresslist(m.IdMikrotik, false);
                        var ExisteAddresList = mikrotik.BuscarPoolbyAddress(IPDisponibleAddress.Result);
                        string IpExist = obj.GetIPExist(m.IdMikrotik, false, IPDisponibleAddress.Result).Result;

                        if (IpExist == string.Empty && ExisteAddresList == true)
                        {
                            //No existe en la base pero si en el mikrotik
                            //Lo introduciremos para que lo saltemos y no recorreremos su serie
                            await obj.SavePool(m.IdMikrotik, IPDisponibleAddress.Result, true);
                            HistorialMovimientosModel H = new HistorialMovimientosModel
                            {
                                Id = 0,
                                Descripcion = "La ip " + IPDisponibleAddress.Result + " se encontro en el addres list del mikrotik " + m.Nombre + " pero no esta registrado en la base, se agregara a la base de forma automatica",
                                Pagina = "Servicio cliente",
                                IdUsuario = 1,
                                Estatus = false
                            };
                            await obj.SaveHistorialMovimientos(H);
                            goto NuevaIpAddressFibra;
                        }
                        if (ExisteAddresList == false)
                        {
                            //No existe en el mikrotik se procede a instroducirlo
                            var result = mikrotik.AgregarPool(IPDisponibleAddress.Result);
                            string text = result == true ? "La ip " + IPDisponibleAddress.Result + " no se encontro en el pool del mikrotik " + m.Nombre + ", se agregara a la base e introducira en el mikrotik de forma automatica" :
                                "La ip " + IPDisponibleAddress.Result + " no se logro introducir en el pool del mikrotik " + m.Nombre;
                            bool Estatushistory = result == true ? false : true;
                            HistorialMovimientosModel H = new HistorialMovimientosModel
                            {
                                Id = 0,
                                Descripcion = text,
                                Pagina = "Servicio automatico de planes",
                                IdUsuario = 1,
                                Estatus = Estatushistory
                            };
                            await obj.SaveHistorialMovimientos(H);
                            if (Estatushistory == true)
                            {
                                MessageBox.Show("No se logro introducir la ip en el pool del mikrotik " + m.Nombre  + ", se cancela la solicitud", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            else
                            {
                                goto buscaotraipFibra;
                            }
                        }
                        if (IpExist != string.Empty && ExisteAddresList == true)
                        {
                            //Existe en el mikrotik y tambien en la base
                            goto buscaotraipFibra;
                        }
                    }
                }
                if(IdUsuarioN == 0)
                 {
                    MessageBox.Show("Error al guardar al usuario nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    ub.ub.IdUsuario = IdUsuarioN;
                    if (obj.SaveUbicacion(ub.ub).Result == true)
                    {
                        MessageBox.Show("Guardado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error al guardar la ubicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                BuscarServicios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 0;
                btnNuevo.Enabled = true;
            }
        }
    }
}
