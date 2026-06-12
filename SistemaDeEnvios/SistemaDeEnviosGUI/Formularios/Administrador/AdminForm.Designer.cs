namespace SistemaDeEnviosGUI.Formularios
{
    partial class AdminForm
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
            TreeNode treeNode1 = new TreeNode("Cambiar contraseña");
            TreeNode treeNode2 = new TreeNode("Cambiar idioma");
            TreeNode treeNode3 = new TreeNode("Usuario", new TreeNode[] { treeNode1, treeNode2 });
            TreeNode treeNode4 = new TreeNode("Gestión de usuarios");
            TreeNode treeNode5 = new TreeNode("Bitácora de eventos");
            TreeNode treeNode6 = new TreeNode("Administración", new TreeNode[] { treeNode4, treeNode5 });
            TreeNode treeNode7 = new TreeNode("Solicitar envío");
            TreeNode treeNode8 = new TreeNode("Registrar envío");
            TreeNode treeNode9 = new TreeNode("Consultar estado");
            TreeNode treeNode10 = new TreeNode("Modificar destino");
            TreeNode treeNode11 = new TreeNode("Cancelar envío");
            TreeNode treeNode12 = new TreeNode("Envío", new TreeNode[] { treeNode7, treeNode8, treeNode9, treeNode10, treeNode11 });
            TreeNode treeNode13 = new TreeNode("Registrar pago");
            TreeNode treeNode14 = new TreeNode("Consultar comprobantes");
            TreeNode treeNode15 = new TreeNode("Pago", new TreeNode[] { treeNode13, treeNode14 });
            TreeNode treeNode16 = new TreeNode("Generar rutas");
            TreeNode treeNode17 = new TreeNode("Asignar rutas");
            TreeNode treeNode18 = new TreeNode("Consultar rutas");
            TreeNode treeNode19 = new TreeNode("Logística", new TreeNode[] { treeNode16, treeNode17, treeNode18 });
            TreeNode treeNode20 = new TreeNode("Registrar entrega");
            TreeNode treeNode21 = new TreeNode("Solicitar devolución");
            TreeNode treeNode22 = new TreeNode("Calificar repartidor");
            TreeNode treeNode23 = new TreeNode("Entrega", new TreeNode[] { treeNode20, treeNode21, treeNode22 });
            TreeNode treeNode24 = new TreeNode("Reportar inconveniente");
            TreeNode treeNode25 = new TreeNode("Inconveniente", new TreeNode[] { treeNode24 });
            TreeNode treeNode26 = new TreeNode("Envíos");
            TreeNode treeNode27 = new TreeNode("Entregas");
            TreeNode treeNode28 = new TreeNode("Inconvenientes");
            TreeNode treeNode29 = new TreeNode("Repartidores");
            TreeNode treeNode30 = new TreeNode("Pagos");
            TreeNode treeNode31 = new TreeNode("Analíticas operativas");
            TreeNode treeNode32 = new TreeNode("Reporte", new TreeNode[] { treeNode26, treeNode27, treeNode28, treeNode29, treeNode30, treeNode31 });
            pictureBox1 = new PictureBox();
            treeView1 = new TreeView();
            btnCerrarSesion = new Button();
            btnRelogin = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.png_logo;
            pictureBox1.Location = new Point(449, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(775, 263);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // treeView1
            // 
            treeView1.Location = new Point(13, 12);
            treeView1.Name = "treeView1";
            treeNode1.Name = "Nodo1";
            treeNode1.Text = "Cambiar contraseña";
            treeNode2.Name = "Nodo2";
            treeNode2.Text = "Cambiar idioma";
            treeNode3.Name = "Nodo0";
            treeNode3.Text = "Usuario";
            treeNode4.Name = "Nodo4";
            treeNode4.Text = "Gestión de usuarios";
            treeNode5.Name = "Nodo7";
            treeNode5.Text = "Bitácora de eventos";
            treeNode6.Name = "Nodo3";
            treeNode6.Text = "Administración";
            treeNode7.Name = "Nodo6";
            treeNode7.Text = "Solicitar envío";
            treeNode8.Name = "Nodo8";
            treeNode8.Text = "Registrar envío";
            treeNode9.Name = "Nodo9";
            treeNode9.Text = "Consultar estado";
            treeNode10.Name = "Nodo10";
            treeNode10.Text = "Modificar destino";
            treeNode11.Name = "Nodo11";
            treeNode11.Text = "Cancelar envío";
            treeNode12.Name = "Nodo5";
            treeNode12.Text = "Envío";
            treeNode13.Name = "Nodo13";
            treeNode13.Text = "Registrar pago";
            treeNode14.Name = "Nodo14";
            treeNode14.Text = "Consultar comprobantes";
            treeNode15.Name = "Nodo12";
            treeNode15.Text = "Pago";
            treeNode16.Name = "Nodo16";
            treeNode16.Text = "Generar rutas";
            treeNode17.Name = "Nodo17";
            treeNode17.Text = "Asignar rutas";
            treeNode18.Name = "Nodo18";
            treeNode18.Text = "Consultar rutas";
            treeNode19.Name = "Nodo15";
            treeNode19.Text = "Logística";
            treeNode20.Name = "Nodo20";
            treeNode20.Text = "Registrar entrega";
            treeNode21.Name = "Nodo21";
            treeNode21.Text = "Solicitar devolución";
            treeNode22.Name = "Nodo22";
            treeNode22.Text = "Calificar repartidor";
            treeNode23.Name = "Nodo19";
            treeNode23.Text = "Entrega";
            treeNode24.Name = "Nodo25";
            treeNode24.Text = "Reportar inconveniente";
            treeNode25.Name = "Nodo23";
            treeNode25.Text = "Inconveniente";
            treeNode26.Name = "Nodo26";
            treeNode26.Text = "Envíos";
            treeNode27.Name = "Nodo27";
            treeNode27.Text = "Entregas";
            treeNode28.Name = "Nodo28";
            treeNode28.Text = "Inconvenientes";
            treeNode29.Name = "Nodo29";
            treeNode29.Text = "Repartidores";
            treeNode30.Name = "Nodo30";
            treeNode30.Text = "Pagos";
            treeNode31.Name = "Nodo31";
            treeNode31.Text = "Analíticas operativas";
            treeNode32.Name = "Nodo24";
            treeNode32.Text = "Reporte";
            treeView1.Nodes.AddRange(new TreeNode[] { treeNode3, treeNode6, treeNode12, treeNode15, treeNode19, treeNode23, treeNode25, treeNode32 });
            treeView1.Size = new Size(203, 586);
            treeView1.TabIndex = 1;
            treeView1.AfterSelect += treeView1_AfterSelect;
            // 
            // btnCerrarSesion
            // 
            btnCerrarSesion.Location = new Point(1104, 551);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.Size = new Size(120, 47);
            btnCerrarSesion.TabIndex = 2;
            btnCerrarSesion.Text = "Cerrar sesión";
            btnCerrarSesion.UseVisualStyleBackColor = true;
            btnCerrarSesion.Click += btnCerrarSesion_Click;
            // 
            // btnRelogin
            // 
            btnRelogin.Location = new Point(978, 551);
            btnRelogin.Name = "btnRelogin";
            btnRelogin.Size = new Size(120, 47);
            btnRelogin.TabIndex = 3;
            btnRelogin.Text = "Relogin";
            btnRelogin.UseVisualStyleBackColor = true;
            btnRelogin.Click += btnRelogin_Click;
            // 
            // AdminForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1236, 610);
            Controls.Add(btnRelogin);
            Controls.Add(btnCerrarSesion);
            Controls.Add(treeView1);
            Controls.Add(pictureBox1);
            Name = "AdminForm";
            Text = "AdminForm";
            Load += AdminForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private TreeView treeView1;
        private Button btnCerrarSesion;
        private Button btnRelogin;
    }
}