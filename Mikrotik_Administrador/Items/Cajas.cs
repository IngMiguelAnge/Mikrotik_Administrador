using ClosedXML.Excel;
using Mikrotik_Administrador.Catalogos;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using Mikrotik_Administrador.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador.Items
{
    public partial class Cajas : Form
    {
        private List<ListInstalacionesModel> listInstalaciones = new List<ListInstalacionesModel>();
        public Cajas()
        {
            InitializeComponent();
        }

        private void btnDescargar_Click(object sender, EventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            btnCargar.Enabled = false;
            btnDescargar.Enabled = false;
            try
            {
                // 1.Configurar cuadro de diálogo para guardar el archivo
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Archivo de Excel (*.xlsx)|*.xlsx";
                    saveFileDialog.FileName = "Cajas.xlsx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // 2. Crear el libro de trabajo (Workbook)
                        using (var workbook = new XLWorkbook())
                        {
                            // ==============================================================
                            // HOJA 1: CAMBIOS Y SUSPENSIONES (Aparecerá primero)
                            // ==============================================================
                            var wsCambios = workbook.Worksheets.Add("CAJAS");

                            // Encabezados
                            wsCambios.Cell(1, 1).Value = "IDCLIENTE";                  // A
                            wsCambios.Cell(1, 2).Value = "CLIENTE";                    // B
                            wsCambios.Cell(1, 3).Value = "IDSERVICIO";               // C
                            wsCambios.Cell(1, 4).Value = "SERVICIO";               // D
                            wsCambios.Cell(1, 5).Value = "IDINSTALADOR";                // E
                            wsCambios.Cell(1, 6).Value = "INSTALADOR";   // F
                            wsCambios.Cell(1, 7).Value = "PUERTO";        // G
                            wsCambios.Cell(1, 8).Value = "CAJA";    // H
                            wsCambios.Cell(1, 9).Value = "SPLITER";                // I
                            wsCambios.Cell(1, 10).Value = "PUERTO INTERNO";   // J
                            wsCambios.Cell(1, 11).Value = "FECHA INSTALACION";        // K
                            var headerCambios = wsCambios.Range("A1:K1");
                            headerCambios.Style.Font.Bold = true;
                            headerCambios.Style.Fill.BackgroundColor = XLColor.CornflowerBlue;
                            headerCambios.Style.Font.FontColor = XLColor.White;

                            int filaCambios = 2;
                            wsCambios.Cell(filaCambios, 1).Value = 1;
                            wsCambios.Cell(filaCambios, 2).Value = "JUAN PEREZ";
                            wsCambios.Cell(filaCambios, 3).Value = 1;
                            wsCambios.Cell(filaCambios, 4).Value = "JUAN PEREZ";
                            wsCambios.Cell(filaCambios, 5).Value = 1;
                            wsCambios.Cell(filaCambios, 6).Value = "ADMINISTRADOR";
                            wsCambios.Cell(filaCambios, 7).Value = 1;
                            wsCambios.Cell(filaCambios, 8).Value = "CAJA DE PRUEBA";
                            wsCambios.Cell(filaCambios, 9).Value = "SPLITER DE PRUEBA";
                            wsCambios.Cell(filaCambios, 10).Value = 1;
                            wsCambios.Cell(filaCambios, 11).Value = DateTime.Now;
                            wsCambios.Cell(filaCambios, 11).Style.DateFormat.Format = "dd/MM/yyyy h:mm AM/PM";
                            filaCambios++;


                            // Autoajuste de columnas para Hoja 1
                            wsCambios.Columns().AdjustToContents();

                            // Guardar el archivo
                            workbook.SaveAs(saveFileDialog.FileName);
                        }

                        MessageBox.Show("Archivo excel generado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 100;
                btnDescargar.Enabled = true;
                btnCargar.Enabled = true;
            }
        }

        private async void btnCargar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Archivos de Excel (*.xlsx)|*.xlsx";
                openFileDialog.Title = "Seleccionar archivo Excel";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Llamar al método para leer e mostrar en el DataGridView
                    await CargarDatosExcelCajasAsync(openFileDialog.FileName);
                    btnConfirmar.Visible = true;
                }
            }
        }
        private async Task CargarDatosExcelCajasAsync(string rutaArchivo)
        {
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 30;
            btnCargar.Enabled = false;
            btnConfirmar.Enabled = false;
            listInstalaciones = new List<ListInstalacionesModel>();
            try
            {
                AppRepository obj = new AppRepository();

                using (var workbook = new ClosedXML.Excel.XLWorkbook(rutaArchivo))
                {
                    // Tomamos la primera hoja del libro de Excel
                    var ws = workbook.Worksheet(1);
                    bool primeraFila = true;
                    int Id = 1;
                    foreach (var row in ws.RowsUsed())
                    {
                        if (primeraFila)
                        {
                            primeraFila = false;
                            continue;
                        }
                        ListInstalacionesModel RInstalacion = new ListInstalacionesModel();
                        RInstalacion.Id = Id;
                        // 1. Extraer los datos principales de la fila
                        RInstalacion.IdCliente = row.Cell(1).GetValue<int>();
                        var cliente = obj.GetClienteById(RInstalacion.IdCliente).Result;
                        RInstalacion.Cliente = cliente == null ? string.Empty : cliente.Nombre.ToString();
                        RInstalacion.IdUsuarioM = row.Cell(3).GetValue<int>();
                        var servicio = obj.GetUsuariosMikrotiksById(RInstalacion.IdUsuarioM).Result;
                        RInstalacion.UsuarioM = servicio == null ? string.Empty : servicio.Nombre.ToString();
                        RInstalacion.IdUsuario = row.Cell(5).GetValue<int>();
                        var Instalador = obj.GetUsuariosbyId(RInstalacion.IdUsuario).Result;
                        RInstalacion.Instalador = Instalador == null ? string.Empty : 
                            Instalador.IdTipo == 3 ?
                            Instalador.Nombre.ToString(): string.Empty;
                        RInstalacion.Puerto = row.Cell(7).GetValue<int>();
                        RInstalacion.Caja = row.Cell(8).GetString().Trim();
                        RInstalacion.Spliter = row.Cell(9).GetString().Trim();
                        RInstalacion.PuertoInterno = row.Cell(10).GetValue<int>();
                        string valorCelda = row.Cell(11).GetString().Trim();
                        if (DateTime.TryParse(valorCelda, out DateTime rfecha))
                        {
                            RInstalacion.FechaInstalacion = rfecha;
                        }
                        else
                        {
                            // Opcional: Manejar el caso por si alguna celda viene vacía o con formato incorrecto
                            // Por ejemplo, limpiar los puntos de A.M. / P.M. si es necesario:
                            string fechaLimpiada = valorCelda.Replace(".", "").Replace("  ", " ");

                            if (DateTime.TryParse(fechaLimpiada, out rfecha))
                            {
                                RInstalacion.FechaInstalacion = rfecha;
                            }
                            else
                            {
                                throw new FormatException($"Error de formato hay una fecha no válida. Fila " + Id.ToString());
                            }
                        }
                        listInstalaciones.Add(RInstalacion);
                        Id += 1;
                    }
                }
                CargarTablaInstalaciones();
                MessageBox.Show("Excel de deudores cargado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al leer el archivo Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 100;
                btnConfirmar.Enabled = true;
                btnCargar.Enabled = true;
            }
        }
        public void CargarTablaInstalaciones()
        {
            CrearGridViewInstalaciones();
            var listaFinal = listInstalaciones?.ToList() ?? new List<ListInstalacionesModel>();
            DGVClientes.DataSource = new SortableBindingList<ListInstalacionesModel>(listaFinal);
            if (DGVClientes.Columns["Id"] != null)
                DGVClientes.Columns["Id"].Visible = false;
        }
        public void CrearGridViewInstalaciones()
        {
            DGVClientes.Columns.Clear();
            DGVClientes.AutoGenerateColumns = false;
            DGVClientes.EnableHeadersVisualStyles = false;
            // --- ESTILO DE LOS TÍTULOS (HEADERS) CON TU AZUL LOGO ---
            DGVClientes.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            DGVClientes.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            DGVClientes.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            DGVClientes.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            DGVClientes.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            DGVClientes.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);

            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdCliente",
                HeaderText = "IdCliente",
                DataPropertyName = "IdCliente",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cliente",
                HeaderText = "Cliente",
                DataPropertyName = "Cliente",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdUsuarioM",
                HeaderText = "IdUsuarioM",
                DataPropertyName = "IdUsuarioM",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UsuarioM",
                HeaderText = "Servicio",
                DataPropertyName = "UsuarioM",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdUsuario",
                HeaderText = "IdUsuario",
                DataPropertyName = "IdUsuario",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Instalador",
                HeaderText = "Instalador",
                DataPropertyName = "Instalador",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Puerto",
                HeaderText = "Puerto",
                DataPropertyName = "Puerto",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Caja",
                HeaderText = "Caja",
                DataPropertyName = "Caja",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Spliter",
                HeaderText = "Spliter",
                DataPropertyName = "Spliter",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PuertoInterno",
                HeaderText = "Puerto Interno",
                DataPropertyName = "PuertoInterno",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaInstalacion",
                HeaderText = "FechaInstalacion",
                DataPropertyName = "FechaInstalacion",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.AllowUserToAddRows = false;
        }

        private async void btnConfirmar_Click(object sender, EventArgs e)
        {
            if(listInstalaciones.Count <= 0)
            { return; }
            AppRepository obj = new AppRepository();
            foreach(var item in listInstalaciones)
            {
                if(item.Caja == string.Empty || item.Spliter == string.Empty
                    || item.UsuarioM == string.Empty || item.Cliente == string.Empty
                    || item.Puerto == 0 || item.PuertoInterno == 0 || item.Instalador == string.Empty)
                {
                    MessageBox.Show("Revisar la fila " + item.Id + " los datos estan incompletos o no existen", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
               InstalacionModel model = new InstalacionModel();
                model.Id = 0;
                model.Spliter = item.Spliter;
                model.IdUsuario = item.IdUsuario;
                model.IdUsuarioM = item.IdUsuarioM;
                model.Puerto_Interno = item.PuertoInterno;
                model.Puerto = item.Puerto;
                model.Caja = item.Caja;
                model.Fecha_Instalacion = item.FechaInstalacion;                
               await obj.SaveInstalacion(model);
               MessageBox.Show("Excel guardado exitosamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
