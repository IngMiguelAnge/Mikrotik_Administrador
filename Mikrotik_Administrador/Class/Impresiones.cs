using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using QRCoder;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Mikrotik_Administrador.Class
{
    public class Impresiones
    {
        public void ImprimirSilencioso(string rutaArchivo)
        {
            PdfDocument pdf = new PdfDocument();
            pdf.LoadFromFile(rutaArchivo);
            // En algunas versiones se usa esta propiedad para ocultar el diálogo:
            pdf.PrintSettings.PrintController = new System.Drawing.Printing.StandardPrintController();
            pdf.Print();
        }
        public string CodigodeColor(string Color)
        {
            string Codigo = "";
            switch (Color)
            {
                case "Black":
                    Codigo = "#000000";
                    break;
                case "Red":
                    Codigo = "#FF0000";
                    break;
                case "Blue":
                    Codigo = "#0000FF";
                    break;
                case "Green":
                    Codigo = "#008000";
                    break;
                default:
                    Codigo = "#808080";
                    break;
            }
            return Codigo;
        }
        private TextStyle ObtenerEstiloPersonalizado(string Style, float tamano, string colorHex)
        {
            // Creamos el estilo base con el tamaño y color
            var Estilo = TextStyle.Default
                .FontSize(tamano)
                .FontColor(colorHex);
            switch (Style)
            {
                case "SemiBold":
                    Estilo = Estilo.SemiBold();
                    break;
                case "Medium":
                    Estilo = Estilo.Medium();
                    break;
                case "Bold":
                    Estilo = Estilo.Bold();
                    break;
                default:
                    break;
            }
            // Aplicamos el grosor según el nombre recibido
            return Estilo;
        }
        public void GenerarTicket(VentaModel venta)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            ConfigPageModel ConfigBox;
            List<ListConfigImpressionsModel> ConfigImpressions;
            try
            {
                string nombreArchivo = $"Ticket_{venta.IdTicket}.pdf";
                string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Impresiones");
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                string rutaCompleta = Path.Combine(carpeta, nombreArchivo);

                AppRepository obj = new AppRepository();
                ConfigBox = obj.GetConfigPage().Result;
                ConfigImpressions = obj.GetConfigImpressions("Ticket").Result;

                float mmToPt = 2.83465f;
                float anchoTicketMm = (float)ConfigBox.WidthPage; // Ej. 58f u 80f
                float anchoFinal = anchoTicketMm * mmToPt;

                // Generar QR
                byte[] qrBytes = null;
                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                {
                    string datosQr = $"https://www.master-systems.com.mx/";

                    using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(datosQr, QRCodeGenerator.ECCLevel.Q))
                    using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
                    {
                        qrBytes = qrCode.GetGraphic(10);
                    }
                }

                var documento = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        // CORRECCIÓN: Método oficial de QuestPDF para ancho fijo y alto variable
                        page.ContinuousSize(anchoFinal);

                        page.Margin(2, Unit.Millimetre);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(x => x.FontSize(8).FontFamily(Fonts.Arial));

                        // Carga de estilos desde tu configuración
                        int TituloFontsize = ConfigImpressions.Find(x => x.Name == "Titulo") != null ? Convert.ToInt32(ConfigImpressions.Find(x => x.Name == "Titulo").FontSize) : 10;
                        string TituloColor = ConfigImpressions.Find(x => x.Name == "Titulo") != null ? ConfigImpressions.Find(x => x.Name == "Titulo").FontColor : "Black";
                        TituloColor = CodigodeColor(TituloColor);
                        string TituloFontStyle = ConfigImpressions.Find(x => x.Name == "Titulo") != null ? ConfigImpressions.Find(x => x.Name == "Titulo").FontStyle : "SemiBold";
                        var EstiloTitulo = ObtenerEstiloPersonalizado(TituloFontStyle, TituloFontsize, TituloColor);

                        int CompanyFontsize = ConfigImpressions.Find(x => x.Name == "Company") != null ? Convert.ToInt32(ConfigImpressions.Find(x => x.Name == "Company").FontSize) : 10;
                        string CompanyColor = ConfigImpressions.Find(x => x.Name == "Company") != null ? ConfigImpressions.Find(x => x.Name == "Company").FontColor : "Black";
                        CompanyColor = CodigodeColor(CompanyColor);
                        string CompanyFontStyle = ConfigImpressions.Find(x => x.Name == "Company") != null ? ConfigImpressions.Find(x => x.Name == "Company").FontStyle : "SemiBold";
                        var EstiloCompany = ObtenerEstiloPersonalizado(CompanyFontStyle, CompanyFontsize, CompanyColor);

                        int RFCFontsize = ConfigImpressions.Find(x => x.Name == "RFC") != null ? Convert.ToInt32(ConfigImpressions.Find(x => x.Name == "RFC").FontSize) : 8;
                        string RFCColor = ConfigImpressions.Find(x => x.Name == "RFC") != null ? ConfigImpressions.Find(x => x.Name == "RFC").FontColor : "Black";
                        RFCColor = CodigodeColor(RFCColor);
                        string RFCFontStyle = ConfigImpressions.Find(x => x.Name == "RFC") != null ? ConfigImpressions.Find(x => x.Name == "RFC").FontStyle : "Normal";
                        var EstiloRFC = ObtenerEstiloPersonalizado(RFCFontStyle, RFCFontsize, RFCColor);

                        int FechaFontsize = ConfigImpressions.Find(x => x.Name == "Fecha") != null ? Convert.ToInt32(ConfigImpressions.Find(x => x.Name == "Fecha").FontSize) : 8;
                        string FechaColor = ConfigImpressions.Find(x => x.Name == "Fecha") != null ? ConfigImpressions.Find(x => x.Name == "Fecha").FontColor : "Black";
                        FechaColor = CodigodeColor(FechaColor);
                        string FechaFontStyle = ConfigImpressions.Find(x => x.Name == "Fecha") != null ? ConfigImpressions.Find(x => x.Name == "Fecha").FontStyle : "Normal";
                        var EstiloFecha = ObtenerEstiloPersonalizado(FechaFontStyle, FechaFontsize, FechaColor);

                        int TablaFontsize = ConfigImpressions.Find(x => x.Name == "Tabla") != null ? Convert.ToInt32(ConfigImpressions.Find(x => x.Name == "Tabla").FontSize) : 8;
                        string TablaColor = ConfigImpressions.Find(x => x.Name == "Tabla") != null ? ConfigImpressions.Find(x => x.Name == "Tabla").FontColor : "Black";
                        TablaColor = CodigodeColor(TablaColor);
                        string TablaFontStyle = ConfigImpressions.Find(x => x.Name == "Tabla") != null ? ConfigImpressions.Find(x => x.Name == "Tabla").FontStyle : "Normal";
                        var EstiloTabla = ObtenerEstiloPersonalizado(TablaFontStyle, TablaFontsize, TablaColor);

                        // Encabezado del Ticket
                        page.Header().Column(col =>
                        {
                            col.Item().AlignCenter().Text("TICKET " + venta.IdTicket.ToString()).Style(EstiloTitulo);
                            col.Item().AlignCenter().Text("PAGO DE SERVICIO").Style(EstiloTitulo);
                            col.Item().AlignCenter().Text("master-systems").Style(EstiloCompany);
                            col.Item().AlignCenter().Text("MIRFC").Style(EstiloRFC);
                            col.Item().AlignCenter().Text("MI DIRECCION").Style(EstiloRFC);
                            col.Item().AlignCenter().Text(venta.Cliente).Style(EstiloRFC);
                            col.Item().AlignCenter().Text(DateTime.Now.ToString("dd/MM/yyyy HH:mm")).Style(EstiloFecha);
                            col.Item().PaddingVertical(2).LineHorizontal(1);
                        });

                        // Contenido Principal
                        page.Content().PaddingVertical(2).Column(mainCol =>
                        {
                            // 1. La Tabla
                            mainCol.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(7f);    // Descripción
                                    columns.RelativeColumn(1.8f);  // Total
                                });

                                // Encabezados
                                table.Header(header =>
                                {
                                    header.Cell().AlignLeft().Text("Descripción").Style(EstiloTabla).Bold();
                                    header.Cell().AlignRight().Text("Total").Style(EstiloTabla).Bold();
                                    header.Cell().ColumnSpan(2).PaddingVertical(2).LineHorizontal(0.5f);
                                });


                                table.Cell().PaddingVertical(1).AlignLeft().Text("PAGO DE SERVICIO").Style(EstiloTabla);
                                table.Cell().PaddingVertical(1).AlignRight().Text(venta.Total.ToString("C2")).Style(EstiloTabla);

                            });

                            // Totales
                            int TotalFontsize = ConfigImpressions.Find(x => x.Name == "Total") != null ? Convert.ToInt32(ConfigImpressions.Find(x => x.Name == "Total").FontSize) : 9;
                            string TotalColor = ConfigImpressions.Find(x => x.Name == "Total") != null ? ConfigImpressions.Find(x => x.Name == "Total").FontColor : "Black";
                            TotalColor = CodigodeColor(TotalColor);
                            string TotalFontStyle = ConfigImpressions.Find(x => x.Name == "Total") != null ? ConfigImpressions.Find(x => x.Name == "Total").FontStyle : "SemiBold";
                            var EstiloTotal = ObtenerEstiloPersonalizado(TotalFontStyle, TotalFontsize, TotalColor);

                            // 2. Bloque de Cierre
                            mainCol.Item().PaddingTop(3).Column(totalCol =>
                            {
                                totalCol.Item().LineHorizontal(1);

                                totalCol.Item().PaddingTop(2).AlignRight().Text($"RECIBIDO: {venta.Recibido:C2}").Style(EstiloTotal);
                                totalCol.Item().AlignRight().Text($"TOTAL: {venta.Total:C2}").Style(EstiloTotal);

                                decimal cambio = venta.Recibido - venta.Total < 0 ? 0 : venta.Recibido - venta.Total;
                                totalCol.Item().AlignRight().Text($"CAMBIO: {cambio:C2}").Style(EstiloTotal);

                                totalCol.Item().PaddingTop(8).AlignCenter().Text("¡Gracias por su compra!").Style(EstiloTabla);

                                // Control estricto de tamaño del QR
                                if (qrBytes != null)
                                {
                                    totalCol.Item()
                                            .PaddingTop(8)
                                            .AlignCenter()
                                            .Width(55)
                                            .Image(qrBytes);
                                }
                            });
                        });
                    });
                });

                // Guardado y Ejecución
                documento.GeneratePdf(rutaCompleta);

                if (venta.Imprimir)
                {
                    for (int i = 0; i <= venta.Copias; i++)
                        ImprimirSilencioso(rutaCompleta);
                }
                else
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(rutaCompleta) { UseShellExecute = true });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al imprimir ticket: " + ex.Message);
            }
        }
    }
}
