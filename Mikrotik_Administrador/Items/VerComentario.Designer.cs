namespace Mikrotik_Administrador.Items
{
    partial class VerComentario
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
            this.txtComentario = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtComentario
            // 
            this.txtComentario.BackColor = System.Drawing.Color.White; // Asegura fondo limpio aunque sea ReadOnly
            this.txtComentario.Font = new System.Drawing.Font("Segoe UI", 10F); // CORREGIDO: Consistencia de fuente
            this.txtComentario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40))))); // CORREGIDO: Texto oscuro legible
            this.txtComentario.Location = new System.Drawing.Point(41, 34);
            this.txtComentario.Multiline = true;
            this.txtComentario.Name = "txtComentario";
            this.txtComentario.ReadOnly = true;
            this.txtComentario.ScrollBars = System.Windows.Forms.ScrollBars.Vertical; // AGREGADO: Por si el texto es largo
            this.txtComentario.Size = new System.Drawing.Size(705, 314);
            this.txtComentario.TabIndex = 0;
            // 
            // VerComentario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F); // Ajustado para Segoe UI 9F/10F
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White; // CORREGIDO: Fondo blanco plano
            this.ClientSize = new System.Drawing.Size(800, 400); // Ajustado ligeramente para mejorar proporciones
            this.Controls.Add(this.txtComentario);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Mismo estándar
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VerComentario";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.VerComentario_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtComentario;
    }
}