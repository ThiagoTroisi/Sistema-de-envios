namespace SistemaDeEnviosGUI
{
    partial class CobroEnviosForm
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
            dataGridViewEnvios = new DataGridView();
            lblEnvioSeleccionado = new Label();
            lblImporteAAbonar = new Label();
            txtEnvioSeleccionado = new TextBox();
            txtImporteAAbonar = new TextBox();
            txtDNIRemitente = new TextBox();
            lblDNIRemitente = new Label();
            btnCobrar = new Button();
            btnSalir = new Button();
            label7 = new Label();
            txtCodigoSeguimiento = new TextBox();
            lblCodigoSeguimiento = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEnvios).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewEnvios
            // 
            dataGridViewEnvios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewEnvios.Location = new Point(12, 41);
            dataGridViewEnvios.Name = "dataGridViewEnvios";
            dataGridViewEnvios.Size = new Size(769, 284);
            dataGridViewEnvios.TabIndex = 0;
            dataGridViewEnvios.SelectionChanged += dataGridViewEnvios_SelectionChanged;
            // 
            // lblEnvioSeleccionado
            // 
            lblEnvioSeleccionado.AutoSize = true;
            lblEnvioSeleccionado.Location = new Point(12, 338);
            lblEnvioSeleccionado.Name = "lblEnvioSeleccionado";
            lblEnvioSeleccionado.Size = new Size(111, 15);
            lblEnvioSeleccionado.TabIndex = 1;
            lblEnvioSeleccionado.Text = "Envío seleccionado:";
            // 
            // lblImporteAAbonar
            // 
            lblImporteAAbonar.AutoSize = true;
            lblImporteAAbonar.Location = new Point(12, 367);
            lblImporteAAbonar.Name = "lblImporteAAbonar";
            lblImporteAAbonar.Size = new Size(101, 15);
            lblImporteAAbonar.TabIndex = 2;
            lblImporteAAbonar.Text = "Importe a abonar:";
            // 
            // txtEnvioSeleccionado
            // 
            txtEnvioSeleccionado.Location = new Point(139, 335);
            txtEnvioSeleccionado.Name = "txtEnvioSeleccionado";
            txtEnvioSeleccionado.ReadOnly = true;
            txtEnvioSeleccionado.Size = new Size(137, 23);
            txtEnvioSeleccionado.TabIndex = 3;
            // 
            // txtImporteAAbonar
            // 
            txtImporteAAbonar.Location = new Point(139, 364);
            txtImporteAAbonar.Name = "txtImporteAAbonar";
            txtImporteAAbonar.ReadOnly = true;
            txtImporteAAbonar.Size = new Size(137, 23);
            txtImporteAAbonar.TabIndex = 4;
            // 
            // txtDNIRemitente
            // 
            txtDNIRemitente.Location = new Point(121, 12);
            txtDNIRemitente.Name = "txtDNIRemitente";
            txtDNIRemitente.Size = new Size(104, 23);
            txtDNIRemitente.TabIndex = 6;
            txtDNIRemitente.TextChanged += txtDNIRemitente_TextChanged;
            // 
            // lblDNIRemitente
            // 
            lblDNIRemitente.AutoSize = true;
            lblDNIRemitente.Location = new Point(12, 15);
            lblDNIRemitente.Name = "lblDNIRemitente";
            lblDNIRemitente.Size = new Size(103, 15);
            lblDNIRemitente.TabIndex = 5;
            lblDNIRemitente.Text = "DNI del remitente:";
            // 
            // btnCobrar
            // 
            btnCobrar.Location = new Point(533, 343);
            btnCobrar.Name = "btnCobrar";
            btnCobrar.Size = new Size(121, 44);
            btnCobrar.TabIndex = 7;
            btnCobrar.Text = "Cobrar";
            btnCobrar.UseVisualStyleBackColor = true;
            btnCobrar.Click += btnCobrar_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(660, 343);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(121, 44);
            btnSalir.TabIndex = 9;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(408, 15);
            label7.Name = "label7";
            label7.Size = new Size(37, 15);
            label7.TabIndex = 68;
            label7.Text = "ENV -";
            // 
            // txtCodigoSeguimiento
            // 
            txtCodigoSeguimiento.Location = new Point(446, 12);
            txtCodigoSeguimiento.Name = "txtCodigoSeguimiento";
            txtCodigoSeguimiento.Size = new Size(96, 23);
            txtCodigoSeguimiento.TabIndex = 67;
            txtCodigoSeguimiento.TextChanged += txtCodigoSeguimiento_TextChanged;
            // 
            // lblCodigoSeguimiento
            // 
            lblCodigoSeguimiento.AutoSize = true;
            lblCodigoSeguimiento.Location = new Point(268, 15);
            lblCodigoSeguimiento.Name = "lblCodigoSeguimiento";
            lblCodigoSeguimiento.Size = new Size(134, 15);
            lblCodigoSeguimiento.TabIndex = 66;
            lblCodigoSeguimiento.Text = "Código de seguimiento:";
            // 
            // CobroEnviosForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(793, 399);
            Controls.Add(label7);
            Controls.Add(txtCodigoSeguimiento);
            Controls.Add(lblCodigoSeguimiento);
            Controls.Add(btnSalir);
            Controls.Add(btnCobrar);
            Controls.Add(txtDNIRemitente);
            Controls.Add(lblDNIRemitente);
            Controls.Add(txtImporteAAbonar);
            Controls.Add(txtEnvioSeleccionado);
            Controls.Add(lblImporteAAbonar);
            Controls.Add(lblEnvioSeleccionado);
            Controls.Add(dataGridViewEnvios);
            Name = "CobroEnviosForm";
            Text = "CobroEnviosForm";
            Load += CobroEnviosForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewEnvios).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewEnvios;
        private Label lblEnvioSeleccionado;
        private Label lblImporteAAbonar;
        private TextBox txtEnvioSeleccionado;
        private TextBox txtImporteAAbonar;
        private TextBox txtDNIRemitente;
        private Label lblDNIRemitente;
        private Button btnCobrar;
        private Button btnSalir;
        private Label label7;
        private TextBox txtCodigoSeguimiento;
        private Label lblCodigoSeguimiento;
    }
}