using Mikrotik_Administrador.Data;
using System;
using System.Windows.Forms;

namespace Mikrotik_Administrador
{
    public partial class Mikrotiks : Form
    {
        public Mikrotiks()
        {
            InitializeComponent();
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            InfoMikrotik m = new InfoMikrotik();
            m.IdMikrotik = 0;
            m.Show();
        }

        private void Mikrotiks_Load(object sender, EventArgs e)
        {
            ListaMikrotiks();
        }
        private async void ListaMikrotiks()
        {
            try
            {
                DGVMikrotiks.DataSource = null;
                AppRepository obj = new AppRepository();

                var lista = await obj.GetMikrotiks();

                if (lista != null && lista.Count > 0)
                {
                    DGVMikrotiks.DataSource = lista;

                    if (DGVMikrotiks.Columns["btnEditar"] == null)
                    {
                        AgregarBotones();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }
        private void AgregarBotones()
        {
            // Botón Editar
            DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn();
            btnEditar.Name = "btnEditar";
            btnEditar.HeaderText = "Acción";
            btnEditar.Text = "Editar";
            btnEditar.UseColumnTextForButtonValue = true;
            DGVMikrotiks.Columns.Add(btnEditar);

            // Botón Eliminar
            DataGridViewButtonColumn btnDesactivar = new DataGridViewButtonColumn();
            btnDesactivar.Name = "btnDesactivar";
            btnDesactivar.HeaderText = "Acción";
            btnDesactivar.Text = "Desactivar";
            btnDesactivar.UseColumnTextForButtonValue = true;
            DGVMikrotiks.Columns.Add(btnDesactivar);

            // Botón Configurar
            DataGridViewButtonColumn btnConfig = new DataGridViewButtonColumn();
            btnConfig.Name = "btnUbicacion";
            btnConfig.HeaderText = "Acción";
            btnConfig.Text = "Ubicación";
            btnConfig.UseColumnTextForButtonValue = true;
            DGVMikrotiks.Columns.Add(btnConfig);
        }
        private void DGVMikrotiks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Evitar errores si hacen click en el encabezado
            if (e.RowIndex < 0) return;
            var Id = DGVMikrotiks.Rows[e.RowIndex].Cells["Id"].Value;

            switch (DGVMikrotiks.Columns[e.ColumnIndex].Name)
            {
                case "btnEditar":
                    InfoMikrotik m = new InfoMikrotik();
                    m.IdMikrotik = Convert.ToInt32(Id);
                    m.Show();
                    break;
                case "btnDesactivar":
                    AppRepository obj = new AppRepository();
                    bool result = obj.DesactivarMikrotik(Convert.ToInt32(Id)).Result;
                    if (result == true)
                        MessageBox.Show("Desactivado");
                    else
                        MessageBox.Show("Error al desactivar");
                    break;
                case "btnUbicacion":
                    Ubicacion u = new Ubicacion();
                    u.IdMikrotik = Convert.ToInt32(Id);
                    u.Show();
                    break;
            }

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ListaMikrotiks();
        }

    }
}
