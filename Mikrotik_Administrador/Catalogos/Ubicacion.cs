using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Windows.Forms;
using System.Net;
using Mikrotik_Administrador.Model;
using Mikrotik_Administrador.Data;

namespace Mikrotik_Administrador
{
    public partial class Ubicacion : Form
    {
        public int IdUsuario = 0;
        public int IdMikrotik = 0;

        GMapOverlay capaMarcadores;
        GMarkerGoogle marcador;
        bool estaArrastrando = false;
        public Ubicacion()
        {
            InitializeComponent();
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
        private void Ubicacion_Load(object sender, EventArgs e)
        {
            double latitude = 18.68165869879; // Latitud de tlacotepect
            double longitude = -97.64837265014; // Longitud de tlacotepect

            if (IdMikrotik > 0)
            {
                AppRepository app = new AppRepository();
                var ubicacion = app.GetUbicacionByIds(IdMikrotik, IdUsuario).Result;
                if (ubicacion != null)
                {
                    txtDireccionOficial.Text = ubicacion.Direccion_Oficial;
                    txtDireccion.Text = ubicacion.Direccion;
                    txtLatitud.Text = ubicacion.Latitud;
                    txtLongitud.Text = ubicacion.Longitud;
                    latitude = double.Parse(ubicacion.Latitud, System.Globalization.CultureInfo.InvariantCulture);
                    longitude = double.Parse(ubicacion.Longitud, System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            // 1. Configurar el proveedor y modo (Internet)
            //gMap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            cmbMapas.SelectedIndex = 0;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;

            // 2. Posición inicial (puedes poner las de tu ciudad)
            gMap.Position = new PointLatLng(latitude, longitude); //Ciudad inicial Mexico
            gMap.MinZoom = 2;
            gMap.MaxZoom = 20;
            gMap.Zoom = 18;

            // 3. Permitir mover el mapa con el botón derecho y el marcador con el izquierdo
            gMap.DragButton = MouseButtons.Right;

            // 4. Crear la capa para los marcadores
            capaMarcadores = new GMapOverlay("capa1");
            gMap.Overlays.Add(capaMarcadores);

            // 5. Crear el marcador inicial
            marcador = new GMarkerGoogle(gMap.Position, GMarkerGoogleType.red_pushpin);
            marcador.IsVisible = true;
            capaMarcadores.Markers.Add(marcador);
            //el zoom siga al mouse.
            gMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionWithoutCenter;
        }

        private void gMap_OnMarkerEnter(GMapMarker item)
        {
            // Cuando el mouse entra al marcador, permitimos el arrastre
            marcador = (GMarkerGoogle)item;
        }

        private void gMap_MouseMove(object sender, MouseEventArgs e)
        {
            // Si el usuario deja presionado el botón izquierdo sobre el marcador
            if (e.Button == MouseButtons.Left && marcador != null)
            {
                // Convertimos la posición del mouse en coordenadas de mapa
                PointLatLng pos = gMap.FromLocalToLatLng(e.X, e.Y);
                marcador.Position = pos;

                // Mostrar coordenadas en TextBoxes (opcional)
                txtLatitud.Text = pos.Lat.ToString();
                txtLongitud.Text = pos.Lng.ToString();
            }
        }
        public void BuscarPorDireccion(string direccionBuscada)
        {
            PointLatLng? pos = BuscarEnOpenStreetMap(direccionBuscada);

            if (pos.HasValue)
            {
                // 1. Centrar mapa
                gMap.Position = pos.Value;

                // 2. Mover marcador
                if (marcador != null)
                {
                    marcador.Position = pos.Value;
                    Obtenerdireccion();
                }

                gMap.MinZoom = 2;
                gMap.MaxZoom = 20;
                gMap.Zoom = 18;

                // 3. Llenar tus cuadros de texto
                txtLatitud.Text = pos.Value.Lat.ToString();
                txtLongitud.Text = pos.Value.Lng.ToString();
            }
            else
            {
                MessageBox.Show("No se encontró: '" + direccionBuscada + "'. Intenta ser más específico (ej: Huixcolotla, Puebla, Mexico).", "Sin resultados");
            }
        }
        private void BuscarPorCoordenadas(decimal latitud, decimal longitud)
        {
            try
            {
                if (double.TryParse(txtLatitud.Text, out double lat) && double.TryParse(txtLongitud.Text, out double lng))
                {
                    PointLatLng pos = new PointLatLng(lat, lng);
                    gMap.Position = pos;

                    if (marcador != null)
                    {
                        marcador.Position = pos;
                        Obtenerdireccion();
                    }

                    gMap.MinZoom = 2;
                    gMap.MaxZoom = 20;
                    gMap.Zoom = 18;
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
        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            if((string.IsNullOrWhiteSpace(txtDireccionOficial.Text) 
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
            BtnBuscar.Enabled = false;
            btnAceptarUbicacion.Enabled = false;
            btnCancelarDireccion.Enabled = false;
            
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
                BtnBuscar.Enabled = true;
                if (txtLatitud.Text != string.Empty)
                {
                    btnAceptarUbicacion.Enabled = true;
                }
            }
        }
        public void Obtenerdireccion()
        {
            // Obtenemos la dirección de la posición final del marcador
            string calleEncontrada = ObtenerCalleDesdeCoordenadas(marcador.Position.Lat, marcador.Position.Lng);

            // Lo ponemos en tu TextBox de dirección
            txtDireccionSugerida.Text = calleEncontrada;
        }
        private void gMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && marcador != null)
            {
                Obtenerdireccion();
            }
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

        private void cmbMapas_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbMapas.Text)
            {
                case "Calles (Google)":
                    gMap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
                    break;
                case "Satélite":
                    gMap.MapProvider = GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance;
                    break;
                case "Híbrido":
                    gMap.MapProvider = GMap.NET.MapProviders.GoogleHybridMapProvider.Instance;
                    break;
                case "OpenStreet":
                    GMap.NET.MapProviders.GMapProvider.UserAgent = "Mikrotik_v1.0";
                    gMap.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
                    break;
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (txtDireccionOficial.Text.Trim() == string.Empty || txtLatitud.Text.Trim() == string.Empty || txtLongitud.Text.Trim() == string.Empty ||
                txtDireccion.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Faltan datos por capturar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            UbicacionModel ub = new UbicacionModel();
            ub.Direccion = txtDireccion.Text.Trim();
            ub.Direccion_Oficial = txtDireccionOficial.Text.Trim();
            ub.Latitud = txtLatitud.Text.Trim();
            ub.Longitud = txtLongitud.Text.Trim();
            ub.IdMikrotik = IdMikrotik;
            ub.IdUsuario = IdUsuario;
            AppRepository app = new AppRepository();
            if (app.SaveUbicacion(ub).Result == true)
            {
                MessageBox.Show("Guardado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al guardar la ubicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAceptarUbicacion_Click(object sender, EventArgs e)
        {
            if (txtDireccionSugerida.Text.Trim() == string.Empty)
            {
                MessageBox.Show("No hay una dirección sugerida para aceptar, favor de mover el puntero del mapa.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            txtDireccionOficial.Text = txtDireccionSugerida.Text;
            BtnGuardar.Enabled = true;
            txtDireccionOficial.Enabled = false;
            btnCancelarDireccion.Enabled = true;
            btnAceptarUbicacion.Enabled = false;
        }

        private void btnCancelarDireccion_Click(object sender, EventArgs e)
        {
            txtDireccionOficial.Enabled = true;
            BtnGuardar.Enabled = false;
            btnAceptarUbicacion.Enabled = true;
            btnCancelarDireccion.Enabled = false;
        }
    }
}
