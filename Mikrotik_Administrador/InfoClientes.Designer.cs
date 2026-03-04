namespace Mikrotik_Administrador
{
    partial class InfoClientes
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
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.lblMensaje3 = new System.Windows.Forms.Label();
            this.BtnBuscar = new System.Windows.Forms.Button();
            this.CBTodosMikrotiks = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.CBMikrotiks = new System.Windows.Forms.ComboBox();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DGVClientes = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mikrotiksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.migracionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AsignacionClientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVClientes)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(22, 171);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(261, 26);
            this.txtCliente.TabIndex = 32;
            // 
            // lblMensaje3
            // 
            this.lblMensaje3.AutoSize = true;
            this.lblMensaje3.Location = new System.Drawing.Point(20, 148);
            this.lblMensaje3.Name = "lblMensaje3";
            this.lblMensaje3.Size = new System.Drawing.Size(211, 20);
            this.lblMensaje3.TabIndex = 31;
            this.lblMensaje3.Text = "Escriba el nombre del cliente";
            // 
            // BtnBuscar
            // 
            this.BtnBuscar.Location = new System.Drawing.Point(333, 165);
            this.BtnBuscar.Name = "BtnBuscar";
            this.BtnBuscar.Size = new System.Drawing.Size(89, 39);
            this.BtnBuscar.TabIndex = 30;
            this.BtnBuscar.Text = "Buscar";
            this.BtnBuscar.UseVisualStyleBackColor = true;
            this.BtnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);
            // 
            // CBTodosMikrotiks
            // 
            this.CBTodosMikrotiks.AutoSize = true;
            this.CBTodosMikrotiks.Location = new System.Drawing.Point(367, 81);
            this.CBTodosMikrotiks.Name = "CBTodosMikrotiks";
            this.CBTodosMikrotiks.Size = new System.Drawing.Size(259, 24);
            this.CBTodosMikrotiks.TabIndex = 25;
            this.CBTodosMikrotiks.Text = "¿Buscar en todos los mikrotiks?";
            this.CBTodosMikrotiks.UseVisualStyleBackColor = true;
            this.CBTodosMikrotiks.CheckedChanged += new System.EventHandler(this.CBTodosMikrotiks_CheckedChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(631, 81);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(133, 23);
            this.progressBar1.TabIndex = 27;
            // 
            // CBMikrotiks
            // 
            this.CBMikrotiks.FormattingEnabled = true;
            this.CBMikrotiks.Location = new System.Drawing.Point(153, 84);
            this.CBMikrotiks.Name = "CBMikrotiks";
            this.CBMikrotiks.Size = new System.Drawing.Size(191, 28);
            this.CBMikrotiks.TabIndex = 24;
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.Location = new System.Drawing.Point(18, 84);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(105, 20);
            this.lblMensaje.TabIndex = 26;
            this.lblMensaje.Text = "Mikrotik a ver:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.DGVClientes);
            this.groupBox1.Location = new System.Drawing.Point(12, 210);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(770, 228);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            // 
            // DGVClientes
            // 
            this.DGVClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVClientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGVClientes.Location = new System.Drawing.Point(3, 22);
            this.DGVClientes.Name = "DGVClientes";
            this.DGVClientes.RowHeadersWidth = 62;
            this.DGVClientes.RowTemplate.Height = 28;
            this.DGVClientes.Size = new System.Drawing.Size(764, 203);
            this.DGVClientes.TabIndex = 34;
            this.DGVClientes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVClientes_CellContentClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mikrotiksToolStripMenuItem,
            this.migracionToolStripMenuItem,
            this.clientesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 33);
            this.menuStrip1.TabIndex = 34;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mikrotiksToolStripMenuItem
            // 
            this.mikrotiksToolStripMenuItem.Name = "mikrotiksToolStripMenuItem";
            this.mikrotiksToolStripMenuItem.Size = new System.Drawing.Size(101, 29);
            this.mikrotiksToolStripMenuItem.Text = "Mikrotiks";
            this.mikrotiksToolStripMenuItem.Click += new System.EventHandler(this.mikrotiksToolStripMenuItem_Click);
            // 
            // migracionToolStripMenuItem
            // 
            this.migracionToolStripMenuItem.Name = "migracionToolStripMenuItem";
            this.migracionToolStripMenuItem.Size = new System.Drawing.Size(107, 29);
            this.migracionToolStripMenuItem.Text = "Migración";
            this.migracionToolStripMenuItem.Click += new System.EventHandler(this.migracionToolStripMenuItem_Click);
            // 
            // clientesToolStripMenuItem
            // 
            this.clientesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AsignacionClientesToolStripMenuItem});
            this.clientesToolStripMenuItem.Name = "clientesToolStripMenuItem";
            this.clientesToolStripMenuItem.Size = new System.Drawing.Size(89, 29);
            this.clientesToolStripMenuItem.Text = "Clientes";
            // 
            // AsignacionClientesToolStripMenuItem
            // 
            this.AsignacionClientesToolStripMenuItem.Name = "AsignacionClientesToolStripMenuItem";
            this.AsignacionClientesToolStripMenuItem.Size = new System.Drawing.Size(289, 34);
            this.AsignacionClientesToolStripMenuItem.Text = "Asignación de clientes";
            this.AsignacionClientesToolStripMenuItem.Click += new System.EventHandler(this.AsignacionClientesToolStripMenuItem_Click);
            // 
            // InfoClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtCliente);
            this.Controls.Add(this.lblMensaje3);
            this.Controls.Add(this.BtnBuscar);
            this.Controls.Add(this.CBTodosMikrotiks);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.CBMikrotiks);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "InfoClientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InfoClientes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InfoClientes_FormClosed);
            this.Load += new System.EventHandler(this.InfoClientes_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGVClientes)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label lblMensaje3;
        private System.Windows.Forms.Button BtnBuscar;
        private System.Windows.Forms.CheckBox CBTodosMikrotiks;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ComboBox CBMikrotiks;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView DGVClientes;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mikrotiksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem migracionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AsignacionClientesToolStripMenuItem;
    }
}