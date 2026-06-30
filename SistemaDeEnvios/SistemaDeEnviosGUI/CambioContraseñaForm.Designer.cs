namespace SistemaDeEnviosGUI.Formularios
{
    partial class CambioContraseñaForm
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
            lblActual = new Label();
            txtActual = new TextBox();
            btnAceptar = new Button();
            txtNueva1 = new TextBox();
            lblNueva = new Label();
            txtNueva2 = new TextBox();
            lblNueva2 = new Label();
            btnCancelar = new Button();
            lblError = new Label();
            SuspendLayout();
            // 
            // lblActual
            // 
            lblActual.AutoSize = true;
            lblActual.Location = new Point(12, 9);
            lblActual.Name = "lblActual";
            lblActual.Size = new Size(159, 15);
            lblActual.TabIndex = 0;
            lblActual.Text = "Ingrese su contraseña actual:";
            // 
            // txtActual
            // 
            txtActual.Location = new Point(12, 27);
            txtActual.Name = "txtActual";
            txtActual.Size = new Size(288, 23);
            txtActual.TabIndex = 1;
            // 
            // btnAceptar
            // 
            btnAceptar.Location = new Point(47, 224);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(98, 32);
            btnAceptar.TabIndex = 2;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = true;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // txtNueva1
            // 
            txtNueva1.Location = new Point(12, 89);
            txtNueva1.Name = "txtNueva1";
            txtNueva1.Size = new Size(288, 23);
            txtNueva1.TabIndex = 4;
            // 
            // lblNueva
            // 
            lblNueva.AutoSize = true;
            lblNueva.Location = new Point(12, 71);
            lblNueva.Name = "lblNueva";
            lblNueva.Size = new Size(159, 15);
            lblNueva.TabIndex = 3;
            lblNueva.Text = "Ingrese su nueva contraseña:";
            // 
            // txtNueva2
            // 
            txtNueva2.Location = new Point(12, 151);
            txtNueva2.Name = "txtNueva2";
            txtNueva2.Size = new Size(288, 23);
            txtNueva2.TabIndex = 6;
            // 
            // lblNueva2
            // 
            lblNueva2.AutoSize = true;
            lblNueva2.Location = new Point(12, 133);
            lblNueva2.Name = "lblNueva2";
            lblNueva2.Size = new Size(190, 15);
            lblNueva2.TabIndex = 5;
            lblNueva2.Text = "Ingrese nuevamente la contraseña:";
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(167, 224);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(98, 32);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Location = new Point(12, 191);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 15);
            lblError.TabIndex = 8;
            // 
            // CambioContraseñaForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(312, 277);
            Controls.Add(lblError);
            Controls.Add(btnCancelar);
            Controls.Add(txtNueva2);
            Controls.Add(lblNueva2);
            Controls.Add(txtNueva1);
            Controls.Add(lblNueva);
            Controls.Add(btnAceptar);
            Controls.Add(txtActual);
            Controls.Add(lblActual);
            Name = "CambioContraseñaForm";
            Text = "Cambio de contraseña";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblActual;
        private TextBox txtActual;
        private Button btnAceptar;
        private TextBox txtNueva1;
        private Label lblNueva;
        private TextBox txtNueva2;
        private Label lblNueva2;
        private Button btnCancelar;
        private Label lblError;
    }
}