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
            this.lblReferencia = new System.Windows.Forms.Label();
            this.lblNoTicket = new System.Windows.Forms.Label();
            this.txtReferencia = new System.Windows.Forms.TextBox();
            this.NUDTicket = new System.Windows.Forms.NumericUpDown();
            this.lblBanco = new System.Windows.Forms.Label();
            this.CBBanco = new System.Windows.Forms.ComboBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.GBTickets = new System.Windows.Forms.GroupBox();
            this.dgvPagos = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.NUDTicket)).BeginInit();
            this.GBTickets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagos)).BeginInit();
            this.SuspendLayout();
            // 
            // lblReferencia
            // 
            this.lblReferencia.AutoSize = true;
            this.lblReferencia.Location = new System.Drawing.Point(38, 27);
            this.lblReferencia.Name = "lblReferencia";
            this.lblReferencia.Size = new System.Drawing.Size(91, 20);
            this.lblReferencia.TabIndex = 0;
            this.lblReferencia.Text = "Referencia:";
            // 
            // lblNoTicket
            // 
            this.lblNoTicket.AutoSize = true;
            this.lblNoTicket.Location = new System.Drawing.Point(300, 27);
            this.lblNoTicket.Name = "lblNoTicket";
            this.lblNoTicket.Size = new System.Drawing.Size(79, 20);
            this.lblNoTicket.TabIndex = 2;
            this.lblNoTicket.Text = "No Ticket:";
            // 
            // txtReferencia
            // 
            this.txtReferencia.Location = new System.Drawing.Point(42, 72);
            this.txtReferencia.MaxLength = 250;
            this.txtReferencia.Name = "txtReferencia";
            this.txtReferencia.Size = new System.Drawing.Size(197, 26);
            this.txtReferencia.TabIndex = 3;
            // 
            // NUDTicket
            // 
            this.NUDTicket.Location = new System.Drawing.Point(304, 72);
            this.NUDTicket.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.NUDTicket.Name = "NUDTicket";
            this.NUDTicket.Size = new System.Drawing.Size(120, 26);
            this.NUDTicket.TabIndex = 4;
            // 
            // lblBanco
            // 
            this.lblBanco.AutoSize = true;
            this.lblBanco.Location = new System.Drawing.Point(474, 27);
            this.lblBanco.Name = "lblBanco";
            this.lblBanco.Size = new System.Drawing.Size(59, 20);
            this.lblBanco.TabIndex = 5;
            this.lblBanco.Text = "Banco:";
            // 
            // CBBanco
            // 
            this.CBBanco.FormattingEnabled = true;
            this.CBBanco.Location = new System.Drawing.Point(478, 70);
            this.CBBanco.Name = "CBBanco";
            this.CBBanco.Size = new System.Drawing.Size(187, 28);
            this.CBBanco.TabIndex = 6;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(707, 70);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(128, 28);
            this.btnBuscar.TabIndex = 7;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // GBTickets
            // 
            this.GBTickets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBTickets.Controls.Add(this.dgvPagos);
            this.GBTickets.Location = new System.Drawing.Point(42, 132);
            this.GBTickets.Name = "GBTickets";
            this.GBTickets.Size = new System.Drawing.Size(844, 276);
            this.GBTickets.TabIndex = 8;
            this.GBTickets.TabStop = false;
            this.GBTickets.Text = "Pagos";
            // 
            // dgvPagos
            // 
            this.dgvPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPagos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPagos.Location = new System.Drawing.Point(3, 22);
            this.dgvPagos.Name = "dgvPagos";
            this.dgvPagos.RowHeadersWidth = 62;
            this.dgvPagos.RowTemplate.Height = 28;
            this.dgvPagos.Size = new System.Drawing.Size(838, 251);
            this.dgvPagos.TabIndex = 0;
            this.dgvPagos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPagos_CellContentClick);
            // 
            // HistorialPagos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 450);
            this.Controls.Add(this.GBTickets);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.CBBanco);
            this.Controls.Add(this.lblBanco);
            this.Controls.Add(this.NUDTicket);
            this.Controls.Add(this.txtReferencia);
            this.Controls.Add(this.lblNoTicket);
            this.Controls.Add(this.lblReferencia);
            this.MaximizeBox = false;
            this.Name = "HistorialPagos";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.HistorialPagos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NUDTicket)).EndInit();
            this.GBTickets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblReferencia;
        private System.Windows.Forms.Label lblNoTicket;
        private System.Windows.Forms.TextBox txtReferencia;
        private System.Windows.Forms.NumericUpDown NUDTicket;
        private System.Windows.Forms.Label lblBanco;
        private System.Windows.Forms.ComboBox CBBanco;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.GroupBox GBTickets;
        private System.Windows.Forms.DataGridView dgvPagos;
    }
}