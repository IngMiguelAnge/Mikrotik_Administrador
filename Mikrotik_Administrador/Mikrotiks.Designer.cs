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
            this.DGVMikrotiks = new System.Windows.Forms.DataGridView();
            this.BtnNuevo = new System.Windows.Forms.Button();
            this.btnAddresList = new System.Windows.Forms.Button();
            this.btnVerMirkotiks = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.DGVMikrotiks)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DGVMikrotiks
            // 
            this.DGVMikrotiks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVMikrotiks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGVMikrotiks.Location = new System.Drawing.Point(3, 22);
            this.DGVMikrotiks.Name = "DGVMikrotiks";
            this.DGVMikrotiks.RowHeadersWidth = 62;
            this.DGVMikrotiks.RowTemplate.Height = 28;
            this.DGVMikrotiks.Size = new System.Drawing.Size(869, 315);
            this.DGVMikrotiks.TabIndex = 1;
            this.DGVMikrotiks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVMikrotiks_CellContentClick);
            // 
            // BtnNuevo
            // 
            this.BtnNuevo.Location = new System.Drawing.Point(34, 24);
            this.BtnNuevo.Name = "BtnNuevo";
            this.BtnNuevo.Size = new System.Drawing.Size(84, 37);
            this.BtnNuevo.TabIndex = 3;
            this.BtnNuevo.Text = "Nuevo";
            this.BtnNuevo.UseVisualStyleBackColor = true;
            this.BtnNuevo.Click += new System.EventHandler(this.BtnNuevo_Click);
            // 
            // btnAddresList
            // 
            this.btnAddresList.Location = new System.Drawing.Point(322, 24);
            this.btnAddresList.Name = "btnAddresList";
            this.btnAddresList.Size = new System.Drawing.Size(154, 37);
            this.btnAddresList.TabIndex = 5;
            this.btnAddresList.Text = "Ver Address List";
            this.btnAddresList.UseVisualStyleBackColor = true;
            this.btnAddresList.Click += new System.EventHandler(this.btnAddresList_Click);
            // 
            // btnVerMirkotiks
            // 
            this.btnVerMirkotiks.Location = new System.Drawing.Point(146, 24);
            this.btnVerMirkotiks.Name = "btnVerMirkotiks";
            this.btnVerMirkotiks.Size = new System.Drawing.Size(157, 35);
            this.btnVerMirkotiks.TabIndex = 6;
            this.btnVerMirkotiks.Text = "Ver Mikrotiks";
            this.btnVerMirkotiks.UseVisualStyleBackColor = true;
            this.btnVerMirkotiks.Click += new System.EventHandler(this.btnVerMirkotiks_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.DGVMikrotiks);
            this.groupBox1.Location = new System.Drawing.Point(34, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(875, 340);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // Mikrotiks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 429);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnVerMirkotiks);
            this.Controls.Add(this.btnAddresList);
            this.Controls.Add(this.BtnNuevo);
            this.Name = "Mikrotiks";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mikrotiks";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.DGVMikrotiks)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView DGVMikrotiks;
        private System.Windows.Forms.Button BtnNuevo;
        private System.Windows.Forms.Button btnAddresList;
        private System.Windows.Forms.Button btnVerMirkotiks;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}