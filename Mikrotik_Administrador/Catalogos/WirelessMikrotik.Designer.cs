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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvWireless = new System.Windows.Forms.DataGridView();
            this.BtnActualizar = new System.Windows.Forms.Button();
            this.BtnExtraer = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWireless)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvWireless
            // 
            this.dgvWireless.AllowUserToAddRows = false;
            this.dgvWireless.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvWireless.BackgroundColor = System.Drawing.Color.White;
            this.dgvWireless.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvWireless.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvWireless.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvWireless.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvWireless.ColumnHeadersHeight = 35;
            this.dgvWireless.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(242)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvWireless.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvWireless.EnableHeadersVisualStyles = false;
            this.dgvWireless.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvWireless.Location = new System.Drawing.Point(30, 123);
            this.dgvWireless.Name = "dgvWireless";
            this.dgvWireless.RowHeadersVisible = false;
            this.dgvWireless.RowHeadersWidth = 51;
            this.dgvWireless.RowTemplate.Height = 30;
            this.dgvWireless.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWireless.Size = new System.Drawing.Size(940, 317);
            this.dgvWireless.TabIndex = 0;
            // 
            // BtnActualizar
            // 
            this.BtnActualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(233)))), ((int)(((byte)(240)))));
            this.BtnActualizar.Enabled = false;
            this.BtnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnActualizar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.BtnActualizar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.BtnActualizar.Location = new System.Drawing.Point(315, 35);
            this.BtnActualizar.Name = "BtnActualizar";
            this.BtnActualizar.Size = new System.Drawing.Size(160, 38);
            this.BtnActualizar.TabIndex = 1;
            this.BtnActualizar.Text = "Enviar a Base";
            this.BtnActualizar.UseVisualStyleBackColor = false;
            this.BtnActualizar.Click += new System.EventHandler(this.BtnActualizar_Click);
            // 
            // BtnExtraer
            // 
            this.BtnExtraer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(80)))), ((int)(((byte)(196)))));
            this.BtnExtraer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnExtraer.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.BtnExtraer.ForeColor = System.Drawing.Color.White;
            this.BtnExtraer.Location = new System.Drawing.Point(30, 35);
            this.BtnExtraer.Name = "BtnExtraer";
            this.BtnExtraer.Size = new System.Drawing.Size(214, 38);
            this.BtnExtraer.TabIndex = 2;
            this.BtnExtraer.Text = "Extraer del Mikrotik";
            this.BtnExtraer.UseVisualStyleBackColor = false;
            this.BtnExtraer.Click += new System.EventHandler(this.BtnExtraer_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(30, 92);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(235, 12);
            this.progressBar1.TabIndex = 3;
            // 
            // WirelessMikrotik
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 475);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.BtnExtraer);
            this.Controls.Add(this.BtnActualizar);
            this.Controls.Add(this.dgvWireless);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.MaximizeBox = false;
            this.Name = "WirelessMikrotik";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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