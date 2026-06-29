namespace Mikrotik_Administrador.Items
{
    partial class Prorroga
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
            this.lblMeses = new System.Windows.Forms.Label();
            this.NudMeses = new System.Windows.Forms.NumericUpDown();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.lblProximo = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lblProrroga = new System.Windows.Forms.Label();
            this.lblYaPagado = new System.Windows.Forms.Label();
            this.lblMensualidad = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NudMeses)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMeses
            // 
            this.lblMeses.AutoSize = true;
            this.lblMeses.Location = new System.Drawing.Point(41, 149);
            this.lblMeses.Name = "lblMeses";
            this.lblMeses.Size = new System.Drawing.Size(263, 20);
            this.lblMeses.TabIndex = 0;
            this.lblMeses.Text = "Agregue o quite meses de prorroga:";
            // 
            // NudMeses
            // 
            this.NudMeses.Location = new System.Drawing.Point(45, 195);
            this.NudMeses.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.NudMeses.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.NudMeses.Name = "NudMeses";
            this.NudMeses.Size = new System.Drawing.Size(120, 26);
            this.NudMeses.TabIndex = 1;
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.Location = new System.Drawing.Point(41, 53);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(259, 20);
            this.lblMensaje.TabIndex = 2;
            this.lblMensaje.Text = "Meses de prorroga ya agregados: 0";
            // 
            // lblProximo
            // 
            this.lblProximo.AutoSize = true;
            this.lblProximo.Location = new System.Drawing.Point(41, 240);
            this.lblProximo.Name = "lblProximo";
            this.lblProximo.Size = new System.Drawing.Size(221, 20);
            this.lblProximo.TabIndex = 3;
            this.lblProximo.Text = "Proximo pago esperado el dia:";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(41, 281);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(89, 20);
            this.lblFecha.TabIndex = 4;
            this.lblFecha.Text = "27/06/2026";
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Location = new System.Drawing.Point(41, 318);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(179, 20);
            this.lblCantidad.TabIndex = 5;
            this.lblCantidad.Text = "Cantidad a pagar: $0.00";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(213, 409);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(96, 37);
            this.btnGuardar.TabIndex = 6;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            // 
            // lblProrroga
            // 
            this.lblProrroga.AutoSize = true;
            this.lblProrroga.Location = new System.Drawing.Point(41, 101);
            this.lblProrroga.Name = "lblProrroga";
            this.lblProrroga.Size = new System.Drawing.Size(268, 20);
            this.lblProrroga.TabIndex = 7;
            this.lblProrroga.Text = "Meses de prorroga ya consumidos: 0";
            // 
            // lblYaPagado
            // 
            this.lblYaPagado.AutoSize = true;
            this.lblYaPagado.Location = new System.Drawing.Point(41, 365);
            this.lblYaPagado.Name = "lblYaPagado";
            this.lblYaPagado.Size = new System.Drawing.Size(199, 20);
            this.lblYaPagado.TabIndex = 8;
            this.lblYaPagado.Text = "Cantidad ya pagada: $0.00";
            // 
            // lblMensualidad
            // 
            this.lblMensualidad.AutoSize = true;
            this.lblMensualidad.Location = new System.Drawing.Point(41, 20);
            this.lblMensualidad.Name = "lblMensualidad";
            this.lblMensualidad.Size = new System.Drawing.Size(143, 20);
            this.lblMensualidad.TabIndex = 9;
            this.lblMensualidad.Text = "Mensualidad:$0.00";
            // 
            // Prorroga
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 458);
            this.Controls.Add(this.lblMensualidad);
            this.Controls.Add(this.lblYaPagado);
            this.Controls.Add(this.lblProrroga);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.lblProximo);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.NudMeses);
            this.Controls.Add(this.lblMeses);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Prorroga";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.NudMeses)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMeses;
        private System.Windows.Forms.NumericUpDown NudMeses;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Label lblProximo;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblProrroga;
        private System.Windows.Forms.Label lblYaPagado;
        private System.Windows.Forms.Label lblMensualidad;
    }
}