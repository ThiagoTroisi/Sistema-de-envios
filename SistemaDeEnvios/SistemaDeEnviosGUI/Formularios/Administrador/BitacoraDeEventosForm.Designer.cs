namespace SistemaDeEnviosGUI.Formularios.Administrador
{
    partial class BitacoraDeEventosForm
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
            dataGridView1 = new DataGridView();
            txtNombre = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtApellido = new TextBox();
            label4 = new Label();
            dateTimePickerDesde = new DateTimePicker();
            dateTimePickerHasta = new DateTimePicker();
            label5 = new Label();
            label7 = new Label();
            label8 = new Label();
            label3 = new Label();
            label6 = new Label();
            btnAplicar = new Button();
            btnLimpiar = new Button();
            btnImprimir = new Button();
            btnSalir = new Button();
            comboBoxEmail = new ComboBox();
            comboBoxModulo = new ComboBox();
            comboBoxEvento = new ComboBox();
            comboBoxCriticidad = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 12);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(796, 267);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(12, 303);
            txtNombre.Name = "txtNombre";
            txtNombre.ReadOnly = true;
            txtNombre.Size = new Size(129, 23);
            txtNombre.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 285);
            label1.Name = "label1";
            label1.Size = new Size(54, 15);
            label1.TabIndex = 2;
            label1.Text = "Nombre:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(148, 285);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 4;
            label2.Text = "Apellido:";
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(148, 303);
            txtApellido.Name = "txtApellido";
            txtApellido.ReadOnly = true;
            txtApellido.Size = new Size(129, 23);
            txtApellido.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(296, 368);
            label4.Name = "label4";
            label4.Size = new Size(42, 15);
            label4.TabIndex = 8;
            label4.Text = "Desde:";
            // 
            // dateTimePickerDesde
            // 
            dateTimePickerDesde.Checked = false;
            dateTimePickerDesde.Location = new Point(296, 386);
            dateTimePickerDesde.Name = "dateTimePickerDesde";
            dateTimePickerDesde.ShowCheckBox = true;
            dateTimePickerDesde.Size = new Size(229, 23);
            dateTimePickerDesde.TabIndex = 9;
            // 
            // dateTimePickerHasta
            // 
            dateTimePickerHasta.Checked = false;
            dateTimePickerHasta.Location = new Point(579, 386);
            dateTimePickerHasta.Name = "dateTimePickerHasta";
            dateTimePickerHasta.ShowCheckBox = true;
            dateTimePickerHasta.Size = new Size(229, 23);
            dateTimePickerHasta.TabIndex = 11;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(579, 368);
            label5.Name = "label5";
            label5.Size = new Size(40, 15);
            label5.TabIndex = 10;
            label5.Text = "Hasta:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(296, 423);
            label7.Name = "label7";
            label7.Size = new Size(46, 15);
            label7.TabIndex = 15;
            label7.Text = "Evento:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(579, 423);
            label8.Name = "label8";
            label8.Size = new Size(58, 15);
            label8.TabIndex = 17;
            label8.Text = "Criticidad";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 424);
            label3.Name = "label3";
            label3.Size = new Size(52, 15);
            label3.TabIndex = 21;
            label3.Text = "Módulo:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 369);
            label6.Name = "label6";
            label6.Size = new Size(39, 15);
            label6.TabIndex = 18;
            label6.Text = "Email:";
            // 
            // btnAplicar
            // 
            btnAplicar.Location = new Point(12, 481);
            btnAplicar.Name = "btnAplicar";
            btnAplicar.Size = new Size(119, 41);
            btnAplicar.TabIndex = 23;
            btnAplicar.Text = "Aplicar";
            btnAplicar.UseVisualStyleBackColor = true;
            btnAplicar.Click += btnAplicar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(137, 481);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(119, 41);
            btnLimpiar.TabIndex = 24;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnImprimir
            // 
            btnImprimir.Location = new Point(262, 481);
            btnImprimir.Name = "btnImprimir";
            btnImprimir.Size = new Size(119, 41);
            btnImprimir.TabIndex = 25;
            btnImprimir.Text = "Imprimir";
            btnImprimir.UseVisualStyleBackColor = true;
            btnImprimir.Click += btnImprimir_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(689, 481);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(119, 41);
            btnSalir.TabIndex = 26;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // comboBoxEmail
            // 
            comboBoxEmail.FormattingEnabled = true;
            comboBoxEmail.Location = new Point(12, 386);
            comboBoxEmail.Name = "comboBoxEmail";
            comboBoxEmail.Size = new Size(229, 23);
            comboBoxEmail.TabIndex = 27;
            // 
            // comboBoxModulo
            // 
            comboBoxModulo.FormattingEnabled = true;
            comboBoxModulo.Location = new Point(12, 442);
            comboBoxModulo.Name = "comboBoxModulo";
            comboBoxModulo.Size = new Size(229, 23);
            comboBoxModulo.TabIndex = 28;
            // 
            // comboBoxEvento
            // 
            comboBoxEvento.FormattingEnabled = true;
            comboBoxEvento.Location = new Point(296, 442);
            comboBoxEvento.Name = "comboBoxEvento";
            comboBoxEvento.Size = new Size(229, 23);
            comboBoxEvento.TabIndex = 29;
            // 
            // comboBoxCriticidad
            // 
            comboBoxCriticidad.FormattingEnabled = true;
            comboBoxCriticidad.Location = new Point(579, 442);
            comboBoxCriticidad.Name = "comboBoxCriticidad";
            comboBoxCriticidad.Size = new Size(229, 23);
            comboBoxCriticidad.TabIndex = 30;
            // 
            // BitacoraDeEventosForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(820, 543);
            Controls.Add(comboBoxCriticidad);
            Controls.Add(comboBoxEvento);
            Controls.Add(comboBoxModulo);
            Controls.Add(comboBoxEmail);
            Controls.Add(btnSalir);
            Controls.Add(btnImprimir);
            Controls.Add(btnLimpiar);
            Controls.Add(btnAplicar);
            Controls.Add(label3);
            Controls.Add(label6);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(dateTimePickerHasta);
            Controls.Add(label5);
            Controls.Add(dateTimePickerDesde);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(txtApellido);
            Controls.Add(label1);
            Controls.Add(txtNombre);
            Controls.Add(dataGridView1);
            Name = "BitacoraDeEventosForm";
            Text = "Bitacora de eventos";
            Load += BitacoraDeEventosForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private TextBox txtNombre;
        private Label label1;
        private Label label2;
        private TextBox txtApellido;
        private Label label4;
        private DateTimePicker dateTimePickerDesde;
        private DateTimePicker dateTimePickerHasta;
        private Label label5;
        private Label label7;
        private Label label8;
        private Label label3;
        private Label label6;
        private Button btnAplicar;
        private Button btnLimpiar;
        private Button btnImprimir;
        private Button btnSalir;
        private ComboBox comboBoxEmail;
        private ComboBox comboBoxModulo;
        private ComboBox comboBoxEvento;
        private ComboBox comboBoxCriticidad;
    }
}