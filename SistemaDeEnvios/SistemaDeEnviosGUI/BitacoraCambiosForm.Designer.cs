namespace SistemaDeEnviosGUI
{
    partial class BitacoraCambiosForm
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
            dataGridViewHistorial = new DataGridView();
            lblHistorialDeEnvios = new Label();
            btnAplicar = new Button();
            comboBoxRemitente = new ComboBox();
            comboBoxCodigo = new ComboBox();
            lblRemitente = new Label();
            lblCod = new Label();
            dateTimePickerHasta = new DateTimePicker();
            lblHasta = new Label();
            dateTimePickerDesde = new DateTimePicker();
            lblDesde = new Label();
            btnLimpiar = new Button();
            btnSalir = new Button();
            btnActivar = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewHistorial).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewHistorial
            // 
            dataGridViewHistorial.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewHistorial.Location = new Point(12, 27);
            dataGridViewHistorial.Name = "dataGridViewHistorial";
            dataGridViewHistorial.RowHeadersVisible = false;
            dataGridViewHistorial.Size = new Size(1740, 411);
            dataGridViewHistorial.TabIndex = 0;
            dataGridViewHistorial.CellClick += dataGridViewHistorial_CellClick;
            // 
            // lblHistorialDeEnvios
            // 
            lblHistorialDeEnvios.AutoSize = true;
            lblHistorialDeEnvios.Location = new Point(12, 9);
            lblHistorialDeEnvios.Name = "lblHistorialDeEnvios";
            lblHistorialDeEnvios.Size = new Size(104, 15);
            lblHistorialDeEnvios.TabIndex = 1;
            lblHistorialDeEnvios.Text = "Historial de envíos";
            // 
            // btnAplicar
            // 
            btnAplicar.Location = new Point(663, 454);
            btnAplicar.Name = "btnAplicar";
            btnAplicar.Size = new Size(116, 45);
            btnAplicar.TabIndex = 2;
            btnAplicar.Text = "Aplicar";
            btnAplicar.UseVisualStyleBackColor = true;
            btnAplicar.Click += btnAplicar_Click;
            // 
            // comboBoxRemitente
            // 
            comboBoxRemitente.FormattingEnabled = true;
            comboBoxRemitente.Location = new Point(340, 473);
            comboBoxRemitente.Name = "comboBoxRemitente";
            comboBoxRemitente.Size = new Size(284, 23);
            comboBoxRemitente.TabIndex = 38;
            // 
            // comboBoxCodigo
            // 
            comboBoxCodigo.FormattingEnabled = true;
            comboBoxCodigo.Location = new Point(12, 473);
            comboBoxCodigo.Name = "comboBoxCodigo";
            comboBoxCodigo.Size = new Size(284, 23);
            comboBoxCodigo.TabIndex = 37;
            // 
            // lblRemitente
            // 
            lblRemitente.AutoSize = true;
            lblRemitente.Location = new Point(340, 454);
            lblRemitente.Name = "lblRemitente";
            lblRemitente.Size = new Size(64, 15);
            lblRemitente.TabIndex = 36;
            lblRemitente.Text = "Remitente:";
            // 
            // lblCod
            // 
            lblCod.AutoSize = true;
            lblCod.Location = new Point(12, 454);
            lblCod.Name = "lblCod";
            lblCod.Size = new Size(49, 15);
            lblCod.TabIndex = 35;
            lblCod.Text = "Código:";
            // 
            // dateTimePickerHasta
            // 
            dateTimePickerHasta.Checked = false;
            dateTimePickerHasta.Location = new Point(340, 538);
            dateTimePickerHasta.Name = "dateTimePickerHasta";
            dateTimePickerHasta.ShowCheckBox = true;
            dateTimePickerHasta.Size = new Size(284, 23);
            dateTimePickerHasta.TabIndex = 42;
            // 
            // lblHasta
            // 
            lblHasta.AutoSize = true;
            lblHasta.Location = new Point(340, 520);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(40, 15);
            lblHasta.TabIndex = 41;
            lblHasta.Text = "Hasta:";
            // 
            // dateTimePickerDesde
            // 
            dateTimePickerDesde.Checked = false;
            dateTimePickerDesde.Location = new Point(12, 538);
            dateTimePickerDesde.Name = "dateTimePickerDesde";
            dateTimePickerDesde.ShowCheckBox = true;
            dateTimePickerDesde.Size = new Size(284, 23);
            dateTimePickerDesde.TabIndex = 40;
            // 
            // lblDesde
            // 
            lblDesde.AutoSize = true;
            lblDesde.Location = new Point(12, 520);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new Size(42, 15);
            lblDesde.TabIndex = 39;
            lblDesde.Text = "Desde:";
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(663, 516);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(116, 45);
            btnLimpiar.TabIndex = 43;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(1636, 513);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(116, 45);
            btnSalir.TabIndex = 45;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // btnActivar
            // 
            btnActivar.Enabled = false;
            btnActivar.Location = new Point(1636, 451);
            btnActivar.Name = "btnActivar";
            btnActivar.Size = new Size(116, 45);
            btnActivar.TabIndex = 44;
            btnActivar.Text = "Activar";
            btnActivar.UseVisualStyleBackColor = true;
            btnActivar.Click += btnActivar_Click;
            // 
            // BitacoraCambiosForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1764, 573);
            Controls.Add(btnSalir);
            Controls.Add(btnActivar);
            Controls.Add(btnLimpiar);
            Controls.Add(dateTimePickerHasta);
            Controls.Add(lblHasta);
            Controls.Add(dateTimePickerDesde);
            Controls.Add(lblDesde);
            Controls.Add(comboBoxRemitente);
            Controls.Add(comboBoxCodigo);
            Controls.Add(lblRemitente);
            Controls.Add(lblCod);
            Controls.Add(btnAplicar);
            Controls.Add(lblHistorialDeEnvios);
            Controls.Add(dataGridViewHistorial);
            Name = "BitacoraCambiosForm";
            Text = "BitacoraCambiosForm";
            Load += BitacoraCambiosForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewHistorial).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewHistorial;
        private Label lblHistorialDeEnvios;
        private Button btnAplicar;
        private ComboBox comboBoxRemitente;
        private ComboBox comboBoxCodigo;
        private Label lblRemitente;
        private Label lblCod;
        private DateTimePicker dateTimePickerHasta;
        private Label lblHasta;
        private DateTimePicker dateTimePickerDesde;
        private Label lblDesde;
        private Button btnLimpiar;
        private Button btnSalir;
        private Button btnActivar;
    }
}