namespace SistemaDeEnviosGUI
{
    partial class ReporteInteligenteForm
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
            lblReporteInteligente = new Label();
            rbClientesMasFieles = new RadioButton();
            rbClientesConMayorDisminucionDeEnvios = new RadioButton();
            dataGridViewClientes = new DataGridView();
            panel1 = new Panel();
            textBoxMensajeMail = new TextBox();
            btnGenerarPDF = new Button();
            btnPrepararCorreo = new Button();
            btnSalir = new Button();
            btnAyuda = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewClientes).BeginInit();
            SuspendLayout();
            // 
            // lblReporteInteligente
            // 
            lblReporteInteligente.AutoSize = true;
            lblReporteInteligente.Location = new Point(12, 9);
            lblReporteInteligente.Name = "lblReporteInteligente";
            lblReporteInteligente.Size = new Size(197, 15);
            lblReporteInteligente.TabIndex = 0;
            lblReporteInteligente.Text = "Reporte inteligente (últimos 90 días)";
            // 
            // rbClientesMasFieles
            // 
            rbClientesMasFieles.AutoSize = true;
            rbClientesMasFieles.Location = new Point(12, 38);
            rbClientesMasFieles.Name = "rbClientesMasFieles";
            rbClientesMasFieles.Size = new Size(122, 19);
            rbClientesMasFieles.TabIndex = 1;
            rbClientesMasFieles.Text = "Clientes más fieles";
            rbClientesMasFieles.UseVisualStyleBackColor = true;
            // 
            // rbClientesConMayorDisminucionDeEnvios
            // 
            rbClientesConMayorDisminucionDeEnvios.AutoSize = true;
            rbClientesConMayorDisminucionDeEnvios.Location = new Point(160, 38);
            rbClientesConMayorDisminucionDeEnvios.Name = "rbClientesConMayorDisminucionDeEnvios";
            rbClientesConMayorDisminucionDeEnvios.Size = new Size(249, 19);
            rbClientesConMayorDisminucionDeEnvios.TabIndex = 2;
            rbClientesConMayorDisminucionDeEnvios.Text = "Clientes con mayor disminución de envíos";
            rbClientesConMayorDisminucionDeEnvios.UseVisualStyleBackColor = true;
            // 
            // dataGridViewClientes
            // 
            dataGridViewClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewClientes.Location = new Point(13, 76);
            dataGridViewClientes.Name = "dataGridViewClientes";
            dataGridViewClientes.Size = new Size(776, 286);
            dataGridViewClientes.TabIndex = 3;
            // 
            // panel1
            // 
            panel1.Location = new Point(795, 76);
            panel1.Name = "panel1";
            panel1.Size = new Size(304, 280);
            panel1.TabIndex = 4;
            // 
            // textBoxMensajeMail
            // 
            textBoxMensajeMail.Location = new Point(12, 368);
            textBoxMensajeMail.Multiline = true;
            textBoxMensajeMail.Name = "textBoxMensajeMail";
            textBoxMensajeMail.Size = new Size(777, 93);
            textBoxMensajeMail.TabIndex = 5;
            // 
            // btnGenerarPDF
            // 
            btnGenerarPDF.Location = new Point(795, 368);
            btnGenerarPDF.Name = "btnGenerarPDF";
            btnGenerarPDF.Size = new Size(129, 41);
            btnGenerarPDF.TabIndex = 6;
            btnGenerarPDF.Text = "Generar PDF";
            btnGenerarPDF.UseVisualStyleBackColor = true;
            // 
            // btnPrepararCorreo
            // 
            btnPrepararCorreo.Location = new Point(795, 420);
            btnPrepararCorreo.Name = "btnPrepararCorreo";
            btnPrepararCorreo.Size = new Size(129, 41);
            btnPrepararCorreo.TabIndex = 7;
            btnPrepararCorreo.Text = "Preparar correo";
            btnPrepararCorreo.UseVisualStyleBackColor = true;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(993, 420);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(106, 41);
            btnSalir.TabIndex = 9;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            // 
            // btnAyuda
            // 
            btnAyuda.Location = new Point(993, 368);
            btnAyuda.Name = "btnAyuda";
            btnAyuda.Size = new Size(106, 41);
            btnAyuda.TabIndex = 8;
            btnAyuda.Text = "Ayuda";
            btnAyuda.UseVisualStyleBackColor = true;
            btnAyuda.Click += btnAyuda_Click;
            // 
            // ReporteInteligenteForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1111, 473);
            Controls.Add(btnSalir);
            Controls.Add(btnAyuda);
            Controls.Add(btnPrepararCorreo);
            Controls.Add(btnGenerarPDF);
            Controls.Add(textBoxMensajeMail);
            Controls.Add(panel1);
            Controls.Add(dataGridViewClientes);
            Controls.Add(rbClientesConMayorDisminucionDeEnvios);
            Controls.Add(rbClientesMasFieles);
            Controls.Add(lblReporteInteligente);
            Name = "ReporteInteligenteForm";
            Text = "ReporteInteligenteForm";
            ((System.ComponentModel.ISupportInitialize)dataGridViewClientes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblReporteInteligente;
        private RadioButton rbClientesMasFieles;
        private RadioButton rbClientesConMayorDisminucionDeEnvios;
        private DataGridView dataGridViewClientes;
        private Panel panel1;
        private TextBox textBoxMensajeMail;
        private Button btnGenerarPDF;
        private Button btnPrepararCorreo;
        private Button btnSalir;
        private Button btnAyuda;
    }
}