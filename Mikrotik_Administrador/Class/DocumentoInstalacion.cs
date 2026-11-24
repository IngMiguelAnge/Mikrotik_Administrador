using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.IO;

namespace Mikrotik_Administrador.Class
{
    public class DatosInstalacion
    {
        public string Folio { get; set; } = "1951";
        public DateTime Fecha { get; set; } = DateTime.Now;

        // Datos generales
        public string Instalador { get; set; } = "";
        public string NombreCliente { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Direccion { get; set; } = "";
        public string Modalidad { get; set; } = "";
        public string Paquete { get; set; } = "";

        // Equipos (Columna Izquierda)
        public string Antena { get; set; } = "";
        public string Cable { get; set; } = "";
        public string Router { get; set; } = "";
        public string Cargador { get; set; } = "";
        public string Tubo { get; set; } = "";

        // Red / Técnico (Columna Derecha)
        public string Box { get; set; } = "";
        public string Gps { get; set; } = "";
        public string Ip { get; set; } = "";
        public string Rb { get; set; } = "";
        public string Sector { get; set; } = "";

        // Primera renta
        public string DiaPrimerRenta { get; set; } = "";
        public string MesPrimerRenta { get; set; } = "";
        public string AnioPrimerRenta { get; set; } = "2026";
        public decimal MontoPrimerRenta { get; set; }

        // Cierre
        public string Observaciones { get; set; } = "";
        public decimal Total { get; set; }
    }

    public class DocumentoInstalacion : IDocument
    {
        private readonly DatosInstalacion _datos;
        private readonly string _rutaLogo;

        private static readonly string ColorPrimario = "#2D4263";    // Azul marino oscuro
        private static readonly string ColorFondoClaro = "#DCE2EB";  // Azul/gris tenue de las franjas

        public DocumentoInstalacion(DatosInstalacion datos, string rutaLogo = null)
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
                page.Margin(0.32f, Unit.Inch);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontFamily("Arial").FontSize(8.5f).FontColor(ColorPrimario));

                page.Content().Column(col =>
                {
                    // 1. ENCABEZADO
                    col.Item().Element(ComponerEncabezado);

                    col.Item().PaddingTop(4);

                    // 2. DATOS DEL CLIENTE E INSTALADOR (Con fecha arriba a la derecha)
                    col.Item().Element(ComponerDatosGenerales);

                    // 3. EQUIPOS Y PARÁMETROS DE RED (Antena, Cable... / Box, GPS, IP...)
                    col.Item().Element(ComponerEquiposYRed);

                    // 4. FRANJA DE PRIMERA RENTA MENSUAL
                    col.Item().Element(ComponerPrimeraRenta);

                    // 5. FIRMA Y NOTA LEGAL DE EQUIPOS
                    col.Item().Element(ComponerFirmaYNota);

                    // 6. OBSERVACIONES Y TOTAL
                    col.Item().Element(ComponerPieNota);
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
                        col.Item().Height(44).Image(_rutaLogo).FitArea();
                    }
                    else
                    {
                        col.Item().Text("MASTER").FontSize(16).Bold().FontColor(ColorPrimario);
                        col.Item().Text("S Y S T E M").FontSize(9).LetterSpacing(0.3f).FontColor(ColorPrimario);
                    }
                });

                // Datos fiscales
                row.RelativeItem().PaddingHorizontal(5).AlignCenter().Column(col =>
                {
                    col.Item().AlignCenter().Text("INTERNET INALAMBRICO")
                        .FontSize(15).Bold().FontColor(ColorPrimario);
                    col.Item().AlignCenter().Text("PARA TU CASA O NEGOCIO")
                        .FontSize(8).Bold();
                    col.Item().AlignCenter().Text("Asesoría, Venta, Reparación de Computadoras, E Instalación de Redes")
                        .FontSize(6.5f).Bold();
                    col.Item().AlignCenter().Text("I.S.C. CRUZ MARTÍNEZ RODRIGUEZ   R.F.C. MARC820503MU8")
                        .FontSize(6.5f).Bold();
                    col.Item().AlignCenter().Text("AV.PUEBLA #4 TLACOTEPEC DE BENITO JUÁREZ PUEBLA C.P 75680")
                        .FontSize(6f).Bold();
                    col.Item().AlignCenter().Text("CEL: 2371002700   CEL: 2371118548")
                        .FontSize(6.5f).Bold();
                });

                // Recuadro Nota de Venta y Folio
                row.ConstantItem(130).Column(col =>
                {
                    col.Item().Border(1).BorderColor(ColorPrimario).Column(box =>
                    {
                        box.Item().Background(ColorPrimario).PaddingVertical(2).AlignCenter()
                            .Text("NOTA DE VENTA").FontSize(8).Bold().FontColor(Colors.White);

                        box.Item().Background(ColorFondoClaro).PaddingVertical(5).AlignCenter()
                            .Text($"Nº      {_datos.Folio}")
                            .FontSize(12).Bold().FontColor("#B22222");
                    });

                    col.Item().PaddingTop(2).AlignCenter()
                        .Text("master.system@hotmail.com").FontSize(7f).Bold();
                });
            });
        }

        private void ComponerDatosGenerales(IContainer container)
        {
            container.Column(col =>
            {
                // Fila 1: Instalador + Cajas DIA / MES / AÑO
                col.Item().Border(1).BorderColor(ColorPrimario).Background(ColorFondoClaro)
                    .Row(row =>
                    {
                        row.RelativeItem().PaddingHorizontal(5).PaddingVertical(4).Row(r =>
                        {
                            r.ConstantItem(55).AlignBottom().Text("Instalador:").Bold();
                            r.RelativeItem().AlignBottom().Text(_datos.Instalador).FontColor(Colors.Black);
                        });

                        // Sub-tabla para DIA, MES, AÑO en la esquina superior derecha
                        row.ConstantItem(180).BorderLeft(1).BorderColor(ColorPrimario).Column(fechaCol =>
                        {
                            // Etiquetas pequeñas arriba
                            fechaCol.Item().Row(r =>
                            {
                                r.RelativeItem().BorderRight(1).BorderColor(ColorPrimario).AlignCenter()
                                    .Text("DIA").FontSize(5.5f).Bold();
                                r.RelativeItem().BorderRight(1).BorderColor(ColorPrimario).AlignCenter()
                                    .Text("MES").FontSize(5.5f).Bold();
                                r.RelativeItem().AlignCenter()
                                    .Text("AÑO").FontSize(5.5f).Bold();
                            });

                            // Valores de fecha abajo
                            fechaCol.Item().PaddingBottom(2).Row(r =>
                            {
                                r.RelativeItem().BorderRight(1).BorderColor(ColorPrimario).AlignCenter()
                                    .Text(_datos.Fecha.ToString("dd")).FontSize(8.5f).Bold().FontColor(Colors.Black);
                                r.RelativeItem().BorderRight(1).BorderColor(ColorPrimario).AlignCenter()
                                    .Text(_datos.Fecha.ToString("MM")).FontSize(8.5f).Bold().FontColor(Colors.Black);
                                r.RelativeItem().AlignCenter()
                                    .Text(_datos.Fecha.ToString("yyyy")).FontSize(8.5f).Bold().FontColor(Colors.Black);
                            });
                        });
                    });

                // Fila 2: Nombre y Teléfono
                col.Item().BorderLeft(1).BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                    .PaddingHorizontal(5).PaddingVertical(3).Row(row =>
                    {
                        row.ConstantItem(48).Text("Nombre:").Bold();
                        row.RelativeItem().Text(_datos.NombreCliente).FontColor(Colors.Black);
                        row.ConstantItem(50).Text("Telefono:").Bold();
                        row.ConstantItem(150).Text(_datos.Telefono).FontColor(Colors.Black);
                    });

                // Fila 3: Dirección y Localidad
                col.Item().BorderLeft(1).BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                    .Background(ColorFondoClaro).PaddingHorizontal(5).PaddingVertical(3).Row(row =>
                    {
                        row.ConstantItem(52).Text("Localidad:").Bold();
                        row.RelativeItem().Text(_datos.Direccion).FontColor(Colors.Black);
                    });

                // Fila 4: Instalación de internet modalidad y Paquete
                col.Item().BorderLeft(1).BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                    .PaddingHorizontal(5).PaddingVertical(3).Row(row =>
                    {
                        row.ConstantItem(148).Text("Instalación de internet modalidad:").Bold();
                        row.RelativeItem().Text(_datos.Modalidad).FontColor(Colors.Black);
                        row.ConstantItem(45).Text("Paquete:").Bold();
                        row.ConstantItem(150).Text(_datos.Paquete).FontColor(Colors.Black);
                    });
            });
        }

        private void ComponerEquiposYRed(IContainer container)
        {
            container.BorderLeft(1).BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                .Background(ColorFondoClaro).PaddingHorizontal(8).PaddingVertical(5).Row(row =>
                {
                    // Columna Izquierda: Equipos
                    row.RelativeItem().PaddingRight(15).Column(col =>
                    {
                        col.Item().Element(c => FilaLineaEquipo(c, "ANTENA", _datos.Antena, 65));
                        col.Item().Element(c => FilaLineaEquipo(c, "CABLE", _datos.Cable, 65));
                        col.Item().Element(c => FilaLineaEquipo(c, "ROUTER", _datos.Router, 65));
                        col.Item().Element(c => FilaLineaEquipo(c, "CARGADOR", _datos.Cargador, 65));
                        col.Item().Element(c => FilaLineaEquipo(c, "TUBO", _datos.Tubo, 65));
                    });

                    // Columna Derecha: Datos de Red / Técnico
                    row.RelativeItem().PaddingLeft(15).Column(col =>
                    {
                        col.Item().Element(c => FilaLineaEquipo(c, "BOX.", _datos.Box, 50));
                        col.Item().Element(c => FilaLineaEquipo(c, "GPS", _datos.Gps, 50));
                        col.Item().Element(c => FilaLineaEquipo(c, "IP", _datos.Ip, 50));
                        col.Item().Element(c => FilaLineaEquipo(c, "RB", _datos.Rb, 50));
                        col.Item().Element(c => FilaLineaEquipo(c, "SECTOR", _datos.Sector, 50));
                    });
                });
        }

        // Helper para dibujar cada renglón con etiqueta a la izquierda y texto pegado a la línea inferior
        private void FilaLineaEquipo(IContainer container, string etiqueta, string valor, float anchoEtiqueta)
        {
            container.Height(15).Row(row =>
            {
                row.ConstantItem(anchoEtiqueta).AlignBottom().PaddingBottom(1)
                    .Text(etiqueta).FontSize(8.5f).Bold();

                row.RelativeItem().BorderBottom(0.8f).BorderColor(ColorPrimario)
                    .AlignBottom().PaddingBottom(1).PaddingLeft(4)
                    .Text(valor ?? "").FontSize(8.5f).FontColor(Colors.Black);
            });
        }

        private void ComponerPrimeraRenta(IContainer container)
        {
            container.BorderLeft(1).BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                .PaddingHorizontal(6).PaddingVertical(4).Row(row =>
                {
                    row.ConstantItem(175).AlignBottom().PaddingBottom(1)
                        .Text("SU PRIMER RENTA MENSUAL ES EL DIA:").FontSize(7.8f).Bold();

                    // Día
                    row.ConstantItem(65).BorderBottom(0.8f).BorderColor(ColorPrimario)
                        .AlignBottom().PaddingBottom(1).AlignCenter()
                        .Text(_datos.DiaPrimerRenta).FontSize(8).FontColor(Colors.Black);

                    row.ConstantItem(45).AlignBottom().PaddingBottom(1).AlignCenter()
                        .Text("DEL MES").FontSize(7.8f).Bold();

                    // Mes
                    row.ConstantItem(75).BorderBottom(0.8f).BorderColor(ColorPrimario)
                        .AlignBottom().PaddingBottom(1).AlignCenter()
                        .Text(_datos.MesPrimerRenta).FontSize(8).FontColor(Colors.Black);

                    row.ConstantItem(42).AlignBottom().PaddingBottom(1).AlignCenter()
                        .Text($"DE {_datos.AnioPrimerRenta}").FontSize(7.8f).Bold();

                    row.ConstantItem(32).AlignBottom().PaddingBottom(1).AlignRight().PaddingRight(3)
                        .Text("POR $").FontSize(7.8f).Bold();

                    // Monto primera renta
                    row.RelativeItem().BorderBottom(0.8f).BorderColor(ColorPrimario)
                        .AlignBottom().PaddingBottom(1).PaddingLeft(4)
                        .Text(_datos.MontoPrimerRenta > 0 ? _datos.MontoPrimerRenta.ToString("N2") : "")
                        .FontSize(8).Bold().FontColor(Colors.Black);
                });
        }

        private void ComponerFirmaYNota(IContainer container)
        {
            container.BorderLeft(1).BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                .Height(46).Row(row =>
                {
                    // Columna Izquierda: Nombre y Firma de aceptación
                    row.RelativeItem(1.05f).Background(ColorFondoClaro).PaddingTop(4).Column(col =>
                    {
                        col.Item().AlignCenter().Text("Nombre y Firma de aceptación").FontSize(8.5f).Bold();
                    });

                    // Columna Derecha: Nota de equipos prestados
                    row.RelativeItem(1f).BorderLeft(1).BorderColor(ColorPrimario)
                        .Background(ColorFondoClaro).PaddingHorizontal(6).PaddingVertical(4).Column(col =>
                        {
                            col.Item().AlignCenter().Text("NOTA: LOS EQUIPOS SON PRESTADOS POR EL TIEMPO QUE REQUIERAN DEL SERVICIO,")
                                .FontSize(5.7f).Bold();
                            col.Item().AlignCenter().Text("EN CASO DE DAÑO Ó EXTRAVIO EL USUARIO CUBRIRÁ EL MONTO DEL EQUIPO.")
                                .FontSize(5.7f).Bold();

                            col.Item().PaddingTop(4).AlignCenter()
                                .Text("El pago de esta nota cubre mano de obra y el pago de la dirección IP")
                                .FontSize(6.5f).Bold();
                        });
                });
        }

        private void ComponerPieNota(IContainer container)
        {
            container.BorderLeft(1).BorderRight(1).BorderBottom(1).BorderColor(ColorPrimario)
                .Height(30).Row(row =>
                {
                    // Observaciones
                    row.RelativeItem().Padding(5).Column(col =>
                    {
                        col.Item().Text("OBSERVACIONES").FontSize(8).Bold();
                        col.Item().Text(_datos.Observaciones).FontSize(8).FontColor(Colors.Black);
                    });

                    // Etiqueta Total:
                    row.ConstantItem(65).AlignMiddle().AlignRight().PaddingRight(5)
                        .Text("Total:").FontSize(12).Bold();

                    // Recuadro del monto total
                    row.ConstantItem(120).BorderLeft(1).BorderColor(ColorPrimario)
                        .Background(ColorFondoClaro).AlignMiddle().PaddingHorizontal(6).Row(r =>
                        {
                            r.ConstantItem(15).Text("$").FontSize(10).Bold();
                            r.RelativeItem().AlignRight()
                                .Text(_datos.Total > 0 ? _datos.Total.ToString("N2") : "")
                                .FontSize(10).Bold().FontColor(Colors.Black);
                        });
                });
        }
    }
}