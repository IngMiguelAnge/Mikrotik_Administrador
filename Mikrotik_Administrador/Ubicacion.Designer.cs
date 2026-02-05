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
            this.lblUbicacionMikrotik = new System.Windows.Forms.Label();
            this.lblEjemplo = new System.Windows.Forms.Label();
            this.lblCalle = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.BtnGuardar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gMap
            // 
            this.gMap.Bearing = 0F;
            this.gMap.CanDragMap = true;
            this.gMap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMap.GrayScaleMode = false;
            this.gMap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMap.LevelsKeepInMemory = 5;
            this.gMap.Location = new System.Drawing.Point(34, 275);
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
            this.gMap.Size = new System.Drawing.Size(989, 354);
            this.gMap.TabIndex = 8;
            this.gMap.Zoom = 0D;
            this.gMap.OnMarkerEnter += new GMap.NET.WindowsForms.MarkerEnter(this.gMap_OnMarkerEnter);
            this.gMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gMap_MouseMove);
            this.gMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gMap_MouseUp);
            // 
            // txtLatitud
            // 
            this.txtLatitud.Enabled = false;
            this.txtLatitud.Location = new System.Drawing.Point(170, 166);
            this.txtLatitud.Name = "txtLatitud";
            this.txtLatitud.Size = new System.Drawing.Size(129, 26);
            this.txtLatitud.TabIndex = 5;
            // 
            // txtLongitud
            // 
            this.txtLongitud.Enabled = false;
            this.txtLongitud.Location = new System.Drawing.Point(404, 165);
            this.txtLongitud.Name = "txtLongitud";
            this.txtLongitud.Size = new System.Drawing.Size(134, 26);
            this.txtLongitud.TabIndex = 6;
            // 
            // BtnBuscar
            // 
            this.BtnBuscar.Location = new System.Drawing.Point(520, 123);
            this.BtnBuscar.Name = "BtnBuscar";
            this.BtnBuscar.Size = new System.Drawing.Size(105, 33);
            this.BtnBuscar.TabIndex = 2;
            this.BtnBuscar.Text = "Buscar";
            this.BtnBuscar.UseVisualStyleBackColor = true;
            this.BtnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);
            // 
            // txtDireccionOficial
            // 
            this.txtDireccionOficial.Location = new System.Drawing.Point(159, 85);
            this.txtDireccionOficial.Name = "txtDireccionOficial";
            this.txtDireccionOficial.Size = new System.Drawing.Size(807, 26);
            this.txtDireccionOficial.TabIndex = 1;
            // 
            // cmbMapas
            // 
            this.cmbMapas.FormattingEnabled = true;
            this.cmbMapas.Items.AddRange(new object[] {
            "Calles (Google)",
            "Satélite",
            "Híbrido",
            "OpenStreet"});
            this.cmbMapas.Location = new System.Drawing.Point(789, 166);
            this.cmbMapas.Name = "cmbMapas";
            this.cmbMapas.Size = new System.Drawing.Size(177, 28);
            this.cmbMapas.TabIndex = 3;
            this.cmbMapas.SelectedIndexChanged += new System.EventHandler(this.cmbMapas_SelectedIndexChanged);
            // 
            // lblGPS
            // 
            this.lblGPS.AutoSize = true;
            this.lblGPS.Location = new System.Drawing.Point(30, 172);
            this.lblGPS.Name = "lblGPS";
            this.lblGPS.Size = new System.Drawing.Size(49, 20);
            this.lblGPS.TabIndex = 6;
            this.lblGPS.Text = "*GPS";
            // 
            // lblLatitud
            // 
            this.lblLatitud.AutoSize = true;
            this.lblLatitud.Location = new System.Drawing.Point(102, 172);
            this.lblLatitud.Name = "lblLatitud";
            this.lblLatitud.Size = new System.Drawing.Size(62, 20);
            this.lblLatitud.TabIndex = 7;
            this.lblLatitud.Text = "Latitud:";
            // 
            // lblLongitud
            // 
            this.lblLongitud.AutoSize = true;
            this.lblLongitud.Location = new System.Drawing.Point(323, 171);
            this.lblLongitud.Name = "lblLongitud";
            this.lblLongitud.Size = new System.Drawing.Size(75, 20);
            this.lblLongitud.TabIndex = 8;
            this.lblLongitud.Text = "Longitud:";
            // 
            // lblDireccionSugerida
            // 
            this.lblDireccionSugerida.AutoSize = true;
            this.lblDireccionSugerida.Location = new System.Drawing.Point(30, 212);
            this.lblDireccionSugerida.Name = "lblDireccionSugerida";
            this.lblDireccionSugerida.Size = new System.Drawing.Size(144, 20);
            this.lblDireccionSugerida.TabIndex = 9;
            this.lblDireccionSugerida.Text = "Dirección sugerida:";
            // 
            // txtDireccionSugerida
            // 
            this.txtDireccionSugerida.Enabled = false;
            this.txtDireccionSugerida.Location = new System.Drawing.Point(185, 206);
            this.txtDireccionSugerida.Name = "txtDireccionSugerida";
            this.txtDireccionSugerida.Size = new System.Drawing.Size(781, 26);
            this.txtDireccionSugerida.TabIndex = 7;
            // 
            // lblUbicacion
            // 
            this.lblUbicacion.AutoSize = true;
            this.lblUbicacion.Location = new System.Drawing.Point(30, 85);
            this.lblUbicacion.Name = "lblUbicacion";
            this.lblUbicacion.Size = new System.Drawing.Size(129, 20);
            this.lblUbicacion.TabIndex = 11;
            this.lblUbicacion.Text = "*Dirección oficial:";
            // 
            // lblVista
            // 
            this.lblVista.AutoSize = true;
            this.lblVista.Location = new System.Drawing.Point(568, 171);
            this.lblVista.Name = "lblVista";
            this.lblVista.Size = new System.Drawing.Size(209, 20);
            this.lblVista.TabIndex = 12;
            this.lblVista.Text = "Seleccione la vista de mapa:";
            // 
            // lblUbicacionMikrotik
            // 
            this.lblUbicacionMikrotik.AutoSize = true;
            this.lblUbicacionMikrotik.Location = new System.Drawing.Point(34, 13);
            this.lblUbicacionMikrotik.Name = "lblUbicacionMikrotik";
            this.lblUbicacionMikrotik.Size = new System.Drawing.Size(162, 20);
            this.lblUbicacionMikrotik.TabIndex = 13;
            this.lblUbicacionMikrotik.Text = "Ubicación del mikrotik";
            // 
            // lblEjemplo
            // 
            this.lblEjemplo.AutoSize = true;
            this.lblEjemplo.Location = new System.Drawing.Point(34, 136);
            this.lblEjemplo.Name = "lblEjemplo";
            this.lblEjemplo.Size = new System.Drawing.Size(396, 20);
            this.lblEjemplo.TabIndex = 14;
            this.lblEjemplo.Text = "Ejemplo de dirección: San Salvador Huixcolotla, Puebla";
            // 
            // lblCalle
            // 
            this.lblCalle.AutoSize = true;
            this.lblCalle.Location = new System.Drawing.Point(34, 43);
            this.lblCalle.Name = "lblCalle";
            this.lblCalle.Size = new System.Drawing.Size(85, 20);
            this.lblCalle.TabIndex = 15;
            this.lblCalle.Text = "*Dirección:";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(159, 37);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(817, 26);
            this.txtDireccion.TabIndex = 0;
            // 
            // BtnGuardar
            // 
            this.BtnGuardar.Location = new System.Drawing.Point(729, 123);
            this.BtnGuardar.Name = "BtnGuardar";
            this.BtnGuardar.Size = new System.Drawing.Size(83, 33);
            this.BtnGuardar.TabIndex = 4;
            this.BtnGuardar.Text = "Guardar";
            this.BtnGuardar.UseVisualStyleBackColor = true;
            this.BtnGuardar.Click += new System.EventHandler(this.BtnGuardar_Click);
            // 
            // Ubicacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 660);
            this.Controls.Add(this.BtnGuardar);
            this.Controls.Add(this.txtDireccion);
            this.Controls.Add(this.lblCalle);
            this.Controls.Add(this.lblEjemplo);
            this.Controls.Add(this.lblUbicacionMikrotik);
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
            this.Controls.Add(this.gMap);
            this.MaximizeBox = false;
            this.Name = "Ubicacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ubicacion";
            this.Load += new System.EventHandler(this.Ubicacion_Load);
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
        private System.Windows.Forms.Label lblUbicacionMikrotik;
        private System.Windows.Forms.Label lblEjemplo;
        private System.Windows.Forms.Label lblCalle;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Button BtnGuardar;
    }
}