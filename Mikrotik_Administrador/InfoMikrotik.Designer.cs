namespace Mikrotik_Administrador
{
    partial class InfoMikrotik
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoMikrotik));
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblMensaje1 = new System.Windows.Forms.Label();
            this.lblIP = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnProbar = new System.Windows.Forms.Button();
            this.lblProbar = new System.Windows.Forms.Label();
            this.lblMensaje2 = new System.Windows.Forms.Label();
            this.lblp1 = new System.Windows.Forms.Label();
            this.lblp2 = new System.Windows.Forms.Label();
            this.txtMensaje = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lblnombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtIpMin = new System.Windows.Forms.TextBox();
            this.txtIpMax = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(31, 37);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(173, 20);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "Información de Mikrotik";
            // 
            // lblMensaje1
            // 
            this.lblMensaje1.AutoSize = true;
            this.lblMensaje1.Location = new System.Drawing.Point(31, 105);
            this.lblMensaje1.Name = "lblMensaje1";
            this.lblMensaje1.Size = new System.Drawing.Size(219, 20);
            this.lblMensaje1.TabIndex = 1;
            this.lblMensaje1.Text = "Escriba los datos de conexión";
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(34, 135);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(34, 20);
            this.lblIP.TabIndex = 2;
            this.lblIP.Text = "*IP:";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(280, 135);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(48, 20);
            this.lblPort.TabIndex = 4;
            this.lblPort.Text = "*Port:";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(368, 135);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 26);
            this.txtPort.TabIndex = 2;
            this.txtPort.TextChanged += new System.EventHandler(this.txtPort_TextChanged);
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(34, 185);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(74, 20);
            this.lblUsuario.TabIndex = 6;
            this.lblUsuario.Text = "*Usuario:";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(118, 179);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(132, 26);
            this.txtUsuario.TabIndex = 3;
            this.txtUsuario.TextChanged += new System.EventHandler(this.txtUsuario_TextChanged);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(280, 185);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(88, 20);
            this.lblPassword.TabIndex = 8;
            this.lblPassword.Text = "*Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(368, 185);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(136, 26);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // btnProbar
            // 
            this.btnProbar.Location = new System.Drawing.Point(38, 225);
            this.btnProbar.Name = "btnProbar";
            this.btnProbar.Size = new System.Drawing.Size(150, 43);
            this.btnProbar.TabIndex = 5;
            this.btnProbar.Text = "Probar Conexión";
            this.btnProbar.UseVisualStyleBackColor = true;
            this.btnProbar.Click += new System.EventHandler(this.btnProbar_Click);
            // 
            // lblProbar
            // 
            this.lblProbar.AutoSize = true;
            this.lblProbar.Location = new System.Drawing.Point(214, 232);
            this.lblProbar.Name = "lblProbar";
            this.lblProbar.Size = new System.Drawing.Size(99, 20);
            this.lblProbar.TabIndex = 11;
            this.lblProbar.Text = "Sin conexión";
            // 
            // lblMensaje2
            // 
            this.lblMensaje2.AutoSize = true;
            this.lblMensaje2.Location = new System.Drawing.Point(31, 271);
            this.lblMensaje2.Name = "lblMensaje2";
            this.lblMensaje2.Size = new System.Drawing.Size(492, 20);
            this.lblMensaje2.TabIndex = 12;
            this.lblMensaje2.Text = "*Escriba la serie minima y maxima y maxima que alcanzara el mikrotik";
            // 
            // lblp1
            // 
            this.lblp1.AutoSize = true;
            this.lblp1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblp1.Location = new System.Drawing.Point(189, 324);
            this.lblp1.Name = "lblp1";
            this.lblp1.Size = new System.Drawing.Size(54, 25);
            this.lblp1.TabIndex = 23;
            this.lblp1.Text = ".    2";
            // 
            // lblp2
            // 
            this.lblp2.AutoSize = true;
            this.lblp2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblp2.Location = new System.Drawing.Point(450, 324);
            this.lblp2.Name = "lblp2";
            this.lblp2.Size = new System.Drawing.Size(78, 25);
            this.lblp2.TabIndex = 24;
            this.lblp2.Text = ".    254";
            // 
            // txtMensaje
            // 
            this.txtMensaje.Location = new System.Drawing.Point(35, 373);
            this.txtMensaje.Multiline = true;
            this.txtMensaje.Name = "txtMensaje";
            this.txtMensaje.ReadOnly = true;
            this.txtMensaje.Size = new System.Drawing.Size(488, 116);
            this.txtMensaje.TabIndex = 25;
            this.txtMensaje.Text = resources.GetString("txtMensaje.Text");
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(35, 499);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(87, 35);
            this.btnGuardar.TabIndex = 8;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // lblnombre
            // 
            this.lblnombre.AutoSize = true;
            this.lblnombre.Location = new System.Drawing.Point(34, 73);
            this.lblnombre.Name = "lblnombre";
            this.lblnombre.Size = new System.Drawing.Size(149, 20);
            this.lblnombre.TabIndex = 27;
            this.lblnombre.Text = "Nombre de mikrotik:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(202, 66);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(226, 26);
            this.txtNombre.TabIndex = 0;
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(118, 135);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(132, 26);
            this.txtIP.TabIndex = 1;
            this.txtIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIP_KeyPress);
            this.txtIP.Validating += new System.ComponentModel.CancelEventHandler(this.txtIP_Validating);
            // 
            // txtIpMin
            // 
            this.txtIpMin.Location = new System.Drawing.Point(38, 325);
            this.txtIpMin.Name = "txtIpMin";
            this.txtIpMin.Size = new System.Drawing.Size(150, 26);
            this.txtIpMin.TabIndex = 6;
            this.txtIpMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIpMin_KeyPress);
            this.txtIpMin.Validating += new System.ComponentModel.CancelEventHandler(this.txtIpMin_Validating);
            // 
            // txtIpMax
            // 
            this.txtIpMax.Location = new System.Drawing.Point(284, 325);
            this.txtIpMax.Name = "txtIpMax";
            this.txtIpMax.Size = new System.Drawing.Size(160, 26);
            this.txtIpMax.TabIndex = 7;
            this.txtIpMax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIpMax_KeyPress);
            this.txtIpMax.Validating += new System.ComponentModel.CancelEventHandler(this.txtIpMax_Validating);
            // 
            // InfoMikrotik
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 544);
            this.Controls.Add(this.txtIpMax);
            this.Controls.Add(this.txtIpMin);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblnombre);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtMensaje);
            this.Controls.Add(this.lblp2);
            this.Controls.Add(this.lblp1);
            this.Controls.Add(this.lblMensaje2);
            this.Controls.Add(this.lblProbar);
            this.Controls.Add(this.btnProbar);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.lblIP);
            this.Controls.Add(this.lblMensaje1);
            this.Controls.Add(this.lblInfo);
            this.MaximizeBox = false;
            this.Name = "InfoMikrotik";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InfoMikrotik";
            this.Load += new System.EventHandler(this.InfoMikrotik_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblMensaje1;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnProbar;
        private System.Windows.Forms.Label lblProbar;
        private System.Windows.Forms.Label lblMensaje2;
        private System.Windows.Forms.Label lblp1;
        private System.Windows.Forms.Label lblp2;
        private System.Windows.Forms.TextBox txtMensaje;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblnombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtIpMin;
        private System.Windows.Forms.TextBox txtIpMax;
    }
}