namespace SistemaDeEnviosGUI.Formularios
{
    partial class CambiarIdiomaForm
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
            comboBoxIdioma = new ComboBox();
            lblIdioma = new Label();
            btnAplicar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            // 
            // comboBoxIdioma
            // 
            comboBoxIdioma.FormattingEnabled = true;
            comboBoxIdioma.Location = new Point(84, 28);
            comboBoxIdioma.Name = "comboBoxIdioma";
            comboBoxIdioma.Size = new Size(137, 23);
            comboBoxIdioma.TabIndex = 11;
            // 
            // lblIdioma
            // 
            lblIdioma.AutoSize = true;
            lblIdioma.Location = new Point(19, 28);
            lblIdioma.Name = "lblIdioma";
            lblIdioma.Size = new Size(47, 15);
            lblIdioma.TabIndex = 10;
            lblIdioma.Text = "Idioma:";
            // 
            // btnAplicar
            // 
            btnAplicar.Location = new Point(19, 112);
            btnAplicar.Name = "btnAplicar";
            btnAplicar.Size = new Size(98, 34);
            btnAplicar.TabIndex = 12;
            btnAplicar.Text = "Aplicar";
            btnAplicar.UseVisualStyleBackColor = true;
            btnAplicar.Click += btnAplicar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(123, 112);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(98, 34);
            btnCancelar.TabIndex = 13;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // CambiarIdiomaForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(240, 166);
            Controls.Add(btnCancelar);
            Controls.Add(btnAplicar);
            Controls.Add(comboBoxIdioma);
            Controls.Add(lblIdioma);
            Name = "CambiarIdiomaForm";
            Text = "CambiarIdiomaForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxIdioma;
        private Label lblIdioma;
        private Button btnAplicar;
        private Button btnCancelar;
    }
}