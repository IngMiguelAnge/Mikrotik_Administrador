namespace Mikrotik_Administrador.Items
{
    partial class IniciarPagos
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
            this.lblFechaInicio = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.lblCostoFinal = new System.Windows.Forms.Label();
            this.lblMensualidadOriginal = new System.Windows.Forms.Label();
            this.lblCostoCambios = new System.Windows.Forms.Label();
            this.lblMensualidadActual = new System.Windows.Forms.Label();
            this.lblFechaCorte = new System.Windows.Forms.Label();
            this.lblDiaCorte = new System.Windows.Forms.Label();
            this.NUDCorte = new System.Windows.Forms.NumericUpDown();
            this.lblDias = new System.Windows.Forms.Label();
            this.lblSincambios = new System.Windows.Forms.Label();
            this.panelContenedor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUDCorte)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFechaInicio
            // 
            this.lblFechaInicio.AutoSize = true;
            this.lblFechaInicio.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFechaInicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblFechaInicio.Location = new System.Drawing.Point(26, 20);
            this.lblFechaInicio.Name = "lblFechaInicio";
            this.lblFechaInicio.Size = new System.Drawing.Size(301, 25);
            this.lblFechaInicio.TabIndex = 0;
            this.lblFechaInicio.Text = "Fecha en que inicio la mensualidad";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFechaInicio.Location = new System.Drawing.Point(23, 63);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(393, 34);
            this.dtpFechaInicio.TabIndex = 1;
            this.dtpFechaInicio.ValueChanged += new System.EventHandler(this.dtpFechaInicio_ValueChanged);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(347, 429);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(120, 38);
            this.btnGuardar.TabIndex = 10;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // panelContenedor
            // 
            this.panelContenedor.Controls.Add(this.lblSincambios);
            this.panelContenedor.Controls.Add(this.lblCostoFinal);
            this.panelContenedor.Controls.Add(this.lblMensualidadOriginal);
            this.panelContenedor.Controls.Add(this.lblCostoCambios);
            this.panelContenedor.Controls.Add(this.lblMensualidadActual);
            this.panelContenedor.Controls.Add(this.lblFechaCorte);
            this.panelContenedor.Controls.Add(this.lblDiaCorte);
            this.panelContenedor.Controls.Add(this.NUDCorte);
            this.panelContenedor.Controls.Add(this.lblDias);
            this.panelContenedor.Controls.Add(this.btnGuardar);
            this.panelContenedor.Controls.Add(this.dtpFechaInicio);
            this.panelContenedor.Controls.Add(this.lblFechaInicio);
            this.panelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedor.Location = new System.Drawing.Point(0, 0);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Padding = new System.Windows.Forms.Padding(20);
            this.panelContenedor.Size = new System.Drawing.Size(503, 490);
            this.panelContenedor.TabIndex = 4;
            // 
            // lblCostoFinal
            // 
            this.lblCostoFinal.AutoSize = true;
            this.lblCostoFinal.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblCostoFinal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblCostoFinal.Location = new System.Drawing.Point(26, 381);
            this.lblCostoFinal.Name = "lblCostoFinal";
            this.lblCostoFinal.Size = new System.Drawing.Size(272, 25);
            this.lblCostoFinal.TabIndex = 11;
            this.lblCostoFinal.Text = "Costo total por la mensualidad:";
            // 
            // lblMensualidadOriginal
            // 
            this.lblMensualidadOriginal.AutoSize = true;
            this.lblMensualidadOriginal.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblMensualidadOriginal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblMensualidadOriginal.Location = new System.Drawing.Point(26, 227);
            this.lblMensualidadOriginal.Name = "lblMensualidadOriginal";
            this.lblMensualidadOriginal.Size = new System.Drawing.Size(389, 25);
            this.lblMensualidadOriginal.TabIndex = 7;
            this.lblMensualidadOriginal.Text = "Costo de la mensualidad original del servicio:";
            // 
            // lblCostoCambios
            // 
            this.lblCostoCambios.AutoSize = true;
            this.lblCostoCambios.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblCostoCambios.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblCostoCambios.Location = new System.Drawing.Point(26, 310);
            this.lblCostoCambios.Name = "lblCostoCambios";
            this.lblCostoCambios.Size = new System.Drawing.Size(266, 25);
            this.lblCostoCambios.TabIndex = 9;
            this.lblCostoCambios.Text = "Costo a pagar por los cambios:";
            // 
            // lblMensualidadActual
            // 
            this.lblMensualidadActual.AutoSize = true;
            this.lblMensualidadActual.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblMensualidadActual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblMensualidadActual.Location = new System.Drawing.Point(26, 269);
            this.lblMensualidadActual.Name = "lblMensualidadActual";
            this.lblMensualidadActual.Size = new System.Drawing.Size(374, 25);
            this.lblMensualidadActual.TabIndex = 8;
            this.lblMensualidadActual.Text = "Costo de la mensualidad actual del servicio:";
            // 
            // lblFechaCorte
            // 
            this.lblFechaCorte.AutoSize = true;
            this.lblFechaCorte.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFechaCorte.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblFechaCorte.Location = new System.Drawing.Point(194, 167);
            this.lblFechaCorte.Name = "lblFechaCorte";
            this.lblFechaCorte.Size = new System.Drawing.Size(55, 25);
            this.lblFechaCorte.TabIndex = 6;
            this.lblFechaCorte.Text = "fecha";
            // 
            // lblDiaCorte
            // 
            this.lblDiaCorte.AutoSize = true;
            this.lblDiaCorte.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDiaCorte.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblDiaCorte.Location = new System.Drawing.Point(194, 128);
            this.lblDiaCorte.Name = "lblDiaCorte";
            this.lblDiaCorte.Size = new System.Drawing.Size(273, 25);
            this.lblDiaCorte.TabIndex = 5;
            this.lblDiaCorte.Text = "La mensualidad se pagara hasta";
            // 
            // NUDCorte
            // 
            this.NUDCorte.Location = new System.Drawing.Point(31, 165);
            this.NUDCorte.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.NUDCorte.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUDCorte.Name = "NUDCorte";
            this.NUDCorte.Size = new System.Drawing.Size(120, 31);
            this.NUDCorte.TabIndex = 4;
            this.NUDCorte.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUDCorte.ValueChanged += new System.EventHandler(this.NUDCorte_ValueChanged);
            // 
            // lblDias
            // 
            this.lblDias.AutoSize = true;
            this.lblDias.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDias.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblDias.Location = new System.Drawing.Point(26, 128);
            this.lblDias.Name = "lblDias";
            this.lblDias.Size = new System.Drawing.Size(118, 25);
            this.lblDias.TabIndex = 3;
            this.lblDias.Text = "Día de corte:";
            // 
            // lblSincambios
            // 
            this.lblSincambios.AutoSize = true;
            this.lblSincambios.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSincambios.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblSincambios.Location = new System.Drawing.Point(26, 344);
            this.lblSincambios.Name = "lblSincambios";
            this.lblSincambios.Size = new System.Drawing.Size(304, 25);
            this.lblSincambios.TabIndex = 12;
            this.lblSincambios.Text = "Costo a pagar por días sin cambios:";
            // 
            // IniciarPagos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(503, 490);
            this.Controls.Add(this.panelContenedor);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IniciarPagos";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.IniciarPagos_Load);
            this.panelContenedor.ResumeLayout(false);
            this.panelContenedor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUDCorte)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblFechaInicio;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.NumericUpDown NUDCorte;
        private System.Windows.Forms.Label lblDias;
        private System.Windows.Forms.Label lblDiaCorte;
        private System.Windows.Forms.Label lblFechaCorte;
        private System.Windows.Forms.Label lblMensualidadActual;
        private System.Windows.Forms.Label lblCostoCambios;
        private System.Windows.Forms.Label lblMensualidadOriginal;
        private System.Windows.Forms.Label lblCostoFinal;
        private System.Windows.Forms.Label lblSincambios;
    }
}