namespace Mikrotik_Administrador.Items
{
    partial class Programar
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
            this.btnContinuar = new System.Windows.Forms.Button();
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.CBAccion = new System.Windows.Forms.ComboBox();
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
            this.lblFecha.Size = new System.Drawing.Size(151, 25);
            this.lblFecha.TabIndex = 0;
            this.lblFecha.Text = "Acción a realizar:";
            // 
            // btnContinuar
            // 
            this.btnContinuar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnContinuar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContinuar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnContinuar.ForeColor = System.Drawing.Color.White;
            this.btnContinuar.Location = new System.Drawing.Point(189, 121);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(120, 38);
            this.btnContinuar.TabIndex = 2;
            this.btnContinuar.Text = "Continuar";
            this.btnContinuar.UseVisualStyleBackColor = false;
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            // 
            // panelContenedor
            // 
            this.panelContenedor.Controls.Add(this.CBAccion);
            this.panelContenedor.Controls.Add(this.btnContinuar);
            this.panelContenedor.Controls.Add(this.lblFecha);
            this.panelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedor.Location = new System.Drawing.Point(0, 0);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Padding = new System.Windows.Forms.Padding(20);
            this.panelContenedor.Size = new System.Drawing.Size(341, 171);
            this.panelContenedor.TabIndex = 4;
            // 
            // CBAccion
            // 
            this.CBAccion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBAccion.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.CBAccion.FormattingEnabled = true;
            this.CBAccion.Items.AddRange(new object[] {
            "Seleccione",
            "Cambio de plan",
            "Suspensión",
            "Reactivación"});
            this.CBAccion.Location = new System.Drawing.Point(31, 62);
            this.CBAccion.Name = "CBAccion";
            this.CBAccion.Size = new System.Drawing.Size(278, 33);
            this.CBAccion.TabIndex = 1;
            // 
            // Programar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(341, 171);
            this.Controls.Add(this.panelContenedor);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Programar";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Programar_Load);
            this.panelContenedor.ResumeLayout(false);
            this.panelContenedor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Button btnContinuar;
        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.ComboBox CBAccion;
    }
}