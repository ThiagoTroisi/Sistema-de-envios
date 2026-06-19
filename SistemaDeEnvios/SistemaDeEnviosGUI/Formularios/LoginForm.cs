using BE.Entidades;
using BLL;
using BLL.Gestores;
using BLL.Otros;
using Servicios.GestionIdiomas;
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
    public partial class LoginForm : Form, IObserverIdioma
    {
        public LoginForm()
        {
            InitializeComponent();
            GestorIdiomas.Instancia.Registrar(this);
            CargarIdiomas();
            ActualizarIdioma();
        }
        private SessionManager gestor = new SessionManager();
        private EventoBLL eventobll = new EventoBLL();
        private IdiomaBLL idiomabll = new IdiomaBLL();
        private string ultimoCodigoError;
        private object[] ultimosParametros;
        private void CargarIdiomas()
        {
            comboBoxIdioma.DataSource = idiomabll.ObtenerIdiomas();
            comboBoxIdioma.DisplayMember = "Nombre";
            comboBoxIdioma.ValueMember = "Id";
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string mail = txtEmail.Text;
            string contra = txtContraseña.Text;

            ResultadoOperacion rl = gestor.IniciarSesion(mail, contra);

            if (!rl.Exitoso)
            {
                ultimoCodigoError = rl.Mensaje;
                ultimosParametros = rl.Parametros;

                lblError.Text = string.Format(Traducciones.Traducir(ultimoCodigoError), ultimosParametros ?? Array.Empty<object>());
            }
            else
            {
                lblError.Text = "";
                SesionUsuario.GetInstancia().Iniciar(rl.Usuario);
                MessageBox.Show(Traducciones.Traducir(rl.Mensaje));
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

        public void ActualizarIdioma()
        {
            lblEmail.Text = Traducciones.Traducir("lblEmail");
            lblContraseña.Text = Traducciones.Traducir("lblContraseña");
            lblIdioma.Text = Traducciones.Traducir("lblIdioma");
            btnLogin.Text = Traducciones.Traducir("btnLogin");
            btnRegistrarse.Text = Traducciones.Traducir("btnRegistrarse");
            btnSalir.Text = Traducciones.Traducir("btnSalir");
            if (!string.IsNullOrEmpty(ultimoCodigoError))
            {
                lblError.Text = string.Format(Traducciones.Traducir(ultimoCodigoError), ultimosParametros ?? Array.Empty<object>());
            }
        }

        private void comboBoxIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxIdioma.SelectedItem is Idioma idioma)
            {
                GestorIdiomas.Instancia.CambiarIdioma(idioma);
            }
        }
    }
}
