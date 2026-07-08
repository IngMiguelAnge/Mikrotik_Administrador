namespace Mikrotik_Administrador.Items
{
    partial class VerImagen
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
            this.PBImagen = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PBImagen)).BeginInit();
            this.SuspendLayout();
            // 
            // PBImagen
            // 
            this.PBImagen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250))))); // Fondo gris muy claro neutro para contrastar la imagen
            this.PBImagen.Dock = System.Windows.Forms.DockStyle.Fill; // CORREGIDO: Se expande por completo en el contenedor
            this.PBImagen.Location = new System.Drawing.Point(20, 20); // Margen estético respetando el Padding
            this.PBImagen.Name = "PBImagen";
            this.PBImagen.Size = new System.Drawing.Size(560, 460);
            this.PBImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PBImagen.TabIndex = 0;
            this.PBImagen.TabStop = false;
            // 
            // VerImagen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White; // CORREGIDO: Fondo blanco plano
            this.ClientSize = new System.Drawing.Size(600, 500); // Ajustado para una mejor proporción inicial del comprobante
            this.Controls.Add(this.PBImagen);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VerImagen";
            this.Padding = new System.Windows.Forms.Padding(20); // AGREGADO: Margen interno para que la imagen no toque los bordes rectos de la ventana
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.VerImagen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PBImagen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PBImagen;
    }
}