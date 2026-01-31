namespace Mikrotik_Administrador
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtEthernet = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtUsuario1 = new System.Windows.Forms.TextBox();
            this.txtPassword1 = new System.Windows.Forms.TextBox();
            this.BtnLogin1 = new System.Windows.Forms.Button();
            this.lblConexion = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.BtnLogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtEthernet
            // 
            this.txtEthernet.Location = new System.Drawing.Point(419, 77);
            this.txtEthernet.Name = "txtEthernet";
            this.txtEthernet.Size = new System.Drawing.Size(100, 26);
            this.txtEthernet.TabIndex = 0;
            this.txtEthernet.Text = "10.19.0.1";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(578, 77);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 26);
            this.txtPort.TabIndex = 1;
            this.txtPort.Text = "8728";
            // 
            // txtUsuario1
            // 
            this.txtUsuario1.Location = new System.Drawing.Point(434, 133);
            this.txtUsuario1.Name = "txtUsuario1";
            this.txtUsuario1.Size = new System.Drawing.Size(100, 26);
            this.txtUsuario1.TabIndex = 2;
            this.txtUsuario1.Text = "admin";
            // 
            // txtPassword1
            // 
            this.txtPassword1.Location = new System.Drawing.Point(587, 133);
            this.txtPassword1.Name = "txtPassword1";
            this.txtPassword1.Size = new System.Drawing.Size(100, 26);
            this.txtPassword1.TabIndex = 3;
            this.txtPassword1.Text = "1234";
            // 
            // BtnLogin1
            // 
            this.BtnLogin1.Location = new System.Drawing.Point(434, 202);
            this.BtnLogin1.Name = "BtnLogin1";
            this.BtnLogin1.Size = new System.Drawing.Size(100, 40);
            this.BtnLogin1.TabIndex = 4;
            this.BtnLogin1.Text = "Login";
            this.BtnLogin1.UseVisualStyleBackColor = true;
            this.BtnLogin1.Click += new System.EventHandler(this.BtnLogin1_Click);
            // 
            // lblConexion
            // 
            this.lblConexion.AutoSize = true;
            this.lblConexion.Location = new System.Drawing.Point(637, 202);
            this.lblConexion.Name = "lblConexion";
            this.lblConexion.Size = new System.Drawing.Size(51, 20);
            this.lblConexion.TabIndex = 5;
            this.lblConexion.Text = "label1";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(65, 49);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(76, 20);
            this.lblUsuario.TabIndex = 6;
            this.lblUsuario.Text = "Ususario:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(65, 102);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(82, 20);
            this.lblPassword.TabIndex = 7;
            this.lblPassword.Text = "Password:";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(147, 43);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(152, 26);
            this.txtUser.TabIndex = 8;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(153, 96);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(146, 26);
            this.txtPassword.TabIndex = 9;
            // 
            // BtnLogin
            // 
            this.BtnLogin.Location = new System.Drawing.Point(69, 159);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(75, 29);
            this.BtnLogin.TabIndex = 10;
            this.BtnLogin.Text = "Login";
            this.BtnLogin.UseVisualStyleBackColor = true;
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BtnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.lblConexion);
            this.Controls.Add(this.BtnLogin1);
            this.Controls.Add(this.txtPassword1);
            this.Controls.Add(this.txtUsuario1);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtEthernet);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtEthernet;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtUsuario1;
        private System.Windows.Forms.TextBox txtPassword1;
        private System.Windows.Forms.Button BtnLogin1;
        private System.Windows.Forms.Label lblConexion;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button BtnLogin;
    }
}

