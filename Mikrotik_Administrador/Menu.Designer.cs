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
            this.btnMikrotiks = new System.Windows.Forms.Button();
            this.BtnComments = new System.Windows.Forms.Button();
            this.btnMigracion = new System.Windows.Forms.Button();
            this.btnPlanes = new System.Windows.Forms.Button();
            this.btnAsignacion = new System.Windows.Forms.Button();
            this.btnInformacion = new System.Windows.Forms.Button();
            this.lblMenu = new System.Windows.Forms.Label();
            this.btnBancos = new System.Windows.Forms.Button();
            this.btnPagos = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMikrotiks
            // 
            this.btnMikrotiks.Location = new System.Drawing.Point(30, 72);
            this.btnMikrotiks.Name = "btnMikrotiks";
            this.btnMikrotiks.Size = new System.Drawing.Size(123, 61);
            this.btnMikrotiks.TabIndex = 1;
            this.btnMikrotiks.Text = "Mikrotiks";
            this.btnMikrotiks.UseVisualStyleBackColor = true;
            this.btnMikrotiks.Click += new System.EventHandler(this.btnMikrotiks_Click);
            // 
            // BtnComments
            // 
            this.BtnComments.Location = new System.Drawing.Point(187, 72);
            this.BtnComments.Name = "BtnComments";
            this.BtnComments.Size = new System.Drawing.Size(124, 61);
            this.BtnComments.TabIndex = 2;
            this.BtnComments.Text = "Comments";
            this.BtnComments.UseVisualStyleBackColor = true;
            this.BtnComments.Click += new System.EventHandler(this.BtnComments_Click);
            // 
            // btnMigracion
            // 
            this.btnMigracion.Location = new System.Drawing.Point(351, 72);
            this.btnMigracion.Name = "btnMigracion";
            this.btnMigracion.Size = new System.Drawing.Size(121, 61);
            this.btnMigracion.TabIndex = 3;
            this.btnMigracion.Text = "Migración";
            this.btnMigracion.UseVisualStyleBackColor = true;
            this.btnMigracion.Click += new System.EventHandler(this.btnMigracion_Click);
            // 
            // btnPlanes
            // 
            this.btnPlanes.Location = new System.Drawing.Point(497, 72);
            this.btnPlanes.Name = "btnPlanes";
            this.btnPlanes.Size = new System.Drawing.Size(124, 61);
            this.btnPlanes.TabIndex = 4;
            this.btnPlanes.Text = "Planes";
            this.btnPlanes.UseVisualStyleBackColor = true;
            this.btnPlanes.Click += new System.EventHandler(this.btnPlanes_Click);
            // 
            // btnAsignacion
            // 
            this.btnAsignacion.Location = new System.Drawing.Point(30, 156);
            this.btnAsignacion.Name = "btnAsignacion";
            this.btnAsignacion.Size = new System.Drawing.Size(123, 62);
            this.btnAsignacion.TabIndex = 5;
            this.btnAsignacion.Text = "Asignaciones";
            this.btnAsignacion.UseVisualStyleBackColor = true;
            this.btnAsignacion.Click += new System.EventHandler(this.btnAsignacion_Click);
            // 
            // btnInformacion
            // 
            this.btnInformacion.Location = new System.Drawing.Point(187, 156);
            this.btnInformacion.Name = "btnInformacion";
            this.btnInformacion.Size = new System.Drawing.Size(124, 62);
            this.btnInformacion.TabIndex = 6;
            this.btnInformacion.Text = "Información";
            this.btnInformacion.UseVisualStyleBackColor = true;
            this.btnInformacion.Click += new System.EventHandler(this.btnInformacion_Click);
            // 
            // lblMenu
            // 
            this.lblMenu.AutoSize = true;
            this.lblMenu.Location = new System.Drawing.Point(313, 34);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(56, 20);
            this.lblMenu.TabIndex = 7;
            this.lblMenu.Text = "MENU";
            // 
            // btnBancos
            // 
            this.btnBancos.Location = new System.Drawing.Point(351, 156);
            this.btnBancos.Name = "btnBancos";
            this.btnBancos.Size = new System.Drawing.Size(121, 62);
            this.btnBancos.TabIndex = 8;
            this.btnBancos.Text = "Bancos";
            this.btnBancos.UseVisualStyleBackColor = true;
            this.btnBancos.Click += new System.EventHandler(this.btnBancos_Click);
            // 
            // btnPagos
            // 
            this.btnPagos.Location = new System.Drawing.Point(500, 156);
            this.btnPagos.Name = "btnPagos";
            this.btnPagos.Size = new System.Drawing.Size(121, 62);
            this.btnPagos.TabIndex = 9;
            this.btnPagos.Text = "Pagos";
            this.btnPagos.UseVisualStyleBackColor = true;
            this.btnPagos.Click += new System.EventHandler(this.btnPagos_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 450);
            this.Controls.Add(this.btnPagos);
            this.Controls.Add(this.btnBancos);
            this.Controls.Add(this.lblMenu);
            this.Controls.Add(this.btnInformacion);
            this.Controls.Add(this.btnAsignacion);
            this.Controls.Add(this.btnPlanes);
            this.Controls.Add(this.btnMigracion);
            this.Controls.Add(this.BtnComments);
            this.Controls.Add(this.btnMikrotiks);
            this.MaximizeBox = false;
            this.Name = "Menu";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Menu_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnMikrotiks;
        private System.Windows.Forms.Button BtnComments;
        private System.Windows.Forms.Button btnMigracion;
        private System.Windows.Forms.Button btnPlanes;
        private System.Windows.Forms.Button btnAsignacion;
        private System.Windows.Forms.Button btnInformacion;
        private System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.Button btnBancos;
        private System.Windows.Forms.Button btnPagos;
    }
}