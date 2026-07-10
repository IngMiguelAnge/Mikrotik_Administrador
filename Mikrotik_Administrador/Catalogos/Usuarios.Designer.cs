namespace Mikrotik_Administrador
{
    partial class Usuarios
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.CBTodosMikrotiks = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.CBMikrotiks = new System.Windows.Forms.ComboBox();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.BtnBuscar = new System.Windows.Forms.Button();
            this.lblMensaje2 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.lblMensaje3 = new System.Windows.Forms.Label();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.CBAsignar = new System.Windows.Forms.CheckBox();
            this.BtnAsignar = new System.Windows.Forms.Button();
            this.lblMensaje4 = new System.Windows.Forms.Label();
            this.btnClientesSin = new System.Windows.Forms.Button();
            this.cbTodos = new System.Windows.Forms.CheckBox();
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.btnPlan = new System.Windows.Forms.Button();
            this.lblServiciossin = new System.Windows.Forms.Label();
            this.btnServiciosSin = new System.Windows.Forms.Button();
            this.BtnEliminar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.panelContenedor.SuspendLayout();
            this.SuspendLayout();
            // 
            // CBTodosMikrotiks
            // 
            this.CBTodosMikrotiks.AutoSize = true;
            this.CBTodosMikrotiks.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CBTodosMikrotiks.Location = new System.Drawing.Point(260, 59);
            this.CBTodosMikrotiks.Name = "CBTodosMikrotiks";
            this.CBTodosMikrotiks.Size = new System.Drawing.Size(287, 29);
            this.CBTodosMikrotiks.TabIndex = 8;
            this.CBTodosMikrotiks.Text = "¿Buscar en todos los Mikrotiks?";
            this.CBTodosMikrotiks.UseVisualStyleBackColor = true;
            this.CBTodosMikrotiks.CheckedChanged += new System.EventHandler(this.CBTodosMikrotiks_CheckedChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 283);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(235, 12);
            this.progressBar1.TabIndex = 10;
            // 
            // CBMikrotiks
            // 
            this.CBMikrotiks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBMikrotiks.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.CBMikrotiks.FormattingEnabled = true;
            this.CBMikrotiks.Location = new System.Drawing.Point(25, 59);
            this.CBMikrotiks.Name = "CBMikrotiks";
            this.CBMikrotiks.Size = new System.Drawing.Size(200, 33);
            this.CBMikrotiks.TabIndex = 7;
            this.CBMikrotiks.SelectedIndexChanged += new System.EventHandler(this.CBMikrotiks_SelectedIndexChanged);
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblMensaje.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblMensaje.Location = new System.Drawing.Point(20, 21);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(132, 25);
            this.lblMensaje.TabIndex = 9;
            this.lblMensaje.Text = "Mikrotik a ver:";
            // 
            // BtnBuscar
            // 
            this.BtnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(233)))), ((int)(((byte)(240)))));
            this.BtnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBuscar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.BtnBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.BtnBuscar.Location = new System.Drawing.Point(295, 126);
            this.BtnBuscar.Name = "BtnBuscar";
            this.BtnBuscar.Size = new System.Drawing.Size(95, 35);
            this.BtnBuscar.TabIndex = 13;
            this.BtnBuscar.Text = "Buscar";
            this.BtnBuscar.UseVisualStyleBackColor = false;
            this.BtnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);
            // 
            // lblMensaje2
            // 
            this.lblMensaje2.AutoSize = true;
            this.lblMensaje2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblMensaje2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblMensaje2.Location = new System.Drawing.Point(20, 95);
            this.lblMensaje2.Name = "lblMensaje2";
            this.lblMensaje2.Size = new System.Drawing.Size(336, 25);
            this.lblMensaje2.TabIndex = 11;
            this.lblMensaje2.Text = "Escriba el nombre del usuario a buscar:";
            // 
            // txtNombre
            // 
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtNombre.Location = new System.Drawing.Point(24, 127);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(255, 33);
            this.txtNombre.TabIndex = 12;
            // 
            // dgvUsuarios
            // 
            this.dgvUsuarios.AllowUserToAddRows = false;
            this.dgvUsuarios.BackgroundColor = System.Drawing.Color.White;
            this.dgvUsuarios.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvUsuarios.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvUsuarios.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUsuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUsuarios.ColumnHeadersHeight = 35;
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(242)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUsuarios.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvUsuarios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsuarios.EnableHeadersVisualStyles = false;
            this.dgvUsuarios.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvUsuarios.Location = new System.Drawing.Point(20, 20);
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.RowHeadersVisible = false;
            this.dgvUsuarios.RowHeadersWidth = 51;
            this.dgvUsuarios.RowTemplate.Height = 30;
            this.dgvUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsuarios.Size = new System.Drawing.Size(965, 284);
            this.dgvUsuarios.TabIndex = 14;
            this.dgvUsuarios.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsuarios_CellContentClick);
            this.dgvUsuarios.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvUsuarios_ColumnHeaderMouseClick);
            // 
            // lblMensaje3
            // 
            this.lblMensaje3.AutoSize = true;
            this.lblMensaje3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblMensaje3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblMensaje3.Location = new System.Drawing.Point(20, 175);
            this.lblMensaje3.Name = "lblMensaje3";
            this.lblMensaje3.Size = new System.Drawing.Size(256, 25);
            this.lblMensaje3.TabIndex = 17;
            this.lblMensaje3.Text = "Escriba el nombre del cliente:";
            // 
            // txtCliente
            // 
            this.txtCliente.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtCliente.Location = new System.Drawing.Point(24, 204);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(255, 33);
            this.txtCliente.TabIndex = 18;
            // 
            // CBAsignar
            // 
            this.CBAsignar.AutoSize = true;
            this.CBAsignar.Checked = true;
            this.CBAsignar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CBAsignar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CBAsignar.Location = new System.Drawing.Point(596, 218);
            this.CBAsignar.Name = "CBAsignar";
            this.CBAsignar.Size = new System.Drawing.Size(221, 29);
            this.CBAsignar.TabIndex = 19;
            this.CBAsignar.Text = "¿Clientes Automáticos?";
            this.CBAsignar.UseVisualStyleBackColor = true;
            // 
            // BtnAsignar
            // 
            this.BtnAsignar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(80)))), ((int)(((byte)(196)))));
            this.BtnAsignar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAsignar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.BtnAsignar.ForeColor = System.Drawing.Color.White;
            this.BtnAsignar.Location = new System.Drawing.Point(596, 253);
            this.BtnAsignar.Name = "BtnAsignar";
            this.BtnAsignar.Size = new System.Drawing.Size(176, 42);
            this.BtnAsignar.TabIndex = 20;
            this.BtnAsignar.Text = "Asignar cliente";
            this.BtnAsignar.UseVisualStyleBackColor = false;
            this.BtnAsignar.Click += new System.EventHandler(this.BtnAsignar_Click);
            // 
            // lblMensaje4
            // 
            this.lblMensaje4.AutoSize = true;
            this.lblMensaje4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblMensaje4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblMensaje4.Location = new System.Drawing.Point(591, 59);
            this.lblMensaje4.Name = "lblMensaje4";
            this.lblMensaje4.Size = new System.Drawing.Size(204, 25);
            this.lblMensaje4.TabIndex = 21;
            this.lblMensaje4.Text = "Clientes sin servicios: 0";
            // 
            // btnClientesSin
            // 
            this.btnClientesSin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(233)))), ((int)(((byte)(240)))));
            this.btnClientesSin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClientesSin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClientesSin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnClientesSin.Location = new System.Drawing.Point(596, 90);
            this.btnClientesSin.Name = "btnClientesSin";
            this.btnClientesSin.Size = new System.Drawing.Size(241, 35);
            this.btnClientesSin.TabIndex = 22;
            this.btnClientesSin.Text = "Ver Clientes sin servicios";
            this.btnClientesSin.UseVisualStyleBackColor = false;
            this.btnClientesSin.Click += new System.EventHandler(this.btnClientesSin_Click);
            // 
            // cbTodos
            // 
            this.cbTodos.AutoSize = true;
            this.cbTodos.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbTodos.Location = new System.Drawing.Point(314, 266);
            this.cbTodos.Name = "cbTodos";
            this.cbTodos.Size = new System.Drawing.Size(178, 29);
            this.cbTodos.TabIndex = 23;
            this.cbTodos.Text = "Seleccionar todos";
            this.cbTodos.UseVisualStyleBackColor = true;
            this.cbTodos.CheckedChanged += new System.EventHandler(this.cbTodos_CheckedChanged);
            // 
            // panelContenedor
            // 
            this.panelContenedor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContenedor.Controls.Add(this.dgvUsuarios);
            this.panelContenedor.Location = new System.Drawing.Point(18, 301);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Padding = new System.Windows.Forms.Padding(20);
            this.panelContenedor.Size = new System.Drawing.Size(1005, 324);
            this.panelContenedor.TabIndex = 24;
            // 
            // btnPlan
            // 
            this.btnPlan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(80)))), ((int)(((byte)(196)))));
            this.btnPlan.Enabled = false;
            this.btnPlan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlan.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnPlan.ForeColor = System.Drawing.Color.White;
            this.btnPlan.Location = new System.Drawing.Point(868, 86);
            this.btnPlan.Name = "btnPlan";
            this.btnPlan.Size = new System.Drawing.Size(160, 42);
            this.btnPlan.TabIndex = 25;
            this.btnPlan.Text = "Cambiar Plan";
            this.btnPlan.UseVisualStyleBackColor = false;
            this.btnPlan.Visible = false;
            this.btnPlan.Click += new System.EventHandler(this.btnPlan_Click);
            // 
            // lblServiciossin
            // 
            this.lblServiciossin.AutoSize = true;
            this.lblServiciossin.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblServiciossin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(115)))), ((int)(((byte)(126)))));
            this.lblServiciossin.Location = new System.Drawing.Point(591, 137);
            this.lblServiciossin.Name = "lblServiciossin";
            this.lblServiciossin.Size = new System.Drawing.Size(203, 25);
            this.lblServiciossin.TabIndex = 26;
            this.lblServiciossin.Text = "Servicios sin clientes: 0";
            // 
            // btnServiciosSin
            // 
            this.btnServiciosSin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(233)))), ((int)(((byte)(240)))));
            this.btnServiciosSin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnServiciosSin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnServiciosSin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnServiciosSin.Location = new System.Drawing.Point(596, 165);
            this.btnServiciosSin.Name = "btnServiciosSin";
            this.btnServiciosSin.Size = new System.Drawing.Size(241, 35);
            this.btnServiciosSin.TabIndex = 27;
            this.btnServiciosSin.Text = "Ver Servicios sin Clientes";
            this.btnServiciosSin.UseVisualStyleBackColor = false;
            this.btnServiciosSin.Click += new System.EventHandler(this.btnServiciosSin_Click);
            // 
            // BtnEliminar
            // 
            this.BtnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.BtnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnEliminar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.BtnEliminar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.BtnEliminar.Location = new System.Drawing.Point(923, 12);
            this.BtnEliminar.Name = "BtnEliminar";
            this.BtnEliminar.Size = new System.Drawing.Size(100, 35);
            this.BtnEliminar.TabIndex = 28;
            this.BtnEliminar.Text = "Eliminar";
            this.BtnEliminar.UseVisualStyleBackColor = false;
            this.BtnEliminar.Click += new System.EventHandler(this.BtnEliminar_Click);
            // 
            // Usuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1040, 648);
            this.Controls.Add(this.BtnEliminar);
            this.Controls.Add(this.btnServiciosSin);
            this.Controls.Add(this.lblServiciossin);
            this.Controls.Add(this.btnPlan);
            this.Controls.Add(this.panelContenedor);
            this.Controls.Add(this.cbTodos);
            this.Controls.Add(this.btnClientesSin);
            this.Controls.Add(this.lblMensaje4);
            this.Controls.Add(this.BtnAsignar);
            this.Controls.Add(this.CBAsignar);
            this.Controls.Add(this.txtCliente);
            this.Controls.Add(this.lblMensaje3);
            this.Controls.Add(this.BtnBuscar);
            this.Controls.Add(this.lblMensaje2);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.CBTodosMikrotiks);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.CBMikrotiks);
            this.Controls.Add(this.lblMensaje);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.MaximizeBox = false;
            this.Name = "Usuarios";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Clientes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.panelContenedor.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox CBTodosMikrotiks;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ComboBox CBMikrotiks;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Button BtnBuscar;
        private System.Windows.Forms.Label lblMensaje2;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.Label lblMensaje3;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.CheckBox CBAsignar;
        private System.Windows.Forms.Button BtnAsignar;
        private System.Windows.Forms.Label lblMensaje4;
        private System.Windows.Forms.Button btnClientesSin;
        private System.Windows.Forms.CheckBox cbTodos;
        private System.Windows.Forms.Panel panelContenedor; // Cambiado de GroupBox a Panel plano
        private System.Windows.Forms.Button btnPlan;
        private System.Windows.Forms.Label lblServiciossin;
        private System.Windows.Forms.Button btnServiciosSin;
        private System.Windows.Forms.Button BtnEliminar;
    }
}