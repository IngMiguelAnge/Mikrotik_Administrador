using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using Mikrotik_Administrador.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikrotik_Administrador.Catalogos
{
    public partial class MensualidadesMultiples : Form
    {
        public int IdResponsable { get; set; }
        private List<TiempoCambioModel> ListCambios = new List<TiempoCambioModel>();
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
            if(RBMikrotiks.Checked)
            CargarClientes();
            if(RBMensualidades.Checked)
            CargarTablaClientes();
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
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cliente",
                HeaderText = "Cliente",
                DataPropertyName = "Cliente",
                ReadOnly = true,
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdUsuarioM",
                HeaderText = "IdUsuarioM",
                DataPropertyName = "IdUsuarioM",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Usuario",
                HeaderText = "Servicio",
                DataPropertyName = "Usuario",
                ReadOnly = true,
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IP",
                HeaderText = "IP",
                DataPropertyName = "IP",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Mikrotik",
                HeaderText = "Mikrotik",
                DataPropertyName = "Mikrotik",
                ReadOnly = true,
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PlanOriginal",
                HeaderText = "Plan Original",
                DataPropertyName = "PlanOriginal",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DataGridViewCheckBoxColumn chkSeleccionar = new DataGridViewCheckBoxColumn
            {
                Name = "chkSeleccionar",
                HeaderText = "Descargar",
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
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
                      IP = Convert.ToString(r.Cells["IP"].Value),
                      Mikrotik = Convert.ToString(r.Cells["Mikrotik"].Value),
                      PlanOriginal = Convert.ToString(r.Cells["PlanOriginal"].Value),
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
                    saveFileDialog.FileName = "MensualidadesyServicios.xlsx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // 2. Crear el libro de trabajo (Workbook)
                        using (var workbook = new XLWorkbook())
                        {
                            var wsCambios = workbook.Worksheets.Add("Mensualidades");

                            // Encabezados
                            wsCambios.Cell(1, 1).Value = "IdCliente";                  
                            wsCambios.Cell(1, 2).Value = "Cliente";
                            wsCambios.Cell(1, 3).Value = "IdServicio";                  
                            wsCambios.Cell(1, 4).Value = "Servicio";
                            wsCambios.Cell(1, 5).Value = "Mikrotik";    
                            wsCambios.Cell(1, 6).Value = "Año";            
                            wsCambios.Cell(1, 7).Value = "ENERO";   
                            wsCambios.Cell(1, 8).Value = "FEBRERO"; 
                            wsCambios.Cell(1, 9).Value = "MARZO";   
                            wsCambios.Cell(1, 10).Value = "ABRIL";   
                            wsCambios.Cell(1, 11).Value = "MAYO";   
                            wsCambios.Cell(1, 12).Value = "JUNIO";  
                            wsCambios.Cell(1, 13).Value = "JULIO";      
                            wsCambios.Cell(1, 14).Value = "AGOSTO";    
                            wsCambios.Cell(1, 15).Value = "SEPTIEMBRE"; 
                            wsCambios.Cell(1, 16).Value = "OCTUBRE"; 
                            wsCambios.Cell(1, 17).Value = "NOVIEMBRE"; 
                            wsCambios.Cell(1, 18).Value = "DICIEMBRE";
                            wsCambios.Cell(1, 19).Value = "Día de pago";
                            wsCambios.Cell(1, 20).Value = "Plan Actual";
                            wsCambios.Cell(1, 21).Value = "Fecha de suspension";
                            wsCambios.Cell(1, 22).Value = "Télefono";
                            wsCambios.Cell(1, 23).Value = "Segundo Télefono";
                            wsCambios.Cell(1, 24).Value = "IdInstalador";
                            wsCambios.Cell(1, 25).Value = "Instalador";
                            wsCambios.Cell(1, 26).Value = "Fecha de instalación";
                            wsCambios.Cell(1, 27).Value = "Nota";
                            wsCambios.Cell(1, 28).Value = "Modalidad";
                            wsCambios.Cell(1, 29).Value = "RB";
                            wsCambios.Cell(1, 30).Value = "Antena";
                            wsCambios.Cell(1, 31).Value = "Cable";
                            wsCambios.Cell(1, 32).Value = "Router";
                            wsCambios.Cell(1, 33).Value = "Cargador";
                            wsCambios.Cell(1, 34).Value = "Tubo";
                            wsCambios.Cell(1, 35).Value = "IP";
                            wsCambios.Cell(1, 36).Value = "Sector";
                            wsCambios.Cell(1, 37).Value = "Caja";
                            wsCambios.Cell(1, 38).Value = "Puerto Externo";
                            wsCambios.Cell(1, 39).Value = "Spliter";
                            wsCambios.Cell(1, 40).Value = "Puerto Interno";
                            wsCambios.Cell(1, 41).Value = "Latitud";
                            wsCambios.Cell(1, 42).Value = "Longitud";
                            wsCambios.Cell(1, 43).Value = "Total cobrado";
                            // Formato a los encabezados (A1 a AQ1)
                            var headerCambios = wsCambios.Range("A1:AQ1");
                            headerCambios.Style.Font.Bold = true;
                            headerCambios.Style.Fill.BackgroundColor = XLColor.CornflowerBlue;
                            headerCambios.Style.Font.FontColor = XLColor.White;

                            int filaCambios = 2;
                            foreach (ListClientesDescargaModel item in Seleccionados)
                            {
                                wsCambios.Cell(filaCambios, 1).Value = item.IdCliente;      // 1: IdCliente
                                wsCambios.Cell(filaCambios, 2).Value = item.Cliente;         // 2: Cliente
                                wsCambios.Cell(filaCambios, 3).Value = item.IdUsuarioM;      // 3: IdServicio
                                wsCambios.Cell(filaCambios, 4).Value = item.Usuario;         // 4: Servicio
                                wsCambios.Cell(filaCambios, 5).Value = item.Mikrotik;        // 5: Mikrotik
                                wsCambios.Cell(filaCambios, 6).Value = DateTime.Now.Year;    // 6: Año
                                wsCambios.Cell(filaCambios, 7).Value = 0m;                   // 7: ENERO
                                wsCambios.Cell(filaCambios, 8).Value = 0m;                   // 8: FEBRERO
                                wsCambios.Cell(filaCambios, 9).Value = 0m;                   // 9: MARZO
                                wsCambios.Cell(filaCambios, 10).Value = 0m;                  // 10: ABRIL
                                wsCambios.Cell(filaCambios, 11).Value = 0m;                  // 11: MAYO
                                wsCambios.Cell(filaCambios, 12).Value = 0m;                  // 12: JUNIO
                                wsCambios.Cell(filaCambios, 13).Value = 0m;                  // 13: JULIO
                                wsCambios.Cell(filaCambios, 14).Value = 0m;                  // 14: AGOSTO
                                wsCambios.Cell(filaCambios, 15).Value = 0m;                  // 15: SEPTIEMBRE
                                wsCambios.Cell(filaCambios, 16).Value = 0m;                  // 16: OCTUBRE
                                wsCambios.Cell(filaCambios, 17).Value = 0m;                  // 17: NOVIEMBRE
                                wsCambios.Cell(filaCambios, 18).Value = 0m;                  // 18: DICIEMBRE
                                wsCambios.Cell(filaCambios, 19).Value = 15;                   // 19: Día de pago
                                wsCambios.Cell(filaCambios, 20).Value = item.PlanOriginal;        // 20: Plan Actual
                                wsCambios.Cell(filaCambios, 21).Value = DateTime.Now;         // 21: Fecha de suspension
                                wsCambios.Cell(filaCambios, 21).Style.DateFormat.Format = "dd/MM/yyyy";
                                wsCambios.Cell(filaCambios, 22).Value = "5550000000";         // 22: Télefono
                                wsCambios.Cell(filaCambios, 23).Value = "5550000000";         //23: Télefono2
                                wsCambios.Cell(filaCambios, 24).Value = 1;                    // 24: IdInstalador
                                wsCambios.Cell(filaCambios, 25).Value = "Instalador"; // 25: Instalador

                                wsCambios.Cell(filaCambios, 26).Value = DateTime.Now;         // 26: Fecha de instalación
                                wsCambios.Cell(filaCambios, 26).Style.DateFormat.Format = "dd/MM/yyyy";
                                wsCambios.Cell(filaCambios, 27).Value = string.Empty;
                                wsCambios.Cell(filaCambios, 28).Value = "Instalación";              // 27: Modalidad
                                wsCambios.Cell(filaCambios, 29).Value = string.Empty;           // 28: RB
                                wsCambios.Cell(filaCambios, 30).Value = string.Empty;       // 29: Antena
                                wsCambios.Cell(filaCambios, 31).Value = string.Empty;               // 30: Cable
                                wsCambios.Cell(filaCambios, 32).Value = string.Empty;       // 31: Router
                                wsCambios.Cell(filaCambios, 33).Value = string.Empty;       // 32: Cargador
                                wsCambios.Cell(filaCambios, 34).Value = string.Empty;         // 33: Tubo
                                wsCambios.Cell(filaCambios, 35).Value = item.IP;       // 34: IP
                                wsCambios.Cell(filaCambios, 36).Value = string.Empty;         // 35: Sector
                                wsCambios.Cell(filaCambios, 37).Value = string.Empty;             // 36: Caja
                                wsCambios.Cell(filaCambios, 38).Value = string.Empty;            // 37: Puerto Externo
                                wsCambios.Cell(filaCambios, 39).Value = string.Empty;        // 38: Spliter
                                wsCambios.Cell(filaCambios, 40).Value = string.Empty;        // 39: Puerto Interno
                                wsCambios.Cell(filaCambios, 41).Value = string.Empty;               // 40: Latitud
                                wsCambios.Cell(filaCambios, 42).Value = string.Empty;              // 41: Longitud
                                wsCambios.Cell(filaCambios, 43).Value = 0m;                   // 42: Total cobrado
                                filaCambios++;
                            }

                            // Autoajuste de columnas para Hoja 1
                            wsCambios.Columns().AdjustToContents();
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
            int filaCambios = 0;
            ListClientes = new List<UsuariosandPlanesModel>();
            ListMensualidades = new List<MensualidadModel>();
            ListHistorialPagos = new List<HistorialPagosModel>();
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 30;
            btnCargar.Enabled = false;
            BtnBuscar.Enabled = false;
            btnDescargar.Enabled = false;

            try
            {
                await Task.Run(() =>
                {
                    using (var workbook = new ClosedXML.Excel.XLWorkbook(rutaArchivo))
                    {
                        var wsCambios = workbook.Worksheet("Mensualidades");
                        bool primeraFila = true;
                        AppRepository obj = new AppRepository();

                        int IdMensualidad = 1;
                        foreach (var row in wsCambios.RowsUsed())
                        {
                            if (primeraFila)
                            {
                                primeraFila = false;
                                continue;
                            }

                            // Omite filas completamente vacías si existieran
                            if (row.IsEmpty()) continue;

                            filaCambios += 1;
                            int IdUsuarioM = Convert.ToInt32(row.Cell(3).GetValue<int>());
                            var DatosUsuarioM = obj.GetUsuariosMikrotiksById(IdUsuarioM).Result;
                            var DatosPlan = obj.GetPlanById(DatosUsuarioM.IdPlanOriginal).Result;
                            //var DatosResponsable = obj.GetUsuariosbyId(IdResponsable).Result;
                        
                            int Correr = 7;
                            int Anio = Convert.ToInt32(row.Cell(6).GetValue<int>());
                            int DiaCorte = Convert.ToInt32(row.Cell(19).GetValue<int>());
                            DateTime Instalacion = row.Cell(26).DataType == ClosedXML.Excel.XLDataType.DateTime
                            ? row.Cell(26).GetDateTime()
                            : Convert.ToDateTime(row.Cell(26).GetString().Trim());
                            var celdaSuspension = row.Cell(21);
                            string textoSuspension = celdaSuspension.GetFormattedString().Trim();
                            DateTime? Suspencion = null;

                            if (celdaSuspension.DataType == ClosedXML.Excel.XLDataType.DateTime)
                            {
                                Suspencion = celdaSuspension.GetDateTime();
                            }
                            else if (!string.IsNullOrWhiteSpace(textoSuspension) &&
                                     DateTime.TryParse(
                                         textoSuspension,
                                         System.Globalization.CultureInfo.CurrentCulture,
                                         System.Globalization.DateTimeStyles.None,
                                         out DateTime fechaSuspParseada))
                            {
                                Suspencion = fechaSuspParseada;
                            }
                            DateTime? fechaSusp = Suspencion?.Date;
                            for (int i = 1; i <= 12; i++)
                            {
                                if (fechaSusp.HasValue &&
                                (fechaSusp.Value.Year < Anio || (fechaSusp.Value.Year == Anio && fechaSusp.Value.Month < i)))
                                {
                                    break;
                                }
                                var celda = row.Cell(Correr);
                                string textoCelda = celda.GetFormattedString().Trim();
                                string textoMayus = textoCelda.ToUpperInvariant();
                                string comentario = celda.HasComment ? (celda.GetComment().Text ?? string.Empty).Trim() : string.Empty;

                                bool tieneComentarioEnCero = textoCelda == "0" && !string.IsNullOrWhiteSpace(comentario);
                                bool esMarcaEspecial = textoMayus == "OJO" || textoMayus == "P";
                                bool esNumeroDistintoDeCero = decimal.TryParse(
                                    textoCelda,
                                    System.Globalization.NumberStyles.Any,
                                    System.Globalization.CultureInfo.CurrentCulture,
                                    out decimal valorNumerico
                                ) && valorNumerico != 0m;
                                bool esMesDeSuspension = fechaSusp.HasValue &&
                            fechaSusp.Value.Year == Anio &&
                            fechaSusp.Value.Month == i;
                                if (!string.IsNullOrWhiteSpace(textoCelda) && (esNumeroDistintoDeCero || tieneComentarioEnCero || esMarcaEspecial))
                                {
                                    // Si es número válido usa ese número; si es "OJO", "P" o texto no numérico, queda en 0m
                                    decimal Pago = decimal.TryParse(
                                        textoCelda,
                                        System.Globalization.NumberStyles.Any,
                                        System.Globalization.CultureInfo.CurrentCulture,
                                        out decimal pagoParseado
                                    ) ? pagoParseado : 0m;
                                    DateTime fechaInicio = (Instalacion.Year == Anio && Instalacion.Month == i)
                                    ? Instalacion
                                    : ObtenerFechaCorteMes(Anio, i, DiaCorte);
                                    DateTime fechaLimite = fechaInicio.AddMonths(1);

                                    if (esMesDeSuspension)
                                    {
                                        fechaLimite = fechaSusp.Value;
                                    }
                                    else if (fechaSusp.HasValue && fechaLimite > fechaSusp.Value)
                                    {
                                        fechaLimite = fechaSusp.Value;
                                    }
                                    MensualidadModel LMensualidades = new MensualidadModel()
                                    {
                                        Id = IdMensualidad,
                                        DiaCorte = DiaCorte,
                                        FechaInicio = fechaInicio,
                                        FechaLimite = fechaLimite,
                                        IdUsuario = IdResponsable,
                                        IdUsuarioM = IdUsuarioM,
                                        Mensualidad = DatosPlan.Precio,
                                        Pagado = Pago <= DatosPlan.Precio ? true : false
                                    };
                                    ListMensualidades.Add(LMensualidades);
                                    HistorialPagosModel hp = new HistorialPagosModel()
                                    {
                                        Id = IdMensualidad,
                                        FechaRecibido = ObtenerFechaCorteMes(Anio, i, DiaCorte),
                                        Cantidad = Pago <= DatosPlan.Precio ? Pago:DatosPlan.Precio,
                                        Comentario = row.Cell(Correr).GetComment().Text,
                                        IdBanco = 1,
                                        Banco = "PAGOS EFECTIVO",
                                        Referencia = string.Empty,
                                        Imagen = null,
                                        IdMensualidad = IdMensualidad,
                                        IdUsuario = IdResponsable
                                    };
                                    ListHistorialPagos.Add(hp);
                                    IdMensualidad++;
                                }
                                Correr += 1;
                            }
                            var ultimaMensualidad = ListMensualidades
                                .Where(x => x.IdUsuarioM == IdUsuarioM)
                                .OrderByDescending(x => x.FechaInicio)
                                .FirstOrDefault();
                            UsuariosandPlanesModel cliente = new UsuariosandPlanesModel()
                            {
                                IdCliente = Convert.ToInt32(row.Cell(1).GetValue<int>()),
                                Cliente = row.Cell(2).GetString().Trim(),
                                IdUser = IdUsuarioM,
                                Usuario = row.Cell(4).GetString().Trim(),
                                IdPlan = DatosUsuarioM.IdPlanOriginal,
                                Plan = DatosPlan.Nombre,
                                Estatus = DatosUsuarioM.Estatus,
                                Mikrotik = row.Cell(5).GetString().Trim(),
                                Mensualidad = ultimaMensualidad.FechaInicio >= DateTime.Now
                                && ultimaMensualidad.Pagado == true ? "Pagado"
                                : "Pendiente"
                            };
                            ListClientes.Add(cliente);

                            ////var item = new RegistroMensualidad
                            ////{
                            ////    FechaSuspension = LeerFecha(row.Cell(21)),
                            ////    Telefono = LeerTexto(row.Cell(22)),
                            ////    IdInstalador = LeerInt(row.Cell(23)),
                            ////    Instalador = LeerTexto(row.Cell(24)),
                            ////    FechaInstalacionTexto = LeerTexto(row.Cell(25)),
                            ////    Modalidad = LeerTexto(row.Cell(26)),

                            ////    Rb = LeerTexto(row.Cell(27)),
                            ////    Antena = LeerTexto(row.Cell(28)),
                            ////    Cable = LeerTexto(row.Cell(29)),
                            ////    Router = LeerTexto(row.Cell(30)),
                            ////    Cargador = LeerTexto(row.Cell(31)),
                            ////    Tubo = LeerTexto(row.Cell(32)),
                            ////    Ip = LeerTexto(row.Cell(33)),
                            ////    Sector = LeerTexto(row.Cell(34)),
                            ////    Caja = LeerTexto(row.Cell(35)),
                            ////    PuertoExterno = LeerTexto(row.Cell(36)),
                            ////    Spliter = LeerTexto(row.Cell(37)),
                            ////    PuertoInterno = LeerTexto(row.Cell(38)),
                            ////    Latitud = LeerDoubleNullable(row.Cell(39)),
                            ////    Longitud = LeerDoubleNullable(row.Cell(40)),
                            ////    TotalCobrado = LeerDecimal(row.Cell(41))
                            ////};

                            ////registros.Add(item);
                        }
                    }
                });
                MessageBox.Show(
                    $"Excel cargado correctamente. Filas leídas: {filaCambios}",
                    "Información",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                CargarTablaClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al leer el Excel: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
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
        private static DateTime ObtenerFechaCorteMes(int anio, int mes, int diaCorte)
        {
            int ultimoDiaDelMes = DateTime.DaysInMonth(anio, mes);
            int diaAjustado = Math.Min(Math.Max(diaCorte, 1), ultimoDiaDelMes);
            return new DateTime(anio, mes, diaAjustado);
        }
        // Función auxiliar para calcular el costo por tramos exactos dentro de un período de mensualidad
        private async Task<decimal> CalcularCostoMensualidadPorTramosAsync(int idServicio, DateTime inicioPeriodo, DateTime finPeriodo, decimal precioBaseInicial, List<TiempoCambioModel> cambios, AppRepository obj)
        {
            decimal costoTotal = 0;
            DateTime cursor = inicioPeriodo;

            // Filtrar cambios aplicables que se cruzan con este periodo
            var cambiosEnPeriodo = cambios
                .Where(x => x.IdUsuarioM == idServicio && x.FechaFin >= inicioPeriodo && x.FechaInicio < finPeriodo)
                .OrderBy(x => x.FechaInicio)
                .ToList();

            // Si NO hay cambios en todo el periodo, es una mensualidad completa de un mes comercial (30 días equivalentes)
            if (cambiosEnPeriodo.Count == 0)
            {
                return RedondearMontoFinanciero(precioBaseInicial);
            }

            foreach (var cambio in cambiosEnPeriodo)
            {
                // Días comerciales antes del cambio dentro del periodo
                if (cursor < cambio.FechaInicio)
                {
                    int diasBase = (int)(cambio.FechaInicio - cursor).TotalDays;
                    costoTotal += diasBase * (precioBaseInicial / 30m);
                    cursor = cambio.FechaInicio;
                }

                // Días que dura el cambio con el precio del plan nuevo
                DateTime inicioCambioEfectivo = cursor > cambio.FechaInicio ? cursor : cambio.FechaInicio;
                DateTime finCambioEfectivo = finPeriodo < cambio.FechaFin.AddDays(1) ? finPeriodo : cambio.FechaFin.AddDays(1);

                if (inicioCambioEfectivo < finCambioEfectivo)
                {
                    int diasCambio = (int)(finCambioEfectivo - inicioCambioEfectivo).TotalDays;
                    var planNuevo = await obj.GetPlanById(cambio.IdPlan);
                    decimal precioPlanNuevo = planNuevo != null ? planNuevo.Precio : 0;

                    costoTotal += diasCambio * (precioPlanNuevo / 30m);
                    cursor = finCambioEfectivo;
                }

                // Actualizar el precio base posterior con el IdPlan original de este cambio
                var planOriginalDelCambio = await obj.GetPlanById(cambio.IdPlanOriginal);
                if (planOriginalDelCambio != null)
                {
                    precioBaseInicial = planOriginalDelCambio.Precio;
                }
            }

            // Días restantes del período después de todos los cambios
            if (cursor < finPeriodo)
            {
                int diasAcumuladosTramos = (int)(cursor - inicioPeriodo).TotalDays;

                // El mes comercial estricto es de 30 días. Los días restantes completan los 30 días exactos del ciclo comercial.
                int diasRestantes = 30 - diasAcumuladosTramos;

                if (diasRestantes < 0) diasRestantes = 0;

                // Si el periodo natural real es menor (por configuración o cierre), respetamos el remanente, 
                // pero evitamos que un mes de 28 o 31 días naturales altere el ciclo comercial estándar de 30 días.
                int diasNaturalesRestantes = (int)(finPeriodo - cursor).TotalDays;

                // Si estamos completando un ciclo mensual normal, nos atenemos a los días comerciales faltantes para sumar el mes exacto.
                if (diasAcumuladosTramos == 0 && diasNaturalesRestantes != 30)
                {
                    // Si por alguna razón el periodo natural difiere pero no hubo tramos cortados, 
                    // aseguramos que el mes completo se cobre íntegro (30 días comerciales).
                    diasRestantes = 30;
                }
                else if (diasNaturalesRestantes < diasRestantes && diasAcumuladosTramos > 0)
                {
                    diasRestantes = diasNaturalesRestantes;
                }

                costoTotal += diasRestantes * (precioBaseInicial / 30m);
            }

            // Aplicar tu función de redondeo financiero al resultado total del periodo
            return RedondearMontoFinanciero(costoTotal);
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
            if (txtCliente.Text.Trim() != string.Empty || txtUsuario.Text.Trim() != string.Empty)
                listaFinal = listaFinal.Where(x =>
                        (!string.IsNullOrWhiteSpace(txtCliente.Text.Trim()) &&
                         !string.IsNullOrEmpty(x.Cliente) &&
                         x.Cliente.IndexOf(txtCliente.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0)
                        ||
                        (!string.IsNullOrWhiteSpace(txtUsuario.Text.Trim()) &&
                         !string.IsNullOrEmpty(x.Usuario) &&
                         x.Usuario.IndexOf(txtUsuario.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0)
                    ).ToList();
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
                Name = "IdCliente",
                HeaderText = "IdCliente",
                DataPropertyName = "IdCliente",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cliente",
                HeaderText = "Cliente",
                DataPropertyName = "Cliente",
                ReadOnly = true,
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdUser",
                HeaderText = "IdUser",
                DataPropertyName = "IdUser",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Usuario",
                HeaderText = "Servicio",
                DataPropertyName = "Usuario",
                ReadOnly = true,
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdPlan",
                HeaderText = "IdPlan",
                DataPropertyName = "IdPlan",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Plan Actual",
                HeaderText = "Plan",
                DataPropertyName = "Plan",
                ReadOnly = true,
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                HeaderText = "Estatus del servicio",
                DataPropertyName = "Estatus",
                ReadOnly = true,
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Mikrotik",
                HeaderText = "Mikrotik",
                DataPropertyName = "Mikrotik",
                ReadOnly = true,
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Mensualidad",
                HeaderText = "Mensualidad",
                DataPropertyName = "Mensualidad",
                ReadOnly = true,
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DataGridViewButtonColumn btnMensualidad = new DataGridViewButtonColumn
            {
                Name = "btnMensualidad",
                HeaderText = "Acción",
                Text = "Mensualidad",
                UseColumnTextForButtonValue = true,
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
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
                                Responsable = "Administrador",
                                Comentario = item.Comentario,
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

                            // 1. Filtrar eventos (cambios temporales o suspensiones) que se cruzan con este período
                            var detalles = ListCambios.Where(x => x.IdUsuarioM == IdUsuarioMRevision
                                                                && x.FechaInicio <= hasta
                                                                && x.FechaFin >= desde
                                                                && x.Modo == "Temporal")
                                                     .OrderBy(x => x.FechaInicio)
                                                     .ToList();

                            ListDestalles = new List<ListDetallesMensualidadModel>();
                            AppRepository objRepo = new AppRepository();

                            // Obtener el plan base vigente al inicio de este período
                            var planBasePeriodo = await objRepo.GetPlanByIdUsuarioM(IdUsuarioMRevision);
                            int idPlanActual = planBasePeriodo != null ? planBasePeriodo.Id : 0;
                            string nombrePlanActual = planBasePeriodo != null ? planBasePeriodo.Nombre : "Plan Base";
                            decimal precioPlanActual = planBasePeriodo != null ? planBasePeriodo.Precio : 0m;

                            // Si hay un cambio previo que afecte el inicio, buscamos su plan original
                            var cambioAnterior = ListCambios
                                .Where(x => x.IdUsuarioM == IdUsuarioMRevision && x.FechaFin < desde)
                                .OrderByDescending(x => x.FechaFin)
                                .FirstOrDefault();

                            if (cambioAnterior != null)
                            {
                                var planAnt = await objRepo.GetPlanById(cambioAnterior.IdPlanOriginal);
                                if (planAnt != null)
                                {
                                    nombrePlanActual = planAnt.Nombre;
                                    precioPlanActual = planAnt.Precio;
                                }
                            }

                            DateTime cursor = desde;

                            // 2. Recorrer de forma continua desde la fecha 'desde' hasta la fecha 'hasta'
                            foreach (var cambio in detalles)
                            {
                                // Si hay un espacio libre antes de que empiece este cambio, pertenece al plan base actual
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
                                            Id = 0,
                                            FechaInicio = cursor,
                                            FechaFin = finTramoBase,
                                            Estatus = "Activo",
                                            Plan = nombrePlanActual,
                                            Costo = costoBase
                                        });
                                    }
                                    cursor = cambio.FechaInicio;
                                }

                                // Acotar el rango del cambio dentro de los límites del período de la mensualidad
                                DateTime inicioCambioEfectivo = cursor > cambio.FechaInicio ? cursor : cambio.FechaInicio;
                                DateTime finCambioEfectivo = hasta < cambio.FechaFin ? hasta : cambio.FechaFin;

                                if (inicioCambioEfectivo <= finCambioEfectivo)
                                {
                                    int diasCambio = (int)(finCambioEfectivo - inicioCambioEfectivo).TotalDays + 1;
                                    var planNuevo = await objRepo.GetPlanById(cambio.IdPlan);
                                    decimal precioPlanNuevo = planNuevo != null ? planNuevo.Precio : 0m;
                                    decimal costoCambio = RedondearMontoFinanciero(diasCambio * (precioPlanNuevo / 30.0m));

                                    ListDestalles.Add(new ListDetallesMensualidadModel
                                    {
                                        Id = cambio.Id,
                                        FechaInicio = inicioCambioEfectivo,
                                        FechaFin = finCambioEfectivo,
                                        Estatus = cambio.Estatus,
                                        Plan = cambio.Plan,
                                        Costo = costoCambio
                                    });

                                    cursor = finCambioEfectivo.AddDays(1);
                                }

                                // Actualizar el plan base posterior según el IdPlanOriginal de este cambio
                                var planOrigCambio = await objRepo.GetPlanById(cambio.IdPlanOriginal);
                                if (planOrigCambio != null)
                                {
                                    nombrePlanActual = planOrigCambio.Nombre;
                                    precioPlanActual = planOrigCambio.Precio;
                                }
                            }

                            // 3. Si queda tiempo después del último cambio hasta llegar a la fecha límite ('hasta')
                            if (cursor <= hasta)
                            {
                                DateTime fechaFinTramoFinal = hasta;

                                if (cursor <= fechaFinTramoFinal)
                                {
                                    // Validamos si abarca el mes completo sin interrupciones para aplicar el precio fijo exacto
                                    bool esMesCompleto = (cursor == desde && fechaFinTramoFinal == hasta && ListDestalles.Count == 0);

                                    decimal costoFinal;
                                    if (esMesCompleto)
                                    {
                                        // Respeta los 300 cerrados de la mensualidad sin alterarse por meses de 31 días
                                        costoFinal = precioPlanActual;
                                    }
                                    else
                                    {
                                        int diasFinales = (int)(fechaFinTramoFinal - cursor).TotalDays;
                                        if (diasFinales <= 0) diasFinales = 1;
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
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaInicio",
                HeaderText = "Empezó",
                DataPropertyName = "FechaInicio",
                ReadOnly = true,
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaFin",
                HeaderText = "Terminó",
                DataPropertyName = "FechaFin",
                ReadOnly = true,
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                HeaderText = "Estatus",
                DataPropertyName = "Estatus",
                ReadOnly = true,
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Plan",
                HeaderText = "Plan",
                DataPropertyName = "Plan",
                ReadOnly = true,
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic,
            });

            // Formato de Moneda ($MXN)
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Costo",
                HeaderText = "Costo",
                DataPropertyName = "Costo",
                ReadOnly = true,
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
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
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaRecibido",
                HeaderText = "Fecha en que se recibe",
                DataPropertyName = "FechaRecibido",
                ReadOnly = true,
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic,
                DefaultCellStyle = { Format = "dd/MM/yyyy hh:mm:ss tt" }
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cantidad",
                HeaderText = "Cantidad",
                DataPropertyName = "Cantidad",
                ReadOnly = true,
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
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
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Banco",
                HeaderText = "Banco",
                DataPropertyName = "Banco",
                ReadOnly = true,
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
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
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Comentario",
                HeaderText = "Comentario",
                DataPropertyName = "Comentario",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells,
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
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DiaCorte",
                HeaderText = "Día Corte",
                DataPropertyName = "DiaCorte",
                ReadOnly = true,
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaInicio",
                HeaderText = "Inicia Mes",
                DataPropertyName = "FechaInicio",
                ReadOnly = true,
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic,
                 DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaLimite",
                HeaderText = "Fecha Limite",
                DataPropertyName = "FechaLimite",
                ReadOnly = true,
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Responsable",
                HeaderText = "Responsable",
                DataPropertyName = "Responsable",
                ReadOnly = true,
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Mensualidad",
                HeaderText = "Mensualidad",
                DataPropertyName = "Mensualidad",
                ReadOnly = true,
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
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
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
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
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
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
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
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
                AutoSizeMode =DataGridViewAutoSizeColumnMode.DisplayedCells,
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
            DialogResult resultado = MessageBox.Show("Está confirmando que la información mostrada es la correcta ¿Quiere continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
            if (resultado == DialogResult.No)
            {
                return;
            }

            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 30;
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
                        using (var workbook = new XLWorkbook())
                        {
                            // ==============================================================
                            // HOJA 1: CAMBIOS Y SUSPENSIONES
                            // ==============================================================
                            var wsCambios = workbook.Worksheets.Add("Cambios");

                            wsCambios.Cell(1, 1).Value = "Descripción";
                            wsCambios.Cell(1, 2).Value = "Resultado";

                            var headerCambios = wsCambios.Range("A1:B1");
                            headerCambios.Style.Font.Bold = true;
                            headerCambios.Style.Fill.BackgroundColor = XLColor.CornflowerBlue;
                            headerCambios.Style.Font.FontColor = XLColor.White;

                            int filaCambios = 2;
                            foreach (var item in ListCambios)
                            {
                                ContadorCambios += 1;

                                // 1. Validar existencia del servicio
                                var existServicio = obj.GetUsuariosMikrotiksById(item.IdUsuarioM).Result;
                                if (existServicio == null || existServicio.Id == 0)
                                {
                                    wsCambios.Cell(filaCambios, 1).Value = "El servicio en cambios con id " + item.IdUsuarioM.ToString() + " no existe en el sistema";
                                    wsCambios.Cell(filaCambios, 2).Value = "Error";
                                    filaCambios++;
                                    continue;
                                }

                                // 2. Validar plan origen
                                var existPlanOriginal = obj.GetPlanById(item.IdPlanOriginal).Result;
                                if (existPlanOriginal == null || existPlanOriginal.Id == 0)
                                {
                                    wsCambios.Cell(filaCambios, 1).Value = "El plan origen con id " + item.IdPlanOriginal.ToString() + " no existe en el sistema";
                                    wsCambios.Cell(filaCambios, 2).Value = "Error";
                                    filaCambios++;
                                    continue;
                                }

                                // 3. Validar plan nuevo (si cambió)
                                if (item.IdPlanOriginal != item.IdPlan)
                                {
                                    var existPlanNuevo = obj.GetPlanById(item.IdPlan).Result;
                                    if (existPlanNuevo == null || existPlanNuevo.Id == 0)
                                    {
                                        wsCambios.Cell(filaCambios, 1).Value = "El plan nuevo con id " + item.IdPlan.ToString() + " no existe en el sistema";
                                        wsCambios.Cell(filaCambios, 2).Value = "Error";
                                        filaCambios++;
                                        continue;
                                    }
                                }

                                // 4. Validar Mikrotik origen
                                var existMikrotikOriginal = obj.GetMikrotikById(item.IdMikrotikOriginal).Result;
                                if (existMikrotikOriginal == null || existMikrotikOriginal.Id == 0)
                                {
                                    wsCambios.Cell(filaCambios, 1).Value = "El mikrotik origen con id " + item.IdMikrotikOriginal.ToString() + " no existe en el sistema";
                                    wsCambios.Cell(filaCambios, 2).Value = "Error";
                                    filaCambios++;
                                    continue;
                                }

                                // 5. Validar Mikrotik receptor (si cambió)
                                if (item.IdMikrotikOriginal != item.IdMikrotikReceptor)
                                {
                                    var existMikrotikReceptor = obj.GetMikrotikById(item.IdMikrotikReceptor).Result;
                                    if (existMikrotikReceptor == null || existMikrotikReceptor.Id == 0)
                                    {
                                        wsCambios.Cell(filaCambios, 1).Value = "El mikrotik receptor con id " + item.IdMikrotikReceptor.ToString() + " no existe en el sistema";
                                        wsCambios.Cell(filaCambios, 2).Value = "Error";
                                        filaCambios++;
                                        continue;
                                    }
                                }

                                // 6. Validar planes anidados
                                var anidadoNuevo = obj.GetPlanesAnidadosbyParametros(item.IdMikrotikReceptor, item.IdPlan).Result;
                                if (anidadoNuevo == null || anidadoNuevo.Id == 0)
                                {
                                    wsCambios.Cell(filaCambios, 1).Value = "No existe el plan nuevo: " + item.IdPlan.ToString() + " en el mikrotik receptor: " + item.IdMikrotikReceptor.ToString();
                                    wsCambios.Cell(filaCambios, 2).Value = "Error";
                                    filaCambios++;
                                    continue;
                                }

                                var anidadoOriginal = obj.GetPlanesAnidadosbyParametros(item.IdMikrotikOriginal, item.IdPlanOriginal).Result;
                                if (anidadoOriginal == null || anidadoOriginal.Id == 0)
                                {
                                    wsCambios.Cell(filaCambios, 1).Value = "No existe el plan original: " + item.IdPlanOriginal.ToString() + " en el mikrotik original: " + item.IdMikrotikOriginal.ToString();
                                    wsCambios.Cell(filaCambios, 2).Value = "Error";
                                    filaCambios++;
                                    continue;
                                }

                                // 7. Verificar si el cambio ya fue registrado (Permite acumular incrementos sin pisar lo anterior)
                                var exitCambiot = obj.GetTiempoCambio(item.IdUsuarioM, item.FechaInicio, item.FechaFin).Result;
                                if (exitCambiot == null || exitCambiot.Count() == 0)
                                {
                                    ListCambios[ContadorCambios].Id = 0;

                                    var resultcambio = obj.SaveTiempoCambio(ListCambios[ContadorCambios]).Result;
                                    if (resultcambio)
                                    {
                                        wsCambios.Cell(filaCambios, 1).Value = "Se guardó correctamente el cambio en el sistema para el servicio " + item.IdUsuarioM + " con fecha de inicio " + item.FechaInicio.ToString();
                                        wsCambios.Cell(filaCambios, 2).Value = "Satisfactorio";
                                    }
                                    else
                                    {
                                        wsCambios.Cell(filaCambios, 1).Value = "Error al guardar el cambio en el sistema para el servicio " + item.IdUsuarioM + " con fecha de inicio " + item.FechaInicio.ToString();
                                        wsCambios.Cell(filaCambios, 2).Value = "Error";
                                    }
                                    filaCambios++;
                                }
                                else
                                {
                                    wsCambios.Cell(filaCambios, 1).Value = "El servicio " + item.IdUsuarioM + " ya cuenta con " +
                                        " un cambio de la fecha "+
                                        item.FechaInicio.ToString() + " hasta la fecha" + item.FechaFin.ToString() +
                                        " registrado, para ajustar debe de ir a la sección de cambios";
                                    wsCambios.Cell(filaCambios, 2).Value = "Error";
                                    filaCambios++;
                                }
                            }
                            wsCambios.Columns().AdjustToContents();

                            // ==============================================================
                            // HOJA 2: PAGOS Y MENSUALIDADES
                            // ==============================================================
                            var wPagos = workbook.Worksheets.Add("Pagos");

                            wPagos.Cell(1, 1).Value = "Descripción";
                            wPagos.Cell(1, 2).Value = "Resultado";

                            // CORRECCIÓN: Aplicar estilos a la hoja de Pagos y no a la de cambios
                            var headerPagos = wPagos.Range("A1:B1");
                            headerPagos.Style.Font.Bold = true;
                            headerPagos.Style.Fill.BackgroundColor = XLColor.CornflowerBlue;
                            headerPagos.Style.Font.FontColor = XLColor.White;

                            int ContadorPagos = -1;
                            int filaPagos = 2;
                            int ContadorFolios = 0;
                            int Folio = 0;
                            DateTime FechaFolio = DateTime.Now;
                            foreach (var item in ListMensualidades)
                            {
                                ContadorPagos += 1;
                                var existServicio = obj.GetUsuariosMikrotiksById(item.IdUsuarioM).Result;
                                if (existServicio == null || existServicio.Id == 0)
                                {
                                    wPagos.Cell(filaPagos, 1).Value = "El servicio en pagos con id " + item.IdUsuarioM.ToString() + " no existe en el sistema";
                                    wPagos.Cell(filaPagos, 2).Value = "Error";
                                    filaPagos++;
                                    continue;
                                }

                                var exitMensualidad = obj.GetMensualidadbyIdUsuarioM(item.IdUsuarioM).Result;
                                if (exitMensualidad == null || exitMensualidad.Count() == 0)
                                {
                                    int idMensualidadOriginalLocal = ListMensualidades[ContadorPagos].Id;
                                    ListMensualidades[ContadorPagos].Id = 0;

                                    var resultMensualidad = obj.SaveMensualidad(ListMensualidades[ContadorPagos]).Result;
                                    if (resultMensualidad != 0)
                                    {
                                        wPagos.Cell(filaPagos, 1).Value = "Se guardó correctamente la mensualidad con fecha " + item.FechaInicio.ToString() + " en el sistema para el servicio " + item.IdUsuarioM;
                                        wPagos.Cell(filaPagos, 2).Value = "Satisfactorio";
                                        filaPagos++;

                                        // Guardar pagos asociados a esta nueva mensualidad
                                        var Pagos = ListHistorialPagos.Where(x => x.IdMensualidad == idMensualidadOriginalLocal).ToList();
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
                                                if(ContadorFolios == 0 || ContadorFolios == 6
                                                    || FechaFolio != HP.FechaRecibido)
                                                {
                                                    ContadorFolios = 0;
                                                    FechaFolio = HP.FechaRecibido;
                                                    Folio = obj.GetUltimoFolio("Mensualidades").Result.Folio + 1;
                                                }
                                                var r = obj.UpdateFoliobyIdHistorial(rhp, "Mensualidades", Folio).Result;
                                                ContadorFolios++;
                                                wPagos.Cell(filaPagos, 1).Value = "Se guardó correctamente el pago con fecha " + itempagos.FechaRecibido.ToString() + " de la mensualidad con fecha " + item.FechaInicio.ToString() + " en el sistema";
                                                wPagos.Cell(filaPagos, 2).Value = "Satisfactorio";
                                                filaPagos++;
                                            }
                                            else
                                            {
                                                wPagos.Cell(filaPagos, 1).Value = "Error al guardar el pago con fecha " + itempagos.FechaRecibido.ToString() + " de la mensualidad con fecha " + item.FechaInicio.ToString() + " en el sistema";
                                                wPagos.Cell(filaPagos, 2).Value = "Error";
                                                filaPagos++;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        wPagos.Cell(filaPagos, 1).Value = "Error al intentar guardar la mensualidad con fecha " + item.FechaInicio.ToString() + " en el sistema para el servicio " + item.IdUsuarioM;
                                        wPagos.Cell(filaPagos, 2).Value = "Error";
                                        filaPagos++;
                                    }
                                }
                                else
                                {
                                    // La mensualidad ya existe, pero evaluamos si le incrementaron pagos nuevos (comportamiento incremental sin sobreescribir)
                                    wPagos.Cell(filaPagos, 1).Value = "Ya existe la mensualidad registradas en el sistema para el servicio " + item.IdUsuarioM;
                                    wPagos.Cell(filaPagos, 2).Value = "Error";
                                    filaPagos++;
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

        private void cbTodos_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = cbTodos.Checked;

            DGVClientes.EndEdit();
            DGVClientes.SuspendLayout();

            // Guardamos el modo actual de autoajuste de columnas y lo desactivamos temporalmente
            var modoAnterior = DGVClientes.AutoSizeColumnsMode;
            DGVClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            foreach (DataGridViewRow row in DGVClientes.Rows)
            {
                if (!row.IsNewRow)
                {
                    row.Cells["chkSeleccionar"].Value = isChecked;
                }
            }

            // Restauramos el modo y forzamos un solo redibujado final
            DGVClientes.AutoSizeColumnsMode = modoAnterior;
            DGVClientes.ResumeLayout();
            DGVClientes.Refresh();
        }

        private void CBMikrotiks_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGVClientes.Columns.Clear();
        }

        private void RBMikrotiks_CheckedChanged(object sender, EventArgs e)
        {
            RBCajas.Checked = false;
            RBMensualidades.Checked = false;
        }

        private void RBMensualidades_CheckedChanged(object sender, EventArgs e)
        {
            RBCajas.Checked = false;
            RBMikrotiks.Checked = false;
        }

        private void RBCajas_CheckedChanged(object sender, EventArgs e)
        {
            RBMikrotiks.Checked = false;
            RBMensualidades.Checked = false;
        }
    }
}
