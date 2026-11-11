namespace Mikrotik_Administrador.Catalogos
{
    partial class Deudas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnPagar = new System.Windows.Forms.Button();
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.btnExtras = new System.Windows.Forms.Button();
            this.DGVDeudas = new System.Windows.Forms.DataGridView();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblPrestamoExtra = new System.Windows.Forms.Label();
            this.lblVentaExtra = new System.Windows.Forms.Label();
            this.lblServicioExtra = new System.Windows.Forms.Label();
            this.panelContenedor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVDeudas)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Location = new System.Drawing.Point(33, 31);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(100, 46);
            this.btnNuevo.TabIndex = 0;
            this.btnNuevo.Text = "+ Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnPagar
            // 
            this.btnPagar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnPagar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPagar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnPagar.ForeColor = System.Drawing.Color.White;
            this.btnPagar.Location = new System.Drawing.Point(183, 31);
            this.btnPagar.Name = "btnPagar";
            this.btnPagar.Size = new System.Drawing.Size(100, 46);
            this.btnPagar.TabIndex = 5;
            this.btnPagar.Text = "Pagar";
            this.btnPagar.UseVisualStyleBackColor = false;
            this.btnPagar.Click += new System.EventHandler(this.btnPagar_Click);
            // 
            // panelContenedor
            // 
            this.panelContenedor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContenedor.Controls.Add(this.DGVDeudas);
            this.panelContenedor.Location = new System.Drawing.Point(21, 174);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Padding = new System.Windows.Forms.Padding(20);
            this.panelContenedor.Size = new System.Drawing.Size(755, 264);
            this.panelContenedor.TabIndex = 6;
            // 
            // btnExtras
            // 
            this.btnExtras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(42)))), ((int)(((byte)(107)))));
            this.btnExtras.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExtras.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnExtras.ForeColor = System.Drawing.Color.White;
            this.btnExtras.Location = new System.Drawing.Point(329, 31);
            this.btnExtras.Name = "btnExtras";
            this.btnExtras.Size = new System.Drawing.Size(113, 46);
            this.btnExtras.TabIndex = 20;
            this.btnExtras.Text = "Extras";
            this.btnExtras.UseVisualStyleBackColor = false;
            this.btnExtras.Click += new System.EventHandler(this.btnExtras_Click);
            // 
            // DGVDeudas
            // 
            this.DGVDeudas.AllowUserToAddRows = false;
            this.DGVDeudas.BackgroundColor = System.Drawing.Color.White;
            this.DGVDeudas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DGVDeudas.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.DGVDeudas.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVDeudas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DGVDeudas.ColumnHeadersHeight = 35;
            this.DGVDeudas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(242)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGVDeudas.DefaultCellStyle = dataGridViewCellStyle4;
            this.DGVDeudas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGVDeudas.EnableHeadersVisualStyles = false;
            this.DGVDeudas.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DGVDeudas.Location = new System.Drawing.Point(20, 20);
            this.DGVDeudas.Name = "DGVDeudas";
            this.DGVDeudas.RowHeadersVisible = false;
            this.DGVDeudas.RowHeadersWidth = 51;
            this.DGVDeudas.RowTemplate.Height = 30;
            this.DGVDeudas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVDeudas.Size = new System.Drawing.Size(715, 224);
            this.DGVDeudas.TabIndex = 9;
            this.DGVDeudas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVDeudas_CellContentClick);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(21, 83);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(235, 12);
            this.progressBar1.TabIndex = 7;
            // 
            // lblPrestamoExtra
            // 
            this.lblPrestamoExtra.AutoSize = true;
            this.lblPrestamoExtra.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrestamoExtra.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblPrestamoExtra.Location = new System.Drawing.Point(419, 98);
            this.lblPrestamoExtra.Name = "lblPrestamoExtra";
            this.lblPrestamoExtra.Size = new System.Drawing.Size(106, 25);
            this.lblPrestamoExtra.TabIndex = 19;
            this.lblPrestamoExtra.Text = "Costoextra:";
            // 
            // lblVentaExtra
            // 
            this.lblVentaExtra.AutoSize = true;
            this.lblVentaExtra.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblVentaExtra.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblVentaExtra.Location = new System.Drawing.Point(28, 146);
            this.lblVentaExtra.Name = "lblVentaExtra";
            this.lblVentaExtra.Size = new System.Drawing.Size(106, 25);
            this.lblVentaExtra.TabIndex = 18;
            this.lblVentaExtra.Text = "Costoextra:";
            // 
            // lblServicioExtra
            // 
            this.lblServicioExtra.AutoSize = true;
            this.lblServicioExtra.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblServicioExtra.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblServicioExtra.Location = new System.Drawing.Point(28, 98);
            this.lblServicioExtra.Name = "lblServicioExtra";
            this.lblServicioExtra.Size = new System.Drawing.Size(106, 25);
            this.lblServicioExtra.TabIndex = 17;
            this.lblServicioExtra.Text = "Costoextra:";
            // 
            // Deudas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnExtras);
            this.Controls.Add(this.lblPrestamoExtra);
            this.Controls.Add(this.lblVentaExtra);
            this.Controls.Add(this.lblServicioExtra);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.panelContenedor);
            this.Controls.Add(this.btnPagar);
            this.Controls.Add(this.btnNuevo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Deudas";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Deudas_Load);
            this.panelContenedor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGVDeudas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnPagar;
        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.DataGridView DGVDeudas;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblPrestamoExtra;
        private System.Windows.Forms.Label lblVentaExtra;
        private System.Windows.Forms.Label lblServicioExtra;
        private System.Windows.Forms.Button btnExtras;
    }
}