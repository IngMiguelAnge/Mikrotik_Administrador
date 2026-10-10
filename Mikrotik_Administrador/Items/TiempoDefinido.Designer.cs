namespace Mikrotik_Administrador.Items
{
    partial class TiempoDefinido
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
            this.lblTiempo = new System.Windows.Forms.Label();
            this.lblDias = new System.Windows.Forms.Label();
            this.lblHoras = new System.Windows.Forms.Label();
            this.NUDDias = new System.Windows.Forms.NumericUpDown();
            this.NUDHoras = new System.Windows.Forms.NumericUpDown();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lblFechaInicio = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.lblFechaFinal = new System.Windows.Forms.Label();
            this.CBModo = new System.Windows.Forms.ComboBox();
            this.lblModo = new System.Windows.Forms.Label();
            this.lblMikrotikNuevo = new System.Windows.Forms.Label();
            this.CBMikrotiksNuevo = new System.Windows.Forms.ComboBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblFechaFin = new System.Windows.Forms.Label();
            this.cbPlanNuevo = new System.Windows.Forms.ComboBox();
            this.lblPlanNuevo = new System.Windows.Forms.Label();
            this.CBPlanOriginal = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CBMikrotiksOriginal = new System.Windows.Forms.ComboBox();
            this.lblMikrotikOrigen = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NUDDias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDHoras)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTiempo
            // 
            this.lblTiempo.AutoSize = true;
            this.lblTiempo.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblTiempo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblTiempo.Location = new System.Drawing.Point(228, 108);
            this.lblTiempo.Name = "lblTiempo";
            this.lblTiempo.Size = new System.Drawing.Size(275, 28);
            this.lblTiempo.TabIndex = 4;
            this.lblTiempo.Text = "Tiempo que desea que dure:";
            // 
            // lblDias
            // 
            this.lblDias.AutoSize = true;
            this.lblDias.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDias.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblDias.Location = new System.Drawing.Point(228, 163);
            this.lblDias.Name = "lblDias";
            this.lblDias.Size = new System.Drawing.Size(52, 25);
            this.lblDias.TabIndex = 5;
            this.lblDias.Text = "DÍAS";
            // 
            // lblHoras
            // 
            this.lblHoras.AutoSize = true;
            this.lblHoras.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblHoras.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblHoras.Location = new System.Drawing.Point(375, 163);
            this.lblHoras.Name = "lblHoras";
            this.lblHoras.Size = new System.Drawing.Size(72, 25);
            this.lblHoras.TabIndex = 7;
            this.lblHoras.Text = "HORAS";
            // 
            // NUDDias
            // 
            this.NUDDias.Enabled = false;
            this.NUDDias.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.NUDDias.Location = new System.Drawing.Point(286, 155);
            this.NUDDias.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.NUDDias.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUDDias.Name = "NUDDias";
            this.NUDDias.Size = new System.Drawing.Size(67, 33);
            this.NUDDias.TabIndex = 6;
            this.NUDDias.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.NUDDias.ValueChanged += new System.EventHandler(this.NUDDias_ValueChanged);
            // 
            // NUDHoras
            // 
            this.NUDHoras.Enabled = false;
            this.NUDHoras.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.NUDHoras.Location = new System.Drawing.Point(453, 155);
            this.NUDHoras.Name = "NUDHoras";
            this.NUDHoras.Size = new System.Drawing.Size(68, 33);
            this.NUDHoras.TabIndex = 8;
            this.NUDHoras.ValueChanged += new System.EventHandler(this.NUDHoras_ValueChanged);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(680, 495);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(120, 38);
            this.btnGuardar.TabIndex = 6;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // lblFechaInicio
            // 
            this.lblFechaInicio.AutoSize = true;
            this.lblFechaInicio.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblFechaInicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblFechaInicio.Location = new System.Drawing.Point(40, 108);
            this.lblFechaInicio.Name = "lblFechaInicio";
            this.lblFechaInicio.Size = new System.Drawing.Size(156, 28);
            this.lblFechaInicio.TabIndex = 2;
            this.lblFechaInicio.Text = "Cuando iniciara:";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaInicio.Enabled = false;
            this.dtpFechaInicio.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(45, 151);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(152, 33);
            this.dtpFechaInicio.TabIndex = 3;
            this.dtpFechaInicio.ValueChanged += new System.EventHandler(this.dtpFechaInicio_ValueChanged);
            // 
            // lblFechaFinal
            // 
            this.lblFechaFinal.AutoSize = true;
            this.lblFechaFinal.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblFechaFinal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblFechaFinal.Location = new System.Drawing.Point(40, 211);
            this.lblFechaFinal.Name = "lblFechaFinal";
            this.lblFechaFinal.Size = new System.Drawing.Size(244, 28);
            this.lblFechaFinal.TabIndex = 9;
            this.lblFechaFinal.Text = "Este cambio durara hasta:";
            // 
            // CBModo
            // 
            this.CBModo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBModo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.CBModo.FormattingEnabled = true;
            this.CBModo.Items.AddRange(new object[] {
            "Seleccione",
            "Test",
            "Temporal"});
            this.CBModo.Location = new System.Drawing.Point(44, 65);
            this.CBModo.Name = "CBModo";
            this.CBModo.Size = new System.Drawing.Size(153, 33);
            this.CBModo.TabIndex = 1;
            this.CBModo.SelectedIndexChanged += new System.EventHandler(this.CBModo_SelectedIndexChanged);
            // 
            // lblModo
            // 
            this.lblModo.AutoSize = true;
            this.lblModo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblModo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblModo.Location = new System.Drawing.Point(40, 23);
            this.lblModo.Name = "lblModo";
            this.lblModo.Size = new System.Drawing.Size(138, 25);
            this.lblModo.TabIndex = 0;
            this.lblModo.Text = "Aplicar cambio:";
            // 
            // lblMikrotikNuevo
            // 
            this.lblMikrotikNuevo.AutoSize = true;
            this.lblMikrotikNuevo.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblMikrotikNuevo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblMikrotikNuevo.Location = new System.Drawing.Point(40, 367);
            this.lblMikrotikNuevo.Name = "lblMikrotikNuevo";
            this.lblMikrotikNuevo.Size = new System.Drawing.Size(303, 28);
            this.lblMikrotikNuevo.TabIndex = 13;
            this.lblMikrotikNuevo.Text = "En que mikrotik aplicara el plan:";
            // 
            // CBMikrotiksNuevo
            // 
            this.CBMikrotiksNuevo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBMikrotiksNuevo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.CBMikrotiksNuevo.FormattingEnabled = true;
            this.CBMikrotiksNuevo.Location = new System.Drawing.Point(44, 415);
            this.CBMikrotiksNuevo.Name = "CBMikrotiksNuevo";
            this.CBMikrotiksNuevo.Size = new System.Drawing.Size(285, 33);
            this.CBMikrotiksNuevo.TabIndex = 14;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(251, 86);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(235, 12);
            this.progressBar1.TabIndex = 15;
            // 
            // lblFechaFin
            // 
            this.lblFechaFin.AutoSize = true;
            this.lblFechaFin.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblFechaFin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblFechaFin.Location = new System.Drawing.Point(290, 211);
            this.lblFechaFin.Name = "lblFechaFin";
            this.lblFechaFin.Size = new System.Drawing.Size(186, 28);
            this.lblFechaFin.TabIndex = 10;
            this.lblFechaFin.Text = "Fecha que termina:";
            // 
            // cbPlanNuevo
            // 
            this.cbPlanNuevo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlanNuevo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cbPlanNuevo.FormattingEnabled = true;
            this.cbPlanNuevo.Location = new System.Drawing.Point(44, 315);
            this.cbPlanNuevo.Name = "cbPlanNuevo";
            this.cbPlanNuevo.Size = new System.Drawing.Size(285, 33);
            this.cbPlanNuevo.TabIndex = 17;
            this.cbPlanNuevo.SelectedIndexChanged += new System.EventHandler(this.cbPlanNuevo_SelectedIndexChanged);
            // 
            // lblPlanNuevo
            // 
            this.lblPlanNuevo.AutoSize = true;
            this.lblPlanNuevo.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblPlanNuevo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblPlanNuevo.Location = new System.Drawing.Point(39, 274);
            this.lblPlanNuevo.Name = "lblPlanNuevo";
            this.lblPlanNuevo.Size = new System.Drawing.Size(353, 28);
            this.lblPlanNuevo.TabIndex = 16;
            this.lblPlanNuevo.Text = "Plan que se usara durante el periodo:";
            // 
            // CBPlanOriginal
            // 
            this.CBPlanOriginal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBPlanOriginal.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.CBPlanOriginal.FormattingEnabled = true;
            this.CBPlanOriginal.Location = new System.Drawing.Point(435, 315);
            this.CBPlanOriginal.Name = "CBPlanOriginal";
            this.CBPlanOriginal.Size = new System.Drawing.Size(285, 33);
            this.CBPlanOriginal.TabIndex = 21;
            this.CBPlanOriginal.SelectedIndexChanged += new System.EventHandler(this.CBPlanOriginal_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.label1.Location = new System.Drawing.Point(430, 274);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(370, 28);
            this.label1.TabIndex = 20;
            this.label1.Text = "Plan que se usara despues del periodo:";
            // 
            // CBMikrotiksOriginal
            // 
            this.CBMikrotiksOriginal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBMikrotiksOriginal.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.CBMikrotiksOriginal.FormattingEnabled = true;
            this.CBMikrotiksOriginal.Location = new System.Drawing.Point(435, 410);
            this.CBMikrotiksOriginal.Name = "CBMikrotiksOriginal";
            this.CBMikrotiksOriginal.Size = new System.Drawing.Size(285, 33);
            this.CBMikrotiksOriginal.TabIndex = 19;
            // 
            // lblMikrotikOrigen
            // 
            this.lblMikrotikOrigen.AutoSize = true;
            this.lblMikrotikOrigen.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblMikrotikOrigen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblMikrotikOrigen.Location = new System.Drawing.Point(430, 367);
            this.lblMikrotikOrigen.Name = "lblMikrotikOrigen";
            this.lblMikrotikOrigen.Size = new System.Drawing.Size(303, 28);
            this.lblMikrotikOrigen.TabIndex = 18;
            this.lblMikrotikOrigen.Text = "En que mikrotik aplicara el plan:";
            // 
            // TiempoDefinido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(830, 547);
            this.Controls.Add(this.CBPlanOriginal);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CBMikrotiksOriginal);
            this.Controls.Add(this.lblMikrotikOrigen);
            this.Controls.Add(this.cbPlanNuevo);
            this.Controls.Add(this.lblPlanNuevo);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.CBMikrotiksNuevo);
            this.Controls.Add(this.lblMikrotikNuevo);
            this.Controls.Add(this.CBModo);
            this.Controls.Add(this.lblModo);
            this.Controls.Add(this.lblFechaFin);
            this.Controls.Add(this.lblFechaFinal);
            this.Controls.Add(this.dtpFechaInicio);
            this.Controls.Add(this.lblFechaInicio);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.NUDHoras);
            this.Controls.Add(this.NUDDias);
            this.Controls.Add(this.lblHoras);
            this.Controls.Add(this.lblDias);
            this.Controls.Add(this.lblTiempo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TiempoDefinido";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.TiempoDefinido_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NUDDias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDHoras)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Label lblTiempo;
        private System.Windows.Forms.Label lblDias;
        private System.Windows.Forms.Label lblHoras;
        private System.Windows.Forms.NumericUpDown NUDDias;
        private System.Windows.Forms.NumericUpDown NUDHoras;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblFechaInicio;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Label lblFechaFinal;
        private System.Windows.Forms.ComboBox CBModo;
        private System.Windows.Forms.Label lblModo;
        private System.Windows.Forms.Label lblMikrotikNuevo;
        private System.Windows.Forms.ComboBox CBMikrotiksNuevo;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblFechaFin;
        private System.Windows.Forms.ComboBox cbPlanNuevo;
        private System.Windows.Forms.Label lblPlanNuevo;
        private System.Windows.Forms.ComboBox CBPlanOriginal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CBMikrotiksOriginal;
        private System.Windows.Forms.Label lblMikrotikOrigen;
    }
}