namespace Mikrotik_Administrador
{
    partial class Menu
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mikrotiksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.migracionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.planesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asignacionClientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informacionClientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mikrotiksToolStripMenuItem,
            this.commentsToolStripMenuItem,
            this.migracionToolStripMenuItem,
            this.planesToolStripMenuItem,
            this.clientesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 33);
            this.menuStrip1.TabIndex = 0;
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
            // planesToolStripMenuItem
            // 
            this.planesToolStripMenuItem.Name = "planesToolStripMenuItem";
            this.planesToolStripMenuItem.Size = new System.Drawing.Size(78, 29);
            this.planesToolStripMenuItem.Text = "Planes";
            this.planesToolStripMenuItem.Click += new System.EventHandler(this.planesToolStripMenuItem_Click);
            // 
            // clientesToolStripMenuItem
            // 
            this.clientesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.asignacionClientesToolStripMenuItem,
            this.informacionClientesToolStripMenuItem});
            this.clientesToolStripMenuItem.Name = "clientesToolStripMenuItem";
            this.clientesToolStripMenuItem.Size = new System.Drawing.Size(89, 29);
            this.clientesToolStripMenuItem.Text = "Clientes";
            // 
            // asignacionClientesToolStripMenuItem
            // 
            this.asignacionClientesToolStripMenuItem.Name = "asignacionClientesToolStripMenuItem";
            this.asignacionClientesToolStripMenuItem.Size = new System.Drawing.Size(301, 34);
            this.asignacionClientesToolStripMenuItem.Text = "Asignación de Clientes";
            this.asignacionClientesToolStripMenuItem.Click += new System.EventHandler(this.asignacionClientesToolStripMenuItem_Click);
            // 
            // informacionClientesToolStripMenuItem
            // 
            this.informacionClientesToolStripMenuItem.Name = "informacionClientesToolStripMenuItem";
            this.informacionClientesToolStripMenuItem.Size = new System.Drawing.Size(301, 34);
            this.informacionClientesToolStripMenuItem.Text = "Información de Clientes";
            this.informacionClientesToolStripMenuItem.Click += new System.EventHandler(this.informacionClientesToolStripMenuItem_Click);
            // 
            // commentsToolStripMenuItem
            // 
            this.commentsToolStripMenuItem.Name = "commentsToolStripMenuItem";
            this.commentsToolStripMenuItem.Size = new System.Drawing.Size(115, 29);
            this.commentsToolStripMenuItem.Text = "Comments";
            this.commentsToolStripMenuItem.Click += new System.EventHandler(this.commentsToolStripMenuItem_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Menu_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mikrotiksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem migracionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asignacionClientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informacionClientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem planesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commentsToolStripMenuItem;
    }
}