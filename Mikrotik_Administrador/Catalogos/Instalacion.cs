using DocumentFormat.OpenXml.Office2010.Excel;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Mikrotik_Administrador.Class;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Items;
using Mikrotik_Administrador.Model;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador.Catalogos
{
    public partial class Instalacion : Form
    {
        public InstalacionModel InstalacionporExcel {  get; set; }
        public int Id { get; set; }
        public int IdUsuarioM { get; set; }
        public string IP { get; set; }
        private string calleEncontrada;
        GMarkerGoogle marcador;
        public Instalacion()
        {
            InitializeComponent();
        }

        private async void Instalacion_Load(object sender, EventArgs e)
        {
            AppRepository obj = new AppRepository();
            var listaUsuarios = await obj.GetUsuarios(string.Empty, string.Empty);
            // Insertamos un objeto "fantasma" al inicio para el placeholder
            listaUsuarios.Insert(0, new ListUsuariosModel { Id = 0, Nombre = "Selecciona un tecnico", IdTipo = 3 });

            // Configuramos el ComboBox
            CBInstalador.DisplayMember = "Nombre"; // Lo que el usuario VE
            CBInstalador.ValueMember = "Id";      // El dato que procesas por DETRÁS
            CBInstalador.DataSource = listaUsuarios.Where(x => x.IdTipo == 3).ToList();
            CBInstalador.SelectedIndex = 0;
            dtpFecha.Value = DateTime.Now;
            CBModalidad.SelectedIndex = 0;
            txtIP.Text = IP;
            RBAntena.Checked = true;
            if (Id == 0)
                return;
            var RInstalacion = InstalacionporExcel != null?
                InstalacionporExcel : obj.GetInstalacionbyId(Id).Result;
            if (RInstalacion != null)
            {
                CBInstalador.SelectedValue = RInstalacion.IdUsuario;
                dtpFecha.Value = RInstalacion.Fecha_Instalacion;
                CBModalidad.SelectedItem = RInstalacion.Modalidad;
                if (RInstalacion.Antena == string.Empty)
                {
                    RBFibra.Checked = true;
                }
                else
                {
                    RBAntena.Checked = true;
                }
                txtAntena.Text = RInstalacion.Antena;
                txtRB.Text = RInstalacion.RB;
                txtSector.Text = RInstalacion.Sector;
                txtTubo.Text = RInstalacion.Tubo;
                txtIP.Text = RInstalacion.IP;
                NUDCaja.Value = RInstalacion.Caja;
                NUDSpliter.Value = RInstalacion.Spliter;
                txtPuertoExterno.Text = RInstalacion.Puerto_Externo;
                NUDSpliter.Value = RInstalacion.Spliter;
                NUDPuertoInterno.Value = RInstalacion.Puerto_Interno;
                txtCable.Text = RInstalacion.Cable;
                txtRouter.Text = RInstalacion.Router;
                txtCargador.Text = RInstalacion.Cargador;
                txtLatitud.Text = RInstalacion.Latitud;
                txtLongitud.Text = RInstalacion.Longitud;
                NUDTotal.Value = RInstalacion.Total;
                txtNota.Text = RInstalacion.Nota;
                if(InstalacionporExcel != null)
                   btnGuardar.Visible = false;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if ((int)CBInstalador.SelectedValue == 0 ||
                CBModalidad.SelectedIndex == 0 ||
                txtIP.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Datos incompletos, favor de llenar toda la información necesaria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (RBAntena.Checked == true && (txtAntena.Text.Trim() == string.Empty))
            {
                MessageBox.Show("Datos incompletos, favor de llenar toda la información necesaria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtLongitud.Text.Trim() == string.Empty
              ||
              txtLatitud.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Se requiere los datos de latitud y longitud", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            BuscarDireccion();
            if(calleEncontrada == "Dirección no encontrada" || 
                calleEncontrada == "Error al obtener dirección")
            {
                MessageBox.Show(calleEncontrada, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            InstalacionModel Ins = new InstalacionModel()
            {
                Id = 0,
                IdUsuarioM = IdUsuarioM,
                IdUsuario = (int)CBInstalador.SelectedValue,
                Fecha_Instalacion = dtpFecha.Value,
                Modalidad = CBModalidad.SelectedItem.ToString(),
                RB = txtRB.Text.Trim(),
                Antena = txtAntena.Text.Trim(),
                Cable = txtCable.Text.Trim(),
                Router = txtRouter.Text.Trim(),
                Cargador = txtCargador.Text.Trim(),
                Tubo = txtTubo.Text.Trim(),
                IP = txtIP.Text.Trim(),
                Sector = txtSector.Text.Trim(),
                Caja = (int)NUDCaja.Value,
                Puerto_Externo = txtPuertoExterno.Text.Trim(),
                Spliter = (int)NUDSpliter.Value,
                Puerto_Interno = (int)NUDPuertoInterno.Value,
                Latitud = txtLatitud.Text.Trim(),
                Longitud = txtLongitud.Text.Trim(),
                Total = NUDTotal.Value,
                Nota = txtNota.Text.Trim(),
            };
            AppRepository ap = new AppRepository();
            var r = ap.SaveInstalacion(Ins).Result; //Esta vez se obtiene el folio y no el id
            if (r > 0)
            {
                MessageBox.Show("Guardado correctamante", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if(Id== 0)
                {
                    Imprimir(r);
                }
                
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al guardar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Imprimir(int Folio)
        {
            try
            {
                AppRepository obj = new AppRepository();
                var DatosServicio = obj.GetUsuariosMikrotiksById(IdUsuarioM).Result;
                var DatosCliente = obj.GetClienteById(DatosServicio.IdCliente).Result;
                var DatosPlan = obj.GetPlanById(DatosServicio.IdPlan).Result;
                var datos = new DatosInstalacion
                {
                    Folio = "Ins" + Folio.ToString(),
                    Fecha = dtpFecha.Value,
                    Instalador = CBInstalador.Text,
                    NombreCliente = DatosCliente.Nombre,
                    Telefono = DatosCliente.Telefono1,
                    Direccion = calleEncontrada,
                    Modalidad = CBModalidad.SelectedItem.ToString(),
                    Paquete = DatosPlan.Nombre,
                    Antena = txtAntena.Text.Trim(),
                    Cable = txtCable.Text.Trim(),
                    Router = txtRouter.Text.Trim(),
                    Cargador = txtCargador.Text.Trim(),
                    Tubo = txtTubo.Text.Trim(),
                    Box = NUDCaja.Value.ToString(),
                    Gps = txtLatitud.Text.Trim() + "," + txtLongitud.Text.Trim(),
                    Ip = txtIP.Text.Trim(),
                    Rb = txtRB.Text.Trim(),
                    Sector = txtSector.Text,
                    DiaPrimerRenta = "16",
                    MesPrimerRenta = "DICIEMBRE",
                    AnioPrimerRenta = "2026",
                    MontoPrimerRenta = 350.00m,

                    Observaciones = "Instalación limpia en azotea con buena línea de vista.",
                    Total = 800.00m
                };

                string rutaLogo = Path.Combine(Application.StartupPath, "Imagenes", "LOGO MASTER SISTEM.png");
                string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Impresiones");
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                string rutaCompleta = Path.Combine(carpeta, "Instalacion.pdf");

                var documento = new DocumentoInstalacion(datos, rutaLogo);
                documento.GeneratePdf(rutaCompleta);

                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(rutaCompleta) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RBAntena_CheckedChanged(object sender, EventArgs e)
        {
            if (RBAntena.Checked == false)
                return;
            txtAntena.Enabled = true;
            txtCable.Enabled = true;
            txtTubo.Enabled = true;
            NUDCaja.Enabled = false;
            NUDSpliter.Enabled = false;
            NUDPuertoInterno.Enabled = false;
            txtAntena.Text = string.Empty;
            txtCable.Text = string.Empty;
            txtTubo.Text = string.Empty;
            NUDCaja.Value = 0;
            NUDSpliter.Value = 0;
            NUDPuertoInterno.Value = 0;
        }

        private void RBFibra_CheckedChanged(object sender, EventArgs e)
        {
            if (RBFibra.Checked == false)
                return;
            txtAntena.Enabled = false;
            txtCable.Enabled = false;
            txtTubo.Enabled = false;
            NUDCaja.Enabled = true;
            NUDSpliter.Enabled = true;
            NUDPuertoInterno.Enabled = true;
            txtAntena.Text = string.Empty;
            txtCable.Text = "Fibra óptica";
            txtTubo.Text = string.Empty;
            NUDCaja.Value = 0;
            NUDSpliter.Value = 0;
            NUDPuertoInterno.Value = 0;
        }

        private void btnLupa_Click(object sender, EventArgs e)
        {
            BuscarDireccion();            
        }
        private void BuscarDireccion()
        {
            Mapa m = new Mapa();
            if (txtLongitud.Text.Trim() == string.Empty
               ||
               txtLatitud.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Se requiere los datos de latitud y longitud", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            m.Latitud = txtLatitud.Text;
            m.Longitud = txtLongitud.Text;
            if (m.ShowDialog() == DialogResult.OK)
            {
                txtLatitud.Text = m.Latitud;
                txtLongitud.Text = m.Longitud;
                BuscarPorCoordenadas(Convert.ToDecimal(txtLatitud.Text), Convert.ToDecimal(txtLongitud.Text));
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
           calleEncontrada = ObtenerCalleDesdeCoordenadas(marcador.Position.Lat, marcador.Position.Lng);

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


    }
}