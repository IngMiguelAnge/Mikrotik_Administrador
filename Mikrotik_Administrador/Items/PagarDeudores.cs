using ImageMagick;
using Mikrotik_Administrador.Catalogos;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Mikrotik_Administrador.Items
{
    public partial class PagarDeudores : Form
    {
        public int IdExtra { get; set; }
        public int IdHistorial { get; set; }
        public int IdResponsable { get; set; }
        public string Cliente { get; set; }
        public int IdDeudor { get; set; }
        public int IdDeuda { get; set; }
        List<ListDeudasModel> ListaPagos = new List<ListDeudasModel>();
        private decimal TotalFaltante;
        private decimal PagoOriginal;
        private decimal TotalExtra;
        public PagarDeudores()
        {
            InitializeComponent();
        }

        private void PagarDeudores_Load(object sender, EventArgs e)
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
            decimal TotalDeuda = 0m;
            ListaPagos = new List<ListDeudasModel>();
            BuscarExtras();
            if (IdHistorial == 0)
            {
                CBBanco.SelectedIndex = 0;
                if (IdExtra == 0)
                {
                    //Saca el total por todas las deudas
                    List<DeudaModel> ListaDeudas = new List<DeudaModel>();
                    ListaDeudas = obj.GetFaltantesPagarbyIdDeudor(IdDeudor).Result.ToList();
                    if (IdDeuda != 0)
                    {
                        ListaDeudas = ListaDeudas.Where(x => x.Id == IdDeuda).ToList();
                    }
                    if (ListaDeudas.Count() > 0)
                    {

                        foreach (var item in ListaDeudas)
                        {
                            var Pagos = obj.GetHistorialPagosDeudas(item.Id, string.Empty, 0, 0);
                            decimal TotalHistorial = Pagos.Result.Where(x => x.Estatus == "Activo").Sum(x => x.Cantidad);
                            TotalDeuda += item.Cantidad;
                            TotalPagado += TotalHistorial;
                            TotalFaltante +=
                            item.Cantidad - TotalHistorial < 0 ? 0 :
                            item.Cantidad - TotalHistorial;
                            ListaPagos.Add(new ListDeudasModel
                            {
                                Id = item.Id,
                                Cantidad = item.Cantidad,
                                Faltante = item.Cantidad - TotalHistorial < 0 ? 0 :
                                item.Cantidad - TotalHistorial,
                                Year = item.Year,
                                Mes = item.Mes,
                                Recibido = TotalHistorial,
                                Estatus = item.Estatus ? "Activo" : "Inactivo"
                            });
                        }
                        lblTotalDeudas.Text = $"Total deudas: {TotalDeuda:C}";
                        lblTotalPagado.Text = $"Total pagado: {TotalPagado:C}";
                        lblFaltante.Text = $"Total a pagar: {TotalFaltante:C}";// + TotalExtra:C}";
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
                    lblTotalDeudas.Visible = false;
                    lblTotalPagado.Visible = false;
                    lblFaltante.Visible = false;
                    if (TotalExtra <= 0)
                    {
                        MessageBox.Show("No hay costos extras pendientes de pago.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        return;
                    }
                }
            }
            else
            {
                if (IdExtra == 0)
                {
                    var Pago = obj.GetHistorialPagosDeudasById(IdHistorial).Result;
                    PagoOriginal = Pago.Cantidad;
                    var Pagos = obj.GetHistorialPagosDeudas(Pago.IdDeuda, string.Empty, 0, 0);
                    TotalPagado = Pagos.Result.Where(x => x.Estatus == "Activo").Sum(x => x.Cantidad);
                    lblTotalPagado.Text = $"Total pagado: {TotalPagado:C}";
                    var Deuda = obj.GetDeudabyId(Pago.IdDeuda).Result;
                    lblTotalDeudas.Text = $"Total deuda: {Deuda.Cantidad:C}";
                    TotalFaltante = Deuda.Cantidad - TotalPagado < 0 ? 0 :
                        Deuda.Cantidad - TotalPagado;
                    lblFaltante.Text = $"Total a pagar: {TotalFaltante:C}"; //+ TotalExtra:C}";
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
                    ListaPagos.Add(new ListDeudasModel
                    {
                        Id = Pago.IdDeuda,
                        Cantidad = Deuda.Cantidad,
                        Faltante = TotalFaltante,
                        Year = Deuda.Year,
                        Mes = Deuda.Mes,
                        Recibido = TotalPagado,
                        Estatus = Deuda.Estatus ? "Activo" : "Inactivo"
                    });
                }
                else
                {
                    lblTotalDeudas.Visible = false;
                    lblTotalPagado.Visible = false;
                    lblFaltante.Visible = false;
                    var Pago = obj.GetHistorialPagosExtrasById(IdHistorial).Result;
                    PagoOriginal = Pago.Cantidad;
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
                }
            }
        }
        public void BuscarExtras()
        {
            AppRepository obj = new AppRepository();
            var ListExtras = obj.GetCostoExtra(0, IdDeudor, IdExtra).Result;
            decimal Ventas = ListExtras != null ?
                ListExtras.Where(x => x.Tipo == "Venta").Sum(x => x.TotalPendiente) : 0;
            decimal Servicios = ListExtras != null ?
                ListExtras.Where(x => x.Tipo == "Servicio").Sum(x => x.TotalPendiente) : 0;
            if (IdExtra != 0 && ListExtras.First().Tipo == "Prestamo")
            {
                Ventas = ListExtras != null ?
                ListExtras.Where(x => x.Tipo == "Prestamo").Sum(x => x.TotalPendiente) : 0;
                lblTotalVenta.Text = $"Total prestamo a pagar: {Ventas:C}";
                lblTotalServicio.Visible = false;
            }
            else
            {
                lblTotalVenta.Text = $"Total venta a pagar: {Ventas:C}";
                lblTotalServicio.Text = $"Total servicio a pagar: {Servicios:C}";
            }
            TotalExtra = Ventas + Servicios;
            lblFaltante.Text = $"Total a pagar: {TotalFaltante + TotalExtra:C}";

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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtReferencia.Text.Trim() == string.Empty
               || CBBanco.SelectedIndex == 0)
            {
                MessageBox.Show("Datos incompletos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            decimal Recibido = 0;
            bool CubrirExtra = false;
            if (IdHistorial == 0)
            {
                ConfirmarPago confirmacion = new ConfirmarPago();
                confirmacion.IsExtra = IdExtra == 0 ? false : true;
                confirmacion.Total = TotalFaltante;
                if (TotalExtra > 0)
                {
                    if (IdExtra == 0)
                    {
                        DialogResult resultado = MessageBox.Show("Se ha detectado que tiene un cobro extra pendiente, ¿Quiere que primero, se cubra ese cobro?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (resultado == DialogResult.Yes)
                        {
                            confirmacion.Total = TotalFaltante + TotalExtra;
                            CubrirExtra = true;
                        }
                    }
                    else
                    {
                        confirmacion.Total = TotalFaltante + TotalExtra;
                        CubrirExtra = true;
                    }
                }
                confirmacion.IdMensualidadRecibido = IdDeuda;
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
            List<int> ListIdHistorialExtra = new List<int>();
            string ExtraNota = string.Empty;
            ExtraNota += Environment.NewLine + "Nota: Se recibio un total de $" + Recibido +
                " y se repartio de la siguiente forma:" + Environment.NewLine;
            if (IdHistorial == 0)
            {
                if (CubrirExtra)
                {
                    var ListExtras = obj.GetCostoExtra(0,IdDeudor, IdExtra).Result;
                    if (IdExtra == 0)
                        ListExtras = ListExtras.Where(x => x.Tipo != "Prestamo").ToList();
                    foreach (var Extra in ListExtras)
                    {
                        if (Recibido == 0)
                            break;
                        HistorialPagosExtrasModel sv = new HistorialPagosExtrasModel
                        {
                            Id = 0,
                            FechaRecibido = dtpFechaPago.Value,
                            Cantidad = Recibido - Extra.TotalPendiente < 0 ? Recibido
                            : Extra.TotalPendiente,
                            IdExtra = Extra.Id,
                            Comentario = txtComentario.Text.Trim(),
                            IdBanco = (int)CBBanco.SelectedValue,
                            Referencia = txtReferencia.Text.Trim(),
                            Imagen = ImageToByteArray(),
                            IdUsuario = IdResponsable
                        };
                        var result = obj.SaveHistorialPagosExtras(sv,true).Result;
                        ListIdHistorialExtra.Add(result);
                        ExtraNota += "Cobro extra " + Extra.Nombre + " con faltante a pagar: " +
                             " $" + sv.Cantidad.ToString() + Environment.NewLine;
                        Recibido = Recibido - sv.Cantidad < 0 ? 0 : Recibido - sv.Cantidad;
                    }
                }
                if (IdExtra == 0)
                {
                    foreach (var item in ListaPagos)
                    {
                        if (Recibido == 0)
                            break;
                        HistorialPagosDeudaModel sv = new HistorialPagosDeudaModel
                        {
                            Id = 0,
                            FechaRecibido = dtpFechaPago.Value,
                            Cantidad = Recibido - item.Faltante < 0 ? Recibido
                            : item.Faltante,
                            IdDeuda = item.Id,
                            Comentario = txtComentario.Text.Trim(),
                            IdBanco = (int)CBBanco.SelectedValue,
                            Referencia = txtReferencia.Text.Trim(),
                            Imagen = ImageToByteArray(),
                            IdUsuario = IdResponsable
                        };
                        var result = obj.SaveHistorialPagosDeudas(sv).Result;
                        ListIdHistorial.Add(result);
                        ExtraNota += "Deuda con año: " + item.Year.ToString() + " y mes: " + item.Mes.ToString()
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
                        if (IdExtra == 0)
                        {
                            bool Revasototal = ListaPagos.First().Recibido + (Recibido - PagoOriginal) >
                              ListaPagos.First().Faltante ? true : false;
                            if (Revasototal)
                            {
                                MessageBox.Show("No se puede agregar una cantidad mayor a lo que falta por pagar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        else
                        {
                            bool Revasototal = TotalExtra - (Recibido - PagoOriginal) <
                             0 ? true : false;
                            if (Revasototal)
                            {
                                MessageBox.Show("No se puede agregar una cantidad mayor a lo que falta por pagar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    if (IdExtra == 0)
                    {
                        HistorialPagosDeudaModel sv = new HistorialPagosDeudaModel
                        {
                            Id = IdHistorial,
                            FechaRecibido = dtpFechaPago.Value,
                            Cantidad = NUDCantidad.Value,
                            IdDeuda = ListaPagos.FirstOrDefault().Id,
                            Comentario = txtComentario.Text.Trim(),
                            IdBanco = (int)CBBanco.SelectedValue,
                            Referencia = txtReferencia.Text.Trim(),
                            Imagen = ImageToByteArray(),
                            IdUsuario = IdResponsable
                        };
                        var result = obj.SaveHistorialPagosDeudas(sv).Result;
                    }
                    else
                    {
                        HistorialPagosExtrasModel sv = new HistorialPagosExtrasModel
                        {
                            Id = IdHistorial,
                            FechaRecibido = dtpFechaPago.Value,
                            Cantidad = NUDCantidad.Value,
                            IdExtra = IdExtra,
                            Comentario = txtComentario.Text.Trim(),
                            IdBanco = (int)CBBanco.SelectedValue,
                            Referencia = txtReferencia.Text.Trim(),
                            Imagen = ImageToByteArray(),
                            IdUsuario = IdResponsable
                        };
                        var result = obj.SaveHistorialPagosExtras(sv,true).Result;
                    }
                    ExtraNota = string.Empty;
                }
                else
                {
                    if (IdExtra == 0)
                    {
                        HistorialPagosDeudaModel sv = new HistorialPagosDeudaModel
                        {
                            Id = IdHistorial,
                            FechaRecibido = dtpFechaPago.Value,
                            Cantidad = NUDCantidad.Value,
                            IdDeuda = IdDeuda,
                            Comentario = txtComentario.Text.Trim(),
                            IdBanco = (int)CBBanco.SelectedValue,
                            Referencia = txtReferencia.Text.Trim(),
                            Imagen = ImageToByteArray(),
                            IdUsuario = IdResponsable
                        };
                        int res = obj.SaveHistorialPagosDeudas(sv).Result;
                    }
                    else
                    {
                        HistorialPagosExtrasModel sv = new HistorialPagosExtrasModel
                        {
                            Id = IdHistorial,
                            FechaRecibido = dtpFechaPago.Value,
                            Cantidad = NUDCantidad.Value,
                            IdExtra = IdExtra,
                            Comentario = txtComentario.Text.Trim(),
                            IdBanco = (int)CBBanco.SelectedValue,
                            Referencia = txtReferencia.Text.Trim(),
                            Imagen = ImageToByteArray(),
                            IdUsuario = IdResponsable
                        };
                        int res = obj.SaveHistorialPagosExtras(sv,true).Result;
                    }
                    ExtraNota = string.Empty;
                }
            }

            ExtraNota = txtComentario.Text.ToString().Trim() != string.Empty ?
                ExtraNota.TrimEnd() : ExtraNota.TrimEnd().TrimStart();
            bool r = false;
            foreach (var resultextra in ListIdHistorialExtra)
            {
                r = obj.UpdateDeudasNotaExtra(resultextra, ExtraNota, true).Result;
            }
            foreach (var result in ListIdHistorial)
            {
                r = obj.UpdateDeudasNotaExtra(result, ExtraNota, false).Result;
            }
            MessageBox.Show("Pago guardado correctamente");
            DialogResult = DialogResult.OK;
            this.Close();
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

        private void btnExtras_Click(object sender, EventArgs e)
        {
            Extras ext = new Extras();
            ext.IdDeudor = IdDeudor;
            ext.IdUsuarioM = 0;
            ext.IdResponsable = IdResponsable;
            ext.Ocultar = true;
            ext.ShowDialog();
            BuscarExtras();
        }
    }
}
