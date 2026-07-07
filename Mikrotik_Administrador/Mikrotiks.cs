using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

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
            m.ShowDialog();
            ListaMikrotiks();
        }

        private async void ListaMikrotiks()
        {
            try
            {
                CrearGridView();
                AppRepository obj = new AppRepository();

                var lista = await obj.GetMikrotiks();
                var listaFinal = lista?.ToList() ?? new List<ListMikrotikModel>();
                DGVMikrotiks.DataSource = new BindingList<ListMikrotikModel>(listaFinal);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    m.ShowDialog();
                    ListaMikrotiks();
                    break;
                case "btnLanWireless":
                    WirelessMikrotik w = new WirelessMikrotik();
                    w.IdMikrotik = Convert.ToInt32(Id);
                    w.ShowDialog();
                    ListaWireless();
                    break;                    
                case "btnDesactivar":
                    AppRepository obj = new AppRepository();
                    bool result = obj.DesactivarMikrotik(Convert.ToInt32(Id)).Result;
                    if (result == true)
                        MessageBox.Show("Desactivado");
                    else
                        MessageBox.Show("Error al desactivar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ListaMikrotiks();
                    break;
                case "btnUbicacion":
                    Ubicacion u = new Ubicacion();
                    u.IdUsuario = 0;
                    u.IdMikrotik = Convert.ToInt32(Id);
                    u.ShowDialog();
                    break;
                case "btnCambio":
                    AppRepository obje = new AppRepository();
                    bool resulte = obje.UpdateEstatusWireless(Convert.ToInt32(Id)).Result;
                    if (resulte == true)
                        MessageBox.Show("Cambiado");
                    else
                        MessageBox.Show("Error al cambiar el estatus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ListaWireless();
                    break;
            }
        }

        private void btnAddresList_Click(object sender, EventArgs e)
        {
            ListaWireless();
        }
        public void CrearGridViewListaWireless()
        {
            DGVMikrotiks.Columns.Clear();
            DGVMikrotiks.AutoGenerateColumns = false;
            DGVMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            DGVMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Address",
                HeaderText = "Address",
                DataPropertyName = "Address",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            DGVMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Comment",
                HeaderText = "Comment",
                DataPropertyName = "Comment",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            DGVMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Mikrotik",
                HeaderText = "Mikrotik",
                DataPropertyName = "Mikrotik",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            DGVMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                HeaderText = "Estatus",
                DataPropertyName = "Estatus",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            DataGridViewButtonColumn btnCambio = new DataGridViewButtonColumn
            {
                Name = "btnCambio",
                HeaderText = "Acción",
                Text = "Cambiar Estatus",
                UseColumnTextForButtonValue = true,
                Width = 90,
                FlatStyle = FlatStyle.Flat
            };

            DGVMikrotiks.Columns.Add(btnCambio);
            DGVMikrotiks.AllowUserToAddRows = false;
        }
        private async void ListaWireless()
        {
            CrearGridViewListaWireless();
            try
            {
                AppRepository obj = new AppRepository();

                var lista = await obj.GetWireless();

                var listaFinal = lista?.ToList() ?? new List<ListWirelessModel>();
                DGVMikrotiks.DataSource = new BindingList<ListWirelessModel>(listaFinal);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CrearGridView()
        {
            DGVMikrotiks.Columns.Clear();
            DGVMikrotiks.AutoGenerateColumns = false;
            DGVMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            DGVMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Mikrotik",
                DataPropertyName = "Nombre",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            DGVMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IP",
                HeaderText = "IP",
                DataPropertyName = "IP",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            DGVMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PlanAceptado",
                HeaderText = "Planes",
                DataPropertyName = "PlanAceptado",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            DGVMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Limite_Alcanzado",
                HeaderText = "Limite Alcanzado",
                DataPropertyName = "Limite_Alcanzado",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            DGVMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                HeaderText = "Estatus",
                DataPropertyName = "Estatus",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn
            {
                Name = "btnEditar",
                HeaderText = "Acción",
                Text = "Editar",
                UseColumnTextForButtonValue = true,
                Width = 90,
                FlatStyle = FlatStyle.Flat
            };

            DGVMikrotiks.Columns.Add(btnEditar);
            DataGridViewButtonColumn btnLanWireless = new DataGridViewButtonColumn
            {
                Name = "btnLanWireless",
                HeaderText = "Acción",
                Text = "LanWireless",
                UseColumnTextForButtonValue = true,
                Width = 90,
                FlatStyle = FlatStyle.Flat
            };

            DGVMikrotiks.Columns.Add(btnLanWireless);
            DataGridViewButtonColumn btnDesactivar = new DataGridViewButtonColumn
            {
                Name = "btnDesactivar",
                HeaderText = "Acción",
                Text = "Cambiar Estatus",
                UseColumnTextForButtonValue = true,
                Width = 90,
                Visible = false,
                FlatStyle = FlatStyle.Flat
            };

            DGVMikrotiks.Columns.Add(btnDesactivar);
            DataGridViewButtonColumn btnUbicacion = new DataGridViewButtonColumn
            {
                Name = "btnUbicacion",
                HeaderText = "Acción",
                Text = "Ubicación",
                UseColumnTextForButtonValue = true,
                Width = 90,
                Visible = false,
                FlatStyle = FlatStyle.Flat
            };

            DGVMikrotiks.Columns.Add(btnUbicacion);

            DGVMikrotiks.AllowUserToAddRows = false;
        }
        private void InformacionClientesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            InfoClientes m = new InfoClientes();
            m.Show();
            this.Hide();
        }

        private void btnVerMirkotiks_Click(object sender, EventArgs e)
        {
            ListaMikrotiks();
        }
    }
}