using ClosedXML.Excel;
using Mikrotik_Administrador.Class;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using Mikrotik_Administrador.Settings;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Mikrotik_Administrador.Catalogos
{
    public partial class MensualidadesMultiples : Form
    {
        public MensualidadesMultiples()
        {
            InitializeComponent();
        }

        private async void MensualidadesMultiples_Load(object sender, EventArgs e)
        {
            AppRepository obj = new AppRepository();
            var listaMikrotiks = await obj.GetMikrotiks();

            // Insertamos un objeto "fantasma" al inicio para el placeholder
            listaMikrotiks.Insert(0, new ListMikrotikModel { Id = 0, Nombre = "Selecciona un Mikrotik" });

            // Configuramos el ComboBox
            CBMikrotiks.DisplayMember = "Nombre"; // Lo que el usuario VE
            CBMikrotiks.ValueMember = "Id";      // El dato que procesas por DETRÁS
            CBMikrotiks.DataSource = listaMikrotiks;
            CBMikrotiks.SelectedIndex = 0;
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            if (CBMikrotiks.SelectedValue.ToString() == "0" && CBTodosMikrotiks.Checked == false)
            {
                MessageBox.Show("Por favor, selecciona un Mikrotik.");
                return;
            }
            if (txtCliente.Text.Trim() == "")
            {
                DialogResult resultado = MessageBox.Show("Ha dejado el campo vacio, esto buscara a todos los clientes pero puede demorar ¿Quiere continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.No)
                {
                    return;
                }
            }
            CargarClientes();
        }
        public async void CargarClientes()
        {
            CrearGridView();
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            BtnBuscar.Enabled = false;
            int IdMikrotik = CBTodosMikrotiks.Checked == true ? 0 : (int)CBMikrotiks.SelectedValue;
            try
            {
                AppRepository obj = new AppRepository();
                var lista = await Task.Run(() => obj.GetUsuariosAll(txtCliente.Text, IdMikrotik, txtUsuario.Text));
                var listaFinal = lista?.ToList() ?? new List<ListClientesDescargaModel>();
                DGVClientes.DataSource = new SortableBindingList<ListClientesDescargaModel>(listaFinal);
                if (DGVClientes.Columns["IdCliente"] != null)
                    DGVClientes.Columns["IdCliente"].Visible = false;
                if (DGVClientes.Columns["IdUsuarioM"] != null)
                    DGVClientes.Columns["IdUsuarioM"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 0;
                BtnBuscar.Enabled = true;
            }
        }
        public void CrearGridView()
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
                Name = "Usuario",
                HeaderText = "Usuario",
                DataPropertyName = "Usuario",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                HeaderText = "Estatus",
                DataPropertyName = "Estatus",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DataGridViewCheckBoxColumn chkSeleccionar = new DataGridViewCheckBoxColumn
            {
                Name = "chkSeleccionar",
                HeaderText = "Descargar",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                TrueValue = true,
                FalseValue = false,
                IndeterminateValue = false
            };
            DGVClientes.Columns.Add(chkSeleccionar);
            DGVClientes.AllowUserToAddRows = false;
        }

        private void btnDescargar_Click(object sender, EventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            btnCargar.Enabled = false;
            BtnBuscar.Enabled = false;
            btnDescargar.Enabled = false;
            try
            {
                List<ListClientesDescargaModel> Seleccionados = new List<ListClientesDescargaModel>();
                Seleccionados = DGVClientes.Rows.Cast<DataGridViewRow>()
                 .Where(r => Convert.ToBoolean(r.Cells["chkSeleccionar"].Value))
                  .Select(r => new ListClientesDescargaModel
                  {
                      IdCliente = Convert.ToInt32(r.Cells["IdCliente"].Value),
                      Cliente = Convert.ToString(r.Cells["Cliente"].Value),
                      IdUsuarioM = Convert.ToInt32(r.Cells["IdUsuarioM"].Value),
                      Usuario = Convert.ToString(r.Cells["Usuario"].Value),
                      Estatus = Convert.ToString(r.Cells["Estatus"].Value)
                  })
                   .ToList();

                if (Seleccionados.Count == 0)
                {
                    MessageBox.Show("No has seleccionado ningún usuario para descargar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                AppRepository obj = new AppRepository();
                // 1.Configurar cuadro de diálogo para guardar el archivo
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Archivo de Excel (*.xlsx)|*.xlsx";
                    saveFileDialog.FileName = "Descarga de Mensualidades.xlsx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // 2. Crear el libro de trabajo (Workbook)
                        using (var workbook = new XLWorkbook())
                        {
                            // ==============================================================
                            // HOJA 1: CAMBIOS Y SUSPENSIONES (Aparecerá primero)
                            // ==============================================================
                            var wsCambios = workbook.Worksheets.Add("Cambios y suspensiones");

                            // Encabezados
                            wsCambios.Cell(1, 1).Value = "IdUsuarioM";                 // A
                            wsCambios.Cell(1, 2).Value = "Usuario del mikrotik";       // B
                            wsCambios.Cell(1, 3).Value = "Servicio extra";              // C
                            wsCambios.Cell(1, 4).Value = "Cuando inicio";               // D
                            wsCambios.Cell(1, 5).Value = "Días que duro";               // E
                            wsCambios.Cell(1, 6).Value = "Id Plan que recibiria";       // F
                            wsCambios.Cell(1, 7).Value = "Nombre Plan que recibiria";   // G
                            wsCambios.Cell(1, 8).Value = "Id mikrotik receptor";        // H
                            wsCambios.Cell(1, 9).Value = "Nombre mikrotik receptor";    // I

                            // Formato a los encabezados (A1 a I1)
                            var headerCambios = wsCambios.Range("A1:I1");
                            headerCambios.Style.Font.Bold = true;
                            headerCambios.Style.Fill.BackgroundColor = XLColor.CornflowerBlue;
                            headerCambios.Style.Font.FontColor = XLColor.White;

                            int filaCambios = 2;
                            foreach (ListClientesDescargaModel item in Seleccionados)
                            {
                                wsCambios.Cell(filaCambios, 1).Value = item.IdUsuarioM;
                                wsCambios.Cell(filaCambios, 2).Value = item.Usuario;
                                wsCambios.Cell(filaCambios, 3).Value = "Cambio de plan";
                                wsCambios.Cell(filaCambios, 4).Value = DateTime.Now;
                                wsCambios.Cell(filaCambios, 4).Style.DateFormat.Format = "dd/MM/yyyy HH:mm:ss";
                                wsCambios.Cell(filaCambios, 5).Value = 1;
                                wsCambios.Cell(filaCambios, 6).Value = 1;
                                wsCambios.Cell(filaCambios, 7).Value = "Plan Basico";
                                wsCambios.Cell(filaCambios, 8).Value = 1;
                                wsCambios.Cell(filaCambios, 9).Value = "Santa Maria";
                                filaCambios++;
                            }

                            // Autoajuste de columnas para Hoja 1
                            wsCambios.Columns().AdjustToContents();

                            // ==============================================================
                            // HOJA 2: MENSUALIDADES (Aparecerá segundo)
                            // ==============================================================
                            var wsMensualidades = workbook.Worksheets.Add("Mensualidades");

                            // Encabezados
                            wsMensualidades.Cell(1, 1).Value = "N°Mensualidad"; //A
                            wsMensualidades.Cell(1, 2).Value = "IdCliente";                            // B
                            wsMensualidades.Cell(1, 3).Value = "Cliente";                              // C
                            wsMensualidades.Cell(1, 4).Value = "IdUsuarioM";                           // D
                            wsMensualidades.Cell(1, 5).Value = "UsuarioM";                             // E
                            wsMensualidades.Cell(1, 6).Value = "Fecha en que comenzo la mensualidad";  // F
                            wsMensualidades.Cell(1, 7).Value = "Día de corte";                         // G
                            wsMensualidades.Cell(1, 8).Value = "IdUsuarioResponsable";                            // H
                            wsMensualidades.Cell(1, 9).Value = "Responsable de dar de alta la mensualidad"; // I

                            // Formato a los encabezados (A1 a H1)
                            var headerMensualidades = wsMensualidades.Range("A1:I1");
                            headerMensualidades.Style.Font.Bold = true;
                            headerMensualidades.Style.Fill.BackgroundColor = XLColor.CornflowerBlue;
                            headerMensualidades.Style.Font.FontColor = XLColor.White;

                            int filaMensualidades = 2;
                            foreach (ListClientesDescargaModel item in Seleccionados)
                            {
                                wsMensualidades.Cell(filaMensualidades, 1).Value = 1;
                                wsMensualidades.Cell(filaMensualidades, 2).Value = item.IdCliente;
                                wsMensualidades.Cell(filaMensualidades, 3).Value = item.Cliente;
                                wsMensualidades.Cell(filaMensualidades, 4).Value = item.IdUsuarioM;
                                wsMensualidades.Cell(filaMensualidades, 5).Value = item.Usuario;
                                wsMensualidades.Cell(filaMensualidades, 6).Value = DateTime.Now;
                                wsMensualidades.Cell(filaMensualidades, 6).Style.DateFormat.Format = "dd/MM/yyyy";
                                wsMensualidades.Cell(filaMensualidades, 7).Value = 1;
                                wsMensualidades.Cell(filaMensualidades, 8).Value = 1;
                                wsMensualidades.Cell(filaMensualidades, 9).Value = "Administrador";
                                filaMensualidades++;
                            }

                            // Autoajuste de columnas para Hoja 2
                            wsMensualidades.Columns().AdjustToContents();


                            // ==============================================================
                            // HOJA 2: MENSUALIDADES (Aparecerá segundo)
                            // ==============================================================
                            var wsPagos = workbook.Worksheets.Add("Pagos");

                            // Encabezados
                            wsPagos.Cell(1, 1).Value = "N°Mensualidad"; //A
                            wsPagos.Cell(1, 2).Value = "N°Pago";        //B
                            wsPagos.Cell(1, 3).Value = "Fecha en que se recibio";  // C
                            wsPagos.Cell(1, 4).Value = "Cantidad";     // D
                            wsPagos.Cell(1, 5).Value = "Comentario";   // E
                            wsPagos.Cell(1, 6).Value = "IdBanco";  // F
                            wsPagos.Cell(1, 7).Value = "Nombre de banco";  // G
                            wsPagos.Cell(1, 8).Value = "Referencia";      // H
                            wsPagos.Cell(1, 9).Value = "Ruta de la imagen"; // I
                            wsPagos.Cell(1, 10).Value = "IdUsuarioResponsable"; // J
                            wsPagos.Cell(1, 11).Value = "Responsable de recibir el pago"; // K
                            // Formato a los encabezados (A1 a K1)
                            var headerPagos = wsPagos.Range("A1:K1");
                            headerPagos.Style.Font.Bold = true;
                            headerPagos.Style.Fill.BackgroundColor = XLColor.CornflowerBlue;
                            headerPagos.Style.Font.FontColor = XLColor.White;

                            wsPagos.Cell(2, 1).Value = 1;
                            wsPagos.Cell(2, 2).Value = 1;
                            wsPagos.Cell(2, 3).Value = DateTime.Now;
                            wsPagos.Cell(2, 3).Style.DateFormat.Format = "dd/MM/yyyy HH:mm:ss";
                            wsPagos.Cell(2, 4).Value = 0;
                            wsPagos.Cell(2, 5).Value = "";
                            wsPagos.Cell(2, 6).Value = 1;
                            wsPagos.Cell(2, 7).Value = "PAGOS EFECTIVO";
                            wsPagos.Cell(2, 8).Value = "1234ASD";
                            wsPagos.Cell(2, 9).Value = "C:\\Users\\Lenovo\\OneDrive\\Desktop\\Imagenes\\1.jpg";
                            wsPagos.Cell(2, 10).Value = 1;
                            wsPagos.Cell(2, 11).Value = "Administrador";

                            // Autoajuste de columnas para Hoja 2
                            wsPagos.Columns().AdjustToContents();
                            // 5. Guardar el archivo
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
                BtnBuscar.Enabled = true;
                btnDescargar.Enabled = true;
                btnCargar.Enabled = true;
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Archivos de Excel (*.xlsx)|*.xlsx";
                openFileDialog.Title = "Seleccionar archivo Excel";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Llamar al método para leer e mostrar en el DataGridView
                    CargarDatosExcel(openFileDialog.FileName);
                }
            }

        }
        private void CargarDatosExcel(string rutaArchivo)
        {
            try
            {
                DataTable dt = new DataTable();

                using (var workbook = new XLWorkbook(rutaArchivo))
                {
                    // Tomar la primera hoja de trabajo
                    var worksheet = workbook.Worksheet(1);

                    // Define si la primera fila tiene nombres de columnas
                    bool primeraFilaEsEncabezado = true;

                    foreach (var row in worksheet.RowsUsed())
                    {
                        if (primeraFilaEsEncabezado)
                        {
                            // Crear las columnas en el DataTable con el texto del encabezado
                            foreach (var cell in row.CellsUsed())
                            {
                                dt.Columns.Add(cell.Value.ToString());
                            }
                            primeraFilaEsEncabezado = false;
                        }
                        else
                        {
                            // Agregar las filas de datos
                            dt.Rows.Add();
                            int i = 0;
                            foreach (var cell in row.Cells(1, dt.Columns.Count))
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                                i++;
                            }
                        }
                    }
                }
                string mira = dt.Rows[0]["Producto"].ToString();
                MessageBox.Show("Archivo Excel cargado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
