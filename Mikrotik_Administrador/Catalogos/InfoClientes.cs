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
    public partial class InfoClientes : Form
    {
        public InfoClientes()
        {
            InitializeComponent();
        }

        private async void InfoClientes_Load(object sender, EventArgs e)
        {
            AppRepository obj = new AppRepository();
            var listaMikrotiks = await obj.GetMikrotiks();

            // Insertamos un objeto "fantasma" al inicio para el placeholder
            listaMikrotiks.Insert(0, new ListMikrotikModel { Id = 0, Nombre = "Selecciona un Mikrotik" });

            // Configuramos el ComboBox
            CBMikrotiks.DisplayMember = "Nombre"; // Lo que el usuario VE
            CBMikrotiks.ValueMember = "Id";      // El dato que procesas por DETRÁS
            CBMikrotiks.DataSource = listaMikrotiks;
            CBMikrotiks.SelectedIndex = 0;
        }

        private void mikrotiksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mikrotiks m = new Mikrotiks();
            m.Show();
            this.Hide();
        }

        private void migracionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Migracion m = new Migracion();
            m.Show();
            this.Hide();
        }

        private void AsignacionClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Usuarios m = new Usuarios();
            m.Show();
            this.Hide();
        }

        public async void CargarClientes()
        {
            CrearGridView();
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            BtnBuscar.Enabled = false;
            int IdMikrotik = CBTodosMikrotiks.Checked == true ? 0 : (int)CBMikrotiks.SelectedValue;
            try
            {
                AppRepository obj = new AppRepository();
                var lista = await Task.Run(() => obj.GetClientesbyName(txtCliente.Text, IdMikrotik));
                var listaFinal = lista?.ToList() ?? new List<ListClientesModel>();
                DGVClientes.DataSource = new SortableBindingList<ListClientesModel>(listaFinal);
                if (DGVClientes.Columns["Id"] != null)
                    DGVClientes.Columns["Id"].Visible = false;
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 0;
                BtnBuscar.Enabled = true;
            }
        }
        public void CrearGridView()
        {
            DGVClientes.Columns.Clear();
            DGVClientes.AutoGenerateColumns = false;
            DGVClientes.EnableHeadersVisualStyles = false;
            // --- ESTILO DE LOS TÍTULOS (HEADERS) CON TU AZUL LOGO ---
            DGVClientes.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            DGVClientes.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            DGVClientes.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            DGVClientes.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            DGVClientes.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            DGVClientes.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);

            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Visible = false,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                HeaderText = "Estatus",
                DataPropertyName = "Estatus",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalServicios",
                HeaderText = "TotalServicios",
                DataPropertyName = "TotalServicios",
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
            DGVClientes.Columns.Add(btnEditar);
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
            DGVClientes.Columns.Add(btnCambiarEstatus);
            DataGridViewButtonColumn btnServicios = new DataGridViewButtonColumn
            {
                Name = "btnServicios",
                HeaderText = "Acción",
                Text = "Servicios",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            DGVClientes.Columns.Add(btnServicios);
            DGVClientes.AllowUserToAddRows = false;
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            if (CBMikrotiks.SelectedValue.ToString() == "0" && CBTodosMikrotiks.Checked == false)
            {
                MessageBox.Show("Por favor, selecciona un Mikrotik.");
                return;
            }
            if (txtCliente.Text.Trim() == "")
            {
                DialogResult resultado = MessageBox.Show("Ha dejado el campo vacio, esto buscara a todos los clientes pero puede demorar ¿Quiere continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.No)
                {
                    return;
                }
            }
            CargarClientes();
        }

     
        private void CBTodosMikrotiks_CheckedChanged(object sender, EventArgs e)
        {
            if (CBTodosMikrotiks.Checked)
            {
                CBMikrotiks.Enabled = false;
            }
            else
            {
                CBMikrotiks.Enabled = true;
            }
        }

        private void DGVClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Evitar errores si hacen click en el encabezado
            if (e.RowIndex < 0) return;
            var Id = DGVClientes.Rows[e.RowIndex].Cells["Id"].Value;

            switch (DGVClientes.Columns[e.ColumnIndex].Name)
            {
                case "btnEditar":
                    InfoCliente m = new InfoCliente();
                    m.IdCliente = Convert.ToInt32(Id);
                    m.Show();
                    break;
                case "btnCambiarEstatus":
                    var TotalServicios = DGVClientes.Rows[e.RowIndex].Cells["TotalServicios"].Value;
                    if (Convert.ToInt32(TotalServicios) > 0)
                    {
                        MessageBox.Show("No se puede cambiar el estatus del cliente porque tiene servicios activos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    AppRepository obj = new AppRepository();
                    bool result = obj.UpdateEstatusCliente(Convert.ToInt32(Id)).Result;
                    if (result == true)
                    {
                        MessageBox.Show("Estatus cambiado");
                        CargarClientes();
                    }
                    else
                        MessageBox.Show("Error al desactivar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "btnServicios":
                    ServiciosCliente s = new ServiciosCliente();
                    s.IdCliente = Convert.ToInt32(Id);
                    s.ShowDialog();
                    CargarClientes();
                    break;
            }
        }
    }
}
