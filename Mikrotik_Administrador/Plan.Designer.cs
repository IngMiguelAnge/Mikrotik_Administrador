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
            this.lblNota2 = new System.Windows.Forms.Label();
            this.CBSinCosto = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.NUDPrecio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDSubida)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDDescarga)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblNombre.Location = new System.Drawing.Point(40, 30);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(93, 25);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "*Nombre:";
            // 
            // txtNombre
            // 
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtNombre.Location = new System.Drawing.Point(44, 58);
            this.txtNombre.MaxLength = 250;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(320, 33);
            this.txtNombre.TabIndex = 1;
            // 
            // lblPrecio
            // 
            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrecio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblPrecio.Location = new System.Drawing.Point(40, 110);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(76, 25);
            this.lblPrecio.TabIndex = 2;
            this.lblPrecio.Text = "*Precio:";
            // 
            // NUDPrecio
            // 
            this.NUDPrecio.DecimalPlaces = 2;
            this.NUDPrecio.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.NUDPrecio.Location = new System.Drawing.Point(44, 138);
            this.NUDPrecio.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.NUDPrecio.Name = "NUDPrecio";
            this.NUDPrecio.Size = new System.Drawing.Size(140, 33);
            this.NUDPrecio.TabIndex = 3;
            // 
            // lblSubida
            // 
            this.lblSubida.AutoSize = true;
            this.lblSubida.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSubida.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblSubida.Location = new System.Drawing.Point(40, 190);
            this.lblSubida.Name = "lblSubida";
            this.lblSubida.Size = new System.Drawing.Size(167, 25);
            this.lblSubida.TabIndex = 4;
            this.lblSubida.Text = "*Velocidad Subida:";
            // 
            // NUDSubida
            // 
            this.NUDSubida.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.NUDSubida.Location = new System.Drawing.Point(44, 218);
            this.NUDSubida.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.NUDSubida.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUDSubida.Name = "NUDSubida";
            this.NUDSubida.Size = new System.Drawing.Size(140, 33);
            this.NUDSubida.TabIndex = 5;
            this.NUDSubida.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbSubida
            // 
            this.cbSubida.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSubida.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cbSubida.FormattingEnabled = true;
            this.cbSubida.Items.AddRange(new object[] {
            "Seleccione",
            "M",
            "k"});
            this.cbSubida.Location = new System.Drawing.Point(195, 218);
            this.cbSubida.Name = "cbSubida";
            this.cbSubida.Size = new System.Drawing.Size(130, 33);
            this.cbSubida.TabIndex = 6;
            // 
            // lblDescarga
            // 
            this.lblDescarga.AutoSize = true;
            this.lblDescarga.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescarga.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblDescarga.Location = new System.Drawing.Point(40, 270);
            this.lblDescarga.Name = "lblDescarga";
            this.lblDescarga.Size = new System.Drawing.Size(185, 25);
            this.lblDescarga.TabIndex = 7;
            this.lblDescarga.Text = "*Velocidad Descarga:";
            // 
            // NUDDescarga
            // 
            this.NUDDescarga.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.NUDDescarga.Location = new System.Drawing.Point(44, 298);
            this.NUDDescarga.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.NUDDescarga.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUDDescarga.Name = "NUDDescarga";
            this.NUDDescarga.Size = new System.Drawing.Size(140, 33);
            this.NUDDescarga.TabIndex = 8;
            this.NUDDescarga.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // CBDescarga
            // 
            this.CBDescarga.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBDescarga.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.CBDescarga.FormattingEnabled = true;
            this.CBDescarga.Items.AddRange(new object[] {
            "Seleccione",
            "M",
            "k"});
            this.CBDescarga.Location = new System.Drawing.Point(195, 298);
            this.CBDescarga.Name = "CBDescarga";
            this.CBDescarga.Size = new System.Drawing.Size(130, 33);
            this.CBDescarga.TabIndex = 9;
            // 
            // BtnGuardar
            // 
            this.BtnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.BtnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnGuardar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.BtnGuardar.ForeColor = System.Drawing.Color.White;
            this.BtnGuardar.Location = new System.Drawing.Point(45, 534);
            this.BtnGuardar.Name = "BtnGuardar";
            this.BtnGuardar.Size = new System.Drawing.Size(120, 38);
            this.BtnGuardar.TabIndex = 12;
            this.BtnGuardar.Text = "Guardar";
            this.BtnGuardar.UseVisualStyleBackColor = false;
            this.BtnGuardar.Click += new System.EventHandler(this.BtnGuardar_Click);
            // 
            // lblPerteneceA
            // 
            this.lblPerteneceA.AutoSize = true;
            this.lblPerteneceA.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPerteneceA.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblPerteneceA.Location = new System.Drawing.Point(40, 350);
            this.lblPerteneceA.Name = "lblPerteneceA";
            this.lblPerteneceA.Size = new System.Drawing.Size(125, 25);
            this.lblPerteneceA.TabIndex = 10;
            this.lblPerteneceA.Text = "*Pertenece A:";
            // 
            // CBPerteneceA
            // 
            this.CBPerteneceA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBPerteneceA.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.CBPerteneceA.FormattingEnabled = true;
            this.CBPerteneceA.Items.AddRange(new object[] {
            "Seleccione",
            "Antena",
            "Fibra"});
            this.CBPerteneceA.Location = new System.Drawing.Point(44, 378);
            this.CBPerteneceA.Name = "CBPerteneceA";
            this.CBPerteneceA.Size = new System.Drawing.Size(200, 33);
            this.CBPerteneceA.TabIndex = 11;
            // 
            // lblNota
            // 
            this.lblNota.AutoSize = true;
            this.lblNota.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.lblNota.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.lblNota.Location = new System.Drawing.Point(40, 425);
            this.lblNota.Name = "lblNota";
            this.lblNota.Size = new System.Drawing.Size(304, 20);
            this.lblNota.TabIndex = 13;
            this.lblNota.Text = "Al guardar un plan, este será registrado";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(186, 534);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(235, 12);
            this.progressBar1.TabIndex = 14;
            // 
            // lblMensajeplan
            // 
            this.lblMensajeplan.AutoSize = true;
            this.lblMensajeplan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMensajeplan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblMensajeplan.Location = new System.Drawing.Point(39, 484);
            this.lblMensajeplan.Name = "lblMensajeplan";
            this.lblMensajeplan.Size = new System.Drawing.Size(368, 25);
            this.lblMensajeplan.TabIndex = 15;
            this.lblMensajeplan.Text = "Cantidad de usuarios registrados con el plan:";
            // 
            // lblCantidadenPlan
            // 
            this.lblCantidadenPlan.AutoSize = true;
            this.lblCantidadenPlan.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblCantidadenPlan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(80)))), ((int)(((byte)(196)))));
            this.lblCantidadenPlan.Location = new System.Drawing.Point(422, 484);
            this.lblCantidadenPlan.Name = "lblCantidadenPlan";
            this.lblCantidadenPlan.Size = new System.Drawing.Size(23, 25);
            this.lblCantidadenPlan.TabIndex = 16;
            this.lblCantidadenPlan.Text = "0";
            // 
            // lblNota2
            // 
            this.lblNota2.AutoSize = true;
            this.lblNota2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.lblNota2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.lblNota2.Location = new System.Drawing.Point(40, 448);
            this.lblNota2.Name = "lblNota2";
            this.lblNota2.Size = new System.Drawing.Size(458, 20);
            this.lblNota2.TabIndex = 17;
            this.lblNota2.Text = "en todos los mikrotiks activos y que tengan el plan permitido";
            // 
            // CBSinCosto
            // 
            this.CBSinCosto.AutoSize = true;
            this.CBSinCosto.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CBSinCosto.Location = new System.Drawing.Point(205, 140);
            this.CBSinCosto.Name = "CBSinCosto";
            this.CBSinCosto.Size = new System.Drawing.Size(154, 29);
            this.CBSinCosto.TabIndex = 18;
            this.CBSinCosto.Text = "No tiene costo";
            this.CBSinCosto.UseVisualStyleBackColor = true;
            this.CBSinCosto.CheckedChanged += new System.EventHandler(this.CBSinCosto_CheckedChanged);
            // 
            // Plan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(524, 584);
            this.Controls.Add(this.CBSinCosto);
            this.Controls.Add(this.lblNota2);
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
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Plan";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
        private System.Windows.Forms.Label lblNota2;
        private System.Windows.Forms.CheckBox CBSinCosto;
    }
}