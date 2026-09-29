namespace SistemaDeEnviosGUI
{
    partial class MaestroPersonasForm
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
            btnCancelar = new Button();
            btnAplicar = new Button();
            btnSalir = new Button();
            btnModificar = new Button();
            lblTelefono = new Label();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblApellido = new Label();
            txtApellido = new TextBox();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblDNI = new Label();
            txtDni = new TextBox();
            btnAlta = new Button();
            dataGridViewPersonas = new DataGridView();
            txtTelefono = new TextBox();
            btnBaja = new Button();
            btnReactivar = new Button();
            lblModo = new Label();
            lblModoActual = new Label();
            radioButtonActivos = new RadioButton();
            lblMostrar = new Label();
            radioButtonTodos = new RadioButton();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPersonas).BeginInit();
            SuspendLayout();
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(692, 317);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(143, 42);
            btnCancelar.TabIndex = 54;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnAplicar
            // 
            btnAplicar.Location = new Point(692, 269);
            btnAplicar.Name = "btnAplicar";
            btnAplicar.Size = new Size(143, 42);
            btnAplicar.TabIndex = 53;
            btnAplicar.Text = "Aplicar";
            btnAplicar.UseVisualStyleBackColor = true;
            btnAplicar.Click += btnAplicar_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(692, 439);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(143, 42);
            btnSalir.TabIndex = 48;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(311, 269);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(143, 42);
            btnModificar.TabIndex = 46;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new Point(12, 461);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(55, 15);
            lblTelefono.TabIndex = 43;
            lblTelefono.Text = "Teléfono:";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(12, 432);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(39, 15);
            lblEmail.TabIndex = 42;
            lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(82, 429);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(158, 23);
            txtEmail.TabIndex = 41;
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Location = new Point(12, 403);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(54, 15);
            lblApellido.TabIndex = 40;
            lblApellido.Text = "Apellido:";
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(82, 400);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(158, 23);
            txtApellido.TabIndex = 39;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(12, 374);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(54, 15);
            lblNombre.TabIndex = 38;
            lblNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(82, 371);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(158, 23);
            txtNombre.TabIndex = 37;
            // 
            // lblDNI
            // 
            lblDNI.AutoSize = true;
            lblDNI.Location = new Point(13, 345);
            lblDNI.Name = "lblDNI";
            lblDNI.Size = new Size(30, 15);
            lblDNI.TabIndex = 36;
            lblDNI.Text = "DNI:";
            // 
            // txtDni
            // 
            txtDni.Location = new Point(82, 342);
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(158, 23);
            txtDni.TabIndex = 35;
            // 
            // btnAlta
            // 
            btnAlta.Location = new Point(13, 269);
            btnAlta.Name = "btnAlta";
            btnAlta.Size = new Size(143, 42);
            btnAlta.TabIndex = 34;
            btnAlta.Text = "Alta";
            btnAlta.UseVisualStyleBackColor = true;
            btnAlta.Click += btnAlta_Click;
            // 
            // dataGridViewPersonas
            // 
            dataGridViewPersonas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewPersonas.Location = new Point(12, 12);
            dataGridViewPersonas.MultiSelect = false;
            dataGridViewPersonas.Name = "dataGridViewPersonas";
            dataGridViewPersonas.ReadOnly = true;
            dataGridViewPersonas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewPersonas.Size = new Size(823, 226);
            dataGridViewPersonas.TabIndex = 33;
            dataGridViewPersonas.CellClick += dataGridViewPersonas_CellClick;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(82, 458);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(158, 23);
            txtTelefono.TabIndex = 59;
            // 
            // btnBaja
            // 
            btnBaja.Location = new Point(162, 269);
            btnBaja.Name = "btnBaja";
            btnBaja.Size = new Size(143, 42);
            btnBaja.TabIndex = 60;
            btnBaja.Text = "Baja";
            btnBaja.UseVisualStyleBackColor = true;
            btnBaja.Click += btnBaja_Click;
            // 
            // btnReactivar
            // 
            btnReactivar.Location = new Point(460, 269);
            btnReactivar.Name = "btnReactivar";
            btnReactivar.Size = new Size(143, 42);
            btnReactivar.TabIndex = 61;
            btnReactivar.Text = "Reactivar";
            btnReactivar.UseVisualStyleBackColor = true;
            btnReactivar.Click += btnReactivar_Click;
            // 
            // lblModo
            // 
            lblModo.AutoSize = true;
            lblModo.Location = new Point(394, 246);
            lblModo.Name = "lblModo";
            lblModo.Size = new Size(0, 15);
            lblModo.TabIndex = 66;
            // 
            // lblModoActual
            // 
            lblModoActual.AutoSize = true;
            lblModoActual.Location = new Point(311, 246);
            lblModoActual.Name = "lblModoActual";
            lblModoActual.Size = new Size(77, 15);
            lblModoActual.TabIndex = 65;
            lblModoActual.Text = "Modo actual:";
            // 
            // radioButtonActivos
            // 
            radioButtonActivos.AutoSize = true;
            radioButtonActivos.Checked = true;
            radioButtonActivos.Location = new Point(82, 244);
            radioButtonActivos.Name = "radioButtonActivos";
            radioButtonActivos.Size = new Size(64, 19);
            radioButtonActivos.TabIndex = 64;
            radioButtonActivos.TabStop = true;
            radioButtonActivos.Text = "Activos";
            radioButtonActivos.UseVisualStyleBackColor = true;
            radioButtonActivos.CheckedChanged += radioButtonActivos_CheckedChanged;
            // 
            // lblMostrar
            // 
            lblMostrar.AutoSize = true;
            lblMostrar.Location = new Point(13, 246);
            lblMostrar.Name = "lblMostrar";
            lblMostrar.Size = new Size(51, 15);
            lblMostrar.TabIndex = 63;
            lblMostrar.Text = "Mostrar:";
            // 
            // radioButtonTodos
            // 
            radioButtonTodos.AutoSize = true;
            radioButtonTodos.Location = new Point(160, 244);
            radioButtonTodos.Name = "radioButtonTodos";
            radioButtonTodos.Size = new Size(56, 19);
            radioButtonTodos.TabIndex = 62;
            radioButtonTodos.Text = "Todos";
            radioButtonTodos.UseVisualStyleBackColor = true;
            radioButtonTodos.CheckedChanged += radioButtonTodos_CheckedChanged;
            // 
            // MaestroPersonasForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(847, 497);
            Controls.Add(lblModo);
            Controls.Add(lblModoActual);
            Controls.Add(radioButtonActivos);
            Controls.Add(lblMostrar);
            Controls.Add(radioButtonTodos);
            Controls.Add(btnReactivar);
            Controls.Add(btnBaja);
            Controls.Add(txtTelefono);
            Controls.Add(btnCancelar);
            Controls.Add(btnAplicar);
            Controls.Add(btnSalir);
            Controls.Add(btnModificar);
            Controls.Add(lblTelefono);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(lblApellido);
            Controls.Add(txtApellido);
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(lblDNI);
            Controls.Add(txtDni);
            Controls.Add(btnAlta);
            Controls.Add(dataGridViewPersonas);
            Name = "MaestroPersonasForm";
            Text = "Maestro Personas";
            Load += MaestroPersonasForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewPersonas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnCancelar;
        private Button btnAplicar;
        private Button btnSalir;
        private Button btnModificar;
        private Label lblTelefono;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblApellido;
        private TextBox txtApellido;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblDNI;
        private TextBox txtDni;
        private Button btnAlta;
        private DataGridView dataGridViewPersonas;
        private TextBox txtTelefono;
        private Button btnBaja;
        private Button btnReactivar;
        private Label lblModo;
        private Label lblModoActual;
        private RadioButton radioButtonActivos;
        private Label lblMostrar;
        private RadioButton radioButtonTodos;
    }
}