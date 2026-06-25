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

namespace Mikrotik_Administrador.Catalogos
{
    public partial class Bancos : Form
    {
        public Bancos()
        {
            InitializeComponent();
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
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            btnNuevo.Enabled = false;
            btnBuscar.Enabled = false;

            try
            {
                AppRepository obj = new AppRepository();
                string Tipo = cbTipo.Text == "Seleccione"? string.Empty : cbTipo.Text;
                var lista = await Task.Run(() => obj.GetBancos(txtNombre.Text, Tipo));
                dgvBancos.DataSource = lista != null && lista.Count > 0 ? lista : null;

                if (lista == null || lista.Count == 0)
                {
                    MessageBox.Show("No se encontraron bancos.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    AgregarBotones();
                    MessageBox.Show("Carga completa", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                progressBar1.MarqueeAnimationSpeed = 0;
                btnNuevo.Enabled = true;
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
            dgvBancos.Columns.Add(btnEditar);
            DataGridViewButtonColumn btnCambiar = new DataGridViewButtonColumn();
            btnCambiar.Name = "btnCambiar";
            btnCambiar.HeaderText = "Acción";
            btnCambiar.Text = "Cambiar Estatus";
            btnCambiar.UseColumnTextForButtonValue = true;
            dgvBancos.Columns.Add(btnCambiar);
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
