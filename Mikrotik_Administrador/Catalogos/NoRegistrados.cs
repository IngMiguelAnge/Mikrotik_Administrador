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
    public partial class NoRegistrados : Form
    {
        public NoRegistrados()
        {
            InitializeComponent();
        }

        private void DGVClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int IdDeudor = (int)DGVClientes.Rows[e.RowIndex].Cells["Id"].Value;
            try
            {
                DGVClientes.Enabled = false;
                switch (DGVClientes.Columns[e.ColumnIndex].Name)
                {
                    case "btnEditar":
                        NoRegistrado noRegistrado = new NoRegistrado();
                        noRegistrado.Id = IdDeudor;
                        if (noRegistrado.ShowDialog() == DialogResult.OK)
                        {
                            CargarClientes();
                        }
                        break;
                    case "btnCambiarEstatus":
                        AppRepository r = new AppRepository();
                        var resultD = r.UpdateEstatusDeudor(IdDeudor).Result;
                        CargarClientes();
                        break;
                    case "btnVerNota":
                        VerComentario verComentario = new VerComentario();
                        verComentario.Comentario = DGVClientes.Rows[e.RowIndex].Cells["Nota"].Value.ToString();
                        verComentario.ShowDialog();
                        break;
                    case "btnVerMapa":
                        if (string.IsNullOrEmpty(DGVClientes.Rows[e.RowIndex].Cells["Latitud"].Value?.ToString()))
                        {
                            DialogResult resultado = MessageBox.Show("No hay coordenadas registradas en este cliente, se va a registrar coordenadas si continua. ¿Quiere continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (resultado == DialogResult.No)
                            {
                                return;
                            }
                        }
                        string Latitud = DGVClientes.Rows[e.RowIndex].Cells["Latitud"].Value.ToString();
                        string Longitud = DGVClientes.Rows[e.RowIndex].Cells["Longitud"].Value.ToString();
                        Mapa mp = new Mapa();
                        mp.Latitud = DGVClientes.Rows[e.RowIndex].Cells["Latitud"].Value.ToString();
                        mp.Longitud = DGVClientes.Rows[e.RowIndex].Cells["Longitud"].Value.ToString();
                        if (mp.ShowDialog() == DialogResult.OK)
                        {
                            AppRepository obj = new AppRepository();
                            var result = obj.SaveDeudoresCoordenadas(IdDeudor, mp.Latitud, mp.Longitud);
                            DGVClientes.Rows[e.RowIndex].Cells["Latitud"].Value = mp.Latitud;
                            DGVClientes.Rows[e.RowIndex].Cells["Longitud"].Value = mp.Longitud;
                        }

                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}");
            }
            finally
            {
                DGVClientes.Enabled = true;
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
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
        public async void CargarClientes()
        {
            CrearGridView();
            progressBar1.Style = ProgressBarStyle.Marquee; // La barra empieza a moverse sola
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            BtnBuscar.Enabled = false;
            try
            {
                AppRepository obj = new AppRepository();
                var lista = await Task.Run(() => obj.GetDeudores(txtCliente.Text));
                var listaFinal = lista?.ToList() ?? new List<ListDeudoresModel>();
                DGVClientes.DataSource = new SortableBindingList<ListDeudoresModel>(listaFinal);
                if (DGVClientes.Columns["Id"] != null)
                    DGVClientes.Columns["Id"].Visible = false;
                if (DGVClientes.Columns["Nota"] != null)
                    DGVClientes.Columns["Nota"].Visible = false;
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
                ReadOnly = true,
                Visible = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Cliente",
                DataPropertyName = "Nombre",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Latitud",
                HeaderText = "Latitud",
                DataPropertyName = "Latitud",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Longitud",
                HeaderText = "Longitud",
                DataPropertyName = "Longitud",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Recogido",
                HeaderText = "Recogidos",
                DataPropertyName = "Recogido",
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
                Name = "Nota",
                HeaderText = "Nota",
                DataPropertyName = "Nota",
                ReadOnly = true,
                Visible = false,
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
            DataGridViewButtonColumn btnVerNota = new DataGridViewButtonColumn
            {
                Name = "btnVerNota",
                HeaderText = "Acción",
                Text = "Ver Nota",
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
            DataGridViewButtonColumn btnVerMapa = new DataGridViewButtonColumn
            {
                Name = "btnVerMapa",
                HeaderText = "Acción",
                Text = "Ver Mapa",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            DGVClientes.Columns.Add(btnVerMapa);
            DGVClientes.AllowUserToAddRows = false;
        }
    }
}
