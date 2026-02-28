namespace Mikrotik_Administrador
{
    partial class Clientes
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
            this.CBTodosMikrotiks = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.CBMikrotiks = new System.Windows.Forms.ComboBox();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.BtnBuscar = new System.Windows.Forms.Button();
            this.lblMensaje2 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mikrotiksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.migracionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblMensaje3 = new System.Windows.Forms.Label();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.CBAsignar = new System.Windows.Forms.CheckBox();
            this.BtnAsignar = new System.Windows.Forms.Button();
            this.lblMensaje4 = new System.Windows.Forms.Label();
            this.btnClientesSin = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CBTodosMikrotiks
            // 
            this.CBTodosMikrotiks.AutoSize = true;
            this.CBTodosMikrotiks.Location = new System.Drawing.Point(359, 43);
            this.CBTodosMikrotiks.Name = "CBTodosMikrotiks";
            this.CBTodosMikrotiks.Size = new System.Drawing.Size(259, 24);
            this.CBTodosMikrotiks.TabIndex = 8;
            this.CBTodosMikrotiks.Text = "¿Buscar en todos los mikrotiks?";
            this.CBTodosMikrotiks.UseVisualStyleBackColor = true;
            this.CBTodosMikrotiks.CheckedChanged += new System.EventHandler(this.CBTodosMikrotiks_CheckedChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(623, 43);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(133, 23);
            this.progressBar1.TabIndex = 10;
            // 
            // CBMikrotiks
            // 
            this.CBMikrotiks.FormattingEnabled = true;
            this.CBMikrotiks.Location = new System.Drawing.Point(145, 46);
            this.CBMikrotiks.Name = "CBMikrotiks";
            this.CBMikrotiks.Size = new System.Drawing.Size(191, 28);
            this.CBMikrotiks.TabIndex = 7;
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.Location = new System.Drawing.Point(10, 46);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(105, 20);
            this.lblMensaje.TabIndex = 9;
            this.lblMensaje.Text = "Mikrotik a ver:";
            // 
            // BtnBuscar
            // 
            this.BtnBuscar.Location = new System.Drawing.Point(297, 164);
            this.BtnBuscar.Name = "BtnBuscar";
            this.BtnBuscar.Size = new System.Drawing.Size(89, 39);
            this.BtnBuscar.TabIndex = 13;
            this.BtnBuscar.Text = "Buscar";
            this.BtnBuscar.UseVisualStyleBackColor = true;
            this.BtnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);
            // 
            // lblMensaje2
            // 
            this.lblMensaje2.AutoSize = true;
            this.lblMensaje2.Location = new System.Drawing.Point(8, 98);
            this.lblMensaje2.Name = "lblMensaje2";
            this.lblMensaje2.Size = new System.Drawing.Size(286, 20);
            this.lblMensaje2.TabIndex = 11;
            this.lblMensaje2.Text = "Escriba el nombre del usuario a buscar:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(12, 133);
            this.txtNombre.Multiline = true;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(263, 26);
            this.txtNombre.TabIndex = 12;
            // 
            // dgvUsuarios
            // 
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuarios.Location = new System.Drawing.Point(14, 256);
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.RowHeadersWidth = 62;
            this.dgvUsuarios.RowTemplate.Height = 28;
            this.dgvUsuarios.Size = new System.Drawing.Size(870, 483);
            this.dgvUsuarios.TabIndex = 14;
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mikrotiksToolStripMenuItem,
            this.migracionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(919, 33);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mikrotiksToolStripMenuItem
            // 
            this.mikrotiksToolStripMenuItem.Name = "mikrotiksToolStripMenuItem";
            this.mikrotiksToolStripMenuItem.Size = new System.Drawing.Size(101, 32);
            this.mikrotiksToolStripMenuItem.Text = "Mikrotiks";
            this.mikrotiksToolStripMenuItem.Click += new System.EventHandler(this.mikrotiksToolStripMenuItem_Click);
            // 
            // migracionToolStripMenuItem
            // 
            this.migracionToolStripMenuItem.Name = "migracionToolStripMenuItem";
            this.migracionToolStripMenuItem.Size = new System.Drawing.Size(107, 32);
            this.migracionToolStripMenuItem.Text = "Migración";
            this.migracionToolStripMenuItem.Click += new System.EventHandler(this.migracionToolStripMenuItem_Click);
            // 
            // lblMensaje3
            // 
            this.lblMensaje3.AutoSize = true;
            this.lblMensaje3.Location = new System.Drawing.Point(12, 183);
            this.lblMensaje3.Name = "lblMensaje3";
            this.lblMensaje3.Size = new System.Drawing.Size(211, 20);
            this.lblMensaje3.TabIndex = 17;
            this.lblMensaje3.Text = "Escriba el nombre del cliente";
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(14, 206);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(261, 26);
            this.txtCliente.TabIndex = 18;
            // 
            // CBAsignar
            // 
            this.CBAsignar.AutoSize = true;
            this.CBAsignar.Checked = true;
            this.CBAsignar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CBAsignar.Location = new System.Drawing.Point(424, 208);
            this.CBAsignar.Name = "CBAsignar";
            this.CBAsignar.Size = new System.Drawing.Size(203, 24);
            this.CBAsignar.TabIndex = 19;
            this.CBAsignar.Text = "¿Clientes Automaticos?";
            this.CBAsignar.UseVisualStyleBackColor = true;
            // 
            // BtnAsignar
            // 
            this.BtnAsignar.Location = new System.Drawing.Point(658, 206);
            this.BtnAsignar.Name = "BtnAsignar";
            this.BtnAsignar.Size = new System.Drawing.Size(98, 32);
            this.BtnAsignar.TabIndex = 20;
            this.BtnAsignar.Text = "Asignar";
            this.BtnAsignar.UseVisualStyleBackColor = true;
            this.BtnAsignar.Click += new System.EventHandler(this.BtnAsignar_Click);
            // 
            // lblMensaje4
            // 
            this.lblMensaje4.AutoSize = true;
            this.lblMensaje4.Location = new System.Drawing.Point(420, 164);
            this.lblMensaje4.Name = "lblMensaje4";
            this.lblMensaje4.Size = new System.Drawing.Size(171, 20);
            this.lblMensaje4.TabIndex = 21;
            this.lblMensaje4.Text = "Clientes sin servicios: 0";
            // 
            // btnClientesSin
            // 
            this.btnClientesSin.Location = new System.Drawing.Point(658, 151);
            this.btnClientesSin.Name = "btnClientesSin";
            this.btnClientesSin.Size = new System.Drawing.Size(98, 33);
            this.btnClientesSin.TabIndex = 22;
            this.btnClientesSin.Text = "Ver";
            this.btnClientesSin.UseVisualStyleBackColor = true;
            this.btnClientesSin.Click += new System.EventHandler(this.btnClientesSin_Click);
            // 
            // Clientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 789);
            this.Controls.Add(this.btnClientesSin);
            this.Controls.Add(this.lblMensaje4);
            this.Controls.Add(this.BtnAsignar);
            this.Controls.Add(this.CBAsignar);
            this.Controls.Add(this.txtCliente);
            this.Controls.Add(this.lblMensaje3);
            this.Controls.Add(this.dgvUsuarios);
            this.Controls.Add(this.BtnBuscar);
            this.Controls.Add(this.lblMensaje2);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.CBTodosMikrotiks);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.CBMikrotiks);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Clientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clientes";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Clientes_FormClosed);
            this.Load += new System.EventHandler(this.Clientes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox CBTodosMikrotiks;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ComboBox CBMikrotiks;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Button BtnBuscar;
        private System.Windows.Forms.Label lblMensaje2;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mikrotiksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem migracionToolStripMenuItem;
        private System.Windows.Forms.Label lblMensaje3;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.CheckBox CBAsignar;
        private System.Windows.Forms.Button BtnAsignar;
        private System.Windows.Forms.Label lblMensaje4;
        private System.Windows.Forms.Button btnClientesSin;
    }
}