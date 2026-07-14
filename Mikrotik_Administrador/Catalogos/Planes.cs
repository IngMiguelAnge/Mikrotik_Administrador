using Mikrotik_Administrador.Catalogos;
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

namespace Mikrotik_Administrador
{
    public partial class Planes : Form
    {
        public bool PorUsuarios { get; set; }
        public int IdMikrotik { get; set; }
        public string Tipo { get; set; }
        public int IdSeleccionado { get; set; } 
        public Planes()
        {
            InitializeComponent();
        }
        public void BuscarPlanes() 
        {
            CrearGridView();
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            btnBuscar.Enabled = false;
            try
            {
                AppRepository obj = new AppRepository();
                bool? IsAntena = Tipo == string.Empty ? (bool?)null :
                    Tipo == "Antena" ? true : false;
                var lista = obj.GetPlanesbyName(txtNombre.Text, IsAntena, PorUsuarios,IdMikrotik).Result;
                var listaFinal = lista?.ToList() ?? new List<ListPlanesModel>();
                dgvPlanes.DataSource = new SortableBindingList<ListPlanesModel>(listaFinal);
                if (dgvPlanes.Columns["Id"] != null)
                    dgvPlanes.Columns["Id"].Visible = false;
                if (dgvPlanes.Columns["Correctos"] != null && PorUsuarios == true)
                    dgvPlanes.Columns["Correctos"].Visible = false;
                if (dgvPlanes.Columns["Erroneos"] != null && PorUsuarios == true)
                    dgvPlanes.Columns["Erroneos"].Visible = false;
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
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Trim() == "")
            {
                DialogResult resultado = MessageBox.Show("Ha dejado el campo vacio, esto buscara a todos los clientes pero puede demorar ¿Quiere continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.No)
                {
                    return;
                }
            }
          BuscarPlanes();
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
                Visible = false,
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
                Name = "Estatus",
                HeaderText = "Estatus",
                DataPropertyName = "Estatus",
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
            dgvPlanes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Correctos",
                HeaderText = "Mikrotiks conectados",
                DataPropertyName = "Correctos",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvPlanes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Erroneos",
                HeaderText = "Mikrotiks que fallaron",
                DataPropertyName = "Erroneos",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            if (PorUsuarios == false)
            {
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
                dgvPlanes.Columns.Add(btnEditar);

                DataGridViewButtonColumn btnVerMikrotiks = new DataGridViewButtonColumn
                {
                    Name = "btnVerMikrotiks",
                    HeaderText = "Acción",
                    Text = "Ver Mikrotiks",
                    UseColumnTextForButtonValue = true,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                    FlatStyle = FlatStyle.Flat,
                    DefaultCellStyle = estiloBotones
                };
                dgvPlanes.Columns.Add(btnVerMikrotiks);
            }
            else {
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
            }   
            dgvPlanes.AllowUserToAddRows = false;
        }

       

        private void dgvPlanes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Evitar errores si hacen click en el encabezado
            if (e.RowIndex < 0) return;
            var Id = dgvPlanes.Rows[e.RowIndex].Cells["Id"].Value;

            switch (dgvPlanes.Columns[e.ColumnIndex].Name)
            {
                case "btnEditar":
                    Plan m = new Plan();
                    m.Id = Convert.ToInt32(Id);
                    m.ShowDialog();
                    BuscarPlanes();
                    break;
                case "btnAsignar":
                    var Nombre = (string)dgvPlanes.Rows[e.RowIndex].Cells["Nombre"].Value;
                    if(Nombre.Trim() == string.Empty)
                    {
                        MessageBox.Show("Solo se pueden asignar planes con nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    // 1. Guardamos el ID en la propiedad pública
                    this.IdSeleccionado = Convert.ToInt32(Id);
                    // 2. Indicamos que la operación fue exitosa
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    break;
                case "btnVerMikrotiks":
                    var NombrePlan = (string)dgvPlanes.Rows[e.RowIndex].Cells["Nombre"].Value;
                    if (NombrePlan.Trim() == string.Empty)
                    {
                        MessageBox.Show("Solo se pueden ver mikrotiks de planes con nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    MikrotiksAnidados MA = new MikrotiksAnidados();
                    MA.IdPlan = Convert.ToInt32(Id);
                    MA.ShowDialog();
                    break;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Plan m = new Plan();
            m.Id = 0;
            m.ShowDialog();
            BuscarPlanes();
        }

        private void Planes_Load(object sender, EventArgs e)
        {
            if(PorUsuarios != false)
            {
              btnNuevo.Visible = false;
            }
        }
    }
}
