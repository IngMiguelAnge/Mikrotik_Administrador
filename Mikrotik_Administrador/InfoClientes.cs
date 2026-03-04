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
            Clientes m = new Clientes();
            m.Show();
            this.Hide();
        }

        private void InfoClientes_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public void CargarClientes()
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
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            BtnBuscar.Enabled = false;
            DGVClientes.DataSource = null;
            DGVClientes.Columns.Clear(); // Limpiar columnas anteriores
            int Id_Mikrotik = CBTodosMikrotiks.Checked == true ? 0 : (int)CBMikrotiks.SelectedValue;
            try
            {
                AppRepository obj = new AppRepository();
                var lista = obj.GetClientesbyName(txtCliente.Text, Id_Mikrotik).Result;

                if (lista != null && lista.Count > 0)
                {
                    DGVClientes.DataSource = lista;
                    AgregarBotones();
                    MessageBox.Show("Carga completa", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se encontraron usuarios en el Mikrotik seleccionado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                BtnBuscar.Enabled = true;
            }
        }
        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            CargarClientes();
        }

        private void AgregarBotones()
        {
            // Botón Editar
            DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn();
            btnEditar.Name = "btnEditar";
            btnEditar.HeaderText = "Acción";
            btnEditar.Text = "Editar";
            btnEditar.UseColumnTextForButtonValue = true;
            DGVClientes.Columns.Add(btnEditar);

            // Botón cambio de estatus
            DataGridViewButtonColumn btnDesactivar = new DataGridViewButtonColumn();
            btnDesactivar.Name = "btnDesactivar";
            btnDesactivar.HeaderText = "Acción";
            btnDesactivar.Text = "Cambiar Estatus";
            btnDesactivar.UseColumnTextForButtonValue = true;
            DGVClientes.Columns.Add(btnDesactivar);
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
                case "btnDesactivar":
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
            }
        }
    }
}
