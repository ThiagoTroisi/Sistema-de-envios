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
        EventoBLL eventobll = new EventoBLL();
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
                eventobll.RegistrarEvento("Usuarios", "Login", 1);
                this.Hide();
                int rol = rl.Usuario.IdRol;
                switch (rol)
                {
                    case 1: // Admin
                        new AdminForm().ShowDialog();
                        break;

                    case 2: // Recepcionista

                        break;

                    case 3: // Gestor

                        break;

                    case 4: // Repartidor

                        break;

                    case 5: // Remitente / Destinatario

                        break;
                }
                eventoBLL.RegistrarEvento("Usuarios", "Logout", 1);
                SesionUsuario.GetInstancia().Cerrar();
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
