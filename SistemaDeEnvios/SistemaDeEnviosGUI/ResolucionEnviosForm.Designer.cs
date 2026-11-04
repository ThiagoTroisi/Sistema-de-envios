namespace SistemaDeEnviosGUI
{
    partial class ResolucionEnviosForm
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
            lblEnviosPendientes = new Label();
            dataGridViewEnviosPendientes = new DataGridView();
            lblHistorialIntentos = new Label();
            dataGridViewHistorialIntentos = new DataGridView();
            btnAutorizarNuevoIntento = new Button();
            btnCancelarEnvio = new Button();
            btnSalir = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEnviosPendientes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewHistorialIntentos).BeginInit();
            SuspendLayout();
            // 
            // lblEnviosPendientes
            // 
            lblEnviosPendientes.AutoSize = true;
            lblEnviosPendientes.Location = new Point(12, 9);
            lblEnviosPendientes.Name = "lblEnviosPendientes";
            lblEnviosPendientes.Size = new Size(176, 15);
            lblEnviosPendientes.TabIndex = 3;
            lblEnviosPendientes.Text = "Envios pendientes de resolución";
            // 
            // dataGridViewEnviosPendientes
            // 
            dataGridViewEnviosPendientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewEnviosPendientes.Location = new Point(12, 27);
            dataGridViewEnviosPendientes.Name = "dataGridViewEnviosPendientes";
            dataGridViewEnviosPendientes.Size = new Size(539, 249);
            dataGridViewEnviosPendientes.TabIndex = 2;
            dataGridViewEnviosPendientes.SelectionChanged += dataGridViewEnviosPendientes_SelectionChanged;
            // 
            // lblHistorialIntentos
            // 
            lblHistorialIntentos.AutoSize = true;
            lblHistorialIntentos.Location = new Point(12, 308);
            lblHistorialIntentos.Name = "lblHistorialIntentos";
            lblHistorialIntentos.Size = new Size(236, 15);
            lblHistorialIntentos.TabIndex = 5;
            lblHistorialIntentos.Text = "Historial de intentos del envío seleccionado";
            // 
            // dataGridViewHistorialIntentos
            // 
            dataGridViewHistorialIntentos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewHistorialIntentos.Location = new Point(12, 326);
            dataGridViewHistorialIntentos.Name = "dataGridViewHistorialIntentos";
            dataGridViewHistorialIntentos.Size = new Size(539, 249);
            dataGridViewHistorialIntentos.TabIndex = 4;
            // 
            // btnAutorizarNuevoIntento
            // 
            btnAutorizarNuevoIntento.Location = new Point(12, 583);
            btnAutorizarNuevoIntento.Name = "btnAutorizarNuevoIntento";
            btnAutorizarNuevoIntento.Size = new Size(117, 48);
            btnAutorizarNuevoIntento.TabIndex = 6;
            btnAutorizarNuevoIntento.Text = "Autorizar nuevo intento";
            btnAutorizarNuevoIntento.UseVisualStyleBackColor = true;
            btnAutorizarNuevoIntento.Click += btnAutorizarNuevoIntento_Click;
            // 
            // btnCancelarEnvio
            // 
            btnCancelarEnvio.Location = new Point(135, 583);
            btnCancelarEnvio.Name = "btnCancelarEnvio";
            btnCancelarEnvio.Size = new Size(117, 48);
            btnCancelarEnvio.TabIndex = 7;
            btnCancelarEnvio.Text = "Cancelar envío";
            btnCancelarEnvio.UseVisualStyleBackColor = true;
            btnCancelarEnvio.Click += btnCancelarEnvio_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(440, 583);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(111, 48);
            btnSalir.TabIndex = 8;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // ResolucionEnviosForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(563, 643);
            Controls.Add(btnSalir);
            Controls.Add(btnCancelarEnvio);
            Controls.Add(btnAutorizarNuevoIntento);
            Controls.Add(lblHistorialIntentos);
            Controls.Add(dataGridViewHistorialIntentos);
            Controls.Add(lblEnviosPendientes);
            Controls.Add(dataGridViewEnviosPendientes);
            Name = "ResolucionEnviosForm";
            Text = "ResolucionEnviosForm";
            ((System.ComponentModel.ISupportInitialize)dataGridViewEnviosPendientes).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewHistorialIntentos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblEnviosPendientes;
        private DataGridView dataGridViewEnviosPendientes;
        private Label lblHistorialIntentos;
        private DataGridView dataGridViewHistorialIntentos;
        private Button btnAutorizarNuevoIntento;
        private Button btnCancelarEnvio;
        private Button btnSalir;
    }
}