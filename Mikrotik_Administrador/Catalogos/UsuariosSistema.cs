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
    public partial class UsuariosSistema : Form
    {
        public UsuariosSistema()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Trim() == string.Empty && txtUsuario.Text.Trim() == string.Empty)
            {
                DialogResult resultado = MessageBox.Show("Ha dejado los campos vacios, esto buscara a todos los usuarios pero puede demorar ¿Quiere continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.No)
                {
                    return;
                }
            }
            BuscarUsuarios();
        }
        public void CrearGridView()
        {
            dgvUsuarios.Columns.Clear();
            dgvUsuarios.AutoGenerateColumns = false;
            dgvUsuarios.EnableHeadersVisualStyles = false;
            // --- ESTILO DE LOS TÍTULOS (HEADERS) CON TU AZUL LOGO ---
            dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            dgvUsuarios.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dgvUsuarios.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            dgvUsuarios.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Visible = false,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Usuario",
                HeaderText = "Usuario",
                DataPropertyName = "Usuario",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Password",
                HeaderText = "Password",
                DataPropertyName = "Password",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                HeaderText = "Estatus",
                DataPropertyName = "Estatus",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdTipo",
                HeaderText = "IdTipo",
                DataPropertyName = "IdTipo",
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TipoUsuario",
                HeaderText = "Tipo",
                DataPropertyName = "TipoUsuario",
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
            dgvUsuarios.Columns.Add(btnEditar);
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
            dgvUsuarios.Columns.Add(btnCambiar);
            dgvUsuarios.AllowUserToAddRows = false;
        }
        public async void BuscarUsuarios()
        {
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            btnBuscar.Enabled = false;
            try
            {
                CrearGridView();
                AppRepository obj = new AppRepository();
                var lista = await Task.Run(() => obj.GetUsuarios(txtNombre.Text,txtUsuario.Text));
                var listaFinal = lista?.ToList() ?? new List<ListUsuariosModel>();
                dgvUsuarios.DataSource = new SortableBindingList<ListUsuariosModel>(listaFinal);
                if (dgvUsuarios.Columns["Id"] != null)
                    dgvUsuarios.Columns["Id"].Visible = false;
                if (dgvUsuarios.Columns["Password"] != null)
                    dgvUsuarios.Columns["Password"].Visible = false;
                if (dgvUsuarios.Columns["IdTipo"] != null)
                    dgvUsuarios.Columns["IdTipo"].Visible = false;
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

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var Id = (int)dgvUsuarios.Rows[e.RowIndex].Cells["Id"].Value;
            var Nombre = (string)dgvUsuarios.Rows[e.RowIndex].Cells["Nombre"].Value;
            var Usuario = (string)dgvUsuarios.Rows[e.RowIndex].Cells["Usuario"].Value;
            var Password = (string)dgvUsuarios.Rows[e.RowIndex].Cells["Password"].Value;
            var IdTipo = (int)dgvUsuarios.Rows[e.RowIndex].Cells["IdTipo"].Value;
            AppRepository m = new AppRepository();
            switch (dgvUsuarios.Columns[e.ColumnIndex].Name)
            {
                case "btnEditar":
                    UsuarioSistema c = new UsuarioSistema();
                    c.Id = Id;
                    c.Nombre = Nombre;
                    c.Usuario = Usuario;
                    c.Password = Password;
                    c.IdTipo = IdTipo;
                    c.ShowDialog();
                    BuscarUsuarios();
                    break;
                case "btnCambiar":
                    if (Id == 0)
                    {
                        MessageBox.Show("No se esta permitido desactivar al primer usuario", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return;
                    }
                    bool result = m.UpdateEstatusUsuario(Id).Result;
                    if (result == true)
                    {
                        MessageBox.Show("Estatus cambiado");
                        BuscarUsuarios();
                    }
                    else
                        MessageBox.Show("Error al desactivar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    break;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            UsuarioSistema us = new UsuarioSistema();
            us.ShowDialog();
        }
    }
}
