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
            this.lblDias = new System.Windows.Forms.Label();
            this.NUDCorte = new System.Windows.Forms.NumericUpDown();
            this.lblDiaCorte = new System.Windows.Forms.Label();
            this.lblFechaCorte = new System.Windows.Forms.Label();
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
            this.lblFechaInicio.Size = new System.Drawing.Size(260, 25);
            this.lblFechaInicio.TabIndex = 0;
            this.lblFechaInicio.Text = "Fecha en que inicio el servicio";
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
            this.btnGuardar.Location = new System.Drawing.Point(306, 269);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(120, 38);
            this.btnGuardar.TabIndex = 2;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // panelContenedor
            // 
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
            this.panelContenedor.Size = new System.Drawing.Size(464, 330);
            this.panelContenedor.TabIndex = 4;
            // 
            // lblDias
            // 
            this.lblDias.AutoSize = true;
            this.lblDias.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDias.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblDias.Location = new System.Drawing.Point(26, 128);
            this.lblDias.Name = "lblDias";
            this.lblDias.Size = new System.Drawing.Size(211, 25);
            this.lblDias.TabIndex = 3;
            this.lblDias.Text = "En que dia sera el corte:";
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
            // lblDiaCorte
            // 
            this.lblDiaCorte.AutoSize = true;
            this.lblDiaCorte.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDiaCorte.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblDiaCorte.Location = new System.Drawing.Point(26, 213);
            this.lblDiaCorte.Name = "lblDiaCorte";
            this.lblDiaCorte.Size = new System.Drawing.Size(123, 25);
            this.lblDiaCorte.TabIndex = 5;
            this.lblDiaCorte.Text = "Día del corte:";
            // 
            // lblFechaCorte
            // 
            this.lblFechaCorte.AutoSize = true;
            this.lblFechaCorte.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFechaCorte.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblFechaCorte.Location = new System.Drawing.Point(26, 260);
            this.lblFechaCorte.Name = "lblFechaCorte";
            this.lblFechaCorte.Size = new System.Drawing.Size(55, 25);
            this.lblFechaCorte.TabIndex = 6;
            this.lblFechaCorte.Text = "fecha";
            // 
            // IniciarPagos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(464, 330);
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
    }
}