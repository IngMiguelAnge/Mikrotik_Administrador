using Mikrotik_Administrador.Class;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Items;
using Mikrotik_Administrador.Model;
using Mikrotik_Administrador.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
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
            DataGridViewButtonColumn btnPlan = new DataGridViewButtonColumn
            {
                Name = "btnPlan",
                HeaderText = "Acción",
                Text = "Cambiar Plan",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            DGVServicios.Columns.Add(btnPlan);
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
                Name = "BtnEstatus",
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
                    ListUsuariosGeneralModel objUsuario = new ListUsuariosGeneralModel();
                    objUsuario.Id = Id;
                    objUsuario.IdMikrotik = (int)DGVServicios.Rows[e.RowIndex].Cells["IdMikrotik"].Value;
                    objUsuario.IdInterno = (string)DGVServicios.Rows[e.RowIndex].Cells["IdInterno"].Value;
                    objUsuario.Usuario = (string)DGVServicios.Rows[e.RowIndex].Cells["Usuario"].Value;
                    objUsuario.Estatus = (string)DGVServicios.Rows[e.RowIndex].Cells["Estatus"].Value;
                    objUsuario.Tipo = (string)DGVServicios.Rows[e.RowIndex].Cells["Tipo"].Value;

                    await CambiarEstatus(objUsuario);
                    break;

                case "btnPlan":
                    int IdPlan = (int)DGVServicios.Rows[e.RowIndex].Cells["IdPlan"].Value;
                    Planes p = new Planes();
                    p.PorUsuarios = true;
                    p.Tipo = (string)DGVServicios.Rows[e.RowIndex].Cells["Tipo"].Value;
                    p.IdMikrotik = (int)DGVServicios.Rows[e.RowIndex].Cells["IdMikrotik"].Value;
                    if (p.ShowDialog() == DialogResult.OK)
                    {                
                        TiempoDefinido td = new TiempoDefinido();
                        td.FechaInicio = DGVServicios.Rows[e.RowIndex].Cells["MinFechaInicio"].Value == DBNull.Value || DGVServicios.Rows[e.RowIndex].Cells["MinFechaInicio"].Value == null
                            ? (DateTime?)null : Convert.ToDateTime(DGVServicios.Rows[e.RowIndex].Cells["MinFechaInicio"].Value);
                        td.FechaFin = DGVServicios.Rows[e.RowIndex].Cells["MaxFechaFin"].Value == DBNull.Value || DGVServicios.Rows[e.RowIndex].Cells["MaxFechaFin"].Value == null
                            ? (DateTime?)null : Convert.ToDateTime(DGVServicios.Rows[e.RowIndex].Cells["MaxFechaFin"].Value);
                       
                        if (td.ShowDialog() == DialogResult.Cancel)
                        {
                            MessageBox.Show("Se cancelo el cambio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if(IdPlan == p.IdSeleccionado) //No tiene caso designar el mismo plan
                        {
                            MessageBox.Show("No se puede asignar el mismo plan, por favor seleccione otro plan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            IdPlan = p.IdSeleccionado
                        };
                        AppRepository obj = new AppRepository();
                        var result = obj.SaveTiempoCambio(TD);
                        MessageBox.Show("Se ha enviado la solicitud de cambio de plan satisfactoriamente.", "Resultado de cambio de plan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BuscarServicios();
                    }
                    break;
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
            }
        }
    }
}
