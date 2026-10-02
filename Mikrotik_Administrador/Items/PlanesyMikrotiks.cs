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
    public partial class PlanesyMikrotiks : Form
    {
        public int IdMikrotik { get; set; } = 0;
        public int IdPlan { get; set; } = 0;
        public string VelocidadElegida { get; set; } = string.Empty;
        public string NombrePlanElegido { get; set; } = string.Empty;
        public PlanesyMikrotiks()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtNombrePlan.Text.Trim() == "")
            {
                DialogResult resultado = MessageBox.Show("Ha dejado el campo vacio, esto buscara a todos los planes pero puede demorar ¿Quiere continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.No)
                {
                    return;
                }
            }
            BuscarPlanes();
        }
        public void BuscarPlanes()
        {
            CrearGridView();
            btnBuscar.Enabled = false;
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            btnBuscar.Enabled = false;
            try
            {
                AppRepository obj = new AppRepository();
                var lista = obj.GetPlanesbyName(txtNombrePlan.Text, null, true).Result;
                var listaFinal = lista?.ToList() ?? new List<ListPlanesModel>();
                dgvPlanes.DataSource = new SortableBindingList<ListPlanesModel>(listaFinal);
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
        public void CrearGridView()
        {
            dgvPlanes.Columns.Clear();
            dgvPlanes.AutoGenerateColumns = false;
            dgvPlanes.EnableHeadersVisualStyles = false;
            // --- ESTILO DE LOS TÍTULOS (HEADERS) CON TU AZUL LOGO ---
            dgvPlanes.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            dgvPlanes.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvPlanes.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            dgvPlanes.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dgvPlanes.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            dgvPlanes.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);

            dgvPlanes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvPlanes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvPlanes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Precio",
                HeaderText = "Precio",
                DataPropertyName = "Precio",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvPlanes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Velocidad",
                HeaderText = "Velocidad",
                DataPropertyName = "Velocidad",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvPlanes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PlanDe",
                HeaderText = "Tipo de plan",
                DataPropertyName = "PlanDe",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DataGridViewButtonColumn btnAsignar = new DataGridViewButtonColumn
            {
                Name = "btnAsignar",
                HeaderText = "Acción",
                Text = "Asignar",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            dgvPlanes.Columns.Add(btnAsignar);
            dgvPlanes.AllowUserToAddRows = false;
        }

        private async void dgvPlanes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Evitar errores si hacen click en el encabezado
            if (e.RowIndex < 0) return;
            int Id = (int)dgvPlanes.Rows[e.RowIndex].Cells["Id"].Value;
            string Plan = (string)dgvPlanes.Rows[e.RowIndex].Cells["Nombre"].Value;
            string Velocidad = (string)dgvPlanes.Rows[e.RowIndex].Cells["Velocidad"].Value;
            bool IsAntena = (bool)dgvPlanes.Rows[e.RowIndex].Cells["PlanDe"].Value.ToString().Contains("Antena");
            decimal Precio = (decimal)dgvPlanes.Rows[e.RowIndex].Cells["Precio"].Value;
            switch (dgvPlanes.Columns[e.ColumnIndex].Name)
            {
                case "btnAsignar":
                    if (Plan.Trim() == string.Empty)
                    {
                        MessageBox.Show("Solo se pueden asignar planes con nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    IdPlan = Id;
                    VelocidadElegida = Velocidad;
                    NombrePlanElegido = Plan;
                    AppRepository obj = new AppRepository();
                    var listaMikrotiks = await obj.GetMikrotiksByIdPlan(Id);
                    // Insertamos un objeto "fantasma" al inicio para el placeholder
                    listaMikrotiks.Insert(0, new ListMikrotikModel { Id = 0, Nombre = "Selecciona un Mikrotik" });

                    // Configuramos el ComboBox
                    CBMikrotiks.DisplayMember = "Nombre"; // Lo que el usuario VE
                    CBMikrotiks.ValueMember = "Id";      // El dato que procesas por DETRÁS
                    CBMikrotiks.DataSource = listaMikrotiks;
                    CBMikrotiks.SelectedIndex = 0;

                    foreach (DataGridViewRow r in dgvPlanes.Rows)
                    {
                        r.DefaultCellStyle.BackColor = Color.Empty; // Restablece al estilo global/predeterminado
                    }

                    // 2. Colorear la fila seleccionada
                    dgvPlanes.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.PaleGreen;

                    // 3. Opcional: Desmarcar la selección azul por defecto para que el verde se aprecie de inmediato
                    dgvPlanes.ClearSelection();
                    break;

            }

        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if(CBMikrotiks.SelectedIndex == 0)
            {
                MessageBox.Show("Por favor, selecciona un Mikrotik antes de continuar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            IdMikrotik = (int)CBMikrotiks.SelectedValue;
            this.Close();
        }

        private void PlanesyMikrotiks_Load(object sender, EventArgs e)
        {

        }
    }
}
