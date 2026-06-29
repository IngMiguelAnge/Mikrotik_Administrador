namespace Mikrotik_Administrador.Items
{
    partial class Pagar
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
            this.lblMensualidad = new System.Windows.Forms.Label();
            this.lblProrroga = new System.Windows.Forms.Label();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblProximo = new System.Windows.Forms.Label();
            this.lblYaPagado = new System.Windows.Forms.Label();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.NunCantidad = new System.Windows.Forms.NumericUpDown();
            this.lblPagar = new System.Windows.Forms.Label();
            this.lblSimbolo = new System.Windows.Forms.Label();
            this.lblBanco = new System.Windows.Forms.Label();
            this.CBBanco = new System.Windows.Forms.ComboBox();
            this.lblReferencia = new System.Windows.Forms.Label();
            this.txtReferencia = new System.Windows.Forms.TextBox();
            this.lblFechaPago = new System.Windows.Forms.Label();
            this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
            this.btnGuardar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NunCantidad)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMensualidad
            // 
            this.lblMensualidad.AutoSize = true;
            this.lblMensualidad.Location = new System.Drawing.Point(45, 36);
            this.lblMensualidad.Name = "lblMensualidad";
            this.lblMensualidad.Size = new System.Drawing.Size(147, 20);
            this.lblMensualidad.TabIndex = 0;
            this.lblMensualidad.Text = "Mensualidad: $0.00";
            // 
            // lblProrroga
            // 
            this.lblProrroga.AutoSize = true;
            this.lblProrroga.Location = new System.Drawing.Point(45, 135);
            this.lblProrroga.Name = "lblProrroga";
            this.lblProrroga.Size = new System.Drawing.Size(268, 20);
            this.lblProrroga.TabIndex = 9;
            this.lblProrroga.Text = "Meses de prorroga ya consumidos: 0";
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.Location = new System.Drawing.Point(45, 87);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(259, 20);
            this.lblMensaje.TabIndex = 8;
            this.lblMensaje.Text = "Meses de prorroga ya agregados: 0";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(45, 220);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(89, 20);
            this.lblFecha.TabIndex = 11;
            this.lblFecha.Text = "27/06/2026";
            // 
            // lblProximo
            // 
            this.lblProximo.AutoSize = true;
            this.lblProximo.Location = new System.Drawing.Point(45, 179);
            this.lblProximo.Name = "lblProximo";
            this.lblProximo.Size = new System.Drawing.Size(221, 20);
            this.lblProximo.TabIndex = 10;
            this.lblProximo.Text = "Proximo pago esperado el dia:";
            // 
            // lblYaPagado
            // 
            this.lblYaPagado.AutoSize = true;
            this.lblYaPagado.Location = new System.Drawing.Point(45, 301);
            this.lblYaPagado.Name = "lblYaPagado";
            this.lblYaPagado.Size = new System.Drawing.Size(199, 20);
            this.lblYaPagado.TabIndex = 13;
            this.lblYaPagado.Text = "Cantidad ya pagada: $0.00";
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Location = new System.Drawing.Point(45, 264);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(179, 20);
            this.lblCantidad.TabIndex = 12;
            this.lblCantidad.Text = "Cantidad a pagar: $0.00";
            // 
            // NunCantidad
            // 
            this.NunCantidad.Location = new System.Drawing.Point(486, 81);
            this.NunCantidad.Name = "NunCantidad";
            this.NunCantidad.Size = new System.Drawing.Size(120, 26);
            this.NunCantidad.TabIndex = 14;
            // 
            // lblPagar
            // 
            this.lblPagar.AutoSize = true;
            this.lblPagar.Location = new System.Drawing.Point(482, 58);
            this.lblPagar.Name = "lblPagar";
            this.lblPagar.Size = new System.Drawing.Size(55, 20);
            this.lblPagar.TabIndex = 15;
            this.lblPagar.Text = "Pagar:";
            // 
            // lblSimbolo
            // 
            this.lblSimbolo.AutoSize = true;
            this.lblSimbolo.Location = new System.Drawing.Point(462, 87);
            this.lblSimbolo.Name = "lblSimbolo";
            this.lblSimbolo.Size = new System.Drawing.Size(18, 20);
            this.lblSimbolo.TabIndex = 16;
            this.lblSimbolo.Text = "$";
            // 
            // lblBanco
            // 
            this.lblBanco.AutoSize = true;
            this.lblBanco.Location = new System.Drawing.Point(462, 135);
            this.lblBanco.Name = "lblBanco";
            this.lblBanco.Size = new System.Drawing.Size(59, 20);
            this.lblBanco.TabIndex = 17;
            this.lblBanco.Text = "Banco:";
            // 
            // CBBanco
            // 
            this.CBBanco.FormattingEnabled = true;
            this.CBBanco.Location = new System.Drawing.Point(466, 158);
            this.CBBanco.Name = "CBBanco";
            this.CBBanco.Size = new System.Drawing.Size(121, 28);
            this.CBBanco.TabIndex = 18;
            // 
            // lblReferencia
            // 
            this.lblReferencia.AutoSize = true;
            this.lblReferencia.Location = new System.Drawing.Point(462, 207);
            this.lblReferencia.Name = "lblReferencia";
            this.lblReferencia.Size = new System.Drawing.Size(91, 20);
            this.lblReferencia.TabIndex = 19;
            this.lblReferencia.Text = "Referencia:";
            // 
            // txtReferencia
            // 
            this.txtReferencia.Location = new System.Drawing.Point(466, 241);
            this.txtReferencia.Name = "txtReferencia";
            this.txtReferencia.Size = new System.Drawing.Size(189, 26);
            this.txtReferencia.TabIndex = 20;
            // 
            // lblFechaPago
            // 
            this.lblFechaPago.AutoSize = true;
            this.lblFechaPago.Location = new System.Drawing.Point(462, 291);
            this.lblFechaPago.Name = "lblFechaPago";
            this.lblFechaPago.Size = new System.Drawing.Size(196, 20);
            this.lblFechaPago.TabIndex = 21;
            this.lblFechaPago.Text = "Cuando se realizo el pago:";
            // 
            // dtpFechaPago
            // 
            this.dtpFechaPago.Location = new System.Drawing.Point(466, 330);
            this.dtpFechaPago.Name = "dtpFechaPago";
            this.dtpFechaPago.Size = new System.Drawing.Size(325, 26);
            this.dtpFechaPago.TabIndex = 22;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(699, 399);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(92, 39);
            this.btnGuardar.TabIndex = 23;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            // 
            // Pagar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 450);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.dtpFechaPago);
            this.Controls.Add(this.lblFechaPago);
            this.Controls.Add(this.txtReferencia);
            this.Controls.Add(this.lblReferencia);
            this.Controls.Add(this.CBBanco);
            this.Controls.Add(this.lblBanco);
            this.Controls.Add(this.lblSimbolo);
            this.Controls.Add(this.lblPagar);
            this.Controls.Add(this.NunCantidad);
            this.Controls.Add(this.lblYaPagado);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.lblProximo);
            this.Controls.Add(this.lblProrroga);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.lblMensualidad);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Pagar";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.NunCantidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMensualidad;
        private System.Windows.Forms.Label lblProrroga;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblProximo;
        private System.Windows.Forms.Label lblYaPagado;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.NumericUpDown NunCantidad;
        private System.Windows.Forms.Label lblPagar;
        private System.Windows.Forms.Label lblSimbolo;
        private System.Windows.Forms.Label lblBanco;
        private System.Windows.Forms.ComboBox CBBanco;
        private System.Windows.Forms.Label lblReferencia;
        private System.Windows.Forms.TextBox txtReferencia;
        private System.Windows.Forms.Label lblFechaPago;
        private System.Windows.Forms.DateTimePicker dtpFechaPago;
        private System.Windows.Forms.Button btnGuardar;
    }
}