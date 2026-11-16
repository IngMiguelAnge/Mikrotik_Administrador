using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;

namespace Mikrotik_Administrador.Class
{
    public class DatosCobroTecnico
    {
        public string Folio { get; set; } = "2001";
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Recibe { get; set; } = "";
        public string NombreCliente { get; set; } = "";
        public string Observaciones { get; set; } = "";
        public decimal Total { get; set; }
        public List<DetalleCobroTecnico> Detalles { get; set; } = new List<DetalleCobroTecnico>();
    }

    public class DetalleCobroTecnico
    {
        public string Mes { get; set; } = "";
        public string Anio { get; set; } = "2026";
        public decimal PrecioUnitario { get; set; }
        public decimal Importe { get; set; }
    }

    public class DocumentoCobroTecnico : IDocument
    {
        private readonly DatosCobroTecnico _datos;
        private readonly string _rutaLogo;

        private static readonly string ColorPrimario = "#2D4263";    // Azul marino oscuro
        private static readonly string ColorFondoClaro = "#DCE2EB";  // Azul/gris tenue de las franjas

        public DocumentoCobroTecnico(DatosCobroTecnico datos, string rutaLogo = null)
        {
            _datos = datos;
            _rutaLogo = rutaLogo;
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                // Formato media carta horizontal (8.5 x 5.5 pulgadas)
                page.Size(new PageSize(8.5f, 5.5f, Unit.Inch));
                page.Margin(0.35f, Unit.Inch);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontFamily("Arial").FontSize(9).FontColor(ColorPrimario));

                page.Content().Column(col =>
                {
                    // 1. ENCABEZADO
                    col.Item().Element(ComponerEncabezado);

                    col.Item().PaddingTop(6);

                    // 2. BLOQUE SUPERIOR (Fecha integrada, Recibe y Nombre)
                    col.Item().Element(ComponerBloqueClienteYFecha);

                    // 3. TABLA DE CONCEPTOS (Descripción, Precio U., Importe)
                    col.Item().Element(ComponerTablaConceptos);

                    // 4. PIE DE NOTA (Observaciones y Total)
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
                        col.Item().Text("MASTER").FontSize(16).Bold().FontColor(ColorPrimario);
                        col.Item().Text("S Y S T E M").FontSize(9).LetterSpacing(0.3f).FontColor(ColorPrimario);
                    }
                });

                // Columna Central: Datos del técnico/negocio
                row.RelativeItem().PaddingHorizontal(5).AlignCenter().Column(col =>
                {
                    col.Item().AlignCenter().Text("INTERNET INALAMBRICO")
                        .FontSize(15).Bold().FontColor(ColorPrimario);
                    col.Item().AlignCenter().Text("PARA TU CASA O NEGOCIO")
                        .FontSize(8).Bold();
                    col.Item().AlignCenter().Text("Asesoría, Venta, Reparación de Computadoras, E Instalación de Redes")
                        .FontSize(6.5f).Bold();
                    col.Item().AlignCenter().Text("I.S.C. CRUZ MARTÍNEZ RODRIGUEZ    R.F.C. MARC820503MU8")
                        .FontSize(6.5f).Bold();
                    col.Item().AlignCenter().Text("AVENIDA REAL DEL MONTE # 28 SANTO NOMBRE SECCIÓN QUINTA, TLACOTEPEC DE BENITO JUÁREZ PUEBLA 75680")
                        .FontSize(5.2f).Bold();
                    col.Item().AlignCenter().Text("CEL: 2371002700   CEL: 2371118548")
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
                            .Text($"Nº      {_datos.Folio}")
                            .FontSize(12).Bold().FontColor("#B22222");
                    });

                    col.Item().PaddingTop(3).AlignCenter()
                        .Text("MASTER.SYSTEM@HOTMAIL.COM").FontSize(6.5f).Bold();
                });
            });
        }

        private void ComponerBloqueClienteYFecha(IContainer container)
        {
            container.Column(col =>
            {
                // Fila 1: Franja con Día | Mes | Año a la derecha
                col.Item().Border(1).BorderColor(ColorPrimario).Background(ColorFondoClaro)
                    .Row(row =>
                    {
                        row.RelativeItem().Height(15); // Espacio izquierdo vacío

                        row.ConstantItem(65).BorderLeft(1).BorderColor(ColorPrimario)
                            .AlignMiddle().AlignCenter().Text("Día").FontSize(8.5f).Bold();
                        row.ConstantItem(65).BorderLeft(1).BorderColor(ColorPrimario)
                            .AlignMiddle().AlignCenter().Text("Mes").FontSize(8.5f).Bold();
                        row.ConstantItem(75).BorderLeft(1).BorderColor(ColorPrimario)
                            .AlignMiddle().AlignCenter().Text("Año").FontSize(8.5f).Bold();
                    });

                // Fila 2: Recibe + "Tlacotepec de B.J,Pue a" + Valores de Fecha
                col.Item().BorderLeft(1).BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                    .Row(row =>
                    {
                        // Texto "Recibe" y valor
                        row.RelativeItem().PaddingHorizontal(5).PaddingVertical(3).Row(r =>
                        {
                            r.ConstantItem(45).AlignMiddle().Text("Recibe").Bold();
                            r.RelativeItem().AlignMiddle().Text(_datos.Recibe).FontColor(Colors.Black);
                        });

                        // Leyenda "Tlacotepec de B.J,Pue a" pegada a las cajas de fecha
                        row.ConstantItem(75).PaddingRight(4).AlignMiddle().AlignRight().Column(c =>
                        {
                            c.Item().AlignRight().Text("Tlacotepec").FontSize(6.5f).Bold();
                            c.Item().AlignRight().Text("de B.J,Pue a").FontSize(6.5f).Bold();
                        });

                        // Celdas de Día, Mes, Año
                        row.ConstantItem(65).BorderLeft(1).BorderColor(ColorPrimario)
                            .AlignMiddle().AlignCenter().Text(_datos.Fecha.ToString("dd")).Bold().FontColor(Colors.Black);
                        row.ConstantItem(65).BorderLeft(1).BorderColor(ColorPrimario)
                            .AlignMiddle().AlignCenter().Text(_datos.Fecha.ToString("MM")).Bold().FontColor(Colors.Black);
                        row.ConstantItem(75).BorderLeft(1).BorderColor(ColorPrimario)
                            .AlignMiddle().AlignCenter().Text(_datos.Fecha.ToString("yyyy")).Bold().FontColor(Colors.Black);
                    });

                // Fila 3: Nombre (con fondo claro)
                col.Item().BorderLeft(1).BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                    .Background(ColorFondoClaro).PaddingHorizontal(5).PaddingVertical(4).Row(row =>
                    {
                        row.ConstantItem(55).Text("Nombre:").Bold();
                        row.RelativeItem().Text(_datos.NombreCliente).FontColor(Colors.Black);
                    });
            });
        }

        private void ComponerTablaConceptos(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();    // DESCRIPCIÓN
                    columns.ConstantColumn(80);  // PRECIO U.
                    columns.ConstantColumn(85);  // IMPORTE.
                });

                // Encabezados de tabla
                table.Header(header =>
                {
                    header.Cell().BorderLeft(1).BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                        .Padding(3).AlignCenter().Text("DESCRIPCIÓN").FontSize(8).Bold();
                    header.Cell().BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                        .Padding(3).AlignCenter().Text("PRECIO U.").FontSize(8).Bold();
                    header.Cell().BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                        .Padding(3).AlignCenter().Text("IMPORTE.").FontSize(8).Bold();
                });

                float altoSubtitulo = 18f;
                float altoRenglon = 20f;

                // COLUMNA 1: DESCRIPCIÓN (Subtítulo + 4 renglones "Mes. _____ de 2026")
                table.Cell().BorderLeft(1).BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                    .PaddingHorizontal(15).PaddingVertical(6).Column(col =>
                    {
                        col.Item().Height(altoSubtitulo).AlignCenter()
                            .Text("Recibo la mensualidad de internet correspondiente.")
                            .FontSize(9.5f).Bold();

                        for (int i = 0; i < _datos.Detalles.Count; i++)
                        {
                            var detalle = _datos.Detalles[i];
                            string anioTexto = detalle != null && !string.IsNullOrEmpty(detalle.Anio)
                                ? detalle.Anio
                                : _datos.Fecha.Year.ToString();

                            col.Item().Height(altoRenglon).Row(row =>
                            {
                                // Etiqueta "Mes."
                                row.ConstantItem(32).AlignBottom().PaddingBottom(1.5f)
                                    .Text("Mes.").FontSize(9.5f).Bold();

                                // Línea para escribir el mes (alineada abajo)
                                row.RelativeItem().BorderBottom(0.8f).BorderColor(ColorPrimario)
                                    .AlignBottom().PaddingBottom(1.5f).PaddingLeft(8)
                                    .Text(detalle?.Mes ?? "").FontSize(9).FontColor(Colors.Black);

                                // Texto "de 2026"
                                row.ConstantItem(55).AlignBottom().PaddingBottom(1.5f).PaddingLeft(6)
                                    .Text($"de  {anioTexto}").FontSize(9.5f).Bold();
                            });
                        }
                    });

                // COLUMNA 2: PRECIO U.
                table.Cell().BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                    .PaddingVertical(6).PaddingRight(6).Column(col =>
                    {
                        col.Item().Height(altoSubtitulo);

                        for (int i = 0; i < _datos.Detalles.Count; i++)
                        {
                            string pu = _datos.Detalles[i].PrecioUnitario.ToString("N2");

                            col.Item().Height(altoRenglon).AlignBottom().PaddingBottom(1.5f)
                                .AlignRight().Text(pu).FontSize(9).FontColor(Colors.Black);
                        }
                    });

                // COLUMNA 3: IMPORTE.
                table.Cell().BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                    .PaddingVertical(6).PaddingRight(6).Column(col =>
                    {
                        col.Item().Height(altoSubtitulo);

                        for (int i = 0; i < _datos.Detalles.Count; i++)
                        {
                            string imp = _datos.Detalles[i].Importe.ToString("N2");

                            col.Item().Height(altoRenglon).AlignBottom().PaddingBottom(1.5f)
                                .AlignRight().Text(imp).FontSize(9).FontColor(Colors.Black);
                        }
                    });
            });
        }

        private void ComponerPieNota(IContainer container)
        {
            container.Column(col =>
            {
                // Fila: OBSERVACIONES y Total:
                col.Item().BorderLeft(1).BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                    .Height(38).Row(row =>
                    {
                        // Cuadro de Observaciones
                        row.RelativeItem().Padding(5).Column(c =>
                        {
                            c.Item().Text("OBSERVACIONES").FontSize(8.5f).Bold();
                            c.Item().Text(_datos.Observaciones).FontSize(8).FontColor(Colors.Black);
                        });

                        // Etiqueta "Total:"
                        row.ConstantItem(80).BorderLeft(1).BorderColor(ColorPrimario)
                            .AlignMiddle().AlignCenter()
                            .Text("Total:").FontSize(11).Bold();

                        // Caja del Total Final (con fondo claro como en la imagen)
                        row.ConstantItem(85).BorderLeft(1).BorderColor(ColorPrimario)
                            .Background(ColorFondoClaro).AlignMiddle().PaddingRight(6).AlignRight()
                            .Text($"$ {_datos.Total:N2}").FontSize(10).Bold().FontColor(Colors.Black);
                    });

                // Franja inferior decorativa gris/azul claro como en el talonario físico
                col.Item().BorderLeft(1).BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                    .Background(ColorFondoClaro).Height(12);
            });
        }
    }
}