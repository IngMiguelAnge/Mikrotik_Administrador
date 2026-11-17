using Mikrotik_Administrador.Class;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador.Catalogos
{
    public partial class PDFs : Form
    {
        public PDFs()
        {
            InitializeComponent();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if(cbTipo.SelectedIndex <= 0)
            {
                MessageBox.Show("Seleccione un tipo de PDF para generar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            switch (cbTipo.Text)
            {
                case "Mensualidades":
                    Mensualidades();
                    break;
                case "Cobros tecnicos":
                    CobrosTecnicos();
                   break;
                case "Instalación":
                    Instalaciones();
                    break;
                case "Nota de servicio":
                    NotasServicios();
                    break;
                default:                
                    break;
            }
        }
        private void NotasServicios()
        {
            try
            {
                var datos = new DatosNotaServicio
                {
                    Folio = "2751",
                    Fecha = DateTime.Now,
                    Recibe = "Ing. Guillermo Martínez",
                    Marca = "HP",
                    Modelo = "Pavilion 15",
                    NombreCliente = "Laura Méndez Cruz",
                    Telefono = "2371024455",
                    Ciudad = "Tlacotepec",
                    CantidadConLetra = "CUATROCIENTOS CINCUENTA PESOS 00/100 M.N.",
                    FechaEntrega = "20/11/2026",
                    Total = 450.00m,
                    Detalles = new List<DetalleServicio>
                    {
                        new DetalleServicio { Cantidad = 1, Descripcion = "Mantenimiento preventivo y limpieza interna", PrecioUnitario = 250.00m },
                        new DetalleServicio { Cantidad = 1, Descripcion = "Cambio de pasta térmica", PrecioUnitario = 200.00m }
                    }
                };

                string rutaLogo = Path.Combine(Application.StartupPath, "Imagenes", "LOGO MASTER SISTEM.png");
                string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Impresiones");
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                string rutaCompleta = Path.Combine(carpeta, "NotaServicio.pdf");

                var documento = new DocumentoNotaServicio(datos, rutaLogo);
                documento.GeneratePdf(rutaCompleta);

                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(rutaCompleta) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Instalaciones()
        {
            try
            {
                var datos = new DatosInstalacion
                {
                    Folio = "1951",
                    Fecha = DateTime.Now,
                    Instalador = "Juan Carlos Gómez",
                    NombreCliente = "Roberto Sánchez",
                    Telefono = "2371054890",
                    Direccion = "Calle Hidalgo #14",
                    Localidad = "Tlacotepec",
                    Modalidad = "Comodato / Renta",
                    Paquete = "20 Megas Hogar",

                    Antena = "LiteBeam 5AC Gen2",
                    Cable = "25 mts UTP Exterior",
                    Router = "TP-Link WR840N",
                    Cargador = "PoE 24V Original",
                    Tubo = "Mástil 3 mts",

                    Box = "Caja 04",
                    Gps = "18.6852, -97.6512",
                    Ip = "192.168.15.42",
                    Rb = "RB-Principal",
                    Sector = "Sector Norte 2",

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
        private void CobrosTecnicos()
        {
            try
            {
                var datos = new DatosCobroTecnico
                {
                    Folio = "2001",
                    Fecha = DateTime.Now,
                    Recibe = "I.S.C. Cruz Martínez",
                    NombreCliente = "Carlos Mendoza Pérez",
                    Observaciones = "Servicio mensual al corriente.",
                    Total = 350.00m,
                    Detalles = new List<DetalleCobroTecnico>
                {
                 new DetalleCobroTecnico { Mes = "NOVIEMBRE", Anio = "2026", PrecioUnitario = 350.00m, Importe = 350.00m },
                 new DetalleCobroTecnico { Mes = "DICIEMBRE", Anio = "2026", PrecioUnitario = 350.00m, Importe = 350.00m },
                 new DetalleCobroTecnico { Mes = "ENERO", Anio = "2027", PrecioUnitario = 350.00m, Importe = 350.00m },
                 new DetalleCobroTecnico { Mes = "FEBRERO", Anio = "2027", PrecioUnitario = 350.00m, Importe = 350.00m },
                }
                };

                string rutaLogo = Path.Combine(Application.StartupPath, "Imagenes", "LOGO MASTER SISTEM.png");
                string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Impresiones");
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                string rutaCompleta = Path.Combine(carpeta, "CobrosTecnico.pdf");

                var documento = new DocumentoCobroTecnico(datos, rutaLogo);
                documento.GeneratePdf(rutaCompleta);

                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(rutaCompleta) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private void Mensualidades()
        {
            try
            {
                var datos = new DatosRecibo
                {
                    Folio = "021902",
                    Fecha = DateTime.Now,
                    Monto = 350.00m,
                    Recibe = "Juan Pérez",
                    NombreCliente = "María López Hernández",
                    Lugar = "Col. Centro, Tlacotepec",
                    Telefono = "2371002030",
                    CantidadConLetra = ConversorMoneda.ConvertirALetras(350.00m),
                    Anotaciones = string.Empty,
                    Detalles = new List<DetalleMensualidad>
                {
                  new DetalleMensualidad { Cantidad = 1, Dia = 1, Mes = "ENERO", Anio = "2026", PrecioUnitario = 350.00m },
                  new DetalleMensualidad { Cantidad = 1, Dia = 1, Mes = "FEBRERO", Anio = "2026", PrecioUnitario = 350.00m },
                  new DetalleMensualidad { Cantidad = 1, Dia = 1, Mes = "MARZO", Anio = "2026", PrecioUnitario = 350.00m },
                  new DetalleMensualidad { Cantidad = 1, Dia = 1, Mes = "ABRIL", Anio = "2026", PrecioUnitario = 350.00m },
                  new DetalleMensualidad { Cantidad = 1, Dia = 1, Mes = "MAYO", Anio = "2026", PrecioUnitario = 350.00m },
                  new DetalleMensualidad { Cantidad = 1, Dia = 1, Mes = "JUNIO", Anio = "2026", PrecioUnitario = 350.00m }
                }
                };

                // Ruta opcional de tu logotipo en PNG/JPG
                string rutaLogo = Path.Combine(Application.StartupPath, "Imagenes", "LOGO MASTER SISTEM.png");

                string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Impresiones");
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                string rutaCompleta = Path.Combine(carpeta, "Mensualidad.pdf");

                var documento = new DocumentoPagoMensual(datos, rutaLogo);
                documento.GeneratePdf(rutaCompleta);

                // Abrir el PDF generado con el visor predeterminado (Usando rutaCompleta)
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(rutaCompleta) { UseShellExecute = true });

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void PDFs_Load(object sender, EventArgs e)
        {
            cbTipo.SelectedIndex = 0;
        }
    }
}
