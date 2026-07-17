using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using Mikrotik_Administrador.Settings;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
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
                    AppRepository objres = new AppRepository();
                    MikrotikModel mikro = await objres.GetMikrotikById((int)Id);
                    // Nombre del archivo que se creará
                    string fileName = $"backup_{mikro.Nombre.Trim().Replace(" ","")}_{DateTime.Now:yyyyMMdd}.backup";
                    string localPath = Path.Combine(Application.StartupPath, "Backups", fileName);

                    // Asegurar que la carpeta local exista
                    Directory.CreateDirectory(Path.GetDirectoryName(localPath));

                    try
                    {
                        // Paso 1: Conectar por SSH y mandar a generar el respaldo en el MikroTik
                        using (var sshClient = new SshClient(mikro.IP, mikro.Usuario, mikro.Password))
                        {
                            sshClient.Connect();
                            // El comando genera el backup. 'dont-encrypt=yes' es opcional si no quieres ponerle clave al archivo
                            var command = sshClient.CreateCommand($"/system backup save name={fileName} dont-encrypt=yes");
                            command.Execute();
                            sshClient.Disconnect();
                        }

                        // Paso 2: Conectar por SFTP para descargar el archivo a tu PC
                        using (var sftpClient = new SftpClient(mikro.IP, mikro.Usuario, mikro.Password))
                        {
                            sftpClient.Connect();

                            using (var fileStream = File.Create(localPath))
                            {
                                // Descargamos el archivo recién creado
                                sftpClient.DownloadFile(fileName, fileStream);
                            }

                            // Opcional: Eliminar el archivo del MikroTik para no saturar su memoria
                            sftpClient.DeleteFile(fileName);

                            sftpClient.Disconnect();
                        }

                        MessageBox.Show($"Respaldo guardado con éxito en: {localPath}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al realizar el respaldo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "btnRestaurar":
                    AppRepository objress = new AppRepository();
                    MikrotikModel mikrot = await objress.GetMikrotikById((int)Id);

                    // === PASO 1: SELECCIONAR EL ARCHIVO DESDE LA PC ===
                    OpenFileDialog buscadorArchivos = new OpenFileDialog();

                    // Configura para que por defecto se abra en la carpeta "Backups" de tu aplicación
                    buscadorArchivos.InitialDirectory = Path.Combine(Application.StartupPath, "Backups");
                    buscadorArchivos.Filter = "Archivos de Respaldo (*.backup)|*.backup";
                    buscadorArchivos.Title = "Selecciona el respaldo para el MikroTik";

                    // Si el usuario selecciona un archivo y presiona "Abrir"
                    if (buscadorArchivos.ShowDialog() == DialogResult.OK)
                    {
                        // Guardamos la ruta completa del archivo elegido en la PC
                        string rutaArchivoLocal = buscadorArchivos.FileName;

                        // === PASO 2: PREPARAR DATOS DE CONEXIÓN Y VALIDAR ===
                        // Extrae solo el nombre del archivo (ej. "backup_20260713.backup")
                        string nombreArchivoRemoto = Path.GetFileName(rutaArchivoLocal);

                        // Validación de seguridad para confirmar que el archivo existe físicamente
                        if (!File.Exists(rutaArchivoLocal))
                        {
                            MessageBox.Show("El archivo seleccionado no existe en la PC.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // === PASO 3: TRANSFERENCIA Y RESTAURACIÓN ===
                        try
                        {
                            // Subir el archivo al MikroTik mediante SFTP
                            using (var sftpClient = new SftpClient(mikrot.IP, mikrot.Usuario, mikrot.Password))
                            {
                                sftpClient.Connect();

                                using (var fileStream = File.OpenRead(rutaArchivoLocal))
                                {
                                    // Sube el archivo a la raíz de la memoria del MikroTik
                                    sftpClient.UploadFile(fileStream, nombreArchivoRemoto);
                                }

                                sftpClient.Disconnect();
                            }

                            // Conectar por SSH para ordenarle al MikroTik que cargue el respaldo
                            using (var sshClient = new SshClient(mikrot.IP, mikrot.Usuario, mikrot.Password))
                            {
                                sshClient.Connect();

                                // Comando para aplicar la restauración
                                string comandoRestaurar = $"/system backup load name={nombreArchivoRemoto} password=\"\" dont-encrypt=yes";
                                var command = sshClient.CreateCommand(comandoRestaurar);

                                try
                                {
                                    command.Execute();
                                }
                                catch (System.IO.IOException)
                                {
                                    // Ignoramos esta excepción porque el MikroTik se reinicia al instante
                                    // y corta la comunicación bruscamente, lo cual es correcto.
                                }

                                sshClient.Disconnect();
                            }

                            // Mensaje final de éxito para orientar al usuario
                            MessageBox.Show("El respaldo se ha subido con éxito. El MikroTik se está reiniciando para aplicar los cambios. Por favor, espera de 1 a 2 minutos para que vuelva a estar en línea.",
                                            "Restauración en Proceso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error durante la restauración: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
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