namespace Mikrotik_Administrador.Catalogos
{
    partial class Pagos
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
            this.lblCliente = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblPlan = new System.Windows.Forms.Label();
            this.lblMikrotik = new System.Windows.Forms.Label();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.CBPlan = new System.Windows.Forms.ComboBox();
            this.CBMikrotik = new System.Windows.Forms.ComboBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.GBClientes = new System.Windows.Forms.GroupBox();
            this.dgvClientes = new System.Windows.Forms.DataGridView();
            this.CBTipo = new System.Windows.Forms.ComboBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.GBClientes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Location = new System.Drawing.Point(34, 32);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(62, 20);
            this.lblCliente.TabIndex = 0;
            this.lblCliente.Text = "Cliente:";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(274, 32);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(68, 20);
            this.lblUsuario.TabIndex = 1;
            this.lblUsuario.Text = "Usuario:";
            // 
            // lblPlan
            // 
            this.lblPlan.AutoSize = true;
            this.lblPlan.Location = new System.Drawing.Point(710, 34);
            this.lblPlan.Name = "lblPlan";
            this.lblPlan.Size = new System.Drawing.Size(44, 20);
            this.lblPlan.TabIndex = 2;
            this.lblPlan.Text = "Plan:";
            // 
            // lblMikrotik
            // 
            this.lblMikrotik.AutoSize = true;
            this.lblMikrotik.Location = new System.Drawing.Point(942, 34);
            this.lblMikrotik.Name = "lblMikrotik";
            this.lblMikrotik.Size = new System.Drawing.Size(67, 20);
            this.lblMikrotik.TabIndex = 3;
            this.lblMikrotik.Text = "Mikrotik:";
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(38, 64);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(216, 26);
            this.txtCliente.TabIndex = 4;
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(278, 64);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(204, 26);
            this.txtUsuario.TabIndex = 5;
            // 
            // CBPlan
            // 
            this.CBPlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBPlan.FormattingEnabled = true;
            this.CBPlan.Location = new System.Drawing.Point(714, 64);
            this.CBPlan.Name = "CBPlan";
            this.CBPlan.Size = new System.Drawing.Size(184, 28);
            this.CBPlan.TabIndex = 6;
            // 
            // CBMikrotik
            // 
            this.CBMikrotik.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBMikrotik.FormattingEnabled = true;
            this.CBMikrotik.Location = new System.Drawing.Point(946, 64);
            this.CBMikrotik.Name = "CBMikrotik";
            this.CBMikrotik.Size = new System.Drawing.Size(184, 28);
            this.CBMikrotik.TabIndex = 7;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(440, 125);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(132, 34);
            this.btnBuscar.TabIndex = 8;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // GBClientes
            // 
            this.GBClientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBClientes.Controls.Add(this.dgvClientes);
            this.GBClientes.Location = new System.Drawing.Point(38, 165);
            this.GBClientes.Name = "GBClientes";
            this.GBClientes.Size = new System.Drawing.Size(1095, 263);
            this.GBClientes.TabIndex = 9;
            this.GBClientes.TabStop = false;
            this.GBClientes.Text = "Clientes";
            // 
            // dgvClientes
            // 
            this.dgvClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvClientes.Location = new System.Drawing.Point(3, 22);
            this.dgvClientes.Name = "dgvClientes";
            this.dgvClientes.RowHeadersWidth = 62;
            this.dgvClientes.RowTemplate.Height = 28;
            this.dgvClientes.Size = new System.Drawing.Size(1089, 238);
            this.dgvClientes.TabIndex = 0;
            this.dgvClientes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvClientes_CellContentClick);
            // 
            // CBTipo
            // 
            this.CBTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBTipo.FormattingEnabled = true;
            this.CBTipo.Items.AddRange(new object[] {
            "Seleccione",
            "Fibra",
            "Antena"});
            this.CBTipo.Location = new System.Drawing.Point(513, 64);
            this.CBTipo.Name = "CBTipo";
            this.CBTipo.Size = new System.Drawing.Size(184, 28);
            this.CBTipo.TabIndex = 11;
            this.CBTipo.SelectedIndexChanged += new System.EventHandler(this.CBTipo_SelectedIndexChanged);
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(509, 34);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(43, 20);
            this.lblTipo.TabIndex = 10;
            this.lblTipo.Text = "Tipo:";
            // 
            // Pagos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 450);
            this.Controls.Add(this.CBTipo);
            this.Controls.Add(this.lblTipo);
            this.Controls.Add(this.GBClientes);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.CBMikrotik);
            this.Controls.Add(this.CBPlan);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.txtCliente);
            this.Controls.Add(this.lblMikrotik);
            this.Controls.Add(this.lblPlan);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.lblCliente);
            this.MaximizeBox = false;
            this.Name = "Pagos";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Pagos_Load);
            this.GBClientes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblPlan;
        private System.Windows.Forms.Label lblMikrotik;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.ComboBox CBPlan;
        private System.Windows.Forms.ComboBox CBMikrotik;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.GroupBox GBClientes;
        private System.Windows.Forms.DataGridView dgvClientes;
        private System.Windows.Forms.ComboBox CBTipo;
        private System.Windows.Forms.Label lblTipo;
    }
}