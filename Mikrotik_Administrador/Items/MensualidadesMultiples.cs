using ClosedXML.Excel;
using Microsoft.Identity.Client.Extensibility;
using Mikrotik_Administrador.Class;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Items;
using Mikrotik_Administrador.Model;
using Mikrotik_Administrador.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Mikrotik_Administrador.Catalogos
{
    public partial class MensualidadesMultiples : Form
    {
        private List<TiempoDefinidosModel> ListCambios = new List<TiempoDefinidosModel>();
        private List<MensualidadModel> ListMensualidades = new List<MensualidadModel>();
        private List<HistorialPagosModel> ListHistorialPagos = new List<HistorialPagosModel>();
        private List<UsuariosandPlanesModel> ListClientes = new List<UsuariosandPlanesModel>();
        private List<ListMensualidadesModel> ListM = new List<ListMensualidadesModel>();
        private List<ListHistorialPagosModel> ListPagos = new List<ListHistorialPagosModel>();
        private List<ListDetallesMensualidadModel> ListDestalles = new List<ListDetallesMensualidadModel>();
        private int Opcion = 0;
        private int IdUsuarioMRevision = 0;

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
            btnConfirmar.Visible = false;
            btnAtras.Visible = false;
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
                HeaderText = "Servicio",
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
            if (btnAtras.Visible == true || btnConfirmar.Visible == true)
            {
                MessageBox.Show("Busque y seleccione usuarios para esta opción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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
                            var wsCambios = workbook.Worksheets.Add("Cambios");

                            // Encabezados
                            wsCambios.Cell(1, 1).Value = "IdServicio";                  // A
                            wsCambios.Cell(1, 2).Value = "Servicio";                    // B
                            wsCambios.Cell(1, 3).Value = "Operación realizada";         // C
                            wsCambios.Cell(1, 4).Value = "Cuando inicio";               // D
                            wsCambios.Cell(1, 5).Value = "Días que duro";               // E
                            wsCambios.Cell(1, 6).Value = "IdPlan original";                // F
                            wsCambios.Cell(1, 7).Value = "Nombre del plan original";   // G
                            wsCambios.Cell(1, 8).Value = "IdMikrotik orginal";        // H
                            wsCambios.Cell(1, 9).Value = "Nombre mikrotik original";    // I
                            wsCambios.Cell(1, 10).Value = "IdPlan nuevo";                // J
                            wsCambios.Cell(1, 11).Value = "Nombre del plan nuevo";   // K
                            wsCambios.Cell(1, 12).Value = "IdMikrotik receptor";        // L
                            wsCambios.Cell(1, 13).Value = "Nombre mikrotik receptor";    // M

                            // Formato a los encabezados (A1 a M1)
                            var headerCambios = wsCambios.Range("A1:M1");
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
                                wsCambios.Cell(filaCambios, 4).Style.DateFormat.Format = "dd/MM/yyyy h:mm AM/PM";
                                wsCambios.Cell(filaCambios, 5).Value = 1;
                                wsCambios.Cell(filaCambios, 6).Value = 1;
                                wsCambios.Cell(filaCambios, 7).Value = "Plan Basico";
                                wsCambios.Cell(filaCambios, 8).Value = 1;
                                wsCambios.Cell(filaCambios, 9).Value = "Santa Maria";
                                wsCambios.Cell(filaCambios, 10).Value = 1;
                                wsCambios.Cell(filaCambios, 11).Value = "Plan Basico";
                                wsCambios.Cell(filaCambios, 12).Value = 1;
                                wsCambios.Cell(filaCambios, 13).Value = "Santa Maria";
                                filaCambios++;
                            }

                            // Autoajuste de columnas para Hoja 1
                            wsCambios.Columns().AdjustToContents();

                            // ==============================================================
                            // HOJA 2: MENSUALIDADES (Aparecerá segundo)
                            // ==============================================================
                            var wsPagos = workbook.Worksheets.Add("Pagos");

                            // Encabezados
                            wsPagos.Cell(1, 1).Value = "IdCliente";                 // A
                            wsPagos.Cell(1, 2).Value = "Cliente";                   // B
                            wsPagos.Cell(1, 3).Value = "IdServicio";                // C
                            wsPagos.Cell(1, 4).Value = "Servicio";                  // D
                            wsPagos.Cell(1, 5).Value = "Inicio la mensualidad";     // E
                            wsPagos.Cell(1, 6).Value = "Día de corte";              // F
                            wsPagos.Cell(1, 7).Value = "IdResponsable";             // G
                            wsPagos.Cell(1, 8).Value = "Responsable";               // H
                            wsPagos.Cell(1, 9).Value = "Cuando se recibio el pago"; // I
                            wsPagos.Cell(1, 10).Value = "Cantidad recibida";        // J
                            wsPagos.Cell(1, 11).Value = "Comentario";               // K
                            wsPagos.Cell(1, 12).Value = "IdBanco";                  // L
                            wsPagos.Cell(1, 13).Value = "Banco";                    // M
                            wsPagos.Cell(1, 14).Value = "Referencia";               // N
                            wsPagos.Cell(1, 15).Value = "Ruta de imagen";           // O
                            // Formato a los encabezados (A1 a O1)
                            var headerPagos = wsPagos.Range("A1:O1");
                            headerPagos.Style.Font.Bold = true;
                            headerPagos.Style.Fill.BackgroundColor = XLColor.CornflowerBlue;
                            headerPagos.Style.Font.FontColor = XLColor.White;

                            int filaPagos = 2;
                            foreach (ListClientesDescargaModel item in Seleccionados)
                            {
                                wsPagos.Cell(filaPagos, 1).Value = item.IdCliente;
                                wsPagos.Cell(filaPagos, 2).Value = item.Cliente;
                                wsPagos.Cell(filaPagos, 3).Value = item.IdUsuarioM;
                                wsPagos.Cell(filaPagos, 4).Value = item.Usuario;
                                wsPagos.Cell(filaPagos, 5).Value = DateTime.Now.Date;
                                wsPagos.Cell(filaPagos, 5).Style.DateFormat.Format = "dd/MM/yyyy";
                                wsPagos.Cell(filaPagos, 6).Value = 1;
                                wsPagos.Cell(filaPagos, 7).Value = 1;
                                wsPagos.Cell(filaPagos, 8).Value = "Administrador";
                                wsPagos.Cell(filaPagos, 9).Value = DateTime.Now;
                                wsPagos.Cell(filaPagos, 9).Style.DateFormat.Format = "dd/MM/yyyy h:mm AM/PM";
                                wsPagos.Cell(filaPagos, 10).Value = 0;
                                wsPagos.Cell(filaPagos, 11).Value = "";
                                wsPagos.Cell(filaPagos, 12).Value = 1;
                                wsPagos.Cell(filaPagos, 13).Value = "PAGOS EFECTIVO";
                                wsPagos.Cell(filaPagos, 14).Value = "1234ASD";
                                wsPagos.Cell(filaPagos, 15).Value = "C:\\Users\\Lenovo\\OneDrive\\Desktop\\Imagenes\\1.jpg";
                                filaPagos++;
                            }

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
            AppRepository obj = new AppRepository();
            var ListBancos = obj.GetBancos(string.Empty, string.Empty);
            if (ListBancos.Result.Count <= 0)
            {
                MessageBox.Show("Para cargar un excel se requiere tener bancos registrados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var ListUsuarios = obj.GetUsuarios(string.Empty, string.Empty);
            if (ListBancos.Result.Count <= 0)
            {
                MessageBox.Show("Para cargar un excel se requiere tener usuarios registrados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Archivos de Excel (*.xlsx)|*.xlsx";
                openFileDialog.Title = "Seleccionar archivo Excel";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Llamar al método para leer e mostrar en el DataGridView
                    CargarDatosExcel(openFileDialog.FileName);
                    btnConfirmar.Visible = true;
                }
            }

        }
        private void CargarDatosExcel(string rutaArchivo)
        {
            try
            {
                ListCambios = new List<TiempoDefinidosModel>();
                ListMensualidades = new List<MensualidadModel>();
                ListHistorialPagos = new List<HistorialPagosModel>();
                ListClientes = new List<UsuariosandPlanesModel>();
                AppRepository obj = new AppRepository();
                int contadorIdMensualidad = 1;
                int contadorIdCambio = 1;

                // Mantiene la última fecha de inicio procesada por cada servicio
                Dictionary<int, DateTime> ultimasFechasInicio = new Dictionary<int, DateTime>();

                using (var workbook = new XLWorkbook(rutaArchivo))
                {
                    // =========================================================================
                    // PÁGINA 1: CAMBIOS Y SUSPENSIONES
                    // =========================================================================
                    var wsCambios = workbook.Worksheet("Cambios");
                    bool primeraFila1 = true;

                    foreach (var row in wsCambios.RowsUsed())
                    {
                        if (primeraFila1) { primeraFila1 = false; continue; }

                        int idServicio = row.Cell(1).GetValue<int>(); // Col A: IdServicio
                        string operacion = row.Cell(3).GetValue<string>(); // Col C: Operación
                        DateTime fechaInicio = row.Cell(4).GetValue<DateTime>(); // Col D: Inicio
                        int diasDuro = row.Cell(5).GetValue<int>(); // Col E: Días que duró
                        DateTime fechaFin = fechaInicio.AddDays(diasDuro);

                        bool valido = !ListCambios.Any(x => x.IdUsuarioM == idServicio &&
                                                            fechaInicio >= x.FechaInicio &&
                                                            fechaInicio <= x.FechaFin);

                        if (valido)
                        {
                            ListCambios.Add(new TiempoDefinidosModel
                            {
                                Id = contadorIdCambio++,
                                Dias = diasDuro,
                                Horas = 0,
                                FechaInicio = fechaInicio,
                                FechaFin = fechaFin,
                                Modo = "Temporal",
                                IdUsuarioM = idServicio,
                                Estatus = fechaFin <= DateTime.Now ? "Completado" : "Ejecutando",
                                IdPlanOriginal = row.Cell(6).GetValue<int>(),
                                IdMikrotikOriginal = row.Cell(8).GetValue<int>(),
                                IdPlan = row.Cell(10).GetValue<int>(),              // Col J: Plan nuevo
                                Plan = row.Cell(11).GetValue<string>(),
                                IdMikrotikReceptor = row.Cell(12).GetValue<int>(),  // Col L: Mikrotik
                                Programacion = operacion,
                                Password = "1234"
                            });
                        }
                    }

                    // =========================================================================
                    // PÁGINA 2: PAGOS Y MENSUALIDADES
                    // =========================================================================
                    var wsPagos = workbook.Worksheet("Pagos");
                    bool primeraFila2 = true;

                    foreach (var row in wsPagos.RowsUsed())
                    {
                        if (primeraFila2) { primeraFila2 = false; continue; }
                        int idCliente = row.Cell(1).GetValue<int>(); // Col C: IdCliente
                        int idServicio = row.Cell(3).GetValue<int>(); // Col C: IdServicio
                        DateTime fechaInicioExcel = row.Cell(5).GetValue<DateTime>(); // Col E: Inicio la mensualidad
                        int diaCorte = row.Cell(6).GetValue<int>(); // Col F
                        int idResponsable = row.Cell(7).GetValue<int>(); // Col G
                        DateTime fechaPago = row.Cell(9).GetValue<DateTime>(); // Col I
                        decimal saldoRestante = row.Cell(10).GetValue<decimal>(); // Col J: Cantidad recibida

                        // Manejo seguro de celdas nulas o vacías
                        string comentario = row.Cell(11).IsEmpty() ? "" : row.Cell(11).GetValue<string>();
                        int idBanco = row.Cell(12).IsEmpty() ? 0 : row.Cell(12).GetValue<int>();
                        string Banco = row.Cell(13).IsEmpty() ? "" : row.Cell(13).GetValue<string>();
                        string referencia = row.Cell(14).IsEmpty() ? "" : row.Cell(14).GetValue<string>();
                        string rutaImagen = row.Cell(15).IsEmpty() ? "" : row.Cell(15).GetValue<string>();

                        // ---------------------------------------------------------------------
                        // REGLA: Si el servicio ya fue procesado con la MISMA fecha de inicio,
                        // no reiniciamos desde cero, continuamos la distribución donde se quedó.
                        // ---------------------------------------------------------------------
                        DateTime fechaInicioActual;

                        if (ultimasFechasInicio.ContainsKey(idServicio) && ultimasFechasInicio[idServicio] == fechaInicioExcel)
                        {
                            // Mismo mes/inicio repetido: tomar la fecha donde quedó la distribución previa
                            var ultimaMensualidad = ListMensualidades.Where(m => m.IdUsuarioM == idServicio).OrderByDescending(m => m.FechaLimite).FirstOrDefault();
                            fechaInicioActual = ultimaMensualidad != null ? ultimaMensualidad.FechaLimite : fechaInicioExcel;
                        }
                        else
                        {
                            // Mes de inicio diferente o primer registro del servicio
                            fechaInicioActual = fechaInicioExcel;
                            ultimasFechasInicio[idServicio] = fechaInicioExcel;
                        }

                        var planBase = obj.GetPlanByIdUsuarioM(idServicio).Result;
                        decimal precioPlanBase = planBase != null ? planBase.Precio : 0;
                        if (ListClientes.Where(x => x.IdCliente == idCliente && x.IdUser == idServicio).ToList().Count() == 0)
                        {
                            //lista de clientes
                            ListClientes.Add(new UsuariosandPlanesModel
                            {
                                Identificador = "Cli" + idCliente + "Us" + idServicio,
                                IdCliente = idCliente,
                                Cliente = row.Cell(2).GetValue<string>(), // Col B: Cliente
                                IdUser = idServicio,
                                Usuario = row.Cell(4).GetValue<string>(), // Col D: Servicio
                                IdPlan = planBase != null ? planBase.Id : 0,
                                Plan = planBase != null ? planBase.Nombre : "Plan Desconocido",
                                Estatus = row.Cell(5).GetValue<string>(), // Col E: Inicio la mensualidad
                                Mikrotik = planBase != null ? planBase.Nombre : "Mikrotik Desconocido",
                                Mensualidad = "Disponible"
                            }
                            );
                        }
                        if (precioPlanBase == 0)
                        {
                            MessageBox.Show($"El servicio con ID {idServicio} no tiene un plan base con costo asignado. Por favor, revisa la configuración.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue; // Saltar a la siguiente fila
                        }
                        // Distribución del saldo
                        while (saldoRestante > 0)
                        {
                            // 1. Calcular FechaLímite del período actual
                            DateTime fechaLimiteActual;
                            if (diaCorte <= fechaInicioActual.Day)
                            {
                                fechaLimiteActual = new DateTime(fechaInicioActual.Year, fechaInicioActual.Month, diaCorte).AddMonths(1);
                            }
                            else
                            {
                                fechaLimiteActual = new DateTime(fechaInicioActual.Year, fechaInicioActual.Month, diaCorte);
                            }

                            // 2. Calcular costo prorrateado considerando cambios/suspensiones
                            decimal costoMensualidad = CalcularCostoMensualidad(idServicio, fechaInicioActual, fechaLimiteActual, diaCorte, precioPlanBase, ListCambios);
                            if (costoMensualidad <= 0) costoMensualidad = precioPlanBase;

                            // 3. Determinar el monto de este abono para esta mensualidad
                            decimal pagoParaEstaMensualidad = Math.Min(saldoRestante, costoMensualidad);
                            bool estaTotalmentePagado = saldoRestante >= costoMensualidad;

                            int idMensualidad = contadorIdMensualidad++;

                            // 4. Registrar la mensualidad
                            ListMensualidades.Add(new MensualidadModel
                            {
                                Id = idMensualidad,
                                Pagado = estaTotalmentePagado,
                                IdUsuarioM = idServicio,
                                DiaCorte = diaCorte,
                                FechaInicio = fechaInicioActual,
                                FechaLimite = fechaLimiteActual,
                                IdUsuario = idResponsable,
                                Mensualidad = costoMensualidad
                            });


                            // 5. Registrar el pago en el historial
                            ListHistorialPagos.Add(new HistorialPagosModel
                            {
                                Id = ListHistorialPagos.Count + 1,
                                FechaRecibido = fechaPago,
                                Cantidad = pagoParaEstaMensualidad,
                                Comentario = comentario,
                                IdBanco = idBanco,
                                Banco = Banco,
                                Referencia = referencia,
                                Imagen = (!string.IsNullOrEmpty(rutaImagen) && File.Exists(rutaImagen)) ? File.ReadAllBytes(rutaImagen) : null,
                                IdMensualidad = idMensualidad,
                                IdUsuario = idResponsable
                            });

                            // 6. Restar la cantidad distribuida
                            saldoRestante -= pagoParaEstaMensualidad;

                            // Si sobra saldo, avanza al siguiente mes consecutivo
                            if (saldoRestante > 0)
                            {
                                fechaInicioActual = fechaLimiteActual;
                            }
                        }
                    }
                }
                CargarTablaClientes();
                //MessageBox.Show("Archivo Excel procesado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void CargarTablaClientes()
        {
            CrearGridViewClientes();
            var listaFinal = ListClientes?.ToList() ?? new List<UsuariosandPlanesModel>();
            DGVClientes.DataSource = new SortableBindingList<UsuariosandPlanesModel>(listaFinal);
            if (DGVClientes.Columns["IdCliente"] != null)
                DGVClientes.Columns["IdCliente"].Visible = false;
            if (DGVClientes.Columns["IdUser"] != null)
                DGVClientes.Columns["IdUser"].Visible = false;
            if (DGVClientes.Columns["IdPlan"] != null)
                DGVClientes.Columns["IdPlan"].Visible = false;
        }
        private decimal CalcularCostoMensualidad(int idServicio, DateTime fechaInicio, DateTime fechaLimite, int diaCorte, decimal precioPlanBase, List<TiempoDefinidosModel> cambios)
        {
            // 1. Días totales del periodo comercial (Base 30)
            int diasTotalesPeriodo;
            if (fechaInicio.Day == diaCorte)
            {
                diasTotalesPeriodo = 30;
            }
            else if (fechaInicio.Day < diaCorte)
            {
                diasTotalesPeriodo = (diaCorte - fechaInicio.Day) + 1;
            }
            else
            {
                diasTotalesPeriodo = (30 - fechaInicio.Day) + diaCorte;
            }

            // 2. Filtrar cambios de plan dentro del periodo
            var cambiosPeriodo = cambios.Where(x => x.IdUsuarioM == idServicio
                                                 && x.FechaInicio <= fechaLimite
                                                 && x.FechaFin >= fechaInicio
                                                 && x.Programacion == "Cambio de plan"
                                                 && x.Modo == "Temporal").ToList();

            int diasConPlanNuevo = 0;
            decimal costoPlanesNuevos = 0;

            AppRepository obj = new AppRepository();

            foreach (var tc in cambiosPeriodo)
            {
                DateTime fInicioEfectiva = fechaInicio > tc.FechaInicio ? fechaInicio : tc.FechaInicio;
                DateTime fFinEfectiva = fechaLimite < tc.FechaFin ? fechaLimite : tc.FechaFin;

                int diasEfectivos = (int)(fFinEfectiva.Date - fInicioEfectiva.Date).TotalDays + 1;

                // Obtener el precio del plan nuevo asignado en el cambio
                var planNuevo = obj.GetPlanById(tc.IdPlan).Result;
                decimal precioPlanNuevo = planNuevo != null ? planNuevo.Precio : 0;

                diasConPlanNuevo += diasEfectivos;
                costoPlanesNuevos += diasEfectivos * (precioPlanNuevo / 30.0m);
            }

            // 3. Suspensiones (Días a costo 0)
            var suspensionesPeriodo = cambios.Where(x => x.IdUsuarioM == idServicio
                                                      && x.FechaInicio <= fechaLimite
                                                      && x.FechaFin >= fechaInicio
                                                      && x.Programacion == "Suspensión"
                                                      && x.Modo == "Temporal").ToList();

            int diasSuspendidos = 0;
            foreach (var sus in suspensionesPeriodo)
            {
                DateTime fInicioEfectiva = fechaInicio > sus.FechaInicio ? fechaInicio : sus.FechaInicio;
                DateTime fFinEfectiva = fechaLimite < sus.FechaFin ? fechaLimite : sus.FechaFin;

                diasSuspendidos += (int)(fFinEfectiva.Date - fInicioEfectiva.Date).TotalDays + 1;
            }

            // 4. Días restantes con tarifa normal
            int diasRestantes = diasTotalesPeriodo - diasConPlanNuevo - diasSuspendidos;
            if (diasRestantes < 0) diasRestantes = 0;

            decimal montoBruto = costoPlanesNuevos + (diasRestantes * (precioPlanBase / 30.0m));

            // 5. Redondeo Financiero
            decimal parteEntera = Math.Floor(montoBruto);
            decimal parteDecimal = montoBruto - parteEntera;

            if (parteDecimal > 0.00m && parteDecimal < 0.30m)
                return parteEntera;
            else if (parteDecimal >= 0.30m && parteDecimal <= 0.50m)
                return parteEntera + 0.50m;
            else if (parteDecimal > 0.50m)
                return parteEntera + 1.00m;

            return parteEntera;
        }
        public void CrearGridViewClientes()
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
                Name = "Identificador",
                HeaderText = "Identificador",
                DataPropertyName = "Identificador",
                ReadOnly = true,
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
                Name = "IdUser",
                HeaderText = "IdUser",
                DataPropertyName = "IdUser",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Usuario",
                HeaderText = "Servicio",
                DataPropertyName = "Usuario",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdPlan",
                HeaderText = "IdPlan",
                DataPropertyName = "IdPlan",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Plan Actual",
                HeaderText = "Plan",
                DataPropertyName = "Plan",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                HeaderText = "Estatus del servicio",
                DataPropertyName = "Estatus",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Mikrotik",
                HeaderText = "Mikrotik",
                DataPropertyName = "Mikrotik",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Mensualidad",
                HeaderText = "Mensualidad",
                DataPropertyName = "Mensualidad",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DataGridViewButtonColumn btnMensualidad = new DataGridViewButtonColumn
            {
                Name = "btnMensualidad",
                HeaderText = "Acción",
                Text = "Mensualidad",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            DGVClientes.Columns.Add(btnMensualidad);


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
                    case "btnMensualidad":
                        string Mensualidad = (string)DGVClientes.Rows[e.RowIndex].Cells["Mensualidad"].Value;
                        IdUsuarioMRevision = (int)DGVClientes.Rows[e.RowIndex].Cells["IdUser"].Value;
                        //ListMensualidades
                        int idUsuarioM = (int)DGVClientes.Rows[e.RowIndex].Cells["IdUser"].Value;
                        var Mensualidades = ListMensualidades.Where(x => x.IdUsuarioM == idUsuarioM).ToList();
                        ListM = new List<ListMensualidadesModel>();
                        decimal CostoMensualidad = 0;
                        decimal Recibido = 0;
                        foreach (var item in Mensualidades)
                        {
                            CostoMensualidad = item.Mensualidad == null ? 0 : (decimal)item.Mensualidad;
                            Recibido = ListHistorialPagos.Where(x => x.IdMensualidad == item.Id).Sum(x => x.Cantidad);
                            ListMensualidadesModel list = new ListMensualidadesModel()
                            {
                                Id = item.Id,
                                DiaCorte = item.DiaCorte,
                                FechaInicio = item.FechaInicio,
                                FechaLimite = item.FechaLimite,
                                Responsable = "Administrador",
                                Mensualidad = CostoMensualidad,
                                Recibido = Recibido,
                                Faltante = CostoMensualidad - Recibido
                            };
                            ListM.Add(list);
                        }
                        btnAtras.Visible = true;
                        Opcion = 1;
                        CrearTablaMensualidades();
                        break;
                    case "btnHistorial":
                        var Pagos = ListHistorialPagos.Where(x => x.IdMensualidad == (int)DGVClientes.Rows[e.RowIndex].Cells["Id"].Value).ToList();
                        ListPagos = new List<ListHistorialPagosModel>();
                        foreach (var item in Pagos)
                        {
                            ListHistorialPagosModel HP = new ListHistorialPagosModel
                            {
                                Id = item.Id,
                                FechaRecibido = item.FechaRecibido,
                                Cantidad = item.Cantidad,
                                Estatus = "Activo",
                                Banco = item.Banco,
                                Referencia = item.Referencia,
                                Responsable = "Administrador"
                            };
                            ListPagos.Add(HP);
                        }
                        Opcion = 2;
                        CrearTablaHistorial();

                        break;

                    case "btnVerDetalles":
                        DateTime Desde = (DateTime)DGVClientes.Rows[e.RowIndex].Cells["FechaInicio"].Value;
                        DateTime Hasta = (DateTime)DGVClientes.Rows[e.RowIndex].Cells["FechaLimite"].Value;

                        var Detalles = ListCambios.Where(x => x.IdUsuarioM == IdUsuarioMRevision &&
                        x.FechaInicio >= Desde && x.FechaFin <= Hasta).ToList();
                        if (Detalles.Count() == 0)
                        {
                            MessageBox.Show("Este plan transcurrio con normalidad, sin cambios encontrados.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        foreach (var item in Detalles)
                        {
                            ListDetallesMensualidadModel LD = new ListDetallesMensualidadModel
                            {
                                Id = item.Id,
                                FechaInicio = item.FechaInicio,
                                FechaFin = item.FechaFin,
                                Estatus = "Activo",
                                Programacion = item.Programacion,
                                Plan = item.Plan,
                            };
                            ListDestalles.Add(LD);
                        }
                        CrearTablaDetalles();
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
        public void CrearTablaDetalles()
        {
            CrearGridViewDetalles();
            var listaFinal = ListDestalles?.ToList() ?? new List<ListDetallesMensualidadModel>();
            DGVClientes.DataSource = new SortableBindingList<ListDetallesMensualidadModel>(listaFinal);
        }
        public void CrearTablaHistorial()
        {
            CrearGridViewHistorialPagos();
            var listaFinal = ListPagos?.ToList() ?? new List<ListHistorialPagosModel>();
            DGVClientes.DataSource = new SortableBindingList<ListHistorialPagosModel>(listaFinal);
        }
        public void CrearTablaMensualidades()
        {
            CrearGridViewMensualidades();
            var listaFinal = ListM?.ToList() ?? new List<ListMensualidadesModel>();
            DGVClientes.DataSource = new SortableBindingList<ListMensualidadesModel>(listaFinal);
        }
        public void CrearGridViewDetalles()
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
                Name = "FechaInicio",
                HeaderText = "Empezo",
                DataPropertyName = "FechaInicio",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaFin",
                HeaderText = "Termino",
                DataPropertyName = "FechaFin",
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
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Programacion",
                HeaderText = "Acción",
                DataPropertyName = "Programacion",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Plan",
                HeaderText = "Plan",
                DataPropertyName = "Plan",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic,
            });

            DGVClientes.AllowUserToAddRows = false;
        }
        public void CrearGridViewHistorialPagos()
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
                HeaderText = "NoTicket",
                DataPropertyName = "Id",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaRecibido",
                HeaderText = "Fecha en que se recibe",
                DataPropertyName = "FechaRecibido",
                ReadOnly = true,
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
                Name = "Estatus",
                HeaderText = "Estatus",
                DataPropertyName = "Estatus",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Banco",
                HeaderText = "Banco",
                DataPropertyName = "Banco",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Referencia",
                HeaderText = "Referencia",
                DataPropertyName = "Referencia",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Responsable",
                HeaderText = "Responsable",
                DataPropertyName = "Responsable",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.AllowUserToAddRows = false;
        }
        public void CrearGridViewMensualidades()
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
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DiaCorte",
                HeaderText = "Día Corte",
                DataPropertyName = "DiaCorte",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaInicio",
                HeaderText = "Inicia Mes",
                DataPropertyName = "FechaInicio",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaLimite",
                HeaderText = "Fecha Limite",
                DataPropertyName = "FechaLimite",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Responsable",
                HeaderText = "Responsable",
                DataPropertyName = "Responsable",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Mensualidad",
                HeaderText = "Mensualidad",
                DataPropertyName = "Mensualidad",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2", // Aplica formato de moneda local (ej: $120.00 o $120.50)
                    FormatProvider = new System.Globalization.CultureInfo("es-MX") // Forzado a pesos mexicanos
                }
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Recibido",
                HeaderText = "Recibido",
                DataPropertyName = "Recibido",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2", // Aplica formato de moneda local (ej: $120.00 o $120.50)
                    FormatProvider = new System.Globalization.CultureInfo("es-MX") // Forzado a pesos mexicanos
                }
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Faltante",
                HeaderText = "Faltante",
                DataPropertyName = "Faltante",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2", // Aplica formato de moneda local (ej: $120.00 o $120.50)
                    FormatProvider = new System.Globalization.CultureInfo("es-MX") // Forzado a pesos mexicanos
                }
            });
            DataGridViewButtonColumn btnHistorial = new DataGridViewButtonColumn
            {
                Name = "btnHistorial",
                HeaderText = "Acción",
                Text = "Historial de pagos",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            DGVClientes.Columns.Add(btnHistorial);
            DataGridViewButtonColumn btnVerDetalles = new DataGridViewButtonColumn
            {
                Name = "btnVerDetalles",
                HeaderText = "Acción",
                Text = "Detalles",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            DGVClientes.Columns.Add(btnVerDetalles);
            DGVClientes.AllowUserToAddRows = false;
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            switch (Opcion)
            {
                case 1:
                    CargarTablaClientes();
                    Opcion = 0;
                    btnAtras.Visible = false;
                    break;
                case 2:
                    CrearTablaMensualidades();
                    Opcion = 1;
                    break;
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Esta confirmando que la información mostrada es la correcta ¿Quiere continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
            if (resultado == DialogResult.No)
            {
                return;
            }
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            btnCargar.Enabled = false;
            BtnBuscar.Enabled = false;
            btnDescargar.Enabled = false;
            try
            {
                AppRepository obj = new AppRepository();
                int ContadorCambios = -1;
                
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Archivo de Excel (*.xlsx)|*.xlsx";
                    saveFileDialog.FileName = "Reporte de carga de excel.xlsx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // 2. Crear el libro de trabajo (Workbook)
                        using (var workbook = new XLWorkbook())
                        {
                            // ==============================================================
                            // HOJA 1: CAMBIOS Y SUSPENSIONES (Aparecerá primero)
                            // ==============================================================
                            var wsCambios = workbook.Worksheets.Add("Cambios");

                            // Encabezados
                            wsCambios.Cell(1, 1).Value = "Descupción";  // A
                            wsCambios.Cell(1, 2).Value = "Resultado";  // B
                            // Formato a los encabezados (A1 a B1)
                            var headerCambios = wsCambios.Range("A1:B1");
                            headerCambios.Style.Font.Bold = true;
                            headerCambios.Style.Fill.BackgroundColor = XLColor.CornflowerBlue;
                            headerCambios.Style.Font.FontColor = XLColor.White;

                            int filaCambios = 2;
                            foreach (var item in ListCambios)
                            {
                                ContadorCambios += 1;
                                var existServicio = obj.GetUsuariosMikrotiksById(item.IdUsuarioM).Result;
                                if (existServicio.Id == 0)
                                {
                                    wsCambios.Cell(filaCambios, 1).Value = 
                                        "El servicio con id " + item.IdUsuarioM.ToString() + " no existe en el sistema";
                                    wsCambios.Cell(filaCambios, 2).Value = "Error";
                                    filaCambios++;
                                    continue;
                                }
                                var existPlan = obj.GetPlanById(item.IdPlanOriginal).Result;
                                if (existServicio.Id == 0)
                                {
                                    wsCambios.Cell(filaCambios, 1).Value =
                                        "El plan origen con id " + item.IdPlanOriginal.ToString() + " no existe en el sistema";
                                    wsCambios.Cell(filaCambios, 2).Value = "Error";
                                    filaCambios++;
                                    continue;
                                }
                                if(item.IdPlanOriginal != item.IdPlan)
                                {
                                    existPlan = obj.GetPlanById(item.IdPlan).Result;
                                    if (existServicio.Id == 0)
                                    {
                                        wsCambios.Cell(filaCambios, 1).Value =
                                            "El plan nuevo con id " + item.IdPlan.ToString() + " no existe en el sistema";
                                        wsCambios.Cell(filaCambios, 2).Value = "Error";
                                        filaCambios++;
                                        continue;
                                    }
                                }
                                var existMikrotik = obj.GetMikrotikById(item.IdMikrotikOriginal).Result;
                                if (existServicio.Id == 0)
                                {
                                    wsCambios.Cell(filaCambios, 1).Value =
                                        "El mikrotik origen con id " + item.IdMikrotikOriginal.ToString() + " no existe en el sistema";
                                    wsCambios.Cell(filaCambios, 2).Value = "Error";
                                    filaCambios++;
                                    continue;
                                }
                                if(item.IdMikrotikOriginal != item.IdMikrotikReceptor)
                                {
                                    existMikrotik = obj.GetMikrotikById(item.IdMikrotikReceptor).Result;
                                    if (existServicio.Id == 0)
                                    {
                                        wsCambios.Cell(filaCambios, 1).Value =
                                            "El mikrotik receptor con id " + item.IdMikrotikReceptor.ToString() + " no existe en el sistema";
                                        wsCambios.Cell(filaCambios, 2).Value = "Error";
                                        filaCambios++;
                                        continue;
                                    }
                                }
                                var Anidado = obj.GetPlanesAnidadosbyParametros(item.IdMikrotikReceptor,item.IdPlan).Result;
                                int IdPlanAnidado = Anidado?.Id ?? 0;
                                if(IdPlanAnidado == 0)
                                {
                                    wsCambios.Cell(filaCambios, 1).Value =
                                          "No existe el plan nuevo: " + item.IdPlan.ToString() + " en el mikrotik receptor: " + item.IdMikrotikReceptor.ToString() ;
                                    wsCambios.Cell(filaCambios, 2).Value = "Error";
                                    filaCambios++;
                                    continue;
                                }
                                Anidado = obj.GetPlanesAnidadosbyParametros(item.IdMikrotikOriginal, item.IdPlanOriginal).Result;
                                IdPlanAnidado = Anidado?.Id ?? 0;
                                if (IdPlanAnidado == 0)
                                {
                                    wsCambios.Cell(filaCambios, 1).Value =
                                          "No existe el plan original: " + item.IdPlan.ToString() + " en el mikrotik original: " + item.IdMikrotikOriginal.ToString();
                                    wsCambios.Cell(filaCambios, 2).Value = "Error";
                                    filaCambios++;
                                    continue;
                                }
                                var exitCambiot = obj.GetTiempoCambiobyIdUsuarioM(item.IdUsuarioM, item.FechaInicio, item.FechaFin).Result;
                                if (exitCambiot.Count() == 0)
                                {
                                    var resultcambio = obj.SaveTiempoCambio(ListCambios[ContadorCambios]).Result;
                                    if(resultcambio)
                                    {
                                        wsCambios.Cell(filaCambios, 1).Value =
                                    "Ya existe el(la) " + item.Programacion + " registrado en el sistema para el servicio " + item.IdUsuarioM +
                                    " con fecha de inicio " + item.FechaInicio.ToString();
                                        wsCambios.Cell(filaCambios, 2).Value = "Satisfactorio";
                                    }
                                    else
                                    {
                                        wsCambios.Cell(filaCambios, 2).Value = "Error";
                                    }
                                    filaCambios++;
                                }
                                else
                                {
                                    wsCambios.Cell(filaCambios, 1).Value =
                                         "Ya existe el(la) " + item.Programacion + " registrado en el sistema para el servicio " + item.IdUsuarioM +
                                         " con fecha de inicio " + item.FechaInicio.ToString();
                                    wsCambios.Cell(filaCambios, 2).Value = "Error";
                                    filaCambios++;
                                    continue;
                                }
                            }
                            // Autoajuste de columnas para Hoja 1
                            wsCambios.Columns().AdjustToContents();

                            //// ==============================================================
                            //// HOJA 2: MENSUALIDADES (Aparecerá segundo)
                            //// ==============================================================
                            //var wsPagos = workbook.Worksheets.Add("Pagos");

                            //// Encabezados
                            //wsPagos.Cell(1, 1).Value = "IdCliente";                 // A
                            //wsPagos.Cell(1, 2).Value = "Cliente";                   // B
                            //wsPagos.Cell(1, 3).Value = "IdServicio";                // C
                            //wsPagos.Cell(1, 4).Value = "Servicio";                  // D
                            //wsPagos.Cell(1, 5).Value = "Inicio la mensualidad";     // E
                            //wsPagos.Cell(1, 6).Value = "Día de corte";              // F
                            //wsPagos.Cell(1, 7).Value = "IdResponsable";             // G
                            //wsPagos.Cell(1, 8).Value = "Responsable";               // H
                            //wsPagos.Cell(1, 9).Value = "Cuando se recibio el pago"; // I
                            //wsPagos.Cell(1, 10).Value = "Cantidad recibida";        // J
                            //wsPagos.Cell(1, 11).Value = "Comentario";               // K
                            //wsPagos.Cell(1, 12).Value = "IdBanco";                  // L
                            //wsPagos.Cell(1, 13).Value = "Banco";                    // M
                            //wsPagos.Cell(1, 14).Value = "Referencia";               // N
                            //wsPagos.Cell(1, 15).Value = "Ruta de imagen";           // O
                            //// Formato a los encabezados (A1 a O1)
                            //var headerPagos = wsPagos.Range("A1:O1");
                            //headerPagos.Style.Font.Bold = true;
                            //headerPagos.Style.Fill.BackgroundColor = XLColor.CornflowerBlue;
                            //headerPagos.Style.Font.FontColor = XLColor.White;

                            //int filaPagos = 2;
                            //foreach (ListClientesDescargaModel item in Seleccionados)
                            //{
                            //    wsPagos.Cell(filaPagos, 1).Value = item.IdCliente;
                            //    wsPagos.Cell(filaPagos, 2).Value = item.Cliente;
                            //    wsPagos.Cell(filaPagos, 3).Value = item.IdUsuarioM;
                            //    wsPagos.Cell(filaPagos, 4).Value = item.Usuario;
                            //    wsPagos.Cell(filaPagos, 5).Value = DateTime.Now.Date;
                            //    wsPagos.Cell(filaPagos, 5).Style.DateFormat.Format = "dd/MM/yyyy";
                            //    wsPagos.Cell(filaPagos, 6).Value = 1;
                            //    wsPagos.Cell(filaPagos, 7).Value = 1;
                            //    wsPagos.Cell(filaPagos, 8).Value = "Administrador";
                            //    wsPagos.Cell(filaPagos, 9).Value = DateTime.Now;
                            //    wsPagos.Cell(filaPagos, 9).Style.DateFormat.Format = "dd/MM/yyyy h:mm AM/PM";
                            //    wsPagos.Cell(filaPagos, 10).Value = 0;
                            //    wsPagos.Cell(filaPagos, 11).Value = "";
                            //    wsPagos.Cell(filaPagos, 12).Value = 1;
                            //    wsPagos.Cell(filaPagos, 13).Value = "PAGOS EFECTIVO";
                            //    wsPagos.Cell(filaPagos, 14).Value = "1234ASD";
                            //    wsPagos.Cell(filaPagos, 15).Value = "C:\\Users\\Lenovo\\OneDrive\\Desktop\\Imagenes\\1.jpg";
                            //    filaPagos++;
                            //}

                            //// Autoajuste de columnas para Hoja 2
                            //wsPagos.Columns().AdjustToContents();
                            //// 5. Guardar el archivo
                            workbook.SaveAs(saveFileDialog.FileName);
                        }

                        MessageBox.Show("Reporte generado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
