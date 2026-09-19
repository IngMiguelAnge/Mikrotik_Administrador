using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
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
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador.Catalogos
{
    public partial class PreregistroCliente : Form
    {
        public int IdCliente { get; set; }
        public int IdResponsable { get; set; }
        MK mikrotik;
        private int IdPlan;
        private decimal Costo;
        private int IdMikrotik;
        private string VelocidadPlan;
        private string NombrePlan;
        private bool Confirmado = false;
        GMarkerGoogle marcador;
        GMapOverlay capaMarcadores;
        public PreregistroCliente()
        {
            InitializeComponent();
        }

        private void btnLupa_Click(object sender, EventArgs e)
        {
            Mapa m = new Mapa();
            m.Latitud = txtLatitud.Text;
            m.Longitud = txtLongitud.Text;
            if (m.ShowDialog() == DialogResult.OK)
            {
                txtLatitud.Text = m.Latitud;
                txtLongitud.Text = m.Longitud;
                BuscarPorCoordenadas(Convert.ToDecimal(txtLatitud.Text), Convert.ToDecimal(txtLongitud.Text));
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtNombrePlan.Text.Trim() == "")
            {
                DialogResult resultado = MessageBox.Show("Ha dejado el campo vacio, esto buscara a todos los planes pero puede demorar ¿Quiere continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.No)
                {
                    return;
                }
            }
            BuscarPlanes();
        }
        public void CrearGridView()
        {
            dgvPlanes.Columns.Clear();
            dgvPlanes.AutoGenerateColumns = false;
            dgvPlanes.EnableHeadersVisualStyles = false;
            // --- ESTILO DE LOS TÍTULOS (HEADERS) CON TU AZUL LOGO ---
            dgvPlanes.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            dgvPlanes.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvPlanes.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            dgvPlanes.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dgvPlanes.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            dgvPlanes.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);

            dgvPlanes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvPlanes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvPlanes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Precio",
                HeaderText = "Precio",
                DataPropertyName = "Precio",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvPlanes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Velocidad",
                HeaderText = "Velocidad",
                DataPropertyName = "Velocidad",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvPlanes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PlanDe",
                HeaderText = "Tipo de plan",
                DataPropertyName = "PlanDe",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DataGridViewButtonColumn btnAsignar = new DataGridViewButtonColumn
            {
                Name = "btnAsignar",
                HeaderText = "Acción",
                Text = "Asignar",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            dgvPlanes.Columns.Add(btnAsignar);
            dgvPlanes.AllowUserToAddRows = false;
        }
        public void BuscarPlanes()
        {
            CrearGridView();
            btnBuscar.Enabled = false;
            btnBuscarCoordenadas.Enabled = false;
            btnLupa.Enabled = false;
            btnGuardar.Enabled = false;
            btnCancelarDireccion.Enabled = false;
            btnAceptarUbicacion.Enabled = false;
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            btnBuscar.Enabled = false;
            try
            {
                AppRepository obj = new AppRepository();
                var lista = obj.GetPlanesbyName(txtNombrePlan.Text, null, true).Result;
                var listaFinal = lista?.ToList() ?? new List<ListPlanesModel>();
                dgvPlanes.DataSource = new SortableBindingList<ListPlanesModel>(listaFinal);
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
                btnBuscarCoordenadas.Enabled = true;
                btnLupa.Enabled = true;
                btnGuardar.Enabled = Confirmado;
                btnCancelarDireccion.Enabled = Confirmado;
                btnAceptarUbicacion.Enabled = Confirmado;
            }
        }

        private async void dgvPlanes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Evitar errores si hacen click en el encabezado
            if (e.RowIndex < 0) return;
            int Id = (int)dgvPlanes.Rows[e.RowIndex].Cells["Id"].Value;
            string Plan = (string)dgvPlanes.Rows[e.RowIndex].Cells["Nombre"].Value;
            bool IsAntena = (bool)dgvPlanes.Rows[e.RowIndex].Cells["PlanDe"].Value.ToString().Contains("Antena");
            decimal Precio = (decimal)dgvPlanes.Rows[e.RowIndex].Cells["Precio"].Value;
            switch (dgvPlanes.Columns[e.ColumnIndex].Name)
            {
                case "btnAsignar":
                    if (Plan.Trim() == string.Empty)
                    {
                        MessageBox.Show("Solo se pueden asignar planes sin nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MikrotiksDisponibles m = new MikrotiksDisponibles();
                    m.IdPlan = Id;
                    if (m.ShowDialog() != DialogResult.OK)
                    {
                        MessageBox.Show("Debe seleccionar un mikrotik", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    txtMikrotik.Text = m.Nombre;
                    IdMikrotik = m.IdMikrotik;
                    string comment = string.Empty;
                    if (IsAntena)
                    {
                        lblPassword.Visible = false;
                        txtPassword.Visible = false;
                        AppRepository obj = new AppRepository();
                        var listacomments = await Task.Run(() => obj.GetCommentsActivos(m.IdMikrotik));
                        if (listacomments.Count == 0)
                        {
                            MessageBox.Show("No se encontraron commments activos en el mikrotik seleccionado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        comment = listacomments.First().Nombre;
                    }
                    else
                    {
                        lblPassword.Visible = true;
                        txtPassword.Visible = true;
                        txtPassword.Text = "1234";
                        MessageBox.Show("Este plan requerira de un password favor de escribirlo", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    IdPlan = Id;
                    Costo = Precio;
                    VelocidadPlan = (string)dgvPlanes.Rows[e.RowIndex].Cells["Velocidad"].Value;
                    NombrePlan = Plan;
                    foreach (DataGridViewRow r in dgvPlanes.Rows)
                    {
                        r.DefaultCellStyle.BackColor = Color.Empty; // Restablece al estilo global/predeterminado
                    }

                    // 2. Colorear la fila seleccionada
                    dgvPlanes.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.PaleGreen;

                    // 3. Opcional: Desmarcar la selección azul por defecto para que el verde se aprecie de inmediato
                    dgvPlanes.ClearSelection();
                    break;

            }
        }

        private void btnBuscarCoordenadas_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrWhiteSpace(txtDireccionOficial.Text)
               && CBCoordendadas.Checked == false)
               || ((string.IsNullOrWhiteSpace(txtLatitud.Text) ||
               string.IsNullOrWhiteSpace(txtLongitud.Text))
               && CBCoordendadas.Checked == true)
               )
            {
                MessageBox.Show("Por favor, ingresa una dirección o coordenadas para buscar.", "Datos insuficientes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // 1. Cambiar cursor a espera
            this.Cursor = Cursors.WaitCursor;
            btnBuscar.Enabled = false;
            btnBuscarCoordenadas.Enabled = false;
            btnLupa.Enabled = false;
            btnGuardar.Enabled = false;
            btnCancelarDireccion.Enabled = false;
            btnAceptarUbicacion.Enabled = false;

            try
            {
                if (!string.IsNullOrWhiteSpace(txtDireccionOficial.Text) && CBCoordendadas.Checked == false)
                    BuscarPorDireccion(txtDireccionOficial.Text);
                if (!string.IsNullOrWhiteSpace(txtLatitud.Text) && !string.IsNullOrWhiteSpace(txtLongitud.Text) && CBCoordendadas.Checked == true)
                    BuscarPorCoordenadas(Convert.ToDecimal(txtLatitud.Text),
                    Convert.ToDecimal(txtLongitud.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error revisar sus coordenadas " + ex.Message);
            }
            finally
            {
                // 2. Regresar cursor a la normalidad aunque haya error
                this.Cursor = Cursors.Default;
                if (txtLatitud.Text != string.Empty)
                {
                    btnAceptarUbicacion.Enabled = true;
                }
                else
                {
                    Confirmado = false;
                }
                btnBuscar.Enabled = true;
                btnBuscarCoordenadas.Enabled = true;
                btnLupa.Enabled = true;
                btnGuardar.Enabled = Confirmado;
                btnCancelarDireccion.Enabled = Confirmado;
            }
        }
        public void BuscarPorDireccion(string direccionBuscada)
        {
            PointLatLng? pos = BuscarEnOpenStreetMap(direccionBuscada);

            if (pos.HasValue)
            {
                if (marcador != null)
                {
                    marcador.Position = pos.Value;
                    Obtenerdireccion();
                }

                //Llenar tus cuadros de texto
                txtLatitud.Text = pos.Value.Lat.ToString();
                txtLongitud.Text = pos.Value.Lng.ToString();
            }
            else
            {
                MessageBox.Show("No se encontró: '" + direccionBuscada + "'. Intenta ser más específico (ej: Huixcolotla, Puebla, Mexico).", "Sin resultados");
            }
        }
        private PointLatLng? BuscarEnOpenStreetMap(string direccion)
        {
            try
            {
                // OpenStreetMap requiere un User-Agent para no bloquear la búsqueda
                WebClient client = new WebClient();
                client.Encoding = System.Text.Encoding.UTF8;
                client.Headers.Add("user-agent", "MiAppMikrotik/1.0 (administrador@correo.com)");

                // Creamos la URL de búsqueda (Nominatim)
                string url = $"https://nominatim.openstreetmap.org/search?format=json&q={Uri.EscapeDataString(direccion)}&limit=1";

                string json = client.DownloadString(url);

                // Si no hay resultados, el JSON viene como "[]"
                if (json.Length < 10) return null;

                // Para no instalar librerías de JSON extras, buscamos las coordenadas con texto (sucio pero efectivo)
                int latStart = json.IndexOf("\"lat\":\"") + 7;
                int latEnd = json.IndexOf("\"", latStart);
                string latStr = json.Substring(latStart, latEnd - latStart);

                int lonStart = json.IndexOf("\"lon\":\"") + 7;
                int lonEnd = json.IndexOf("\"", lonStart);
                string lonStr = json.Substring(lonStart, lonEnd - lonStart);

                double lat = double.Parse(latStr, System.Globalization.CultureInfo.InvariantCulture);
                double lon = double.Parse(lonStr, System.Globalization.CultureInfo.InvariantCulture);

                return new PointLatLng(lat, lon);
            }
            catch
            {
                return null;
            }
        }
        private void BuscarPorCoordenadas(decimal latitud, decimal longitud)
        {
            try
            {
                if (double.TryParse(txtLatitud.Text, out double lat) && double.TryParse(txtLongitud.Text, out double lng))
                {
                    PointLatLng pos = new PointLatLng(lat, lng);

                    if (marcador != null)
                    {
                        marcador.Position = pos;
                        Obtenerdireccion();
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, ingresa coordenadas numéricas válidas.", "Error de formato");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al posicionar las coordenadas: " + ex.Message);
            }
        }
        public void Obtenerdireccion()
        {
            // Obtenemos la dirección de la posición final del marcador
            string calleEncontrada = ObtenerCalleDesdeCoordenadas(marcador.Position.Lat, marcador.Position.Lng);

            // Lo ponemos en tu TextBox de dirección
            txtDireccionSugerida.Text = calleEncontrada;
            btnAceptarUbicacion.Enabled = true;
        }
        private string ObtenerCalleDesdeCoordenadas(double lat, double lon)
        {
            try
            {
                WebClient client = new WebClient();
                client.Encoding = System.Text.Encoding.UTF8;

                client.Headers.Add("user-agent", "MiAppMikrotik/1.0");

                string url = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                    "https://nominatim.openstreetmap.org/reverse?format=json&lat={0}&lon={1}&zoom=18&addressdetails=1", lat, lon);

                string json = client.DownloadString(url);

                if (json.Contains("\"display_name\":\""))
                {
                    int start = json.IndexOf("\"display_name\":\"") + 16;
                    int end = json.IndexOf("\"", start);
                    string direccionCompleta = json.Substring(start, end - start);

                    // Unescape para manejar los \u00f3 etc, y ahora con UTF8 se verá perfecto
                    return System.Text.RegularExpressions.Regex.Unescape(direccionCompleta);
                }
                return "Dirección no encontrada";
            }
            catch
            {
                return "Error al obtener dirección";
            }
        }

        private void btnAceptarUbicacion_Click(object sender, EventArgs e)
        {
            if (txtDireccionSugerida.Text.Trim() == string.Empty)
            {
                MessageBox.Show("No hay una dirección sugerida para aceptar, favor de mover el puntero del mapa.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Confirmado = true;
            btnGuardar.Enabled = true;
            txtDireccionOficial.Text = txtDireccionSugerida.Text;
            txtDireccionOficial.Enabled = false;
            btnCancelarDireccion.Enabled = true;
            btnAceptarUbicacion.Enabled = false;
        }

        private void btnCancelarDireccion_Click(object sender, EventArgs e)
        {
            Confirmado = false;
            btnGuardar.Enabled = false;
            txtDireccionOficial.Enabled = true;
            btnCancelarDireccion.Enabled = false;
            btnAceptarUbicacion.Enabled = true;
        }

        private void PreregistroCliente_Load(object sender, EventArgs e)
        {
            if(IdCliente != 0)
            {
                gbDatosCliente.Visible = false;
            }
           
            IdPlan = 0;
            Costo = 0;
            txtLatitud.Text = "18.68165869879";
            txtLongitud.Text = "-97.64837265014";
            // 1. Configurar el proveedor y modo (Internet)
            gMapOculto.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            double latitude = double.Parse(txtLatitud.Text, System.Globalization.CultureInfo.InvariantCulture);
            double longitude = double.Parse(txtLongitud.Text, System.Globalization.CultureInfo.InvariantCulture);

            // 2. Posición inicial (puedes poner las de tu ciudad)
            gMapOculto.Position = new PointLatLng(latitude, longitude); //Ciudad inicial Mexico
            gMapOculto.MinZoom = 2;
            gMapOculto.MaxZoom = 20;
            gMapOculto.Zoom = 18;

            // 3. Permitir mover el mapa con el botón derecho y el marcador con el izquierdo
            gMapOculto.DragButton = MouseButtons.Right;

            // 4. Crear la capa para los marcadores
            capaMarcadores = new GMapOverlay("capa1");
            gMapOculto.Overlays.Add(capaMarcadores);

            // 5. Crear el marcador inicial
            marcador = new GMarkerGoogle(gMapOculto.Position, GMarkerGoogleType.red_pushpin);
            marcador.IsVisible = true;
            capaMarcadores.Markers.Add(marcador);
            //el zoom siga al mouse.
            gMapOculto.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionWithoutCenter;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            int IdUsuarioN = 0;
            btnBuscar.Enabled = false;
            btnBuscarCoordenadas.Enabled = false;
            btnLupa.Enabled = false;
            btnGuardar.Enabled = false;
            btnCancelarDireccion.Enabled = false;
            btnAceptarUbicacion.Enabled = false;
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            try
            {
                if (gbDatosCliente.Visible == true && txtNombreCliente.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Por favor, completa todos los campos antes de guardar.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtDireccionOficial.Text.Trim() == string.Empty || txtLatitud.Text.Trim() == string.Empty || txtLongitud.Text.Trim() == string.Empty
                || txtNombreServicio.Text.Trim() == string.Empty || txtDireccion.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Por favor, completa todos los campos antes de guardar.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtPassword.Visible && txtPassword.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Por favor, ingresa un password para el plan seleccionado.", "Password requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                AppRepository obj = new AppRepository();
                if(gbDatosCliente.Visible == true)
                {
                    var result = obj.GetClientesbyName(txtNombreCliente.Text.Trim(), 0).Result;
                    if (result.Count > 0)
                    {
                        MessageBox.Show("Ya existe un cliente con el mismo nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            
                List<UbicacionesCercanas> listUbicaciones = obj.GetUbicacionesCercanas(txtLatitud.Text, txtLongitud.Text).Result.ToList();
                if (listUbicaciones.Count > 0)
                {
                    ListUbicacionesCercanas ubicacion = new ListUbicacionesCercanas();
                    ubicacion.listUbicaciones = listUbicaciones;
                    if (ubicacion.ShowDialog() != DialogResult.OK)
                    { return; }
                }
                if (IdPlan == 0)
                {
                    MessageBox.Show("Por favor, selecciona un plan antes de guardar.", "Plan no seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Costo == 0)
                {
                    MessageBox.Show("Por favor, selecciona un plan que tenga un costo disponible, para iniciar su servicio.", "Plan no seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bool IsAntena = txtPassword.Visible == true ? false : true;
                List<ListUsuariosGeneralModel> UsuariosGeneral = obj.GetUsuariosMikrotiksByName(txtNombreServicio.Text.Trim(), IdMikrotik, IsAntena).Result.ToList();
                if (UsuariosGeneral.Count > 0)
                {
                    MessageBox.Show("Ya existe un servicio con el mismo nombre en el mikrotik seleccionado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (IdMikrotik == 0)
                {
                    MessageBox.Show("No se detecto el mikrotik ha asignar.", "Mikrotik no seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string comment = string.Empty;
                if (txtPassword.Visible == false)//es antena si sale false
                {
                    var listacomments = await Task.Run(() => obj.GetCommentsActivos(IdMikrotik));
                    if (listacomments.Count == 0)
                    {
                        MessageBox.Show("No se encontraron commments activos en el mikrotik seleccionado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    comment = listacomments.First().Nombre;
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

                if (txtPassword.Visible == false)//es antena si sale false
                {
                    //Procesos para introducir en antena
                    string ExisteEnQueue = string.Empty;
                    ExisteEnQueue = mikrotik.VerIdQueue(txtNombreServicio.Text.Trim());
                    if (ExisteEnQueue != string.Empty)
                    {
                        MessageBox.Show("Ya existe un servicio con el mismo nombre en el mikrotik seleccionado y no esta informado el sistema", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    List<Antenas> ExisteEnAntenas = new List<Antenas>();
                    ExisteEnAntenas = mikrotik.VerAntenasbyComment(txtNombreServicio.Text.Trim());
                    if (ExisteEnAntenas.Count() > 0)
                    {
                        MessageBox.Show("Ya existe un servicio con el mismo nombre en el mikrotik seleccionado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                buscaotraipAntena:
                    var IPDisponible = obj.GetIPDisponible(IdMikrotik, true);
                    if (IPDisponible.Result != string.Empty)
                    {
                        //Checamos que no exista el ip que continua, si existe mandaremos una mensaje para que lo revisen
                        ExisteEnQueue = mikrotik.VerIdQueuebyAddress(IPDisponible.Result);//Se extrae el id del queues
                        ExisteEnAntenas = mikrotik.VerAntenasbyAddress(IPDisponible.Result);
                        if (ExisteEnAntenas.Count() == 0 && ExisteEnQueue != string.Empty)//No existe en firewall pero si en queue
                        {
                            MessageBox.Show("En el recorrido de las ips se encontro un error logico, en quest existe la ip " + IPDisponible.Result + " pero en firewall no se encontro cohincidencia, perteneciente al mikrotik " + txtMikrotik.Text + ", se cancela la solicitud", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (ExisteEnAntenas.Count() > 0) //Si existe en firewall
                        {
                            HistorialMovimientosModel H = new HistorialMovimientosModel
                            {
                                Id = 0,
                                Descripcion = "Ya se encuentra registrado el ip " + IPDisponible.Result + " para antena, en el mikrotik " + txtMikrotik.Text + " y no esta informado el sistema favor de actualizar, se procedera a guardarlo en el sistema, favor de revisar",
                                Pagina = "PreRegistroCliente",
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
                                    Pagina = "PreRegistroCliente",
                                    IdUsuario = 1,
                                    Estatus = true
                                };    //solo quedara registrado en el sistema mas no afectara a mikrotik
                                await obj.SaveHistorialMovimientos(H);
                            }
                            PlanModel objPlan = new PlanModel();
                            objPlan.Velocidad = ExisteEnAntenas.First().velocidad == string.Empty ? "1k/1k" : ExisteEnAntenas.First().velocidad;
                            objPlan.IsAntena = true;
                            var resultsave = obj.SavePlanByMigracion(objPlan);
                            if (resultsave.Result == 0)
                            {
                                MessageBox.Show("No se logro guardar el plan para la solicitud asignada en la base de datos favor de revisar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            objPlan.Id = resultsave.Result;
                            PlanAnidadoModel objAnidado = new PlanAnidadoModel();
                            objAnidado.IdMikrotik = IdMikrotik;
                            objAnidado.IdPlanInterno = string.Empty;
                            objAnidado.IdPlan = objPlan.Id;
                            objAnidado.IsAntena = true;
                            objAnidado.Id = 0;
                            var ress = obj.SavePlanAnidadoByMigracion(objAnidado);
                            UsuariosGeneralModel objuser = new UsuariosGeneralModel();
                            objuser.IdMikrotik = IdMikrotik;
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
                            if(gbDatosCliente.Visible == true)
                            {
                                ClienteModel cliente = new ClienteModel();
                                cliente.Id = 0;
                                cliente.Nombre = txtNombreCliente.Text.Trim();
                                cliente.Correo = txtCorreo.Text.Trim();
                                cliente.Telefono1 = txtTelefono1.Text.Trim();
                                cliente.Telefono2 = txtTelefono2.Text.Trim();
                                IdCliente = obj.SaveCliente(cliente).Result;
                                if (IdCliente == 0)
                                {
                                    MessageBox.Show("No se logro guardar el cliente para la solicitud asignada en la base de datos favor de revisar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                         
                            //No existe en firewall ni en queue, se procede a introducirlo
                            PlanAnidadoModel objAnidado = new PlanAnidadoModel();
                            objAnidado.IdMikrotik = IdMikrotik;
                            objAnidado.IdPlanInterno = string.Empty;
                            objAnidado.IdPlan = IdPlan;
                            objAnidado.IsAntena = true;
                            objAnidado.Id = 0;
                            var ress = obj.SavePlanAnidadoByMigracion(objAnidado);

                            //Insertamos en mikrotik
                            bool r = mikrotik.CrearSimpleQueue(txtNombreServicio.Text.Trim(), IPDisponible.Result, VelocidadPlan, comment);
                            bool r2 = mikrotik.AgregarAntena(comment, IPDisponible.Result, txtNombreServicio.Text.Trim(), true);
                            ExisteEnAntenas = new List<Antenas>();
                            ExisteEnAntenas = mikrotik.VerAntenasbyAddress(IPDisponible.Result);
                            UsuariosGeneralModel objuser = new UsuariosGeneralModel();
                            objuser.IdMikrotik = IdMikrotik;
                            objuser.Nombre = txtNombreServicio.Text.Trim();
                            objuser.Address = IPDisponible.Result;
                            objuser.IdInterno = ExisteEnAntenas.First().id;
                            objuser.Estatus = "Inactivo";
                            objuser.Id = 0;
                            objuser.IdPlan = IdPlan;
                            IdUsuarioN = obj.SaveUsuariosNuevo(objuser, IdResponsable, IdCliente).Result;
                            mikrotik.CambiarEstatusAntena(objuser.IdInterno, "Activo");
                            mikrotik.CambiarEstatusQueues(objuser.Nombre, "Activo");
                            HistorialMovimientosModel H = new HistorialMovimientosModel
                            {
                                Id = 0,
                                Descripcion = "Se creo el usuario " + objuser.Nombre + " en antenas",
                                Pagina = "PreRegistroCliente",
                                IdUsuario = IdResponsable,
                                Estatus = false
                            };
                        }
                    }
                    else
                    {
                    NuevaIpAddres:
                        //Se acabaron las ips disponibles de esa serie 
                        var IPDisponibleAddress = obj.GetIPDisponibleAdresslist(IdMikrotik, true);
                        var ExisteAddresList = mikrotik.VerAddresbyAddress(IPDisponibleAddress.Result);
                        string IpExist = obj.GetIPExist(IdMikrotik, true, IPDisponibleAddress.Result).Result;
                        if (IpExist == string.Empty && ExisteAddresList.ToList().Count() > 0)
                        {
                            //No existe en la base pero si en el mikrotik
                            //Lo introduciremos para que lo saltemos y no recorreremos su serie
                            InsertListWirelessModel model = new InsertListWirelessModel
                            {
                                IdMikrotik = IdMikrotik,
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
                                Descripcion = "La ip " + IPDisponibleAddress.Result + " se encontro en el addres list del mikrotik " + txtMikrotik.Text + " pero no esta registrado en la base, se agregara a la base de forma automatica",
                                Pagina = "PreRegistroCliente",
                                IdUsuario = 1,
                                Estatus = false
                            };
                            await obj.SaveHistorialMovimientos(H);
                            goto NuevaIpAddres;
                        }
                        if (ExisteAddresList.ToList().Count() == 0)
                        {
                            //No existe en el mikrotik se procede a instroducirlo
                            var resultaddres = mikrotik.AgregarIPAddress(IPDisponibleAddress.Result, "LAN_ServiciosCliente" + IdCliente.ToString(), "LAN_ServiciosCliente" + IdCliente.ToString());
                            string text = resultaddres == true ? "La ip " + IPDisponibleAddress.Result + " no se encontro en el addres list del mikrotik " + txtMikrotik.Text + ", se agregara a la base e introducira en el mikrotik de forma automatica" :
                                "La ip " + IPDisponibleAddress.Result + " no se logro introducir en el addres list del mikrotik " + txtMikrotik.Text;
                            bool Estatushistory = resultaddres == true ? false : true;
                            HistorialMovimientosModel H = new HistorialMovimientosModel
                            {
                                Id = 0,
                                Descripcion = text,
                                Pagina = "PreRegistroCliente",
                                IdUsuario = 1,
                                Estatus = Estatushistory
                            };
                            await obj.SaveHistorialMovimientos(H);
                            if (Estatushistory == true)
                            {
                                MessageBox.Show("No se logro introducir la ip en el addres list del mikrotik " + txtMikrotik.Text + ", se cancela la solicitud", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    //Procesos para introducir en fibra
                    List<Fibra> ExisteEnFibra = mikrotik.VerFibra(txtNombreServicio.Text.Trim());
                    if (ExisteEnFibra.Count() > 0)
                    {
                        MessageBox.Show("Ya existe un servicio con el mismo nombre en el mikrotik seleccionado y no esta informado el sistema", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                buscaotraipFibra:
                    var IPDisponibleFibra = obj.GetIPDisponible(IdMikrotik, false);

                    if (IPDisponibleFibra.Result != string.Empty)
                    {
                        ExisteEnFibra = mikrotik.VerFibrabyAddress(IPDisponibleFibra.Result);
                        if (ExisteEnFibra.Count() > 0) //Ya existe en secret
                        {
                            HistorialMovimientosModel H = new HistorialMovimientosModel
                            {
                                Id = 0,
                                Descripcion = "Ya se encuentra registrado el ip " + IPDisponibleFibra.Result + " para fibra, en el mikrotik " + txtMikrotik.Text + " y no esta informado el sistema favor de actualizar, se procedera a guardarlo en el sistema, favor de revisar",
                                Pagina = "Preregistro Cliente",
                                IdUsuario = 1,
                                Estatus = true
                            };
                            await obj.SaveHistorialMovimientos(H);

                            PlanModel objPlan = new PlanModel();
                            objPlan.Velocidad = ExisteEnFibra.First().velocidad == string.Empty ? "1k/1k" : ExisteEnFibra.First().velocidad;
                            objPlan.IsAntena = false;
                            var resultfibra = obj.SavePlanByMigracion(objPlan);
                            if (resultfibra.Result == 0)
                            {
                                MessageBox.Show("No se logro guardar el plan para la solicitud asignada en la base de datos favor de revisar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            objPlan.Id = resultfibra.Result;
                            PlanAnidadoModel objAnidado = new PlanAnidadoModel();
                            objAnidado.IdMikrotik = IdMikrotik;
                            objAnidado.IdPlanInterno = ExisteEnFibra.First().idplan;
                            objAnidado.IdPlan = objPlan.Id;
                            objAnidado.IsAntena = false;
                            objAnidado.Id = 0;
                            var ress = obj.SavePlanAnidadoByMigracion(objAnidado);
                            UsuariosGeneralModel objuser = new UsuariosGeneralModel();
                            objuser.IdMikrotik = IdMikrotik;
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
                            string IdPlanInterno = mikrotik.BuscarPerfil(NombrePlan);
                            if (IdPlanInterno == string.Empty)
                            {
                                MessageBox.Show("No se logro extraer el perfil del plan para la solicitud asignada en el mikrotik, es posible que lo hayan borrado fuera del sistema. Favor de revisar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            if (gbDatosCliente.Visible == true)
                            {
                                ClienteModel cliente = new ClienteModel();
                                cliente.Id = 0;
                                cliente.Nombre = txtNombreCliente.Text.Trim();
                                cliente.Correo = txtCorreo.Text.Trim();
                                cliente.Telefono1 = txtTelefono1.Text.Trim();
                                cliente.Telefono2 = txtTelefono2.Text.Trim();
                                IdCliente = obj.SaveCliente(cliente).Result;
                                if (IdCliente == 0)
                                {
                                    MessageBox.Show("No se logro guardar el cliente para la solicitud asignada en la base de datos favor de revisar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            
                            string idCreado = mikrotik.CrearFibra(txtNombreServicio.Text, IPDisponibleFibra.Result, NombrePlan, txtPassword.Text);
                            UsuariosGeneralModel objuser = new UsuariosGeneralModel();
                            objuser.IdMikrotik = IdMikrotik;
                            objuser.Nombre = txtNombreServicio.Text.Trim();
                            objuser.Address = IPDisponibleFibra.Result;
                            objuser.IdInterno = idCreado;
                            objuser.Estatus = "Inactivo";
                            objuser.Id = 0;
                            objuser.IdPlan = IdPlan;
                            IdUsuarioN = obj.SaveUsuariosNuevo(objuser, IdResponsable, IdCliente).Result;
                            mikrotik.CambiarEstatusFibra(objuser.IdInterno, "Activo");
                            HistorialMovimientosModel H = new HistorialMovimientosModel
                            {
                                Id = 0,
                                Descripcion = "Se creo el usuario " + objuser.Nombre + " en fibras",
                                Pagina = "Preregistro Cliente",
                                IdUsuario = IdResponsable,
                                Estatus = false
                            };
                        }
                    }
                    else
                    {
                    NuevaIpAddressFibra:
                        //Se acabaron las ips disponibles de esa serie 
                        var IPDisponibleAddress = obj.GetIPDisponibleAdresslist(IdMikrotik, false);
                        var ExisteAddresList = mikrotik.BuscarPoolbyAddress(IPDisponibleAddress.Result);
                        string IpExist = obj.GetIPExist(IdMikrotik, false, IPDisponibleAddress.Result).Result;

                        if (IpExist == string.Empty && ExisteAddresList == true)
                        {
                            //No existe en la base pero si en el mikrotik
                            //Lo introduciremos para que lo saltemos y no recorreremos su serie
                            await obj.SavePool(IdMikrotik, IPDisponibleAddress.Result, true);
                            HistorialMovimientosModel H = new HistorialMovimientosModel
                            {
                                Id = 0,
                                Descripcion = "La ip " + IPDisponibleAddress.Result + " se encontro en el pool del mikrotik " + txtMikrotik.Text + " pero no esta registrado en la base, se agregara a la base de forma automatica",
                                Pagina = "Preregistro Cliente",
                                IdUsuario = 1,
                                Estatus = false
                            };
                            await obj.SaveHistorialMovimientos(H);
                            goto NuevaIpAddressFibra;
                        }
                        if (ExisteAddresList == false)
                        {
                            //No existe en el mikrotik se procede a instroducirlo
                            var resultpool = mikrotik.AgregarPool(IPDisponibleAddress.Result);
                            string text = resultpool == true ? "La ip " + IPDisponibleAddress.Result + " no se encontro en el pool del mikrotik " + txtMikrotik.Text + ", se agregara a la base e introducira en el mikrotik de forma automatica" :
                                "La ip " + IPDisponibleAddress.Result + " no se logro introducir en el pool del mikrotik " + txtMikrotik.Text;
                            bool Estatushistory = resultpool == true ? false : true;
                            HistorialMovimientosModel H = new HistorialMovimientosModel
                            {
                                Id = 0,
                                Descripcion = text,
                                Pagina = "Preregistro Cliente",
                                IdUsuario = 1,
                                Estatus = Estatushistory
                            };
                            await obj.SaveHistorialMovimientos(H);
                            if (Estatushistory == true)
                            {
                                MessageBox.Show("No se logro introducir la ip en el pool del mikrotik " + txtMikrotik.Text + ", se cancela la solicitud", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                UbicacionModel ub = new UbicacionModel();
                ub.Direccion = txtDireccion.Text.Trim();
                ub.Direccion_Oficial = txtDireccionOficial.Text.Trim();
                ub.Latitud = txtLatitud.Text.Trim();
                ub.Longitud = txtLongitud.Text.Trim();
                ub.IdMikrotik = IdMikrotik;
                ub.IdUsuario = IdUsuarioN;
                ub.Id = 0;
                var resub = obj.SaveUbicacion(ub).Result;
                if (resub == false)
                {
                    MessageBox.Show("No se logro guardar la ubicación para la solicitud asignada en la base de datos favor de revisar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    MessageBox.Show("Se ha guardado correctamente la información del cliente y su servicio.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
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
                btnBuscar.Enabled = true;
                btnBuscarCoordenadas.Enabled = true;
                btnLupa.Enabled = true;
                btnGuardar.Enabled = Confirmado;
                btnCancelarDireccion.Enabled = Confirmado;
                btnAceptarUbicacion.Enabled = Confirmado;
            }
        }
    }
}
