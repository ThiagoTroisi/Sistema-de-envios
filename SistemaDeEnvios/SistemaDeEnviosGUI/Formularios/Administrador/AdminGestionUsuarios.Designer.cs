namespace SistemaDeEnviosGUI.Formularios.Administrador
{
    partial class AdminGestionUsuarios
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
            btnAlta = new Button();
            txtDni = new TextBox();
            label1 = new Label();
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
            comboBox1 = new ComboBox();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(776, 222);
            dataGridView1.TabIndex = 0;
            // 
            // btnAlta
            // 
            btnAlta.Location = new Point(280, 264);
            btnAlta.Name = "btnAlta";
            btnAlta.Size = new Size(125, 42);
            btnAlta.TabIndex = 1;
            btnAlta.Text = "Alta";
            btnAlta.UseVisualStyleBackColor = true;
            btnAlta.Click += btnAlta_Click;
            // 
            // txtDni
            // 
            txtDni.Location = new Point(72, 264);
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(158, 23);
            txtDni.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 240);
            label1.Name = "label1";
            label1.Size = new Size(201, 15);
            label1.TabIndex = 3;
            label1.Text = "DATOS PARA ALTA Y MODIFICACIÓN";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(13, 267);
            label2.Name = "label2";
            label2.Size = new Size(30, 15);
            label2.TabIndex = 4;
            label2.Text = "DNI:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 296);
            label3.Name = "label3";
            label3.Size = new Size(54, 15);
            label3.TabIndex = 6;
            label3.Text = "Nombre:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(72, 293);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(158, 23);
            txtNombre.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 325);
            label4.Name = "label4";
            label4.Size = new Size(54, 15);
            label4.TabIndex = 8;
            label4.Text = "Apellido:";
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(72, 322);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(158, 23);
            txtApellido.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 354);
            label5.Name = "label5";
            label5.Size = new Size(39, 15);
            label5.TabIndex = 10;
            label5.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(72, 351);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(158, 23);
            txtEmail.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 384);
            label6.Name = "label6";
            label6.Size = new Size(27, 15);
            label6.TabIndex = 11;
            label6.Text = "Rol:";
            // 
            // btnActivar
            // 
            btnActivar.Location = new Point(427, 264);
            btnActivar.Name = "btnActivar";
            btnActivar.Size = new Size(125, 42);
            btnActivar.TabIndex = 15;
            btnActivar.Text = "Activar usuario";
            btnActivar.UseVisualStyleBackColor = true;
            btnActivar.Click += btnActivar_Click;
            // 
            // btnDesactivar
            // 
            btnDesactivar.Location = new Point(427, 322);
            btnDesactivar.Name = "btnDesactivar";
            btnDesactivar.Size = new Size(125, 42);
            btnDesactivar.TabIndex = 16;
            btnDesactivar.Text = "Desactivar usuario";
            btnDesactivar.UseVisualStyleBackColor = true;
            btnDesactivar.Click += btnDesactivar_Click;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(280, 322);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(125, 42);
            btnModificar.TabIndex = 17;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Recepcionista", "Gestor", "Repartidor" });
            comboBox1.Location = new Point(72, 381);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(158, 23);
            comboBox1.TabIndex = 18;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(558, 264);
            label7.Name = "label7";
            label7.Size = new Size(86, 15);
            label7.TabIndex = 19;
            label7.Text = "Recordar que...";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(558, 287);
            label8.Name = "label8";
            label8.Size = new Size(102, 15);
            label8.TabIndex = 20;
            label8.Text = "1 = Usuario activo";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(558, 310);
            label9.Name = "label9";
            label9.Size = new Size(119, 15);
            label9.TabIndex = 21;
            label9.Text = "0 = Usuario no activo";
            // 
            // AdminGestionUsuarios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 467);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(comboBox1);
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
            Controls.Add(label1);
            Controls.Add(txtDni);
            Controls.Add(btnAlta);
            Controls.Add(dataGridView1);
            Name = "AdminGestionUsuarios";
            Text = "Gestión de usuarios";
            Load += AdminGestionUsuarios_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button btnAlta;
        private TextBox txtDni;
        private Label label1;
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
        private ComboBox comboBox1;
        private Label label7;
        private Label label8;
        private Label label9;
    }
}