namespace Mikrotik_Administrador.Catalogos
{
    partial class PreregistroCliente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreregistroCliente));
            this.tblMainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.gbDatosCliente = new System.Windows.Forms.GroupBox();
            this.tblDatosCliente = new System.Windows.Forms.TableLayoutPanel();
            this.lblMensaje1 = new System.Windows.Forms.Label();
            this.txtNombreCliente = new System.Windows.Forms.TextBox();
            this.lblCorreo = new System.Windows.Forms.Label();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.lblMensaje3 = new System.Windows.Forms.Label();
            this.txtTelefono1 = new System.Windows.Forms.TextBox();
            this.lblMensaje4 = new System.Windows.Forms.Label();
            this.txtTelefono2 = new System.Windows.Forms.TextBox();
            this.gbPlanes = new System.Windows.Forms.GroupBox();
            this.tblPlanes = new System.Windows.Forms.TableLayoutPanel();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombrePlan = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.llbNombreUsuario = new System.Windows.Forms.Label();
            this.txtNombreServicio = new System.Windows.Forms.TextBox();
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.dgvPlanes = new System.Windows.Forms.DataGridView();
            this.lblMikrotikSeleccionado = new System.Windows.Forms.Label();
            this.txtMikrotik = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.gbUbicacion = new System.Windows.Forms.GroupBox();
            this.tblUbicacion = new System.Windows.Forms.TableLayoutPanel();
            this.lblCalle = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.lblUbicacion = new System.Windows.Forms.Label();
            this.txtDireccionOficial = new System.Windows.Forms.TextBox();
            this.lblGPS = new System.Windows.Forms.Label();
            this.pnlGPS = new System.Windows.Forms.FlowLayoutPanel();
            this.lblLatitud = new System.Windows.Forms.Label();
            this.txtLatitud = new System.Windows.Forms.TextBox();
            this.lblLongitud = new System.Windows.Forms.Label();
            this.txtLongitud = new System.Windows.Forms.TextBox();
            this.CBCoordendadas = new System.Windows.Forms.CheckBox();
            this.btnBuscarCoordenadas = new System.Windows.Forms.Button();
            this.lblDireccionSugerida = new System.Windows.Forms.Label();
            this.txtDireccionSugerida = new System.Windows.Forms.TextBox();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnAceptarUbicacion = new System.Windows.Forms.Button();
            this.btnCancelarDireccion = new System.Windows.Forms.Button();
            this.btnLupa = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.gMapOculto = new GMap.NET.WindowsForms.GMapControl();
            this.tblMainLayout.SuspendLayout();
            this.gbDatosCliente.SuspendLayout();
            this.tblDatosCliente.SuspendLayout();
            this.gbPlanes.SuspendLayout();
            this.tblPlanes.SuspendLayout();
            this.panelContenedor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlanes)).BeginInit();
            this.gbUbicacion.SuspendLayout();
            this.tblUbicacion.SuspendLayout();
            this.pnlGPS.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblMainLayout
            // 
            this.tblMainLayout.ColumnCount = 1;
            this.tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainLayout.Controls.Add(this.gbDatosCliente, 0, 0);
            this.tblMainLayout.Controls.Add(this.gbPlanes, 0, 1);
            this.tblMainLayout.Controls.Add(this.gbUbicacion, 0, 2);
            this.tblMainLayout.Controls.Add(this.panelBotones, 0, 3);
            this.tblMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMainLayout.Location = new System.Drawing.Point(0, 0);
            this.tblMainLayout.Name = "tblMainLayout";
            this.tblMainLayout.Padding = new System.Windows.Forms.Padding(10);
            this.tblMainLayout.RowCount = 4;
            this.tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tblMainLayout.Size = new System.Drawing.Size(1100, 800);
            this.tblMainLayout.TabIndex = 0;
            // 
            // gbDatosCliente
            // 
            this.gbDatosCliente.Controls.Add(this.tblDatosCliente);
            this.gbDatosCliente.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDatosCliente.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.gbDatosCliente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(42)))), ((int)(((byte)(107)))));
            this.gbDatosCliente.Location = new System.Drawing.Point(13, 13);
            this.gbDatosCliente.Name = "gbDatosCliente";
            this.gbDatosCliente.Padding = new System.Windows.Forms.Padding(10);
            this.gbDatosCliente.Size = new System.Drawing.Size(1074, 135);
            this.gbDatosCliente.TabIndex = 0;
            this.gbDatosCliente.TabStop = false;
            this.gbDatosCliente.Text = "Datos Personales del Cliente";
            // 
            // tblDatosCliente
            // 
            this.tblDatosCliente.ColumnCount = 2;
            this.tblDatosCliente.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblDatosCliente.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblDatosCliente.Controls.Add(this.lblMensaje1, 0, 0);
            this.tblDatosCliente.Controls.Add(this.txtNombreCliente, 0, 1);
            this.tblDatosCliente.Controls.Add(this.lblCorreo, 1, 0);
            this.tblDatosCliente.Controls.Add(this.txtCorreo, 1, 1);
            this.tblDatosCliente.Controls.Add(this.lblMensaje3, 0, 2);
            this.tblDatosCliente.Controls.Add(this.txtTelefono1, 0, 3);
            this.tblDatosCliente.Controls.Add(this.lblMensaje4, 1, 2);
            this.tblDatosCliente.Controls.Add(this.txtTelefono2, 1, 3);
            this.tblDatosCliente.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblDatosCliente.Location = new System.Drawing.Point(10, 36);
            this.tblDatosCliente.Name = "tblDatosCliente";
            this.tblDatosCliente.RowCount = 4;
            this.tblDatosCliente.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblDatosCliente.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblDatosCliente.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblDatosCliente.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblDatosCliente.Size = new System.Drawing.Size(1054, 89);
            this.tblDatosCliente.TabIndex = 0;
            // 
            // lblMensaje1
            // 
            this.lblMensaje1.AutoSize = true;
            this.lblMensaje1.Location = new System.Drawing.Point(3, 0);
            this.lblMensaje1.Name = "lblMensaje1";
            this.lblMensaje1.Size = new System.Drawing.Size(101, 28);
            this.lblMensaje1.TabIndex = 0;
            this.lblMensaje1.Text = "*Nombre:";
            // 
            // txtNombreCliente
            // 
            this.txtNombreCliente.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNombreCliente.Location = new System.Drawing.Point(3, 31);
            this.txtNombreCliente.Name = "txtNombreCliente";
            this.txtNombreCliente.Size = new System.Drawing.Size(521, 33);
            this.txtNombreCliente.TabIndex = 1;
            // 
            // lblCorreo
            // 
            this.lblCorreo.AutoSize = true;
            this.lblCorreo.Location = new System.Drawing.Point(530, 0);
            this.lblCorreo.Name = "lblCorreo";
            this.lblCorreo.Size = new System.Drawing.Size(184, 28);
            this.lblCorreo.TabIndex = 2;
            this.lblCorreo.Text = "Correo electrónico:";
            // 
            // txtCorreo
            // 
            this.txtCorreo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCorreo.Location = new System.Drawing.Point(530, 31);
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.Size = new System.Drawing.Size(521, 33);
            this.txtCorreo.TabIndex = 3;
            // 
            // lblMensaje3
            // 
            this.lblMensaje3.AutoSize = true;
            this.lblMensaje3.Location = new System.Drawing.Point(3, 67);
            this.lblMensaje3.Name = "lblMensaje3";
            this.lblMensaje3.Size = new System.Drawing.Size(80, 28);
            this.lblMensaje3.TabIndex = 4;
            this.lblMensaje3.Text = "Celular:";
            // 
            // txtTelefono1
            // 
            this.txtTelefono1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTelefono1.Location = new System.Drawing.Point(3, 98);
            this.txtTelefono1.Name = "txtTelefono1";
            this.txtTelefono1.Size = new System.Drawing.Size(521, 33);
            this.txtTelefono1.TabIndex = 5;
            // 
            // lblMensaje4
            // 
            this.lblMensaje4.AutoSize = true;
            this.lblMensaje4.Location = new System.Drawing.Point(530, 67);
            this.lblMensaje4.Name = "lblMensaje4";
            this.lblMensaje4.Size = new System.Drawing.Size(201, 28);
            this.lblMensaje4.TabIndex = 6;
            this.lblMensaje4.Text = "Teléfono secundario:";
            // 
            // txtTelefono2
            // 
            this.txtTelefono2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTelefono2.Location = new System.Drawing.Point(530, 98);
            this.txtTelefono2.Name = "txtTelefono2";
            this.txtTelefono2.Size = new System.Drawing.Size(521, 33);
            this.txtTelefono2.TabIndex = 7;
            // 
            // gbPlanes
            // 
            this.gbPlanes.Controls.Add(this.tblPlanes);
            this.gbPlanes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPlanes.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.gbPlanes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(42)))), ((int)(((byte)(107)))));
            this.gbPlanes.Location = new System.Drawing.Point(13, 154);
            this.gbPlanes.Name = "gbPlanes";
            this.gbPlanes.Padding = new System.Windows.Forms.Padding(10);
            this.gbPlanes.Size = new System.Drawing.Size(1074, 317);
            this.gbPlanes.TabIndex = 1;
            this.gbPlanes.TabStop = false;
            this.gbPlanes.Text = "Selección de Plan";
            // 
            // tblPlanes
            // 
            this.tblPlanes.ColumnCount = 3;
            this.tblPlanes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tblPlanes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tblPlanes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblPlanes.Controls.Add(this.lblNombre, 0, 0);
            this.tblPlanes.Controls.Add(this.txtNombrePlan, 0, 1);
            this.tblPlanes.Controls.Add(this.btnBuscar, 0, 2);
            this.tblPlanes.Controls.Add(this.progressBar1, 0, 3);
            this.tblPlanes.Controls.Add(this.llbNombreUsuario, 1, 0);
            this.tblPlanes.Controls.Add(this.txtNombreServicio, 1, 1);
            this.tblPlanes.Controls.Add(this.panelContenedor, 0, 4);
            this.tblPlanes.Controls.Add(this.lblMikrotikSeleccionado, 2, 0);
            this.tblPlanes.Controls.Add(this.txtMikrotik, 2, 1);
            this.tblPlanes.Controls.Add(this.lblPassword, 2, 3);
            this.tblPlanes.Controls.Add(this.txtPassword, 2, 4);
            this.tblPlanes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblPlanes.Location = new System.Drawing.Point(10, 36);
            this.tblPlanes.Name = "tblPlanes";
            this.tblPlanes.RowCount = 5;
            this.tblPlanes.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblPlanes.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblPlanes.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblPlanes.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblPlanes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPlanes.Size = new System.Drawing.Size(1054, 271);
            this.tblPlanes.TabIndex = 0;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(3, 0);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(121, 28);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Buscar plan:";
            // 
            // txtNombrePlan
            // 
            this.txtNombrePlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNombrePlan.Location = new System.Drawing.Point(3, 59);
            this.txtNombrePlan.Name = "txtNombrePlan";
            this.txtNombrePlan.Size = new System.Drawing.Size(468, 33);
            this.txtNombrePlan.TabIndex = 1;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(3, 98);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 30);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(0, 136);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(474, 18);
            this.progressBar1.TabIndex = 3;
            // 
            // llbNombreUsuario
            // 
            this.llbNombreUsuario.AutoSize = true;
            this.llbNombreUsuario.Location = new System.Drawing.Point(477, 0);
            this.llbNombreUsuario.Name = "llbNombreUsuario";
            this.llbNombreUsuario.Size = new System.Drawing.Size(243, 28);
            this.llbNombreUsuario.TabIndex = 4;
            this.llbNombreUsuario.Text = "*Nombre para el servicio:";
            // 
            // txtNombreServicio
            // 
            this.txtNombreServicio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNombreServicio.Location = new System.Drawing.Point(477, 59);
            this.txtNombreServicio.Name = "txtNombreServicio";
            this.txtNombreServicio.Size = new System.Drawing.Size(362, 33);
            this.txtNombreServicio.TabIndex = 5;
            // 
            // panelContenedor
            // 
            this.tblPlanes.SetColumnSpan(this.panelContenedor, 2);
            this.panelContenedor.Controls.Add(this.dgvPlanes);
            this.panelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedor.Location = new System.Drawing.Point(3, 162);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(836, 106);
            this.panelContenedor.TabIndex = 6;
            // 
            // dgvPlanes
            // 
            this.dgvPlanes.AllowUserToAddRows = false;
            this.dgvPlanes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPlanes.BackgroundColor = System.Drawing.Color.White;
            this.dgvPlanes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPlanes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(42)))), ((int)(((byte)(107)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(42)))), ((int)(((byte)(107)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPlanes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPlanes.ColumnHeadersHeight = 32;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(42)))), ((int)(((byte)(107)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(242)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPlanes.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPlanes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPlanes.EnableHeadersVisualStyles = false;
            this.dgvPlanes.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvPlanes.Location = new System.Drawing.Point(0, 0);
            this.dgvPlanes.Name = "dgvPlanes";
            this.dgvPlanes.RowHeadersVisible = false;
            this.dgvPlanes.RowHeadersWidth = 62;
            this.dgvPlanes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPlanes.Size = new System.Drawing.Size(836, 106);
            this.dgvPlanes.TabIndex = 0;
            this.dgvPlanes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPlanes_CellContentClick);
            // 
            // lblMikrotikSeleccionado
            // 
            this.lblMikrotikSeleccionado.AutoSize = true;
            this.lblMikrotikSeleccionado.Location = new System.Drawing.Point(845, 0);
            this.lblMikrotikSeleccionado.Name = "lblMikrotikSeleccionado";
            this.lblMikrotikSeleccionado.Size = new System.Drawing.Size(134, 56);
            this.lblMikrotikSeleccionado.TabIndex = 7;
            this.lblMikrotikSeleccionado.Text = "Mikrotik seleccionado:";
            // 
            // txtMikrotik
            // 
            this.txtMikrotik.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMikrotik.Location = new System.Drawing.Point(845, 59);
            this.txtMikrotik.Name = "txtMikrotik";
            this.txtMikrotik.ReadOnly = true;
            this.tblPlanes.SetRowSpan(this.txtMikrotik, 2);
            this.txtMikrotik.Size = new System.Drawing.Size(206, 33);
            this.txtMikrotik.TabIndex = 8;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(845, 131);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(111, 28);
            this.lblPassword.TabIndex = 9;
            this.lblPassword.Text = "*Password:";
            this.lblPassword.Visible = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtPassword.Location = new System.Drawing.Point(845, 162);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(206, 33);
            this.txtPassword.TabIndex = 10;
            this.txtPassword.Visible = false;
            // 
            // gbUbicacion
            // 
            this.gbUbicacion.Controls.Add(this.tblUbicacion);
            this.gbUbicacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbUbicacion.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.gbUbicacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(42)))), ((int)(((byte)(107)))));
            this.gbUbicacion.Location = new System.Drawing.Point(13, 477);
            this.gbUbicacion.Name = "gbUbicacion";
            this.gbUbicacion.Padding = new System.Windows.Forms.Padding(10);
            this.gbUbicacion.Size = new System.Drawing.Size(1074, 259);
            this.gbUbicacion.TabIndex = 2;
            this.gbUbicacion.TabStop = false;
            this.gbUbicacion.Text = "Ubicación y Geolocalización";
            // 
            // tblUbicacion
            // 
            this.tblUbicacion.ColumnCount = 2;
            this.tblUbicacion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblUbicacion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblUbicacion.Controls.Add(this.lblCalle, 0, 0);
            this.tblUbicacion.Controls.Add(this.txtDireccion, 0, 1);
            this.tblUbicacion.Controls.Add(this.lblUbicacion, 1, 0);
            this.tblUbicacion.Controls.Add(this.txtDireccionOficial, 1, 1);
            this.tblUbicacion.Controls.Add(this.lblGPS, 0, 2);
            this.tblUbicacion.Controls.Add(this.pnlGPS, 0, 3);
            this.tblUbicacion.Controls.Add(this.lblDireccionSugerida, 0, 4);
            this.tblUbicacion.Controls.Add(this.txtDireccionSugerida, 0, 5);
            this.tblUbicacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblUbicacion.Location = new System.Drawing.Point(10, 36);
            this.tblUbicacion.Name = "tblUbicacion";
            this.tblUbicacion.RowCount = 6;
            this.tblUbicacion.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblUbicacion.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblUbicacion.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblUbicacion.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblUbicacion.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblUbicacion.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblUbicacion.Size = new System.Drawing.Size(1054, 213);
            this.tblUbicacion.TabIndex = 0;
            // 
            // lblCalle
            // 
            this.lblCalle.AutoSize = true;
            this.lblCalle.Location = new System.Drawing.Point(3, 0);
            this.lblCalle.Name = "lblCalle";
            this.lblCalle.Size = new System.Drawing.Size(252, 28);
            this.lblCalle.TabIndex = 0;
            this.lblCalle.Text = "*Como se conoce el lugar:";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDireccion.Location = new System.Drawing.Point(3, 31);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(521, 33);
            this.txtDireccion.TabIndex = 1;
            // 
            // lblUbicacion
            // 
            this.lblUbicacion.AutoSize = true;
            this.lblUbicacion.Location = new System.Drawing.Point(530, 0);
            this.lblUbicacion.Name = "lblUbicacion";
            this.lblUbicacion.Size = new System.Drawing.Size(512, 28);
            this.lblUbicacion.TabIndex = 2;
            this.lblUbicacion.Text = "*Dirección oficial: (Ej: San Salvador Huixcolotla, Puebla)";
            // 
            // txtDireccionOficial
            // 
            this.txtDireccionOficial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDireccionOficial.Location = new System.Drawing.Point(530, 31);
            this.txtDireccionOficial.Name = "txtDireccionOficial";
            this.txtDireccionOficial.Size = new System.Drawing.Size(521, 33);
            this.txtDireccionOficial.TabIndex = 3;
            // 
            // lblGPS
            // 
            this.lblGPS.AutoSize = true;
            this.lblGPS.Location = new System.Drawing.Point(3, 67);
            this.lblGPS.Name = "lblGPS";
            this.lblGPS.Size = new System.Drawing.Size(188, 28);
            this.lblGPS.TabIndex = 4;
            this.lblGPS.Text = "*Coordenadas GPS:";
            // 
            // pnlGPS
            // 
            this.pnlGPS.AutoSize = true;
            this.tblUbicacion.SetColumnSpan(this.pnlGPS, 2);
            this.pnlGPS.Controls.Add(this.lblLatitud);
            this.pnlGPS.Controls.Add(this.txtLatitud);
            this.pnlGPS.Controls.Add(this.lblLongitud);
            this.pnlGPS.Controls.Add(this.txtLongitud);
            this.pnlGPS.Controls.Add(this.CBCoordendadas);
            this.pnlGPS.Controls.Add(this.btnBuscarCoordenadas);
            this.pnlGPS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGPS.Location = new System.Drawing.Point(3, 98);
            this.pnlGPS.Name = "pnlGPS";
            this.pnlGPS.Size = new System.Drawing.Size(1048, 39);
            this.pnlGPS.TabIndex = 5;
            // 
            // lblLatitud
            // 
            this.lblLatitud.AutoSize = true;
            this.lblLatitud.Location = new System.Drawing.Point(0, 6);
            this.lblLatitud.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lblLatitud.Name = "lblLatitud";
            this.lblLatitud.Size = new System.Drawing.Size(44, 28);
            this.lblLatitud.TabIndex = 0;
            this.lblLatitud.Text = "Lat:";
            // 
            // txtLatitud
            // 
            this.txtLatitud.Location = new System.Drawing.Point(47, 3);
            this.txtLatitud.Name = "txtLatitud";
            this.txtLatitud.Size = new System.Drawing.Size(140, 33);
            this.txtLatitud.TabIndex = 1;
            // 
            // lblLongitud
            // 
            this.lblLongitud.AutoSize = true;
            this.lblLongitud.Location = new System.Drawing.Point(200, 6);
            this.lblLongitud.Margin = new System.Windows.Forms.Padding(10, 6, 0, 0);
            this.lblLongitud.Name = "lblLongitud";
            this.lblLongitud.Size = new System.Drawing.Size(51, 28);
            this.lblLongitud.TabIndex = 2;
            this.lblLongitud.Text = "Lng:";
            // 
            // txtLongitud
            // 
            this.txtLongitud.Location = new System.Drawing.Point(254, 3);
            this.txtLongitud.Name = "txtLongitud";
            this.txtLongitud.Size = new System.Drawing.Size(140, 33);
            this.txtLongitud.TabIndex = 3;
            // 
            // CBCoordendadas
            // 
            this.CBCoordendadas.AutoSize = true;
            this.CBCoordendadas.Checked = true;
            this.CBCoordendadas.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CBCoordendadas.Location = new System.Drawing.Point(412, 4);
            this.CBCoordendadas.Margin = new System.Windows.Forms.Padding(15, 4, 0, 0);
            this.CBCoordendadas.Name = "CBCoordendadas";
            this.CBCoordendadas.Size = new System.Drawing.Size(274, 32);
            this.CBCoordendadas.TabIndex = 4;
            this.CBCoordendadas.Text = "¿Buscar por coordenadas?";
            // 
            // btnBuscarCoordenadas
            // 
            this.btnBuscarCoordenadas.Location = new System.Drawing.Point(701, 0);
            this.btnBuscarCoordenadas.Margin = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnBuscarCoordenadas.Name = "btnBuscarCoordenadas";
            this.btnBuscarCoordenadas.Size = new System.Drawing.Size(135, 30);
            this.btnBuscarCoordenadas.TabIndex = 5;
            this.btnBuscarCoordenadas.Text = "Buscar GPS";
            this.btnBuscarCoordenadas.Click += new System.EventHandler(this.btnBuscarCoordenadas_Click);
            // 
            // lblDireccionSugerida
            // 
            this.lblDireccionSugerida.AutoSize = true;
            this.lblDireccionSugerida.Location = new System.Drawing.Point(3, 140);
            this.lblDireccionSugerida.Name = "lblDireccionSugerida";
            this.lblDireccionSugerida.Size = new System.Drawing.Size(185, 28);
            this.lblDireccionSugerida.TabIndex = 6;
            this.lblDireccionSugerida.Text = "Dirección sugerida:";
            // 
            // txtDireccionSugerida
            // 
            this.tblUbicacion.SetColumnSpan(this.txtDireccionSugerida, 2);
            this.txtDireccionSugerida.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDireccionSugerida.Location = new System.Drawing.Point(3, 171);
            this.txtDireccionSugerida.Name = "txtDireccionSugerida";
            this.txtDireccionSugerida.ReadOnly = true;
            this.txtDireccionSugerida.Size = new System.Drawing.Size(1048, 33);
            this.txtDireccionSugerida.TabIndex = 7;
            // 
            // panelBotones
            // 
            this.panelBotones.Controls.Add(this.btnAceptarUbicacion);
            this.panelBotones.Controls.Add(this.btnCancelarDireccion);
            this.panelBotones.Controls.Add(this.btnLupa);
            this.panelBotones.Controls.Add(this.btnGuardar);
            this.panelBotones.Controls.Add(this.gMapOculto);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBotones.Location = new System.Drawing.Point(13, 742);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.panelBotones.Size = new System.Drawing.Size(1074, 45);
            this.panelBotones.TabIndex = 3;
            // 
            // btnAceptarUbicacion
            // 
            this.btnAceptarUbicacion.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAceptarUbicacion.Location = new System.Drawing.Point(280, 5);
            this.btnAceptarUbicacion.Name = "btnAceptarUbicacion";
            this.btnAceptarUbicacion.Size = new System.Drawing.Size(150, 35);
            this.btnAceptarUbicacion.TabIndex = 0;
            this.btnAceptarUbicacion.Text = "Confirmar dirección";
            this.btnAceptarUbicacion.Click += new System.EventHandler(this.btnAceptarUbicacion_Click);
            // 
            // btnCancelarDireccion
            // 
            this.btnCancelarDireccion.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCancelarDireccion.Location = new System.Drawing.Point(130, 5);
            this.btnCancelarDireccion.Name = "btnCancelarDireccion";
            this.btnCancelarDireccion.Size = new System.Drawing.Size(150, 35);
            this.btnCancelarDireccion.TabIndex = 1;
            this.btnCancelarDireccion.Text = "Cancelar dirección";
            this.btnCancelarDireccion.Click += new System.EventHandler(this.btnCancelarDireccion_Click);
            // 
            // btnLupa
            // 
            this.btnLupa.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnLupa.Image = ((System.Drawing.Image)(resources.GetObject("btnLupa.Image")));
            this.btnLupa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLupa.Location = new System.Drawing.Point(0, 5);
            this.btnLupa.Name = "btnLupa";
            this.btnLupa.Size = new System.Drawing.Size(130, 35);
            this.btnLupa.TabIndex = 2;
            this.btnLupa.Text = "Ver mapa";
            this.btnLupa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLupa.Click += new System.EventHandler(this.btnLupa_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnGuardar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnGuardar.Enabled = false;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(934, 5);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(140, 35);
            this.btnGuardar.TabIndex = 3;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // gMapOculto
            // 
            this.gMapOculto.Bearing = 0F;
            this.gMapOculto.CanDragMap = true;
            this.gMapOculto.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapOculto.GrayScaleMode = false;
            this.gMapOculto.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapOculto.LevelsKeepInMemory = 5;
            this.gMapOculto.Location = new System.Drawing.Point(0, 0);
            this.gMapOculto.MarkersEnabled = true;
            this.gMapOculto.MaxZoom = 2;
            this.gMapOculto.MinZoom = 2;
            this.gMapOculto.MouseWheelZoomEnabled = true;
            this.gMapOculto.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapOculto.Name = "gMapOculto";
            this.gMapOculto.NegativeMode = false;
            this.gMapOculto.PolygonsEnabled = true;
            this.gMapOculto.RetryLoadTile = 0;
            this.gMapOculto.RoutesEnabled = true;
            this.gMapOculto.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapOculto.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapOculto.ShowTileGridLines = false;
            this.gMapOculto.Size = new System.Drawing.Size(150, 150);
            this.gMapOculto.TabIndex = 4;
            this.gMapOculto.Visible = false;
            this.gMapOculto.Zoom = 0D;
            // 
            // PreregistroCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1100, 800);
            this.Controls.Add(this.tblMainLayout);
            this.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.MinimumSize = new System.Drawing.Size(950, 650);
            this.Name = "PreregistroCliente";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PreregistroCliente_Load);
            this.tblMainLayout.ResumeLayout(false);
            this.gbDatosCliente.ResumeLayout(false);
            this.tblDatosCliente.ResumeLayout(false);
            this.tblDatosCliente.PerformLayout();
            this.gbPlanes.ResumeLayout(false);
            this.tblPlanes.ResumeLayout(false);
            this.tblPlanes.PerformLayout();
            this.panelContenedor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlanes)).EndInit();
            this.gbUbicacion.ResumeLayout(false);
            this.tblUbicacion.ResumeLayout(false);
            this.tblUbicacion.PerformLayout();
            this.pnlGPS.ResumeLayout(false);
            this.pnlGPS.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblMainLayout;
        private System.Windows.Forms.TableLayoutPanel tblDatosCliente;
        private System.Windows.Forms.TableLayoutPanel tblPlanes;
        private System.Windows.Forms.TableLayoutPanel tblUbicacion;
        private System.Windows.Forms.FlowLayoutPanel pnlGPS;

        private System.Windows.Forms.TextBox txtTelefono2;
        private System.Windows.Forms.Label lblMensaje4;
        private System.Windows.Forms.TextBox txtTelefono1;
        private System.Windows.Forms.Label lblMensaje3;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Label lblCorreo;
        private System.Windows.Forms.TextBox txtNombreCliente;
        private System.Windows.Forms.Label lblMensaje1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtNombrePlan;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.DataGridView dgvPlanes;
        private System.Windows.Forms.TextBox txtNombreServicio;
        private System.Windows.Forms.Label llbNombreUsuario;
        private System.Windows.Forms.CheckBox CBCoordendadas;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label lblCalle;
        private System.Windows.Forms.Label lblUbicacion;
        private System.Windows.Forms.TextBox txtDireccionSugerida;
        private System.Windows.Forms.Label lblDireccionSugerida;
        private System.Windows.Forms.Label lblLongitud;
        private System.Windows.Forms.Label lblLatitud;
        private System.Windows.Forms.Label lblGPS;
        private System.Windows.Forms.TextBox txtDireccionOficial;
        private System.Windows.Forms.Button btnBuscarCoordenadas;
        private System.Windows.Forms.TextBox txtLongitud;
        private System.Windows.Forms.TextBox txtLatitud;
        private System.Windows.Forms.Button btnCancelarDireccion;
        private System.Windows.Forms.Button btnAceptarUbicacion;
        private System.Windows.Forms.Button btnLupa;
        private System.Windows.Forms.GroupBox gbDatosCliente;
        private System.Windows.Forms.GroupBox gbPlanes;
        private System.Windows.Forms.GroupBox gbUbicacion;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnGuardar;
        private GMap.NET.WindowsForms.GMapControl gMapOculto;
        private System.Windows.Forms.Label lblMikrotikSeleccionado;
        private System.Windows.Forms.TextBox txtMikrotik;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
    }
}