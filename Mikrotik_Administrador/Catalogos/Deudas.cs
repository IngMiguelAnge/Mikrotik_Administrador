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
    public partial class Deudas : Form
    {
        public int IdDeudor { get; set; }
        public int IdResponsable { get; set; }
        public string Cliente { get; set; }
        public Deudas()
        {
            InitializeComponent();
        }

        private void Deudas_Load(object sender, EventArgs e)
        {
            Buscar();
        }
        public async void Buscar()
        {
            CrearGridView();
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            btnNuevo.Enabled = false;
            btnPagar.Enabled = false;
            try
            {
                AppRepository obj = new AppRepository();
                var lista = await Task.Run(() => obj.GetDeudasbyIdDeudor(IdDeudor));
                var listaFinal = lista?.ToList() ?? new List<ListDeudasModel>();
                DGVDeudas.DataSource = new SortableBindingList<ListDeudasModel>(listaFinal);
                if (DGVDeudas.Columns["Id"] != null)
                    DGVDeudas.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 0;
                btnPagar.Enabled = true;
                btnNuevo.Enabled = true;
            }
        }
        public void CrearGridView()
        {
            DGVDeudas.Columns.Clear();
            DGVDeudas.AutoGenerateColumns = false;
            DGVDeudas.EnableHeadersVisualStyles = false;
            // --- ESTILO DE LOS TÍTULOS (HEADERS) CON TU AZUL LOGO ---
            DGVDeudas.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            DGVDeudas.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            DGVDeudas.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            DGVDeudas.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            DGVDeudas.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            DGVDeudas.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);

            DGVDeudas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVDeudas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Year",
                HeaderText = "Año",
                DataPropertyName = "Year",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVDeudas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Mes",
                HeaderText = "Mes",
                DataPropertyName = "Mes",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVDeudas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cantidad",
                HeaderText = "Cantidad",
                DataPropertyName = "Cantidad",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2", // Aplica formato de moneda local (ej: $120.00 o $120.50)
                    FormatProvider = new System.Globalization.CultureInfo("es-MX") // Forzado a pesos mexicanos
                }
            });
            DGVDeudas.Columns.Add(new DataGridViewTextBoxColumn
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
           
            DGVDeudas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                HeaderText = "Estatus",
                DataPropertyName = "Estatus",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn
            {
                Name = "btnEditar",
                HeaderText = "Acción",
                Text = "Editar",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            DGVDeudas.Columns.Add(btnEditar);
            DataGridViewButtonColumn btnCambiarEstatus = new DataGridViewButtonColumn
            {
                Name = "btnCambiarEstatus",
                HeaderText = "Acción",
                Text = "Cambiar Estatus",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            DGVDeudas.Columns.Add(btnCambiarEstatus);
            DataGridViewButtonColumn btnHistorial = new DataGridViewButtonColumn
            {
                Name = "btnHistorial",
                HeaderText = "Acción",
                Text = "Historial",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            DGVDeudas.Columns.Add(btnHistorial);
            DGVDeudas.AllowUserToAddRows = false;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Deuda d = new Deuda();
            d.Id = 0;
            d.IdDeudor = IdDeudor;
            if (d.ShowDialog() == DialogResult.OK)
            {
                Buscar();
            }
        }

        private void DGVDeudas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int Id = (int)DGVDeudas.Rows[e.RowIndex].Cells["Id"].Value;
            try
            {
                DGVDeudas.Enabled = false;
                switch (DGVDeudas.Columns[e.ColumnIndex].Name)
                {
                    case "btnHistorial":
                        HistorialPagosDeudores h = new HistorialPagosDeudores();
                        h.IdResponsable = IdResponsable;
                        h.IdDeudor = IdDeudor;
                        h.IdDeuda = Id;
                        h.Cliente = Cliente;
                        h.Faltante = (decimal)DGVDeudas.Rows[e.RowIndex].Cells["Faltante"].Value;
                        h.ShowDialog();
                        break;
                    case "btnEditar":
                        Deuda d = new Deuda();
                        d.Id = Id;
                        d.IdDeudor = IdDeudor;
                        if (d.ShowDialog() == DialogResult.OK)
                        {
                            Buscar();
                        }
                        break;
                    case "btnCambiarEstatus":
                        AppRepository r = new AppRepository();
                        var resultD = r.UpdateEstatusDeuda(Id).Result;
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
                DGVDeudas.Enabled = true;
            }
        }
    }
}
