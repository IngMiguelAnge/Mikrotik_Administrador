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
            this.gbDatosCliente = new System.Windows.Forms.GroupBox();
            this.lblMensaje1 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblCorreo = new System.Windows.Forms.Label();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.lblMensaje3 = new System.Windows.Forms.Label();
            this.txtTelefono1 = new System.Windows.Forms.TextBox();
            this.lblMensaje4 = new System.Windows.Forms.Label();
            this.txtTelefono2 = new System.Windows.Forms.TextBox();
            this.gbPlanes = new System.Windows.Forms.GroupBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtBuscarPlan = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.llbNombreUsuario = new System.Windows.Forms.Label();
            this.txtNombreServicio = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.dgvPlanes = new System.Windows.Forms.DataGridView();
            this.gbUbicacion = new System.Windows.Forms.GroupBox();
            this.lblCalle = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.lblUbicacion = new System.Windows.Forms.Label();
            this.lblEjemplo = new System.Windows.Forms.Label();
            this.txtDireccionOficial = new System.Windows.Forms.TextBox();
            this.lblGPS = new System.Windows.Forms.Label();
            this.lblLatitud = new System.Windows.Forms.Label();
            this.txtLatitud = new System.Windows.Forms.TextBox();
            this.lblLongitud = new System.Windows.Forms.Label();
            this.txtLongitud = new System.Windows.Forms.TextBox();
            this.CBCoordendadas = new System.Windows.Forms.CheckBox();
            this.btnBuscarCoordenadas = new System.Windows.Forms.Button();
            this.lblDireccionSugerida = new System.Windows.Forms.Label();
            this.txtDireccionSugerida = new System.Windows.Forms.TextBox();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.gMapOculto = new GMap.NET.WindowsForms.GMapControl();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnAceptarUbicacion = new System.Windows.Forms.Button();
            this.btnCancelarDireccion = new System.Windows.Forms.Button();
            this.btnLupa = new System.Windows.Forms.Button();
            this.lblMikrotikSeleccionado = new System.Windows.Forms.Label();
            this.txtMikrotik = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.gbDatosCliente.SuspendLayout();
            this.gbPlanes.SuspendLayout();
            this.panelContenedor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlanes)).BeginInit();
            this.gbUbicacion.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDatosCliente
            // 
            this.gbDatosCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDatosCliente.Controls.Add(this.lblMensaje1);
            this.gbDatosCliente.Controls.Add(this.txtNombre);
            this.gbDatosCliente.Controls.Add(this.lblCorreo);
            this.gbDatosCliente.Controls.Add(this.txtCorreo);
            this.gbDatosCliente.Controls.Add(this.lblMensaje3);
            this.gbDatosCliente.Controls.Add(this.txtTelefono1);
            this.gbDatosCliente.Controls.Add(this.lblMensaje4);
            this.gbDatosCliente.Controls.Add(this.txtTelefono2);
            this.gbDatosCliente.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.gbDatosCliente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(42)))), ((int)(((byte)(107)))));
            this.gbDatosCliente.Location = new System.Drawing.Point(20, 15);
            this.gbDatosCliente.Name = "gbDatosCliente";
            this.gbDatosCliente.Size = new System.Drawing.Size(1005, 155);
            this.gbDatosCliente.TabIndex = 0;
            this.gbDatosCliente.TabStop = false;
            this.gbDatosCliente.Text = "Datos Personales del Cliente";
            // 
            // lblMensaje1
            // 
            this.lblMensaje1.AutoSize = true;
            this.lblMensaje1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblMensaje1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblMensaje1.Location = new System.Drawing.Point(20, 30);
            this.lblMensaje1.Name = "lblMensaje1";
            this.lblMensaje1.Size = new System.Drawing.Size(93, 25);
            this.lblMensaje1.TabIndex = 0;
            this.lblMensaje1.Text = "*Nombre:";
            // 
            // txtNombre
            // 
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtNombre.Location = new System.Drawing.Point(23, 53);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(430, 33);
            this.txtNombre.TabIndex = 1;
            // 
            // lblCorreo
            // 
            this.lblCorreo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCorreo.AutoSize = true;
            this.lblCorreo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblCorreo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblCorreo.Location = new System.Drawing.Point(540, 30);
            this.lblCorreo.Name = "lblCorreo";
            this.lblCorreo.Size = new System.Drawing.Size(171, 25);
            this.lblCorreo.TabIndex = 2;
            this.lblCorreo.Text = "Correo electrónico:";
            // 
            // txtCorreo
            // 
            this.txtCorreo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCorreo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtCorreo.Location = new System.Drawing.Point(543, 53);
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.Size = new System.Drawing.Size(435, 33);
            this.txtCorreo.TabIndex = 2;
            // 
            // lblMensaje3
            // 
            this.lblMensaje3.AutoSize = true;
            this.lblMensaje3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblMensaje3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblMensaje3.Location = new System.Drawing.Point(20, 90);
            this.lblMensaje3.Name = "lblMensaje3";
            this.lblMensaje3.Size = new System.Drawing.Size(75, 25);
            this.lblMensaje3.TabIndex = 3;
            this.lblMensaje3.Text = "Celular:";
            // 
            // txtTelefono1
            // 
            this.txtTelefono1.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTelefono1.Location = new System.Drawing.Point(23, 113);
            this.txtTelefono1.Name = "txtTelefono1";
            this.txtTelefono1.Size = new System.Drawing.Size(430, 33);
            this.txtTelefono1.TabIndex = 3;
            // 
            // lblMensaje4
            // 
            this.lblMensaje4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMensaje4.AutoSize = true;
            this.lblMensaje4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblMensaje4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblMensaje4.Location = new System.Drawing.Point(540, 90);
            this.lblMensaje4.Name = "lblMensaje4";
            this.lblMensaje4.Size = new System.Drawing.Size(184, 25);
            this.lblMensaje4.TabIndex = 4;
            this.lblMensaje4.Text = "Teléfono secundario:";
            // 
            // txtTelefono2
            // 
            this.txtTelefono2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTelefono2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTelefono2.Location = new System.Drawing.Point(543, 113);
            this.txtTelefono2.Name = "txtTelefono2";
            this.txtTelefono2.Size = new System.Drawing.Size(435, 33);
            this.txtTelefono2.TabIndex = 4;
            // 
            // gbPlanes
            // 
            this.gbPlanes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPlanes.Controls.Add(this.txtPassword);
            this.gbPlanes.Controls.Add(this.lblPassword);
            this.gbPlanes.Controls.Add(this.txtMikrotik);
            this.gbPlanes.Controls.Add(this.lblMikrotikSeleccionado);
            this.gbPlanes.Controls.Add(this.lblNombre);
            this.gbPlanes.Controls.Add(this.txtBuscarPlan);
            this.gbPlanes.Controls.Add(this.btnBuscar);
            this.gbPlanes.Controls.Add(this.llbNombreUsuario);
            this.gbPlanes.Controls.Add(this.txtNombreServicio);
            this.gbPlanes.Controls.Add(this.progressBar1);
            this.gbPlanes.Controls.Add(this.panelContenedor);
            this.gbPlanes.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.gbPlanes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(42)))), ((int)(((byte)(107)))));
            this.gbPlanes.Location = new System.Drawing.Point(20, 180);
            this.gbPlanes.Name = "gbPlanes";
            this.gbPlanes.Size = new System.Drawing.Size(1005, 297);
            this.gbPlanes.TabIndex = 1;
            this.gbPlanes.TabStop = false;
            this.gbPlanes.Text = "Selección de Plan";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblNombre.Location = new System.Drawing.Point(20, 28);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(111, 25);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Buscar plan:";
            // 
            // txtBuscarPlan
            // 
            this.txtBuscarPlan.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtBuscarPlan.Location = new System.Drawing.Point(23, 50);
            this.txtBuscarPlan.Name = "txtBuscarPlan";
            this.txtBuscarPlan.Size = new System.Drawing.Size(280, 33);
            this.txtBuscarPlan.TabIndex = 5;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(42)))), ((int)(((byte)(107)))));
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(341, 42);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(90, 41);
            this.btnBuscar.TabIndex = 6;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // llbNombreUsuario
            // 
            this.llbNombreUsuario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llbNombreUsuario.AutoSize = true;
            this.llbNombreUsuario.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.llbNombreUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.llbNombreUsuario.Location = new System.Drawing.Point(496, 28);
            this.llbNombreUsuario.Name = "llbNombreUsuario";
            this.llbNombreUsuario.Size = new System.Drawing.Size(223, 25);
            this.llbNombreUsuario.TabIndex = 7;
            this.llbNombreUsuario.Text = "*Nombre para el servicio:";
            // 
            // txtNombreServicio
            // 
            this.txtNombreServicio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombreServicio.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtNombreServicio.Location = new System.Drawing.Point(501, 50);
            this.txtNombreServicio.Name = "txtNombreServicio";
            this.txtNombreServicio.Size = new System.Drawing.Size(313, 33);
            this.txtNombreServicio.TabIndex = 7;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(23, 94);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(377, 8);
            this.progressBar1.TabIndex = 8;
            // 
            // panelContenedor
            // 
            this.panelContenedor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContenedor.Controls.Add(this.dgvPlanes);
            this.panelContenedor.Location = new System.Drawing.Point(23, 116);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(778, 166);
            this.panelContenedor.TabIndex = 9;
            // 
            // dgvPlanes
            // 
            this.dgvPlanes.AllowUserToAddRows = false;
            this.dgvPlanes.BackgroundColor = System.Drawing.Color.White;
            this.dgvPlanes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPlanes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPlanes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPlanes.ColumnHeadersHeight = 30;
            this.dgvPlanes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
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
            this.dgvPlanes.RowTemplate.Height = 28;
            this.dgvPlanes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPlanes.Size = new System.Drawing.Size(778, 166);
            this.dgvPlanes.TabIndex = 0;
            this.dgvPlanes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPlanes_CellContentClick);
            // 
            // gbUbicacion
            // 
            this.gbUbicacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbUbicacion.Controls.Add(this.lblCalle);
            this.gbUbicacion.Controls.Add(this.txtDireccion);
            this.gbUbicacion.Controls.Add(this.lblUbicacion);
            this.gbUbicacion.Controls.Add(this.lblEjemplo);
            this.gbUbicacion.Controls.Add(this.txtDireccionOficial);
            this.gbUbicacion.Controls.Add(this.lblGPS);
            this.gbUbicacion.Controls.Add(this.lblLatitud);
            this.gbUbicacion.Controls.Add(this.txtLatitud);
            this.gbUbicacion.Controls.Add(this.lblLongitud);
            this.gbUbicacion.Controls.Add(this.txtLongitud);
            this.gbUbicacion.Controls.Add(this.CBCoordendadas);
            this.gbUbicacion.Controls.Add(this.btnBuscarCoordenadas);
            this.gbUbicacion.Controls.Add(this.lblDireccionSugerida);
            this.gbUbicacion.Controls.Add(this.txtDireccionSugerida);
            this.gbUbicacion.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.gbUbicacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(42)))), ((int)(((byte)(107)))));
            this.gbUbicacion.Location = new System.Drawing.Point(20, 483);
            this.gbUbicacion.Name = "gbUbicacion";
            this.gbUbicacion.Size = new System.Drawing.Size(1005, 240);
            this.gbUbicacion.TabIndex = 2;
            this.gbUbicacion.TabStop = false;
            this.gbUbicacion.Text = "Ubicación y Geolocalización";
            // 
            // lblCalle
            // 
            this.lblCalle.AutoSize = true;
            this.lblCalle.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblCalle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblCalle.Location = new System.Drawing.Point(20, 28);
            this.lblCalle.Name = "lblCalle";
            this.lblCalle.Size = new System.Drawing.Size(229, 25);
            this.lblCalle.TabIndex = 0;
            this.lblCalle.Text = "*Como se conoce el lugar:";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDireccion.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtDireccion.Location = new System.Drawing.Point(23, 50);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(377, 33);
            this.txtDireccion.TabIndex = 10;
            // 
            // lblUbicacion
            // 
            this.lblUbicacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUbicacion.AutoSize = true;
            this.lblUbicacion.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblUbicacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblUbicacion.Location = new System.Drawing.Point(446, 30);
            this.lblUbicacion.Name = "lblUbicacion";
            this.lblUbicacion.Size = new System.Drawing.Size(156, 25);
            this.lblUbicacion.TabIndex = 11;
            this.lblUbicacion.Text = "*Dirección oficial:";
            // 
            // lblEjemplo
            // 
            this.lblEjemplo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEjemplo.AutoSize = true;
            this.lblEjemplo.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblEjemplo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(145)))));
            this.lblEjemplo.Location = new System.Drawing.Point(616, 28);
            this.lblEjemplo.Name = "lblEjemplo";
            this.lblEjemplo.Size = new System.Drawing.Size(268, 21);
            this.lblEjemplo.TabIndex = 12;
            this.lblEjemplo.Text = "(Ej: San Salvador Huixcolotla, Puebla)";
            // 
            // txtDireccionOficial
            // 
            this.txtDireccionOficial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDireccionOficial.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtDireccionOficial.Location = new System.Drawing.Point(445, 50);
            this.txtDireccionOficial.Name = "txtDireccionOficial";
            this.txtDireccionOficial.Size = new System.Drawing.Size(533, 33);
            this.txtDireccionOficial.TabIndex = 11;
            // 
            // lblGPS
            // 
            this.lblGPS.AutoSize = true;
            this.lblGPS.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblGPS.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblGPS.Location = new System.Drawing.Point(20, 95);
            this.lblGPS.Name = "lblGPS";
            this.lblGPS.Size = new System.Drawing.Size(172, 25);
            this.lblGPS.TabIndex = 13;
            this.lblGPS.Text = "*Coordenadas GPS:";
            // 
            // lblLatitud
            // 
            this.lblLatitud.AutoSize = true;
            this.lblLatitud.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblLatitud.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblLatitud.Location = new System.Drawing.Point(19, 133);
            this.lblLatitud.Name = "lblLatitud";
            this.lblLatitud.Size = new System.Drawing.Size(67, 23);
            this.lblLatitud.TabIndex = 14;
            this.lblLatitud.Text = "Latitud:";
            // 
            // txtLatitud
            // 
            this.txtLatitud.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtLatitud.Location = new System.Drawing.Point(109, 123);
            this.txtLatitud.Name = "txtLatitud";
            this.txtLatitud.Size = new System.Drawing.Size(140, 33);
            this.txtLatitud.TabIndex = 12;
            // 
            // lblLongitud
            // 
            this.lblLongitud.AutoSize = true;
            this.lblLongitud.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblLongitud.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblLongitud.Location = new System.Drawing.Point(255, 133);
            this.lblLongitud.Name = "lblLongitud";
            this.lblLongitud.Size = new System.Drawing.Size(82, 23);
            this.lblLongitud.TabIndex = 15;
            this.lblLongitud.Text = "Longitud:";
            // 
            // txtLongitud
            // 
            this.txtLongitud.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtLongitud.Location = new System.Drawing.Point(341, 123);
            this.txtLongitud.Name = "txtLongitud";
            this.txtLongitud.Size = new System.Drawing.Size(140, 33);
            this.txtLongitud.TabIndex = 13;
            // 
            // CBCoordendadas
            // 
            this.CBCoordendadas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CBCoordendadas.AutoSize = true;
            this.CBCoordendadas.Checked = true;
            this.CBCoordendadas.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CBCoordendadas.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CBCoordendadas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.CBCoordendadas.Location = new System.Drawing.Point(568, 127);
            this.CBCoordendadas.Name = "CBCoordendadas";
            this.CBCoordendadas.Size = new System.Drawing.Size(246, 29);
            this.CBCoordendadas.TabIndex = 16;
            this.CBCoordendadas.Text = "¿Buscar por coordenadas?";
            // 
            // btnBuscarCoordenadas
            // 
            this.btnBuscarCoordenadas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscarCoordenadas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(233)))), ((int)(((byte)(240)))));
            this.btnBuscarCoordenadas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarCoordenadas.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnBuscarCoordenadas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnBuscarCoordenadas.Location = new System.Drawing.Point(833, 114);
            this.btnBuscarCoordenadas.Name = "btnBuscarCoordenadas";
            this.btnBuscarCoordenadas.Size = new System.Drawing.Size(123, 42);
            this.btnBuscarCoordenadas.TabIndex = 14;
            this.btnBuscarCoordenadas.Text = "Buscar GPS";
            this.btnBuscarCoordenadas.UseVisualStyleBackColor = false;
            this.btnBuscarCoordenadas.Click += new System.EventHandler(this.btnBuscarCoordenadas_Click);
            // 
            // lblDireccionSugerida
            // 
            this.lblDireccionSugerida.AutoSize = true;
            this.lblDireccionSugerida.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDireccionSugerida.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblDireccionSugerida.Location = new System.Drawing.Point(20, 165);
            this.lblDireccionSugerida.Name = "lblDireccionSugerida";
            this.lblDireccionSugerida.Size = new System.Drawing.Size(171, 25);
            this.lblDireccionSugerida.TabIndex = 17;
            this.lblDireccionSugerida.Text = "Dirección sugerida:";
            // 
            // txtDireccionSugerida
            // 
            this.txtDireccionSugerida.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDireccionSugerida.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.txtDireccionSugerida.Enabled = false;
            this.txtDireccionSugerida.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtDireccionSugerida.Location = new System.Drawing.Point(23, 188);
            this.txtDireccionSugerida.Name = "txtDireccionSugerida";
            this.txtDireccionSugerida.ReadOnly = true;
            this.txtDireccionSugerida.Size = new System.Drawing.Size(955, 33);
            this.txtDireccionSugerida.TabIndex = 15;
            // 
            // panelBotones
            // 
            this.panelBotones.Controls.Add(this.gMapOculto);
            this.panelBotones.Controls.Add(this.btnGuardar);
            this.panelBotones.Controls.Add(this.btnAceptarUbicacion);
            this.panelBotones.Controls.Add(this.btnCancelarDireccion);
            this.panelBotones.Controls.Add(this.btnLupa);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotones.Location = new System.Drawing.Point(0, 729);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(1045, 81);
            this.panelBotones.TabIndex = 3;
            // 
            // gMapOculto
            // 
            this.gMapOculto.Bearing = 0F;
            this.gMapOculto.CanDragMap = true;
            this.gMapOculto.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapOculto.GrayScaleMode = false;
            this.gMapOculto.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapOculto.LevelsKeepInMemory = 5;
            this.gMapOculto.Location = new System.Drawing.Point(565, 9);
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
            this.gMapOculto.Size = new System.Drawing.Size(57, 37);
            this.gMapOculto.TabIndex = 19;
            this.gMapOculto.Visible = false;
            this.gMapOculto.Zoom = 0D;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnGuardar.Enabled = false;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(866, 13);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(110, 46);
            this.btnGuardar.TabIndex = 10;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnAceptarUbicacion
            // 
            this.btnAceptarUbicacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(42)))), ((int)(((byte)(107)))));
            this.btnAceptarUbicacion.Enabled = false;
            this.btnAceptarUbicacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptarUbicacion.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnAceptarUbicacion.ForeColor = System.Drawing.Color.White;
            this.btnAceptarUbicacion.Location = new System.Drawing.Point(20, 12);
            this.btnAceptarUbicacion.Name = "btnAceptarUbicacion";
            this.btnAceptarUbicacion.Size = new System.Drawing.Size(160, 47);
            this.btnAceptarUbicacion.TabIndex = 16;
            this.btnAceptarUbicacion.Text = "Confirmar dirección";
            this.btnAceptarUbicacion.UseVisualStyleBackColor = false;
            this.btnAceptarUbicacion.Click += new System.EventHandler(this.btnAceptarUbicacion_Click);
            // 
            // btnCancelarDireccion
            // 
            this.btnCancelarDireccion.BackColor = System.Drawing.Color.White;
            this.btnCancelarDireccion.Enabled = false;
            this.btnCancelarDireccion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarDireccion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancelarDireccion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.btnCancelarDireccion.Location = new System.Drawing.Point(190, 12);
            this.btnCancelarDireccion.Name = "btnCancelarDireccion";
            this.btnCancelarDireccion.Size = new System.Drawing.Size(150, 47);
            this.btnCancelarDireccion.TabIndex = 17;
            this.btnCancelarDireccion.Text = "Cancelar dirección";
            this.btnCancelarDireccion.UseVisualStyleBackColor = false;
            this.btnCancelarDireccion.Click += new System.EventHandler(this.btnCancelarDireccion_Click);
            // 
            // btnLupa
            // 
            this.btnLupa.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnLupa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnLupa.Image = ((System.Drawing.Image)(resources.GetObject("btnLupa.Image")));
            this.btnLupa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLupa.Location = new System.Drawing.Point(350, 9);
            this.btnLupa.Name = "btnLupa";
            this.btnLupa.Size = new System.Drawing.Size(179, 50);
            this.btnLupa.TabIndex = 18;
            this.btnLupa.Text = "Ver mapa";
            this.btnLupa.UseVisualStyleBackColor = true;
            this.btnLupa.Click += new System.EventHandler(this.btnLupa_Click);
            // 
            // lblMikrotikSeleccionado
            // 
            this.lblMikrotikSeleccionado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMikrotikSeleccionado.AutoSize = true;
            this.lblMikrotikSeleccionado.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblMikrotikSeleccionado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblMikrotikSeleccionado.Location = new System.Drawing.Point(801, 94);
            this.lblMikrotikSeleccionado.Name = "lblMikrotikSeleccionado";
            this.lblMikrotikSeleccionado.Size = new System.Drawing.Size(198, 25);
            this.lblMikrotikSeleccionado.TabIndex = 10;
            this.lblMikrotikSeleccionado.Text = "Mikrotik seleccionado:";
            // 
            // txtMikrotik
            // 
            this.txtMikrotik.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMikrotik.Enabled = false;
            this.txtMikrotik.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtMikrotik.Location = new System.Drawing.Point(822, 122);
            this.txtMikrotik.Multiline = true;
            this.txtMikrotik.Name = "txtMikrotik";
            this.txtMikrotik.Size = new System.Drawing.Size(166, 103);
            this.txtMikrotik.TabIndex = 11;
            // 
            // lblPassword
            // 
            this.lblPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblPassword.Location = new System.Drawing.Point(807, 228);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(102, 25);
            this.lblPassword.TabIndex = 12;
            this.lblPassword.Text = "*Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtPassword.Location = new System.Drawing.Point(822, 256);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(166, 33);
            this.txtPassword.TabIndex = 13;
            // 
            // PreregistroCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1045, 810);
            this.Controls.Add(this.gbDatosCliente);
            this.Controls.Add(this.gbPlanes);
            this.Controls.Add(this.gbUbicacion);
            this.Controls.Add(this.panelBotones);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(980, 500);
            this.Name = "PreregistroCliente";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.PreregistroCliente_Load);
            this.gbDatosCliente.ResumeLayout(false);
            this.gbDatosCliente.PerformLayout();
            this.gbPlanes.ResumeLayout(false);
            this.gbPlanes.PerformLayout();
            this.panelContenedor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlanes)).EndInit();
            this.gbUbicacion.ResumeLayout(false);
            this.gbUbicacion.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.TextBox txtTelefono2;
        private System.Windows.Forms.Label lblMensaje4;
        private System.Windows.Forms.TextBox txtTelefono1;
        private System.Windows.Forms.Label lblMensaje3;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Label lblCorreo;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblMensaje1;
        private System.Windows.Forms.Label lblSeleccionePlan;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscarPlan;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.DataGridView dgvPlanes;
        private System.Windows.Forms.TextBox txtNombreServicio;
        private System.Windows.Forms.Label llbNombreUsuario;
        private System.Windows.Forms.CheckBox CBCoordendadas;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label lblCalle;
        private System.Windows.Forms.Label lblEjemplo;
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