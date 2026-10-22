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
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.lblFecha = new System.Windows.Forms.Label();
            this.NUDPuertoInterno = new System.Windows.Forms.NumericUpDown();
            this.lblPuertoInterno = new System.Windows.Forms.Label();
            this.lblSpliter = new System.Windows.Forms.Label();
            this.txtSpliter = new System.Windows.Forms.TextBox();
            this.lblCaja = new System.Windows.Forms.Label();
            this.txtCaja = new System.Windows.Forms.TextBox();
            this.NUDPuerto = new System.Windows.Forms.NumericUpDown();
            this.CBInstalador = new System.Windows.Forms.ComboBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lblPuerto = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NUDPuertoInterno)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDPuerto)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpFecha
            // 
            this.dtpFecha.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpFecha.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.Location = new System.Drawing.Point(37, 313);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(230, 33);
            this.dtpFecha.TabIndex = 11;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFecha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblFecha.Location = new System.Drawing.Point(33, 285);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(178, 25);
            this.lblFecha.TabIndex = 10;
            this.lblFecha.Text = "Fecha de instalación";
            // 
            // NUDPuertoInterno
            // 
            this.NUDPuertoInterno.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.NUDPuertoInterno.Location = new System.Drawing.Point(369, 229);
            this.NUDPuertoInterno.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.NUDPuertoInterno.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUDPuertoInterno.Name = "NUDPuertoInterno";
            this.NUDPuertoInterno.Size = new System.Drawing.Size(140, 33);
            this.NUDPuertoInterno.TabIndex = 9;
            this.NUDPuertoInterno.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblPuertoInterno
            // 
            this.lblPuertoInterno.AutoSize = true;
            this.lblPuertoInterno.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPuertoInterno.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblPuertoInterno.Location = new System.Drawing.Point(364, 201);
            this.lblPuertoInterno.Name = "lblPuertoInterno";
            this.lblPuertoInterno.Size = new System.Drawing.Size(149, 25);
            this.lblPuertoInterno.TabIndex = 8;
            this.lblPuertoInterno.Text = "*Puerto Interno:";
            // 
            // lblSpliter
            // 
            this.lblSpliter.AutoSize = true;
            this.lblSpliter.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpliter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblSpliter.Location = new System.Drawing.Point(32, 201);
            this.lblSpliter.Name = "lblSpliter";
            this.lblSpliter.Size = new System.Drawing.Size(80, 25);
            this.lblSpliter.TabIndex = 6;
            this.lblSpliter.Text = "*Spliter:";
            // 
            // txtSpliter
            // 
            this.txtSpliter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSpliter.Location = new System.Drawing.Point(37, 229);
            this.txtSpliter.MaxLength = 50;
            this.txtSpliter.Name = "txtSpliter";
            this.txtSpliter.Size = new System.Drawing.Size(280, 34);
            this.txtSpliter.TabIndex = 7;
            // 
            // lblCaja
            // 
            this.lblCaja.AutoSize = true;
            this.lblCaja.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaja.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblCaja.Location = new System.Drawing.Point(207, 126);
            this.lblCaja.Name = "lblCaja";
            this.lblCaja.Size = new System.Drawing.Size(58, 25);
            this.lblCaja.TabIndex = 4;
            this.lblCaja.Text = "*Caja:";
            // 
            // txtCaja
            // 
            this.txtCaja.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCaja.Location = new System.Drawing.Point(212, 154);
            this.txtCaja.MaxLength = 50;
            this.txtCaja.Name = "txtCaja";
            this.txtCaja.Size = new System.Drawing.Size(280, 34);
            this.txtCaja.TabIndex = 5;
            // 
            // NUDPuerto
            // 
            this.NUDPuerto.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.NUDPuerto.Location = new System.Drawing.Point(37, 154);
            this.NUDPuerto.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.NUDPuerto.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUDPuerto.Name = "NUDPuerto";
            this.NUDPuerto.Size = new System.Drawing.Size(140, 33);
            this.NUDPuerto.TabIndex = 3;
            this.NUDPuerto.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // CBInstalador
            // 
            this.CBInstalador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBInstalador.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBInstalador.FormattingEnabled = true;
            this.CBInstalador.Location = new System.Drawing.Point(37, 57);
            this.CBInstalador.Name = "CBInstalador";
            this.CBInstalador.Size = new System.Drawing.Size(271, 33);
            this.CBInstalador.TabIndex = 1;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(373, 313);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(140, 38);
            this.btnGuardar.TabIndex = 12;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // lblPuerto
            // 
            this.lblPuerto.AutoSize = true;
            this.lblPuerto.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPuerto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblPuerto.Location = new System.Drawing.Point(27, 121);
            this.lblPuerto.Name = "lblPuerto";
            this.lblPuerto.Size = new System.Drawing.Size(82, 25);
            this.lblPuerto.TabIndex = 2;
            this.lblPuerto.Text = "*Puerto:";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblNombre.Location = new System.Drawing.Point(27, 19);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(108, 25);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "*Instalador:";
            // 
            // Instalacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(570, 371);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.NUDPuertoInterno);
            this.Controls.Add(this.lblPuertoInterno);
            this.Controls.Add(this.lblSpliter);
            this.Controls.Add(this.txtSpliter);
            this.Controls.Add(this.lblCaja);
            this.Controls.Add(this.txtCaja);
            this.Controls.Add(this.NUDPuerto);
            this.Controls.Add(this.CBInstalador);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.lblPuerto);
            this.Controls.Add(this.lblNombre);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Instalacion";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Instalacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NUDPuertoInterno)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDPuerto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.NumericUpDown NUDPuertoInterno;
        private System.Windows.Forms.Label lblPuertoInterno;
        private System.Windows.Forms.Label lblSpliter;
        private System.Windows.Forms.TextBox txtSpliter;
        private System.Windows.Forms.Label lblCaja;
        private System.Windows.Forms.TextBox txtCaja;
        private System.Windows.Forms.NumericUpDown NUDPuerto;
        private System.Windows.Forms.ComboBox CBInstalador;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblPuerto;
        private System.Windows.Forms.Label lblNombre;
    }
}