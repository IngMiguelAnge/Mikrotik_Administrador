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
            this.cbSinEntregar = new System.Windows.Forms.CheckBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.CBRecibe = new System.Windows.Forms.ComboBox();
            this.lblRecibe = new System.Windows.Forms.Label();
            this.CBRepara = new System.Windows.Forms.ComboBox();
            this.lblRepara = new System.Windows.Forms.Label();
            this.CBEntrega = new System.Windows.Forms.ComboBox();
            this.lblEntrega = new System.Windows.Forms.Label();
            this.txtNota = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NUDCosto)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNombre
            // 
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNombre.Location = new System.Drawing.Point(32, 61);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(280, 34);
            this.txtNombre.TabIndex = 1;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblNombre.Location = new System.Drawing.Point(28, 35);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(166, 25);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "*Servicio o objeto:";
            // 
            // NUDCosto
            // 
            this.NUDCosto.DecimalPlaces = 2;
            this.NUDCosto.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.NUDCosto.Location = new System.Drawing.Point(32, 156);
            this.NUDCosto.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.NUDCosto.Name = "NUDCosto";
            this.NUDCosto.Size = new System.Drawing.Size(140, 33);
            this.NUDCosto.TabIndex = 3;
            // 
            // lblCosto
            // 
            this.lblCosto.AutoSize = true;
            this.lblCosto.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblCosto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblCosto.Location = new System.Drawing.Point(27, 128);
            this.lblCosto.Name = "lblCosto";
            this.lblCosto.Size = new System.Drawing.Size(64, 25);
            this.lblCosto.TabIndex = 2;
            this.lblCosto.Text = "Costo:";
            // 
            // dtpFechaRecibido
            // 
            this.dtpFechaRecibido.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpFechaRecibido.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpFechaRecibido.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRecibido.Location = new System.Drawing.Point(364, 62);
            this.dtpFechaRecibido.Name = "dtpFechaRecibido";
            this.dtpFechaRecibido.Size = new System.Drawing.Size(230, 33);
            this.dtpFechaRecibido.TabIndex = 9;
            // 
            // lblFechaPago
            // 
            this.lblFechaPago.AutoSize = true;
            this.lblFechaPago.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFechaPago.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblFechaPago.Location = new System.Drawing.Point(360, 34);
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
            this.dtpFechaEntrega.Location = new System.Drawing.Point(364, 224);
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
            this.lblFechaEntrega.Location = new System.Drawing.Point(360, 196);
            this.lblFechaEntrega.Name = "lblFechaEntrega";
            this.lblFechaEntrega.Size = new System.Drawing.Size(175, 25);
            this.lblFechaEntrega.TabIndex = 11;
            this.lblFechaEntrega.Text = "Cuando se entrego:";
            // 
            // cbSinEntregar
            // 
            this.cbSinEntregar.AutoSize = true;
            this.cbSinEntregar.Checked = true;
            this.cbSinEntregar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSinEntregar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.cbSinEntregar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.cbSinEntregar.Location = new System.Drawing.Point(365, 124);
            this.cbSinEntregar.Name = "cbSinEntregar";
            this.cbSinEntregar.Size = new System.Drawing.Size(207, 29);
            this.cbSinEntregar.TabIndex = 10;
            this.cbSinEntregar.Text = "¿Aun no entregado?";
            this.cbSinEntregar.UseVisualStyleBackColor = true;
            this.cbSinEntregar.CheckedChanged += new System.EventHandler(this.cbSinEntregar_CheckedChanged);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(866, 390);
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
            this.CBRecibe.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBRecibe.FormattingEnabled = true;
            this.CBRecibe.Location = new System.Drawing.Point(32, 224);
            this.CBRecibe.Name = "CBRecibe";
            this.CBRecibe.Size = new System.Drawing.Size(271, 33);
            this.CBRecibe.TabIndex = 5;
            // 
            // lblRecibe
            // 
            this.lblRecibe.AutoSize = true;
            this.lblRecibe.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecibe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblRecibe.Location = new System.Drawing.Point(28, 196);
            this.lblRecibe.Name = "lblRecibe";
            this.lblRecibe.Size = new System.Drawing.Size(147, 25);
            this.lblRecibe.TabIndex = 4;
            this.lblRecibe.Text = "*¿Quien recibe?:";
            // 
            // CBRepara
            // 
            this.CBRepara.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBRepara.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBRepara.FormattingEnabled = true;
            this.CBRepara.Location = new System.Drawing.Point(32, 322);
            this.CBRepara.Name = "CBRepara";
            this.CBRepara.Size = new System.Drawing.Size(271, 33);
            this.CBRepara.TabIndex = 7;
            // 
            // lblRepara
            // 
            this.lblRepara.AutoSize = true;
            this.lblRepara.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRepara.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblRepara.Location = new System.Drawing.Point(28, 294);
            this.lblRepara.Name = "lblRepara";
            this.lblRepara.Size = new System.Drawing.Size(228, 25);
            this.lblRepara.TabIndex = 6;
            this.lblRepara.Text = "¿Quien repara? (Opcional)";
            // 
            // CBEntrega
            // 
            this.CBEntrega.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBEntrega.Enabled = false;
            this.CBEntrega.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBEntrega.FormattingEnabled = true;
            this.CBEntrega.Location = new System.Drawing.Point(365, 322);
            this.CBEntrega.Name = "CBEntrega";
            this.CBEntrega.Size = new System.Drawing.Size(271, 33);
            this.CBEntrega.TabIndex = 14;
            // 
            // lblEntrega
            // 
            this.lblEntrega.AutoSize = true;
            this.lblEntrega.Enabled = false;
            this.lblEntrega.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntrega.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblEntrega.Location = new System.Drawing.Point(360, 294);
            this.lblEntrega.Name = "lblEntrega";
            this.lblEntrega.Size = new System.Drawing.Size(157, 25);
            this.lblEntrega.TabIndex = 13;
            this.lblEntrega.Text = "*¿Quien entrega?";
            // 
            // txtNota
            // 
            this.txtNota.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNota.Location = new System.Drawing.Point(658, 61);
            this.txtNota.Multiline = true;
            this.txtNota.Name = "txtNota";
            this.txtNota.Size = new System.Drawing.Size(328, 322);
            this.txtNota.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.label4.Location = new System.Drawing.Point(653, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 25);
            this.label4.TabIndex = 15;
            this.label4.Text = "Nota:";
            // 
            // Extra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1007, 440);
            this.Controls.Add(this.txtNota);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CBEntrega);
            this.Controls.Add(this.lblEntrega);
            this.Controls.Add(this.CBRepara);
            this.Controls.Add(this.lblRepara);
            this.Controls.Add(this.CBRecibe);
            this.Controls.Add(this.lblRecibe);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.cbSinEntregar);
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
        private System.Windows.Forms.CheckBox cbSinEntregar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.ComboBox CBRecibe;
        private System.Windows.Forms.Label lblRecibe;
        private System.Windows.Forms.ComboBox CBRepara;
        private System.Windows.Forms.Label lblRepara;
        private System.Windows.Forms.ComboBox CBEntrega;
        private System.Windows.Forms.Label lblEntrega;
        private System.Windows.Forms.TextBox txtNota;
        private System.Windows.Forms.Label label4;
    }
}