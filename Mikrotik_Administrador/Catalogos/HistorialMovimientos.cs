using Mikrotik_Administrador.Class;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Items;
using Mikrotik_Administrador.Model;
using Mikrotik_Administrador.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador.Catalogos
{
    public partial class HistorialMovimientos : Form
    {
        public bool Urgente = false;
        MK mikrotik;
        public HistorialMovimientos()
        {
            InitializeComponent();
        }
        public void CrearGridView()
        {
            dgvHistorial.Columns.Clear();
            dgvHistorial.AutoGenerateColumns = false;
            dgvHistorial.EnableHeadersVisualStyles = false;

            // --- ALTO FIJO Y SALTO DE LÍNEA A 2 FILAS DE TEXTO ---
            // Alto fijo de 52px para 2 líneas de texto sin costo de procesamiento en la UI
            dgvHistorial.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvHistorial.RowTemplate.Height = 52;
            dgvHistorial.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // --- ESTILO DE LOS TÍTULOS (HEADERS) ---
            dgvHistorial.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            dgvHistorial.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvHistorial.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            dgvHistorial.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dgvHistorial.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            dgvHistorial.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);

            // --- COLUMNAS CON ANCHO DEFINIDO (OPTIMIZADAS) ---
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Visible = false,
                ReadOnly = true
            });

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Usuario",
                HeaderText = "Responsable",
                DataPropertyName = "Usuario",
                ReadOnly = true,
                Width = 130,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Pagina",
                HeaderText = "Página",
                DataPropertyName = "Pagina",
                ReadOnly = true,
                Width = 120,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaCreacion",
                HeaderText = "Fecha",
                DataPropertyName = "FechaCreacion",
                ReadOnly = true,
                Width = 140,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descripcion",
                HeaderText = "Descripción",
                DataPropertyName = "Descripcion",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, // Se expande en el espacio sobrante
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                HeaderText = "Estatus",
                DataPropertyName = "Estatus",
                ReadOnly = true,
                Width = 100,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdMikrotik",
                HeaderText = "IdMikrotik",
                DataPropertyName = "IdMikrotik",
                ReadOnly = false,
                Visible = false,
                Width = 100,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Address",
                HeaderText = "Address",
                DataPropertyName = "Address",
                ReadOnly = false,
                Visible = false,
                Width = 100,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Comment",
                HeaderText = "Comment",
                DataPropertyName = "Comment",
                ReadOnly = false,
                Visible = false,
                Width = 100,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IsAntena",
                HeaderText = "IsAntena",
                DataPropertyName = "IsAntena",
                ReadOnly = false,
                Visible = false,
                Width = 100,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DataGridViewButtonColumn btnCambiar = new DataGridViewButtonColumn
            {
                Name = "btnCambiar",
                HeaderText = "Acción",
                Text = "Cambiar Estatus",
                UseColumnTextForButtonValue = true,
                Width = 130,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            dgvHistorial.Columns.Add(btnCambiar);
            DataGridViewButtonColumn btnRestaurar = new DataGridViewButtonColumn
            {
                Name = "btnRestaurar",
                HeaderText = "Acción",
                Text = "Restaurar",
                UseColumnTextForButtonValue = true,
                Width = 130,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            dgvHistorial.Columns.Add(btnRestaurar);

            dgvHistorial.AllowUserToAddRows = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Urgente = false;
            Buscar();
        }
        public async void Buscar()
        {
            CrearGridView();
            btnBuscar.Enabled = false;
            try
            {
                progressBar1.Style = ProgressBarStyle.Marquee;
                progressBar1.MarqueeAnimationSpeed = 30;
                AppRepository obj = new AppRepository();
                if (Urgente == false)
                {
                    var lista = await Task.Run(() => obj.GetHistorialMovimientos(dtpFechaInicio.Value, dtpFechaFinal.Value));
                    var listaFinal = lista?.ToList() ?? new List<ListHistorialMovimientosModel>();
                    dgvHistorial.DataSource = new SortableBindingList<ListHistorialMovimientosModel>(listaFinal);
                }
                else
                {
                    var lista = await Task.Run(() => obj.GetHistorialMovimientosUrgentes());
                    var listaFinal = lista?.ToList() ?? new List<ListHistorialMovimientosModel>();
                    dgvHistorial.DataSource = new SortableBindingList<ListHistorialMovimientosModel>(listaFinal);
                }
                if (dgvHistorial.Columns["Id"] != null)
                    dgvHistorial.Columns["Id"].Visible = false;
                if (dgvHistorial.Columns["IdMikrotik"] != null)
                    dgvHistorial.Columns["IdMikrotik"].Visible = false;
                if (dgvHistorial.Columns["Address"] != null)
                    dgvHistorial.Columns["Address"].Visible = false;
                if (dgvHistorial.Columns["Comment"] != null)
                    dgvHistorial.Columns["Comment"].Visible = false;
                if (dgvHistorial.Columns["IsAntena"] != null)
                    dgvHistorial.Columns["IsAntena"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 0;
                btnBuscar.Enabled = true;
            }
        }

        private async void dgvHistorial_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var Id = (int)dgvHistorial.Rows[e.RowIndex].Cells["Id"].Value;
            AppRepository m = new AppRepository();
            switch (dgvHistorial.Columns[e.ColumnIndex].Name)
            {
                case "btnCambiar":

                    bool result = m.UpdateEstatusHistorialMovimiento(Id).Result;
                    if (result == true)
                    {
                        MessageBox.Show("Estatus cambiado");
                        Buscar();
                    }
                    else
                        MessageBox.Show("Error al desactivar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    break;
                case "btnRestaurar":
                    var Pagina = (string)dgvHistorial.Rows[e.RowIndex].Cells["Pagina"].Value;
                    var Address = (string)dgvHistorial.Rows[e.RowIndex].Cells["Address"].Value;
                    var IdMikrotik = (int)dgvHistorial.Rows[e.RowIndex].Cells["IdMikrotik"].Value;
                    var IsAntena = (bool)dgvHistorial.Rows[e.RowIndex].Cells["IsAntena"].Value;
                    var NombreServicio = (string)dgvHistorial.Rows[e.RowIndex].Cells["Comment"].Value;
                    if (Address == string.Empty)
                    {
                        MessageBox.Show("Error esta opción es solo valida para la restauración de una ip borrada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    DialogResult resultado = MessageBox.Show("Este usuario no se encuentra registrado en el sistema, solo se recuperara dentro del mikrotik ¿Quiere continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    if (resultado == DialogResult.No)
                    {
                        return;
                    }
                    AppRepository obj = new AppRepository();
                    MikrotikModel mikro = new MikrotikModel();
                    mikro = obj.GetMikrotikById(IdMikrotik).Result;
                    string IdInternoNuevo = string.Empty;
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
                    if (IsAntena == true)
                    {
                        string ExisteEnQueue = mikrotik.VerIdQueue(NombreServicio);
                        if (ExisteEnQueue != string.Empty)
                        {
                            MessageBox.Show("Ya existe un servicio con el mismo nombre en el mikrotik seleccionado y no esta informado el sistema", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        List<Antenas> ExisteEnAntenas = new List<Antenas>();
                        ExisteEnAntenas = mikrotik.VerAntenasbyComment(NombreServicio);
                        if (ExisteEnAntenas.Count() > 0)
                        {
                            MessageBox.Show("Ya existe un servicio con el mismo nombre en el mikrotik seleccionado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        ExisteEnQueue = mikrotik.VerIdQueuebyAddress(Address);//Se extrae el id del queues
                        ExisteEnAntenas = mikrotik.VerAntenasbyAddress(Address);
                        if (ExisteEnAntenas.Count() > 0 || ExisteEnQueue != string.Empty)//No existe en firewall pero si en queue
                        {
                            MessageBox.Show(
                                 "Ya se encuentra registrado el ip " + Address + " para antena, en el mikrotik " + mikro.Nombre + " y no esta informado el sistema favor de revisar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        var listacomments = await Task.Run(() => obj.GetCommentsActivos(IdMikrotik));
                        if (listacomments.Count == 0)
                        {
                            MessageBox.Show("No se encontraron commments activos en el mikrotik seleccionado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        Programar pr = new Programar();
                        pr.listComments = listacomments.ToList();
                        pr.ShowDialog();
                        if (pr.SePrograma == string.Empty)
                        {
                            MessageBox.Show("No se selecciono un comment para el servicio a restaurar, favor de seleccionar uno.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        PlanesyMikrotiks PM = new PlanesyMikrotiks();
                        PM.ShowDialog();
                        if (PM.IdMikrotik == 0)
                        {
                            MessageBox.Show("No se selecciono un plan de velocidad, favor de seleccionar uno.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if(PM.IdMikrotik != IdMikrotik)
                        {
                            MessageBox.Show("El plan de velocidad seleccionado no pertenece al mikrotik donde se va a restaurar el servicio, favor de seleccionar uno.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        NombreServicio = NombreServicio.Trim();
                        bool r = mikrotik.CrearSimpleQueue(NombreServicio, Address, PM.VelocidadElegida, pr.SePrograma);
                        bool r2 = mikrotik.AgregarAntena(pr.SePrograma, Address, NombreServicio, true);
                        ExisteEnAntenas = new List<Antenas>();
                        ExisteEnAntenas = mikrotik.VerAntenasbyAddress(Address);
                        IdInternoNuevo = ExisteEnAntenas.First().id;
                    }
                    else
                    {
                        //Procesos para introducir en fibra
                        List<Fibra> ExisteEnFibra = mikrotik.VerFibra(NombreServicio);
                        if (ExisteEnFibra.Count() > 0)
                        {
                            MessageBox.Show("Ya existe un servicio con el mismo nombre en el mikrotik seleccionado y no esta informado el sistema", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        ExisteEnFibra = mikrotik.VerFibrabyAddress(Address);
                        if (ExisteEnFibra.Count() > 0) //Ya existe en secret
                        {
                            MessageBox.Show(
                              "Ya se encuentra registrado el ip " + Address + " para antena, en el mikrotik " + mikro.Nombre + " y no esta informado el sistema favor de revisar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        PlanesyMikrotiks PM = new PlanesyMikrotiks();
                        PM.ShowDialog();
                        if (PM.IdMikrotik == 0)
                        {
                            MessageBox.Show("No se selecciono un plan de velocidad, favor de seleccionar uno.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (PM.IdMikrotik != IdMikrotik)
                        {
                            MessageBox.Show("El plan de velocidad seleccionado no pertenece al mikrotik donde se va a restaurar el servicio, favor de seleccionar uno.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        string IdPlanInterno = mikrotik.BuscarPerfil(PM.NombrePlanElegido);
                        if (IdPlanInterno == string.Empty)
                        {
                            MessageBox.Show("No se logro extraer el perfil del plan para la solicitud asignada en el mikrotik, es posible que lo hayan borrado fuera del sistema. Favor de revisar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        PasswordFibra pw = new PasswordFibra();
                        pw.ShowDialog();
                        NombreServicio = NombreServicio.Trim();
                        IdInternoNuevo = mikrotik.CrearFibra(NombreServicio, Address, PM.NombrePlanElegido, pw.Password);
                    }
                        obj.UpdateEstatusGeneralbyIdInterno(IdMikrotik, IdInternoNuevo, IsAntena, "Restauración", Address).Wait();
                    MessageBox.Show("El usuario a sido restaurado, en caso de estar registrado en el sistema revisar sus datos en asignaciones.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }

        private void btnUrgentes_Click(object sender, EventArgs e)
        {
            Urgente = true;
            Buscar();
        }
    }
}
