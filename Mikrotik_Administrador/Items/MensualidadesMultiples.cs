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
using System.Runtime.Remoting;
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
                            wsCambios.Cell(1, 3).Value = "Cuando inicio";               // C
                            wsCambios.Cell(1, 4).Value = "Días que duro";               // D
                            wsCambios.Cell(1, 5).Value = "IdPlan original";                // E
                            wsCambios.Cell(1, 6).Value = "Nombre del plan original";   // F
                            wsCambios.Cell(1, 7).Value = "IdMikrotik original";        // G
                            wsCambios.Cell(1, 8).Value = "Nombre mikrotik original";    // H
                            wsCambios.Cell(1, 9).Value = "IdPlan nuevo";                // I
                            wsCambios.Cell(1, 10).Value = "Nombre del plan nuevo";   // J
                            wsCambios.Cell(1, 11).Value = "IdMikrotik receptor";        // K
                            wsCambios.Cell(1, 12).Value = "Nombre mikrotik receptor";    // L

                            // Formato a los encabezados (A1 a L1)
                            var headerCambios = wsCambios.Range("A1:L1");
                            headerCambios.Style.Font.Bold = true;
                            headerCambios.Style.Fill.BackgroundColor = XLColor.CornflowerBlue;
                            headerCambios.Style.Font.FontColor = XLColor.White;

                            int filaCambios = 2;
                            foreach (ListClientesDescargaModel item in Seleccionados)
                            {
                                wsCambios.Cell(filaCambios, 1).Value = item.IdUsuarioM;
                                wsCambios.Cell(filaCambios, 2).Value = item.Usuario;
                                wsCambios.Cell(filaCambios, 3).Value = DateTime.Now;
                                wsCambios.Cell(filaCambios, 3).Style.DateFormat.Format = "dd/MM/yyyy";
                                wsCambios.Cell(filaCambios, 4).Value = 1;
                                wsCambios.Cell(filaCambios, 5).Value = 1;
                                wsCambios.Cell(filaCambios, 6).Value = "Plan Basico";
                                wsCambios.Cell(filaCambios, 7).Value = 1;
                                wsCambios.Cell(filaCambios, 8).Value = "Santa Maria";
                                wsCambios.Cell(filaCambios, 9).Value = 1;
                                wsCambios.Cell(filaCambios, 10).Value = "Plan Basico";
                                wsCambios.Cell(filaCambios, 11).Value = 1;
                                wsCambios.Cell(filaCambios, 12).Value = "Santa Maria";
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

                Dictionary<int, DateTime> ultimasFechasInicio = new Dictionary<int, DateTime>();

                using (var workbook = new ClosedXML.Excel.XLWorkbook(rutaArchivo))
                {
                    // =========================================================================
                    // PÁGINA 1: CAMBIOS Y SUSPENSIONES
                    // =========================================================================
                    var wsCambios = workbook.Worksheet("Cambios");
                    bool primeraFila1 = true;

                    foreach (var row in wsCambios.RowsUsed())
                    {
                        if (primeraFila1) { primeraFila1 = false; continue; }

                        int idServicio = row.Cell(1).GetValue<int>();             // Col A: IdServicio
                        DateTime fechaInicio = row.Cell(3).GetValue<DateTime>(); // Col C: Cuando inicio
                        int diasDuro = row.Cell(4).GetValue<int>();              // Col D: Días que duró

                        if (diasDuro < 1)
                        {
                            MessageBox.Show("Los días de duración no pueden ser menor a 1", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }

                        // Fecha fin visual e inclusiva para guardar (ej. del 01/01 al 10/01)
                        DateTime fechaFin = fechaInicio.AddDays(diasDuro - 1);

                        bool valido = !ListCambios.Any(x => x.IdUsuarioM == idServicio &&
                                                           fechaInicio >= x.FechaInicio &&
                                                           fechaInicio <= x.FechaFin);

                        if (valido)
                        {
                            int idPlanOriginal = row.Cell(5).GetValue<int>();      // Col E: IdPlan original
                            int idMikrotikOriginal = row.Cell(7).GetValue<int>();  // Col G: IdMikrotik original
                            int idPlanNuevo = row.Cell(9).GetValue<int>();         // Col I: IdPlan nuevo
                            int idMikrotikReceptor = row.Cell(11).GetValue<int>(); // Col K: IdMikrotik receptor

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
                                IdPlanOriginal = idPlanOriginal,
                                IdMikrotikOriginal = idMikrotikOriginal,
                                IdPlan = idPlanNuevo,
                                Plan = planB != null ? planB.Nombre : "Plan Desconocido",
                                IdMikrotikReceptor = idMikrotikReceptor,
                                Nota = "Introducido por Excel",
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

                        int idCliente = row.Cell(1).GetValue<int>();                  // Col A: IdCliente
                        int idServicio = row.Cell(3).GetValue<int>();                 // Col C: IdServicio
                        DateTime fechaInicioExcel = row.Cell(5).GetValue<DateTime>(); // Col E: Inicio la mensualidad
                        int diaCorte = row.Cell(6).GetValue<int>();                   // Col F: Día de corte
                        int idResponsable = row.Cell(7).GetValue<int>();              // Col G: IdResponsable
                        DateTime fechaPago = row.Cell(9).GetValue<DateTime>();        // Col I: Cuando se recibio el pago
                        decimal saldoRestante = row.Cell(10).GetValue<decimal>();     // Col J: Cantidad recibida

                        string comentario = row.Cell(11).IsEmpty() ? "" : row.Cell(11).GetValue<string>();
                        int idBanco = row.Cell(12).IsEmpty() ? 0 : row.Cell(12).GetValue<int>();
                        string banco = row.Cell(13).IsEmpty() ? "" : row.Cell(13).GetValue<string>();
                        string referencia = row.Cell(14).IsEmpty() ? "" : row.Cell(14).GetValue<string>();
                        string rutaImagen = row.Cell(15).IsEmpty() ? "" : row.Cell(15).GetValue<string>();

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

                        var planBaseBD = await obj.GetPlanByIdUsuarioM(idServicio);
                        decimal precioPlanBaseBD = planBaseBD != null ? planBaseBD.Precio : 0;
                        var usuarioMInfo = await obj.GetUsuariosMikrotiksById(idServicio);
                        var mikrotikInfo = usuarioMInfo != null ? await obj.GetMikrotikById(usuarioMInfo.IdMikrotik) : null;
                        var clienteInfo = await obj.GetClienteById(idCliente);

                        if (!ListClientes.Any(x => x.IdCliente == idCliente && x.IdUser == idServicio))
                        {
                            ListClientes.Add(new UsuariosandPlanesModel
                            {
                                Identificador = $"Cli{idCliente}Us{idServicio}",
                                IdCliente = idCliente,
                                Cliente = clienteInfo != null ? clienteInfo.Nombre : "Cliente Desconocido",
                                IdUser = idServicio,
                                Usuario = usuarioMInfo != null ? usuarioMInfo.Nombre : "Usuario Desconocido",
                                IdPlan = planBaseBD != null ? planBaseBD.Id : 0,
                                Plan = planBaseBD != null ? planBaseBD.Nombre : "Plan Desconocido",
                                Estatus = usuarioMInfo != null ? usuarioMInfo.Estatus : "Inactivo",
                                Mikrotik = mikrotikInfo != null ? mikrotikInfo.Nombre : "Mikrotik Desconocido",
                                Mensualidad = "Disponible"
                            });
                        }

                        // Dispersión del pago
                        while (saldoRestante > 0)
                        {
                            DateTime fechaLimiteActual;
                            if (diaCorte <= fechaInicioActual.Day)
                            {
                                fechaLimiteActual = new DateTime(fechaInicioActual.Year, fechaInicioActual.Month, diaCorte).AddMonths(1);
                            }
                            else
                            {
                                fechaLimiteActual = new DateTime(fechaInicioActual.Year, fechaInicioActual.Month, diaCorte);
                            }

                            var mensualidadExistente = ListMensualidades.FirstOrDefault(m =>
                                m.IdUsuarioM == idServicio &&
                                m.FechaInicio == fechaInicioActual &&
                                m.FechaLimite == fechaLimiteActual);

                            // DETERMINAR EL PRECIO DEL PLAN BASE PARA EL PERÍODO
                            decimal precioTargetBD = precioPlanBaseBD;

                            // Solo si existe un cambio registrado cuya fecha de inicio sea menor o igual a la mensualidad actual, usamos el IdPlanOriginal
                            var primerCambio = ListCambios.Where(x => x.IdUsuarioM == idServicio && x.IdPlanOriginal > 0 && x.FechaInicio <= fechaLimiteActual)
                                                          .OrderBy(x => x.FechaInicio)
                                                          .FirstOrDefault();

                            if (primerCambio != null)
                            {
                                var planOrigObj = await obj.GetPlanById(primerCambio.IdPlanOriginal);
                                if (planOrigObj != null && planOrigObj.Precio > 0)
                                {
                                    precioTargetBD = planOrigObj.Precio; // Aplica $300 a partir del mes del cambio
                                }
                            }

                            // Calcular el costo exacto del mes
                            decimal costoMensualidad = await CalcularCostoMensualidadAsync(idServicio, fechaInicioActual, fechaLimiteActual, precioTargetBD, ListCambios);

                            if (costoMensualidad <= 0)
                            {
                                bool esSuspensionValida = ListCambios.Any(x => x.IdUsuarioM == idServicio
                                                                            && x.FechaInicio < fechaLimiteActual
                                                                            && x.FechaFin >= fechaInicioActual);

                                if (!esSuspensionValida)
                                {
                                    MessageBox.Show($"El servicio ID {idServicio} tiene un costo calculado de $0 y no se indicó un 'Plan Original' con costo en la pestaña Cambios.\n" +
                                                    $"No es posible dispersar el saldo de ${saldoRestante}.",
                                                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    break;
                                }
                            }

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

                            decimal pagoParaEstaMensualidad = Math.Min(saldoRestante, saldoPendienteMensualidad);

                            decimal totalPagadoAcumulado = ListHistorialPagos
                                .Where(h => h.IdMensualidad == idMensualidad)
                                .Sum(h => h.Cantidad) + pagoParaEstaMensualidad;

                            if (totalPagadoAcumulado >= costoMensualidad)
                            {
                                mensualidadExistente.Pagado = true;
                            }

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

                            saldoRestante -= pagoParaEstaMensualidad;

                            if (saldoRestante > 0)
                            {
                                fechaInicioActual = fechaLimiteActual;
                            }
                        }
                    }

                    CargarTablaClientes();
                    MessageBox.Show("Excel cargado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================================================================
        // MÉTODO DE CÁLCULO DE COSTO CON PRORRATEO Y BASE 30 DÍAS
        // =========================================================================
        private async Task<decimal> CalcularCostoMensualidadAsync(
     int idServicio,
     DateTime fechaInicio,
     DateTime fechaLimite,
     decimal precioPlanBase,
     List<TiempoDefinidosModel> listaCambios)
        {
            AppRepository objRepo = new AppRepository();

            // 1. Filtrar eventos de cambio que afecten esta mensualidad
            var eventosDelPeriodo = listaCambios.Where(x => x.IdUsuarioM == idServicio
                                                         && x.FechaInicio < fechaLimite
                                                         && x.FechaFin >= fechaInicio
                                                         && x.Modo == "Temporal")
                                                .OrderBy(x => x.FechaInicio)
                                                .ToList();

            // =========================================================================
            // 2. CASO A: Mes sin cambios ni suspensiones (Ejemplo: Diciembre sin cambios, Febrero)
            // =========================================================================
            if (eventosDelPeriodo.Count == 0)
            {
                // Si el ciclo inicia en un día intermedio (ej. día 15) y termina el día 1, es un inicio prorrateado (15 días)
                if (fechaInicio.Day != fechaLimite.Day && fechaInicio.Day == 15 && fechaLimite.Day == 1)
                {
                    decimal costoProrrateadoInicial = 15 * (precioPlanBase / 30.0m);
                    return RedondearMontoFinanciero(costoProrrateadoInicial); // Retorna $250.00 para $500 base
                }

                // Para cualquier mes completo (sea Febrero de 28 días, Marzo de 31 o Abril de 30),
                // al ser un ciclo completo se cobra la tarifa integra del plan base.
                return precioPlanBase; // Retorna $300.00 para Febrero
            }

            // =========================================================================
            // 3. CASO B: Mes con cambios temporales (Ejemplo: Enero con cambio de 10 días)
            // =========================================================================
            int diasOcupadosPorCambios = 0;
            decimal costoTotalAcumulado = 0m;

            foreach (var cambio in eventosDelPeriodo)
            {
                int diasEfectivos = cambio.Dias;

                if (diasEfectivos > 0)
                {
                    if (diasEfectivos > 30) diasEfectivos = 30;

                    diasOcupadosPorCambios += diasEfectivos;

                    var planNuevo = await objRepo.GetPlanById(cambio.IdPlan);
                    decimal precioPlanNuevo = planNuevo != null ? planNuevo.Precio : 0m;

                    decimal costoTramo = diasEfectivos * (precioPlanNuevo / 30.0m);
                    costoTotalAcumulado += RedondearMontoFinanciero(costoTramo);
                }
            }

            // 4. Completar los días restantes a base 30 comerciales con el plan original
            int diasRestantesPlanBase = 30 - diasOcupadosPorCambios;
            if (diasRestantesPlanBase > 0)
            {
                decimal costoTramoRestante = diasRestantesPlanBase * (precioPlanBase / 30.0m);
                costoTotalAcumulado += RedondearMontoFinanciero(costoTramoRestante);
            }

            return costoTotalAcumulado;
        }
        // =========================================================================
        // MÉTODO DE CÁLCULO DE COSTO CON PRORRATEO Y PLAN ORIGINAL
        // =========================================================================


        // =========================================================================
        // MÉTODO REUTILIZABLE DE REDONDEO FINANCIERO (.00, .50, 1.00)
        // =========================================================================
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

        private async void DGVClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                            DateTime desde = Convert.ToDateTime(DGVClientes.Rows[e.RowIndex].Cells["FechaInicio"].Value);
                            DateTime hasta = Convert.ToDateTime(DGVClientes.Rows[e.RowIndex].Cells["FechaLimite"].Value);
                            decimal costoTotalMensualidad = Convert.ToDecimal(DGVClientes.Rows[e.RowIndex].Cells["Mensualidad"].Value);

                            // 1. Filtrar eventos (cambios temporales o suspensiones) registrados
                            var detalles = ListCambios.Where(x => x.IdUsuarioM == IdUsuarioMRevision
                                                               && x.FechaInicio <= hasta
                                                               && x.FechaFin >= desde
                                                               && x.Modo == "Temporal")
                                                      .OrderBy(x => x.FechaInicio)
                                                      .ToList();

                            if (detalles.Count == 0)
                            {
                                MessageBox.Show("Este período transcurrió con normalidad. No hay cambios que detallar.",
                                                "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            int diasTotalesMensualidad = 30;
                            ListDestalles = new List<ListDetallesMensualidadModel>();
                            AppRepository objRepo = new AppRepository();

                            int diasOcupadosPorCambios = 0;
                            decimal costoAcumuladoDetalles = 0m;
                            int idPlanOriginalPeriodo = 0;
                            DateTime fechaProcesadaHasta = desde;

                            // 2. Agregar tramos de Cambios / Suspensiones
                            foreach (var item in detalles)
                            {
                                if (idPlanOriginalPeriodo == 0 && item.IdPlanOriginal > 0)
                                {
                                    idPlanOriginalPeriodo = item.IdPlanOriginal;
                                }

                                // Acotar rango de fechas efectivo dentro de la mensualidad
                                DateTime fInicioEfectiva = desde > item.FechaInicio ? desde : item.FechaInicio;
                                DateTime fFinEfectiva = hasta < item.FechaFin ? hasta : item.FechaFin;

                                // Días del evento de cambio
                                int diasEfectivos = item.Dias;

                                if (diasEfectivos > 0)
                                {
                                    if (diasEfectivos > diasTotalesMensualidad)
                                        diasEfectivos = diasTotalesMensualidad;

                                    var planNuevo = await objRepo.GetPlanById(item.IdPlan);
                                    decimal precioPlan = planNuevo != null ? planNuevo.Precio : 0m;

                                    decimal costoCalculado = 0m;
                                    if (diasEfectivos >= diasTotalesMensualidad)
                                    {
                                        costoCalculado = precioPlan;
                                        diasOcupadosPorCambios = 30;
                                    }
                                    else
                                    {
                                        diasOcupadosPorCambios += diasEfectivos;
                                        decimal costoBruto = diasEfectivos * (precioPlan / 30.0m);
                                        costoCalculado = RedondearMontoFinanciero(costoBruto);
                                    }

                                    costoAcumuladoDetalles += costoCalculado;

                                    // CORRECCIÓN: Usar directamente fFinEfectiva para respetar los días reales (ej. del 15 al 25 exactos)
                                    DateTime fFinVisual = fFinEfectiva;

                                    ListDestalles.Add(new ListDetallesMensualidadModel
                                    {
                                        Id = item.Id,
                                        FechaInicio = fInicioEfectiva,
                                        FechaFin = fFinVisual,
                                        Estatus = item.Estatus,
                                        Plan = item.Plan,
                                        Costo = costoCalculado
                                    });

                                    // La fecha de inicio del siguiente tramo será el día posterior a la finalización de este cambio
                                    fechaProcesadaHasta = fFinVisual.AddDays(1);
                                }
                            }

                            // 3. Agregar el tramo restante con el Plan Original / Base
                            int diasRestantesPlanBase = 30 - diasOcupadosPorCambios;
                            if (diasRestantesPlanBase > 0 && fechaProcesadaHasta < hasta)
                            {
                                var planOriginal = idPlanOriginalPeriodo > 0
                                    ? await objRepo.GetPlanById(idPlanOriginalPeriodo)
                                    : await objRepo.GetPlanByIdUsuarioM(IdUsuarioMRevision);

                                string nombrePlanBase = planOriginal != null ? planOriginal.Nombre : "Plan Original";

                                decimal costoBaseFinal = costoTotalMensualidad - costoAcumuladoDetalles;
                                if (costoBaseFinal < 0) costoBaseFinal = 0m;

                                // Fecha fin visual del tramo base (un día antes de la fecha límite del mes o la fecha límite exacta)
                                DateTime fFinOriginalVisual = (hasta.Day == 1) ? hasta.AddDays(-1) : hasta;

                                ListDestalles.Add(new ListDetallesMensualidadModel
                                {
                                    Id = 0,
                                    FechaInicio = fechaProcesadaHasta, // Comienza exactamente al día siguiente de finalizar el cambio
                                    FechaFin = fFinOriginalVisual,     // Finaliza en el último día del período
                                    Estatus = "Activo",
                                    Plan = nombrePlanBase,
                                    Costo = costoBaseFinal
                                });
                            }

                            Opcion = 2;
                            CrearTablaDetalles();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al cargar el detalle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
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
                HeaderText = "Empezó",
                DataPropertyName = "FechaInicio",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaFin",
                HeaderText = "Terminó",
                DataPropertyName = "FechaFin",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
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
                Name = "Plan",
                HeaderText = "Plan",
                DataPropertyName = "Plan",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic,
            });

            // Formato de Moneda ($MXN)
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Costo",
                HeaderText = "Costo",
                DataPropertyName = "Costo",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    FormatProvider = new System.Globalization.CultureInfo("es-MX")
                }
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
                SortMode = DataGridViewColumnSortMode.Automatic,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    FormatProvider = new System.Globalization.CultureInfo("es-MX")
                }
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
                                var exitCambiot = obj.GetTiempoCambio(item.IdUsuarioM, item.FechaInicio, item.FechaFin).Result;
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
                                    "Se guardo correctamente el cambio en el sistema para el servicio " + item.IdUsuarioM +
                                    " con fecha de inicio " + item.FechaInicio.ToString();
                                        wsCambios.Cell(filaCambios, 2).Value = "Satisfactorio";
                                    }
                                    else
                                    {
                                        wsCambios.Cell(filaCambios, 1).Value =
                               "Error al guardar el cambio en el sistema para el servicio " + item.IdUsuarioM +
                               " con fecha de inicio " + item.FechaInicio.ToString();
                                        wsCambios.Cell(filaCambios, 2).Value = "Error";
                                    }
                                    filaCambios++;
                                }
                                else
                                {
                                    wsCambios.Cell(filaCambios, 1).Value =
                                         "Ya existe el cambio registrado en el sistema para el servicio " + item.IdUsuarioM +
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
