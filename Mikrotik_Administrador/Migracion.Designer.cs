namespace Mikrotik_Administrador
{
    partial class Migracion
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
            this.Menu = new System.Windows.Forms.MenuStrip();
            this.mikrotiksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.CBMikrotiks = new System.Windows.Forms.ComboBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblMensaje2 = new System.Windows.Forms.Label();
            this.BtnBuscar = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.RBBase = new System.Windows.Forms.RadioButton();
            this.RBMikrotik = new System.Windows.Forms.RadioButton();
            this.lblMensaje3 = new System.Windows.Forms.Label();
            this.CBTodosMikrotiks = new System.Windows.Forms.CheckBox();
            this.btnExportar = new System.Windows.Forms.Button();
            this.cbExportar = new System.Windows.Forms.CheckBox();
            this.Menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.SuspendLayout();
            // 
            // Menu
            // 
            this.Menu.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.Menu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mikrotiksToolStripMenuItem});
            this.Menu.Location = new System.Drawing.Point(0, 0);
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(800, 33);
            this.Menu.TabIndex = 0;
            this.Menu.Text = "menuStrip1";
            // 
            // mikrotiksToolStripMenuItem
            // 
            this.mikrotiksToolStripMenuItem.Name = "mikrotiksToolStripMenuItem";
            this.mikrotiksToolStripMenuItem.Size = new System.Drawing.Size(101, 29);
            this.mikrotiksToolStripMenuItem.Text = "Mikrotiks";
            this.mikrotiksToolStripMenuItem.Click += new System.EventHandler(this.mikrotiksToolStripMenuItem_Click);
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.Location = new System.Drawing.Point(29, 64);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(105, 20);
            this.lblMensaje.TabIndex = 1;
            this.lblMensaje.Text = "Mikrotik a ver:";
            // 
            // CBMikrotiks
            // 
            this.CBMikrotiks.FormattingEnabled = true;
            this.CBMikrotiks.Location = new System.Drawing.Point(164, 64);
            this.CBMikrotiks.Name = "CBMikrotiks";
            this.CBMikrotiks.Size = new System.Drawing.Size(191, 28);
            this.CBMikrotiks.TabIndex = 0;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(378, 137);
            this.txtNombre.Multiline = true;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(263, 26);
            this.txtNombre.TabIndex = 4;
            // 
            // lblMensaje2
            // 
            this.lblMensaje2.AutoSize = true;
            this.lblMensaje2.Location = new System.Drawing.Point(374, 105);
            this.lblMensaje2.Name = "lblMensaje2";
            this.lblMensaje2.Size = new System.Drawing.Size(286, 20);
            this.lblMensaje2.TabIndex = 4;
            this.lblMensaje2.Text = "Escriba el nombre del usuario a buscar:";
            // 
            // BtnBuscar
            // 
            this.BtnBuscar.Location = new System.Drawing.Point(668, 131);
            this.BtnBuscar.Name = "BtnBuscar";
            this.BtnBuscar.Size = new System.Drawing.Size(89, 39);
            this.BtnBuscar.TabIndex = 5;
            this.BtnBuscar.Text = "Buscar";
            this.BtnBuscar.UseVisualStyleBackColor = true;
            this.BtnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(642, 61);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(133, 23);
            this.progressBar1.TabIndex = 6;
            // 
            // dgvUsuarios
            // 
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuarios.Location = new System.Drawing.Point(23, 228);
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.RowHeadersWidth = 62;
            this.dgvUsuarios.RowTemplate.Height = 28;
            this.dgvUsuarios.Size = new System.Drawing.Size(755, 157);
            this.dgvUsuarios.TabIndex = 7;
            this.dgvUsuarios.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsuarios_CellContentClick);
            // 
            // RBBase
            // 
            this.RBBase.AutoSize = true;
            this.RBBase.Checked = true;
            this.RBBase.Location = new System.Drawing.Point(33, 131);
            this.RBBase.Name = "RBBase";
            this.RBBase.Size = new System.Drawing.Size(132, 24);
            this.RBBase.TabIndex = 2;
            this.RBBase.TabStop = true;
            this.RBBase.Text = "Base General";
            this.RBBase.UseVisualStyleBackColor = true;
            this.RBBase.CheckedChanged += new System.EventHandler(this.RBBase_CheckedChanged);
            // 
            // RBMikrotik
            // 
            this.RBMikrotik.AutoSize = true;
            this.RBMikrotik.Location = new System.Drawing.Point(33, 182);
            this.RBMikrotik.Name = "RBMikrotik";
            this.RBMikrotik.Size = new System.Drawing.Size(88, 24);
            this.RBMikrotik.TabIndex = 3;
            this.RBMikrotik.Text = "Mikrotik";
            this.RBMikrotik.UseVisualStyleBackColor = true;
            this.RBMikrotik.CheckedChanged += new System.EventHandler(this.RBMikrotik_CheckedChanged);
            // 
            // lblMensaje3
            // 
            this.lblMensaje3.AutoSize = true;
            this.lblMensaje3.Location = new System.Drawing.Point(33, 105);
            this.lblMensaje3.Name = "lblMensaje3";
            this.lblMensaje3.Size = new System.Drawing.Size(277, 20);
            this.lblMensaje3.TabIndex = 10;
            this.lblMensaje3.Text = "Seleccione el lugar donde se buscara:";
            // 
            // CBTodosMikrotiks
            // 
            this.CBTodosMikrotiks.AutoSize = true;
            this.CBTodosMikrotiks.Location = new System.Drawing.Point(378, 61);
            this.CBTodosMikrotiks.Name = "CBTodosMikrotiks";
            this.CBTodosMikrotiks.Size = new System.Drawing.Size(259, 24);
            this.CBTodosMikrotiks.TabIndex = 1;
            this.CBTodosMikrotiks.Text = "¿Buscar en todos los mikrotiks?";
            this.CBTodosMikrotiks.UseVisualStyleBackColor = true;
            this.CBTodosMikrotiks.CheckedChanged += new System.EventHandler(this.CBTodosMikrotiks_CheckedChanged);
            // 
            // btnExportar
            // 
            this.btnExportar.Location = new System.Drawing.Point(342, 177);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(80, 35);
            this.btnExportar.TabIndex = 7;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Visible = false;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // cbExportar
            // 
            this.cbExportar.AutoSize = true;
            this.cbExportar.Location = new System.Drawing.Point(187, 182);
            this.cbExportar.Name = "cbExportar";
            this.cbExportar.Size = new System.Drawing.Size(149, 24);
            this.cbExportar.TabIndex = 6;
            this.cbExportar.Text = "¿Exportar todo?";
            this.cbExportar.UseVisualStyleBackColor = true;
            this.cbExportar.Visible = false;
            // 
            // Migracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cbExportar);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.CBTodosMikrotiks);
            this.Controls.Add(this.lblMensaje3);
            this.Controls.Add(this.RBMikrotik);
            this.Controls.Add(this.RBBase);
            this.Controls.Add(this.dgvUsuarios);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.BtnBuscar);
            this.Controls.Add(this.lblMensaje2);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.CBMikrotiks);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.Menu);
            this.MainMenuStrip = this.Menu;
            this.MaximizeBox = false;
            this.Name = "Migracion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Migracion";
            this.Load += new System.EventHandler(this.Migracion_Load);
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem mikrotiksToolStripMenuItem;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.ComboBox CBMikrotiks;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblMensaje2;
        private System.Windows.Forms.Button BtnBuscar;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.RadioButton RBBase;
        private System.Windows.Forms.RadioButton RBMikrotik;
        private System.Windows.Forms.Label lblMensaje3;
        private System.Windows.Forms.CheckBox CBTodosMikrotiks;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.CheckBox cbExportar;
    }
}