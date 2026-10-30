namespace Mikrotik_Administrador.Catalogos
{
    partial class Extra
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
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.NUDCosto = new System.Windows.Forms.NumericUpDown();
            this.lblCosto = new System.Windows.Forms.Label();
            this.dtpFechaRecibido = new System.Windows.Forms.DateTimePicker();
            this.lblFechaPago = new System.Windows.Forms.Label();
            this.dtpFechaEntrega = new System.Windows.Forms.DateTimePicker();
            this.lblFechaEntrega = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.CBRecibe = new System.Windows.Forms.ComboBox();
            this.lblRecibe = new System.Windows.Forms.Label();
            this.CBRepara = new System.Windows.Forms.ComboBox();
            this.lblRepara = new System.Windows.Forms.Label();
            this.CBEntrega = new System.Windows.Forms.ComboBox();
            this.lblEntrega = new System.Windows.Forms.Label();
            this.txtNota = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CBTipo = new System.Windows.Forms.ComboBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.NudDescuento = new System.Windows.Forms.NumericUpDown();
            this.lblDescuento = new System.Windows.Forms.Label();
            this.lblCostoTotal = new System.Windows.Forms.Label();
            this.dtpFechaRecogida = new System.Windows.Forms.DateTimePicker();
            this.lblFechaRecoge = new System.Windows.Forms.Label();
            this.dtpFechaReparacion = new System.Windows.Forms.DateTimePicker();
            this.lblFechaRepara = new System.Windows.Forms.Label();
            this.CBRecoge = new System.Windows.Forms.ComboBox();
            this.lblRecoge = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NUDCosto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudDescuento)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNombre
            // 
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNombre.Location = new System.Drawing.Point(34, 110);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(280, 34);
            this.txtNombre.TabIndex = 3;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblNombre.Location = new System.Drawing.Point(30, 84);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(152, 25);
            this.lblNombre.TabIndex = 2;
            this.lblNombre.Text = "*Servicio/objeto:";
            // 
            // NUDCosto
            // 
            this.NUDCosto.DecimalPlaces = 2;
            this.NUDCosto.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.NUDCosto.Location = new System.Drawing.Point(35, 191);
            this.NUDCosto.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.NUDCosto.Name = "NUDCosto";
            this.NUDCosto.Size = new System.Drawing.Size(140, 33);
            this.NUDCosto.TabIndex = 5;
            this.NUDCosto.ValueChanged += new System.EventHandler(this.NUDCosto_ValueChanged);
            this.NUDCosto.Enter += new System.EventHandler(this.NUDCosto_Enter);
            this.NUDCosto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NUDCosto_KeyPress);
            this.NUDCosto.KeyUp += new System.Windows.Forms.KeyEventHandler(this.NUDCosto_KeyUp);
            // 
            // lblCosto
            // 
            this.lblCosto.AutoSize = true;
            this.lblCosto.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblCosto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblCosto.Location = new System.Drawing.Point(30, 163);
            this.lblCosto.Name = "lblCosto";
            this.lblCosto.Size = new System.Drawing.Size(64, 25);
            this.lblCosto.TabIndex = 4;
            this.lblCosto.Text = "Costo:";
            // 
            // dtpFechaRecibido
            // 
            this.dtpFechaRecibido.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpFechaRecibido.Enabled = false;
            this.dtpFechaRecibido.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpFechaRecibido.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRecibido.Location = new System.Drawing.Point(407, 110);
            this.dtpFechaRecibido.Name = "dtpFechaRecibido";
            this.dtpFechaRecibido.Size = new System.Drawing.Size(230, 33);
            this.dtpFechaRecibido.TabIndex = 9;
            // 
            // lblFechaPago
            // 
            this.lblFechaPago.AutoSize = true;
            this.lblFechaPago.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFechaPago.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblFechaPago.Location = new System.Drawing.Point(401, 82);
            this.lblFechaPago.Name = "lblFechaPago";
            this.lblFechaPago.Size = new System.Drawing.Size(204, 25);
            this.lblFechaPago.TabIndex = 8;
            this.lblFechaPago.Text = "Fecha en que se recibe:";
            // 
            // dtpFechaEntrega
            // 
            this.dtpFechaEntrega.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpFechaEntrega.Enabled = false;
            this.dtpFechaEntrega.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpFechaEntrega.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaEntrega.Location = new System.Drawing.Point(406, 423);
            this.dtpFechaEntrega.Name = "dtpFechaEntrega";
            this.dtpFechaEntrega.Size = new System.Drawing.Size(230, 33);
            this.dtpFechaEntrega.TabIndex = 12;
            // 
            // lblFechaEntrega
            // 
            this.lblFechaEntrega.AutoSize = true;
            this.lblFechaEntrega.Enabled = false;
            this.lblFechaEntrega.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFechaEntrega.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblFechaEntrega.Location = new System.Drawing.Point(402, 395);
            this.lblFechaEntrega.Name = "lblFechaEntrega";
            this.lblFechaEntrega.Size = new System.Drawing.Size(175, 25);
            this.lblFechaEntrega.TabIndex = 11;
            this.lblFechaEntrega.Text = "Cuando se entrego:";
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(982, 606);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(120, 38);
            this.btnGuardar.TabIndex = 17;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // CBRecibe
            // 
            this.CBRecibe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBRecibe.Enabled = false;
            this.CBRecibe.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBRecibe.FormattingEnabled = true;
            this.CBRecibe.Location = new System.Drawing.Point(406, 37);
            this.CBRecibe.Name = "CBRecibe";
            this.CBRecibe.Size = new System.Drawing.Size(271, 33);
            this.CBRecibe.TabIndex = 5;
            this.CBRecibe.SelectedIndexChanged += new System.EventHandler(this.CBRecibe_SelectedIndexChanged);
            // 
            // lblRecibe
            // 
            this.lblRecibe.AutoSize = true;
            this.lblRecibe.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecibe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblRecibe.Location = new System.Drawing.Point(399, 9);
            this.lblRecibe.Name = "lblRecibe";
            this.lblRecibe.Size = new System.Drawing.Size(139, 25);
            this.lblRecibe.TabIndex = 4;
            this.lblRecibe.Text = "¿Quien recibe?:";
            // 
            // CBRepara
            // 
            this.CBRepara.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBRepara.Enabled = false;
            this.CBRepara.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBRepara.FormattingEnabled = true;
            this.CBRepara.Location = new System.Drawing.Point(407, 189);
            this.CBRepara.Name = "CBRepara";
            this.CBRepara.Size = new System.Drawing.Size(271, 33);
            this.CBRepara.TabIndex = 7;
            this.CBRepara.SelectedIndexChanged += new System.EventHandler(this.CBRepara_SelectedIndexChanged);
            // 
            // lblRepara
            // 
            this.lblRepara.AutoSize = true;
            this.lblRepara.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRepara.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblRepara.Location = new System.Drawing.Point(405, 163);
            this.lblRepara.Name = "lblRepara";
            this.lblRepara.Size = new System.Drawing.Size(137, 25);
            this.lblRepara.TabIndex = 6;
            this.lblRepara.Text = "¿Quien repara?";
            // 
            // CBEntrega
            // 
            this.CBEntrega.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBEntrega.Enabled = false;
            this.CBEntrega.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBEntrega.FormattingEnabled = true;
            this.CBEntrega.Location = new System.Drawing.Point(407, 348);
            this.CBEntrega.Name = "CBEntrega";
            this.CBEntrega.Size = new System.Drawing.Size(271, 33);
            this.CBEntrega.TabIndex = 14;
            this.CBEntrega.SelectedIndexChanged += new System.EventHandler(this.CBEntrega_SelectedIndexChanged);
            // 
            // lblEntrega
            // 
            this.lblEntrega.AutoSize = true;
            this.lblEntrega.Enabled = false;
            this.lblEntrega.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntrega.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblEntrega.Location = new System.Drawing.Point(402, 320);
            this.lblEntrega.Name = "lblEntrega";
            this.lblEntrega.Size = new System.Drawing.Size(149, 25);
            this.lblEntrega.TabIndex = 13;
            this.lblEntrega.Text = "¿Quien entrega?";
            // 
            // txtNota
            // 
            this.txtNota.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNota.Location = new System.Drawing.Point(717, 37);
            this.txtNota.Multiline = true;
            this.txtNota.Name = "txtNota";
            this.txtNota.Size = new System.Drawing.Size(385, 501);
            this.txtNota.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.label4.Location = new System.Drawing.Point(712, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 25);
            this.label4.TabIndex = 15;
            this.label4.Text = "Nota:";
            // 
            // CBTipo
            // 
            this.CBTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBTipo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBTipo.FormattingEnabled = true;
            this.CBTipo.Items.AddRange(new object[] {
            "Seleccione",
            "Venta",
            "Prestamo",
            "Servicio"});
            this.CBTipo.Location = new System.Drawing.Point(34, 37);
            this.CBTipo.Name = "CBTipo";
            this.CBTipo.Size = new System.Drawing.Size(271, 33);
            this.CBTipo.TabIndex = 1;
            this.CBTipo.SelectedIndexChanged += new System.EventHandler(this.CBTipo_SelectedIndexChanged);
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Enabled = false;
            this.lblTipo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblTipo.Location = new System.Drawing.Point(29, 9);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(134, 25);
            this.lblTipo.TabIndex = 0;
            this.lblTipo.Text = "*Tipo de extra:";
            // 
            // NudDescuento
            // 
            this.NudDescuento.DecimalPlaces = 2;
            this.NudDescuento.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.NudDescuento.Location = new System.Drawing.Point(205, 191);
            this.NudDescuento.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.NudDescuento.Name = "NudDescuento";
            this.NudDescuento.Size = new System.Drawing.Size(140, 33);
            this.NudDescuento.TabIndex = 7;
            this.NudDescuento.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.NudDescuento.ValueChanged += new System.EventHandler(this.NudDescuento_ValueChanged);
            this.NudDescuento.Enter += new System.EventHandler(this.NudDescuento_Enter);
            this.NudDescuento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NudDescuento_KeyPress);
            this.NudDescuento.KeyUp += new System.Windows.Forms.KeyEventHandler(this.NudDescuento_KeyUp);
            // 
            // lblDescuento
            // 
            this.lblDescuento.AutoSize = true;
            this.lblDescuento.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescuento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblDescuento.Location = new System.Drawing.Point(200, 163);
            this.lblDescuento.Name = "lblDescuento";
            this.lblDescuento.Size = new System.Drawing.Size(135, 25);
            this.lblDescuento.TabIndex = 6;
            this.lblDescuento.Text = "% Descontado:";
            // 
            // lblCostoTotal
            // 
            this.lblCostoTotal.AutoSize = true;
            this.lblCostoTotal.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblCostoTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblCostoTotal.Location = new System.Drawing.Point(31, 245);
            this.lblCostoTotal.Name = "lblCostoTotal";
            this.lblCostoTotal.Size = new System.Drawing.Size(123, 25);
            this.lblCostoTotal.TabIndex = 8;
            this.lblCostoTotal.Text = "Costo total: $";
            // 
            // dtpFechaRecogida
            // 
            this.dtpFechaRecogida.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpFechaRecogida.Enabled = false;
            this.dtpFechaRecogida.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpFechaRecogida.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRecogida.Location = new System.Drawing.Point(407, 581);
            this.dtpFechaRecogida.Name = "dtpFechaRecogida";
            this.dtpFechaRecogida.Size = new System.Drawing.Size(230, 33);
            this.dtpFechaRecogida.TabIndex = 20;
            // 
            // lblFechaRecoge
            // 
            this.lblFechaRecoge.AutoSize = true;
            this.lblFechaRecoge.Enabled = false;
            this.lblFechaRecoge.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFechaRecoge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblFechaRecoge.Location = new System.Drawing.Point(402, 549);
            this.lblFechaRecoge.Name = "lblFechaRecoge";
            this.lblFechaRecoge.Size = new System.Drawing.Size(171, 25);
            this.lblFechaRecoge.TabIndex = 19;
            this.lblFechaRecoge.Text = "Cuando se recogio:";
            // 
            // dtpFechaReparacion
            // 
            this.dtpFechaReparacion.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpFechaReparacion.Enabled = false;
            this.dtpFechaReparacion.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpFechaReparacion.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaReparacion.Location = new System.Drawing.Point(406, 269);
            this.dtpFechaReparacion.Name = "dtpFechaReparacion";
            this.dtpFechaReparacion.Size = new System.Drawing.Size(230, 33);
            this.dtpFechaReparacion.TabIndex = 22;
            // 
            // lblFechaRepara
            // 
            this.lblFechaRepara.AutoSize = true;
            this.lblFechaRepara.Enabled = false;
            this.lblFechaRepara.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFechaRepara.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblFechaRepara.Location = new System.Drawing.Point(402, 236);
            this.lblFechaRepara.Name = "lblFechaRepara";
            this.lblFechaRepara.Size = new System.Drawing.Size(163, 25);
            this.lblFechaRepara.TabIndex = 21;
            this.lblFechaRepara.Text = "Cuando se reparo:";
            // 
            // CBRecoge
            // 
            this.CBRecoge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBRecoge.Enabled = false;
            this.CBRecoge.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBRecoge.FormattingEnabled = true;
            this.CBRecoge.Location = new System.Drawing.Point(406, 505);
            this.CBRecoge.Name = "CBRecoge";
            this.CBRecoge.Size = new System.Drawing.Size(271, 33);
            this.CBRecoge.TabIndex = 24;
            this.CBRecoge.SelectedIndexChanged += new System.EventHandler(this.CBRecoge_SelectedIndexChanged);
            // 
            // lblRecoge
            // 
            this.lblRecoge.AutoSize = true;
            this.lblRecoge.Enabled = false;
            this.lblRecoge.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecoge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblRecoge.Location = new System.Drawing.Point(401, 477);
            this.lblRecoge.Name = "lblRecoge";
            this.lblRecoge.Size = new System.Drawing.Size(141, 25);
            this.lblRecoge.TabIndex = 23;
            this.lblRecoge.Text = "¿Quien recoge?";
            // 
            // Extra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1152, 656);
            this.Controls.Add(this.CBRecoge);
            this.Controls.Add(this.lblRecoge);
            this.Controls.Add(this.dtpFechaReparacion);
            this.Controls.Add(this.lblFechaRepara);
            this.Controls.Add(this.dtpFechaRecogida);
            this.Controls.Add(this.lblFechaRecoge);
            this.Controls.Add(this.lblCostoTotal);
            this.Controls.Add(this.NudDescuento);
            this.Controls.Add(this.lblDescuento);
            this.Controls.Add(this.CBTipo);
            this.Controls.Add(this.lblTipo);
            this.Controls.Add(this.txtNota);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CBEntrega);
            this.Controls.Add(this.lblEntrega);
            this.Controls.Add(this.CBRepara);
            this.Controls.Add(this.lblRepara);
            this.Controls.Add(this.CBRecibe);
            this.Controls.Add(this.lblRecibe);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.dtpFechaEntrega);
            this.Controls.Add(this.lblFechaEntrega);
            this.Controls.Add(this.dtpFechaRecibido);
            this.Controls.Add(this.lblFechaPago);
            this.Controls.Add(this.NUDCosto);
            this.Controls.Add(this.lblCosto);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblNombre);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Extra";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Extra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NUDCosto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudDescuento)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.NumericUpDown NUDCosto;
        private System.Windows.Forms.Label lblCosto;
        private System.Windows.Forms.DateTimePicker dtpFechaRecibido;
        private System.Windows.Forms.Label lblFechaPago;
        private System.Windows.Forms.DateTimePicker dtpFechaEntrega;
        private System.Windows.Forms.Label lblFechaEntrega;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.ComboBox CBRecibe;
        private System.Windows.Forms.Label lblRecibe;
        private System.Windows.Forms.ComboBox CBRepara;
        private System.Windows.Forms.Label lblRepara;
        private System.Windows.Forms.ComboBox CBEntrega;
        private System.Windows.Forms.Label lblEntrega;
        private System.Windows.Forms.TextBox txtNota;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CBTipo;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.NumericUpDown NudDescuento;
        private System.Windows.Forms.Label lblDescuento;
        private System.Windows.Forms.Label lblCostoTotal;
        private System.Windows.Forms.DateTimePicker dtpFechaRecogida;
        private System.Windows.Forms.Label lblFechaRecoge;
        private System.Windows.Forms.DateTimePicker dtpFechaReparacion;
        private System.Windows.Forms.Label lblFechaRepara;
        private System.Windows.Forms.ComboBox CBRecoge;
        private System.Windows.Forms.Label lblRecoge;
    }
}