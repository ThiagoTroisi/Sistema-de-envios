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
            label2 = new Label();
            label3 = new Label();
            txtNombre = new TextBox();
            label4 = new Label();
            txtApellido = new TextBox();
            label5 = new Label();
            txtEmail = new TextBox();
            label6 = new Label();
            btnActivar = new Button();
            btnDesactivar = new Button();
            btnModificar = new Button();
            comboBoxRol = new ComboBox();
            btnSalir = new Button();
            btnDesbloquear = new Button();
            radioButtonTodos = new RadioButton();
            label1 = new Label();
            radioButtonActivos = new RadioButton();
            btnAplicar = new Button();
            btnCancelar = new Button();
            checkBoxBloqueado = new CheckBox();
            checkBoxActivo = new CheckBox();
            label7 = new Label();
            labelModo = new Label();
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
            txtDni.Location = new Point(72, 338);
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(158, 23);
            txtDni.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(13, 341);
            label2.Name = "label2";
            label2.Size = new Size(30, 15);
            label2.TabIndex = 4;
            label2.Text = "DNI:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 370);
            label3.Name = "label3";
            label3.Size = new Size(54, 15);
            label3.TabIndex = 6;
            label3.Text = "Nombre:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(72, 367);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(158, 23);
            txtNombre.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 399);
            label4.Name = "label4";
            label4.Size = new Size(54, 15);
            label4.TabIndex = 8;
            label4.Text = "Apellido:";
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(72, 396);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(158, 23);
            txtApellido.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 428);
            label5.Name = "label5";
            label5.Size = new Size(39, 15);
            label5.TabIndex = 10;
            label5.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(72, 425);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(158, 23);
            txtEmail.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 458);
            label6.Name = "label6";
            label6.Size = new Size(27, 15);
            label6.TabIndex = 11;
            label6.Text = "Rol:";
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
            // comboBoxRol
            // 
            comboBoxRol.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxRol.FormattingEnabled = true;
            comboBoxRol.Items.AddRange(new object[] { "Recepcionista", "Gestor", "Repartidor" });
            comboBoxRol.Location = new Point(72, 455);
            comboBoxRol.Name = "comboBoxRol";
            comboBoxRol.Size = new Size(158, 23);
            comboBoxRol.TabIndex = 18;
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 242);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 25;
            label1.Text = "Mostrar:";
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
            checkBoxBloqueado.Location = new Point(72, 484);
            checkBoxBloqueado.Name = "checkBoxBloqueado";
            checkBoxBloqueado.Size = new Size(83, 19);
            checkBoxBloqueado.TabIndex = 29;
            checkBoxBloqueado.Text = "Bloqueado";
            checkBoxBloqueado.UseVisualStyleBackColor = true;
            // 
            // checkBoxActivo
            // 
            checkBoxActivo.AutoSize = true;
            checkBoxActivo.Location = new Point(72, 509);
            checkBoxActivo.Name = "checkBoxActivo";
            checkBoxActivo.Size = new Size(60, 19);
            checkBoxActivo.TabIndex = 30;
            checkBoxActivo.Text = "Activo";
            checkBoxActivo.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(323, 242);
            label7.Name = "label7";
            label7.Size = new Size(77, 15);
            label7.TabIndex = 31;
            label7.Text = "Modo actual:";
            // 
            // labelModo
            // 
            labelModo.AutoSize = true;
            labelModo.Location = new Point(406, 242);
            labelModo.Name = "labelModo";
            labelModo.Size = new Size(0, 15);
            labelModo.TabIndex = 32;
            // 
            // GestionUsuariosForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(788, 564);
            Controls.Add(labelModo);
            Controls.Add(label7);
            Controls.Add(checkBoxActivo);
            Controls.Add(checkBoxBloqueado);
            Controls.Add(btnCancelar);
            Controls.Add(btnAplicar);
            Controls.Add(radioButtonActivos);
            Controls.Add(label1);
            Controls.Add(radioButtonTodos);
            Controls.Add(btnDesbloquear);
            Controls.Add(btnSalir);
            Controls.Add(comboBoxRol);
            Controls.Add(btnModificar);
            Controls.Add(btnDesactivar);
            Controls.Add(btnActivar);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(txtEmail);
            Controls.Add(label4);
            Controls.Add(txtApellido);
            Controls.Add(label3);
            Controls.Add(txtNombre);
            Controls.Add(label2);
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
        private Label label2;
        private Label label3;
        private TextBox txtNombre;
        private Label label4;
        private TextBox txtApellido;
        private Label label5;
        private TextBox txtEmail;
        private Label label6;
        private Button btnActivar;
        private Button btnDesactivar;
        private Button btnModificar;
        private ComboBox comboBoxRol;
        private Button btnSalir;
        private Button btnDesbloquear;
        private RadioButton radioButtonTodos;
        private Label label1;
        private RadioButton radioButtonActivos;
        private Button btnAplicar;
        private Button btnCancelar;
        private CheckBox checkBoxBloqueado;
        private CheckBox checkBoxActivo;
        private Label label7;
        private Label labelModo;
    }
}