namespace Mikrotik_Administrador
{
    partial class Mikrotiks
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
            this.components = new System.ComponentModel.Container();
            this.Menu = new System.Windows.Forms.MenuStrip();
            this.migracionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DGVMikrotiks = new System.Windows.Forms.DataGridView();
            this.lblListaMikrotiks = new System.Windows.Forms.Label();
            this.BtnNuevo = new System.Windows.Forms.Button();
            this.btnAddresList = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnVerMirkotiks = new System.Windows.Forms.Button();
            this.clientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVMikrotiks)).BeginInit();
            this.SuspendLayout();
            // 
            // Menu
            // 
            this.Menu.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.Menu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.migracionToolStripMenuItem,
            this.clientesToolStripMenuItem});
            this.Menu.Location = new System.Drawing.Point(0, 0);
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(1204, 33);
            this.Menu.TabIndex = 0;
            this.Menu.Text = "menuStrip1";
            // 
            // migracionToolStripMenuItem
            // 
            this.migracionToolStripMenuItem.Name = "migracionToolStripMenuItem";
            this.migracionToolStripMenuItem.Size = new System.Drawing.Size(107, 29);
            this.migracionToolStripMenuItem.Text = "Migración";
            this.migracionToolStripMenuItem.Click += new System.EventHandler(this.migracionToolStripMenuItem_Click);
            // 
            // DGVMikrotiks
            // 
            this.DGVMikrotiks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVMikrotiks.Location = new System.Drawing.Point(34, 147);
            this.DGVMikrotiks.Name = "DGVMikrotiks";
            this.DGVMikrotiks.RowHeadersWidth = 62;
            this.DGVMikrotiks.RowTemplate.Height = 28;
            this.DGVMikrotiks.Size = new System.Drawing.Size(1128, 261);
            this.DGVMikrotiks.TabIndex = 1;
            this.DGVMikrotiks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVMikrotiks_CellContentClick);
            // 
            // lblListaMikrotiks
            // 
            this.lblListaMikrotiks.AutoSize = true;
            this.lblListaMikrotiks.Location = new System.Drawing.Point(51, 124);
            this.lblListaMikrotiks.Name = "lblListaMikrotiks";
            this.lblListaMikrotiks.Size = new System.Drawing.Size(161, 20);
            this.lblListaMikrotiks.TabIndex = 2;
            this.lblListaMikrotiks.Text = "Mikrotiks Registrados";
            // 
            // BtnNuevo
            // 
            this.BtnNuevo.Location = new System.Drawing.Point(34, 63);
            this.BtnNuevo.Name = "BtnNuevo";
            this.BtnNuevo.Size = new System.Drawing.Size(84, 37);
            this.BtnNuevo.TabIndex = 3;
            this.BtnNuevo.Text = "Nuevo";
            this.toolTip1.SetToolTip(this.BtnNuevo, "Registra nuevo mikrotik");
            this.BtnNuevo.UseVisualStyleBackColor = true;
            this.BtnNuevo.Click += new System.EventHandler(this.BtnNuevo_Click);
            // 
            // btnAddresList
            // 
            this.btnAddresList.Location = new System.Drawing.Point(322, 63);
            this.btnAddresList.Name = "btnAddresList";
            this.btnAddresList.Size = new System.Drawing.Size(154, 37);
            this.btnAddresList.TabIndex = 5;
            this.btnAddresList.Text = "Ver Address List";
            this.toolTip1.SetToolTip(this.btnAddresList, "Permite ver los Address list guardados en la base general");
            this.btnAddresList.UseVisualStyleBackColor = true;
            this.btnAddresList.Click += new System.EventHandler(this.btnAddresList_Click);
            // 
            // btnVerMirkotiks
            // 
            this.btnVerMirkotiks.Location = new System.Drawing.Point(146, 63);
            this.btnVerMirkotiks.Name = "btnVerMirkotiks";
            this.btnVerMirkotiks.Size = new System.Drawing.Size(157, 35);
            this.btnVerMirkotiks.TabIndex = 6;
            this.btnVerMirkotiks.Text = "Ver Mikrotiks";
            this.btnVerMirkotiks.UseVisualStyleBackColor = true;
            this.btnVerMirkotiks.Click += new System.EventHandler(this.btnVerMirkotiks_Click);
            // 
            // clientesToolStripMenuItem
            // 
            this.clientesToolStripMenuItem.Name = "clientesToolStripMenuItem";
            this.clientesToolStripMenuItem.Size = new System.Drawing.Size(89, 29);
            this.clientesToolStripMenuItem.Text = "Clientes";
            this.clientesToolStripMenuItem.Click += new System.EventHandler(this.clientesToolStripMenuItem_Click);
            // 
            // Mikrotiks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1204, 420);
            this.Controls.Add(this.btnVerMirkotiks);
            this.Controls.Add(this.btnAddresList);
            this.Controls.Add(this.BtnNuevo);
            this.Controls.Add(this.lblListaMikrotiks);
            this.Controls.Add(this.DGVMikrotiks);
            this.Controls.Add(this.Menu);
            this.MainMenuStrip = this.Menu;
            this.MaximizeBox = false;
            this.Name = "Mikrotiks";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mikrotiks";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Mikrotiks_FormClosed);
            this.Load += new System.EventHandler(this.Mikrotiks_Load);
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVMikrotiks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem migracionToolStripMenuItem;
        private System.Windows.Forms.DataGridView DGVMikrotiks;
        private System.Windows.Forms.Label lblListaMikrotiks;
        private System.Windows.Forms.Button BtnNuevo;
        private System.Windows.Forms.Button btnAddresList;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnVerMirkotiks;
        private System.Windows.Forms.ToolStripMenuItem clientesToolStripMenuItem;
    }
}