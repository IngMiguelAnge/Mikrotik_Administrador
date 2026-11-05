namespace Mikrotik_Administrador.Catalogos
{
    partial class Deuda
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
            this.NUDCantidad = new System.Windows.Forms.NumericUpDown();
            this.lblDeuda = new System.Windows.Forms.Label();
            this.NUDYear = new System.Windows.Forms.NumericUpDown();
            this.lblYear = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CBMes = new System.Windows.Forms.ComboBox();
            this.BtnGuardar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NUDCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDYear)).BeginInit();
            this.SuspendLayout();
            // 
            // NUDCantidad
            // 
            this.NUDCantidad.DecimalPlaces = 2;
            this.NUDCantidad.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.NUDCantidad.Location = new System.Drawing.Point(30, 53);
            this.NUDCantidad.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.NUDCantidad.Name = "NUDCantidad";
            this.NUDCantidad.Size = new System.Drawing.Size(140, 33);
            this.NUDCantidad.TabIndex = 1;
            // 
            // lblDeuda
            // 
            this.lblDeuda.AutoSize = true;
            this.lblDeuda.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDeuda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblDeuda.Location = new System.Drawing.Point(25, 23);
            this.lblDeuda.Name = "lblDeuda";
            this.lblDeuda.Size = new System.Drawing.Size(70, 25);
            this.lblDeuda.TabIndex = 0;
            this.lblDeuda.Text = "Deuda:";
            // 
            // NUDYear
            // 
            this.NUDYear.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.NUDYear.Location = new System.Drawing.Point(30, 132);
            this.NUDYear.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.NUDYear.Minimum = new decimal(new int[] {
            2023,
            0,
            0,
            0});
            this.NUDYear.Name = "NUDYear";
            this.NUDYear.Size = new System.Drawing.Size(140, 33);
            this.NUDYear.TabIndex = 3;
            this.NUDYear.Value = new decimal(new int[] {
            2023,
            0,
            0,
            0});
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblYear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblYear.Location = new System.Drawing.Point(25, 104);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(50, 25);
            this.lblYear.TabIndex = 2;
            this.lblYear.Text = "Año:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.label2.Location = new System.Drawing.Point(189, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Mes:";
            this.label2.Visible = false;
            // 
            // CBMes
            // 
            this.CBMes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBMes.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.CBMes.FormattingEnabled = true;
            this.CBMes.Items.AddRange(new object[] {
            "Seleccione",
            "Enero",
            "Febrero",
            "Marzo",
            "Abril",
            "Mayo",
            "Junio",
            "Julio",
            "Agosto",
            "Septiembre",
            "Octubre",
            "Noviembre",
            "Diciembre"});
            this.CBMes.Location = new System.Drawing.Point(194, 131);
            this.CBMes.Name = "CBMes";
            this.CBMes.Size = new System.Drawing.Size(206, 33);
            this.CBMes.TabIndex = 5;
            // 
            // BtnGuardar
            // 
            this.BtnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.BtnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnGuardar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.BtnGuardar.ForeColor = System.Drawing.Color.White;
            this.BtnGuardar.Location = new System.Drawing.Point(290, 206);
            this.BtnGuardar.Name = "BtnGuardar";
            this.BtnGuardar.Size = new System.Drawing.Size(110, 42);
            this.BtnGuardar.TabIndex = 6;
            this.BtnGuardar.Text = "Guardar";
            this.BtnGuardar.UseVisualStyleBackColor = false;
            this.BtnGuardar.Click += new System.EventHandler(this.BtnGuardar_Click);
            // 
            // Deuda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(446, 260);
            this.Controls.Add(this.BtnGuardar);
            this.Controls.Add(this.CBMes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NUDYear);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.NUDCantidad);
            this.Controls.Add(this.lblDeuda);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MaximizeBox = false;
            this.Name = "Deuda";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Deuda_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NUDCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDYear)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown NUDCantidad;
        private System.Windows.Forms.Label lblDeuda;
        private System.Windows.Forms.NumericUpDown NUDYear;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CBMes;
        private System.Windows.Forms.Button BtnGuardar;
    }
}