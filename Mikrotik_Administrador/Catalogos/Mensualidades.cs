using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Items;
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

namespace Mikrotik_Administrador.Catalogos
{
    public partial class Mensualidades : Form
    {
        public int IdResponsable { get; set; }
        public int IdUsuarioM { get; set; }
        public string Cliente { get; set; }
        public string UsuarioM { get; set; }
        public Mensualidades()
        {
            InitializeComponent();
        }

        private void Mensualidades_Load(object sender, EventArgs e)
        {
            cbTipo.SelectedIndex = 2;
            Buscar();
        }
        public async void Buscar()
        {
            if (cbTipo.SelectedIndex == 0)
            {
                MessageBox.Show("Seleccione un tipo de menualidad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnBuscar.Enabled = false;
            CrearGridView();
            AppRepository obj = new AppRepository();
            try
            {
                bool Pagado = cbTipo.SelectedIndex == 1 ? true : false;
                var Mensualidades = await obj.GetMensualidades(IdUsuarioM, Pagado);
                var listaFinal = Mensualidades?.ToList() ?? new List<ListMensualidadesModel>();
                dgvMensualidades.DataSource = new SortableBindingList<ListMensualidadesModel>(listaFinal);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnBuscar.Enabled = true;
            }
        }
        public void CrearGridView()
        {
            dgvMensualidades.Columns.Clear();
            dgvMensualidades.AutoGenerateColumns = false;
            dgvMensualidades.EnableHeadersVisualStyles = false;
            // --- ESTILO DE LOS TÍTULOS (HEADERS) CON TU AZUL LOGO ---
            dgvMensualidades.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            dgvMensualidades.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvMensualidades.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            dgvMensualidades.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dgvMensualidades.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            dgvMensualidades.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);


            dgvMensualidades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvMensualidades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DiaCorte",
                HeaderText = "Día Corte",
                DataPropertyName = "DiaCorte",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dgvMensualidades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaInicio",
                HeaderText = "Inicia Mes",
                DataPropertyName = "FechaInicio",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvMensualidades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaLimite",
                HeaderText = "Fecha Limite",
                DataPropertyName = "FechaLimite",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvMensualidades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Responsable",
                HeaderText = "Responsable",
                DataPropertyName = "Responsable",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvMensualidades.Columns.Add(new DataGridViewTextBoxColumn
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
            dgvMensualidades.Columns.Add(new DataGridViewTextBoxColumn
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
            dgvMensualidades.Columns.Add(new DataGridViewTextBoxColumn
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
            DataGridViewButtonColumn btnModificar = new DataGridViewButtonColumn
            {
                Name = "btnModificar",
                HeaderText = "Acción",
                Text = "Modificar",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            dgvMensualidades.Columns.Add(btnModificar);
            DataGridViewButtonColumn btnPagar = new DataGridViewButtonColumn
            {
                Name = "btnPagar",
                HeaderText = "Acción",
                Text = "Pagar",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            dgvMensualidades.Columns.Add(btnPagar);
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
            dgvMensualidades.Columns.Add(btnHistorial);
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
            dgvMensualidades.Columns.Add(btnVerDetalles);
            dgvMensualidades.AllowUserToAddRows = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void dgvMensualidades_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                dgvMensualidades.Enabled = false;
                AppRepository r = new AppRepository();
                switch (dgvMensualidades.Columns[e.ColumnIndex].Name)
                {
                    case "btnModificar":
                        IniciarPagos ini = new IniciarPagos();
                        ini.IdMensualidad = (int)dgvMensualidades.Rows[e.RowIndex].Cells["Id"].Value;
                        ini.IdUsuarioM = IdUsuarioM;
                        ini.IdResponsable = IdResponsable;
                        ini.ShowDialog();
                        break;
                    case "btnVerDetalles":
                        DetallesMensualidad detalles = new DetallesMensualidad();
                        detalles.IdUsuarioM = IdUsuarioM;
                        detalles.Desde = (DateTime)dgvMensualidades.Rows[e.RowIndex].Cells["FechaInicio"].Value;
                        detalles.Hasta = (DateTime)dgvMensualidades.Rows[e.RowIndex].Cells["FechaLimite"].Value;
                        detalles.ShowDialog();
                        break;
                    case "btnHistorial":
                        HistorialPagos Hi = new HistorialPagos();
                        Hi.IdMensualidad = (int)dgvMensualidades.Rows[e.RowIndex].Cells["Id"].Value;
                        Hi.Faltante = (decimal)dgvMensualidades.Rows[e.RowIndex].Cells["Faltante"].Value;
                        Hi.Mensualidad = (decimal)dgvMensualidades.Rows[e.RowIndex].Cells["Mensualidad"].Value;
                        Hi.IdResponsable = IdResponsable;
                        Hi.Cliente = Cliente;
                        Hi.UsuarioM = UsuarioM;                     
                        Hi.ShowDialog();
                        break;
                     case "btnPagar":
                        if((decimal)dgvMensualidades.Rows[e.RowIndex].Cells["Faltante"].Value == 0)
                        {
                            MessageBox.Show("Esta mensualidad se encuentra completamente pagada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        Pagar iniP = new Pagar();
                        iniP.Id = 0;
                        iniP.IdMensualidad = (int)dgvMensualidades.Rows[e.RowIndex].Cells["Id"].Value;
                        iniP.Mensualidad = (decimal)dgvMensualidades.Rows[e.RowIndex].Cells["Mensualidad"].Value;
                        iniP.IdResponsable = IdResponsable;
                        iniP.Cliente = Cliente;
                        iniP.UsuarioM = UsuarioM;
                        iniP.Faltante = (decimal)dgvMensualidades.Rows[e.RowIndex].Cells["Faltante"].Value;
                        iniP.ShowDialog();
                        Buscar();
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
                dgvMensualidades.Enabled = true;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            IniciarPagos ini = new IniciarPagos();
            ini.IdMensualidad = 0;
            ini.IdUsuarioM = IdUsuarioM;
            ini.IdResponsable = IdResponsable;
            if (ini.ShowDialog() != DialogResult.OK)
            { return; }
        }
    }
}
