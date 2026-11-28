namespace Mikrotik_Administrador.Catalogos
{
    partial class Instalacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Instalacion));
            this.lblFecha = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.lblNombre = new System.Windows.Forms.Label();
            this.CBInstalador = new System.Windows.Forms.ComboBox();
            this.lblModalidad = new System.Windows.Forms.Label();
            this.CBModalidad = new System.Windows.Forms.ComboBox();
            this.lblTipoInstalacion = new System.Windows.Forms.Label();
            this.RBAntena = new System.Windows.Forms.RadioButton();
            this.RBFibra = new System.Windows.Forms.RadioButton();
            this.lblAntena = new System.Windows.Forms.Label();
            this.txtAntena = new System.Windows.Forms.TextBox();
            this.lblRB = new System.Windows.Forms.Label();
            this.txtRB = new System.Windows.Forms.TextBox();
            this.lblSector = new System.Windows.Forms.Label();
            this.txtSector = new System.Windows.Forms.TextBox();
            this.lblTubo = new System.Windows.Forms.Label();
            this.txtTubo = new System.Windows.Forms.TextBox();
            this.lblCaja = new System.Windows.Forms.Label();
            this.NUDCaja = new System.Windows.Forms.NumericUpDown();
            this.lblPuerto = new System.Windows.Forms.Label();
            this.lblSpliter = new System.Windows.Forms.Label();
            this.NUDSpliter = new System.Windows.Forms.NumericUpDown();
            this.lblPuertoInterno = new System.Windows.Forms.Label();
            this.NUDPuertoInterno = new System.Windows.Forms.NumericUpDown();
            this.lblCable = new System.Windows.Forms.Label();
            this.txtCable = new System.Windows.Forms.TextBox();
            this.lblRouter = new System.Windows.Forms.Label();
            this.txtRouter = new System.Windows.Forms.TextBox();
            this.lblCargador = new System.Windows.Forms.Label();
            this.txtCargador = new System.Windows.Forms.TextBox();
            this.lblIP = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.lblLatitud = new System.Windows.Forms.Label();
            this.txtLatitud = new System.Windows.Forms.TextBox();
            this.lblLongitud = new System.Windows.Forms.Label();
            this.txtLongitud = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.NUDTotal = new System.Windows.Forms.NumericUpDown();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtPuertoExterno = new System.Windows.Forms.TextBox();
            this.btnLupa = new System.Windows.Forms.Button();
            this.lblNota = new System.Windows.Forms.Label();
            this.txtNota = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.NUDCaja)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDSpliter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDPuertoInterno)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDTotal)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFecha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblFecha.Location = new System.Drawing.Point(28, 83);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(182, 25);
            this.lblFecha.TabIndex = 4;
            this.lblFecha.Text = "Fecha de instalación:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpFecha.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.Location = new System.Drawing.Point(28, 109);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(280, 33);
            this.dtpFecha.TabIndex = 5;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblNombre.Location = new System.Drawing.Point(28, 15);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(108, 25);
            this.lblNombre.TabIndex = 2;
            this.lblNombre.Text = "*Instalador:";
            // 
            // CBInstalador
            // 
            this.CBInstalador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBInstalador.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.CBInstalador.FormattingEnabled = true;
            this.CBInstalador.Location = new System.Drawing.Point(28, 41);
            this.CBInstalador.Name = "CBInstalador";
            this.CBInstalador.Size = new System.Drawing.Size(280, 33);
            this.CBInstalador.TabIndex = 3;
            // 
            // lblModalidad
            // 
            this.lblModalidad.AutoSize = true;
            this.lblModalidad.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblModalidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblModalidad.Location = new System.Drawing.Point(28, 151);
            this.lblModalidad.Name = "lblModalidad";
            this.lblModalidad.Size = new System.Drawing.Size(113, 25);
            this.lblModalidad.TabIndex = 6;
            this.lblModalidad.Text = "*Modalidad:";
            // 
            // CBModalidad
            // 
            this.CBModalidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBModalidad.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.CBModalidad.FormattingEnabled = true;
            this.CBModalidad.Items.AddRange(new object[] {
            "Seleccione",
            "MIGRACIÓN",
            "INSTALACIÓN",
            "RECOMENDACIÓN",
            "PROMOCIÓN"});
            this.CBModalidad.Location = new System.Drawing.Point(28, 177);
            this.CBModalidad.Name = "CBModalidad";
            this.CBModalidad.Size = new System.Drawing.Size(280, 33);
            this.CBModalidad.TabIndex = 7;
            // 
            // lblTipoInstalacion
            // 
            this.lblTipoInstalacion.AutoSize = true;
            this.lblTipoInstalacion.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblTipoInstalacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblTipoInstalacion.Location = new System.Drawing.Point(360, 18);
            this.lblTipoInstalacion.Name = "lblTipoInstalacion";
            this.lblTipoInstalacion.Size = new System.Drawing.Size(181, 25);
            this.lblTipoInstalacion.TabIndex = 8;
            this.lblTipoInstalacion.Text = "*Tipo de Tecnología:";
            // 
            // RBAntena
            // 
            this.RBAntena.AutoSize = true;
            this.RBAntena.Checked = true;
            this.RBAntena.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.RBAntena.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.RBAntena.Location = new System.Drawing.Point(360, 46);
            this.RBAntena.Name = "RBAntena";
            this.RBAntena.Size = new System.Drawing.Size(99, 29);
            this.RBAntena.TabIndex = 9;
            this.RBAntena.TabStop = true;
            this.RBAntena.Text = "Antena";
            this.RBAntena.UseVisualStyleBackColor = true;
            this.RBAntena.CheckedChanged += new System.EventHandler(this.RBAntena_CheckedChanged);
            // 
            // RBFibra
            // 
            this.RBFibra.AutoSize = true;
            this.RBFibra.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.RBFibra.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.RBFibra.Location = new System.Drawing.Point(500, 46);
            this.RBFibra.Name = "RBFibra";
            this.RBFibra.Size = new System.Drawing.Size(80, 29);
            this.RBFibra.TabIndex = 10;
            this.RBFibra.Text = "Fibra";
            this.RBFibra.UseVisualStyleBackColor = true;
            this.RBFibra.CheckedChanged += new System.EventHandler(this.RBFibra_CheckedChanged);
            // 
            // lblAntena
            // 
            this.lblAntena.AutoSize = true;
            this.lblAntena.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblAntena.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblAntena.Location = new System.Drawing.Point(357, 86);
            this.lblAntena.Name = "lblAntena";
            this.lblAntena.Size = new System.Drawing.Size(83, 25);
            this.lblAntena.TabIndex = 1;
            this.lblAntena.Text = "*Antena:";
            // 
            // txtAntena
            // 
            this.txtAntena.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAntena.Location = new System.Drawing.Point(357, 112);
            this.txtAntena.MaxLength = 50;
            this.txtAntena.Name = "txtAntena";
            this.txtAntena.Size = new System.Drawing.Size(280, 34);
            this.txtAntena.TabIndex = 12;
            // 
            // lblRB
            // 
            this.lblRB.AutoSize = true;
            this.lblRB.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblRB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblRB.Location = new System.Drawing.Point(357, 154);
            this.lblRB.Name = "lblRB";
            this.lblRB.Size = new System.Drawing.Size(161, 25);
            this.lblRB.TabIndex = 13;
            this.lblRB.Text = "RB (RouterBoard):";
            // 
            // txtRB
            // 
            this.txtRB.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtRB.Location = new System.Drawing.Point(357, 180);
            this.txtRB.MaxLength = 50;
            this.txtRB.Name = "txtRB";
            this.txtRB.Size = new System.Drawing.Size(280, 34);
            this.txtRB.TabIndex = 14;
            // 
            // lblSector
            // 
            this.lblSector.AutoSize = true;
            this.lblSector.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSector.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblSector.Location = new System.Drawing.Point(357, 222);
            this.lblSector.Name = "lblSector";
            this.lblSector.Size = new System.Drawing.Size(70, 25);
            this.lblSector.TabIndex = 15;
            this.lblSector.Text = "Sector:";
            // 
            // txtSector
            // 
            this.txtSector.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSector.Location = new System.Drawing.Point(357, 248);
            this.txtSector.MaxLength = 50;
            this.txtSector.Name = "txtSector";
            this.txtSector.Size = new System.Drawing.Size(280, 34);
            this.txtSector.TabIndex = 16;
            // 
            // lblTubo
            // 
            this.lblTubo.AutoSize = true;
            this.lblTubo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblTubo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblTubo.Location = new System.Drawing.Point(357, 290);
            this.lblTubo.Name = "lblTubo";
            this.lblTubo.Size = new System.Drawing.Size(58, 25);
            this.lblTubo.TabIndex = 17;
            this.lblTubo.Text = "Tubo:";
            // 
            // txtTubo
            // 
            this.txtTubo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTubo.Location = new System.Drawing.Point(357, 316);
            this.txtTubo.MaxLength = 50;
            this.txtTubo.Name = "txtTubo";
            this.txtTubo.Size = new System.Drawing.Size(280, 34);
            this.txtTubo.TabIndex = 18;
            // 
            // lblCaja
            // 
            this.lblCaja.AutoSize = true;
            this.lblCaja.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblCaja.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblCaja.Location = new System.Drawing.Point(654, 20);
            this.lblCaja.Name = "lblCaja";
            this.lblCaja.Size = new System.Drawing.Size(50, 25);
            this.lblCaja.TabIndex = 21;
            this.lblCaja.Text = "Caja:";
            // 
            // NUDCaja
            // 
            this.NUDCaja.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.NUDCaja.Location = new System.Drawing.Point(654, 46);
            this.NUDCaja.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.NUDCaja.Name = "NUDCaja";
            this.NUDCaja.Size = new System.Drawing.Size(280, 33);
            this.NUDCaja.TabIndex = 22;
            // 
            // lblPuerto
            // 
            this.lblPuerto.AutoSize = true;
            this.lblPuerto.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPuerto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblPuerto.Location = new System.Drawing.Point(654, 88);
            this.lblPuerto.Name = "lblPuerto";
            this.lblPuerto.Size = new System.Drawing.Size(143, 25);
            this.lblPuerto.TabIndex = 23;
            this.lblPuerto.Text = "Puerto Externo:";
            // 
            // lblSpliter
            // 
            this.lblSpliter.AutoSize = true;
            this.lblSpliter.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSpliter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblSpliter.Location = new System.Drawing.Point(654, 156);
            this.lblSpliter.Name = "lblSpliter";
            this.lblSpliter.Size = new System.Drawing.Size(72, 25);
            this.lblSpliter.TabIndex = 25;
            this.lblSpliter.Text = "Spliter:";
            // 
            // NUDSpliter
            // 
            this.NUDSpliter.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.NUDSpliter.Location = new System.Drawing.Point(654, 182);
            this.NUDSpliter.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.NUDSpliter.Name = "NUDSpliter";
            this.NUDSpliter.Size = new System.Drawing.Size(280, 33);
            this.NUDSpliter.TabIndex = 26;
            // 
            // lblPuertoInterno
            // 
            this.lblPuertoInterno.AutoSize = true;
            this.lblPuertoInterno.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPuertoInterno.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblPuertoInterno.Location = new System.Drawing.Point(654, 224);
            this.lblPuertoInterno.Name = "lblPuertoInterno";
            this.lblPuertoInterno.Size = new System.Drawing.Size(141, 25);
            this.lblPuertoInterno.TabIndex = 27;
            this.lblPuertoInterno.Text = "Puerto Interno:";
            // 
            // NUDPuertoInterno
            // 
            this.NUDPuertoInterno.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.NUDPuertoInterno.Location = new System.Drawing.Point(654, 250);
            this.NUDPuertoInterno.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.NUDPuertoInterno.Name = "NUDPuertoInterno";
            this.NUDPuertoInterno.Size = new System.Drawing.Size(280, 33);
            this.NUDPuertoInterno.TabIndex = 28;
            // 
            // lblCable
            // 
            this.lblCable.AutoSize = true;
            this.lblCable.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblCable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblCable.Location = new System.Drawing.Point(974, 15);
            this.lblCable.Name = "lblCable";
            this.lblCable.Size = new System.Drawing.Size(62, 25);
            this.lblCable.TabIndex = 29;
            this.lblCable.Text = "Cable:";
            // 
            // txtCable
            // 
            this.txtCable.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCable.Location = new System.Drawing.Point(974, 41);
            this.txtCable.MaxLength = 50;
            this.txtCable.Name = "txtCable";
            this.txtCable.Size = new System.Drawing.Size(280, 34);
            this.txtCable.TabIndex = 30;
            // 
            // lblRouter
            // 
            this.lblRouter.AutoSize = true;
            this.lblRouter.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblRouter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblRouter.Location = new System.Drawing.Point(974, 83);
            this.lblRouter.Name = "lblRouter";
            this.lblRouter.Size = new System.Drawing.Size(74, 25);
            this.lblRouter.TabIndex = 31;
            this.lblRouter.Text = "Router:";
            // 
            // txtRouter
            // 
            this.txtRouter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtRouter.Location = new System.Drawing.Point(974, 109);
            this.txtRouter.MaxLength = 50;
            this.txtRouter.Name = "txtRouter";
            this.txtRouter.Size = new System.Drawing.Size(280, 34);
            this.txtRouter.TabIndex = 32;
            // 
            // lblCargador
            // 
            this.lblCargador.AutoSize = true;
            this.lblCargador.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblCargador.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblCargador.Location = new System.Drawing.Point(974, 151);
            this.lblCargador.Name = "lblCargador";
            this.lblCargador.Size = new System.Drawing.Size(93, 25);
            this.lblCargador.TabIndex = 33;
            this.lblCargador.Text = "Cargador:";
            // 
            // txtCargador
            // 
            this.txtCargador.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCargador.Location = new System.Drawing.Point(974, 177);
            this.txtCargador.MaxLength = 50;
            this.txtCargador.Name = "txtCargador";
            this.txtCargador.Size = new System.Drawing.Size(280, 34);
            this.txtCargador.TabIndex = 34;
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblIP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblIP.Location = new System.Drawing.Point(360, 358);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(123, 25);
            this.lblIP.TabIndex = 19;
            this.lblIP.Text = "*Dirección IP:";
            // 
            // txtIP
            // 
            this.txtIP.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtIP.Location = new System.Drawing.Point(360, 384);
            this.txtIP.MaxLength = 50;
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(280, 34);
            this.txtIP.TabIndex = 20;
            // 
            // lblLatitud
            // 
            this.lblLatitud.AutoSize = true;
            this.lblLatitud.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblLatitud.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblLatitud.Location = new System.Drawing.Point(974, 219);
            this.lblLatitud.Name = "lblLatitud";
            this.lblLatitud.Size = new System.Drawing.Size(83, 25);
            this.lblLatitud.TabIndex = 35;
            this.lblLatitud.Text = "*Latitud:";
            // 
            // txtLatitud
            // 
            this.txtLatitud.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLatitud.Location = new System.Drawing.Point(974, 245);
            this.txtLatitud.MaxLength = 50;
            this.txtLatitud.Name = "txtLatitud";
            this.txtLatitud.Size = new System.Drawing.Size(132, 34);
            this.txtLatitud.TabIndex = 36;
            // 
            // lblLongitud
            // 
            this.lblLongitud.AutoSize = true;
            this.lblLongitud.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblLongitud.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblLongitud.Location = new System.Drawing.Point(1122, 219);
            this.lblLongitud.Name = "lblLongitud";
            this.lblLongitud.Size = new System.Drawing.Size(100, 25);
            this.lblLongitud.TabIndex = 37;
            this.lblLongitud.Text = "*Longitud:";
            // 
            // txtLongitud
            // 
            this.txtLongitud.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLongitud.Location = new System.Drawing.Point(1122, 245);
            this.txtLongitud.MaxLength = 50;
            this.txtLongitud.Name = "txtLongitud";
            this.txtLongitud.Size = new System.Drawing.Size(132, 34);
            this.txtLongitud.TabIndex = 38;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblTotal.Location = new System.Drawing.Point(974, 287);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(83, 25);
            this.lblTotal.TabIndex = 39;
            this.lblTotal.Text = "Total ($):";
            // 
            // NUDTotal
            // 
            this.NUDTotal.DecimalPlaces = 2;
            this.NUDTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.NUDTotal.Location = new System.Drawing.Point(974, 313);
            this.NUDTotal.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.NUDTotal.Name = "NUDTotal";
            this.NUDTotal.Size = new System.Drawing.Size(280, 34);
            this.NUDTotal.TabIndex = 40;
            this.NUDTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(1114, 448);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(140, 38);
            this.btnGuardar.TabIndex = 41;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtPuertoExterno
            // 
            this.txtPuertoExterno.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPuertoExterno.Location = new System.Drawing.Point(654, 114);
            this.txtPuertoExterno.MaxLength = 50;
            this.txtPuertoExterno.Name = "txtPuertoExterno";
            this.txtPuertoExterno.Size = new System.Drawing.Size(280, 34);
            this.txtPuertoExterno.TabIndex = 24;
            // 
            // btnLupa
            // 
            this.btnLupa.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnLupa.Image = ((System.Drawing.Image)(resources.GetObject("btnLupa.Image")));
            this.btnLupa.Location = new System.Drawing.Point(945, 448);
            this.btnLupa.Name = "btnLupa";
            this.btnLupa.Size = new System.Drawing.Size(122, 35);
            this.btnLupa.TabIndex = 42;
            this.btnLupa.Text = "Ver mapa";
            this.btnLupa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLupa.UseVisualStyleBackColor = true;
            this.btnLupa.Click += new System.EventHandler(this.btnLupa_Click);
            // 
            // lblNota
            // 
            this.lblNota.AutoSize = true;
            this.lblNota.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNota.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblNota.Location = new System.Drawing.Point(28, 237);
            this.lblNota.Name = "lblNota";
            this.lblNota.Size = new System.Drawing.Size(57, 25);
            this.lblNota.TabIndex = 43;
            this.lblNota.Text = "Nota:";
            // 
            // txtNota
            // 
            this.txtNota.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNota.Location = new System.Drawing.Point(28, 263);
            this.txtNota.Multiline = true;
            this.txtNota.Name = "txtNota";
            this.txtNota.Size = new System.Drawing.Size(280, 220);
            this.txtNota.TabIndex = 44;
            // 
            // Instalacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1281, 495);
            this.Controls.Add(this.lblNota);
            this.Controls.Add(this.txtNota);
            this.Controls.Add(this.btnLupa);
            this.Controls.Add(this.txtPuertoExterno);
            this.Controls.Add(this.lblCaja);
            this.Controls.Add(this.NUDCaja);
            this.Controls.Add(this.lblAntena);
            this.Controls.Add(this.lblPuerto);
            this.Controls.Add(this.txtAntena);
            this.Controls.Add(this.lblSpliter);
            this.Controls.Add(this.NUDSpliter);
            this.Controls.Add(this.lblRB);
            this.Controls.Add(this.lblPuertoInterno);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.NUDPuertoInterno);
            this.Controls.Add(this.txtRB);
            this.Controls.Add(this.CBInstalador);
            this.Controls.Add(this.lblSector);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.txtSector);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.lblTubo);
            this.Controls.Add(this.lblModalidad);
            this.Controls.Add(this.txtTubo);
            this.Controls.Add(this.CBModalidad);
            this.Controls.Add(this.lblTipoInstalacion);
            this.Controls.Add(this.RBAntena);
            this.Controls.Add(this.RBFibra);
            this.Controls.Add(this.lblIP);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.lblCable);
            this.Controls.Add(this.txtCable);
            this.Controls.Add(this.lblRouter);
            this.Controls.Add(this.txtRouter);
            this.Controls.Add(this.lblCargador);
            this.Controls.Add(this.txtCargador);
            this.Controls.Add(this.lblLatitud);
            this.Controls.Add(this.txtLatitud);
            this.Controls.Add(this.lblLongitud);
            this.Controls.Add(this.txtLongitud);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.NUDTotal);
            this.Controls.Add(this.btnGuardar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Instalacion";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Instalacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NUDCaja)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDSpliter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDPuertoInterno)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDTotal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.ComboBox CBInstalador;
        private System.Windows.Forms.Label lblModalidad;
        private System.Windows.Forms.ComboBox CBModalidad;
        private System.Windows.Forms.Label lblTipoInstalacion;
        private System.Windows.Forms.RadioButton RBAntena;
        private System.Windows.Forms.RadioButton RBFibra;
        private System.Windows.Forms.Label lblAntena;
        private System.Windows.Forms.TextBox txtAntena;
        private System.Windows.Forms.Label lblRB;
        private System.Windows.Forms.TextBox txtRB;
        private System.Windows.Forms.Label lblSector;
        private System.Windows.Forms.TextBox txtSector;
        private System.Windows.Forms.Label lblTubo;
        private System.Windows.Forms.TextBox txtTubo;
        private System.Windows.Forms.Label lblCaja;
        private System.Windows.Forms.NumericUpDown NUDCaja;
        private System.Windows.Forms.Label lblPuerto;
        private System.Windows.Forms.Label lblSpliter;
        private System.Windows.Forms.NumericUpDown NUDSpliter;
        private System.Windows.Forms.Label lblPuertoInterno;
        private System.Windows.Forms.NumericUpDown NUDPuertoInterno;
        private System.Windows.Forms.Label lblCable;
        private System.Windows.Forms.TextBox txtCable;
        private System.Windows.Forms.Label lblRouter;
        private System.Windows.Forms.TextBox txtRouter;
        private System.Windows.Forms.Label lblCargador;
        private System.Windows.Forms.TextBox txtCargador;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label lblLatitud;
        private System.Windows.Forms.TextBox txtLatitud;
        private System.Windows.Forms.Label lblLongitud;
        private System.Windows.Forms.TextBox txtLongitud;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.NumericUpDown NUDTotal;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtPuertoExterno;
        private System.Windows.Forms.Button btnLupa;
        private System.Windows.Forms.Label lblNota;
        private System.Windows.Forms.TextBox txtNota;
    }
}