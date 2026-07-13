using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using Mikrotik_Administrador.Settings;
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
            btnAddresList.Enabled = false;
            btnVerMirkotiks.Enabled = false;
            BtnNuevo.Enabled = false;
            try
            {
                CrearGridView();
                AppRepository obj = new AppRepository();

                var lista = await obj.GetMikrotiks();
                var listaFinal = lista?.ToList() ?? new List<ListMikrotikModel>();
                 DGVMikrotiks.DataSource = new SortableBindingList<ListMikrotikModel>(listaFinal);
                if (DGVMikrotiks.Columns["Id"] != null)
                DGVMikrotiks.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnAddresList.Enabled = true;
                btnVerMirkotiks.Enabled = true;
                BtnNuevo.Enabled = true;
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
            DGVMikrotiks.EnableHeadersVisualStyles = false;
            // --- ESTILO DE LOS TÍTULOS (HEADERS) CON TU AZUL LOGO ---
            DGVMikrotiks.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            DGVMikrotiks.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            DGVMikrotiks.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            DGVMikrotiks.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            DGVMikrotiks.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            DGVMikrotiks.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196); 
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110); 
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);

            DGVMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Address",
                HeaderText = "Address",
                DataPropertyName = "Address",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Comment",
                HeaderText = "Comment",
                DataPropertyName = "Comment",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Mikrotik",
                HeaderText = "Mikrotik",
                DataPropertyName = "Mikrotik",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                HeaderText = "Estatus",
                DataPropertyName = "Estatus",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DataGridViewButtonColumn btnCambio = new DataGridViewButtonColumn
            {
                Name = "btnCambio",
                HeaderText = "Acción",
                Text = "Cambiar Estatus",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
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
                if (DGVMikrotiks.Columns["Id"] != null)
                    DGVMikrotiks.Columns["Id"].Visible = false;
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
            DGVMikrotiks.EnableHeadersVisualStyles = false;
            // --- ESTILO DE LOS TÍTULOS (HEADERS) CON TU AZUL LOGO ---
            DGVMikrotiks.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            DGVMikrotiks.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            DGVMikrotiks.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);

            // --- ESTILO GENERAL DE LAS CELDAS DE TEXTO ---
            DGVMikrotiks.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            DGVMikrotiks.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 196, 205);
            DGVMikrotiks.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            // --- ESTILO EXCLUSIVO PARA LOS BOTONES DENTRO DEL GRID ---
            System.Windows.Forms.DataGridViewCellStyle estiloBotones = new System.Windows.Forms.DataGridViewCellStyle();
            estiloBotones.BackColor = System.Drawing.Color.FromArgb(43, 80, 196);
            estiloBotones.ForeColor = System.Drawing.Color.White;
            estiloBotones.SelectionBackColor = System.Drawing.Color.FromArgb(20, 34, 110);
            estiloBotones.SelectionForeColor = System.Drawing.Color.White;
            estiloBotones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);

            DGVMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Mikrotik",
                DataPropertyName = "Nombre",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IP",
                HeaderText = "IP",
                DataPropertyName = "IP",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PlanAceptado",
                HeaderText = "Planes",
                DataPropertyName = "PlanAceptado",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Limite_Alcanzado",
                HeaderText = "Limite Alcanzado",
                DataPropertyName = "Limite_Alcanzado",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
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

            DGVMikrotiks.Columns.Add(btnEditar);
            DataGridViewButtonColumn btnLanWireless = new DataGridViewButtonColumn
            {
                Name = "btnLanWireless",
                HeaderText = "Acción",
                Text = "LanWireless",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };

            DGVMikrotiks.Columns.Add(btnLanWireless);
            DataGridViewButtonColumn btnDesactivar = new DataGridViewButtonColumn
            {
                Name = "btnDesactivar",
                HeaderText = "Acción",
                Text = "Desactivar",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };

            DGVMikrotiks.Columns.Add(btnDesactivar);
            DataGridViewButtonColumn btnUbicacion = new DataGridViewButtonColumn
            {
                Name = "btnUbicacion",
                HeaderText = "Acción",
                Text = "Ubicación",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };

            DGVMikrotiks.Columns.Add(btnUbicacion);

            DGVMikrotiks.AllowUserToAddRows = false;
        }
        private void btnVerMirkotiks_Click(object sender, EventArgs e)
        {
            ListaMikrotiks();
        }
    }
}