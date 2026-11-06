namespace SistemaDeEnviosGUI
{
    partial class RepartidorForm
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
            dataGridViewEnviosDeLaRuta = new DataGridView();
            lblBienvenida = new Label();
            grpDatosRuta = new GroupBox();
            btnRegistrarEntregas = new Button();
            btnFinalizarRuta = new Button();
            btnImprimirHojaRuta = new Button();
            btnIniciarRuta = new Button();
            pbProgreso = new ProgressBar();
            lblCantidadProgreso = new Label();
            lblValorFechaAsignacion = new Label();
            lblValorEstadoRuta = new Label();
            lblValorNumeroRuta = new Label();
            lblProgreso = new Label();
            lblFechaAsignacion = new Label();
            lblEstadoRuta = new Label();
            lblNumeroRuta = new Label();
            lblEnviosDeLaRuta = new Label();
            dataGridViewHistorial = new DataGridView();
            lblHistorial = new Label();
            btnImprimirInforme = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEnviosDeLaRuta).BeginInit();
            grpDatosRuta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewHistorial).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewEnviosDeLaRuta
            // 
            dataGridViewEnviosDeLaRuta.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewEnviosDeLaRuta.Location = new Point(12, 271);
            dataGridViewEnviosDeLaRuta.Name = "dataGridViewEnviosDeLaRuta";
            dataGridViewEnviosDeLaRuta.Size = new Size(502, 252);
            dataGridViewEnviosDeLaRuta.TabIndex = 0;
            // 
            // lblBienvenida
            // 
            lblBienvenida.AutoSize = true;
            lblBienvenida.Location = new Point(12, 9);
            lblBienvenida.Name = "lblBienvenida";
            lblBienvenida.Size = new Size(81, 15);
            lblBienvenida.TabIndex = 1;
            lblBienvenida.Text = "Bienvenido, ...";
            // 
            // grpDatosRuta
            // 
            grpDatosRuta.Controls.Add(btnRegistrarEntregas);
            grpDatosRuta.Controls.Add(btnFinalizarRuta);
            grpDatosRuta.Controls.Add(btnImprimirHojaRuta);
            grpDatosRuta.Controls.Add(btnIniciarRuta);
            grpDatosRuta.Controls.Add(pbProgreso);
            grpDatosRuta.Controls.Add(lblCantidadProgreso);
            grpDatosRuta.Controls.Add(lblValorFechaAsignacion);
            grpDatosRuta.Controls.Add(lblValorEstadoRuta);
            grpDatosRuta.Controls.Add(lblValorNumeroRuta);
            grpDatosRuta.Controls.Add(lblProgreso);
            grpDatosRuta.Controls.Add(lblFechaAsignacion);
            grpDatosRuta.Controls.Add(lblEstadoRuta);
            grpDatosRuta.Controls.Add(lblNumeroRuta);
            grpDatosRuta.Location = new Point(12, 42);
            grpDatosRuta.Name = "grpDatosRuta";
            grpDatosRuta.Size = new Size(502, 199);
            grpDatosRuta.TabIndex = 11;
            grpDatosRuta.TabStop = false;
            grpDatosRuta.Text = "Ruta actual";
            // 
            // btnRegistrarEntregas
            // 
            btnRegistrarEntregas.Location = new Point(254, 151);
            btnRegistrarEntregas.Name = "btnRegistrarEntregas";
            btnRegistrarEntregas.Size = new Size(118, 42);
            btnRegistrarEntregas.TabIndex = 16;
            btnRegistrarEntregas.Text = "Registrar entregas";
            btnRegistrarEntregas.UseVisualStyleBackColor = true;
            btnRegistrarEntregas.Click += btnRegistrarEntregas_Click;
            // 
            // btnFinalizarRuta
            // 
            btnFinalizarRuta.Location = new Point(378, 151);
            btnFinalizarRuta.Name = "btnFinalizarRuta";
            btnFinalizarRuta.Size = new Size(118, 42);
            btnFinalizarRuta.TabIndex = 15;
            btnFinalizarRuta.Text = "Finalizar ruta";
            btnFinalizarRuta.UseVisualStyleBackColor = true;
            btnFinalizarRuta.Click += btnFinalizarRuta_Click;
            // 
            // btnImprimirHojaRuta
            // 
            btnImprimirHojaRuta.Location = new Point(130, 151);
            btnImprimirHojaRuta.Name = "btnImprimirHojaRuta";
            btnImprimirHojaRuta.Size = new Size(118, 42);
            btnImprimirHojaRuta.TabIndex = 14;
            btnImprimirHojaRuta.Text = "Imprimir hoja de ruta";
            btnImprimirHojaRuta.UseVisualStyleBackColor = true;
            btnImprimirHojaRuta.Click += btnImprimirHojaRuta_Click;
            // 
            // btnIniciarRuta
            // 
            btnIniciarRuta.Location = new Point(6, 151);
            btnIniciarRuta.Name = "btnIniciarRuta";
            btnIniciarRuta.Size = new Size(118, 42);
            btnIniciarRuta.TabIndex = 13;
            btnIniciarRuta.Text = "Iniciar ruta";
            btnIniciarRuta.UseVisualStyleBackColor = true;
            btnIniciarRuta.Click += btnIniciarRuta_Click;
            // 
            // pbProgreso
            // 
            pbProgreso.Location = new Point(6, 89);
            pbProgreso.Name = "pbProgreso";
            pbProgreso.Size = new Size(490, 23);
            pbProgreso.TabIndex = 12;
            // 
            // lblCantidadProgreso
            // 
            lblCantidadProgreso.AutoSize = true;
            lblCantidadProgreso.Location = new Point(6, 124);
            lblCantidadProgreso.Name = "lblCantidadProgreso";
            lblCantidadProgreso.Size = new Size(75, 15);
            lblCantidadProgreso.TabIndex = 11;
            lblCantidadProgreso.Text = "0 de 0 envíos";
            // 
            // lblValorFechaAsignacion
            // 
            lblValorFechaAsignacion.AutoSize = true;
            lblValorFechaAsignacion.Location = new Point(392, 28);
            lblValorFechaAsignacion.Name = "lblValorFechaAsignacion";
            lblValorFechaAsignacion.Size = new Size(13, 15);
            lblValorFechaAsignacion.TabIndex = 9;
            lblValorFechaAsignacion.Text = "0";
            // 
            // lblValorEstadoRuta
            // 
            lblValorEstadoRuta.AutoSize = true;
            lblValorEstadoRuta.Location = new Point(160, 28);
            lblValorEstadoRuta.Name = "lblValorEstadoRuta";
            lblValorEstadoRuta.Size = new Size(13, 15);
            lblValorEstadoRuta.TabIndex = 8;
            lblValorEstadoRuta.Text = "0";
            // 
            // lblValorNumeroRuta
            // 
            lblValorNumeroRuta.AutoSize = true;
            lblValorNumeroRuta.Location = new Point(46, 28);
            lblValorNumeroRuta.Name = "lblValorNumeroRuta";
            lblValorNumeroRuta.Size = new Size(13, 15);
            lblValorNumeroRuta.TabIndex = 7;
            lblValorNumeroRuta.Text = "0";
            // 
            // lblProgreso
            // 
            lblProgreso.AutoSize = true;
            lblProgreso.Location = new Point(6, 62);
            lblProgreso.Name = "lblProgreso";
            lblProgreso.Size = new Size(57, 15);
            lblProgreso.TabIndex = 6;
            lblProgreso.Text = "Progreso:";
            // 
            // lblFechaAsignacion
            // 
            lblFechaAsignacion.AutoSize = true;
            lblFechaAsignacion.Location = new Point(269, 28);
            lblFechaAsignacion.Name = "lblFechaAsignacion";
            lblFechaAsignacion.Size = new Size(117, 15);
            lblFechaAsignacion.TabIndex = 5;
            lblFechaAsignacion.Text = "\tFecha de asignación:";
            // 
            // lblEstadoRuta
            // 
            lblEstadoRuta.AutoSize = true;
            lblEstadoRuta.Location = new Point(109, 28);
            lblEstadoRuta.Name = "lblEstadoRuta";
            lblEstadoRuta.Size = new Size(45, 15);
            lblEstadoRuta.TabIndex = 4;
            lblEstadoRuta.Text = "Estado:";
            // 
            // lblNumeroRuta
            // 
            lblNumeroRuta.AutoSize = true;
            lblNumeroRuta.Location = new Point(6, 28);
            lblNumeroRuta.Name = "lblNumeroRuta";
            lblNumeroRuta.Size = new Size(34, 15);
            lblNumeroRuta.TabIndex = 3;
            lblNumeroRuta.Text = "Ruta:";
            // 
            // lblEnviosDeLaRuta
            // 
            lblEnviosDeLaRuta.AutoSize = true;
            lblEnviosDeLaRuta.Location = new Point(11, 253);
            lblEnviosDeLaRuta.Name = "lblEnviosDeLaRuta";
            lblEnviosDeLaRuta.Size = new Size(93, 15);
            lblEnviosDeLaRuta.TabIndex = 16;
            lblEnviosDeLaRuta.Text = "Envíos de la ruta";
            // 
            // dataGridViewHistorial
            // 
            dataGridViewHistorial.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewHistorial.Location = new Point(578, 27);
            dataGridViewHistorial.Name = "dataGridViewHistorial";
            dataGridViewHistorial.Size = new Size(667, 448);
            dataGridViewHistorial.TabIndex = 17;
            // 
            // lblHistorial
            // 
            lblHistorial.AutoSize = true;
            lblHistorial.Location = new Point(578, 9);
            lblHistorial.Name = "lblHistorial";
            lblHistorial.Size = new Size(96, 15);
            lblHistorial.TabIndex = 18;
            lblHistorial.Text = "Historial de rutas";
            // 
            // btnImprimirInforme
            // 
            btnImprimirInforme.Location = new Point(578, 481);
            btnImprimirInforme.Name = "btnImprimirInforme";
            btnImprimirInforme.Size = new Size(118, 42);
            btnImprimirInforme.TabIndex = 17;
            btnImprimirInforme.Text = "Imprimir informe";
            btnImprimirInforme.UseVisualStyleBackColor = true;
            btnImprimirInforme.Click += btnImprimirInforme_Click;
            // 
            // RepartidorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1257, 535);
            Controls.Add(btnImprimirInforme);
            Controls.Add(lblHistorial);
            Controls.Add(dataGridViewHistorial);
            Controls.Add(lblEnviosDeLaRuta);
            Controls.Add(grpDatosRuta);
            Controls.Add(lblBienvenida);
            Controls.Add(dataGridViewEnviosDeLaRuta);
            Name = "RepartidorForm";
            Text = "RepartidorForm";
            Load += RepartidorForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewEnviosDeLaRuta).EndInit();
            grpDatosRuta.ResumeLayout(false);
            grpDatosRuta.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewHistorial).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewEnviosDeLaRuta;
        private Label lblBienvenida;
        private GroupBox grpDatosRuta;
        private Label lblValorFechaAsignacion;
        private Label lblValorEstadoRuta;
        private Label lblValorNumeroRuta;
        private Label lblProgreso;
        private Label lblFechaAsignacion;
        private Label lblEstadoRuta;
        private Label lblNumeroRuta;
        private Button btnFinalizarRuta;
        private Button btnImprimirHojaRuta;
        private Button btnIniciarRuta;
        private ProgressBar pbProgreso;
        private Label lblCantidadProgreso;
        private Label lblEnviosDeLaRuta;
        private Button btnRegistrarEntregas;
        private DataGridView dataGridViewHistorial;
        private Label lblHistorial;
        private Button btnImprimirInforme;
    }
}