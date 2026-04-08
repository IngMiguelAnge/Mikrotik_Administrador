using Mikrotik_Administrador.Data;
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
        public string Tipo { get; set; }
        public int IdSeleccionado { get; set; } 
        public Planes()
        {
            InitializeComponent();
        }
        public void BuscarPlanes() 
        {
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            btnBuscar.Enabled = false;
            dgvPlanes.DataSource = null;
            dgvPlanes.Columns.Clear();
            try
            {
                AppRepository obj = new AppRepository();
                bool? IsAntena = Tipo == string.Empty ? (bool?)null :
                    Tipo == "Antena" ? true : false;
                var lista = obj.GetPlanesbyName(txtNombre.Text, IsAntena).Result;

                if (lista != null && lista.Count > 0)
                {
                    dgvPlanes.DataSource = lista;
                    AgregarBotones();
                    MessageBox.Show("Carga completa", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se encontraron Planes.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
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

        private void AgregarBotones()
        {
            // Botón Editar
            if (PorUsuarios == false)
            {
                DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn();
                btnEditar.Name = "btnEditar";
                btnEditar.HeaderText = "Acción";
                btnEditar.Text = "Editar";
                btnEditar.UseColumnTextForButtonValue = true;
                dgvPlanes.Columns.Add(btnEditar);
            }
            else
            {
                DataGridViewButtonColumn btnAsignar = new DataGridViewButtonColumn();
                btnAsignar.Name = "btnAsignar";
                btnAsignar.HeaderText = "Acción";
                btnAsignar.Text = "Asignar";
                btnAsignar.UseColumnTextForButtonValue = true;
                dgvPlanes.Columns.Add(btnAsignar);
            }
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
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Plan m = new Plan();
            m.Id = 0;
            m.Show();
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
