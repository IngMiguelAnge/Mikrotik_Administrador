namespace Mikrotik_Administrador
{
    partial class Ubicacion
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
            this.gMap = new GMap.NET.WindowsForms.GMapControl();
            this.txtLatitud = new System.Windows.Forms.TextBox();
            this.txtLongitud = new System.Windows.Forms.TextBox();
            this.BtnBuscar = new System.Windows.Forms.Button();
            this.txtDireccionOficial = new System.Windows.Forms.TextBox();
            this.cmbMapas = new System.Windows.Forms.ComboBox();
            this.lblGPS = new System.Windows.Forms.Label();
            this.lblLatitud = new System.Windows.Forms.Label();
            this.lblLongitud = new System.Windows.Forms.Label();
            this.lblDireccionSugerida = new System.Windows.Forms.Label();
            this.txtDireccionSugerida = new System.Windows.Forms.TextBox();
            this.lblUbicacion = new System.Windows.Forms.Label();
            this.lblVista = new System.Windows.Forms.Label();
            this.lblEjemplo = new System.Windows.Forms.Label();
            this.lblCalle = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.BtnGuardar = new System.Windows.Forms.Button();
            this.btnAceptarUbicacion = new System.Windows.Forms.Button();
            this.btnCancelarDireccion = new System.Windows.Forms.Button();
            this.panelMapa = new System.Windows.Forms.Panel();
            this.CBCoordendadas = new System.Windows.Forms.CheckBox();
            this.panelMapa.SuspendLayout();
            this.SuspendLayout();
            // 
            // gMap
            // 
            this.gMap.Bearing = 0F;
            this.gMap.CanDragMap = true;
            this.gMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gMap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMap.GrayScaleMode = false;
            this.gMap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMap.LevelsKeepInMemory = 5;
            this.gMap.Location = new System.Drawing.Point(0, 0);
            this.gMap.MarkersEnabled = true;
            this.gMap.MaxZoom = 2;
            this.gMap.MinZoom = 2;
            this.gMap.MouseWheelZoomEnabled = true;
            this.gMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMap.Name = "gMap";
            this.gMap.NegativeMode = false;
            this.gMap.PolygonsEnabled = true;
            this.gMap.RetryLoadTile = 0;
            this.gMap.RoutesEnabled = true;
            this.gMap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMap.ShowTileGridLines = false;
            this.gMap.Size = new System.Drawing.Size(967, 410);
            this.gMap.TabIndex = 17;
            this.gMap.Zoom = 0D;
            this.gMap.OnMarkerEnter += new GMap.NET.WindowsForms.MarkerEnter(this.gMap_OnMarkerEnter);
            this.gMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gMap_MouseMove);
            this.gMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gMap_MouseUp);
            // 
            // txtLatitud
            // 
            this.txtLatitud.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtLatitud.Location = new System.Drawing.Point(170, 160);
            this.txtLatitud.Name = "txtLatitud";
            this.txtLatitud.Size = new System.Drawing.Size(135, 33);
            this.txtLatitud.TabIndex = 8;
            // 
            // txtLongitud
            // 
            this.txtLongitud.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtLongitud.Location = new System.Drawing.Point(415, 160);
            this.txtLongitud.Name = "txtLongitud";
            this.txtLongitud.Size = new System.Drawing.Size(135, 33);
            this.txtLongitud.TabIndex = 10;
            // 
            // BtnBuscar
            // 
            this.BtnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(233)))), ((int)(((byte)(240)))));
            this.BtnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBuscar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.BtnBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.BtnBuscar.Location = new System.Drawing.Point(340, 203);
            this.BtnBuscar.Name = "BtnBuscar";
            this.BtnBuscar.Size = new System.Drawing.Size(110, 36);
            this.BtnBuscar.TabIndex = 5;
            this.BtnBuscar.Text = "Buscar";
            this.BtnBuscar.UseVisualStyleBackColor = false;
            this.BtnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);
            // 
            // txtDireccionOficial
            // 
            this.txtDireccionOficial.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtDireccionOficial.Location = new System.Drawing.Point(170, 80);
            this.txtDireccionOficial.Name = "txtDireccionOficial";
            this.txtDireccionOficial.Size = new System.Drawing.Size(796, 33);
            this.txtDireccionOficial.TabIndex = 3;
            // 
            // cmbMapas
            // 
            this.cmbMapas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMapas.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cmbMapas.FormattingEnabled = true;
            this.cmbMapas.Items.AddRange(new object[] {
            "Calles (Google)",
            "Satélite",
            "Híbrido",
            "OpenStreet"});
            this.cmbMapas.Location = new System.Drawing.Point(760, 160);
            this.cmbMapas.Name = "cmbMapas";
            this.cmbMapas.Size = new System.Drawing.Size(206, 33);
            this.cmbMapas.TabIndex = 12;
            this.cmbMapas.SelectedIndexChanged += new System.EventHandler(this.cmbMapas_SelectedIndexChanged);
            // 
            // lblGPS
            // 
            this.lblGPS.AutoSize = true;
            this.lblGPS.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblGPS.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblGPS.Location = new System.Drawing.Point(34, 164);
            this.lblGPS.Name = "lblGPS";
            this.lblGPS.Size = new System.Drawing.Size(54, 25);
            this.lblGPS.TabIndex = 6;
            this.lblGPS.Text = "*GPS";
            // 
            // lblLatitud
            // 
            this.lblLatitud.AutoSize = true;
            this.lblLatitud.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLatitud.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblLatitud.Location = new System.Drawing.Point(100, 164);
            this.lblLatitud.Name = "lblLatitud";
            this.lblLatitud.Size = new System.Drawing.Size(70, 25);
            this.lblLatitud.TabIndex = 7;
            this.lblLatitud.Text = "Latitud:";
            // 
            // lblLongitud
            // 
            this.lblLongitud.AutoSize = true;
            this.lblLongitud.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLongitud.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblLongitud.Location = new System.Drawing.Point(325, 164);
            this.lblLongitud.Name = "lblLongitud";
            this.lblLongitud.Size = new System.Drawing.Size(87, 25);
            this.lblLongitud.TabIndex = 9;
            this.lblLongitud.Text = "Longitud:";
            // 
            // lblDireccionSugerida
            // 
            this.lblDireccionSugerida.AutoSize = true;
            this.lblDireccionSugerida.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDireccionSugerida.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblDireccionSugerida.Location = new System.Drawing.Point(34, 250);
            this.lblDireccionSugerida.Name = "lblDireccionSugerida";
            this.lblDireccionSugerida.Size = new System.Drawing.Size(171, 25);
            this.lblDireccionSugerida.TabIndex = 13;
            this.lblDireccionSugerida.Text = "Dirección sugerida:";
            // 
            // txtDireccionSugerida
            // 
            this.txtDireccionSugerida.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.txtDireccionSugerida.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtDireccionSugerida.Location = new System.Drawing.Point(210, 246);
            this.txtDireccionSugerida.Name = "txtDireccionSugerida";
            this.txtDireccionSugerida.ReadOnly = true;
            this.txtDireccionSugerida.Size = new System.Drawing.Size(756, 33);
            this.txtDireccionSugerida.TabIndex = 14;
            // 
            // lblUbicacion
            // 
            this.lblUbicacion.AutoSize = true;
            this.lblUbicacion.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblUbicacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblUbicacion.Location = new System.Drawing.Point(34, 84);
            this.lblUbicacion.Name = "lblUbicacion";
            this.lblUbicacion.Size = new System.Drawing.Size(156, 25);
            this.lblUbicacion.TabIndex = 2;
            this.lblUbicacion.Text = "*Dirección oficial:";
            // 
            // lblVista
            // 
            this.lblVista.AutoSize = true;
            this.lblVista.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblVista.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblVista.Location = new System.Drawing.Point(755, 132);
            this.lblVista.Name = "lblVista";
            this.lblVista.Size = new System.Drawing.Size(138, 25);
            this.lblVista.TabIndex = 11;
            this.lblVista.Text = "Vista del mapa:";
            // 
            // lblEjemplo
            // 
            this.lblEjemplo.AutoSize = true;
            this.lblEjemplo.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblEjemplo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(165)))));
            this.lblEjemplo.Location = new System.Drawing.Point(166, 116);
            this.lblEjemplo.Name = "lblEjemplo";
            this.lblEjemplo.Size = new System.Drawing.Size(405, 23);
            this.lblEjemplo.TabIndex = 4;
            this.lblEjemplo.Text = "Ejemplo de dirección: San Salvador Huixcolotla, Puebla";
            // 
            // lblCalle
            // 
            this.lblCalle.AutoSize = true;
            this.lblCalle.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblCalle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblCalle.Location = new System.Drawing.Point(34, 38);
            this.lblCalle.Name = "lblCalle";
            this.lblCalle.Size = new System.Drawing.Size(102, 25);
            this.lblCalle.TabIndex = 0;
            this.lblCalle.Text = "*Dirección:";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtDireccion.Location = new System.Drawing.Point(170, 34);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(796, 33);
            this.txtDireccion.TabIndex = 1;
            // 
            // BtnGuardar
            // 
            this.BtnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.BtnGuardar.Enabled = false;
            this.BtnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnGuardar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.BtnGuardar.ForeColor = System.Drawing.Color.White;
            this.BtnGuardar.Location = new System.Drawing.Point(856, 295);
            this.BtnGuardar.Name = "BtnGuardar";
            this.BtnGuardar.Size = new System.Drawing.Size(110, 36);
            this.BtnGuardar.TabIndex = 16;
            this.BtnGuardar.Text = "Guardar";
            this.BtnGuardar.UseVisualStyleBackColor = false;
            this.BtnGuardar.Click += new System.EventHandler(this.BtnGuardar_Click);
            // 
            // btnAceptarUbicacion
            // 
            this.btnAceptarUbicacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(80)))), ((int)(((byte)(196)))));
            this.btnAceptarUbicacion.Enabled = false;
            this.btnAceptarUbicacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptarUbicacion.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnAceptarUbicacion.ForeColor = System.Drawing.Color.White;
            this.btnAceptarUbicacion.Location = new System.Drawing.Point(34, 295);
            this.btnAceptarUbicacion.Name = "btnAceptarUbicacion";
            this.btnAceptarUbicacion.Size = new System.Drawing.Size(180, 36);
            this.btnAceptarUbicacion.TabIndex = 15;
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
            this.btnCancelarDireccion.Location = new System.Drawing.Point(230, 295);
            this.btnCancelarDireccion.Name = "btnCancelarDireccion";
            this.btnCancelarDireccion.Size = new System.Drawing.Size(160, 36);
            this.btnCancelarDireccion.TabIndex = 18;
            this.btnCancelarDireccion.Text = "Cancelar dirección";
            this.btnCancelarDireccion.UseVisualStyleBackColor = false;
            this.btnCancelarDireccion.Click += new System.EventHandler(this.btnCancelarDireccion_Click);
            // 
            // panelMapa
            // 
            this.panelMapa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMapa.Controls.Add(this.gMap);
            this.panelMapa.Location = new System.Drawing.Point(34, 345);
            this.panelMapa.Name = "panelMapa";
            this.panelMapa.Size = new System.Drawing.Size(967, 410);
            this.panelMapa.TabIndex = 19;
            // 
            // CBCoordendadas
            // 
            this.CBCoordendadas.AutoSize = true;
            this.CBCoordendadas.Checked = true;
            this.CBCoordendadas.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CBCoordendadas.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CBCoordendadas.Location = new System.Drawing.Point(34, 207);
            this.CBCoordendadas.Name = "CBCoordendadas";
            this.CBCoordendadas.Size = new System.Drawing.Size(246, 29);
            this.CBCoordendadas.TabIndex = 20;
            this.CBCoordendadas.Text = "¿Buscar por coordenadas?";
            this.CBCoordendadas.UseVisualStyleBackColor = true;
            // 
            // Ubicacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1035, 780);
            this.Controls.Add(this.CBCoordendadas);
            this.Controls.Add(this.panelMapa);
            this.Controls.Add(this.btnCancelarDireccion);
            this.Controls.Add(this.btnAceptarUbicacion);
            this.Controls.Add(this.BtnGuardar);
            this.Controls.Add(this.txtDireccion);
            this.Controls.Add(this.lblCalle);
            this.Controls.Add(this.lblEjemplo);
            this.Controls.Add(this.lblVista);
            this.Controls.Add(this.lblUbicacion);
            this.Controls.Add(this.txtDireccionSugerida);
            this.Controls.Add(this.lblDireccionSugerida);
            this.Controls.Add(this.lblLongitud);
            this.Controls.Add(this.lblLatitud);
            this.Controls.Add(this.lblGPS);
            this.Controls.Add(this.cmbMapas);
            this.Controls.Add(this.txtDireccionOficial);
            this.Controls.Add(this.BtnBuscar);
            this.Controls.Add(this.txtLongitud);
            this.Controls.Add(this.txtLatitud);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Ubicacion";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Geolocalización y Ubicación";
            this.Load += new System.EventHandler(this.Ubicacion_Load);
            this.panelMapa.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gMap;
        private System.Windows.Forms.TextBox txtLatitud;
        private System.Windows.Forms.TextBox txtLongitud;
        private System.Windows.Forms.Button BtnBuscar;
        private System.Windows.Forms.TextBox txtDireccionOficial;
        private System.Windows.Forms.ComboBox cmbMapas;
        private System.Windows.Forms.Label lblGPS;
        private System.Windows.Forms.Label lblLatitud;
        private System.Windows.Forms.Label lblLongitud;
        private System.Windows.Forms.Label lblDireccionSugerida;
        private System.Windows.Forms.TextBox txtDireccionSugerida;
        private System.Windows.Forms.Label lblUbicacion;
        private System.Windows.Forms.Label lblVista;
        private System.Windows.Forms.Label lblEjemplo;
        private System.Windows.Forms.Label lblCalle;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Button BtnGuardar;
        private System.Windows.Forms.Button btnAceptarUbicacion;
        private System.Windows.Forms.Button btnCancelarDireccion;
        private System.Windows.Forms.Panel panelMapa; // Modificado de GroupBox a Panel
        private System.Windows.Forms.CheckBox CBCoordendadas;
    }
}