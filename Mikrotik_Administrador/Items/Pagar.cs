using ImageMagick;
using Mikrotik_Administrador.Class;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
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
using System.Xml.Linq;

namespace Mikrotik_Administrador.Items
{
    public partial class Pagar : Form
    {
        public int IdUsuarioM { get; set; }
        private decimal TotalReal = 0;
        public string Cliente { get; set; }
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
            CBBanco.SelectedIndex = 0;    
            Buscar();
        }
        public async void Buscar()
        {
            CrearGridView();
            AppRepository obj = new AppRepository();
            try
            {
                var Servicios = await obj.GetDetallesMensualidad(IdUsuarioM);
                var listaFinal = Servicios?.ToList() ?? new List<ListDetallesMensualidadModel>();
                dgvDetalles.DataSource = new BindingList<ListDetallesMensualidadModel>(listaFinal);
                TotalReal = listaFinal.Where(c=> c.Estatus == "Saldo Pendiente").Select(x => Convert.ToDecimal(x.Cantidad)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void CrearGridView()
        {
            dgvDetalles.Columns.Clear();
            dgvDetalles.AutoGenerateColumns = false;
            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaOrden",
                HeaderText = "FechaOrden",
                DataPropertyName = "FechaOrden",
                Visible = false,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "OrdenVisual",
                HeaderText = "OrdenVisual",
                DataPropertyName = "OrdenVisual",
                Visible = false,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descripcion",
                HeaderText = "Descripción",
                DataPropertyName = "Descripcion",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cantidad",
                HeaderText = "Cantidad",
                DataPropertyName = "Cantidad",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                HeaderText = "Estatus",
                DataPropertyName = "Estatus",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            //DataGridViewButtonColumn btnVer = new DataGridViewButtonColumn
            //{
            //    Name = "Motivo",
            //    HeaderText = "Acción",
            //    Text = "Ver",
            //    UseColumnTextForButtonValue = true,
            //    Width = 90,
            //    FlatStyle = FlatStyle.Flat
            //};
            //dgvDetalles.Columns.Add(btnVer);
            dgvDetalles.AllowUserToAddRows = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(txtReferencia.Text.Trim() == string.Empty 
                || CBBanco.SelectedIndex == 0 || PBImagen.Image == null)
            {
                MessageBox.Show("Datos incompletos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ConfirmarPago confirmacion = new ConfirmarPago();
            confirmacion.Total = TotalReal;
            if (confirmacion.ShowDialog() != DialogResult.OK)
            { return; }
            VentaModel venta = new VentaModel
            {
                Copias = 0,
                Imprimir = false,
                Recibido = confirmacion.Recibido,
                IdTicket = 0,
                Cliente = Cliente,
                Total = TotalReal,
                Title = string.Empty
            };
            SaveHistorialPagosModel sv = new SaveHistorialPagosModel
            {
                IdUsuarioM = IdUsuarioM,
                FechaRecibido = dtpFechaPago.Value,
                Cantidad = confirmacion.Recibido - TotalReal < 0 ? confirmacion.Recibido : TotalReal,
                Comentario = txtComentario.Text.Trim(),
                IdBanco = (int)CBBanco.SelectedValue,
                Referencia = txtReferencia.Text.Trim(),
                Imagen = ImageToByteArray()
            };
            AppRepository obj = new AppRepository();
            var result = obj.SaveHistorialPagos(sv).Result;
            if (result)
            {
                MessageBox.Show("Pago registrado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Impresiones im = new Impresiones();
                im.GenerarTicket(venta);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al registrar el pago", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
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
