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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DGVMikrotiks = new System.Windows.Forms.DataGridView();
            this.BtnNuevo = new System.Windows.Forms.Button();
            this.btnAddresList = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnVerMirkotiks = new System.Windows.Forms.Button();
            this.panelContenedor = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.DGVMikrotiks)).BeginInit();
            this.panelTop.SuspendLayout();
            this.panelContenedor.SuspendLayout();
            this.SuspendLayout();
            // 
            // DGVMikrotiks
            // 
            this.DGVMikrotiks.AllowUserToAddRows = false;
            this.DGVMikrotiks.BackgroundColor = System.Drawing.Color.White;
            this.DGVMikrotiks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DGVMikrotiks.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.DGVMikrotiks.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVMikrotiks.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGVMikrotiks.ColumnHeadersHeight = 35;
            this.DGVMikrotiks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(242)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGVMikrotiks.DefaultCellStyle = dataGridViewCellStyle2;
            this.DGVMikrotiks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGVMikrotiks.EnableHeadersVisualStyles = false;
            this.DGVMikrotiks.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DGVMikrotiks.Location = new System.Drawing.Point(20, 20);
            this.DGVMikrotiks.Name = "DGVMikrotiks";
            this.DGVMikrotiks.RowHeadersVisible = false;
            this.DGVMikrotiks.RowHeadersWidth = 51;
            this.DGVMikrotiks.RowTemplate.Height = 30;
            this.DGVMikrotiks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVMikrotiks.Size = new System.Drawing.Size(900, 319);
            this.DGVMikrotiks.TabIndex = 1;
            this.DGVMikrotiks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVMikrotiks_CellContentClick);
            // 
            // BtnNuevo
            // 
            this.BtnNuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.BtnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnNuevo.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.BtnNuevo.ForeColor = System.Drawing.Color.White;
            this.BtnNuevo.Location = new System.Drawing.Point(399, 16);
            this.BtnNuevo.Name = "BtnNuevo";
            this.BtnNuevo.Size = new System.Drawing.Size(100, 38);
            this.BtnNuevo.TabIndex = 3;
            this.BtnNuevo.Text = "+ Nuevo";
            this.BtnNuevo.UseVisualStyleBackColor = false;
            this.BtnNuevo.Click += new System.EventHandler(this.BtnNuevo_Click);
            // 
            // btnAddresList
            // 
            this.btnAddresList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(80)))), ((int)(((byte)(196)))));
            this.btnAddresList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddresList.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnAddresList.ForeColor = System.Drawing.Color.White;
            this.btnAddresList.Location = new System.Drawing.Point(198, 16);
            this.btnAddresList.Name = "btnAddresList";
            this.btnAddresList.Size = new System.Drawing.Size(160, 38);
            this.btnAddresList.TabIndex = 5;
            this.btnAddresList.Text = "Ver Address List";
            this.btnAddresList.UseVisualStyleBackColor = false;
            this.btnAddresList.Click += new System.EventHandler(this.btnAddresList_Click);
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.White;
            this.panelTop.Controls.Add(this.BtnNuevo);
            this.panelTop.Controls.Add(this.btnVerMirkotiks);
            this.panelTop.Controls.Add(this.btnAddresList);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(940, 70);
            this.panelTop.TabIndex = 8;
            // 
            // btnVerMirkotiks
            // 
            this.btnVerMirkotiks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(80)))), ((int)(((byte)(196)))));
            this.btnVerMirkotiks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerMirkotiks.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnVerMirkotiks.ForeColor = System.Drawing.Color.White;
            this.btnVerMirkotiks.Location = new System.Drawing.Point(20, 16);
            this.btnVerMirkotiks.Name = "btnVerMirkotiks";
            this.btnVerMirkotiks.Size = new System.Drawing.Size(150, 38);
            this.btnVerMirkotiks.TabIndex = 6;
            this.btnVerMirkotiks.Text = "Actualizar Lista";
            this.btnVerMirkotiks.UseVisualStyleBackColor = false;
            this.btnVerMirkotiks.Click += new System.EventHandler(this.btnVerMirkotiks_Click);
            // 
            // panelContenedor
            // 
            this.panelContenedor.Controls.Add(this.DGVMikrotiks);
            this.panelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedor.Location = new System.Drawing.Point(0, 70);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Padding = new System.Windows.Forms.Padding(20);
            this.panelContenedor.Size = new System.Drawing.Size(940, 359);
            this.panelContenedor.TabIndex = 9;
            // 
            // Mikrotiks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(940, 429);
            this.Controls.Add(this.panelContenedor);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.MaximizeBox = false;
            this.Name = "Mikrotiks";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.DGVMikrotiks)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelContenedor.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView DGVMikrotiks;
        private System.Windows.Forms.Button BtnNuevo;
        private System.Windows.Forms.Button btnAddresList;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.Button btnVerMirkotiks;
    }
}