namespace SistemaDeEnviosGUI
{
    partial class ErrorIntegridadForm
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
            dgvRegistros = new DataGridView();
            btnRecalcularDV = new Button();
            lblOpcion = new Label();
            btnRealizarRestore = new Button();
            listBoxTablas = new ListBox();
            lblTablasAfectadas = new Label();
            lblRegistrosAfectados = new Label();
            btnSalir = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvRegistros).BeginInit();
            SuspendLayout();
            // 
            // dgvRegistros
            // 
            dgvRegistros.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRegistros.Location = new Point(138, 27);
            dgvRegistros.Name = "dgvRegistros";
            dgvRegistros.Size = new Size(475, 199);
            dgvRegistros.TabIndex = 0;
            // 
            // btnRecalcularDV
            // 
            btnRecalcularDV.Location = new Point(12, 254);
            btnRecalcularDV.Name = "btnRecalcularDV";
            btnRecalcularDV.Size = new Size(95, 50);
            btnRecalcularDV.TabIndex = 3;
            btnRecalcularDV.Text = "Recalcular DVH y DVV";
            btnRecalcularDV.UseVisualStyleBackColor = true;
            btnRecalcularDV.Click += btnRecalcularDV_Click;
            // 
            // lblOpcion
            // 
            lblOpcion.AutoSize = true;
            lblOpcion.Location = new Point(12, 236);
            lblOpcion.Name = "lblOpcion";
            lblOpcion.Size = new Size(132, 15);
            lblOpcion.TabIndex = 4;
            lblOpcion.Text = "Seleccione una opción: ";
            // 
            // btnRealizarRestore
            // 
            btnRealizarRestore.Location = new Point(113, 254);
            btnRealizarRestore.Name = "btnRealizarRestore";
            btnRealizarRestore.Size = new Size(95, 50);
            btnRealizarRestore.TabIndex = 5;
            btnRealizarRestore.Text = "Realizar restore";
            btnRealizarRestore.UseVisualStyleBackColor = true;
            btnRealizarRestore.Click += btnRealizarRestore_Click;
            // 
            // listBoxTablas
            // 
            listBoxTablas.FormattingEnabled = true;
            listBoxTablas.ItemHeight = 15;
            listBoxTablas.Location = new Point(12, 27);
            listBoxTablas.Name = "listBoxTablas";
            listBoxTablas.Size = new Size(120, 199);
            listBoxTablas.TabIndex = 6;
            listBoxTablas.SelectedIndexChanged += listBoxTablas_SelectedIndexChanged;
            // 
            // lblTablasAfectadas
            // 
            lblTablasAfectadas.AutoSize = true;
            lblTablasAfectadas.Location = new Point(12, 9);
            lblTablasAfectadas.Name = "lblTablasAfectadas";
            lblTablasAfectadas.Size = new Size(95, 15);
            lblTablasAfectadas.TabIndex = 7;
            lblTablasAfectadas.Text = "Tablas afectadas:";
            // 
            // lblRegistrosAfectados
            // 
            lblRegistrosAfectados.AutoSize = true;
            lblRegistrosAfectados.Location = new Point(138, 9);
            lblRegistrosAfectados.Name = "lblRegistrosAfectados";
            lblRegistrosAfectados.Size = new Size(112, 15);
            lblRegistrosAfectados.TabIndex = 8;
            lblRegistrosAfectados.Text = "Registros afectados:";
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(518, 254);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(95, 50);
            btnSalir.TabIndex = 9;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // ErrorIntegridadForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(625, 316);
            Controls.Add(btnSalir);
            Controls.Add(lblRegistrosAfectados);
            Controls.Add(lblTablasAfectadas);
            Controls.Add(listBoxTablas);
            Controls.Add(btnRealizarRestore);
            Controls.Add(lblOpcion);
            Controls.Add(btnRecalcularDV);
            Controls.Add(dgvRegistros);
            Name = "ErrorIntegridadForm";
            Text = "ErrorIntegridadForm";
            Load += ErrorIntegridadForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvRegistros).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvRegistros;
        private Button btnRecalcularDV;
        private Label lblOpcion;
        private Button btnRealizarRestore;
        private ListBox listBoxTablas;
        private Label lblTablasAfectadas;
        private Label lblRegistrosAfectados;
        private Button btnSalir;
    }
}