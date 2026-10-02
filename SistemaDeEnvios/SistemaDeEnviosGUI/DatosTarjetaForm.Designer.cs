namespace SistemaDeEnviosGUI
{
    partial class DatosTarjetaForm
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
            lblEnvioSeleccionado = new Label();
            txtEnvioSeleccionado = new TextBox();
            txtImporte = new TextBox();
            lblImporteAPagar = new Label();
            txtNombreTarjeta = new TextBox();
            lblNombreTarjeta = new Label();
            txtNumeroTarjeta = new TextBox();
            lblNumeroTarjeta = new Label();
            txtCodigoSeguridad = new TextBox();
            lblCodigoSeguridad = new Label();
            lblVencimiento = new Label();
            cboMes = new ComboBox();
            cboAño = new ComboBox();
            btnPagar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            // 
            // lblEnvioSeleccionado
            // 
            lblEnvioSeleccionado.AutoSize = true;
            lblEnvioSeleccionado.Location = new Point(12, 18);
            lblEnvioSeleccionado.Name = "lblEnvioSeleccionado";
            lblEnvioSeleccionado.Size = new Size(111, 15);
            lblEnvioSeleccionado.TabIndex = 0;
            lblEnvioSeleccionado.Text = "Envío seleccionado:";
            // 
            // txtEnvioSeleccionado
            // 
            txtEnvioSeleccionado.Location = new Point(142, 15);
            txtEnvioSeleccionado.Name = "txtEnvioSeleccionado";
            txtEnvioSeleccionado.ReadOnly = true;
            txtEnvioSeleccionado.Size = new Size(158, 23);
            txtEnvioSeleccionado.TabIndex = 1;
            // 
            // txtImporte
            // 
            txtImporte.Location = new Point(142, 44);
            txtImporte.Name = "txtImporte";
            txtImporte.ReadOnly = true;
            txtImporte.Size = new Size(158, 23);
            txtImporte.TabIndex = 3;
            // 
            // lblImporteAPagar
            // 
            lblImporteAPagar.AutoSize = true;
            lblImporteAPagar.Location = new Point(12, 47);
            lblImporteAPagar.Name = "lblImporteAPagar";
            lblImporteAPagar.Size = new Size(94, 15);
            lblImporteAPagar.TabIndex = 2;
            lblImporteAPagar.Text = "Importe a pagar:";
            // 
            // txtNombreTarjeta
            // 
            txtNombreTarjeta.Location = new Point(12, 119);
            txtNombreTarjeta.Name = "txtNombreTarjeta";
            txtNombreTarjeta.Size = new Size(288, 23);
            txtNombreTarjeta.TabIndex = 5;
            // 
            // lblNombreTarjeta
            // 
            lblNombreTarjeta.AutoSize = true;
            lblNombreTarjeta.Location = new Point(12, 101);
            lblNombreTarjeta.Name = "lblNombreTarjeta";
            lblNombreTarjeta.Size = new Size(118, 15);
            lblNombreTarjeta.TabIndex = 4;
            lblNombreTarjeta.Text = "Nombre en la tarjeta:";
            // 
            // txtNumeroTarjeta
            // 
            txtNumeroTarjeta.Location = new Point(12, 172);
            txtNumeroTarjeta.Name = "txtNumeroTarjeta";
            txtNumeroTarjeta.Size = new Size(288, 23);
            txtNumeroTarjeta.TabIndex = 7;
            // 
            // lblNumeroTarjeta
            // 
            lblNumeroTarjeta.AutoSize = true;
            lblNumeroTarjeta.Location = new Point(12, 154);
            lblNumeroTarjeta.Name = "lblNumeroTarjeta";
            lblNumeroTarjeta.Size = new Size(106, 15);
            lblNumeroTarjeta.TabIndex = 6;
            lblNumeroTarjeta.Text = "Número de tarjeta:";
            // 
            // txtCodigoSeguridad
            // 
            txtCodigoSeguridad.Location = new Point(138, 255);
            txtCodigoSeguridad.Name = "txtCodigoSeguridad";
            txtCodigoSeguridad.Size = new Size(68, 23);
            txtCodigoSeguridad.TabIndex = 9;
            // 
            // lblCodigoSeguridad
            // 
            lblCodigoSeguridad.AutoSize = true;
            lblCodigoSeguridad.Location = new Point(12, 258);
            lblCodigoSeguridad.Name = "lblCodigoSeguridad";
            lblCodigoSeguridad.Size = new Size(120, 15);
            lblCodigoSeguridad.TabIndex = 8;
            lblCodigoSeguridad.Text = "Código de seguridad:";
            // 
            // lblVencimiento
            // 
            lblVencimiento.AutoSize = true;
            lblVencimiento.Location = new Point(12, 215);
            lblVencimiento.Name = "lblVencimiento";
            lblVencimiento.Size = new Size(76, 15);
            lblVencimiento.TabIndex = 10;
            lblVencimiento.Text = "Vencimiento:";
            // 
            // cboMes
            // 
            cboMes.FormattingEnabled = true;
            cboMes.Location = new Point(94, 212);
            cboMes.Name = "cboMes";
            cboMes.Size = new Size(53, 23);
            cboMes.TabIndex = 11;
            // 
            // cboAño
            // 
            cboAño.FormattingEnabled = true;
            cboAño.Location = new Point(153, 212);
            cboAño.Name = "cboAño";
            cboAño.Size = new Size(53, 23);
            cboAño.TabIndex = 12;
            // 
            // btnPagar
            // 
            btnPagar.Location = new Point(52, 310);
            btnPagar.Name = "btnPagar";
            btnPagar.Size = new Size(101, 41);
            btnPagar.TabIndex = 13;
            btnPagar.Text = "Pagar";
            btnPagar.UseVisualStyleBackColor = true;
            btnPagar.Click += btnPagar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(159, 310);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(101, 41);
            btnCancelar.TabIndex = 14;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // DatosTarjetaForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(312, 363);
            Controls.Add(btnCancelar);
            Controls.Add(btnPagar);
            Controls.Add(cboAño);
            Controls.Add(cboMes);
            Controls.Add(lblVencimiento);
            Controls.Add(txtCodigoSeguridad);
            Controls.Add(lblCodigoSeguridad);
            Controls.Add(txtNumeroTarjeta);
            Controls.Add(lblNumeroTarjeta);
            Controls.Add(txtNombreTarjeta);
            Controls.Add(lblNombreTarjeta);
            Controls.Add(txtImporte);
            Controls.Add(lblImporteAPagar);
            Controls.Add(txtEnvioSeleccionado);
            Controls.Add(lblEnvioSeleccionado);
            Name = "DatosTarjetaForm";
            Text = "DatosTarjetaForm";
            Load += DatosTarjetaForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblEnvioSeleccionado;
        private TextBox txtEnvioSeleccionado;
        private TextBox txtImporte;
        private Label lblImporteAPagar;
        private TextBox txtNombreTarjeta;
        private Label lblNombreTarjeta;
        private TextBox txtNumeroTarjeta;
        private Label lblNumeroTarjeta;
        private TextBox txtCodigoSeguridad;
        private Label lblCodigoSeguridad;
        private Label lblVencimiento;
        private ComboBox cboMes;
        private ComboBox cboAño;
        private Button btnPagar;
        private Button btnCancelar;
    }
}