using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Net;
using System.Windows.Forms;

namespace Mikrotik_Administrador.Items
{
    public partial class Mapa : Form
    {
        GMapOverlay capaMarcadores;
        GMarkerGoogle marcador;
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public Mapa()
        {
            InitializeComponent();
        }

        private void Mapa_Load(object sender, EventArgs e)
        {
            double latitude = double.Parse(Latitud, System.Globalization.CultureInfo.InvariantCulture);
            double longitude = double.Parse(Longitud, System.Globalization.CultureInfo.InvariantCulture);
            txtLatitud.Text = latitude.ToString();
            txtLongitud.Text = longitude.ToString();
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

        private void gMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && marcador != null)
            {
                Obtenerdireccion();
            }
        }
        public void Obtenerdireccion()
        {
            // Obtenemos la dirección de la posición final del marcador
            string calleEncontrada = ObtenerCalleDesdeCoordenadas(marcador.Position.Lat, marcador.Position.Lng);

            // Lo ponemos en tu TextBox de dirección
            //txtDireccionSugerida.Text = calleEncontrada;
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

        private void gMap_OnMarkerEnter(GMapMarker item)
        {
            // Cuando el mouse entra al marcador, permitimos el arrastre
            marcador = (GMarkerGoogle)item;
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

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            Latitud = txtLatitud.Text;
            Longitud = txtLongitud.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
