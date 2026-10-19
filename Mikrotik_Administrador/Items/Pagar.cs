using DocumentFormat.OpenXml.Office2010.PowerPoint;
using GMap.NET.MapProviders;
using ImageMagick;
using Microsoft.IdentityModel.Logging;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador.Items
{
    public partial class Pagar : Form
    {
        public int IdHistorial { get; set; }
        public int IdResponsable { get; set; }
        public string Cliente { get; set; }
        public string UsuarioM { get; set; }
        public int IdUsuarioM { get; set; }
        public decimal TotalFaltante { get; set; }
        public int IdMensualidadRecibida { get; set; }
        private DateTime FechaCorte;
        private decimal MensualidadFinal;
        private decimal PagoOriginal;
        List<ListMensualidadesModel> ListaPagos = new List<ListMensualidadesModel>();
        public Pagar()
        {
            InitializeComponent();
        }

        private void btnSubir_Click(object sender, EventArgs e)
        {
            OpenFileDialog selector = new OpenFileDialog();
            selector.Filter = "Imagenes|*.jpg;*.jpeg;*.png;*.bmp;*.webp";

            if (selector.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // 1. Usamos MagickImage para leer el archivo (aunque sea WebP con extensión .jpg)
                    using (MagickImage image = new MagickImage(selector.FileName))
                    {
                        // 2. Definimos explícitamente el formato a Bmp para asegurar compatibilidad
                        image.Format = MagickFormat.Bmp;

                        // 3. Obtenemos los bytes de la imagen convertida
                        byte[] bytes = image.ToByteArray();

                        using (MemoryStream ms = new MemoryStream(bytes))
                        {
                            // 4. CREAR UN NUEVO BITMAP: Esto es vital. 
                            // Al hacer 'new Bitmap(ms)', el PictureBox ya no depende del stream.
                            Bitmap bmp = new Bitmap(ms);

                            // 5. Limpieza de memoria de la imagen anterior
                            if (PBImagen.Image != null)
                            {
                                PBImagen.Image.Dispose();
                            }

                            // 6. Asignamos la nueva imagen y refrescamos
                            PBImagen.Image = bmp;
                            PBImagen.SizeMode = PictureBoxSizeMode.Zoom;
                            PBImagen.Refresh(); // Forzamos al control a redibujarse
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error visualizando la imagen: " + ex.Message);
                }
            }
        }

        private void Pagar_Load(object sender, EventArgs e)
        {
            AppRepository obj = new AppRepository();
            var ListBancos = obj.GetBancosActivos().Result.OrderBy(x => x.Nombre).ToList();
            // Insertamos un objeto "fantasma" al inicio para el placeholder
            ListBancos.Insert(0, new ListBancosModel { Id = 0, Nombre = "Seleccione" });
            CBBanco.DataSource = null;
            CBBanco.DisplayMember = "Nombre";
            CBBanco.ValueMember = "Id";
            CBBanco.DataSource = ListBancos;
            decimal TotalPagado = 0m;
            decimal TotalMensualidad = 0m;
            ListaPagos = new List<ListMensualidadesModel>();
            if (IdHistorial == 0)
            {
                CBBanco.SelectedIndex = 0;
                //Saca el total por todas las mensualidades
                List<MensualidadModel> ListaMensualidades = new List<MensualidadModel>();
                ListaMensualidades = obj.GetFaltantesPagarbyIdUsuarioM(IdUsuarioM).Result.ToList();
                if (IdMensualidadRecibida != 0)
                {
                    ListaMensualidades = ListaMensualidades.Where(x => x.Id == IdMensualidadRecibida).ToList();
                }
                if (ListaMensualidades.Count() > 0)
                {

                    foreach (var item in ListaMensualidades)
                    {
                        var Pagos = obj.GetHistorialPagos(item.Id, string.Empty, 0, 0);
                        decimal TotalHistorial = Pagos.Result.Where(x => x.Estatus == "Activo").Sum(x => x.Cantidad);
                        TotalMensualidad += item.Mensualidad ?? 0m;
                        TotalPagado += TotalHistorial;
                        TotalFaltante +=
                        (item.Mensualidad ?? 0m) - TotalHistorial < 0 ? 0 :
                        (item.Mensualidad ?? 0m) - TotalHistorial;
                        ListaPagos.Add(new ListMensualidadesModel
                        {
                            Id = item.Id,
                            DiaCorte = item.DiaCorte,
                            FechaInicio = item.FechaInicio,
                            FechaLimite = item.FechaLimite,
                            Responsable = string.Empty,
                            Mensualidad = item.Mensualidad ?? 0m,
                            Recibido = TotalHistorial,
                            Faltante = (item.Mensualidad ?? 0m) - TotalHistorial < 0 ? 0 :
                           (item.Mensualidad ?? 0m) - TotalHistorial,
                            Estatus = item.Estatus ? "Activo" : "Inactivo"
                        });
                    }
                    lblTotalMensualidades.Text = $"Total mensualidades: {TotalMensualidad:C}";
                    lblTotalPagado.Text = $"Total pagado: {TotalPagado:C}";
                    lblFaltante.Text = $"Total a pagar: {TotalFaltante:C}";
                }
                else
                {
                    MessageBox.Show("No hay mensualidades pendientes de pago.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }
            }
            else
            {
                //Solo nos interesa la mensualidad de ese pago
                var Pago = obj.GetHistorialPagosById(IdHistorial).Result;
                PagoOriginal = Pago.Cantidad;
                var Pagos = obj.GetHistorialPagos(Pago.IdMensualidad, string.Empty, 0, 0);
                TotalPagado = Pagos.Result.Where(x => x.Estatus == "Activo").Sum(x => x.Cantidad);
                lblTotalPagado.Text = $"Total pagado: {TotalPagado:C}";
                var Mensualidad = obj.GetMensualidadbyId(Pago.IdMensualidad).Result;
                lblTotalMensualidades.Text = $"Total mensualidades: {Mensualidad.Mensualidad:C}";
                TotalFaltante = (Mensualidad.Mensualidad ?? 0m) - TotalPagado < 0 ? 0 :
                    (Mensualidad.Mensualidad ?? 0m) - TotalPagado;
                lblFaltante.Text = $"Total a pagar: {TotalFaltante:C}";
                CBBanco.SelectedValue = Pago.IdBanco;
                txtReferencia.Text = Pago.Referencia;
                txtComentario.Text = Pago.Comentario;
                if (Pago.Imagen != null && Pago.Imagen.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream(Pago.Imagen))
                    {
                        PBImagen.Image = new Bitmap(ms);
                        PBImagen.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
                dtpFechaPago.Value = Pago.FechaRecibido;
                NUDCantidad.Value = Pago.Cantidad;
                NUDCantidad.Visible = true;
                lblCantidadRecibida.Visible = true;
                ListaPagos.Add(new ListMensualidadesModel
                {
                    Id = Pago.IdMensualidad,
                    DiaCorte = Mensualidad.DiaCorte,
                    FechaInicio = Mensualidad.FechaInicio,
                    FechaLimite = Mensualidad.FechaLimite,
                    Responsable = string.Empty,
                    Mensualidad = Mensualidad.Mensualidad ?? 0m,
                    Recibido = TotalPagado,
                    Faltante = TotalFaltante,
                    Estatus = Mensualidad.Estatus ? "Activo" : "Inactivo"
                });
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtReferencia.Text.Trim() == string.Empty
                || CBBanco.SelectedIndex == 0)
            {
                MessageBox.Show("Datos incompletos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            decimal Recibido = 0;
            if (IdHistorial == 0)
            {
                ConfirmarPago confirmacion = new ConfirmarPago();
                confirmacion.Total = TotalFaltante;
                confirmacion.IdMensualidadRecibido = IdMensualidadRecibida;
                if (confirmacion.ShowDialog() != DialogResult.OK)
                { return; }
                Recibido = confirmacion.Recibido;
            }
            else
            {
                Recibido = NUDCantidad.Value;
            }

            AppRepository obj = new AppRepository();
            List<int> ListIdHistorial = new List<int>();
            string ExtraNota = string.Empty;
            ExtraNota += Environment.NewLine + "Nota: Se recibio un total de $" + Recibido +
                " y se repartio de la siguiente forma:" + Environment.NewLine;
            if (IdHistorial == 0)
            {
                foreach (var item in ListaPagos)
                {
                    HistorialPagosModel sv = new HistorialPagosModel
                    {
                        Id = 0,
                        FechaRecibido = dtpFechaPago.Value,
                        Cantidad = Recibido - item.Faltante < 0 ? Recibido
                        : item.Faltante,
                        IdMensualidad = item.Id,
                        Comentario = txtComentario.Text.Trim(),
                        IdBanco = (int)CBBanco.SelectedValue,
                        Referencia = txtReferencia.Text.Trim(),
                        Imagen = ImageToByteArray(),
                        IdUsuario = IdResponsable
                    };
                    var result = obj.SaveHistorialPagos(sv).Result;
                    ListIdHistorial.Add(result);
                    ExtraNota += "Mensualidad con fecha limite: " + item.FechaLimite.ToShortDateString()
                        + " $" + sv.Cantidad.ToString() + Environment.NewLine;
                    Recibido = Recibido - sv.Cantidad < 0 ? 0 : Recibido - sv.Cantidad;
                }
                if (Recibido > 0)
                {
                    var UltimaMensualidad = obj.GetUltimaMensualidadbyIdUsuarioM(IdUsuarioM).Result;
                    while (Recibido > 0)
                    {
                        await CambiarFecha(UltimaMensualidad.DiaCorte, UltimaMensualidad.FechaLimite);
                        if (MensualidadFinal == 0)
                        {
                            MessageBox.Show("No se logro calcular la mensualidad para el siguiente mes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                        MensualidadModel mensualidad = new MensualidadModel
                        {
                            Id = 0,
                            Pagado = false,//No importa si pones true internamente se calculara
                            IdUsuarioM = IdUsuarioM,
                            DiaCorte = UltimaMensualidad.DiaCorte,
                            FechaInicio = UltimaMensualidad.FechaLimite,
                            FechaLimite = FechaCorte,
                            IdUsuario = IdResponsable,
                            Mensualidad = MensualidadFinal,
                        };
                        var result = obj.SaveMensualidad(mensualidad).Result;
                        HistorialPagosModel sv = new HistorialPagosModel
                        {
                            Id = 0,
                            FechaRecibido = dtpFechaPago.Value,
                            Cantidad = Recibido - MensualidadFinal < 0 ? Recibido
                            : MensualidadFinal,
                            IdMensualidad = result,
                            Comentario = txtComentario.Text.Trim(),
                            IdBanco = (int)CBBanco.SelectedValue,
                            Referencia = txtReferencia.Text.Trim(),
                            Imagen = ImageToByteArray(),
                            IdUsuario = IdResponsable
                        };
                        var result2 = obj.SaveHistorialPagos(sv).Result;
                        ListIdHistorial.Add(result2);
                        ExtraNota += "Mensualidad con fecha limite: " + mensualidad.FechaLimite.ToShortDateString()
                        + " $" + sv.Cantidad.ToString() + Environment.NewLine;
                        Recibido = Recibido - sv.Cantidad < 0 ? 0 : Recibido - sv.Cantidad;
                    }
                }
            }
            else
            {
                if (PagoOriginal != Recibido)
                {
                    if (PagoOriginal < Recibido)
                    {
                        bool Revasototal = ListaPagos.First().Recibido + (Recibido - PagoOriginal) >
                              ListaPagos.First().Faltante ? true : false;
                        if (Revasototal)
                        {
                            MessageBox.Show("No se puede agregar una cantidad mayor a lo que falta por pagar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    HistorialPagosModel sv = new HistorialPagosModel
                    {
                        Id = IdHistorial,
                        FechaRecibido = dtpFechaPago.Value,
                        Cantidad = NUDCantidad.Value,
                        IdMensualidad = ListaPagos.FirstOrDefault().Id,
                        Comentario = txtComentario.Text.Trim(),
                        IdBanco = (int)CBBanco.SelectedValue,
                        Referencia = txtReferencia.Text.Trim(),
                        Imagen = ImageToByteArray(),
                        IdUsuario = IdResponsable
                    };
                    var result = obj.SaveHistorialPagos(sv).Result;
                    ExtraNota = string.Empty;
                }
                else
                {
                    ListIdHistorial.Add(IdHistorial);
                    HistorialPagosModel sv = new HistorialPagosModel
                    {
                        Id = IdHistorial,
                        FechaRecibido = dtpFechaPago.Value,
                        Cantidad = NUDCantidad.Value,
                        IdMensualidad = IdMensualidadRecibida,
                        Comentario = txtComentario.Text.Trim(),
                        IdBanco = (int)CBBanco.SelectedValue,
                        Referencia = txtReferencia.Text.Trim(),
                        Imagen = ImageToByteArray(),
                        IdUsuario = IdResponsable
                    };
                    int res = obj.SaveHistorialPagos(sv).Result;
                    ExtraNota = string.Empty;
                }
            }

            ExtraNota = txtComentario.Text.ToString().Trim() != string.Empty ?
                ExtraNota.TrimEnd() : ExtraNota.TrimEnd().TrimStart();
            bool r = false;
            foreach (var result in ListIdHistorial)
            {
                r = obj.UpdateNotaExtra(result, ExtraNota).Result;
            }
            MessageBox.Show("Pago guardada");
            DialogResult = DialogResult.OK;
        }
        public async Task CalcularCostos(DateTime FechaInicio, DateTime FechaCorte)
        {
            var tramossincambios = await ObtenerSoloTramosBaseAsync(IdUsuarioM, FechaInicio, FechaCorte);
            var tramostotales = await CalcularTramosMensualidadAsync(IdUsuarioM, FechaInicio, FechaCorte);

            decimal sumaCambios = tramostotales.Sum(t => t.Costo);
            decimal sumaBase = tramossincambios.Sum(t => t.Costo);

            MensualidadFinal = sumaCambios == 0 ? sumaBase : sumaCambios;
        }
        public async Task<List<ListDetallesMensualidadModel>> ObtenerSoloTramosBaseAsync(int idUsuarioM, DateTime desde, DateTime hasta)
        {
            AppRepository obj = new AppRepository();
            var Detalles = await obj.GetTiempoCambioforDetalles(idUsuarioM, desde, hasta);

            List<ListDetallesMensualidadModel> ListDestalles = new List<ListDetallesMensualidadModel>();

            var usuarioMikrotik = await obj.GetUsuariosMikrotiksById(idUsuarioM);
            var planBasePeriodo = await obj.GetPlanById(usuarioMikrotik.IdPlanOriginal);
            string nombrePlanActual = planBasePeriodo != null ? planBasePeriodo.Nombre : "Plan Base";
            decimal precioPlanActual = planBasePeriodo != null ? planBasePeriodo.Precio : 0m;

            var cambioAnterior = Detalles?
                .Where(x => x.FechaFin < desde && x.Estatus != "Cancelado")
                .OrderByDescending(x => x.FechaFin)
                .FirstOrDefault();

            if (cambioAnterior != null)
            {
                var planAnt = await obj.GetPlanById(cambioAnterior.IdPlanOriginal);
                if (planAnt != null)
                {
                    nombrePlanActual = planAnt.Nombre;
                    precioPlanActual = planAnt.Precio;
                }
            }

            var cambiosOrdenados = Detalles?
                .Where(x => x.FechaInicio <= hasta && x.FechaFin >= desde && x.Estatus != "Cancelado")
                .OrderBy(x => x.FechaInicio)
                .ToList() ?? new List<TiempoCambioModel>();

            DateTime cursor = desde;

            foreach (var cambio in cambiosOrdenados)
            {
                if (cursor < cambio.FechaInicio)
                {
                    DateTime finTramoBase = cambio.FechaInicio.AddDays(-1);
                    if (finTramoBase > hasta) finTramoBase = hasta;

                    if (cursor <= finTramoBase)
                    {
                        int diasBase = (int)(finTramoBase - cursor).TotalDays + 1;
                        decimal costoBase = 0m;

                        bool esMesCompletoExacto = (cursor.Day == finTramoBase.AddDays(1).Day) &&
                                                   (cursor.AddMonths(1).Date == finTramoBase.AddDays(1).Date);

                        if (esMesCompletoExacto || diasBase >= 28 && diasBase <= 31 && cursor.Month != finTramoBase.Month)
                        {
                            costoBase = precioPlanActual;
                        }
                        else
                        {
                            costoBase = RedondearMontoFinanciero(diasBase * (precioPlanActual / 30.0m));
                        }

                        ListDestalles.Add(new ListDetallesMensualidadModel
                        {
                            Id = 0,
                            FechaInicio = cursor,
                            FechaFin = finTramoBase,
                            Estatus = "Activo",
                            Plan = nombrePlanActual,
                            Costo = costoBase
                        });
                    }
                }

                cursor = cambio.FechaFin >= hasta ? hasta.AddDays(1) : cambio.FechaFin.AddDays(1);

                var planOrigCambio = await obj.GetPlanById(cambio.IdPlanOriginal);
                if (planOrigCambio != null)
                {
                    nombrePlanActual = planOrigCambio.Nombre;
                    precioPlanActual = planOrigCambio.Precio;
                }
            }

            if (cursor <= hasta)
            {
                DateTime fechaFinTramoFinal = hasta;
                if (cursor <= fechaFinTramoFinal)
                {
                    int diasFinales = (int)(fechaFinTramoFinal - cursor).TotalDays + 1;
                    decimal costoFinal = 0m;

                    // Validación robusta para el tramo final de un mes completo (ej. 14 de enero al 14 de febrero)
                    bool esMesCompletoFinal = (cursor.Date == desde.Date && fechaFinTramoFinal.Date == desde.AddMonths(1).Date);

                    // Si es un mes completo exacto, ignoramos si trajo 31 días y cobramos la tarifa plana
                    if (esMesCompletoFinal)
                    {
                        costoFinal = precioPlanActual; // Forzará los $300 cerrados
                    }
                    else
                    {
                        costoFinal = RedondearMontoFinanciero(diasFinales * (precioPlanActual / 30.0m));
                    }

                    ListDestalles.Add(new ListDetallesMensualidadModel
                    {
                        Id = 0,
                        FechaInicio = cursor,
                        FechaFin = fechaFinTramoFinal,
                        Estatus = "Activo",
                        Plan = nombrePlanActual,
                        Costo = costoFinal
                    });
                }
            }

            return ListDestalles;
        }
        private decimal RedondearMontoFinanciero(decimal monto)
        {
            decimal parteEntera = Math.Floor(monto);
            decimal parteDecimal = monto - parteEntera;

            if (parteDecimal > 0.00m && parteDecimal < 0.30m)
                return parteEntera;
            else if (parteDecimal >= 0.30m && parteDecimal <= 0.50m)
                return parteEntera + 0.50m;
            else if (parteDecimal > 0.50m)
                return parteEntera + 1.00m;

            return parteEntera;
        }

        public async Task<List<ListDetallesMensualidadModel>> CalcularTramosMensualidadAsync(int idUsuarioM, DateTime desde, DateTime hasta)
        {
            AppRepository obj = new AppRepository();
            var Detalles = await obj.GetTiempoCambioforDetalles(idUsuarioM, desde, hasta);
            if (Detalles == null || !Detalles.Any())
            {
                return new List<ListDetallesMensualidadModel>();
            }

            List<ListDetallesMensualidadModel> ListDestalles = new List<ListDetallesMensualidadModel>();

            var usuarioMikrotik = await obj.GetUsuariosMikrotiksById(idUsuarioM);
            var planBasePeriodo = await obj.GetPlanById(usuarioMikrotik.IdPlanOriginal);
            string nombrePlanActual = planBasePeriodo != null ? planBasePeriodo.Nombre : "Plan Base";
            decimal precioPlanActual = planBasePeriodo != null ? planBasePeriodo.Precio : 0m;

            var cambioAnterior = Detalles
                .Where(x => x.FechaFin < desde && x.Estatus != "Cancelado")
                .OrderByDescending(x => x.FechaFin)
                .FirstOrDefault();

            if (cambioAnterior != null)
            {
                var planAnt = await obj.GetPlanById(cambioAnterior.IdPlanOriginal);
                if (planAnt != null)
                {
                    nombrePlanActual = planAnt.Nombre;
                    precioPlanActual = planAnt.Precio;
                }
            }

            var cambiosOrdenados = Detalles
                .Where(x => x.FechaInicio <= hasta && x.FechaFin >= desde && x.Estatus != "Cancelado")
                .OrderBy(x => x.FechaInicio)
                .ToList();

            DateTime cursor = desde;

            foreach (var cambio in cambiosOrdenados)
            {
                if (cursor < cambio.FechaInicio)
                {
                    DateTime finTramoBase = cambio.FechaInicio.AddDays(-1);
                    if (finTramoBase > hasta) finTramoBase = hasta;

                    if (cursor <= finTramoBase)
                    {
                        int diasBase = (int)(finTramoBase - cursor).TotalDays + 1;
                        decimal costoBase = RedondearMontoFinanciero(diasBase * (precioPlanActual / 30.0m));

                        ListDestalles.Add(new ListDetallesMensualidadModel
                        {
                            Id = 0, // O el Id de la mensualidad base correspondiente si lo manejas
                            FechaInicio = cursor,
                            FechaFin = finTramoBase,
                            Estatus = "Activo",
                            Plan = nombrePlanActual,
                            Costo = costoBase
                        });
                    }
                    cursor = cambio.FechaInicio;
                }

                DateTime inicioCambioEfectivo = cursor > cambio.FechaInicio ? cursor : cambio.FechaInicio;
                DateTime finCambioEfectivo = hasta < cambio.FechaFin ? hasta : cambio.FechaFin;

                if (inicioCambioEfectivo <= finCambioEfectivo)
                {
                    int diasCambio = (int)(finCambioEfectivo - inicioCambioEfectivo).TotalDays + 1;
                    var planNuevo = await obj.GetPlanById(cambio.IdPlan);
                    decimal precioPlanNuevo = planNuevo != null ? planNuevo.Precio : 0m;
                    decimal costoCambio = RedondearMontoFinanciero(diasCambio * (precioPlanNuevo / 30.0m));

                    ListDestalles.Add(new ListDetallesMensualidadModel
                    {
                        Id = cambio.Id, // ID del cambio / mensualidad a afectar
                        FechaInicio = inicioCambioEfectivo,
                        FechaFin = finCambioEfectivo,
                        Estatus = cambio.Estatus,
                        Plan = cambio.Plan,
                        Costo = costoCambio
                    });

                    cursor = finCambioEfectivo.AddDays(1);
                }

                var planOrigCambio = await obj.GetPlanById(cambio.IdPlanOriginal);
                if (planOrigCambio != null)
                {
                    nombrePlanActual = planOrigCambio.Nombre;
                    precioPlanActual = planOrigCambio.Precio;
                }
            }

            if (cursor <= hasta)
            {
                DateTime fechaFinTramoFinal = hasta;
                if (cursor <= fechaFinTramoFinal)
                {
                    int diasFinales = (int)(fechaFinTramoFinal - cursor).TotalDays + 1;
                    decimal costoFinal = RedondearMontoFinanciero(diasFinales * (precioPlanActual / 30.0m));

                    ListDestalles.Add(new ListDetallesMensualidadModel
                    {
                        Id = 0,
                        FechaInicio = cursor,
                        FechaFin = fechaFinTramoFinal,
                        Estatus = "Activo",
                        Plan = nombrePlanActual,
                        Costo = costoFinal
                    });
                }
            }

            return ListDestalles;
        }

        public async Task CambiarFecha(int DiaCorte, DateTime FechaInicio)
        {
            // 1. Determinamos el año y mes base del inicio
            int anioCorte = FechaInicio.Year;
            int mesCorte = FechaInicio.Month;

            // 2. Evaluamos si el corte cae en el mismo mes o pasa al siguiente
            // Si el día de corte es menor o igual al día de inicio, corresponde al mes siguiente.
            if (DiaCorte <= FechaInicio.Day)
            {
                // Sumamos un mes respetando el salto de año si es diciembre
                DateTime siguienteMes = FechaInicio.AddMonths(1);
                anioCorte = siguienteMes.Year;
                mesCorte = siguienteMes.Month;
            }

            // 3. Obtenemos el máximo de días que tiene el mes calculado (ej. 28 para febrero, 30, 31, etc.)
            int diasEnElMes = DateTime.DaysInMonth(anioCorte, mesCorte);

            // 4. Si el día deseado es mayor que los días del mes (ej. elegiste 30 pero febrero tiene 28), 
            // se ajusta automáticamente al último día disponible del mes.
            int diaFinal = Math.Min(DiaCorte, diasEnElMes);

            // 5. Creamos la fecha final de corte y la asignamos al Label
            FechaCorte = new DateTime(anioCorte, mesCorte, diaFinal);
            await CalcularCostos(FechaInicio, FechaCorte);
        }
        public byte[] ImageToByteArray()
        {
            if (PBImagen.Image == null) return null;

            // Creamos una copia de la imagen para evitar bloqueos de GDI+
            using (Bitmap tempImage = new Bitmap(PBImagen.Image))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // Forzamos el guardado en un formato específico (ej. Png o Jpeg)
                    // Esto es mucho más seguro que usar RawFormat
                    tempImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                    return ms.ToArray();
                }
            }
        }
    }
}
