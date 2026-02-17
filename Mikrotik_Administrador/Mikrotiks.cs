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
                DGVMikrotiks.Columns.Clear();
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
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            // Botón LanWireless
            DataGridViewButtonColumn btnLanWireless = new DataGridViewButtonColumn();
            btnLanWireless.Name = "btnLanWireless";
            btnLanWireless.HeaderText = "Acción";
            btnLanWireless.Text = "LanWireless";
            btnLanWireless.UseColumnTextForButtonValue = true;
            DGVMikrotiks.Columns.Add(btnLanWireless);

            // Botón Eliminar
            DataGridViewButtonColumn btnDesactivar = new DataGridViewButtonColumn();
            btnDesactivar.Name = "btnDesactivar";
            btnDesactivar.HeaderText = "Acción";
            btnDesactivar.Text = "Desactivar";
            btnDesactivar.UseColumnTextForButtonValue = true;
            DGVMikrotiks.Columns.Add(btnDesactivar);

            // Botón Ubicacion
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
                case "btnLanWireless":
                    WirelessMikrotik w = new WirelessMikrotik();
                    w.Id_Mikrotik = Convert.ToInt32(Id);
                    w.Show();
                    break;                    
                case "btnDesactivar":
                    AppRepository obj = new AppRepository();
                    bool result = obj.DesactivarMikrotik(Convert.ToInt32(Id)).Result;
                    if (result == true)
                        MessageBox.Show("Desactivado");
                    else
                        MessageBox.Show("Error al desactivar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "btnUbicacion":
                    Ubicacion u = new Ubicacion();
                    u.Id_Mikrotik = Convert.ToInt32(Id);
                    u.Show();
                    break;
            }
        }

        private void migraciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Migracion m = new Migracion();
            m.Show();
            this.Close();
        }

        private void btnAddresList_Click(object sender, EventArgs e)
        {
            ListaWireless();
        }
        private async void ListaWireless()
        {
            try
            {
                DGVMikrotiks.DataSource = null;
                DGVMikrotiks.Columns.Clear();
                AppRepository obj = new AppRepository();

                var lista = await obj.GetWireless();

                if (lista != null && lista.Count > 0)
                {
                    DGVMikrotiks.DataSource = lista;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Mikrotiks_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnVerMirkotiks_Click(object sender, EventArgs e)
        {
            ListaMikrotiks();
        }
    }
}
