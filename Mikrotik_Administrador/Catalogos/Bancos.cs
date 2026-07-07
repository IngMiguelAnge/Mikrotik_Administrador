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

namespace Mikrotik_Administrador.Catalogos
{
    public partial class Bancos : Form
    {
        public Bancos()
        {
            InitializeComponent();
        }
        public void CrearGridView()
        {
            dgvBancos.Columns.Clear();
            dgvBancos.AutoGenerateColumns = false;
            dgvBancos.EnableHeadersVisualStyles = false;
            // --- ESTILO DE LOS TÍTULOS (HEADERS) CON TU AZUL LOGO ---
            dgvBancos.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            dgvBancos.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvBancos.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            dgvBancos.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dgvBancos.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            dgvBancos.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);

            dgvBancos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvBancos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Banco",
                DataPropertyName = "Nombre",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvBancos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tipo",
                HeaderText = "Tipo",
                DataPropertyName = "Tipo",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });          
            dgvBancos.Columns.Add(new DataGridViewTextBoxColumn
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

            dgvBancos.Columns.Add(btnEditar);
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

            dgvBancos.Columns.Add(btnCambiar);           
            dgvBancos.AllowUserToAddRows = false;
        }
        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Trim() == "")
            {
                DialogResult resultado = MessageBox.Show("Ha dejado el campo vacio, esto buscara a todos los articulos pero puede demorar ¿Quiere continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.No)
                {
                    return;
                }
            }
            await BuscarBancos();
        }

        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            Banco m = new Banco();
            m.Id = 0;
            m.ShowDialog();
            await BuscarBancos();
        }
        public async Task BuscarBancos()
        {
            btnNuevo.Enabled = false;
            btnBuscar.Enabled = false;

            try
            {
                AppRepository obj = new AppRepository();
                string Tipo = cbTipo.Text == "Seleccione"? string.Empty : cbTipo.Text;
                var lista = await Task.Run(() => obj.GetBancos(txtNombre.Text, Tipo));
                var listaFinal = lista?.ToList() ?? new List<ListBancosModel>();
                dgvBancos.DataSource = new SortableBindingList<ListBancosModel>(listaFinal);
                if (dgvBancos.Columns["Id"] != null)
                    dgvBancos.Columns["Id"].Visible = false;
                dgvBancos.DataSource = lista != null && lista.Count > 0 ? lista : null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnNuevo.Enabled = true;
                btnBuscar.Enabled = true;
            }
        }

        private async void dgvBancos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Evitar errores si hacen click en el encabezado
            if (e.RowIndex < 0) return;
            var Id = dgvBancos.Rows[e.RowIndex].Cells["Id"].Value;

            switch (dgvBancos.Columns[e.ColumnIndex].Name)
            {
                case "btnEditar":
                    Banco m = new Banco();
                    m.Id = Convert.ToInt32(Id);
                    m.ShowDialog();
                    await BuscarBancos();
                    break;
                case "btnCambiar":
                    AppRepository obj = new AppRepository();
                    await obj.UpdateStatusBanco(Convert.ToInt32(Id));
                    await BuscarBancos();
                    break;
            }
        }

        private void Bancos_Load(object sender, EventArgs e)
        {
            cbTipo.SelectedIndex = 0;
        }
    }
}
