using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;

namespace Mikrotik_Administrador.Class
{
    public class DatosNotaServicio
    {
        public string Folio { get; set; } = "";
        public DateTime Fecha { get; set; } = DateTime.Now;

        // Datos de recepción y equipo
        public string Cobra { get; set; } = "";
        public string Marca { get; set; } = "";
        public string Modelo { get; set; } = "";

        // Datos del cliente
        public string Cliente { get; set; } = "";
        public string Servicio { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Ciudad { get; set; } = "";

        // Cierre
        public string CantidadConLetra { get; set; } = "";
        public string FechaEntrega { get; set; } = "";
        public decimal Total { get; set; }

        public List<DetalleServicio> Detalles { get; set; } = new List<DetalleServicio>();
    }

    public class DetalleServicio
    {
        public int Cantidad { get; set; } = 1;
        public string Descripcion { get; set; } = "";
        public decimal PrecioUnitario { get; set; }
        public decimal Importe => Cantidad * PrecioUnitario;
    }

    public class DocumentoNotaServicio : IDocument
    {
        private readonly DatosNotaServicio _datos;
        private readonly string _rutaLogo;

        private static readonly string ColorPrimario = "#2D4263";    // Azul marino oscuro
        private static readonly string ColorFondoClaro = "#DCE2EB";  // Azul/gris tenue de las franjas

        public DocumentoNotaServicio(DatosNotaServicio datos, string rutaLogo = null)
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
                page.Margin(0.30f, Unit.Inch);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontFamily("Arial").FontSize(8.5f).FontColor(ColorPrimario));

                page.Content().Column(col =>
                {
                    // 1. ENCABEZADO
                    col.Item().Element(ComponerEncabezado);

                    col.Item().PaddingTop(3);

                    // 2. BLOQUE SUPERIOR (Recibe, Fecha, Marca, Modelo, Nombre, Teléfono, Ciudad)
                    col.Item().Element(ComponerDatosRecepcion);

                    // 3. TABLA DE ARTÍCULOS / SERVICIOS (10 filas cuadriculadas)
                    col.Item().Element(ComponerTablaServicios);

                    // 4. PIE DE NOTA (Cantidad con letra, Firma, Fecha de entrega y Total)
                    col.Item().Element(ComponerPieNota);

                    col.Item().PaddingTop(3);

                    // 5. CLÁUSULAS DE GARANTÍA (Puntos 1 al 11 en dos columnas)
                    col.Item().Element(ComponerClausulas);
                });
            });
        }

        private void ComponerEncabezado(IContainer container)
        {
            container.Row(row =>
            {
                // Logo
                row.ConstantItem(150).Column(col =>
                {
                    if (!string.IsNullOrEmpty(_rutaLogo) && File.Exists(_rutaLogo))
                    {
                        col.Item().Height(42).Image(_rutaLogo).FitArea();
                    }
                    else
                    {
                        col.Item().Text("MASTER").FontSize(16).Bold().FontColor(ColorPrimario);
                        col.Item().Text("S Y S T E M").FontSize(9).LetterSpacing(0.3f).FontColor(ColorPrimario);
                    }
                });

                // Información del negocio
                row.RelativeItem().PaddingHorizontal(5).AlignCenter().Column(col =>
                {
                    col.Item().AlignCenter().Text("INTERNET INALAMBRICO")
                        .FontSize(15).Bold().FontColor(ColorPrimario);
                    col.Item().AlignCenter().Text("PARA TU CASA O NEGOCIO")
                        .FontSize(8).Bold();
                    col.Item().AlignCenter().Text("Asesoría, Venta, Reparación de Computadoras, E Instalación de Redes")
                        .FontSize(6.5f).Bold();
                    col.Item().AlignCenter().Text("ING.GUILLERMO MARTÍNEZ RODRIGUEZ  R.F.C. MARG780111H35")
                        .FontSize(6.3f).Bold();
                    col.Item().AlignCenter().Text("DIR. BOULEVARD JUÁREZ #33, COL CENTRO TLACOTEPEC DE BENITO JUÁREZ PUEBLA C.P 75680")
                        .FontSize(5.4f).Bold();
                    col.Item().AlignCenter().Text("TELEFONO: 237 111 85 48   CEL: 237 113 85 31")
                        .FontSize(6.5f).Bold();
                });

                // Nota de Venta y Folio
                row.ConstantItem(130).Column(col =>
                {
                    col.Item().Border(1).BorderColor(ColorPrimario).Column(box =>
                    {
                        box.Item().Background(ColorPrimario).PaddingVertical(2).AlignCenter()
                            .Text("NOTA DE VENTA").FontSize(8).Bold().FontColor(Colors.White);

                        box.Item().Background(ColorFondoClaro).PaddingVertical(5).AlignCenter()
                            .Text($"{_datos.Folio}")
                            .FontSize(12).Bold().FontColor("#B22222");
                    });

                    col.Item().PaddingTop(2).AlignCenter()
                        .Text("MASTER.SYSTEM@HOTMAIL.COM").FontSize(6.5f).Bold();
                });
            });
        }

        private void ComponerDatosRecepcion(IContainer container)
        {
            container.Column(col =>
            {
                // Fila 1: Recibe | [Día][Mes][Año] | marca | modelo
                col.Item().Border(1).BorderColor(ColorPrimario).Background(ColorFondoClaro)
                    .Height(18).Row(row =>
                    {
                        // Recibe
                        row.ConstantItem(165).PaddingHorizontal(4).AlignMiddle().Row(r =>
                        {
                            r.ConstantItem(42).Text("Recibe").Bold();
                            r.RelativeItem().Text(_datos.Cobra).FontSize(8).FontColor(Colors.Black);
                        });

                        // 3 cajas de fecha en medio
                        row.ConstantItem(32).BorderLeft(1).BorderColor(ColorPrimario)
                            .AlignMiddle().AlignCenter().Text(_datos.Fecha.ToString("dd")).FontSize(8).Bold().FontColor(Colors.Black);
                        row.ConstantItem(32).BorderLeft(1).BorderColor(ColorPrimario)
                            .AlignMiddle().AlignCenter().Text(_datos.Fecha.ToString("MM")).FontSize(8).Bold().FontColor(Colors.Black);
                        row.ConstantItem(38).BorderLeft(1).BorderRight(1).BorderColor(ColorPrimario)
                            .AlignMiddle().AlignCenter().Text(_datos.Fecha.ToString("yyyy")).FontSize(8).Bold().FontColor(Colors.Black);

                        // Marca
                        row.RelativeItem(1).PaddingHorizontal(4).AlignMiddle().Row(r =>
                        {
                            r.ConstantItem(36).Text("marca").Bold();
                            r.RelativeItem().Text(_datos.Marca).FontSize(8).FontColor(Colors.Black);
                        });

                        // Modelo
                        row.RelativeItem(1).PaddingHorizontal(4).AlignMiddle().Row(r =>
                        {
                            r.ConstantItem(42).Text("modelo").Bold();
                            r.RelativeItem().Text(_datos.Modelo).FontSize(8).FontColor(Colors.Black);
                        });
                    });

                // Fila 2: Nombre
                col.Item().BorderLeft(1).BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                    .PaddingHorizontal(4).PaddingVertical(2.5f).Row(row =>
                    {
                        row.ConstantItem(52).Text("Cliente:").Bold();
                        row.RelativeItem().Text(_datos.Cliente).FontColor(Colors.Black);
                        row.ConstantItem(52).Text("Servicio:").Bold();
                        row.RelativeItem().Text(_datos.Servicio).FontColor(Colors.Black);
                    });

                // Fila 3: Teléfono y Ciudad
                col.Item().BorderLeft(1).BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                    .Background(ColorFondoClaro).PaddingHorizontal(4).PaddingVertical(2.5f).Row(row =>
                    {
                        row.ConstantItem(52).Text("Localidad:").Bold();
                        row.RelativeItem().PaddingRight(10).Text(_datos.Ciudad).FontColor(Colors.Black);
                        row.ConstantItem(52).Text("Telefono:").Bold();
                        row.ConstantItem(85).Text(_datos.Telefono).FontColor(Colors.Black);
                    });
            });
        }

        private void ComponerTablaServicios(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(65);  // Cantidad:
                    columns.RelativeColumn();    // artículo / servicio
                    columns.ConstantColumn(75);  // Precio U.
                    columns.ConstantColumn(80);  // Importe.
                });

                // Encabezado de la tabla
                table.Header(header =>
                {
                    header.Cell().BorderLeft(1).BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                        .PaddingVertical(2).AlignCenter().Text("Cantidad:").FontSize(8).Bold();
                    header.Cell().BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                        .PaddingVertical(2).AlignCenter().Text("artículo / servicio").FontSize(8).Bold();
                    header.Cell().BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                        .PaddingVertical(2).AlignCenter().Text("Precio U.").FontSize(8).Bold();
                    header.Cell().BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                        .PaddingVertical(2).AlignCenter().Text("Importe.").FontSize(8).Bold();
                });

                // 10 filas cuadriculadas fijas igual que en la nota física
                for (int i = 0; i < _datos.Detalles.Count; i++)
                {
                    var item = _datos.Detalles[i];
                    float altoFila = 13.5f;

                    // Cantidad
                    table.Cell().BorderLeft(1).BorderRight(1).BorderBottom(0.8f).BorderColor(ColorPrimario)
                        .Height(altoFila).AlignMiddle().AlignCenter()
                        .Text(item != null ? item.Cantidad.ToString() : "").FontSize(8).FontColor(Colors.Black);

                    // Artículo / Servicio
                    table.Cell().BorderRight(1).BorderBottom(0.8f).BorderColor(ColorPrimario)
                        .Height(altoFila).AlignMiddle().PaddingHorizontal(4)
                        .Text(item?.Descripcion ?? "").FontSize(8).FontColor(Colors.Black);

                    // Precio Unitario
                    table.Cell().BorderRight(1).BorderBottom(0.8f).BorderColor(ColorPrimario)
                        .Height(altoFila).AlignMiddle().PaddingRight(4).AlignRight()
                        .Text(item != null ? item.PrecioUnitario.ToString("N2") : "").FontSize(8).FontColor(Colors.Black);

                    // Importe
                    table.Cell().BorderRight(1).BorderBottom(0.8f).BorderColor(ColorPrimario)
                        .Height(altoFila).AlignMiddle().PaddingRight(4).AlignRight()
                        .Text(item != null ? item.Importe.ToString("N2") : "").FontSize(8).FontColor(Colors.Black);
                }
            });
        }

        private void ComponerPieNota(IContainer container)
        {
            container.Column(col =>
            {
                // Fila: Cantidad con letra
                col.Item().BorderLeft(1).BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                    .Background(ColorFondoClaro).Row(row =>
                    {
                        row.RelativeItem().PaddingHorizontal(4).PaddingVertical(2.5f).Row(r =>
                        {
                            r.ConstantItem(92).Text("Cantidad con letra").Bold();
                            r.RelativeItem().Text(_datos.CantidadConLetra).FontSize(8).FontColor(Colors.Black);
                        });

                        // Mantiene la línea divisoria vertical de la última columna
                        row.ConstantItem(80).BorderLeft(1).BorderColor(ColorPrimario);
                    });

                // Fila: Nombre y Firma | Fecha de entrega | Nota revisión | Total:
                col.Item().BorderLeft(1).BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                    .Height(24).Row(row =>
                    {
                        // Nombre y Firma de aceptación
                        row.ConstantItem(175).BorderRight(1).BorderColor(ColorPrimario)
                            .PaddingLeft(4).PaddingBottom(2).AlignBottom()
                            .Text("Nombre y Firma de aceptación").FontSize(7.5f).Bold();

                        // Fecha de entrega:
                        row.ConstantItem(115).BorderRight(1).BorderColor(ColorPrimario)
                            .PaddingHorizontal(4).PaddingBottom(2).AlignBottom().Row(r =>
                            {
                                r.ConstantItem(62).Text("Fecha de entrega:").FontSize(6.5f).Bold();
                                r.RelativeItem().Text(_datos.FechaEntrega).FontSize(7.5f).FontColor(Colors.Black);
                            });

                        // Leyenda "EL CLIENTE DEBERA REVISAR SU EQUIPO A LA ENTREGA"
                        row.ConstantItem(78).BorderRight(1).BorderColor(ColorPrimario)
                            .Background(ColorFondoClaro).PaddingHorizontal(2).AlignMiddle().AlignCenter()
                            .Text("EL CLIENTE DEBERA REVISAR\nSU EQUIPO A LA ENTREGA").FontSize(4.6f).Bold();

                        // Etiqueta Total:
                        row.RelativeItem().PaddingRight(4).AlignMiddle().AlignRight()
                            .Text("Total:").FontSize(10).Bold();

                        // Valor Total
                        row.ConstantItem(80).BorderLeft(1).BorderColor(ColorPrimario)
                            .AlignMiddle().PaddingRight(4).AlignRight()
                            .Text($"$ {_datos.Total:N2}").FontSize(9.5f).Bold().FontColor(Colors.Black);
                    });
            });
        }

        private void ComponerClausulas(IContainer container)
        {
            container.PaddingHorizontal(2).Row(row =>
            {
                // Columna Izquierda: Puntos 1 al 5
                row.RelativeItem(1f).PaddingRight(6).Column(col =>
                {
                    col.Item().Text("1.-Después de 30 días no nos hacemos responsables por ningún equipo.").FontSize(4.8f);
                    col.Item().Text("2.- Toda garantía es de 30 días en reparaciones y refacciones por 15 días.").FontSize(4.8f);
                    col.Item().Text("3.- Deslindo de toda responsabilidad a MASTER SYSTEM de la procedencia de el equipo.").FontSize(4.8f);
                    col.Item().Text("4.- MASTER SYSTEM se hace responsable únicamente de la reparación de la falla reportada.").FontSize(4.8f);
                    col.Item().Text("5.- En caso de que el equipo haya sido mojado, golpeado o quemado y el cliente autorizara su reparación, esta sería sin responsabilidad para MASTER SYSTEM.").FontSize(4.8f);
                });

                // Columna Derecha: Puntos 6 al 11
                row.RelativeItem(1.15f).PaddingLeft(6).Column(col =>
                {
                    col.Item().Text("6.- Si después de 30 días naturales, el cliente no se comunica, se da por enterado y autoriza la venta de su equipo a MASTER SYSTEM para que este recupere los costos de reparación, resguardo, manejo y almacenaje.").FontSize(4.8f);
                    col.Item().Text("7.- No hay devoluciones de efectivo en apartados de equipo.").FontSize(4.8f);
                    col.Item().Text("8.- Sin nota no hay garantía, ni entregarán equipos.").FontSize(4.8f);
                    col.Item().Text("9.- Por perdida de nota el costo es de $ 50.00 ( el equipo se entregará previa identificación).").FontSize(4.8f);
                    col.Item().Text("10.- Toda revisión tiene un costo de $50.00").FontSize(4.8f);
                    col.Item().Text("11.-En Software no hay garantía.").FontSize(4.8f);
                });
            });
        }
    }
}