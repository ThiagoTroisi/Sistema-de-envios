namespace SistemaDeEnviosGUI
{
    partial class GestionFacturasForm
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
            label7 = new Label();
            txtCodigoSeguimiento = new TextBox();
            lblCodigoSeguimiento = new Label();
            btnSalir = new Button();
            btnGenerarFactura = new Button();
            txtDNIRemitente = new TextBox();
            lblDNIRemitente = new Label();
            txtEnvioSeleccionado = new TextBox();
            lblEnvioSeleccionado = new Label();
            dataGridViewEnvios = new DataGridView();
            btnImprimirFactura = new Button();
            rbTodos = new RadioButton();
            lblMostrar = new Label();
            rbSinFactura = new RadioButton();
            rbConFactura = new RadioButton();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEnvios).BeginInit();
            SuspendLayout();
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(377, 15);
            label7.Name = "label7";
            label7.Size = new Size(37, 15);
            label7.TabIndex = 78;
            label7.Text = "ENV -";
            // 
            // txtCodigoSeguimiento
            // 
            txtCodigoSeguimiento.Location = new Point(420, 12);
            txtCodigoSeguimiento.Name = "txtCodigoSeguimiento";
            txtCodigoSeguimiento.Size = new Size(94, 23);
            txtCodigoSeguimiento.TabIndex = 77;
            txtCodigoSeguimiento.TextChanged += txtCodigoSeguimiento_TextChanged;
            // 
            // lblCodigoSeguimiento
            // 
            lblCodigoSeguimiento.AutoSize = true;
            lblCodigoSeguimiento.Location = new Point(237, 15);
            lblCodigoSeguimiento.Name = "lblCodigoSeguimiento";
            lblCodigoSeguimiento.Size = new Size(134, 15);
            lblCodigoSeguimiento.TabIndex = 76;
            lblCodigoSeguimiento.Text = "Código de seguimiento:";
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(661, 348);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(121, 36);
            btnSalir.TabIndex = 75;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // btnGenerarFactura
            // 
            btnGenerarFactura.Location = new Point(407, 348);
            btnGenerarFactura.Name = "btnGenerarFactura";
            btnGenerarFactura.Size = new Size(121, 36);
            btnGenerarFactura.TabIndex = 74;
            btnGenerarFactura.Text = "Generar factura";
            btnGenerarFactura.UseVisualStyleBackColor = true;
            btnGenerarFactura.Click += btnGenerarFactura_Click;
            // 
            // txtDNIRemitente
            // 
            txtDNIRemitente.Location = new Point(121, 12);
            txtDNIRemitente.Name = "txtDNIRemitente";
            txtDNIRemitente.Size = new Size(94, 23);
            txtDNIRemitente.TabIndex = 73;
            txtDNIRemitente.TextChanged += txtDNIRemitente_TextChanged;
            // 
            // lblDNIRemitente
            // 
            lblDNIRemitente.AutoSize = true;
            lblDNIRemitente.Location = new Point(12, 15);
            lblDNIRemitente.Name = "lblDNIRemitente";
            lblDNIRemitente.Size = new Size(103, 15);
            lblDNIRemitente.TabIndex = 72;
            lblDNIRemitente.Text = "DNI del remitente:";
            // 
            // txtEnvioSeleccionado
            // 
            txtEnvioSeleccionado.Location = new Point(139, 352);
            txtEnvioSeleccionado.Name = "txtEnvioSeleccionado";
            txtEnvioSeleccionado.ReadOnly = true;
            txtEnvioSeleccionado.Size = new Size(137, 23);
            txtEnvioSeleccionado.TabIndex = 71;
            // 
            // lblEnvioSeleccionado
            // 
            lblEnvioSeleccionado.AutoSize = true;
            lblEnvioSeleccionado.Location = new Point(12, 355);
            lblEnvioSeleccionado.Name = "lblEnvioSeleccionado";
            lblEnvioSeleccionado.Size = new Size(111, 15);
            lblEnvioSeleccionado.TabIndex = 70;
            lblEnvioSeleccionado.Text = "Envío seleccionado:";
            // 
            // dataGridViewEnvios
            // 
            dataGridViewEnvios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewEnvios.Location = new Point(12, 52);
            dataGridViewEnvios.Name = "dataGridViewEnvios";
            dataGridViewEnvios.Size = new Size(770, 290);
            dataGridViewEnvios.TabIndex = 69;
            // 
            // btnImprimirFactura
            // 
            btnImprimirFactura.Location = new Point(534, 348);
            btnImprimirFactura.Name = "btnImprimirFactura";
            btnImprimirFactura.Size = new Size(121, 36);
            btnImprimirFactura.TabIndex = 79;
            btnImprimirFactura.Text = "Imprimir factura";
            btnImprimirFactura.UseVisualStyleBackColor = true;
            btnImprimirFactura.Click += btnImprimirFactura_Click;
            // 
            // rbTodos
            // 
            rbTodos.AutoSize = true;
            rbTodos.Location = new Point(534, 27);
            rbTodos.Name = "rbTodos";
            rbTodos.Size = new Size(56, 19);
            rbTodos.TabIndex = 80;
            rbTodos.Text = "Todos";
            rbTodos.UseVisualStyleBackColor = true;
            rbTodos.CheckedChanged += rbTodos_CheckedChanged;
            // 
            // lblMostrar
            // 
            lblMostrar.AutoSize = true;
            lblMostrar.Location = new Point(534, 9);
            lblMostrar.Name = "lblMostrar";
            lblMostrar.Size = new Size(51, 15);
            lblMostrar.TabIndex = 81;
            lblMostrar.Text = "Mostrar:";
            // 
            // rbSinFactura
            // 
            rbSinFactura.AutoSize = true;
            rbSinFactura.Location = new Point(602, 27);
            rbSinFactura.Name = "rbSinFactura";
            rbSinFactura.Size = new Size(81, 19);
            rbSinFactura.TabIndex = 82;
            rbSinFactura.Text = "Sin factura";
            rbSinFactura.UseVisualStyleBackColor = true;
            rbSinFactura.CheckedChanged += rbSinFactura_CheckedChanged;
            // 
            // rbConFactura
            // 
            rbConFactura.AutoSize = true;
            rbConFactura.Location = new Point(695, 27);
            rbConFactura.Name = "rbConFactura";
            rbConFactura.Size = new Size(87, 19);
            rbConFactura.TabIndex = 83;
            rbConFactura.Text = "Con factura";
            rbConFactura.UseVisualStyleBackColor = true;
            rbConFactura.CheckedChanged += rbConFactura_CheckedChanged;
            // 
            // GestionFacturasForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(794, 396);
            Controls.Add(rbConFactura);
            Controls.Add(rbSinFactura);
            Controls.Add(lblMostrar);
            Controls.Add(rbTodos);
            Controls.Add(btnImprimirFactura);
            Controls.Add(label7);
            Controls.Add(txtCodigoSeguimiento);
            Controls.Add(lblCodigoSeguimiento);
            Controls.Add(btnSalir);
            Controls.Add(btnGenerarFactura);
            Controls.Add(txtDNIRemitente);
            Controls.Add(lblDNIRemitente);
            Controls.Add(txtEnvioSeleccionado);
            Controls.Add(lblEnvioSeleccionado);
            Controls.Add(dataGridViewEnvios);
            Name = "GestionFacturasForm";
            Text = "GestionFacturasForm";
            Load += GestionFacturasForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewEnvios).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label7;
        private TextBox txtCodigoSeguimiento;
        private Label lblCodigoSeguimiento;
        private Button btnSalir;
        private Button btnGenerarFactura;
        private TextBox txtDNIRemitente;
        private Label lblDNIRemitente;
        private TextBox txtEnvioSeleccionado;
        private Label lblEnvioSeleccionado;
        private DataGridView dataGridViewEnvios;
        private Button btnImprimirFactura;
        private RadioButton rbTodos;
        private Label lblMostrar;
        private RadioButton rbSinFactura;
        private RadioButton rbConFactura;
    }
}