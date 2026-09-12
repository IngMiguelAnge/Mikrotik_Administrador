using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador.Catalogos
{
    public partial class PreregistroCliente : Form
    {
        private int IdPlan;
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
            if (txtNombre.Text.Trim() == "")
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
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            btnBuscar.Enabled = false;
            try
            {
                AppRepository obj = new AppRepository();
                var lista = obj.GetPlanesbyName(txtNombre.Text, null, true).Result;
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
            }
        }

        private void dgvPlanes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Evitar errores si hacen click en el encabezado
            if (e.RowIndex < 0) return;
            int Id = (int)dgvPlanes.Rows[e.RowIndex].Cells["Id"].Value;
            string NombrePlan = (string)dgvPlanes.Rows[e.RowIndex].Cells["Nombre"].Value;
            switch (dgvPlanes.Columns[e.ColumnIndex].Name)
            {
                case "btnAsignar":
                    if (NombrePlan.Trim() == string.Empty)
                    {
                        MessageBox.Show("Solo se pueden asignar planes con nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    IdPlan = Id;
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
            btnBuscarCoordenadas.Enabled = false;
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
                btnBuscarCoordenadas.Enabled = true;
                if (txtLatitud.Text != string.Empty)
                {
                    btnAceptarUbicacion.Enabled = true;
                }
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
            btnGuardar.Enabled = true;
            txtDireccionOficial.Text = txtDireccionSugerida.Text;
            txtDireccionOficial.Enabled = false;
            btnCancelarDireccion.Enabled = true;
            btnAceptarUbicacion.Enabled = false;
        }

        private void btnCancelarDireccion_Click(object sender, EventArgs e)
        {
            txtDireccionOficial.Enabled = true;
            btnGuardar.Enabled = false;
            btnAceptarUbicacion.Enabled = true;
            btnCancelarDireccion.Enabled = false;
        }

        private void PreregistroCliente_Load(object sender, EventArgs e)
        {
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
    }
}
