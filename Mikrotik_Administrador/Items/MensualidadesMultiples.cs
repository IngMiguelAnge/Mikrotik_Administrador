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
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;

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
                            wsCambios.Cell(1, 8).Value = "IdMikrotik original";        // H
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

        private async void btnCargar_Click(object sender, EventArgs e)
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
                    await CargarDatosExcelAsync(openFileDialog.FileName);
                    btnConfirmar.Visible = true;
                }
            }

        }
        private async Task CargarDatosExcelAsync(string rutaArchivo)
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
                            int idPlanNuevo = row.Cell(10).GetValue<int>(); // Col J: Plan nuevo
                            var planB = await obj.GetPlanById(idPlanNuevo);

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
                                IdPlan = idPlanNuevo,
                                Plan = planB != null ? planB.Nombre : "Plan Desconocido",
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

                        int idCliente = row.Cell(1).GetValue<int>();             // Col A: IdCliente
                        int idServicio = row.Cell(3).GetValue<int>();            // Col C: IdServicio
                        DateTime fechaInicioExcel = row.Cell(5).GetValue<DateTime>(); // Col E: Inicio mensualidad
                        int diaCorte = row.Cell(6).GetValue<int>();              // Col F: Día de corte
                        int idResponsable = row.Cell(7).GetValue<int>();         // Col G: IdResponsable
                        DateTime fechaPago = row.Cell(9).GetValue<DateTime>();   // Col I: Fecha de pago
                        decimal saldoRestante = row.Cell(10).GetValue<decimal>();// Col J: Cantidad recibida

                        // Manejo seguro de celdas nulas o vacías
                        string comentario = row.Cell(11).IsEmpty() ? "" : row.Cell(11).GetValue<string>();
                        int idBanco = row.Cell(12).IsEmpty() ? 0 : row.Cell(12).GetValue<int>();
                        string banco = row.Cell(13).IsEmpty() ? "" : row.Cell(13).GetValue<string>();
                        string referencia = row.Cell(14).IsEmpty() ? "" : row.Cell(14).GetValue<string>();
                        string rutaImagen = row.Cell(15).IsEmpty() ? "" : row.Cell(15).GetValue<string>();

                        // ---------------------------------------------------------------------
                        // Determinación del punto de inicio de la mensualidad
                        // ---------------------------------------------------------------------
                        DateTime fechaInicioActual;

                        if (ultimasFechasInicio.TryGetValue(idServicio, out DateTime ultimaFecha) && ultimaFecha == fechaInicioExcel)
                        {
                            var ultimaMensualidad = ListMensualidades
                                .Where(m => m.IdUsuarioM == idServicio)
                                .OrderByDescending(m => m.FechaLimite)
                                .FirstOrDefault();

                            if (ultimaMensualidad != null)
                            {
                                fechaInicioActual = ultimaMensualidad.Pagado ? ultimaMensualidad.FechaLimite : ultimaMensualidad.FechaInicio;
                            }
                            else
                            {
                                fechaInicioActual = fechaInicioExcel;
                            }
                        }
                        else
                        {
                            fechaInicioActual = fechaInicioExcel;
                            ultimasFechasInicio[idServicio] = fechaInicioExcel;
                        }

                        // Carga de plan base e información complementaria (Asíncrono)
                        var planBase = await obj.GetPlanByIdUsuarioM(idServicio);
                        decimal precioPlanBase = planBase != null ? planBase.Precio : 0;
                        var usuarioMInfo = await obj.GetUsuariosMikrotiksById(idServicio);
                        var mikrotikInfo = usuarioMInfo != null ? await obj.GetMikrotikById(usuarioMInfo.IdMikrotik) : null;
                        var clienteInfo = await obj.GetClienteById(idCliente);

                        // Registro del cliente en la lista si no existe
                        if (!ListClientes.Any(x => x.IdCliente == idCliente && x.IdUser == idServicio))
                        {
                            ListClientes.Add(new UsuariosandPlanesModel
                            {
                                Identificador = $"Cli{idCliente}Us{idServicio}",
                                IdCliente = idCliente,
                                Cliente = clienteInfo != null ? clienteInfo.Nombre : "Cliente Desconocido",
                                IdUser = idServicio,
                                Usuario = usuarioMInfo != null ? usuarioMInfo.Nombre : "Usuario Desconocido",
                                IdPlan = planBase != null ? planBase.Id : 0,
                                Plan = planBase != null ? planBase.Nombre : "Plan Desconocido",
                                Estatus = usuarioMInfo != null ? usuarioMInfo.Estatus : "Inactivo",
                                Mikrotik = mikrotikInfo != null ? mikrotikInfo.Nombre : "Mikrotik Desconocido",
                                Mensualidad = "Disponible"
                            });
                        }

                        if (precioPlanBase == 0)
                        {
                            MessageBox.Show($"El servicio con ID {idServicio} no tiene un plan base con costo asignado. Por favor, revisa la configuración.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue; // Saltar a la siguiente fila
                        }

                        // ---------------------------------------------------------------------
                        // Distribución del saldo
                        // ---------------------------------------------------------------------
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

                            // 2. BUSCAR SI YA EXISTE LA MENSUALIDAD EN LA LISTA
                            var mensualidadExistente = ListMensualidades.FirstOrDefault(m =>
                                m.IdUsuarioM == idServicio &&
                                m.FechaInicio == fechaInicioActual &&
                                m.FechaLimite == fechaLimiteActual);

                            decimal costoMensualidad = await CalcularCostoMensualidadAsync(idServicio, fechaInicioActual, fechaLimiteActual, diaCorte, precioPlanBase, ListCambios, obj);
                            if (costoMensualidad <= 0) costoMensualidad = precioPlanBase;

                            int idMensualidad;
                            decimal saldoPendienteMensualidad;

                            if (mensualidadExistente != null)
                            {
                                decimal yaPagado = ListHistorialPagos
                                    .Where(h => h.IdMensualidad == mensualidadExistente.Id)
                                    .Sum(h => h.Cantidad);

                                saldoPendienteMensualidad = costoMensualidad - yaPagado;
                                idMensualidad = mensualidadExistente.Id;

                                if (saldoPendienteMensualidad <= 0)
                                {
                                    fechaInicioActual = fechaLimiteActual;
                                    continue;
                                }
                            }
                            else
                            {
                                saldoPendienteMensualidad = costoMensualidad;
                                idMensualidad = contadorIdMensualidad++;

                                mensualidadExistente = new MensualidadModel
                                {
                                    Id = idMensualidad,
                                    Pagado = false,
                                    IdUsuarioM = idServicio,
                                    DiaCorte = diaCorte,
                                    FechaInicio = fechaInicioActual,
                                    FechaLimite = fechaLimiteActual,
                                    IdUsuario = idResponsable,
                                    Mensualidad = costoMensualidad
                                };

                                ListMensualidades.Add(mensualidadExistente);
                            }

                            // 3. Determinar el monto de este abono
                            decimal pagoParaEstaMensualidad = Math.Min(saldoRestante, saldoPendienteMensualidad);

                            // 4. Calcular la suma acumulada de abonos recibidos tras este pago
                            decimal totalPagadoAcumulado = ListHistorialPagos
                                .Where(h => h.IdMensualidad == idMensualidad)
                                .Sum(h => h.Cantidad) + pagoParaEstaMensualidad;

                            if (totalPagadoAcumulado >= costoMensualidad)
                            {
                                mensualidadExistente.Pagado = true;
                            }

                            // 5. Registrar el pago en el historial
                            ListHistorialPagos.Add(new HistorialPagosModel
                            {
                                Id = ListHistorialPagos.Count + 1,
                                FechaRecibido = fechaPago,
                                Cantidad = pagoParaEstaMensualidad,
                                Comentario = comentario,
                                IdBanco = idBanco,
                                Banco = banco,
                                Referencia = referencia,
                                Imagen = (!string.IsNullOrEmpty(rutaImagen) && File.Exists(rutaImagen)) ? File.ReadAllBytes(rutaImagen) : null,
                                IdMensualidad = idMensualidad,
                                IdUsuario = idResponsable
                            });

                            // 6. Restar la cantidad distribuida
                            saldoRestante -= pagoParaEstaMensualidad;

                            if (saldoRestante > 0)
                            {
                                fechaInicioActual = fechaLimiteActual;
                            }
                        }
                    }
                }

                CargarTablaClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<decimal> CalcularCostoMensualidadAsync(int idServicio, DateTime fechaInicio, DateTime fechaLimite, int diaCorte, decimal precioPlanBase, List<TiempoDefinidosModel> cambios, AppRepository objRepo)
        {
            // 1. Días reales naturales del mes/periodo actual
            int diasNaturalesPeriodo = (int)(fechaLimite.Date - fechaInicio.Date).TotalDays;
            if (diasNaturalesPeriodo <= 0) diasNaturalesPeriodo = 30;

            // 2. Buscar cambios de plan temporales aplicables a este periodo
            var cambiosPeriodo = cambios.Where(x => x.IdUsuarioM == idServicio
                                                   && x.FechaInicio < fechaLimite
                                                   && x.FechaFin > fechaInicio
                                                   && x.Programacion == "Cambio de plan"
                                                   && x.Modo == "Temporal").ToList();

            int diasConPlanNuevo = 0;
            decimal costoTotalPlanesNuevos = 0;

            foreach (var tc in cambiosPeriodo)
            {
                DateTime fInicioEfectiva = fechaInicio.Date > tc.FechaInicio.Date ? fechaInicio.Date : tc.FechaInicio.Date;
                DateTime fFinEfectiva = fechaLimite.Date < tc.FechaFin.Date ? fechaLimite.Date : tc.FechaFin.Date;

                int diasEfectivos = (int)(fFinEfectiva - fInicioEfectiva).TotalDays;

                if (diasEfectivos > 0)
                {
                    var planNuevo = await objRepo.GetPlanById(tc.IdPlan);
                    decimal precioPlanNuevo = planNuevo != null ? planNuevo.Precio : 0;

                    // SI EL CAMBIO DE PLAN CUBRE TODO EL MES/PERIODO COMPLETO:
                    if (diasEfectivos >= diasNaturalesPeriodo)
                    {
                        return precioPlanNuevo;
                    }

                    diasConPlanNuevo += diasEfectivos;
                    // Prorrateo diario en base comercial (30 días) para días parciales
                    costoTotalPlanesNuevos += diasEfectivos * (precioPlanNuevo / 30.0m);
                }
            }

            // 3. Evaluar suspensiones si existen
            var suspensionesPeriodo = cambios.Where(x => x.IdUsuarioM == idServicio
                                                         && x.FechaInicio < fechaLimite
                                                         && x.FechaFin > fechaInicio
                                                         && x.Programacion == "Suspensión"
                                                         && x.Modo == "Temporal").ToList();

            int diasSuspendidos = 0;
            foreach (var sus in suspensionesPeriodo)
            {
                DateTime fInicioEfectiva = fechaInicio.Date > sus.FechaInicio.Date ? fechaInicio.Date : sus.FechaInicio.Date;
                DateTime fFinEfectiva = fechaLimite.Date < sus.FechaFin.Date ? fechaLimite.Date : sus.FechaFin.Date;

                int diasEfectivos = (int)(fFinEfectiva - fInicioEfectiva).TotalDays;
                if (diasEfectivos > 0) diasSuspendidos += diasEfectivos;
            }

            // 4. Si no hubo cambio de plan nuevo o cubrió solo parte del mes
            int diasRestantes = 30 - diasConPlanNuevo - diasSuspendidos;
            if (diasRestantes < 0) diasRestantes = 0;

            decimal montoBruto = costoTotalPlanesNuevos + (diasRestantes * (precioPlanBase / 30.0m));

            // 5. Redondeo financiero
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
                        try
                        {
                            DateTime Desde = (DateTime)DGVClientes.Rows[e.RowIndex].Cells["FechaInicio"].Value;
                            DateTime Hasta = (DateTime)DGVClientes.Rows[e.RowIndex].Cells["FechaLimite"].Value;

                            // 1. Filtrar los cambios/suspensiones que afectan a esta mensualidad
                            var Detalles = ListCambios.Where(
                                x => x.IdUsuarioM == IdUsuarioMRevision
                                  && x.FechaInicio < Hasta
                                  && x.FechaFin > Desde
                                  && x.Modo == "Temporal"
                            ).ToList();

                            // REGLA: Si no hay cambios ni suspensiones, no se abre ni genera detalle
                            if (Detalles.Count == 0)
                            {
                                MessageBox.Show("Este período transcurrió con normalidad en su plan base. No hay cambios ni suspensiones que detallar.",
                                                "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            // Días reales que abarca esta mensualidad específica
                            int diasTotalesMensualidad = (int)(Hasta.Date - Desde.Date).TotalDays;
                            if (diasTotalesMensualidad <= 0) diasTotalesMensualidad = 30;

                            ListDestalles = new List<ListDetallesMensualidadModel>();
                            AppRepository objRepo = new AppRepository();

                            int diasOcupadosPorCambios = 0;

                            // Variable para almacenar el ID del plan original usado durante esta mensualidad
                            int idPlanOriginalPeriodo = 0;

                            // 2. Procesar y agregar cada Cambio o Suspensión registrado
                            foreach (var item in Detalles)
                            {
                                decimal costoCalculado = 0;

                                // Guardamos el ID del plan original para usarlo en los días sobrantes del plan base
                                if (idPlanOriginalPeriodo == 0 && item.IdPlanOriginal > 0)
                                {
                                    idPlanOriginalPeriodo = item.IdPlanOriginal;
                                }

                                // Recortar las fechas al rango efectivo dentro de la mensualidad [Desde, Hasta]
                                DateTime fInicioEfectiva = Desde > item.FechaInicio ? Desde : item.FechaInicio;
                                DateTime fFinEfectiva = Hasta < item.FechaFin ? Hasta : item.FechaFin;

                                int diasEfectivos = (int)(fFinEfectiva.Date - fInicioEfectiva.Date).TotalDays;

                                if (diasEfectivos > diasTotalesMensualidad)
                                    diasEfectivos = diasTotalesMensualidad;

                                if (item.Programacion == "Cambio de plan")
                                {
                                    var planNuevo = objRepo.GetPlanById(item.IdPlan).Result;
                                    decimal precioPlan = planNuevo != null ? planNuevo.Precio : 0;

                                    // SI EL CAMBIO DE PLAN CUBRE TODO EL MES/PERIODO COMPLETO:
                                    if (diasEfectivos >= diasTotalesMensualidad)
                                    {
                                        costoCalculado = precioPlan;
                                        diasOcupadosPorCambios = 30;
                                    }
                                    else
                                    {
                                        diasOcupadosPorCambios += diasEfectivos;

                                        // Cálculo de costo prorrateado sobre base 30 para días parciales
                                        decimal costoBruto = diasEfectivos * (precioPlan / 30.0m);

                                        // Redondeo financiero
                                        decimal parteEntera = Math.Floor(costoBruto);
                                        decimal parteDecimal = costoBruto - parteEntera;

                                        if (parteDecimal > 0.00m && parteDecimal < 0.30m)
                                            costoCalculado = parteEntera;
                                        else if (parteDecimal >= 0.30m && parteDecimal <= 0.50m)
                                            costoCalculado = parteEntera + 0.50m;
                                        else if (parteDecimal > 0.50m)
                                            costoCalculado = parteEntera + 1.00m;
                                        else
                                            costoCalculado = parteEntera;
                                    }
                                }
                                else if (item.Programacion == "Suspensión")
                                {
                                    costoCalculado = 0.00m;
                                    diasOcupadosPorCambios += diasEfectivos;
                                }

                                ListDetallesMensualidadModel LD = new ListDetallesMensualidadModel
                                {
                                    Id = item.Id,
                                    FechaInicio = fInicioEfectiva,
                                    FechaFin = fFinEfectiva,
                                    Estatus = "Activo",
                                    Programacion = $"{item.Programacion} ({diasEfectivos} días)",
                                    Plan = item.Plan,
                                    Costo = costoCalculado
                                };

                                ListDestalles.Add(LD);
                            }

                            // 3. Calcular los días restantes del plan base (sobre base comercial de 30 días)
                            int diasRestantesPlanBase = 30 - diasOcupadosPorCambios;
                            if (diasRestantesPlanBase < 0) diasRestantesPlanBase = 0;

                            // 4. Agregar la fila del Plan Base cobrando con el PLAN ORIGINAL registrado en el Excel
                            if (diasRestantesPlanBase > 0)
                            {
                                // Consultar el plan original registrado en ese periodo, o en su defecto el plan del usuario
                                var planOriginal = idPlanOriginalPeriodo > 0
                                    ? objRepo.GetPlanById(idPlanOriginalPeriodo).Result
                                    : objRepo.GetPlanByIdUsuarioM(IdUsuarioMRevision).Result;

                                decimal precioPlanBase = planOriginal != null ? planOriginal.Precio : 0;
                                string nombrePlanBase = planOriginal != null ? planOriginal.Nombre : "Plan Original";

                                decimal costoBrutoBase = diasRestantesPlanBase * (precioPlanBase / 30.0m);

                                // Redondeo financiero para el consumo del Plan Base / Original
                                decimal entBase = Math.Floor(costoBrutoBase);
                                decimal decBase = costoBrutoBase - entBase;
                                decimal costoBaseRedondeado = entBase;

                                if (decBase > 0.00m && decBase < 0.30m)
                                    costoBaseRedondeado = entBase;
                                else if (decBase >= 0.30m && decBase <= 0.50m)
                                    costoBaseRedondeado = entBase + 0.50m;
                                else if (decBase > 0.50m)
                                    costoBaseRedondeado = entBase + 1.00m;

                                ListDetallesMensualidadModel LDBase = new ListDetallesMensualidadModel
                                {
                                    Id = 0,
                                    FechaInicio = Desde,
                                    FechaFin = Hasta,
                                    Estatus = "Activo",
                                    Programacion = $"Consumo Plan Original ({diasRestantesPlanBase} días)",
                                    Plan = nombrePlanBase,
                                    Costo = costoBaseRedondeado
                                };

                                ListDestalles.Add(LDBase);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al cargar el detalle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        Opcion = 2;
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
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Costo",
                HeaderText = "Costo",
                DataPropertyName = "Costo",
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
                case 3:
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
                            wsCambios.Cell(1, 1).Value = "Descripción";  // A
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
                                        "El servicio en cambios con id " + item.IdUsuarioM.ToString() + " no existe en el sistema";
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
                                if (item.IdPlanOriginal != item.IdPlan)
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
                                if (item.IdMikrotikOriginal != item.IdMikrotikReceptor)
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
                                var Anidado = obj.GetPlanesAnidadosbyParametros(item.IdMikrotikReceptor, item.IdPlan).Result;
                                int IdPlanAnidado = Anidado?.Id ?? 0;
                                if (IdPlanAnidado == 0)
                                {
                                    wsCambios.Cell(filaCambios, 1).Value =
                                          "No existe el plan nuevo: " + item.IdPlan.ToString() + " en el mikrotik receptor: " + item.IdMikrotikReceptor.ToString();
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
                                    ListCambios[ContadorCambios].Id = 0;
                                    var infocliente = obj.GetUsuariosMikrotiksById(ListCambios[ContadorCambios].IdUsuarioM).Result;
                                    if(infocliente.IdMikrotikOriginal != ListCambios[ContadorCambios].IdMikrotikOriginal ||
                                       infocliente.IdPlanOriginal != ListCambios[ContadorCambios].IdPlanOriginal)
                                    {
                                       bool roriginal= obj.UpdateOriginalesbyIdUsuarioM(ListCambios[ContadorCambios].IdUsuarioM,
                                            ListCambios[ContadorCambios].IdPlanOriginal,
                                            ListCambios[ContadorCambios].IdMikrotikOriginal
                                            ).Result;
                                    }
                                    var resultcambio = obj.SaveTiempoCambio(ListCambios[ContadorCambios]).Result;
                                    if (resultcambio)
                                    {
                                        wsCambios.Cell(filaCambios, 1).Value =
                                    "Se guardo correctamente el(la) " + item.Programacion + " en el sistema para el servicio " + item.IdUsuarioM +
                                    " con fecha de inicio " + item.FechaInicio.ToString();
                                        wsCambios.Cell(filaCambios, 2).Value = "Satisfactorio";
                                    }
                                    else
                                    {
                                        wsCambios.Cell(filaCambios, 1).Value =
                               "Error al guardar el(la) " + item.Programacion + " en el sistema para el servicio " + item.IdUsuarioM +
                               " con fecha de inicio " + item.FechaInicio.ToString();
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
                            wsCambios.Columns().AdjustToContents();
                            // ==============================================================
                            // HOJA 21: Mensualidades y pagos (Aparecerá segundo)
                            // ==============================================================
                            var wPagos = workbook.Worksheets.Add("Pagos");

                            // Encabezados
                            wPagos.Cell(1, 1).Value = "Descripción";  // A
                            wPagos.Cell(1, 2).Value = "Resultado";  // B
                            // Formato a los encabezados (A1 a B1)
                            var headerPagos = wsCambios.Range("A1:B1");
                            headerPagos.Style.Font.Bold = true;
                            headerPagos.Style.Fill.BackgroundColor = XLColor.CornflowerBlue;
                            headerPagos.Style.Font.FontColor = XLColor.White;
                            int ContadorPagos = -1;
                            int filaPagos = 2;
                            int IdMensualidad = 0;
                            foreach (var item in ListMensualidades)
                            {
                                ContadorPagos += 1;
                                var existServicio = obj.GetUsuariosMikrotiksById(item.IdUsuarioM).Result;
                                if (existServicio.Id == 0)
                                {
                                    wPagos.Cell(filaPagos, 1).Value =
                                        "El servicio en pagos con id " + item.IdUsuarioM.ToString() + " no existe en el sistema";
                                    wPagos.Cell(filaPagos, 2).Value = "Error";
                                    filaPagos++;
                                    continue;
                                }
                                var exitMensualidad = obj.GetMensualidadbyIdUsuarioM(item.IdUsuarioM, item.FechaInicio, item.FechaLimite).Result;
                                if (exitMensualidad.Count() == 0)
                                {
                                    IdMensualidad = ListMensualidades[ContadorPagos].Id;
                                    ListMensualidades[ContadorPagos].Id = 0;
                                    var resultMensualidad = obj.SaveMensualidad(ListMensualidades[ContadorPagos]).Result;
                                    if (resultMensualidad != 0)
                                    {
                                        wPagos.Cell(filaPagos, 1).Value =
                                    "Se guardo correctamente la mensualidad con fecha " + item.FechaInicio.ToString() +
                                    " en el sistema para el servicio " + item.IdUsuarioM;
                                        wPagos.Cell(filaPagos, 2).Value = "Satisfactorio";
                                        filaPagos++;
                                        var Pagos = ListHistorialPagos.Where(x => x.IdMensualidad == IdMensualidad).ToList();
                                        ListPagos = new List<ListHistorialPagosModel>();
                                        foreach (var itempagos in Pagos)
                                        {
                                            HistorialPagosModel HP = new HistorialPagosModel
                                            {
                                                Id = 0,
                                                FechaRecibido = itempagos.FechaRecibido,
                                                Cantidad = itempagos.Cantidad,
                                                Comentario = itempagos.Comentario,
                                                IdBanco = itempagos.IdBanco,
                                                Referencia = itempagos.Referencia,
                                                Imagen = itempagos.Imagen,
                                                IdMensualidad = resultMensualidad,
                                                IdUsuario = itempagos.IdUsuario
                                            };
                                            int rhp = obj.SaveHistorialPagos(HP).Result;
                                            if (rhp != 0)
                                            {
                                                wPagos.Cell(filaPagos, 1).Value =
                                    "Se guardo correctamente el pago con fecha " + itempagos.FechaRecibido.ToString() +
                                       "de la mensualidad con fecha " + item.FechaInicio.ToString() +
                                    " en el sistema";
                                                wPagos.Cell(filaPagos, 2).Value = "Satisfactorio";
                                                filaPagos++;
                                            }
                                            else
                                            {
                                                wPagos.Cell(filaPagos, 1).Value =
                                   "Error al guardar el pago con fecha " + itempagos.FechaRecibido.ToString() +
                                      "de la mensualidad con fecha " + item.FechaInicio.ToString() +
                                   " en el sistema";
                                                wPagos.Cell(filaPagos, 2).Value = "Error";
                                                filaPagos++;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        wPagos.Cell(filaPagos, 1).Value =
                           "Error al intentar guaardar la mensualidad con fecha " + item.FechaInicio.ToString() +
                           " en el sistema para el servicio " + item.IdUsuarioM;
                                        wPagos.Cell(filaPagos, 2).Value = "Error";
                                    }
                                    filaPagos++;
                                }
                                else
                                {
                                    wPagos.Cell(filaPagos, 1).Value =
                                         "Ya existe la mensualidad con fecha " + item.FechaInicio.ToString() +
                                         " registrado en el sistema para el servicio " + item.IdUsuarioM;
                                    wPagos.Cell(filaPagos, 2).Value = "Error";
                                    filaPagos++;
                                    var Pagos = ListHistorialPagos.Where(x => x.IdMensualidad == exitMensualidad[0].Id).ToList();
                                    ListPagos = new List<ListHistorialPagosModel>();
                                    int PagosGuardados = obj.GetHistorialPagos(exitMensualidad[0].Id, string.Empty, 0, 0).Result.ToList().Count();
                                   if(PagosGuardados >  0)
                                    {
                                        wPagos.Cell(filaPagos, 1).Value =
                                        "La mensualidad con fecha " + item.FechaInicio.ToString() +
                                        " ya cuenta con " + PagosGuardados.ToString() + " pagos guardados previamente registrado en el sistema";
                                        wPagos.Cell(filaPagos, 2).Value = "Información";
                                        filaPagos++;
                                    }
                                    foreach (var itempagos in Pagos)
                                    {
                                        PagosGuardados--;
                                        if(PagosGuardados <= 0)
                                        {
                                            HistorialPagosModel HP = new HistorialPagosModel
                                            {
                                                Id = 0,
                                                FechaRecibido = itempagos.FechaRecibido,
                                                Cantidad = itempagos.Cantidad,
                                                Comentario = itempagos.Comentario,
                                                IdBanco = itempagos.IdBanco,
                                                Referencia = itempagos.Referencia,
                                                Imagen = itempagos.Imagen,
                                                IdMensualidad = exitMensualidad[0].Id,
                                                IdUsuario = itempagos.IdUsuario
                                            };

                                            int rhp = obj.SaveHistorialPagos(HP).Result;
                                            if (rhp != 0)
                                            {
                                                wPagos.Cell(filaPagos, 1).Value =
                                    "Se guardo correctamente el pago con fecha " + itempagos.FechaRecibido.ToString() +
                                       "de la mensualidad con fecha " + item.FechaInicio.ToString() +
                                    " en el sistema";
                                                wPagos.Cell(filaPagos, 2).Value = "Satisfactorio";
                                                filaPagos++;
                                            }
                                            else
                                            {
                                                wPagos.Cell(filaPagos, 1).Value =
                                   "Error al guardar el pago con fecha " + itempagos.FechaRecibido.ToString() +
                                      "de la mensualidad con fecha " + item.FechaInicio.ToString() +
                                   " en el sistema";
                                                wPagos.Cell(filaPagos, 2).Value = "Error";
                                                filaPagos++;
                                            }
                                        }                                        
                                    }
                                }
                            }
                            wPagos.Columns().AdjustToContents();
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
