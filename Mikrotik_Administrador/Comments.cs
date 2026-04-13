using GMap.NET.MapProviders;
using Microsoft.VisualBasic;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
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
    public partial class Comments : Form
    {
        public Comments()
        {
            InitializeComponent();
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
            BuscarComments();
        }
        public void BuscarComments()
        {
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            btnBuscar.Enabled = false;
            dgvComments.DataSource = null;
            dgvComments.Columns.Clear();
            try
            {
                AppRepository obj = new AppRepository();
                var lista = obj.GetComments(txtNombre.Text).Result;

                if (lista != null && lista.Count > 0)
                {
                    dgvComments.DataSource = lista;
                    AgregarBotones();
                    MessageBox.Show("Carga completa", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se encontraron comennts.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void AgregarBotones()
        {
            DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn();
            btnEditar.Name = "btnEditar";
            btnEditar.HeaderText = "Acción";
            btnEditar.Text = "Editar";
            btnEditar.UseColumnTextForButtonValue = true;
            dgvComments.Columns.Add(btnEditar);
            DataGridViewButtonColumn btnCambiar = new DataGridViewButtonColumn();
            btnCambiar.Name = "btnCambiar";
            btnCambiar.HeaderText = "Acción";
            btnCambiar.Text = "Editar";
            btnCambiar.UseColumnTextForButtonValue = true;
            dgvComments.Columns.Add(btnCambiar);
        }

        private void dgvComments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var Id = (int)dgvComments.Rows[e.RowIndex].Cells["Id"].Value;
            AppRepository m = new AppRepository();
            switch (dgvComments.Columns[e.ColumnIndex].Name)
            {
                case "btnEditar":
                    string respuesta = Interaction.InputBox("Ingrese el nuevo commit:", "Commit", string.Empty);
                    if (!string.IsNullOrEmpty(respuesta))
                    {
                        if (m.SaveComment(Id, respuesta).Result != 0)
                        {
                            MessageBox.Show("Comment actualizado");
                            BuscarComments();
                        }
                        else
                            MessageBox.Show("Error al actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;
                case "btnCambiar":
               
                    bool result = m.UpdateEstatusComment(Id).Result;
                    if (result == true)
                    {
                        MessageBox.Show("Estatus cambiado");
                        BuscarComments();
                    }
                    else
                        MessageBox.Show("Error al desactivar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    break;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            AppRepository m = new AppRepository();
            string respuesta = Interaction.InputBox("Ingrese el nuevo commit:", "Commit", string.Empty);
            if (!string.IsNullOrEmpty(respuesta))
            {
                if (m.SaveComment(0, respuesta).Result != 0)
                {
                    MessageBox.Show("Comment guardado");
                    BuscarComments();
                }
                else
                    MessageBox.Show("Error al actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
