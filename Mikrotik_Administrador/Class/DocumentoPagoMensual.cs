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
    // Modelo de apoyo para llenar los datos dinámicos del recibo
    public class DatosRecibo
    {
        public string Folio { get; set; } = "021901";
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal Monto { get; set; }
        public string Recibe { get; set; } = "";
        public string NombreCliente { get; set; } = "";
        public string Lugar { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string CantidadConLetra { get; set; } = "";
        public string Anotaciones { get; set; } = "";
        public List<DetalleMensualidad> Detalles { get; set; } = new List<DetalleMensualidad>();
    }

    public class DetalleMensualidad
    {
        public int Dia { get; set; } = 1;
        public int Cantidad { get; set; } = 1;
        public string Mes { get; set; } = "";
        public string Anio { get; set; } = "";
        public decimal PrecioUnitario { get; set; }
        public decimal Importe => Cantidad * PrecioUnitario;
    }

    public class DocumentoPagoMensual : IDocument
    {
        private readonly DatosRecibo _datos;
        private readonly string _rutaLogo;

        // Paleta de colores extraída de tu recibo físico
        private static readonly string ColorPrimario = "#2D4263";    // Azul marino oscuro (textos y bordes)
        private static readonly string ColorFondoClaro = "#DCE2EB";  // Azul/gris tenue de los encabezados

        public DocumentoPagoMensual(DatosRecibo datos, string rutaLogo = null)
        {
            _datos = datos;
            _rutaLogo = rutaLogo;

            // Licencia gratuita de QuestPDF (Community)
            QuestPDF.Settings.License = LicenseType.Community;
        }

        // Métodos requeridos por la interfaz IDocument en QuestPDF
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                // Formato media carta horizontal (8.5 x 5.5 pulgadas) similar al talonario
                page.Size(new PageSize(8.5f, 5.5f, Unit.Inch));
                page.Margin(0.35f, Unit.Inch);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontFamily("Arial").FontSize(9).FontColor(ColorPrimario));

                page.Content().Column(col =>
                {
                    // 1. ENCABEZADO (Logo, Datos de la empresa y Recuadro de Folio)
                    col.Item().Element(ComponerEncabezado);

                    col.Item().PaddingTop(4);

                    // 2. FILA DE FECHA Y MONTO
                    col.Item().Element(ComponerFechaYMonto);

                    // 3. DATOS DEL CLIENTE (Recibe, Nombre, Lugar, Tel)
                    col.Item().Element(ComponerDatosCliente);

                    // 4. TABLA DE CONCEPTOS (Cantidad, Descripción, Precio U., Importe)
                    col.Item().Element(ComponerTablaConceptos);

                    // 5. PIE DE NOTA (Cantidad con letra, Anotaciones y Total)
                    col.Item().Element(ComponerPieNota);
                });
            });
        }

        private void ComponerEncabezado(IContainer container)
        {
            container.Row(row =>
            {
                // Columna Izquierda: Logo / Marca
                row.ConstantItem(150).Column(col =>
                {
                    if (!string.IsNullOrEmpty(_rutaLogo) && File.Exists(_rutaLogo))
                    {
                        col.Item().Height(45).Image(_rutaLogo).FitArea();
                    }
                    else
                    {
                        col.Item().Text("MASTER II").FontSize(16).Bold().FontColor(ColorPrimario);
                        col.Item().Text("S Y S T E M").FontSize(9).LetterSpacing(0.3f).FontColor(ColorPrimario);
                    }
                });

                // Columna Central: Información del negocio
                row.RelativeItem().PaddingHorizontal(5).AlignCenter().Column(col =>
                {
                    col.Item().AlignCenter().Text("INTERNET INALAMBRICO")
                        .FontSize(15).Bold().FontColor(ColorPrimario);
                    col.Item().AlignCenter().Text("PARA TU CASA O NEGOCIO")
                        .FontSize(8).Bold();
                    col.Item().AlignCenter().Text("Asesoría, Venta, Reparación de Computadoras, E Instalación de Redes")
                        .FontSize(6.5f).Bold();
                    col.Item().AlignCenter().Text("GUILLERMO MARTÍNEZ RODRIGUEZ    R.F.C. MARG780111H35")
                        .FontSize(6.5f).Bold();
                    col.Item().AlignCenter().Text("DIR. BOULEVARD JUÁREZ #33, COL CENTRO TLACOTEPEC DE BENITO JUÁREZ PUEBLA C.P 75680")
                        .FontSize(5.5f).Bold();
                    col.Item().AlignCenter().Text("TELEFONO: 2371138531    CEL: 2371118548")
                        .FontSize(6.5f).Bold();
                });

                // Columna Derecha: Nota de Venta y Folio
                row.ConstantItem(130).Column(col =>
                {
                    col.Item().Border(1).BorderColor(ColorPrimario).Column(box =>
                    {
                        box.Item().Background(ColorPrimario).PaddingVertical(2).AlignCenter()
                            .Text("NOTA DE VENTA").FontSize(8).Bold().FontColor(Colors.White);

                        box.Item().Background(ColorFondoClaro).PaddingVertical(6).AlignCenter()
                            .Text($"Nº  {_datos.Folio}")
                            .FontSize(12).Bold().FontColor("#B22222");
                    });

                    col.Item().PaddingTop(3).AlignCenter()
                        .Text("MASTER.SYSTEM@HOTMAIL.COM").FontSize(6.5f).Bold();
                });
            });
        }

        private void ComponerFechaYMonto(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem();

                row.ConstantItem(255).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(40);
                        columns.ConstantColumn(40);
                        columns.ConstantColumn(45);
                        columns.ConstantColumn(65);
                        columns.ConstantColumn(65);
                    });

                    table.Cell().AlignCenter().Text("Dia").FontSize(8).Bold();
                    table.Cell().AlignCenter().Text("Mes").FontSize(8).Bold();
                    table.Cell().AlignCenter().Text("Año").FontSize(8).Bold();
                    table.Cell();
                    table.Cell();

                    table.Cell().Border(1).BorderColor(ColorPrimario).Padding(3).AlignCenter()
                        .Text(_datos.Fecha.ToString("dd")).Bold();
                    table.Cell().Border(1).BorderColor(ColorPrimario).Padding(3).AlignCenter()
                        .Text(_datos.Fecha.ToString("MM")).Bold();
                    table.Cell().Border(1).BorderColor(ColorPrimario).Padding(3).AlignCenter()
                        .Text(_datos.Fecha.ToString("yyyy")).Bold();

                    table.Cell().Border(1).BorderColor(ColorPrimario).Background(ColorPrimario)
                        .Padding(3).AlignCenter().Text("Monto $").FontColor(Colors.White).Bold();

                    table.Cell().Border(1).BorderColor(ColorPrimario).Padding(3).AlignRight()
                        .Text(_datos.Monto.ToString("N2")).Bold();
                });
            });
        }

        private void ComponerDatosCliente(IContainer container)
        {
            container.Column(col =>
            {
                col.Item().Border(1).BorderColor(ColorPrimario).Background(ColorFondoClaro)
                    .PaddingHorizontal(5).PaddingVertical(3).Row(row =>
                    {
                        row.ConstantItem(50).Text("Recibe").Bold();
                        row.RelativeItem().Text(_datos.Recibe).FontColor(Colors.Black);
                    });

                col.Item().BorderLeft(1).BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                    .PaddingHorizontal(5).PaddingVertical(3).Row(row =>
                    {
                        row.ConstantItem(55).Text("Nombre:").Bold();
                        row.RelativeItem().Text(_datos.NombreCliente).FontColor(Colors.Black);
                    });

                col.Item().BorderLeft(1).BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                    .Background(ColorFondoClaro).PaddingHorizontal(5).PaddingVertical(3).Row(row =>
                    {
                        row.ConstantItem(45).Text("Lugar:").Bold();
                        row.RelativeItem().Text(_datos.Lugar).FontColor(Colors.Black);
                        row.ConstantItem(30).Text("Tel:").Bold();
                        row.ConstantItem(110).Text(_datos.Telefono).FontColor(Colors.Black);
                    });
            });
        }

        private void ComponerTablaConceptos(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(65);  // CANTIDAD
                    columns.RelativeColumn();    // DESCRIPCIÓN
                    columns.ConstantColumn(75);  // PRECIO U.
                    columns.ConstantColumn(80);  // IMPORTE
                });

                // Encabezados de tabla
                table.Header(header =>
                {
                    header.Cell().BorderLeft(1).BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                        .Padding(2).AlignCenter().Text("CANTIDAD:").FontSize(7.5f).Bold();
                    header.Cell().BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                        .Padding(2).AlignCenter().Text("DESCRIPCIÓN").FontSize(8).Bold();
                    header.Cell().BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                        .Padding(2).AlignCenter().Text("PRECIO U.").FontSize(8).Bold();
                    header.Cell().BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                        .Padding(2).AlignCenter().Text("IMPORTE.").FontSize(8).Bold();
                });

                // Definimos una altura uniforme para cada renglón
                float altoTitulo = 14f;
                float altoRenglon = 16f;

                // COLUMNA 1: CANTIDAD
                table.Cell().BorderLeft(1).BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                    .PaddingVertical(4).Column(col =>
                    {
                        col.Item().Height(altoTitulo); // Espacio equivalente al título "MENSUALIDAD..."

                        for (int i = 0; i < _datos.Detalles.Count; i++)
                        {
                            string cant = _datos.Detalles[i].Cantidad.ToString();

                            // AlignBottom() hace que el número baje al mismo nivel que la línea del texto
                            col.Item().Height(altoRenglon).AlignBottom().PaddingBottom(1.5f)
                                .AlignCenter().Text(cant).FontSize(8.5f).FontColor(Colors.Black);
                        }
                    });

                // COLUMNA 2: DESCRIPCIÓN (Con líneas pegadas al texto)
                table.Cell().BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                    .PaddingHorizontal(8).PaddingVertical(4).Column(col =>
                    {
                        col.Item().Height(altoTitulo).AlignCenter()
                            .Text("MENSUALIDAD  DE  INTERNET  CORRESPONDIENTE  A").FontSize(8.5f).Bold();

                        for (int i = 0; i < _datos.Detalles.Count; i++)
                        {
                            var detalle = _datos.Detalles[i];
                            string contenido = detalle != null ? $"{detalle.Mes} DEL {detalle.Anio}" : "";

                            col.Item().Height(altoRenglon).Row(row =>
                            {
                                // Línea del Día: AlignBottom() pega el texto justo encima de la línea
                                row.ConstantItem(45).BorderBottom(0.8f).BorderColor(ColorPrimario)
                                    .AlignBottom().PaddingBottom(1).AlignCenter()
                                    .Text(detalle?.Dia.ToString() ?? "").FontSize(8.5f).FontColor(Colors.Black);

                                // Palabra "DE" alineada abajo al mismo nivel
                                row.ConstantItem(22).AlignBottom().PaddingBottom(1).AlignCenter()
                                    .Text("DE").FontSize(8.5f).Bold();

                                // Línea del Mes y Año: AlignBottom() pega el texto justo encima de la línea
                                row.RelativeItem().BorderBottom(0.8f).BorderColor(ColorPrimario)
                                    .AlignBottom().PaddingBottom(1).PaddingLeft(5)
                                    .Text(contenido).FontSize(8.5f).FontColor(Colors.Black);
                            });
                        }
                    });

                // COLUMNA 3: PRECIO U.
                table.Cell().BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                    .PaddingVertical(4).PaddingRight(5).Column(col =>
                    {
                        col.Item().Height(altoTitulo);

                        for (int i = 0; i < _datos.Detalles.Count; i++)
                        {
                            string pu = _datos.Detalles[i].PrecioUnitario.ToString("N2");

                            col.Item().Height(altoRenglon).AlignBottom().PaddingBottom(1.5f)
                                .AlignRight().Text(pu).FontSize(8.5f).FontColor(Colors.Black);
                        }
                    });

                // COLUMNA 4: IMPORTE
                table.Cell().BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                    .PaddingVertical(4).PaddingRight(5).Column(col =>
                    {
                        col.Item().Height(altoTitulo);

                        for (int i = 0; i < _datos.Detalles.Count; i++)
                        {
                            string imp = _datos.Detalles[i].Importe.ToString("N2");

                            col.Item().Height(altoRenglon).AlignBottom().PaddingBottom(1.5f)
                                .AlignRight().Text(imp).FontSize(8.5f).FontColor(Colors.Black);
                        }
                    });
            });
        }

        private void ComponerPieNota(IContainer container)
        {
            container.Column(col =>
            {
                col.Item().BorderLeft(1).BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                    .Background(ColorFondoClaro).Row(row =>
                    {
                        row.RelativeItem().PaddingHorizontal(5).PaddingVertical(3).Row(r =>
                        {
                            r.ConstantItem(95).Text("Cantidad con letra").Bold();
                            r.RelativeItem().Text(_datos.CantidadConLetra).FontSize(8).FontColor(Colors.Black);
                        });

                        row.ConstantItem(80).BorderLeft(1).BorderColor(ColorPrimario);
                    });

                col.Item().BorderLeft(1).BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                    .Height(36).Row(row =>
                    {
                        row.RelativeItem().Padding(3).Column(c =>
                        {
                            c.Item().Text("ANOTACIONES").FontSize(9).Bold();
                            c.Item().Text(_datos.Anotaciones).FontSize(6).FontColor(Colors.Black);
                        });

                        row.ConstantItem(75).AlignBottom().PaddingBottom(5).AlignRight().PaddingRight(5)
                            .Text("Total:").FontSize(10).Bold();

                        row.ConstantItem(80).BorderLeft(1).BorderColor(ColorPrimario)
                            .AlignMiddle().PaddingRight(5).AlignRight()
                            .Text($"$ {_datos.Monto:N2}").FontSize(10).Bold().FontColor(Colors.Black);
                    });
            });
        }
    }
}