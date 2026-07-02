namespace SistemaDeEnviosGUI
{
    partial class PerfilesForm
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
            lblGestionar = new Label();
            radioButtonPerfiles = new RadioButton();
            btnAgregar = new Button();
            checkedListBoxPermisos = new CheckedListBox();
            treeViewOrganizacion = new TreeView();
            textBoxNombre = new TextBox();
            radioButtonFamilias = new RadioButton();
            lblNombre = new Label();
            btnActualizar = new Button();
            btnEliminar = new Button();
            listBoxTipo = new ListBox();
            lblTipo = new Label();
            lblPermisos = new Label();
            lblFamilias = new Label();
            checkedListBoxFamilias = new CheckedListBox();
            lblOrganizacion = new Label();
            btnSalir = new Button();
            SuspendLayout();
            // 
            // lblGestionar
            // 
            lblGestionar.AutoSize = true;
            lblGestionar.Location = new Point(12, 9);
            lblGestionar.Name = "lblGestionar";
            lblGestionar.Size = new Size(60, 15);
            lblGestionar.TabIndex = 0;
            lblGestionar.Text = "Gestionar:";
            // 
            // radioButtonPerfiles
            // 
            radioButtonPerfiles.AutoSize = true;
            radioButtonPerfiles.Location = new Point(89, 7);
            radioButtonPerfiles.Name = "radioButtonPerfiles";
            radioButtonPerfiles.Size = new Size(63, 19);
            radioButtonPerfiles.TabIndex = 1;
            radioButtonPerfiles.Text = "Perfiles";
            radioButtonPerfiles.UseVisualStyleBackColor = true;
            radioButtonPerfiles.CheckedChanged += radioButtonPerfiles_CheckedChanged;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(12, 96);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(126, 40);
            btnAgregar.TabIndex = 2;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // checkedListBoxPermisos
            // 
            checkedListBoxPermisos.FormattingEnabled = true;
            checkedListBoxPermisos.Location = new Point(430, 67);
            checkedListBoxPermisos.Name = "checkedListBoxPermisos";
            checkedListBoxPermisos.Size = new Size(228, 526);
            checkedListBoxPermisos.TabIndex = 3;
            // 
            // treeViewOrganizacion
            // 
            treeViewOrganizacion.Location = new Point(948, 67);
            treeViewOrganizacion.Name = "treeViewOrganizacion";
            treeViewOrganizacion.Size = new Size(228, 526);
            treeViewOrganizacion.TabIndex = 4;
            // 
            // textBoxNombre
            // 
            textBoxNombre.Location = new Point(12, 67);
            textBoxNombre.Name = "textBoxNombre";
            textBoxNombre.Size = new Size(126, 23);
            textBoxNombre.TabIndex = 5;
            // 
            // radioButtonFamilias
            // 
            radioButtonFamilias.AutoSize = true;
            radioButtonFamilias.Location = new Point(167, 7);
            radioButtonFamilias.Name = "radioButtonFamilias";
            radioButtonFamilias.Size = new Size(68, 19);
            radioButtonFamilias.TabIndex = 6;
            radioButtonFamilias.Text = "Familias";
            radioButtonFamilias.UseVisualStyleBackColor = true;
            radioButtonFamilias.CheckedChanged += radioButtonFamilias_CheckedChanged;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(12, 49);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(51, 15);
            lblNombre.TabIndex = 7;
            lblNombre.Text = "Nombre";
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(12, 144);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(126, 40);
            btnActualizar.TabIndex = 8;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(12, 192);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(126, 40);
            btnEliminar.TabIndex = 9;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // listBoxTipo
            // 
            listBoxTipo.FormattingEnabled = true;
            listBoxTipo.ItemHeight = 15;
            listBoxTipo.Location = new Point(171, 67);
            listBoxTipo.Name = "listBoxTipo";
            listBoxTipo.Size = new Size(228, 529);
            listBoxTipo.TabIndex = 10;
            listBoxTipo.SelectedIndexChanged += listBoxTipo_SelectedIndexChanged;
            // 
            // lblTipo
            // 
            lblTipo.AutoSize = true;
            lblTipo.Location = new Point(171, 49);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(30, 15);
            lblTipo.TabIndex = 11;
            lblTipo.Text = "Tipo";
            // 
            // lblPermisos
            // 
            lblPermisos.AutoSize = true;
            lblPermisos.Location = new Point(430, 49);
            lblPermisos.Name = "lblPermisos";
            lblPermisos.Size = new Size(55, 15);
            lblPermisos.TabIndex = 12;
            lblPermisos.Text = "Permisos";
            // 
            // lblFamilias
            // 
            lblFamilias.AutoSize = true;
            lblFamilias.Location = new Point(689, 49);
            lblFamilias.Name = "lblFamilias";
            lblFamilias.Size = new Size(50, 15);
            lblFamilias.TabIndex = 14;
            lblFamilias.Text = "Familias";
            // 
            // checkedListBoxFamilias
            // 
            checkedListBoxFamilias.FormattingEnabled = true;
            checkedListBoxFamilias.Location = new Point(689, 67);
            checkedListBoxFamilias.Name = "checkedListBoxFamilias";
            checkedListBoxFamilias.Size = new Size(228, 526);
            checkedListBoxFamilias.TabIndex = 13;
            // 
            // lblOrganizacion
            // 
            lblOrganizacion.AutoSize = true;
            lblOrganizacion.Location = new Point(948, 49);
            lblOrganizacion.Name = "lblOrganizacion";
            lblOrganizacion.Size = new Size(77, 15);
            lblOrganizacion.TabIndex = 15;
            lblOrganizacion.Text = "Organización";
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(12, 553);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(126, 40);
            btnSalir.TabIndex = 16;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // PerfilesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1191, 608);
            Controls.Add(btnSalir);
            Controls.Add(lblOrganizacion);
            Controls.Add(lblFamilias);
            Controls.Add(checkedListBoxFamilias);
            Controls.Add(lblPermisos);
            Controls.Add(lblTipo);
            Controls.Add(listBoxTipo);
            Controls.Add(btnEliminar);
            Controls.Add(btnActualizar);
            Controls.Add(lblNombre);
            Controls.Add(radioButtonFamilias);
            Controls.Add(textBoxNombre);
            Controls.Add(treeViewOrganizacion);
            Controls.Add(checkedListBoxPermisos);
            Controls.Add(btnAgregar);
            Controls.Add(radioButtonPerfiles);
            Controls.Add(lblGestionar);
            Name = "PerfilesForm";
            Text = "PerfilesForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblGestionar;
        private RadioButton radioButtonPerfiles;
        private Button btnAgregar;
        private CheckedListBox checkedListBoxPermisos;
        private TreeView treeViewOrganizacion;
        private TextBox textBoxNombre;
        private RadioButton radioButtonFamilias;
        private Label lblNombre;
        private Button btnActualizar;
        private Button btnEliminar;
        private ListBox listBoxTipo;
        private Label lblTipo;
        private Label lblPermisos;
        private Label lblFamilias;
        private CheckedListBox checkedListBoxFamilias;
        private Label lblOrganizacion;
        private Button btnSalir;
    }
}