namespace SistemaDeEnviosGUI
{
    partial class AsignacionRutaForm
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
            dataGridViewRutasPendientes = new DataGridView();
            btnAsignarRuta = new Button();
            lblRutas = new Label();
            dataGridViewRepartidores = new DataGridView();
            lblRepartidores = new Label();
            lblHistorial = new Label();
            dgvHistorialAsignaciones = new DataGridView();
            btnSalir = new Button();
            btnQuitarAsignacion = new Button();
            groupBox1 = new GroupBox();
            rbRutasPendientes = new RadioButton();
            rbRutasTodas = new RadioButton();
            groupBox2 = new GroupBox();
            rbRepartidoresSinRuta = new RadioButton();
            rbRepartidoresTodos = new RadioButton();
            ((System.ComponentModel.ISupportInitialize)dataGridViewRutasPendientes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewRepartidores).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvHistorialAsignaciones).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridViewRutasPendientes
            // 
            dataGridViewRutasPendientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewRutasPendientes.Location = new Point(12, 45);
            dataGridViewRutasPendientes.Name = "dataGridViewRutasPendientes";
            dataGridViewRutasPendientes.Size = new Size(578, 176);
            dataGridViewRutasPendientes.TabIndex = 0;
            // 
            // btnAsignarRuta
            // 
            btnAsignarRuta.Location = new Point(12, 462);
            btnAsignarRuta.Name = "btnAsignarRuta";
            btnAsignarRuta.Size = new Size(113, 46);
            btnAsignarRuta.TabIndex = 1;
            btnAsignarRuta.Text = "Asignar ruta";
            btnAsignarRuta.UseVisualStyleBackColor = true;
            btnAsignarRuta.Click += btnAsignarRuta_Click;
            // 
            // lblRutas
            // 
            lblRutas.AutoSize = true;
            lblRutas.Location = new Point(12, 16);
            lblRutas.Name = "lblRutas";
            lblRutas.Size = new Size(36, 15);
            lblRutas.TabIndex = 2;
            lblRutas.Text = "Rutas";
            // 
            // dataGridViewRepartidores
            // 
            dataGridViewRepartidores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewRepartidores.Location = new Point(12, 280);
            dataGridViewRepartidores.Name = "dataGridViewRepartidores";
            dataGridViewRepartidores.Size = new Size(578, 176);
            dataGridViewRepartidores.TabIndex = 3;
            // 
            // lblRepartidores
            // 
            lblRepartidores.AutoSize = true;
            lblRepartidores.Location = new Point(12, 251);
            lblRepartidores.Name = "lblRepartidores";
            lblRepartidores.Size = new Size(73, 15);
            lblRepartidores.TabIndex = 4;
            lblRepartidores.Text = "Repartidores";
            // 
            // lblHistorial
            // 
            lblHistorial.AutoSize = true;
            lblHistorial.Location = new Point(606, 16);
            lblHistorial.Name = "lblHistorial";
            lblHistorial.Size = new Size(138, 15);
            lblHistorial.TabIndex = 6;
            lblHistorial.Text = "Historial de asignaciones";
            // 
            // dgvHistorialAsignaciones
            // 
            dgvHistorialAsignaciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHistorialAsignaciones.Location = new Point(606, 45);
            dgvHistorialAsignaciones.Name = "dgvHistorialAsignaciones";
            dgvHistorialAsignaciones.Size = new Size(1006, 411);
            dgvHistorialAsignaciones.TabIndex = 5;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(1499, 462);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(113, 46);
            btnSalir.TabIndex = 7;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // btnQuitarAsignacion
            // 
            btnQuitarAsignacion.Location = new Point(131, 462);
            btnQuitarAsignacion.Name = "btnQuitarAsignacion";
            btnQuitarAsignacion.Size = new Size(113, 46);
            btnQuitarAsignacion.TabIndex = 8;
            btnQuitarAsignacion.Text = "Quitar asignación";
            btnQuitarAsignacion.UseVisualStyleBackColor = true;
            btnQuitarAsignacion.Click += btnQuitarAsignacion_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rbRutasPendientes);
            groupBox1.Controls.Add(rbRutasTodas);
            groupBox1.Location = new Point(68, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(233, 39);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            // 
            // rbRutasPendientes
            // 
            rbRutasPendientes.AutoSize = true;
            rbRutasPendientes.Location = new Point(67, 14);
            rbRutasPendientes.Name = "rbRutasPendientes";
            rbRutasPendientes.Size = new Size(159, 19);
            rbRutasPendientes.TabIndex = 1;
            rbRutasPendientes.TabStop = true;
            rbRutasPendientes.Text = "Pendientes de asignación";
            rbRutasPendientes.UseVisualStyleBackColor = true;
            rbRutasPendientes.CheckedChanged += rbRutasPendientes_CheckedChanged;
            // 
            // rbRutasTodas
            // 
            rbRutasTodas.AutoSize = true;
            rbRutasTodas.Location = new Point(6, 14);
            rbRutasTodas.Name = "rbRutasTodas";
            rbRutasTodas.Size = new Size(55, 19);
            rbRutasTodas.TabIndex = 0;
            rbRutasTodas.TabStop = true;
            rbRutasTodas.Text = "Todas";
            rbRutasTodas.UseVisualStyleBackColor = true;
            rbRutasTodas.CheckedChanged += rbRutasTodas_CheckedChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(rbRepartidoresSinRuta);
            groupBox2.Controls.Add(rbRepartidoresTodos);
            groupBox2.Location = new Point(91, 235);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(210, 39);
            groupBox2.TabIndex = 10;
            groupBox2.TabStop = false;
            // 
            // rbRepartidoresSinRuta
            // 
            rbRepartidoresSinRuta.AutoSize = true;
            rbRepartidoresSinRuta.Location = new Point(67, 14);
            rbRepartidoresSinRuta.Name = "rbRepartidoresSinRuta";
            rbRepartidoresSinRuta.Size = new Size(115, 19);
            rbRepartidoresSinRuta.TabIndex = 1;
            rbRepartidoresSinRuta.TabStop = true;
            rbRepartidoresSinRuta.Text = "Sin ruta asignada";
            rbRepartidoresSinRuta.UseVisualStyleBackColor = true;
            rbRepartidoresSinRuta.CheckedChanged += rbRepartidoresSinRuta_CheckedChanged;
            // 
            // rbRepartidoresTodos
            // 
            rbRepartidoresTodos.AutoSize = true;
            rbRepartidoresTodos.Location = new Point(6, 14);
            rbRepartidoresTodos.Name = "rbRepartidoresTodos";
            rbRepartidoresTodos.Size = new Size(56, 19);
            rbRepartidoresTodos.TabIndex = 0;
            rbRepartidoresTodos.TabStop = true;
            rbRepartidoresTodos.Text = "Todos";
            rbRepartidoresTodos.UseVisualStyleBackColor = true;
            rbRepartidoresTodos.CheckedChanged += rbRepartidoresTodos_CheckedChanged;
            // 
            // AsignacionRutaForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1624, 520);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(btnQuitarAsignacion);
            Controls.Add(btnSalir);
            Controls.Add(lblHistorial);
            Controls.Add(dgvHistorialAsignaciones);
            Controls.Add(lblRepartidores);
            Controls.Add(dataGridViewRepartidores);
            Controls.Add(lblRutas);
            Controls.Add(btnAsignarRuta);
            Controls.Add(dataGridViewRutasPendientes);
            Name = "AsignacionRutaForm";
            Text = "AsignacionRutaForm";
            ((System.ComponentModel.ISupportInitialize)dataGridViewRutasPendientes).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewRepartidores).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvHistorialAsignaciones).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewRutasPendientes;
        private Button btnAsignarRuta;
        private Label lblRutas;
        private DataGridView dataGridViewRepartidores;
        private Label lblRepartidores;
        private Label lblHistorial;
        private DataGridView dgvHistorialAsignaciones;
        private Button btnSalir;
        private Button btnQuitarAsignacion;
        private GroupBox groupBox1;
        private RadioButton rbRutasPendientes;
        private RadioButton rbRutasTodas;
        private GroupBox groupBox2;
        private RadioButton rbRepartidoresSinRuta;
        private RadioButton rbRepartidoresTodos;
    }
}