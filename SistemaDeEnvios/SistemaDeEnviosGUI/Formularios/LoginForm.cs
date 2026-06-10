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
        private SessionManager gestor = new SessionManager();
        private EventoBLL eventobll = new EventoBLL();
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
                lblError.Text = "";
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
                eventobll.RegistrarEvento("Usuarios", "Logout", 1);
                SesionUsuario.GetInstancia().Cerrar();
                this.Show();
            }
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidad en desarrollo. Este registro implica que se crea un usuario bajo el rol de remitente/destinatario");
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtEmail.Text = "admin@sistema.com";
            txtContraseña.Text = "admin123";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
