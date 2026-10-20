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
            this.lblNombre.Size = new System.Drawing.Size(93, 25);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "*Nombre:";
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
            this.dtpFechaRecibido.TabIndex = 5;
            // 
            // lblFechaPago
            // 
            this.lblFechaPago.AutoSize = true;
            this.lblFechaPago.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFechaPago.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblFechaPago.Location = new System.Drawing.Point(360, 34);
            this.lblFechaPago.Name = "lblFechaPago";
            this.lblFechaPago.Size = new System.Drawing.Size(204, 25);
            this.lblFechaPago.TabIndex = 4;
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
            this.dtpFechaEntrega.TabIndex = 8;
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
            this.lblFechaEntrega.TabIndex = 7;
            this.lblFechaEntrega.Text = "Cuando se entrego:";
            // 
            // cbSinEntregar
            // 
            this.cbSinEntregar.AutoSize = true;
            this.cbSinEntregar.Checked = true;
            this.cbSinEntregar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSinEntregar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.cbSinEntregar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.cbSinEntregar.Location = new System.Drawing.Point(364, 141);
            this.cbSinEntregar.Name = "cbSinEntregar";
            this.cbSinEntregar.Size = new System.Drawing.Size(207, 29);
            this.cbSinEntregar.TabIndex = 6;
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
            this.btnGuardar.Location = new System.Drawing.Point(474, 303);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(120, 38);
            this.btnGuardar.TabIndex = 9;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // Extra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(656, 355);
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
    }
}