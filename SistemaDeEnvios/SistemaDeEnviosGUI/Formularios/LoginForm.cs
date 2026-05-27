using BE.Entidades;
using BLL.Gestores;
using BLL.Otros;
using Servicios.Sesión;
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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        private LoginGestor gestor = new LoginGestor();
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string mail = txtEmail.Text;
            string contra = txtContraseña.Text;

            ResultadoOperacion rl = gestor.IniciarSesion(mail, contra);

            if (!rl.Exitoso)
            {
                lblError.Text = rl.Mensaje;
            }
            else
            {
                SesionUsuario.GetInstancia().Iniciar(rl.Usuario);
                MessageBox.Show("Login exitoso");
                this.Hide();
                new AdminForm().ShowDialog();
                this.Show();
            }
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtEmail.Text = "admin@sistema.com";
            txtContraseña.Text = "admin123";
        }
    }
}
