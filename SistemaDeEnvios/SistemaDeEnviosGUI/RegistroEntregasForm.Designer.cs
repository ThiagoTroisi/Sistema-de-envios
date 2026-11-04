namespace SistemaDeEnviosGUI
{
    partial class RegistroEntregasForm
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
            lblEnviosDeLaRuta = new Label();
            dataGridViewEnviosDeLaRuta = new DataGridView();
            lblEnvioSeleccionado = new Label();
            cmbResultado = new ComboBox();
            lblResultado = new Label();
            lblMotivo = new Label();
            cmbMotivo = new ComboBox();
            lblObservaciones = new Label();
            txtObservaciones = new TextBox();
            btnRegistrarIntento = new Button();
            btnLimpiar = new Button();
            btnCerrar = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEnviosDeLaRuta).BeginInit();
            SuspendLayout();
            // 
            // lblEnviosDeLaRuta
            // 
            lblEnviosDeLaRuta.AutoSize = true;
            lblEnviosDeLaRuta.Location = new Point(12, 9);
            lblEnviosDeLaRuta.Name = "lblEnviosDeLaRuta";
            lblEnviosDeLaRuta.Size = new Size(93, 15);
            lblEnviosDeLaRuta.TabIndex = 18;
            lblEnviosDeLaRuta.Text = "Envíos de la ruta";
            // 
            // dataGridViewEnviosDeLaRuta
            // 
            dataGridViewEnviosDeLaRuta.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewEnviosDeLaRuta.Location = new Point(12, 27);
            dataGridViewEnviosDeLaRuta.Name = "dataGridViewEnviosDeLaRuta";
            dataGridViewEnviosDeLaRuta.Size = new Size(723, 252);
            dataGridViewEnviosDeLaRuta.TabIndex = 17;
            dataGridViewEnviosDeLaRuta.SelectionChanged += dataGridViewEnviosDeLaRuta_SelectionChanged;
            // 
            // lblEnvioSeleccionado
            // 
            lblEnvioSeleccionado.AutoSize = true;
            lblEnvioSeleccionado.Location = new Point(12, 299);
            lblEnvioSeleccionado.Name = "lblEnvioSeleccionado";
            lblEnvioSeleccionado.Size = new Size(114, 15);
            lblEnvioSeleccionado.TabIndex = 19;
            lblEnvioSeleccionado.Text = "Envío seleccionado: ";
            // 
            // cmbResultado
            // 
            cmbResultado.FormattingEnabled = true;
            cmbResultado.Location = new Point(12, 356);
            cmbResultado.Name = "cmbResultado";
            cmbResultado.Size = new Size(250, 23);
            cmbResultado.TabIndex = 20;
            cmbResultado.SelectedIndexChanged += cmbResultado_SelectedIndexChanged;
            // 
            // lblResultado
            // 
            lblResultado.AutoSize = true;
            lblResultado.Location = new Point(12, 338);
            lblResultado.Name = "lblResultado";
            lblResultado.Size = new Size(62, 15);
            lblResultado.TabIndex = 21;
            lblResultado.Text = "Resultado:";
            // 
            // lblMotivo
            // 
            lblMotivo.AutoSize = true;
            lblMotivo.Location = new Point(12, 402);
            lblMotivo.Name = "lblMotivo";
            lblMotivo.Size = new Size(145, 15);
            lblMotivo.TabIndex = 23;
            lblMotivo.Text = "Motivo del inconveniente:";
            // 
            // cmbMotivo
            // 
            cmbMotivo.FormattingEnabled = true;
            cmbMotivo.Location = new Point(12, 420);
            cmbMotivo.Name = "cmbMotivo";
            cmbMotivo.Size = new Size(250, 23);
            cmbMotivo.TabIndex = 22;
            // 
            // lblObservaciones
            // 
            lblObservaciones.AutoSize = true;
            lblObservaciones.Location = new Point(291, 338);
            lblObservaciones.Name = "lblObservaciones";
            lblObservaciones.Size = new Size(87, 15);
            lblObservaciones.TabIndex = 24;
            lblObservaciones.Text = "Observaciones:";
            // 
            // txtObservaciones
            // 
            txtObservaciones.Location = new Point(291, 356);
            txtObservaciones.Multiline = true;
            txtObservaciones.Name = "txtObservaciones";
            txtObservaciones.Size = new Size(444, 87);
            txtObservaciones.TabIndex = 25;
            // 
            // btnRegistrarIntento
            // 
            btnRegistrarIntento.Location = new Point(137, 462);
            btnRegistrarIntento.Name = "btnRegistrarIntento";
            btnRegistrarIntento.Size = new Size(114, 42);
            btnRegistrarIntento.TabIndex = 26;
            btnRegistrarIntento.Text = "Registrar intento";
            btnRegistrarIntento.UseVisualStyleBackColor = true;
            btnRegistrarIntento.Click += btnRegistrarIntento_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(316, 462);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(114, 42);
            btnLimpiar.TabIndex = 27;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnCerrar
            // 
            btnCerrar.Location = new Point(495, 462);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(114, 42);
            btnCerrar.TabIndex = 28;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // RegistroEntregasForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(747, 516);
            Controls.Add(btnCerrar);
            Controls.Add(btnLimpiar);
            Controls.Add(btnRegistrarIntento);
            Controls.Add(txtObservaciones);
            Controls.Add(lblObservaciones);
            Controls.Add(lblMotivo);
            Controls.Add(cmbMotivo);
            Controls.Add(lblResultado);
            Controls.Add(cmbResultado);
            Controls.Add(lblEnvioSeleccionado);
            Controls.Add(lblEnviosDeLaRuta);
            Controls.Add(dataGridViewEnviosDeLaRuta);
            Name = "RegistroEntregasForm";
            Text = "RegistroEntregasForm";
            Load += RegistroEntregasForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewEnviosDeLaRuta).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblEnviosDeLaRuta;
        private DataGridView dataGridViewEnviosDeLaRuta;
        private Label lblEnvioSeleccionado;
        private ComboBox cmbResultado;
        private Label lblResultado;
        private Label lblMotivo;
        private ComboBox cmbMotivo;
        private Label lblObservaciones;
        private TextBox txtObservaciones;
        private Button btnRegistrarIntento;
        private Button btnLimpiar;
        private Button btnCerrar;
    }
}