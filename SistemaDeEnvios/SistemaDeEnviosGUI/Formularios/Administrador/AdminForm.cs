using SistemaDeEnviosGUI.Formularios.Administrador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDeEnviosGUI.Formularios
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Beige;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent == null) return;
            switch (e.Node.Text)
            {
                case "Gestión de usuarios":
                    new GestionUsuariosForm().ShowDialog();
                    break;

                case "Bitácora de eventos":
                    new BitacoraDeEventosForm().ShowDialog();
                    break;

                case "Cambiar contraseña":
                    new CambioContraseñaForm().ShowDialog();
                    break;
            }
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
            MessageBox.Show("Se cerró la sesión del usuario");
        }

        private void btnRelogin_Click(object sender, EventArgs e)
        {
            new LoginForm().ShowDialog();
        }
    }
}
