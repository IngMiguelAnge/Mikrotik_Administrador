namespace Mikrotik_Administrador.Catalogos
{
    partial class Bancos
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
            this.pnGeneral = new System.Windows.Forms.Panel();
            this.lblMisBancos = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.GBBancos = new System.Windows.Forms.GroupBox();
            this.dgvBancos = new System.Windows.Forms.DataGridView();
            this.Tipo = new System.Windows.Forms.Label();
            this.cbTipo = new System.Windows.Forms.ComboBox();
            this.pnGeneral.SuspendLayout();
            this.GBBancos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBancos)).BeginInit();
            this.SuspendLayout();
            // 
            // pnGeneral
            // 
            this.pnGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnGeneral.Controls.Add(this.cbTipo);
            this.pnGeneral.Controls.Add(this.Tipo);
            this.pnGeneral.Controls.Add(this.GBBancos);
            this.pnGeneral.Controls.Add(this.progressBar1);
            this.pnGeneral.Controls.Add(this.btnNuevo);
            this.pnGeneral.Controls.Add(this.btnBuscar);
            this.pnGeneral.Controls.Add(this.txtNombre);
            this.pnGeneral.Controls.Add(this.lblNombre);
            this.pnGeneral.Controls.Add(this.lblMisBancos);
            this.pnGeneral.Location = new System.Drawing.Point(12, 12);
            this.pnGeneral.Name = "pnGeneral";
            this.pnGeneral.Size = new System.Drawing.Size(885, 481);
            this.pnGeneral.TabIndex = 0;
            // 
            // lblMisBancos
            // 
            this.lblMisBancos.AutoSize = true;
            this.lblMisBancos.Location = new System.Drawing.Point(26, 23);
            this.lblMisBancos.Name = "lblMisBancos";
            this.lblMisBancos.Size = new System.Drawing.Size(123, 20);
            this.lblMisBancos.TabIndex = 0;
            this.lblMisBancos.Text = "Lista de Bancos";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(26, 75);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(69, 20);
            this.lblNombre.TabIndex = 1;
            this.lblNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(30, 110);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(209, 26);
            this.txtNombre.TabIndex = 2;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(470, 106);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(92, 34);
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Location = new System.Drawing.Point(591, 106);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(89, 32);
            this.btnNuevo.TabIndex = 4;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(110)))), ((int)(((byte)(203)))));
            this.progressBar1.Location = new System.Drawing.Point(30, 162);
            this.progressBar1.MarqueeAnimationSpeed = 0;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(520, 6);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 33;
            // 
            // GBBancos
            // 
            this.GBBancos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBBancos.Controls.Add(this.dgvBancos);
            this.GBBancos.Location = new System.Drawing.Point(30, 197);
            this.GBBancos.Name = "GBBancos";
            this.GBBancos.Size = new System.Drawing.Size(824, 270);
            this.GBBancos.TabIndex = 34;
            this.GBBancos.TabStop = false;
            this.GBBancos.Text = "Bancos";
            // 
            // dgvBancos
            // 
            this.dgvBancos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBancos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBancos.Location = new System.Drawing.Point(3, 22);
            this.dgvBancos.Name = "dgvBancos";
            this.dgvBancos.RowHeadersWidth = 62;
            this.dgvBancos.RowTemplate.Height = 28;
            this.dgvBancos.Size = new System.Drawing.Size(818, 245);
            this.dgvBancos.TabIndex = 0;
            this.dgvBancos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBancos_CellContentClick);
            // 
            // Tipo
            // 
            this.Tipo.AutoSize = true;
            this.Tipo.Location = new System.Drawing.Point(271, 75);
            this.Tipo.Name = "Tipo";
            this.Tipo.Size = new System.Drawing.Size(43, 20);
            this.Tipo.TabIndex = 35;
            this.Tipo.Text = "Tipo:";
            // 
            // cbTipo
            // 
            this.cbTipo.FormattingEnabled = true;
            this.cbTipo.Items.AddRange(new object[] {
            "Seleccione",
            "Fiscal",
            "Opcional"});
            this.cbTipo.Location = new System.Drawing.Point(275, 110);
            this.cbTipo.Name = "cbTipo";
            this.cbTipo.Size = new System.Drawing.Size(174, 28);
            this.cbTipo.TabIndex = 36;
            // 
            // Bancos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 505);
            this.Controls.Add(this.pnGeneral);
            this.MaximizeBox = false;
            this.Name = "Bancos";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Bancos_Load);
            this.pnGeneral.ResumeLayout(false);
            this.pnGeneral.PerformLayout();
            this.GBBancos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBancos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnGeneral;
        private System.Windows.Forms.Label lblMisBancos;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.GroupBox GBBancos;
        private System.Windows.Forms.DataGridView dgvBancos;
        private System.Windows.Forms.ComboBox cbTipo;
        private System.Windows.Forms.Label Tipo;
    }
}