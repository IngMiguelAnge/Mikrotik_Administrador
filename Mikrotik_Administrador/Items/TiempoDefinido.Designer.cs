namespace Mikrotik_Administrador.Items
{
    partial class TiempoDefinido
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
            this.lblTiempo = new System.Windows.Forms.Label();
            this.lblDias = new System.Windows.Forms.Label();
            this.lblHoras = new System.Windows.Forms.Label();
            this.NUDDias = new System.Windows.Forms.NumericUpDown();
            this.NUDHoras = new System.Windows.Forms.NumericUpDown();
            this.btnTEST = new System.Windows.Forms.Button();
            this.btnCobro = new System.Windows.Forms.Button();
            this.lblFechaInicio = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.lblFechaFinal = new System.Windows.Forms.Label();
            this.lblFechaFin = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NUDDias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDHoras)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTiempo
            // 
            this.lblTiempo.AutoSize = true;
            this.lblTiempo.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblTiempo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblTiempo.Location = new System.Drawing.Point(40, 35);
            this.lblTiempo.Name = "lblTiempo";
            this.lblTiempo.Size = new System.Drawing.Size(268, 28);
            this.lblTiempo.TabIndex = 0;
            this.lblTiempo.Text = "Tiempo que dura el cambio:";
            // 
            // lblDias
            // 
            this.lblDias.AutoSize = true;
            this.lblDias.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDias.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblDias.Location = new System.Drawing.Point(40, 85);
            this.lblDias.Name = "lblDias";
            this.lblDias.Size = new System.Drawing.Size(52, 25);
            this.lblDias.TabIndex = 1;
            this.lblDias.Text = "DÍAS";
            // 
            // lblHoras
            // 
            this.lblHoras.AutoSize = true;
            this.lblHoras.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblHoras.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblHoras.Location = new System.Drawing.Point(195, 85);
            this.lblHoras.Name = "lblHoras";
            this.lblHoras.Size = new System.Drawing.Size(72, 25);
            this.lblHoras.TabIndex = 2;
            this.lblHoras.Text = "HORAS";
            // 
            // NUDDias
            // 
            this.NUDDias.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.NUDDias.Location = new System.Drawing.Point(44, 115);
            this.NUDDias.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.NUDDias.Name = "NUDDias";
            this.NUDDias.Size = new System.Drawing.Size(130, 33);
            this.NUDDias.TabIndex = 3;
            this.NUDDias.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.NUDDias.ValueChanged += new System.EventHandler(this.NUDDias_ValueChanged);
            // 
            // NUDHoras
            // 
            this.NUDHoras.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.NUDHoras.Location = new System.Drawing.Point(199, 115);
            this.NUDHoras.Name = "NUDHoras";
            this.NUDHoras.Size = new System.Drawing.Size(130, 33);
            this.NUDHoras.TabIndex = 4;
            this.NUDHoras.ValueChanged += new System.EventHandler(this.NUDHoras_ValueChanged);
            // 
            // btnTEST
            // 
            this.btnTEST.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.btnTEST.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTEST.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnTEST.ForeColor = System.Drawing.Color.White;
            this.btnTEST.Location = new System.Drawing.Point(45, 370);
            this.btnTEST.Name = "btnTEST";
            this.btnTEST.Size = new System.Drawing.Size(110, 38);
            this.btnTEST.TabIndex = 5;
            this.btnTEST.Text = "TEST";
            this.btnTEST.UseVisualStyleBackColor = false;
            this.btnTEST.Click += new System.EventHandler(this.btnTEST_Click);
            // 
            // btnCobro
            // 
            this.btnCobro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnCobro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCobro.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCobro.ForeColor = System.Drawing.Color.White;
            this.btnCobro.Location = new System.Drawing.Point(210, 370);
            this.btnCobro.Name = "btnCobro";
            this.btnCobro.Size = new System.Drawing.Size(120, 38);
            this.btnCobro.TabIndex = 6;
            this.btnCobro.Text = "COBRO";
            this.btnCobro.UseVisualStyleBackColor = false;
            this.btnCobro.Click += new System.EventHandler(this.btnCobro_Click);
            // 
            // lblFechaInicio
            // 
            this.lblFechaInicio.AutoSize = true;
            this.lblFechaInicio.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblFechaInicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblFechaInicio.Location = new System.Drawing.Point(39, 174);
            this.lblFechaInicio.Name = "lblFechaInicio";
            this.lblFechaInicio.Size = new System.Drawing.Size(250, 28);
            this.lblFechaInicio.TabIndex = 7;
            this.lblFechaInicio.Text = "Cuando iniciara el cambio:";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "dd/MM/yyyy hh:mm tt";
            this.dtpFechaInicio.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(44, 221);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(285, 33);
            this.dtpFechaInicio.TabIndex = 8;
            this.dtpFechaInicio.ValueChanged += new System.EventHandler(this.dtpFechaInicio_ValueChanged);
            // 
            // lblFechaFinal
            // 
            this.lblFechaFinal.AutoSize = true;
            this.lblFechaFinal.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblFechaFinal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblFechaFinal.Location = new System.Drawing.Point(40, 274);
            this.lblFechaFinal.Name = "lblFechaFinal";
            this.lblFechaFinal.Size = new System.Drawing.Size(186, 28);
            this.lblFechaFinal.TabIndex = 9;
            this.lblFechaFinal.Text = "Fecha que termina:";
            // 
            // lblFechaFin
            // 
            this.lblFechaFin.AutoSize = true;
            this.lblFechaFin.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblFechaFin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblFechaFin.Location = new System.Drawing.Point(40, 321);
            this.lblFechaFin.Name = "lblFechaFin";
            this.lblFechaFin.Size = new System.Drawing.Size(186, 28);
            this.lblFechaFin.TabIndex = 10;
            this.lblFechaFin.Text = "Fecha que termina:";
            // 
            // TiempoDefinido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(375, 421);
            this.Controls.Add(this.lblFechaFin);
            this.Controls.Add(this.lblFechaFinal);
            this.Controls.Add(this.dtpFechaInicio);
            this.Controls.Add(this.lblFechaInicio);
            this.Controls.Add(this.btnCobro);
            this.Controls.Add(this.btnTEST);
            this.Controls.Add(this.NUDHoras);
            this.Controls.Add(this.NUDDias);
            this.Controls.Add(this.lblHoras);
            this.Controls.Add(this.lblDias);
            this.Controls.Add(this.lblTiempo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TiempoDefinido";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.TiempoDefinido_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NUDDias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDHoras)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Label lblTiempo;
        private System.Windows.Forms.Label lblDias;
        private System.Windows.Forms.Label lblHoras;
        private System.Windows.Forms.NumericUpDown NUDDias;
        private System.Windows.Forms.NumericUpDown NUDHoras;
        private System.Windows.Forms.Button btnTEST;
        private System.Windows.Forms.Button btnCobro;
        private System.Windows.Forms.Label lblFechaInicio;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Label lblFechaFinal;
        private System.Windows.Forms.Label lblFechaFin;
    }
}