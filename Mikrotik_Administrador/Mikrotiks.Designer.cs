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
            this.Menu = new System.Windows.Forms.MenuStrip();
            this.migraciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DGVMikrotiks = new System.Windows.Forms.DataGridView();
            this.lblListaMikrotiks = new System.Windows.Forms.Label();
            this.BtnNuevo = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.Menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVMikrotiks)).BeginInit();
            this.SuspendLayout();
            // 
            // Menu
            // 
            this.Menu.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.Menu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.migraciónToolStripMenuItem});
            this.Menu.Location = new System.Drawing.Point(0, 0);
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(903, 33);
            this.Menu.TabIndex = 0;
            this.Menu.Text = "menuStrip1";
            // 
            // migraciónToolStripMenuItem
            // 
            this.migraciónToolStripMenuItem.Name = "migraciónToolStripMenuItem";
            this.migraciónToolStripMenuItem.Size = new System.Drawing.Size(107, 29);
            this.migraciónToolStripMenuItem.Text = "Migración";
            // 
            // DGVMikrotiks
            // 
            this.DGVMikrotiks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVMikrotiks.Location = new System.Drawing.Point(66, 147);
            this.DGVMikrotiks.Name = "DGVMikrotiks";
            this.DGVMikrotiks.RowHeadersWidth = 62;
            this.DGVMikrotiks.RowTemplate.Height = 28;
            this.DGVMikrotiks.Size = new System.Drawing.Size(778, 150);
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
            this.BtnNuevo.Location = new System.Drawing.Point(34, 69);
            this.BtnNuevo.Name = "BtnNuevo";
            this.BtnNuevo.Size = new System.Drawing.Size(84, 31);
            this.BtnNuevo.TabIndex = 3;
            this.BtnNuevo.Text = "Nuevo";
            this.BtnNuevo.UseVisualStyleBackColor = true;
            this.BtnNuevo.Click += new System.EventHandler(this.BtnNuevo_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(197, 69);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(98, 30);
            this.btnActualizar.TabIndex = 4;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // Mikrotiks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 371);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.BtnNuevo);
            this.Controls.Add(this.lblListaMikrotiks);
            this.Controls.Add(this.DGVMikrotiks);
            this.Controls.Add(this.Menu);
            this.MainMenuStrip = this.Menu;
            this.MaximizeBox = false;
            this.Name = "Mikrotiks";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mikrotiks";
            this.Load += new System.EventHandler(this.Mikrotiks_Load);
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVMikrotiks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem migraciónToolStripMenuItem;
        private System.Windows.Forms.DataGridView DGVMikrotiks;
        private System.Windows.Forms.Label lblListaMikrotiks;
        private System.Windows.Forms.Button BtnNuevo;
        private System.Windows.Forms.Button btnActualizar;
    }
}