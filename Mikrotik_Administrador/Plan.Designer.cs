namespace Mikrotik_Administrador
{
    partial class Plan
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
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.NUDPrecio = new System.Windows.Forms.NumericUpDown();
            this.lblSubida = new System.Windows.Forms.Label();
            this.NUDSubida = new System.Windows.Forms.NumericUpDown();
            this.cbSubida = new System.Windows.Forms.ComboBox();
            this.lblDescarga = new System.Windows.Forms.Label();
            this.NUDDescarga = new System.Windows.Forms.NumericUpDown();
            this.CBDescarga = new System.Windows.Forms.ComboBox();
            this.BtnGuardar = new System.Windows.Forms.Button();
            this.lblPerteneceA = new System.Windows.Forms.Label();
            this.CBPerteneceA = new System.Windows.Forms.ComboBox();
            this.lblNota = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblMensajeplan = new System.Windows.Forms.Label();
            this.lblCantidadenPlan = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NUDPrecio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDSubida)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDDescarga)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(54, 36);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(75, 20);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "*Nombre:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(145, 30);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(252, 26);
            this.txtNombre.TabIndex = 1;
            // 
            // lblPrecio
            // 
            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Location = new System.Drawing.Point(54, 89);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(63, 20);
            this.lblPrecio.TabIndex = 2;
            this.lblPrecio.Text = "*Precio:";
            // 
            // NUDPrecio
            // 
            this.NUDPrecio.DecimalPlaces = 2;
            this.NUDPrecio.Location = new System.Drawing.Point(145, 87);
            this.NUDPrecio.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.NUDPrecio.Name = "NUDPrecio";
            this.NUDPrecio.Size = new System.Drawing.Size(120, 26);
            this.NUDPrecio.TabIndex = 3;
            // 
            // lblSubida
            // 
            this.lblSubida.AutoSize = true;
            this.lblSubida.Location = new System.Drawing.Point(54, 133);
            this.lblSubida.Name = "lblSubida";
            this.lblSubida.Size = new System.Drawing.Size(143, 20);
            this.lblSubida.TabIndex = 4;
            this.lblSubida.Text = "*Velocidad Subida:";
            // 
            // NUDSubida
            // 
            this.NUDSubida.Location = new System.Drawing.Point(229, 133);
            this.NUDSubida.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.NUDSubida.Name = "NUDSubida";
            this.NUDSubida.Size = new System.Drawing.Size(120, 26);
            this.NUDSubida.TabIndex = 5;
            // 
            // cbSubida
            // 
            this.cbSubida.FormattingEnabled = true;
            this.cbSubida.Items.AddRange(new object[] {
            "M",
            "k"});
            this.cbSubida.Location = new System.Drawing.Point(366, 132);
            this.cbSubida.Name = "cbSubida";
            this.cbSubida.Size = new System.Drawing.Size(121, 28);
            this.cbSubida.TabIndex = 6;
            // 
            // lblDescarga
            // 
            this.lblDescarga.AutoSize = true;
            this.lblDescarga.Location = new System.Drawing.Point(54, 191);
            this.lblDescarga.Name = "lblDescarga";
            this.lblDescarga.Size = new System.Drawing.Size(162, 20);
            this.lblDescarga.TabIndex = 7;
            this.lblDescarga.Text = "*Velocidad Descarga:";
            // 
            // NUDDescarga
            // 
            this.NUDDescarga.Location = new System.Drawing.Point(229, 189);
            this.NUDDescarga.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.NUDDescarga.Name = "NUDDescarga";
            this.NUDDescarga.Size = new System.Drawing.Size(120, 26);
            this.NUDDescarga.TabIndex = 8;
            // 
            // CBDescarga
            // 
            this.CBDescarga.FormattingEnabled = true;
            this.CBDescarga.Items.AddRange(new object[] {
            "M",
            "k"});
            this.CBDescarga.Location = new System.Drawing.Point(366, 187);
            this.CBDescarga.Name = "CBDescarga";
            this.CBDescarga.Size = new System.Drawing.Size(121, 28);
            this.CBDescarga.TabIndex = 9;
            // 
            // BtnGuardar
            // 
            this.BtnGuardar.Location = new System.Drawing.Point(58, 377);
            this.BtnGuardar.Name = "BtnGuardar";
            this.BtnGuardar.Size = new System.Drawing.Size(97, 31);
            this.BtnGuardar.TabIndex = 12;
            this.BtnGuardar.Text = "Guardar";
            this.BtnGuardar.UseVisualStyleBackColor = true;
            this.BtnGuardar.Click += new System.EventHandler(this.BtnGuardar_Click);
            // 
            // lblPerteneceA
            // 
            this.lblPerteneceA.AutoSize = true;
            this.lblPerteneceA.Location = new System.Drawing.Point(54, 251);
            this.lblPerteneceA.Name = "lblPerteneceA";
            this.lblPerteneceA.Size = new System.Drawing.Size(107, 20);
            this.lblPerteneceA.TabIndex = 10;
            this.lblPerteneceA.Text = "*Pertenece A:";
            // 
            // CBPerteneceA
            // 
            this.CBPerteneceA.FormattingEnabled = true;
            this.CBPerteneceA.Items.AddRange(new object[] {
            "Antena",
            "Fibra"});
            this.CBPerteneceA.Location = new System.Drawing.Point(191, 243);
            this.CBPerteneceA.Name = "CBPerteneceA";
            this.CBPerteneceA.Size = new System.Drawing.Size(121, 28);
            this.CBPerteneceA.TabIndex = 11;
            // 
            // lblNota
            // 
            this.lblNota.AutoSize = true;
            this.lblNota.Location = new System.Drawing.Point(54, 301);
            this.lblNota.Name = "lblNota";
            this.lblNota.Size = new System.Drawing.Size(496, 20);
            this.lblNota.TabIndex = 13;
            this.lblNota.Text = "Al guardar un plan, este sera registrado en todos los mikrotiks activos";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(343, 86);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(133, 23);
            this.progressBar1.TabIndex = 14;
            // 
            // lblMensajeplan
            // 
            this.lblMensajeplan.AutoSize = true;
            this.lblMensajeplan.Location = new System.Drawing.Point(54, 333);
            this.lblMensajeplan.Name = "lblMensajeplan";
            this.lblMensajeplan.Size = new System.Drawing.Size(326, 20);
            this.lblMensajeplan.TabIndex = 15;
            this.lblMensajeplan.Text = "Cantidad de usuarios registrados con el plan:";
            // 
            // lblCantidadenPlan
            // 
            this.lblCantidadenPlan.AutoSize = true;
            this.lblCantidadenPlan.Location = new System.Drawing.Point(386, 333);
            this.lblCantidadenPlan.Name = "lblCantidadenPlan";
            this.lblCantidadenPlan.Size = new System.Drawing.Size(18, 20);
            this.lblCantidadenPlan.TabIndex = 16;
            this.lblCantidadenPlan.Text = "0";
            // 
            // Plan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 437);
            this.Controls.Add(this.lblCantidadenPlan);
            this.Controls.Add(this.lblMensajeplan);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblNota);
            this.Controls.Add(this.BtnGuardar);
            this.Controls.Add(this.CBPerteneceA);
            this.Controls.Add(this.lblPerteneceA);
            this.Controls.Add(this.CBDescarga);
            this.Controls.Add(this.NUDDescarga);
            this.Controls.Add(this.lblDescarga);
            this.Controls.Add(this.cbSubida);
            this.Controls.Add(this.NUDSubida);
            this.Controls.Add(this.lblSubida);
            this.Controls.Add(this.NUDPrecio);
            this.Controls.Add(this.lblPrecio);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblNombre);
            this.MaximizeBox = false;
            this.Name = "Plan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plan";
            this.Load += new System.EventHandler(this.Plan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NUDPrecio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDSubida)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDDescarga)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.NumericUpDown NUDPrecio;
        private System.Windows.Forms.Label lblSubida;
        private System.Windows.Forms.NumericUpDown NUDSubida;
        private System.Windows.Forms.ComboBox cbSubida;
        private System.Windows.Forms.Label lblDescarga;
        private System.Windows.Forms.NumericUpDown NUDDescarga;
        private System.Windows.Forms.ComboBox CBDescarga;
        private System.Windows.Forms.Button BtnGuardar;
        private System.Windows.Forms.Label lblPerteneceA;
        private System.Windows.Forms.ComboBox CBPerteneceA;
        private System.Windows.Forms.Label lblNota;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblMensajeplan;
        private System.Windows.Forms.Label lblCantidadenPlan;
    }
}