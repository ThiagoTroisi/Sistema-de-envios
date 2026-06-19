namespace SistemaDeEnviosGUI.Formularios
{
    partial class LoginForm
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
            txtEmail = new TextBox();
            lblEmail = new Label();
            lblContraseña = new Label();
            txtContraseña = new TextBox();
            btnLogin = new Button();
            btnRegistrarse = new Button();
            lblError = new Label();
            btnSalir = new Button();
            lblIdioma = new Label();
            comboBoxIdioma = new ComboBox();
            SuspendLayout();
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(12, 27);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(302, 23);
            txtEmail.TabIndex = 0;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(12, 9);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(39, 15);
            lblEmail.TabIndex = 1;
            lblEmail.Text = "Email:";
            // 
            // lblContraseña
            // 
            lblContraseña.AutoSize = true;
            lblContraseña.Location = new Point(12, 69);
            lblContraseña.Name = "lblContraseña";
            lblContraseña.Size = new Size(70, 15);
            lblContraseña.TabIndex = 3;
            lblContraseña.Text = "Contraseña:";
            // 
            // txtContraseña
            // 
            txtContraseña.Location = new Point(12, 87);
            txtContraseña.Name = "txtContraseña";
            txtContraseña.Size = new Size(302, 23);
            txtContraseña.TabIndex = 2;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(70, 152);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(186, 32);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Iniciar sesión";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnRegistrarse
            // 
            btnRegistrarse.Location = new Point(70, 190);
            btnRegistrarse.Name = "btnRegistrarse";
            btnRegistrarse.Size = new Size(186, 32);
            btnRegistrarse.TabIndex = 5;
            btnRegistrarse.Text = "Registrarse";
            btnRegistrarse.UseVisualStyleBackColor = true;
            btnRegistrarse.Click += btnRegistro_Click;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Location = new Point(12, 131);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 15);
            lblError.TabIndex = 6;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(70, 228);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(186, 32);
            btnSalir.TabIndex = 7;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // lblIdioma
            // 
            lblIdioma.AutoSize = true;
            lblIdioma.Location = new Point(70, 276);
            lblIdioma.Name = "lblIdioma";
            lblIdioma.Size = new Size(47, 15);
            lblIdioma.TabIndex = 8;
            lblIdioma.Text = "Idioma:";
            // 
            // comboBoxIdioma
            // 
            comboBoxIdioma.FormattingEnabled = true;
            comboBoxIdioma.Location = new Point(135, 276);
            comboBoxIdioma.Name = "comboBoxIdioma";
            comboBoxIdioma.Size = new Size(121, 23);
            comboBoxIdioma.TabIndex = 9;
            comboBoxIdioma.SelectedIndexChanged += comboBoxIdioma_SelectedIndexChanged;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(326, 341);
            Controls.Add(comboBoxIdioma);
            Controls.Add(lblIdioma);
            Controls.Add(btnSalir);
            Controls.Add(lblError);
            Controls.Add(btnRegistrarse);
            Controls.Add(btnLogin);
            Controls.Add(lblContraseña);
            Controls.Add(txtContraseña);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Name = "LoginForm";
            Text = "LoginForm";
            Load += LoginForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtEmail;
        private Label lblEmail;
        private Label lblContraseña;
        private TextBox txtContraseña;
        private Button btnLogin;
        private Button btnRegistrarse;
        private Label lblError;
        private Button btnSalir;
        private Label lblIdioma;
        private ComboBox comboBoxIdioma;
    }
}