using BE.Entidades;
using BLL;
using BLL.Gestores;
using BLL.Otros;
using DAL;
using Servicios;
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
            btnRegistrarse.Enabled = false;
            if (modoRelogin)
            {
                comboBoxIdioma.Enabled = false;
                btnAplicar.Enabled = false;
            }
            GestorIdiomas.Instancia.Registrar(this);
            ActualizarIdioma();
            txtEmail.Text = "admin@sistema.com";
            txtContraseña.Text = "admin123";
        }
        bool modoRelogin;
        SessionManager sessionmanager = new SessionManager();
        IdiomaBLL idiomabll = new IdiomaBLL();
        string ultimoCodigoError;
        object[] ultimosParametros;
        DVBLL dvbll = new DVBLL();
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

            ResultadoOperacion rl = sessionmanager.IniciarSesion(mail, contra);

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

                List<ErrorTabla> errores = dvbll.VerificarIntegridadSistema();

                if (errores.Count > 0)
                {
                    GestionarErrorIntegridad(errores);
                    return;
                }

                MessageBox.Show(Traducciones.Traducir(rl.Mensaje));
                sessionmanager.ContinuarLogin(mail);
                this.Hide();
                new MainForm().ShowDialog();
                sessionmanager.CerrarSesion();
                try
                {
                    this.Show();
                }
                catch (Exception) { }
            }
        }
        private void GestionarErrorIntegridad(List<ErrorTabla> errores)
        {
            MessageBox.Show(Traducciones.Traducir("integridad afectada"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (SesionUsuario.GetInstancia().UsuarioActual.IdPerfil != 1)
            {
                MessageBox.Show(Traducciones.Traducir("contacte administrador integridad"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                this.Hide();
                new ErrorIntegridadForm(errores).ShowDialog();
                if (dvbll.VerificarIntegridadSistema().Count() != 0)
                {
                    MessageBox.Show(Traducciones.Traducir("integridad no solucionada"));
                }
            }
            Application.Restart();
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
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
