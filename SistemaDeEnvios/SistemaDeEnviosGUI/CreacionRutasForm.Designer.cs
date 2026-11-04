namespace SistemaDeEnviosGUI
{
    partial class CreacionRutasForm
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
            dataGridViewEnviosPendientes = new DataGridView();
            lblEnviosPendientes = new Label();
            lblRutas = new Label();
            dataGridViewRutas = new DataGridView();
            btnCrearRuta = new Button();
            btnCancelarRuta = new Button();
            btnQuitarEnvios = new Button();
            btnAgregarEnvios = new Button();
            lblEnviosDeRuta = new Label();
            dataGridViewEnviosDeRuta = new DataGridView();
            btnBajarOrden = new Button();
            btnSubirOrden = new Button();
            lblOrden = new Label();
            lblDescripcionRuta = new Label();
            txtDescripcionRuta = new TextBox();
            lblEnviosResolucion = new Label();
            btnResolverEnvios = new Button();
            lblValorEnviosResolucion = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEnviosPendientes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewRutas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEnviosDeRuta).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewEnviosPendientes
            // 
            dataGridViewEnviosPendientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewEnviosPendientes.Location = new Point(12, 27);
            dataGridViewEnviosPendientes.Name = "dataGridViewEnviosPendientes";
            dataGridViewEnviosPendientes.Size = new Size(538, 354);
            dataGridViewEnviosPendientes.TabIndex = 0;
            dataGridViewEnviosPendientes.SelectionChanged += dataGridViewEnviosPendientes_SelectionChanged;
            // 
            // lblEnviosPendientes
            // 
            lblEnviosPendientes.AutoSize = true;
            lblEnviosPendientes.Location = new Point(12, 9);
            lblEnviosPendientes.Name = "lblEnviosPendientes";
            lblEnviosPendientes.Size = new Size(178, 15);
            lblEnviosPendientes.TabIndex = 1;
            lblEnviosPendientes.Text = "Envios pendientes de asignación";
            // 
            // lblRutas
            // 
            lblRutas.AutoSize = true;
            lblRutas.Location = new Point(585, 9);
            lblRutas.Name = "lblRutas";
            lblRutas.Size = new Size(79, 15);
            lblRutas.TabIndex = 5;
            lblRutas.Text = "Rutas creadas";
            // 
            // dataGridViewRutas
            // 
            dataGridViewRutas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewRutas.Location = new Point(585, 27);
            dataGridViewRutas.Name = "dataGridViewRutas";
            dataGridViewRutas.Size = new Size(538, 354);
            dataGridViewRutas.TabIndex = 4;
            dataGridViewRutas.SelectionChanged += dataGridViewRutas_SelectionChanged_1;
            // 
            // btnCrearRuta
            // 
            btnCrearRuta.Location = new Point(12, 387);
            btnCrearRuta.Name = "btnCrearRuta";
            btnCrearRuta.Size = new Size(122, 41);
            btnCrearRuta.TabIndex = 6;
            btnCrearRuta.Text = "Crear ruta";
            btnCrearRuta.UseVisualStyleBackColor = true;
            btnCrearRuta.Click += btnCrearRuta_Click;
            // 
            // btnCancelarRuta
            // 
            btnCancelarRuta.Location = new Point(585, 387);
            btnCancelarRuta.Name = "btnCancelarRuta";
            btnCancelarRuta.Size = new Size(122, 41);
            btnCancelarRuta.TabIndex = 7;
            btnCancelarRuta.Text = "Cancelar ruta";
            btnCancelarRuta.UseVisualStyleBackColor = true;
            btnCancelarRuta.Click += btnCancelarRuta_Click;
            // 
            // btnQuitarEnvios
            // 
            btnQuitarEnvios.Location = new Point(713, 387);
            btnQuitarEnvios.Name = "btnQuitarEnvios";
            btnQuitarEnvios.Size = new Size(122, 41);
            btnQuitarEnvios.TabIndex = 11;
            btnQuitarEnvios.Text = "Quitar envío(s)";
            btnQuitarEnvios.UseVisualStyleBackColor = true;
            btnQuitarEnvios.Click += btnQuitarEnvios_Click;
            // 
            // btnAgregarEnvios
            // 
            btnAgregarEnvios.Location = new Point(428, 387);
            btnAgregarEnvios.Name = "btnAgregarEnvios";
            btnAgregarEnvios.Size = new Size(122, 41);
            btnAgregarEnvios.TabIndex = 10;
            btnAgregarEnvios.Text = "Agregar envío(s)";
            btnAgregarEnvios.UseVisualStyleBackColor = true;
            btnAgregarEnvios.Click += btnAgregarEnvios_Click;
            // 
            // lblEnviosDeRuta
            // 
            lblEnviosDeRuta.AutoSize = true;
            lblEnviosDeRuta.Location = new Point(1159, 9);
            lblEnviosDeRuta.Name = "lblEnviosDeRuta";
            lblEnviosDeRuta.Size = new Size(164, 15);
            lblEnviosDeRuta.TabIndex = 9;
            lblEnviosDeRuta.Text = "Envíos de la ruta seleccionada";
            // 
            // dataGridViewEnviosDeRuta
            // 
            dataGridViewEnviosDeRuta.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewEnviosDeRuta.Location = new Point(1159, 27);
            dataGridViewEnviosDeRuta.Name = "dataGridViewEnviosDeRuta";
            dataGridViewEnviosDeRuta.Size = new Size(538, 354);
            dataGridViewEnviosDeRuta.TabIndex = 8;
            // 
            // btnBajarOrden
            // 
            btnBajarOrden.Location = new Point(1346, 387);
            btnBajarOrden.Name = "btnBajarOrden";
            btnBajarOrden.Size = new Size(122, 41);
            btnBajarOrden.TabIndex = 13;
            btnBajarOrden.Text = "Bajar";
            btnBajarOrden.UseVisualStyleBackColor = true;
            btnBajarOrden.Click += btnBajarOrden_Click;
            // 
            // btnSubirOrden
            // 
            btnSubirOrden.Location = new Point(1218, 387);
            btnSubirOrden.Name = "btnSubirOrden";
            btnSubirOrden.Size = new Size(122, 41);
            btnSubirOrden.TabIndex = 12;
            btnSubirOrden.Text = "Subir";
            btnSubirOrden.UseVisualStyleBackColor = true;
            btnSubirOrden.Click += btnSubirOrden_Click;
            // 
            // lblOrden
            // 
            lblOrden.AutoSize = true;
            lblOrden.Location = new Point(1159, 400);
            lblOrden.Name = "lblOrden";
            lblOrden.Size = new Size(43, 15);
            lblOrden.TabIndex = 14;
            lblOrden.Text = "Orden:";
            // 
            // lblDescripcionRuta
            // 
            lblDescripcionRuta.AutoSize = true;
            lblDescripcionRuta.Location = new Point(140, 387);
            lblDescripcionRuta.Name = "lblDescripcionRuta";
            lblDescripcionRuta.Size = new Size(166, 15);
            lblDescripcionRuta.TabIndex = 15;
            lblDescripcionRuta.Text = "Descripción de ruta (opcional)";
            // 
            // txtDescripcionRuta
            // 
            txtDescripcionRuta.Location = new Point(140, 405);
            txtDescripcionRuta.Name = "txtDescripcionRuta";
            txtDescripcionRuta.Size = new Size(250, 23);
            txtDescripcionRuta.TabIndex = 16;
            // 
            // lblEnviosResolucion
            // 
            lblEnviosResolucion.AutoSize = true;
            lblEnviosResolucion.Location = new Point(12, 454);
            lblEnviosResolucion.Name = "lblEnviosResolucion";
            lblEnviosResolucion.Size = new Size(178, 15);
            lblEnviosResolucion.TabIndex = 17;
            lblEnviosResolucion.Text = "Envíos que requieren resolución:";
            // 
            // btnResolverEnvios
            // 
            btnResolverEnvios.Location = new Point(12, 472);
            btnResolverEnvios.Name = "btnResolverEnvios";
            btnResolverEnvios.Size = new Size(122, 41);
            btnResolverEnvios.TabIndex = 18;
            btnResolverEnvios.Text = "Resolver envíos";
            btnResolverEnvios.UseVisualStyleBackColor = true;
            btnResolverEnvios.Click += btnResolverEnvios_Click;
            // 
            // lblValorEnviosResolucion
            // 
            lblValorEnviosResolucion.AutoSize = true;
            lblValorEnviosResolucion.Location = new Point(196, 454);
            lblValorEnviosResolucion.Name = "lblValorEnviosResolucion";
            lblValorEnviosResolucion.Size = new Size(13, 15);
            lblValorEnviosResolucion.TabIndex = 19;
            lblValorEnviosResolucion.Text = "0";
            // 
            // CreacionRutasForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1709, 525);
            Controls.Add(lblValorEnviosResolucion);
            Controls.Add(btnResolverEnvios);
            Controls.Add(lblEnviosResolucion);
            Controls.Add(txtDescripcionRuta);
            Controls.Add(lblDescripcionRuta);
            Controls.Add(lblOrden);
            Controls.Add(btnBajarOrden);
            Controls.Add(btnSubirOrden);
            Controls.Add(btnQuitarEnvios);
            Controls.Add(btnAgregarEnvios);
            Controls.Add(lblEnviosDeRuta);
            Controls.Add(dataGridViewEnviosDeRuta);
            Controls.Add(btnCancelarRuta);
            Controls.Add(btnCrearRuta);
            Controls.Add(lblRutas);
            Controls.Add(dataGridViewRutas);
            Controls.Add(lblEnviosPendientes);
            Controls.Add(dataGridViewEnviosPendientes);
            Name = "CreacionRutasForm";
            Text = "CreacionRutasForm";
            ((System.ComponentModel.ISupportInitialize)dataGridViewEnviosPendientes).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewRutas).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEnviosDeRuta).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewEnviosPendientes;
        private Label lblEnviosPendientes;
        private Label lblRutas;
        private DataGridView dataGridViewRutas;
        private Button btnCrearRuta;
        private Button btnCancelarRuta;
        private Button btnQuitarEnvios;
        private Button btnAgregarEnvios;
        private Label lblEnviosDeRuta;
        private DataGridView dataGridViewEnviosDeRuta;
        private Button btnBajarOrden;
        private Button btnSubirOrden;
        private Label lblOrden;
        private Label lblDescripcionRuta;
        private TextBox txtDescripcionRuta;
        private Label lblEnviosResolucion;
        private Button btnResolverEnvios;
        private Label lblValorEnviosResolucion;
    }
}