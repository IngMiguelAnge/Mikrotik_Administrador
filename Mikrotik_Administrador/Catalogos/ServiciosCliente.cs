using GMap.NET.MapProviders;
using Mikrotik_Administrador.Catalogos;
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
        public int IdResponsable { get; set; }
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
                HeaderText = "Servicio",
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
            DataGridViewButtonColumn btnPlan = new DataGridViewButtonColumn
            {
                Name = "btnPlan",
                HeaderText = "Acción",
                Text = "Cambiar plan",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            DGVServicios.Columns.Add(btnPlan);
            DataGridViewButtonColumn btnCambio = new DataGridViewButtonColumn
            {
                Name = "btnCambio",
                HeaderText = "Acción",
                Text = "Nuevo cambio",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            DGVServicios.Columns.Add(btnCambio);
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
                case "btnCambio":
                    if (Estatus == "Eliminado")
                    {
                        MessageBox.Show("Este servicio se encuentra ya eliminado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    TiempoDefinido TD = new TiempoDefinido();
                    TD.Id = 0;
                    TD.IdUsuarioM = objUsuario.Id;
                    TD.ShowDialog();
                    if (TD.DialogResult == DialogResult.OK)
                    {
                        AppRepository obj = new AppRepository();
                        var mensualidadAfectadas = await obj.GetMensualidadesAfectadas(objUsuario.Id, TD.FechaInicio, TD.FechaFin);
                        foreach (var mens in mensualidadAfectadas)
                        {
                            // Para CADA mensualidad afectada (ej. su propio FechaInicio y FechaLimite), 
                            // recalculamos sus tramos limpios
                            var tramosCalculados = await CalcularTramosMensualidadAsync(objUsuario.Id, mens.FechaInicio, mens.FechaLimite);

                            // Sumamos el costo total de los tramos que cayeron dentro de esta mensualidad
                            decimal costoTotalMensualidad = tramosCalculados.Sum(t => t.Costo);

                            // Actualizamos el costo final de ESTA mensualidad en la base de datos
                            mens.Mensualidad = costoTotalMensualidad;
                            await obj.SaveMensualidad(mens);
                        }
                        BuscarServicios();
                    }
                    break;
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
                    AppRepository objM = new AppRepository();
                    var list = objM.GetExistMensualidadesbyUserM(Id).Result;
                    if(list == null || list.Count() <= 0)
                    {
                        IniciarPagos ini = new IniciarPagos();
                        ini.IdMensualidad = 0;
                        ini.IdUsuarioM = Id;
                        ini.IdResponsable = IdResponsable;
                        if (ini.ShowDialog() != DialogResult.OK)
                        { return; }
                    }
                  
                    await CambiarEstatus(objUsuario);
                    break;

                case "btnPlan":
                    MessageBox.Show("Se estan trabajando mejoras.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
               
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
                    string Queue = await Task.Run(() => mikrotik.VerVelocidadQueue(objUsuario.Address));
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
                    string Queue = await Task.Run(() => mikrotik.VerVelocidadQueue(objUsuario.Address));
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
                    var Res = await obj.UpdateEstatusGeneral(objUsuario.Id, nuevoEstatus, IdResponsable);
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
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            PreregistroCliente m = new PreregistroCliente();
            m.IdCliente = IdCliente;
            m.IdResponsable = IdResponsable;
            m.ShowDialog();
            BuscarServicios();          
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
