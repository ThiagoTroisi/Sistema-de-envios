using BE.Entidades;
using BLL;
using BLL.Gestores;
using BLL.Otros;
using DAL;
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
        public LoginForm(bool relogin = false)
        {
            InitializeComponent();
            modoRelogin = relogin;
            if (modoRelogin)
            {
                comboBoxIdioma.Enabled = false;
                btnRegistrarse.Enabled = false;
                btnAplicar.Enabled = false;
            }
            GestorIdiomas.Instancia.Registrar(this);
            ActualizarIdioma();
        }
        private bool modoRelogin;
        private SessionManager gestor = new SessionManager();
        private EventoBLL eventobll = new EventoBLL();
        UsuarioBLL usuariobll = new UsuarioBLL();
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
                ultimoCodigoError = "";
                ultimosParametros = null;
                comboBoxIdioma.SelectedValue = GestorIdiomas.Instancia.IdiomaActual.Id;
                MessageBox.Show(Traducciones.Traducir(rl.Mensaje));
                this.Hide();
                new MainForm().ShowDialog();
                gestor.CerrarSesion();
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
            CargarIdiomas();
            Idioma actual = GestorIdiomas.Instancia.IdiomaActual;
            if (actual != null) comboBoxIdioma.SelectedValue = actual.Id;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ActualizarIdioma()
        {
            this.Text = Traducciones.Traducir("Login");
            lblEmail.Text = Traducciones.Traducir("Email");
            lblContraseña.Text = Traducciones.Traducir("Contraseña");
            lblIdioma.Text = Traducciones.Traducir("Idioma");
            btnLogin.Text = Traducciones.Traducir("Login");
            btnRegistrarse.Text = Traducciones.Traducir("Registrarse");
            btnSalir.Text = Traducciones.Traducir("Salir");
            if (!string.IsNullOrEmpty(ultimoCodigoError))
            {
                lblError.Text = string.Format(Traducciones.Traducir(ultimoCodigoError), ultimosParametros ?? Array.Empty<object>());
            }
        }

        private void comboBoxIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            GestorIdiomas.Instancia.Desregistrar(this);
            base.OnFormClosed(e);
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (comboBoxIdioma.SelectedItem is Idioma idioma)
            {
                GestorIdiomas.Instancia.CambiarIdioma(idioma);
            }
        }

    }
}
