namespace Mikrotik_Administrador.Items
{
    partial class Pagar
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
            this.lblBanco = new System.Windows.Forms.Label();
            this.CBBanco = new System.Windows.Forms.ComboBox();
            this.lblReferencia = new System.Windows.Forms.Label();
            this.txtReferencia = new System.Windows.Forms.TextBox();
            this.lblFechaPago = new System.Windows.Forms.Label();
            this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnSubir = new System.Windows.Forms.Button();
            this.PBImagen = new System.Windows.Forms.PictureBox();
            this.lblComentario = new System.Windows.Forms.Label();
            this.txtComentario = new System.Windows.Forms.TextBox();
            this.GBDetalles = new System.Windows.Forms.GroupBox();
            this.dgvDetalles = new System.Windows.Forms.DataGridView();
            this.BGRPagos = new System.Windows.Forms.GroupBox();
            this.lblMensaje2 = new System.Windows.Forms.Label();
            this.GBReferencias = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.PBImagen)).BeginInit();
            this.GBDetalles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).BeginInit();
            this.BGRPagos.SuspendLayout();
            this.GBReferencias.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblBanco
            // 
            this.lblBanco.AutoSize = true;
            this.lblBanco.Location = new System.Drawing.Point(23, 40);
            this.lblBanco.Name = "lblBanco";
            this.lblBanco.Size = new System.Drawing.Size(199, 20);
            this.lblBanco.TabIndex = 17;
            this.lblBanco.Text = "*Banco que recibe el pago:";
            // 
            // CBBanco
            // 
            this.CBBanco.FormattingEnabled = true;
            this.CBBanco.Location = new System.Drawing.Point(27, 69);
            this.CBBanco.Name = "CBBanco";
            this.CBBanco.Size = new System.Drawing.Size(219, 28);
            this.CBBanco.TabIndex = 18;
            // 
            // lblReferencia
            // 
            this.lblReferencia.AutoSize = true;
            this.lblReferencia.Location = new System.Drawing.Point(436, 24);
            this.lblReferencia.Name = "lblReferencia";
            this.lblReferencia.Size = new System.Drawing.Size(97, 20);
            this.lblReferencia.TabIndex = 19;
            this.lblReferencia.Text = "*Referencia:";
            // 
            // txtReferencia
            // 
            this.txtReferencia.Location = new System.Drawing.Point(440, 61);
            this.txtReferencia.MaxLength = 250;
            this.txtReferencia.Name = "txtReferencia";
            this.txtReferencia.Size = new System.Drawing.Size(261, 26);
            this.txtReferencia.TabIndex = 20;
            // 
            // lblFechaPago
            // 
            this.lblFechaPago.AutoSize = true;
            this.lblFechaPago.Location = new System.Drawing.Point(23, 115);
            this.lblFechaPago.Name = "lblFechaPago";
            this.lblFechaPago.Size = new System.Drawing.Size(196, 20);
            this.lblFechaPago.TabIndex = 21;
            this.lblFechaPago.Text = "Cuando se realizo el pago:";
            // 
            // dtpFechaPago
            // 
            this.dtpFechaPago.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpFechaPago.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaPago.Location = new System.Drawing.Point(27, 149);
            this.dtpFechaPago.Name = "dtpFechaPago";
            this.dtpFechaPago.Size = new System.Drawing.Size(219, 26);
            this.dtpFechaPago.TabIndex = 22;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(640, 337);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(92, 39);
            this.btnGuardar.TabIndex = 23;
            this.btnGuardar.Text = "Continuar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnSubir
            // 
            this.btnSubir.Location = new System.Drawing.Point(140, 337);
            this.btnSubir.Name = "btnSubir";
            this.btnSubir.Size = new System.Drawing.Size(125, 39);
            this.btnSubir.TabIndex = 24;
            this.btnSubir.Text = "Subir imagen";
            this.btnSubir.UseVisualStyleBackColor = true;
            this.btnSubir.Click += new System.EventHandler(this.btnSubir_Click);
            // 
            // PBImagen
            // 
            this.PBImagen.Location = new System.Drawing.Point(23, 49);
            this.PBImagen.Name = "PBImagen";
            this.PBImagen.Size = new System.Drawing.Size(357, 274);
            this.PBImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PBImagen.TabIndex = 25;
            this.PBImagen.TabStop = false;
            // 
            // lblComentario
            // 
            this.lblComentario.AutoSize = true;
            this.lblComentario.Location = new System.Drawing.Point(436, 104);
            this.lblComentario.Name = "lblComentario";
            this.lblComentario.Size = new System.Drawing.Size(95, 20);
            this.lblComentario.TabIndex = 26;
            this.lblComentario.Text = "Comentario:";
            // 
            // txtComentario
            // 
            this.txtComentario.Location = new System.Drawing.Point(440, 137);
            this.txtComentario.Multiline = true;
            this.txtComentario.Name = "txtComentario";
            this.txtComentario.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComentario.Size = new System.Drawing.Size(292, 186);
            this.txtComentario.TabIndex = 27;
            // 
            // GBDetalles
            // 
            this.GBDetalles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBDetalles.Controls.Add(this.dgvDetalles);
            this.GBDetalles.Location = new System.Drawing.Point(23, 409);
            this.GBDetalles.Name = "GBDetalles";
            this.GBDetalles.Size = new System.Drawing.Size(1195, 472);
            this.GBDetalles.TabIndex = 28;
            this.GBDetalles.TabStop = false;
            this.GBDetalles.Text = "Detalles";
            // 
            // dgvDetalles
            // 
            this.dgvDetalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetalles.Location = new System.Drawing.Point(3, 22);
            this.dgvDetalles.Name = "dgvDetalles";
            this.dgvDetalles.RowHeadersWidth = 62;
            this.dgvDetalles.RowTemplate.Height = 28;
            this.dgvDetalles.Size = new System.Drawing.Size(1189, 447);
            this.dgvDetalles.TabIndex = 0;
            // 
            // BGRPagos
            // 
            this.BGRPagos.Controls.Add(this.dtpFechaPago);
            this.BGRPagos.Controls.Add(this.lblBanco);
            this.BGRPagos.Controls.Add(this.CBBanco);
            this.BGRPagos.Controls.Add(this.lblFechaPago);
            this.BGRPagos.Location = new System.Drawing.Point(23, 22);
            this.BGRPagos.Name = "BGRPagos";
            this.BGRPagos.Size = new System.Drawing.Size(298, 308);
            this.BGRPagos.TabIndex = 29;
            this.BGRPagos.TabStop = false;
            this.BGRPagos.Text = "Datos complementarios";
            // 
            // lblMensaje2
            // 
            this.lblMensaje2.AutoSize = true;
            this.lblMensaje2.Location = new System.Drawing.Point(19, 12);
            this.lblMensaje2.Name = "lblMensaje2";
            this.lblMensaje2.Size = new System.Drawing.Size(196, 20);
            this.lblMensaje2.TabIndex = 30;
            this.lblMensaje2.Text = "*Imagen del comprobante:";
            // 
            // GBReferencias
            // 
            this.GBReferencias.Controls.Add(this.btnGuardar);
            this.GBReferencias.Controls.Add(this.PBImagen);
            this.GBReferencias.Controls.Add(this.lblMensaje2);
            this.GBReferencias.Controls.Add(this.btnSubir);
            this.GBReferencias.Controls.Add(this.txtComentario);
            this.GBReferencias.Controls.Add(this.txtReferencia);
            this.GBReferencias.Controls.Add(this.lblComentario);
            this.GBReferencias.Controls.Add(this.lblReferencia);
            this.GBReferencias.Location = new System.Drawing.Point(338, 12);
            this.GBReferencias.Name = "GBReferencias";
            this.GBReferencias.Size = new System.Drawing.Size(746, 391);
            this.GBReferencias.TabIndex = 31;
            // 
            // Pagar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1242, 900);
            this.Controls.Add(this.GBReferencias);
            this.Controls.Add(this.BGRPagos);
            this.Controls.Add(this.GBDetalles);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Pagar";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Pagar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PBImagen)).EndInit();
            this.GBDetalles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).EndInit();
            this.BGRPagos.ResumeLayout(false);
            this.BGRPagos.PerformLayout();
            this.GBReferencias.ResumeLayout(false);
            this.GBReferencias.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblBanco;
        private System.Windows.Forms.ComboBox CBBanco;
        private System.Windows.Forms.Label lblReferencia;
        private System.Windows.Forms.TextBox txtReferencia;
        private System.Windows.Forms.Label lblFechaPago;
        private System.Windows.Forms.DateTimePicker dtpFechaPago;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnSubir;
        private System.Windows.Forms.PictureBox PBImagen;
        private System.Windows.Forms.Label lblComentario;
        private System.Windows.Forms.TextBox txtComentario;
        private System.Windows.Forms.GroupBox GBDetalles;
        private System.Windows.Forms.GroupBox BGRPagos;
        private System.Windows.Forms.Label lblMensaje2;
        private System.Windows.Forms.Panel GBReferencias;
        private System.Windows.Forms.DataGridView dgvDetalles;
    }
}