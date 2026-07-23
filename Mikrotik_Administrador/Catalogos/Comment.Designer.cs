namespace Mikrotik_Administrador.Items
{
    partial class Comment
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
            this.lblFecha = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.CBMikrotiks = new System.Windows.Forms.ComboBox();
            this.lblMikrotik = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.panelContenedor.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFecha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblFecha.Location = new System.Drawing.Point(26, 20);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(98, 25);
            this.lblFecha.TabIndex = 0;
            this.lblFecha.Text = "Comment:";
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(309, 197);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(120, 38);
            this.btnGuardar.TabIndex = 2;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // panelContenedor
            // 
            this.panelContenedor.Controls.Add(this.CBMikrotiks);
            this.panelContenedor.Controls.Add(this.lblMikrotik);
            this.panelContenedor.Controls.Add(this.txtComment);
            this.panelContenedor.Controls.Add(this.btnGuardar);
            this.panelContenedor.Controls.Add(this.lblFecha);
            this.panelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedor.Location = new System.Drawing.Point(0, 0);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Padding = new System.Windows.Forms.Padding(20);
            this.panelContenedor.Size = new System.Drawing.Size(464, 247);
            this.panelContenedor.TabIndex = 4;
            // 
            // CBMikrotiks
            // 
            this.CBMikrotiks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBMikrotiks.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.CBMikrotiks.FormattingEnabled = true;
            this.CBMikrotiks.Location = new System.Drawing.Point(31, 132);
            this.CBMikrotiks.Name = "CBMikrotiks";
            this.CBMikrotiks.Size = new System.Drawing.Size(230, 33);
            this.CBMikrotiks.TabIndex = 6;
            // 
            // lblMikrotik
            // 
            this.lblMikrotik.AutoSize = true;
            this.lblMikrotik.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblMikrotik.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblMikrotik.Location = new System.Drawing.Point(26, 104);
            this.lblMikrotik.Name = "lblMikrotik";
            this.lblMikrotik.Size = new System.Drawing.Size(87, 25);
            this.lblMikrotik.TabIndex = 5;
            this.lblMikrotik.Text = "Mikrotik:";
            // 
            // txtComment
            // 
            this.txtComment.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtComment.Location = new System.Drawing.Point(31, 48);
            this.txtComment.MaxLength = 250;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(230, 33);
            this.txtComment.TabIndex = 4;
            // 
            // Comment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(464, 247);
            this.Controls.Add(this.panelContenedor);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Comment";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Comment_Load);
            this.panelContenedor.ResumeLayout(false);
            this.panelContenedor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.Label lblMikrotik;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.ComboBox CBMikrotiks;
    }
}