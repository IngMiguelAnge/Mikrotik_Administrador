namespace Mikrotik_Administrador.Catalogos
{
    partial class Mensualidades
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvMensualidades = new System.Windows.Forms.DataGridView();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnVerProrroga = new System.Windows.Forms.Button();
            this.lblFechaProrroga = new System.Windows.Forms.Label();
            this.lblProrroga = new System.Windows.Forms.Label();
            this.btnPagar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.lblCostoExtra = new System.Windows.Forms.Label();
            this.btnExtras = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMensualidades)).BeginInit();
            this.panelTop.SuspendLayout();
            this.panelContenedor.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvMensualidades
            // 
            this.dgvMensualidades.AllowUserToAddRows = false;
            this.dgvMensualidades.BackgroundColor = System.Drawing.Color.White;
            this.dgvMensualidades.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMensualidades.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvMensualidades.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMensualidades.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvMensualidades.ColumnHeadersHeight = 35;
            this.dgvMensualidades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(242)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMensualidades.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvMensualidades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMensualidades.EnableHeadersVisualStyles = false;
            this.dgvMensualidades.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvMensualidades.Location = new System.Drawing.Point(20, 20);
            this.dgvMensualidades.Name = "dgvMensualidades";
            this.dgvMensualidades.RowHeadersVisible = false;
            this.dgvMensualidades.RowHeadersWidth = 51;
            this.dgvMensualidades.RowTemplate.Height = 30;
            this.dgvMensualidades.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMensualidades.Size = new System.Drawing.Size(989, 280);
            this.dgvMensualidades.TabIndex = 0;
            this.dgvMensualidades.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMensualidades_CellContentClick);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(42)))), ((int)(((byte)(107)))));
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(20, 33);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(100, 38);
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.White;
            this.panelTop.Controls.Add(this.btnExtras);
            this.panelTop.Controls.Add(this.lblCostoExtra);
            this.panelTop.Controls.Add(this.btnVerProrroga);
            this.panelTop.Controls.Add(this.lblFechaProrroga);
            this.panelTop.Controls.Add(this.lblProrroga);
            this.panelTop.Controls.Add(this.btnPagar);
            this.panelTop.Controls.Add(this.btnNuevo);
            this.panelTop.Controls.Add(this.btnBuscar);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1029, 109);
            this.panelTop.TabIndex = 1;
            // 
            // btnVerProrroga
            // 
            this.btnVerProrroga.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(42)))), ((int)(((byte)(107)))));
            this.btnVerProrroga.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerProrroga.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnVerProrroga.ForeColor = System.Drawing.Color.White;
            this.btnVerProrroga.Location = new System.Drawing.Point(723, 33);
            this.btnVerProrroga.Name = "btnVerProrroga";
            this.btnVerProrroga.Size = new System.Drawing.Size(145, 38);
            this.btnVerProrroga.TabIndex = 12;
            this.btnVerProrroga.Text = "Ver prorrogas";
            this.btnVerProrroga.UseVisualStyleBackColor = false;
            this.btnVerProrroga.Click += new System.EventHandler(this.btnVerProrroga_Click);
            // 
            // lblFechaProrroga
            // 
            this.lblFechaProrroga.AutoSize = true;
            this.lblFechaProrroga.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFechaProrroga.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblFechaProrroga.Location = new System.Drawing.Point(478, 45);
            this.lblFechaProrroga.Name = "lblFechaProrroga";
            this.lblFechaProrroga.Size = new System.Drawing.Size(190, 25);
            this.lblFechaProrroga.TabIndex = 11;
            this.lblFechaProrroga.Text = "Prorroga activa hasta:";
            // 
            // lblProrroga
            // 
            this.lblProrroga.AutoSize = true;
            this.lblProrroga.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblProrroga.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblProrroga.Location = new System.Drawing.Point(478, 20);
            this.lblProrroga.Name = "lblProrroga";
            this.lblProrroga.Size = new System.Drawing.Size(190, 25);
            this.lblProrroga.TabIndex = 10;
            this.lblProrroga.Text = "Prorroga activa hasta:";
            // 
            // btnPagar
            // 
            this.btnPagar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnPagar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPagar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnPagar.ForeColor = System.Drawing.Color.White;
            this.btnPagar.Location = new System.Drawing.Point(322, 33);
            this.btnPagar.Name = "btnPagar";
            this.btnPagar.Size = new System.Drawing.Size(120, 38);
            this.btnPagar.TabIndex = 8;
            this.btnPagar.Text = "Pagar";
            this.btnPagar.UseVisualStyleBackColor = false;
            this.btnPagar.Click += new System.EventHandler(this.btnPagar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Location = new System.Drawing.Point(148, 33);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(120, 38);
            this.btnNuevo.TabIndex = 7;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // panelContenedor
            // 
            this.panelContenedor.Controls.Add(this.dgvMensualidades);
            this.panelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedor.Location = new System.Drawing.Point(0, 109);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Padding = new System.Windows.Forms.Padding(20);
            this.panelContenedor.Size = new System.Drawing.Size(1029, 320);
            this.panelContenedor.TabIndex = 2;
            // 
            // lblCostoExtra
            // 
            this.lblCostoExtra.AutoSize = true;
            this.lblCostoExtra.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblCostoExtra.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblCostoExtra.Location = new System.Drawing.Point(478, 74);
            this.lblCostoExtra.Name = "lblCostoExtra";
            this.lblCostoExtra.Size = new System.Drawing.Size(190, 25);
            this.lblCostoExtra.TabIndex = 13;
            this.lblCostoExtra.Text = "Prorroga activa hasta:";
            // 
            // btnExtras
            // 
            this.btnExtras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(42)))), ((int)(((byte)(107)))));
            this.btnExtras.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExtras.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnExtras.ForeColor = System.Drawing.Color.White;
            this.btnExtras.Location = new System.Drawing.Point(886, 33);
            this.btnExtras.Name = "btnExtras";
            this.btnExtras.Size = new System.Drawing.Size(113, 38);
            this.btnExtras.TabIndex = 14;
            this.btnExtras.Text = "Extras";
            this.btnExtras.UseVisualStyleBackColor = false;
            this.btnExtras.Click += new System.EventHandler(this.btnExtras_Click);
            // 
            // Mensualidades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1029, 429);
            this.Controls.Add(this.panelContenedor);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.MaximizeBox = false;
            this.Name = "Mensualidades";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Mensualidades_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMensualidades)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelContenedor.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMensualidades;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnPagar;
        private System.Windows.Forms.Label lblProrroga;
        private System.Windows.Forms.Button btnVerProrroga;
        private System.Windows.Forms.Label lblFechaProrroga;
        private System.Windows.Forms.Button btnExtras;
        private System.Windows.Forms.Label lblCostoExtra;
    }
}