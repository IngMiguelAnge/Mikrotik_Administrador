namespace Mikrotik_Administrador.Catalogos
{
    partial class HistorialPagos
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
            this.dgvHistorialPagos = new System.Windows.Forms.DataGridView();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.CBBanco = new System.Windows.Forms.ComboBox();
            this.lblBanco = new System.Windows.Forms.Label();
            this.NUDTicket = new System.Windows.Forms.NumericUpDown();
            this.lblNoTicket = new System.Windows.Forms.Label();
            this.txtReferencia = new System.Windows.Forms.TextBox();
            this.lblReferencia = new System.Windows.Forms.Label();
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.btnNuevo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialPagos)).BeginInit();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUDTicket)).BeginInit();
            this.panelContenedor.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvHistorialPagos
            // 
            this.dgvHistorialPagos.AllowUserToAddRows = false;
            this.dgvHistorialPagos.BackgroundColor = System.Drawing.Color.White;
            this.dgvHistorialPagos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHistorialPagos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvHistorialPagos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHistorialPagos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvHistorialPagos.ColumnHeadersHeight = 35;
            this.dgvHistorialPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(242)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHistorialPagos.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvHistorialPagos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistorialPagos.EnableHeadersVisualStyles = false;
            this.dgvHistorialPagos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvHistorialPagos.Location = new System.Drawing.Point(20, 20);
            this.dgvHistorialPagos.Name = "dgvHistorialPagos";
            this.dgvHistorialPagos.RowHeadersVisible = false;
            this.dgvHistorialPagos.RowHeadersWidth = 51;
            this.dgvHistorialPagos.RowTemplate.Height = 30;
            this.dgvHistorialPagos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistorialPagos.Size = new System.Drawing.Size(931, 321);
            this.dgvHistorialPagos.TabIndex = 0;
            this.dgvHistorialPagos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHistorialPagos_CellContentClick);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(42)))), ((int)(((byte)(107)))));
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(697, 33);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(110, 38);
            this.btnBuscar.TabIndex = 4;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.White;
            this.panelTop.Controls.Add(this.btnNuevo);
            this.panelTop.Controls.Add(this.CBBanco);
            this.panelTop.Controls.Add(this.lblBanco);
            this.panelTop.Controls.Add(this.NUDTicket);
            this.panelTop.Controls.Add(this.lblNoTicket);
            this.panelTop.Controls.Add(this.txtReferencia);
            this.panelTop.Controls.Add(this.lblReferencia);
            this.panelTop.Controls.Add(this.btnBuscar);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(971, 89);
            this.panelTop.TabIndex = 1;
            // 
            // CBBanco
            // 
            this.CBBanco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBBanco.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.CBBanco.FormattingEnabled = true;
            this.CBBanco.Location = new System.Drawing.Point(475, 38);
            this.CBBanco.Name = "CBBanco";
            this.CBBanco.Size = new System.Drawing.Size(190, 33);
            this.CBBanco.TabIndex = 3;
            // 
            // lblBanco
            // 
            this.lblBanco.AutoSize = true;
            this.lblBanco.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblBanco.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblBanco.Location = new System.Drawing.Point(471, 9);
            this.lblBanco.Name = "lblBanco";
            this.lblBanco.Size = new System.Drawing.Size(66, 25);
            this.lblBanco.TabIndex = 7;
            this.lblBanco.Text = "Banco:";
            // 
            // NUDTicket
            // 
            this.NUDTicket.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.NUDTicket.Location = new System.Drawing.Point(255, 38);
            this.NUDTicket.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.NUDTicket.Name = "NUDTicket";
            this.NUDTicket.Size = new System.Drawing.Size(195, 34);
            this.NUDTicket.TabIndex = 2;
            // 
            // lblNoTicket
            // 
            this.lblNoTicket.AutoSize = true;
            this.lblNoTicket.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNoTicket.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblNoTicket.Location = new System.Drawing.Point(251, 9);
            this.lblNoTicket.Name = "lblNoTicket";
            this.lblNoTicket.Size = new System.Drawing.Size(95, 25);
            this.lblNoTicket.TabIndex = 6;
            this.lblNoTicket.Text = "No Ticket:";
            // 
            // txtReferencia
            // 
            this.txtReferencia.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtReferencia.Location = new System.Drawing.Point(30, 38);
            this.txtReferencia.MaxLength = 250;
            this.txtReferencia.Name = "txtReferencia";
            this.txtReferencia.Size = new System.Drawing.Size(200, 34);
            this.txtReferencia.TabIndex = 1;
            // 
            // lblReferencia
            // 
            this.lblReferencia.AutoSize = true;
            this.lblReferencia.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblReferencia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblReferencia.Location = new System.Drawing.Point(26, 9);
            this.lblReferencia.Name = "lblReferencia";
            this.lblReferencia.Size = new System.Drawing.Size(103, 25);
            this.lblReferencia.TabIndex = 5;
            this.lblReferencia.Text = "Referencia:";
            // 
            // panelContenedor
            // 
            this.panelContenedor.Controls.Add(this.dgvHistorialPagos);
            this.panelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedor.Location = new System.Drawing.Point(0, 89);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Padding = new System.Windows.Forms.Padding(20);
            this.panelContenedor.Size = new System.Drawing.Size(971, 361);
            this.panelContenedor.TabIndex = 2;
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Location = new System.Drawing.Point(826, 33);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(100, 36);
            this.btnNuevo.TabIndex = 14;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // HistorialPagos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(971, 450);
            this.Controls.Add(this.panelContenedor);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.MaximizeBox = false;
            this.Name = "HistorialPagos";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.HistorialPagos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialPagos)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUDTicket)).EndInit();
            this.panelContenedor.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.DataGridView dgvHistorialPagos;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.ComboBox CBBanco;
        private System.Windows.Forms.Label lblBanco;
        private System.Windows.Forms.NumericUpDown NUDTicket;
        private System.Windows.Forms.Label lblNoTicket;
        private System.Windows.Forms.TextBox txtReferencia;
        private System.Windows.Forms.Label lblReferencia;
        private System.Windows.Forms.Button btnNuevo;
    }
}