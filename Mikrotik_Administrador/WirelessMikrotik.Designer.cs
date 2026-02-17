namespace Mikrotik_Administrador
{
    partial class WirelessMikrotik
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
            this.dgvWireless = new System.Windows.Forms.DataGridView();
            this.BtnActualizar = new System.Windows.Forms.Button();
            this.BtnExtraer = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWireless)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvWireless
            // 
            this.dgvWireless.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWireless.Location = new System.Drawing.Point(33, 103);
            this.dgvWireless.Name = "dgvWireless";
            this.dgvWireless.RowHeadersWidth = 62;
            this.dgvWireless.RowTemplate.Height = 28;
            this.dgvWireless.Size = new System.Drawing.Size(890, 345);
            this.dgvWireless.TabIndex = 0;
            // 
            // BtnActualizar
            // 
            this.BtnActualizar.Enabled = false;
            this.BtnActualizar.Location = new System.Drawing.Point(443, 50);
            this.BtnActualizar.Name = "BtnActualizar";
            this.BtnActualizar.Size = new System.Drawing.Size(156, 34);
            this.BtnActualizar.TabIndex = 1;
            this.BtnActualizar.Text = "Enviar a Base";
            this.BtnActualizar.UseVisualStyleBackColor = true;
            this.BtnActualizar.Click += new System.EventHandler(this.BtnActualizar_Click);
            // 
            // BtnExtraer
            // 
            this.BtnExtraer.Location = new System.Drawing.Point(33, 50);
            this.BtnExtraer.Name = "BtnExtraer";
            this.BtnExtraer.Size = new System.Drawing.Size(173, 34);
            this.BtnExtraer.TabIndex = 2;
            this.BtnExtraer.Text = "Extraer del mikrotik";
            this.BtnExtraer.UseVisualStyleBackColor = true;
            this.BtnExtraer.Click += new System.EventHandler(this.BtnExtraer_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(238, 50);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(146, 34);
            this.progressBar1.TabIndex = 3;
            // 
            // WirelessMikrotik
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 475);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.BtnExtraer);
            this.Controls.Add(this.BtnActualizar);
            this.Controls.Add(this.dgvWireless);
            this.MaximizeBox = false;
            this.Name = "WirelessMikrotik";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WirelessMikrotik";
            this.Load += new System.EventHandler(this.WirelessMikrotik_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWireless)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvWireless;
        private System.Windows.Forms.Button BtnActualizar;
        private System.Windows.Forms.Button BtnExtraer;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}