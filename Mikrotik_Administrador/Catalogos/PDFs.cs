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
                default:
                    // code block
                    break;
            }
        }
        private void Mensualidades()
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
                CantidadConLetra = "TRESCIENTOS CINCUENTA PESOS 00/100 M.N.",
                Anotaciones = "Pago puntual correspondiente al paquete básico.",
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

        private void PDFs_Load(object sender, EventArgs e)
        {
            cbTipo.SelectedIndex = 0;
        }
    }
}
