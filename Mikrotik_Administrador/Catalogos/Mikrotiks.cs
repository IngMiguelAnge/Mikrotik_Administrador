using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using Mikrotik_Administrador.Settings;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        private async void DGVMikrotiks_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                    string Planes = DGVMikrotiks.Rows[e.RowIndex].Cells["PlanAceptado"].Value.ToString();
                    w.Planes = Planes;
                    w.ShowDialog();
                    ListaWireless();
                    break;
                case "btnDesactivar":
                    var Desactivado = (string)DGVMikrotiks.Rows[e.RowIndex].Cells["Estatus"].Value;
                    if (Desactivado != "Activo")
                    {
                        MessageBox.Show("El Mikrotik ya está desactivado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
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
                case "btnCambioCompletadoPool":
                    AppRepository objpool = new AppRepository();
                    bool resultpool = objpool.UpdateCompletado(Convert.ToInt32(Id), false).Result;
                    if (resultpool == true)
                        MessageBox.Show("Cambiado");
                    else
                        MessageBox.Show("Error al cambiar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ListaPools();
                    break;
                case "btnCambioPool":
                    AppRepository objp = new AppRepository();
                    bool resultp = objp.UpdateEstatusPool(Convert.ToInt32(Id)).Result;
                    if (resultp == true)
                        MessageBox.Show("Cambiado");
                    else
                        MessageBox.Show("Error al cambiar el estatus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ListaPools();
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
                case "btnCambioCompletado":
                    AppRepository objco = new AppRepository();
                    bool resultco = objco.UpdateCompletado(Convert.ToInt32(Id), true).Result;
                    if (resultco == true)
                        MessageBox.Show("Cambiado");
                    else
                        MessageBox.Show("Error al cambiar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ListaWireless();
                    break;
                case "btnRespaldar":
                    // === PASO 0: CONFIGURACIÓN DE LA BARRA DE CARGA Y BOTONES ===
                    progressBar1.Style = ProgressBarStyle.Marquee;
                    progressBar1.MarqueeAnimationSpeed = 30;
                    DGVMikrotiks.Enabled = false;
                    BtnNuevo.Enabled = false;
                    btnVerMirkotiks.Enabled = false;
                    btnAddresList.Enabled = false;

                    try
                    {
                        AppRepository objres = new AppRepository();
                        MikrotikModel mikro = await objres.GetMikrotikById((int)Id);

                        string baseName = $"backup_{mikro.Nombre.Trim().Replace(" ", "")}_{DateTime.Now:yyyyMMdd}";
                        string fileBackup = $"{baseName}.backup";
                        string fileRsc = $"{baseName}.rsc";

                        string localPathBackup = Path.Combine(Application.StartupPath, "Backups", fileBackup);
                        string localPathRsc = Path.Combine(Application.StartupPath, "Backups", fileRsc);

                        Directory.CreateDirectory(Path.GetDirectoryName(localPathBackup));
                        await Task.Run(() =>
                        {
                            var connInfo = new ConnectionInfo(
                                mikro.IP,
                                mikro.Usuario,
                                new PasswordAuthenticationMethod(mikro.Usuario, mikro.Password)
                            )
                            {
                                Timeout = TimeSpan.FromSeconds(20)
                            };

                            // 1. GENERAR BACKUP BINARIO (.backup) EN MIKROTIK
                            using (var sshClient = new SshClient(connInfo))
                            {
                                sshClient.HostKeyReceived += (sshSender, hkArgs) => { hkArgs.CanTrust = true; };
                                sshClient.Connect();

                                using (var cmdBackup = sshClient.CreateCommand($"/system backup save name=\"{baseName}\""))
                                {
                                    cmdBackup.CommandTimeout = TimeSpan.FromSeconds(30);
                                    string resBackup = cmdBackup.Execute();
                                    if (string.IsNullOrEmpty(resBackup) || !resBackup.Contains("saved"))
                                        throw new Exception($"Error en backup: {resBackup}");
                                }

                                sshClient.Disconnect();
                            }

                            System.Threading.Thread.Sleep(2000);

                            // 2. DESCARGAR Y ELIMINAR EL ARCHIVO BINARIO VÍA SFTP
                            using (var sftp = new SftpClient(connInfo))
                            {
                                sftp.HostKeyReceived += (sshSender, hkArgs) => { hkArgs.CanTrust = true; };
                                sftp.Connect();

                                ISftpFile targetBackup = null;
                                int maxRetries = 10;

                                for (int i = 0; i < maxRetries; i++)
                                {
                                    var files = sftp.ListDirectory(".").ToList();
                                    if (sftp.Exists("flash"))
                                    {
                                        files.AddRange(sftp.ListDirectory("flash"));
                                    }

                                    // Solo buscamos el binario .backup
                                    targetBackup = files.FirstOrDefault(f =>
                                        !f.IsDirectory &&
                                        f.Name.EndsWith(".backup", StringComparison.OrdinalIgnoreCase) &&
                                        f.Name.IndexOf(baseName, StringComparison.OrdinalIgnoreCase) >= 0);

                                    if (targetBackup != null)
                                        break;

                                    System.Threading.Thread.Sleep(1000);
                                }

                                if (targetBackup != null)
                                {
                                    using (var saveStream = File.Create(localPathBackup))
                                    {
                                        sftp.DownloadFile(targetBackup.FullName, saveStream);
                                    }
                                    sftp.DeleteFile(targetBackup.FullName);
                                }
                                else
                                {
                                    throw new FileNotFoundException($"No se encontró el archivo .backup para '{baseName}'.");
                                }

                                sftp.Disconnect();
                            }

                            // 3. OBTENER EL SCRIPT (.RSC) POR SHELL STREAM EN SESIÓN INDEPENDIENTE
                            using (var sshClient = new SshClient(connInfo))
                            {
                                sshClient.HostKeyReceived += (sshSender, hkArgs) => { hkArgs.CanTrust = true; };
                                sshClient.Connect();

                                using (var stream = sshClient.CreateShellStream("export_cmd", 80, 24, 800, 600, 1024))
                                {
                                    stream.WriteLine("/export show-sensitive");
                                    System.Threading.Thread.Sleep(3000); // Tiempo para recibir el volcado completo

                                    string output = stream.Read();
                                    if (!string.IsNullOrEmpty(output))
                                    {
                                        File.WriteAllText(localPathRsc, output);
                                    }
                                }

                                sshClient.Disconnect();
                            }
                        });
                        MessageBox.Show($"Respaldos generados y guardados con éxito en:\n{Path.Combine(Application.StartupPath, "Backups")}",
                                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al realizar el respaldo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        progressBar1.Style = ProgressBarStyle.Blocks;
                        progressBar1.Value = 0;
                        btnAddresList.Enabled = true;
                        btnVerMirkotiks.Enabled = true;
                        BtnNuevo.Enabled = true;
                        DGVMikrotiks.Enabled = true;
                    }
                    break;
            }
        }
        private void EliminarArchivoFtp(string url, string usuario, string password)
        {
            try
            {
                var request = (System.Net.FtpWebRequest)System.Net.WebRequest.Create(url);
                request.Method = System.Net.WebRequestMethods.Ftp.DeleteFile;
                request.Credentials = new System.Net.NetworkCredential(usuario, password);
                using (var response = (System.Net.FtpWebResponse)request.GetResponse()) { }
            }
            catch { /* Ignorar si no existe */ }
        }
        private void DescargarArchivoFtpPasivo(string url, string localPath, string usuario, string password)
        {
            var request = (System.Net.FtpWebRequest)System.Net.WebRequest.Create(url);
            request.Method = System.Net.WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new System.Net.NetworkCredential(usuario, password);

            // Puntos clave para evitar bloqueos
            request.UsePassive = true;      // Modo pasivo obligado para atravesar NAT/Firewall
            request.UseBinary = true;       // Descarga binaria sin corrupción
            request.KeepAlive = false;      // Cierra el socket al terminar

            using (var response = (System.Net.FtpWebResponse)request.GetResponse())
            using (var responseStream = response.GetResponseStream())
            using (var fileStream = File.Create(localPath))
            {
                responseStream.CopyTo(fileStream);
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
            DGVMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Completado",
                HeaderText = "Rango de Ips",
                DataPropertyName = "Completado",
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
            DataGridViewButtonColumn btnCambioCompletado = new DataGridViewButtonColumn
            {
                Name = "btnCambioCompletado",
                HeaderText = "Acción",
                Text = "Cambiar Completado",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            DGVMikrotiks.Columns.Add(btnCambioCompletado);

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
                DGVMikrotiks.DataSource = new SortableBindingList<ListWirelessModel>(listaFinal);
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
                Text = "Rangos",
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
            DataGridViewButtonColumn btnRespaldar = new DataGridViewButtonColumn
            {
                Name = "btnRespaldar",
                HeaderText = "Acción",
                Text = "Respaldar",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };

            DGVMikrotiks.Columns.Add(btnRespaldar);
            //DataGridViewButtonColumn btnRestaurar = new DataGridViewButtonColumn
            //{
            //    Name = "btnRestaurar",
            //    HeaderText = "Acción",
            //    Text = "Restaurar",
            //    UseColumnTextForButtonValue = true,
            //    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
            //    FlatStyle = FlatStyle.Flat,
            //    DefaultCellStyle = estiloBotones
            //};

            //DGVMikrotiks.Columns.Add(btnRestaurar);

            DGVMikrotiks.AllowUserToAddRows = false;
        }
        private void btnVerMirkotiks_Click(object sender, EventArgs e)
        {
            ListaMikrotiks();
        }

        private void btnVerPools_Click(object sender, EventArgs e)
        {
            ListaPools();
        }
        private async void ListaPools()
        {
            CrearGridViewListaPools();
            try
            {
                AppRepository obj = new AppRepository();

                var lista = await obj.GetPools();
                var listaFinal = lista?.ToList() ?? new List<ListPoolsModel>();
                DGVMikrotiks.DataSource = new SortableBindingList<ListPoolsModel>(listaFinal);
                if (DGVMikrotiks.Columns["Id"] != null)
                    DGVMikrotiks.Columns["Id"].Visible = false;
                if (DGVMikrotiks.Columns["IdMikrotik"] != null)
                    DGVMikrotiks.Columns["IdMikrotik"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void CrearGridViewListaPools()
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
                Name = "IP",
                HeaderText = "IP",
                DataPropertyName = "IP",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DGVMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdMikrotik",
                HeaderText = "IdMikrotik",
                DataPropertyName = "IdMikrotik",
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
            DGVMikrotiks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Completado",
                HeaderText = "Rango de Ips",
                DataPropertyName = "Completado",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            DataGridViewButtonColumn btnCambioPool = new DataGridViewButtonColumn
            {
                Name = "btnCambioPool",
                HeaderText = "Acción",
                Text = "Cambiar Estatus",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            DGVMikrotiks.Columns.Add(btnCambioPool);
            DataGridViewButtonColumn btnCambioCompletadoPool = new DataGridViewButtonColumn
            {
                Name = "btnCambioCompletadoPool",
                HeaderText = "Acción",
                Text = "Cambiar Completado",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };
            DGVMikrotiks.Columns.Add(btnCambioCompletadoPool);

            DGVMikrotiks.AllowUserToAddRows = false;
        }
    }
}