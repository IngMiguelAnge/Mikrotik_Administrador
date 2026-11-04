using ClosedXML.Excel;
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
    public partial class DeudoresMultiples : Form
    {
        private List<ListDeudoresModel> ListDeudores = new List<ListDeudoresModel>();
        private List<DeudaModel> ListDeuda = new List<DeudaModel>();
        private int Opcion = 0;
        public DeudoresMultiples()
        {
            InitializeComponent();
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
                    await CargarDatosExcelDeudoresAsync(openFileDialog.FileName);
                    btnConfirmar.Visible = true;
                }
            }
        }

        private async void btnConfirmar_Click(object sender, EventArgs e)
        {
            await Confirmado();
        }
        private async Task Confirmado()
        {
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 30;
            btnCargar.Enabled = false;
            btnConfirmar.Enabled = false;
            try
            {
                foreach (var deudor in ListDeudores)
                {
                    AppRepository obj = new AppRepository();
                    DeudoresModel Deudores = new DeudoresModel()
                    {
                        Id = deudor.Id,
                        Nombre = deudor.Nombre,
                        Latitud = deudor.Latitud,
                        Longitud = deudor.Longitud,
                        Recogido = deudor.Recogido.ToUpper() == "SI" ? true : false,
                        Nota = deudor.Nota,
                        Estatus = deudor.Estatus == "Activo" ? true : false
                    };
                    int resultado = await obj.SaveDeudoresbyName(Deudores);
                    if (resultado <= 0)
                    {
                        MessageBox.Show($"Ocurrió un error al guardar el deudor: {deudor.Nombre}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Salir del método si hay un error
                    }
                    var deudasDelDeudor = ListDeuda.Where(d => d.IdDeudor == deudor.Id).ToList();
                    foreach (var item in deudasDelDeudor)
                    {
                        await obj.SaveDeudabyFecha(item);
                    }
                }
                MessageBox.Show("Datos guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 100;
                btnConfirmar.Enabled = true;
                btnCargar.Enabled = true;
            }
        }
        private async Task CargarDatosExcelDeudoresAsync(string rutaArchivo)
        {
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 30;
            btnCargar.Enabled = false;
            btnConfirmar.Enabled = false;
            ListDeudores = new List<ListDeudoresModel>();
            ListDeuda = new List<DeudaModel>();
            Opcion = 0;
            try
            {
                AppRepository obj = new AppRepository();

                using (var workbook = new ClosedXML.Excel.XLWorkbook(rutaArchivo))
                {
                    // Tomamos la primera hoja del libro de Excel
                    var ws = workbook.Worksheet(1);
                    bool primeraFila = true;

                    // Lista de los nombres de los meses en el orden exacto de tus columnas
                    string[] mesesAnio = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
                                   "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

                    int IdDeudor = 1;
                    foreach (var row in ws.RowsUsed())
                    {
                        if (primeraFila)
                        {
                            primeraFila = false;
                            continue;
                        }

                        // 1. Extraer los datos principales de la fila
                        string nombreCliente = row.Cell(1).GetString().Trim(); // Columna A: Cliente
                        string coordenadas = row.Cell(2).GetString().Trim();   // Columna B: Coordenadas
                        string recogido = row.Cell(3).GetString().Trim();
                        string nota = row.Cell(4).GetString().Trim();
                        // Validar si la celda del año tiene valor
                        int anio = 0;
                        if (!row.Cell(5).IsEmpty() && int.TryParse(row.Cell(5).GetString(), out int parsedAnio))
                        {
                            anio = parsedAnio; // Columna C: Año
                        }

                        if (string.IsNullOrEmpty(nombreCliente)) continue;
                        ListDeudoresModel Deudor = new ListDeudoresModel();
                        Deudor.Id = IdDeudor;
                        Deudor.Nombre = nombreCliente;
                        Deudor.Latitud = coordenadas.Trim() != string.Empty?
                            coordenadas.Split(',')[0].Trim(): string.Empty;
                        Deudor.Longitud = coordenadas.Trim() != string.Empty ?
                            coordenadas.Split(',')[1].Trim() : string.Empty;
                        Deudor.Recogido = recogido.ToUpper() != "SI" ? "NO" : "SI";
                        Deudor.Nota = nota;
                        Deudor.Estatus = "Activo";
                        ListDeudores.Add(Deudor);

                        // 2. Iterar sobre las columnas de los meses (Enero a Diciembre empiezan a partir de la columna D / índice 4)
                        int IdDeuda = 1;
                        for (int i = 0; i < mesesAnio.Length; i++)
                        {
                            int numeroColumna = 6 + i;
                            var celdaMes = row.Cell(numeroColumna);

                            // Verificar si la celda tiene información (no está vacía)
                            if (!celdaMes.IsEmpty() && decimal.TryParse(celdaMes.GetString().Trim(), out decimal cantidad))
                            {
                                if (cantidad > 0)
                                {
                                    DeudaModel Deuda = new DeudaModel();
                                    Deuda.Id = IdDeuda;
                                    Deuda.Year = anio;
                                    Deuda.Mes = mesesAnio[i];
                                    Deuda.Cantidad = cantidad;
                                    Deuda.IdDeudor = IdDeudor;
                                    Deuda.Estatus = true;
                                    ListDeuda.Add(Deuda);
                                    IdDeuda += 1;
                                }
                            }
                        }
                        IdDeudor += 1;
                    }
                }
                CargarTablaDeudores();
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
        public void CargarTablaDeudores()
        {
            CrearGridViewDeudores();
            var listaFinal = ListDeudores?.ToList() ?? new List<ListDeudoresModel>();
            DGVClientes.DataSource = new SortableBindingList<ListDeudoresModel>(listaFinal);
            if (DGVClientes.Columns["Id"] != null)
                DGVClientes.Columns["Id"].Visible = false;
        }
        public void CrearGridViewDeudores()
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
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Latitud",
                HeaderText = "Latitud",
                DataPropertyName = "Latitud",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Longitud",
                HeaderText = "Longitud",
                DataPropertyName = "Longitud",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Recogido",
                HeaderText = "Equipos recogidos",
                DataPropertyName = "Recogido",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            DataGridViewButtonColumn btnVerMapa = new DataGridViewButtonColumn
            {
                Name = "btnVerMapa",
                HeaderText = "Acción",
                Text = "Mapa",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            DGVClientes.Columns.Add(btnVerMapa);
            DataGridViewButtonColumn btnVerDeuda = new DataGridViewButtonColumn
            {
                Name = "btnVerDeuda",
                HeaderText = "Acción",
                Text = "Deuda",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            DGVClientes.Columns.Add(btnVerDeuda);

            DGVClientes.AllowUserToAddRows = false;
        }

        private void DGVClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                DGVClientes.Enabled = false;
                switch (DGVClientes.Columns[e.ColumnIndex].Name)
                {
                    case "btnVerDeuda":
                        int IdDeudor = (int)DGVClientes.Rows[e.RowIndex].Cells["Id"].Value;

                        btnAtras.Visible = true;
                        Opcion = 1;
                        CrearTablaDeudas(IdDeudor);
                        break;
                    case "btnVerMapa":
                        if (string.IsNullOrEmpty(DGVClientes.Rows[e.RowIndex].Cells["Latitud"].Value?.ToString()))
                        {
                            MessageBox.Show("No hay coordenadas disponibles para este cliente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        Mapa mp = new Mapa();
                        mp.Latitud = DGVClientes.Rows[e.RowIndex].Cells["Latitud"].Value.ToString();
                        mp.Longitud = DGVClientes.Rows[e.RowIndex].Cells["Longitud"].Value.ToString();
                        mp.ShowDialog();
                        DGVClientes.Rows[e.RowIndex].Cells["Latitud"].Value = mp.Latitud;
                        DGVClientes.Rows[e.RowIndex].Cells["Longitud"].Value = mp.Longitud;
                        ListDeudores.Where(x => x.Id == (int)DGVClientes.Rows[e.RowIndex].Cells["Id"].Value).FirstOrDefault().Latitud = mp.Latitud;
                        ListDeudores.Where(x => x.Id == (int)DGVClientes.Rows[e.RowIndex].Cells["Id"].Value).FirstOrDefault().Longitud = mp.Longitud;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}");
            }
            finally
            {
                DGVClientes.Enabled = true;
            }
        }
        public void CrearTablaDeudas(int IdDeudor)
        {
            CrearGridViewDeudas();
            var listaFinal = ListDeuda.Where(x => x.IdDeudor == IdDeudor)?.ToList() ?? new List<DeudaModel>();
            DGVClientes.DataSource = new SortableBindingList<DeudaModel>(listaFinal);
        }
        public void CrearGridViewDeudas()
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
                Name = "Cantidad",
                HeaderText = "Cantidad",
                DataPropertyName = "Cantidad",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Year",
                HeaderText = "Año",
                DataPropertyName = "Year",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Mes",
                HeaderText = "Mes",
                DataPropertyName = "Mes",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            DGVClientes.AllowUserToAddRows = false;
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            switch (Opcion)
            {
                case 1:
                    CargarTablaDeudores();
                    Opcion = 0;
                    btnAtras.Visible = false;
                    break;
            }
        }

        private void btnDescargar_Click(object sender, EventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            btnCargar.Enabled = false;
            btnAtras.Enabled = false;
            btnDescargar.Enabled = false;
            try
            {
                // 1.Configurar cuadro de diálogo para guardar el archivo
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Archivo de Excel (*.xlsx)|*.xlsx";
                    saveFileDialog.FileName = "Deudores.xlsx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // 2. Crear el libro de trabajo (Workbook)
                        using (var workbook = new XLWorkbook())
                        {
                            // ==============================================================
                            // HOJA 1: CAMBIOS Y SUSPENSIONES (Aparecerá primero)
                            // ==============================================================
                            var wsCambios = workbook.Worksheets.Add("DEUDORES");

                            // Encabezados
                            wsCambios.Cell(1, 1).Value = "CLIENTE";                  // A
                            wsCambios.Cell(1, 2).Value = "COORDENDAS";                    // B
                            wsCambios.Cell(1, 3).Value = "¿EQUIPOS RECOGIDOS?";               // C
                            wsCambios.Cell(1, 4).Value = "NOTA";               // D
                            wsCambios.Cell(1, 5).Value = "AÑO";                // E
                            wsCambios.Cell(1, 6).Value = "ENERO";   // F
                            wsCambios.Cell(1, 7).Value = "FEBRERO";        // G
                            wsCambios.Cell(1, 8).Value = "MARZO";    // H
                            wsCambios.Cell(1, 9).Value = "ABRIL";                // I
                            wsCambios.Cell(1, 10).Value = "JUNIO";   // J
                            wsCambios.Cell(1, 11).Value = "JULIO";        // K
                            wsCambios.Cell(1, 12).Value = "AGOSTO";    // L
                            wsCambios.Cell(1, 13).Value = "SEPTIEMBRE"; //M
                            wsCambios.Cell(1, 14).Value = "OCTUBRE"; //N
                            wsCambios.Cell(1, 15).Value = "NOVIEMBRE"; //O
                            wsCambios.Cell(1, 16).Value = "DICIEMBRE"; //P
                            var headerCambios = wsCambios.Range("A1:P1");
                            headerCambios.Style.Font.Bold = true;
                            headerCambios.Style.Fill.BackgroundColor = XLColor.CornflowerBlue;
                            headerCambios.Style.Font.FontColor = XLColor.White;

                            int filaCambios = 2;
                            wsCambios.Cell(filaCambios, 1).Value = "GERMAN FLORENCIO GUADALUPE";
                            wsCambios.Cell(filaCambios, 2).Value = "18.600841,-97.662201";
                            wsCambios.Cell(filaCambios, 3).Value = "SI";
                            wsCambios.Cell(filaCambios, 4).Value = "";
                            wsCambios.Cell(filaCambios, 5).Value = 2025;
                            wsCambios.Cell(filaCambios, 6).Value = 300;
                            wsCambios.Cell(filaCambios, 7).Value = 0;
                            wsCambios.Cell(filaCambios, 8).Value = 0;
                            wsCambios.Cell(filaCambios, 9).Value = 0;
                            wsCambios.Cell(filaCambios, 10).Value = 0;
                            wsCambios.Cell(filaCambios, 11).Value = 0;
                            wsCambios.Cell(filaCambios, 12).Value = 0;
                            wsCambios.Cell(filaCambios, 13).Value = 0;
                            wsCambios.Cell(filaCambios, 14).Value = 0;
                            wsCambios.Cell(filaCambios, 15).Value = 0;
                            wsCambios.Cell(filaCambios, 16).Value = 0;
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
    }
}
