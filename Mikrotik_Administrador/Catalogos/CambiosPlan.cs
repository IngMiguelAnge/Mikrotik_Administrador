using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using Mikrotik_Administrador.Settings;
using Renci.SshNet;
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

namespace Mikrotik_Administrador.Catalogos
{
    public partial class CambiosPlan : Form
    {
        public CambiosPlan()
        {
            InitializeComponent();
        }

        private void CambiosPlan_Load(object sender, EventArgs e)
        {
        }
        public void CrearGridView()
        {
            DGVCambios.Columns.Clear();
            DGVCambios.AutoGenerateColumns = false;
            DGVCambios.EnableHeadersVisualStyles = false;

            // --- AJUSTES PARA SALTO DE LÍNEA Y ALTO AUTOMÁTICO ---
            DGVCambios.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            DGVCambios.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            // --- ESPACIADO E INTERLINEADO (EVITA QUE SE VEA AMONTONADO) ---
            // Agrega 6px arriba/abajo y 8px a los lados de margen interno en cada celda
            DGVCambios.DefaultCellStyle.Padding = new Padding(8, 6, 8, 6);
            DGVCambios.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // --- ESTILO DE LOS TÍTULOS (HEADERS) ---
            DGVCambios.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            DGVCambios.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            DGVCambios.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            DGVCambios.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            DGVCambios.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            DGVCambios.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            DGVCambios.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            estiloBotones.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Visible = false,
                ReadOnly = true
            });

            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Usuario",
                HeaderText = "Usuario ha afectar",
                DataPropertyName = "Usuario",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Programacion",
                HeaderText = "Se realizara",
                DataPropertyName = "Programacion",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });


            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Dias",
                HeaderText = "Días que durará",
                DataPropertyName = "Dias",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Horas",
                HeaderText = "Horas que durará",
                DataPropertyName = "Horas",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaInicio",
                HeaderText = "Fecha que se iniciara",
                DataPropertyName = "FechaInicio",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaFin",
                HeaderText = "Fecha que terminara",
                DataPropertyName = "FechaFin",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Modo",
                HeaderText = "Modo",
                DataPropertyName = "Modo",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdUsuarioM",
                HeaderText = "IdUsuarioM",
                DataPropertyName = "IdUsuarioM",
                ReadOnly = true,
                Visible = false
            });

            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdPlan",
                HeaderText = "IdPlan",
                DataPropertyName = "IdPlan",
                ReadOnly = true,
                Visible = false // Oculta este ID si no es necesario mostrarlo en pantalla
            });

            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PlanNuevo",
                HeaderText = "Se cambiara por el plan",
                DataPropertyName = "PlanNuevo",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                HeaderText = "Estatus",
                DataPropertyName = "Estatus",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            // --- COLUMNA NOTA OPTIMIZADA ---
            DGVCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nota",
                HeaderText = "Nota",
                DataPropertyName = "Nota",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 250, // Garantiza un espacio horizontal mínimo holgado
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            DataGridViewButtonColumn btnCancelar = new DataGridViewButtonColumn
            {
                Name = "btnCancelar",
                HeaderText = "Acción",
                Text = "Cancelar",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };

            DGVCambios.Columns.Add(btnCancelar);
            DGVCambios.AllowUserToAddRows = false;
        }
        public void Buscar()
        {
            CrearGridView();
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación

            try
            {
                AppRepository obj = new AppRepository();
                var lista = obj.GetTiempoCambio(dtpFechaInicio.Value, dtpFechaFinal.Value).Result;
                var listaFinal = lista?.ToList() ?? new List<ListTiempoCambioModel>();
                DGVCambios.DataSource = new SortableBindingList<ListTiempoCambioModel>(listaFinal);
                if (DGVCambios.Columns["Id"] != null)
                {
                    DGVCambios.Columns["Id"].Visible = false;
                }
                if (DGVCambios.Columns["IdPlan"] != null)
                {
                    DGVCambios.Columns["IdPlan"].Visible = false;
                }
                if (DGVCambios.Columns["IdUsuarioM"] != null)
                {
                    DGVCambios.Columns["IdUsuarioM"].Visible = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 0;
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private async void DGVCambios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var Id = DGVCambios.Rows[e.RowIndex].Cells["Id"].Value;

            switch (DGVCambios.Columns[e.ColumnIndex].Name)
            {
                case "btnCancelar":
                    var Estatus = DGVCambios.Rows[e.RowIndex].Cells["Estatus"].Value.ToString();
                    if(Estatus == "Cancelado")
                    {
                        MessageBox.Show("El cambio de plan ya fue cancelado previamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (Estatus == "Completado")
                    {
                        MessageBox.Show("El cambio de plan ya termino, no se puede cancelar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    AppRepository obj = new AppRepository();
                    await obj.UpdateEstatusTiempoCambio(Convert.ToInt32(Id));
                    MessageBox.Show("Se solicito terminar la programación favor de esperar unos segundos.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Buscar();
                    break;      
            }
        }
    }
}
