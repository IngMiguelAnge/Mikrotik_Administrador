using GMap.NET.MapProviders;
using Microsoft.VisualBasic;
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
        public void CrearGridView()
        {
            dgvComments.Columns.Clear();
            dgvComments.AutoGenerateColumns = false;
            dgvComments.EnableHeadersVisualStyles = false;
            // --- ESTILO DE LOS TÍTULOS (HEADERS) CON TU AZUL LOGO ---
            dgvComments.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            dgvComments.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvComments.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            dgvComments.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dgvComments.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            dgvComments.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);

            dgvComments.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Visible = false,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvComments.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvComments.Columns.Add(new DataGridViewTextBoxColumn
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
            dgvComments.Columns.Add(btnEditar);
            DataGridViewButtonColumn btnCambiar = new DataGridViewButtonColumn
            {
                Name = "btnCambiar",
                HeaderText = "Acción",
                Text = "Cambiar Estatus",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            dgvComments.Columns.Add(btnCambiar);
            dgvComments.AllowUserToAddRows = false;
        }

        public async void BuscarComments()
        {
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            btnBuscar.Enabled = false;
            try
            {
                CrearGridView();
                AppRepository obj = new AppRepository();
                var lista = await Task.Run(() => obj.GetComments(txtNombre.Text));
                var listaFinal = lista?.ToList() ?? new List<ListCommentsModel>();
                dgvComments.DataSource = new SortableBindingList<ListCommentsModel>(listaFinal);
                if (dgvComments.Columns["Id"] != null)
                    dgvComments.Columns["Id"].Visible = false;             
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
