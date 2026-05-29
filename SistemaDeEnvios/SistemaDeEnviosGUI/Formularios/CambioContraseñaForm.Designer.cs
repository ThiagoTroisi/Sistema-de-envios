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
            label1 = new Label();
            txtActual = new TextBox();
            btnAceptar = new Button();
            txtNueva1 = new TextBox();
            label2 = new Label();
            txtNueva2 = new TextBox();
            label3 = new Label();
            btnCancelar = new Button();
            labelError = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(159, 15);
            label1.TabIndex = 0;
            label1.Text = "Ingrese su contraseña actual:";
            // 
            // txtActual
            // 
            txtActual.Location = new Point(12, 27);
            txtActual.Name = "txtActual";
            txtActual.Size = new Size(265, 23);
            txtActual.TabIndex = 1;
            // 
            // btnAceptar
            // 
            btnAceptar.Location = new Point(35, 224);
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
            txtNueva1.Size = new Size(265, 23);
            txtNueva1.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 71);
            label2.Name = "label2";
            label2.Size = new Size(159, 15);
            label2.TabIndex = 3;
            label2.Text = "Ingrese su nueva contraseña:";
            // 
            // txtNueva2
            // 
            txtNueva2.Location = new Point(12, 151);
            txtNueva2.Name = "txtNueva2";
            txtNueva2.Size = new Size(265, 23);
            txtNueva2.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 133);
            label3.Name = "label3";
            label3.Size = new Size(190, 15);
            label3.TabIndex = 5;
            label3.Text = "Ingrese nuevamente la contraseña:";
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(155, 224);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(98, 32);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // labelError
            // 
            labelError.AutoSize = true;
            labelError.Location = new Point(12, 191);
            labelError.Name = "labelError";
            labelError.Size = new Size(0, 15);
            labelError.TabIndex = 8;
            // 
            // CambioContraseñaForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(289, 277);
            Controls.Add(labelError);
            Controls.Add(btnCancelar);
            Controls.Add(txtNueva2);
            Controls.Add(label3);
            Controls.Add(txtNueva1);
            Controls.Add(label2);
            Controls.Add(btnAceptar);
            Controls.Add(txtActual);
            Controls.Add(label1);
            Name = "CambioContraseñaForm";
            Text = "Cambio de contraseña";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtActual;
        private Button btnAceptar;
        private TextBox txtNueva1;
        private Label label2;
        private TextBox txtNueva2;
        private Label label3;
        private Button btnCancelar;
        private Label labelError;
    }
}