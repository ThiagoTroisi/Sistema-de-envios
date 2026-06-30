namespace SistemaDeEnviosGUI.Formularios.Administrador
{
    partial class GestionUsuariosForm
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
            dataGridViewUsuarios = new DataGridView();
            btnAlta = new Button();
            txtDni = new TextBox();
            lblDNI = new Label();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblApellido = new Label();
            txtApellido = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblPerfil = new Label();
            btnActivar = new Button();
            btnDesactivar = new Button();
            btnModificar = new Button();
            comboBoxPerfil = new ComboBox();
            btnSalir = new Button();
            btnDesbloquear = new Button();
            radioButtonTodos = new RadioButton();
            lblMostrar = new Label();
            radioButtonActivos = new RadioButton();
            btnAplicar = new Button();
            btnCancelar = new Button();
            checkBoxBloqueado = new CheckBox();
            checkBoxActivo = new CheckBox();
            lblModoActual = new Label();
            lblModo = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewUsuarios).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewUsuarios
            // 
            dataGridViewUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewUsuarios.Location = new Point(12, 12);
            dataGridViewUsuarios.MultiSelect = false;
            dataGridViewUsuarios.Name = "dataGridViewUsuarios";
            dataGridViewUsuarios.ReadOnly = true;
            dataGridViewUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewUsuarios.Size = new Size(764, 222);
            dataGridViewUsuarios.TabIndex = 0;
            dataGridViewUsuarios.CellClick += dataGridViewUsuarios_CellClick;
            // 
            // btnAlta
            // 
            btnAlta.Location = new Point(13, 265);
            btnAlta.Name = "btnAlta";
            btnAlta.Size = new Size(143, 42);
            btnAlta.TabIndex = 1;
            btnAlta.Text = "Alta";
            btnAlta.UseVisualStyleBackColor = true;
            btnAlta.Click += btnAlta_Click;
            // 
            // txtDni
            // 
            txtDni.Location = new Point(82, 338);
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(158, 23);
            txtDni.TabIndex = 2;
            // 
            // lblDNI
            // 
            lblDNI.AutoSize = true;
            lblDNI.Location = new Point(13, 341);
            lblDNI.Name = "lblDNI";
            lblDNI.Size = new Size(30, 15);
            lblDNI.TabIndex = 4;
            lblDNI.Text = "DNI:";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(12, 370);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(54, 15);
            lblNombre.TabIndex = 6;
            lblNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(82, 367);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(158, 23);
            txtNombre.TabIndex = 5;
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Location = new Point(12, 399);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(54, 15);
            lblApellido.TabIndex = 8;
            lblApellido.Text = "Apellido:";
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(82, 396);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(158, 23);
            txtApellido.TabIndex = 7;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(12, 428);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(39, 15);
            lblEmail.TabIndex = 10;
            lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(82, 425);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(158, 23);
            txtEmail.TabIndex = 9;
            // 
            // lblPerfil
            // 
            lblPerfil.AutoSize = true;
            lblPerfil.Location = new Point(12, 458);
            lblPerfil.Name = "lblPerfil";
            lblPerfil.Size = new Size(37, 15);
            lblPerfil.TabIndex = 11;
            lblPerfil.Text = "Perfil:";
            // 
            // btnActivar
            // 
            btnActivar.Location = new Point(323, 265);
            btnActivar.Name = "btnActivar";
            btnActivar.Size = new Size(143, 42);
            btnActivar.TabIndex = 15;
            btnActivar.Text = "Activar";
            btnActivar.UseVisualStyleBackColor = true;
            btnActivar.Click += btnActivar_Click;
            // 
            // btnDesactivar
            // 
            btnDesactivar.Location = new Point(478, 265);
            btnDesactivar.Name = "btnDesactivar";
            btnDesactivar.Size = new Size(143, 42);
            btnDesactivar.TabIndex = 16;
            btnDesactivar.Text = "Desactivar";
            btnDesactivar.UseVisualStyleBackColor = true;
            btnDesactivar.Click += btnDesactivar_Click;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(168, 265);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(143, 42);
            btnModificar.TabIndex = 17;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // comboBoxPerfil
            // 
            comboBoxPerfil.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPerfil.FormattingEnabled = true;
            comboBoxPerfil.Items.AddRange(new object[] { "Recepcionista", "Gestor", "Repartidor" });
            comboBoxPerfil.Location = new Point(82, 455);
            comboBoxPerfil.Name = "comboBoxPerfil";
            comboBoxPerfil.Size = new Size(158, 23);
            comboBoxPerfil.TabIndex = 18;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(633, 510);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(143, 42);
            btnSalir.TabIndex = 22;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // btnDesbloquear
            // 
            btnDesbloquear.Location = new Point(633, 265);
            btnDesbloquear.Name = "btnDesbloquear";
            btnDesbloquear.Size = new Size(143, 42);
            btnDesbloquear.TabIndex = 23;
            btnDesbloquear.Text = "Desbloquear";
            btnDesbloquear.UseVisualStyleBackColor = true;
            btnDesbloquear.Click += btnDesbloquear_Click;
            // 
            // radioButtonTodos
            // 
            radioButtonTodos.AutoSize = true;
            radioButtonTodos.Location = new Point(160, 240);
            radioButtonTodos.Name = "radioButtonTodos";
            radioButtonTodos.Size = new Size(56, 19);
            radioButtonTodos.TabIndex = 24;
            radioButtonTodos.Text = "Todos";
            radioButtonTodos.UseVisualStyleBackColor = true;
            radioButtonTodos.CheckedChanged += radioButtonTodos_CheckedChanged;
            // 
            // lblMostrar
            // 
            lblMostrar.AutoSize = true;
            lblMostrar.Location = new Point(13, 242);
            lblMostrar.Name = "lblMostrar";
            lblMostrar.Size = new Size(51, 15);
            lblMostrar.TabIndex = 25;
            lblMostrar.Text = "Mostrar:";
            // 
            // radioButtonActivos
            // 
            radioButtonActivos.AutoSize = true;
            radioButtonActivos.Checked = true;
            radioButtonActivos.Location = new Point(82, 240);
            radioButtonActivos.Name = "radioButtonActivos";
            radioButtonActivos.Size = new Size(64, 19);
            radioButtonActivos.TabIndex = 26;
            radioButtonActivos.TabStop = true;
            radioButtonActivos.Text = "Activos";
            radioButtonActivos.UseVisualStyleBackColor = true;
            radioButtonActivos.CheckedChanged += radioButtonActivos_CheckedChanged;
            // 
            // btnAplicar
            // 
            btnAplicar.Location = new Point(633, 338);
            btnAplicar.Name = "btnAplicar";
            btnAplicar.Size = new Size(143, 42);
            btnAplicar.TabIndex = 27;
            btnAplicar.Text = "Aplicar";
            btnAplicar.UseVisualStyleBackColor = true;
            btnAplicar.Click += btnAplicar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(633, 396);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(143, 42);
            btnCancelar.TabIndex = 28;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // checkBoxBloqueado
            // 
            checkBoxBloqueado.AutoSize = true;
            checkBoxBloqueado.Location = new Point(82, 484);
            checkBoxBloqueado.Name = "checkBoxBloqueado";
            checkBoxBloqueado.Size = new Size(83, 19);
            checkBoxBloqueado.TabIndex = 29;
            checkBoxBloqueado.Text = "Bloqueado";
            checkBoxBloqueado.UseVisualStyleBackColor = true;
            // 
            // checkBoxActivo
            // 
            checkBoxActivo.AutoSize = true;
            checkBoxActivo.Location = new Point(82, 509);
            checkBoxActivo.Name = "checkBoxActivo";
            checkBoxActivo.Size = new Size(60, 19);
            checkBoxActivo.TabIndex = 30;
            checkBoxActivo.Text = "Activo";
            checkBoxActivo.UseVisualStyleBackColor = true;
            // 
            // lblModoActual
            // 
            lblModoActual.AutoSize = true;
            lblModoActual.Location = new Point(323, 242);
            lblModoActual.Name = "lblModoActual";
            lblModoActual.Size = new Size(77, 15);
            lblModoActual.TabIndex = 31;
            lblModoActual.Text = "Modo actual:";
            // 
            // lblModo
            // 
            lblModo.AutoSize = true;
            lblModo.Location = new Point(406, 242);
            lblModo.Name = "lblModo";
            lblModo.Size = new Size(0, 15);
            lblModo.TabIndex = 32;
            // 
            // GestionUsuariosForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(788, 564);
            Controls.Add(lblModo);
            Controls.Add(lblModoActual);
            Controls.Add(checkBoxActivo);
            Controls.Add(checkBoxBloqueado);
            Controls.Add(btnCancelar);
            Controls.Add(btnAplicar);
            Controls.Add(radioButtonActivos);
            Controls.Add(lblMostrar);
            Controls.Add(radioButtonTodos);
            Controls.Add(btnDesbloquear);
            Controls.Add(btnSalir);
            Controls.Add(comboBoxPerfil);
            Controls.Add(btnModificar);
            Controls.Add(btnDesactivar);
            Controls.Add(btnActivar);
            Controls.Add(lblPerfil);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(lblApellido);
            Controls.Add(txtApellido);
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(lblDNI);
            Controls.Add(txtDni);
            Controls.Add(btnAlta);
            Controls.Add(dataGridViewUsuarios);
            Name = "GestionUsuariosForm";
            Text = "Gestión de usuarios";
            Load += AdminGestionUsuarios_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewUsuarios).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewUsuarios;
        private Button btnAlta;
        private TextBox txtDni;
        private Label lblDNI;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblApellido;
        private TextBox txtApellido;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblPerfil;
        private Button btnActivar;
        private Button btnDesactivar;
        private Button btnModificar;
        private ComboBox comboBoxPerfil;
        private Button btnSalir;
        private Button btnDesbloquear;
        private RadioButton radioButtonTodos;
        private Label lblMostrar;
        private RadioButton radioButtonActivos;
        private Button btnAplicar;
        private Button btnCancelar;
        private CheckBox checkBoxBloqueado;
        private CheckBox checkBoxActivo;
        private Label lblModoActual;
        private Label lblModo;
    }
}