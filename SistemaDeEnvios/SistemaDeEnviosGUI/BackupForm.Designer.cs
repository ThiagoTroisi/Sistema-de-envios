namespace SistemaDeEnviosGUI
{
    partial class BackupForm
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
            btnCrearBackup = new Button();
            btnRealizarRestore = new Button();
            btnSalir = new Button();
            SuspendLayout();
            // 
            // btnCrearBackup
            // 
            btnCrearBackup.Location = new Point(33, 34);
            btnCrearBackup.Name = "btnCrearBackup";
            btnCrearBackup.Size = new Size(100, 54);
            btnCrearBackup.TabIndex = 0;
            btnCrearBackup.Text = "Crear backup";
            btnCrearBackup.UseVisualStyleBackColor = true;
            btnCrearBackup.Click += btnCrearBackup_Click;
            // 
            // btnRealizarRestore
            // 
            btnRealizarRestore.Location = new Point(158, 34);
            btnRealizarRestore.Name = "btnRealizarRestore";
            btnRealizarRestore.Size = new Size(100, 54);
            btnRealizarRestore.TabIndex = 1;
            btnRealizarRestore.Text = "Realizar restore";
            btnRealizarRestore.UseVisualStyleBackColor = true;
            btnRealizarRestore.Click += btnRealizarRestore_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(108, 106);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(75, 37);
            btnSalir.TabIndex = 2;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // BackupForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(290, 155);
            Controls.Add(btnSalir);
            Controls.Add(btnRealizarRestore);
            Controls.Add(btnCrearBackup);
            Name = "BackupForm";
            Text = "BackupForm";
            ResumeLayout(false);
        }

        #endregion

        private Button btnCrearBackup;
        private Button btnRealizarRestore;
        private Button btnSalir;
    }
}