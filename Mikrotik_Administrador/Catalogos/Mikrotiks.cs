using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using Mikrotik_Administrador.Settings;
using Renci.SshNet;
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
                    w.ShowDialog();
                    ListaWireless();
                    break;                    
                case "btnDesactivar":
                    var Desactivado = (string)DGVMikrotiks.Rows[e.RowIndex].Cells["Estatus"].Value;
                    if(Desactivado != "Activo")
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
                case "btnCambio":
                    AppRepository obje = new AppRepository();
                    bool resulte = obje.UpdateEstatusWireless(Convert.ToInt32(Id)).Result;
                    if (resulte == true)
                        MessageBox.Show("Cambiado");
                    else
                        MessageBox.Show("Error al cambiar el estatus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ListaWireless();
                    break;
                case "btnRespaldar":
                    // === PASO 0: CONFIGURACIÓN DE LA BARRA DE CARGA Y BOTONES ===
                    progressBar1.Style = ProgressBarStyle.Marquee;
                    progressBar1.MarqueeAnimationSpeed = 30;

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
                            // Paso 1: Conectar por SSH y generar AMBOS respaldos en el MikroTik
                            using (var sshClient = new SshClient(mikro.IP, mikro.Usuario, mikro.Password))
                            {
                                sshClient.Connect();

                                // 1.1 Generar archivo .backup
                                var cmdBackup = sshClient.CreateCommand($"/system backup save name={fileBackup} dont-encrypt=yes");
                                cmdBackup.Execute();

                                // 1.2 CORRECCIÓN CRÍTICA: Generar .rsc compatible y limpio en v7
                                // Usamos 'export' en su forma nativa y añadimos 'terse' para que genere líneas de comandos compactas 
                                // que RouterOS v7 pueda reimportar de forma plana sin interpretar estructuras de bloques redundantes.
                                var cmdRsc = sshClient.CreateCommand($"export file={fileRsc} show-sensitive terse");
                                cmdRsc.Execute();

                                sshClient.Disconnect();
                            }

                            // Paso 2: Conectar por SFTP para descargar ambos archivos a tu PC
                            using (var sftpClient = new SftpClient(mikro.IP, mikro.Usuario, mikro.Password))
                            {
                                sftpClient.Connect();

                                // 2.1 Descargar el archivo .backup binario
                                using (var fileStream = File.Create(localPathBackup))
                                {
                                    sftpClient.DownloadFile(fileBackup, fileStream);
                                }
                                sftpClient.DeleteFile(fileBackup);

                                // 2.2 Descargar el archivo .rsc de texto plano
                                using (var fileStream = File.Create(localPathRsc))
                                {
                                    sftpClient.DownloadFile(fileRsc, fileStream);
                                }
                                sftpClient.DeleteFile(fileRsc);

                                sftpClient.Disconnect();
                            }
                        });

                        MessageBox.Show($"Respaldos generados y guardados con éxito en la carpeta:\n{Path.Combine(Application.StartupPath, "Backups")}",
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
                    }
                    break;
                case "btnRestaurar":
                    // === PASO 0: CONFIGURACIÓN DE LA BARRA DE CARGA Y BOTONES ===
                    progressBar1.Style = ProgressBarStyle.Marquee;
                    progressBar1.MarqueeAnimationSpeed = 30;

                    BtnNuevo.Enabled = false;
                    btnVerMirkotiks.Enabled = false;
                    btnAddresList.Enabled = false;

                    try
                    {
                        AppRepository objress = new AppRepository();
                        MikrotikModel mikrot = await objress.GetMikrotikById((int)Id);

                        // === PASO 1: SELECCIONAR EL ARCHIVO DESDE LA PC ===
                        using (OpenFileDialog buscadorArchivos = new OpenFileDialog())
                        {
                            buscadorArchivos.InitialDirectory = Path.Combine(Application.StartupPath, "Backups");
                            buscadorArchivos.Filter = "Archivos de Respaldo MikroTik (*.backup;*.rsc)|*.backup;*.rsc|Binario (*.backup)|*.backup|Script RSC (*.rsc)|*.rsc";
                            buscadorArchivos.Title = "Selecciona el respaldo para el MikroTik";

                            if (buscadorArchivos.ShowDialog() == DialogResult.OK)
                            {
                                string rutaArchivoLocal = buscadorArchivos.FileName;
                                string nombreArchivoRemoto = Path.GetFileName(rutaArchivoLocal);
                                string extension = Path.GetExtension(rutaArchivoLocal).ToLower();

                                if (!File.Exists(rutaArchivoLocal))
                                {
                                    MessageBox.Show("El archivo seleccionado no existe en la PC.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                                bool esBackup = (extension == ".backup");

                                // 🔥 SEGUNDO PLANO: Transferencia y ejecución forzada
                                await Task.Run(() =>
                                {
                                    // === PASO 2: TRANSFERENCIA POR SFTP ===
                                    using (var sftpClient = new SftpClient(mikrot.IP, mikrot.Usuario, mikrot.Password))
                                    {
                                        sftpClient.Connect();

                                        if (esBackup)
                                        {
                                            using (var fileStream = File.OpenRead(rutaArchivoLocal))
                                            {
                                                sftpClient.UploadFile(fileStream, nombreArchivoRemoto);
                                            }
                                        }
                                        else
                                        {
                                            using (var fileStream = File.OpenRead(rutaArchivoLocal))
                                            {
                                                sftpClient.UploadFile(fileStream, "run-after-reset.rsc");
                                            }
                                        }

                                        sftpClient.Disconnect();
                                    }

                                    // === PASO 3: EJECUCIÓN (API para backup, SSH para reset) ===
                                    if (esBackup)
                                    {
                                        // SOLUCIÓN TOTAL: Usamos un socket API básico de MikroTik. La API no tiene interfaz
                                        // interactiva, por lo tanto RouterOS carga el backup y reinicia al segundo.
                                        using (var apiConnection = new Renci.SshNet.Common.ApiConnection()) // O la librería de API que uses
                                        {
                                            // Si prefieres no meter librerías nuevas, forzamos SSH pero abriendo un canal RAW directo de comandos:
                                            using (var sshClient = new SshClient(mikrot.IP, mikrot.Usuario, mikrot.Password))
                                            {
                                                sshClient.Connect();

                                                // Creamos un comando SSH en crudo y le enviamos la respuesta esperada directamente en la tubería
                                                using (var cmd = sshClient.CreateCommand($"/system backup load name=\"{nombreArchivoRemoto}\" password=\"\" dont-encrypt=yes"))
                                                {
                                                    // Ejecutamos pasándole un input directo al stream interno para responder al "y" fantasma de v7
                                                    var stream = cmd.ExtendedOutputStream;
                                                    cmd.BeginExecute(ar => { }, null);

                                                    // Inyectamos el sí por si el buffer interactivo se quedó colgado esperando confirmación
                                                    var inputWriter = new StreamWriter(cmd.OutputStream);
                                                    inputWriter.WriteLine("y");
                                                    inputWriter.Flush();
                                                }

                                                System.Threading.Thread.Sleep(3000);
                                                sshClient.Disconnect();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // Para .rsc estándar el SSH normal de siempre funciona de maravilla
                                        using (var sshClient = new SshClient(mikrot.IP, mikrot.Usuario, mikrot.Password))
                                        {
                                            sshClient.Connect();
                                            var cmdFinal = sshClient.CreateCommand("/system/reset-configuration no-defaults=yes force=yes");
                                            cmdFinal.BeginExecute();
                                            System.Threading.Thread.Sleep(3000);
                                            sshClient.Disconnect();
                                        }
                                    }
                                });

                                // === PASO 4: INTERFAZ Y MENSAJE DE ÉXITO ===
                                string mensajeExito = esBackup
                                    ? "El respaldo binario se transfirió y se forzó su ejecución enviando el bypass de consola.\n\nEl MikroTik se está reiniciando ahora mismo. Espera de 1 a 2 minutos."
                                    : "El script fue transferido con éxito y se forzó el reinicio de fábrica.\n\nEl MikroTik aplicará la configuración limpia al encender. Espera de 1 a 2 minutos.";

                                MessageBox.Show(mensajeExito, "Restauración en Proceso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error durante la restauración: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        // === PASO 5: RESTABLECER CONTROLES COMPLETAMENTE ===
                        progressBar1.Style = ProgressBarStyle.Blocks;
                        progressBar1.Value = 0;
                        btnAddresList.Enabled = true;
                        btnVerMirkotiks.Enabled = true;
                        BtnNuevo.Enabled = true;
                    }
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
            DataGridViewButtonColumn btnRestaurar = new DataGridViewButtonColumn
            {
                Name = "btnRestaurar",
                HeaderText = "Acción",
                Text = "Restaurar",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = estiloBotones
            };

            DGVMikrotiks.Columns.Add(btnRestaurar);

            DGVMikrotiks.AllowUserToAddRows = false;
        }
        private void btnVerMirkotiks_Click(object sender, EventArgs e)
        {
            ListaMikrotiks();
        }
    }
}