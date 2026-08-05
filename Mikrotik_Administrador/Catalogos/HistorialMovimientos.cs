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
    public partial class HistorialMovimientos : Form
    {
        public bool Urgente = false;
        public HistorialMovimientos()
        {
            InitializeComponent();
        }
        public void CrearGridView()
        {
            dgvHistorial.Columns.Clear();
            dgvHistorial.AutoGenerateColumns = false;
            dgvHistorial.EnableHeadersVisualStyles = false;

            // --- ALTO FIJO Y SALTO DE LÍNEA A 2 FILAS DE TEXTO ---
            // Alto fijo de 52px para 2 líneas de texto sin costo de procesamiento en la UI
            dgvHistorial.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvHistorial.RowTemplate.Height = 52;
            dgvHistorial.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // --- ESTILO DE LOS TÍTULOS (HEADERS) ---
            dgvHistorial.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            dgvHistorial.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvHistorial.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            dgvHistorial.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dgvHistorial.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            dgvHistorial.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);

            // --- COLUMNAS CON ANCHO DEFINIDO (OPTIMIZADAS) ---
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Visible = false,
                ReadOnly = true
            });

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Usuario",
                HeaderText = "Responsable",
                DataPropertyName = "Usuario",
                ReadOnly = true,
                Width = 130,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Pagina",
                HeaderText = "Página",
                DataPropertyName = "Pagina",
                ReadOnly = true,
                Width = 120,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaCreacion",
                HeaderText = "Fecha",
                DataPropertyName = "FechaCreacion",
                ReadOnly = true,
                Width = 140,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descripcion",
                HeaderText = "Descripción",
                DataPropertyName = "Descripcion",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, // Se expande en el espacio sobrante
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                HeaderText = "Estatus",
                DataPropertyName = "Estatus",
                ReadOnly = true,
                Width = 100,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            DataGridViewButtonColumn btnCambiar = new DataGridViewButtonColumn
            {
                Name = "btnCambiar",
                HeaderText = "Acción",
                Text = "Cambiar Estatus",
                UseColumnTextForButtonValue = true,
                Width = 130,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            dgvHistorial.Columns.Add(btnCambiar);

            dgvHistorial.AllowUserToAddRows = false;
        }

        private  void btnBuscar_Click(object sender, EventArgs e)
        {
            Urgente = false;
            Buscar();
        }
        public async void Buscar()
        {
            CrearGridView();
            btnBuscar.Enabled = false;
            try
            {
                progressBar1.Style = ProgressBarStyle.Marquee;
                progressBar1.MarqueeAnimationSpeed = 30;
                AppRepository obj = new AppRepository();
                if(Urgente == false)
                {
                    var lista = await Task.Run(() => obj.GetHistorialMovimientos(dtpFechaInicio.Value, dtpFechaFinal.Value));
                    var listaFinal = lista?.ToList() ?? new List<ListHistorialMovimientosModel>();
                    dgvHistorial.DataSource = new SortableBindingList<ListHistorialMovimientosModel>(listaFinal);
                }
                else
                {
                    var lista = await Task.Run(() => obj.GetHistorialMovimientosUrgentes());
                    var listaFinal = lista?.ToList() ?? new List<ListHistorialMovimientosModel>();
                    dgvHistorial.DataSource = new SortableBindingList<ListHistorialMovimientosModel>(listaFinal);
                }
                if (dgvHistorial.Columns["Id"] != null)
                    dgvHistorial.Columns["Id"].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 0;
                btnBuscar.Enabled = true;
            }
        }

        private void dgvHistorial_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var Id = (int)dgvHistorial.Rows[e.RowIndex].Cells["Id"].Value;
            AppRepository m = new AppRepository();
            switch (dgvHistorial.Columns[e.ColumnIndex].Name)
            {
                case "btnCambiar":

                    bool result = m.UpdateEstatusHistorialMovimiento(Id).Result;
                    if (result == true)
                    {
                        MessageBox.Show("Estatus cambiado");
                        Buscar();
                    }
                    else
                        MessageBox.Show("Error al desactivar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    break;
            }
        }

        private void btnUrgentes_Click(object sender, EventArgs e)
        {
            Urgente = true;
            Buscar();
        }
    }
}
