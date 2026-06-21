using BLL;
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
    public partial class CambioContraseñaForm : Form, IObserverIdioma
    {
        private UsuarioBLL bll = new UsuarioBLL();
        private EventoBLL eventobll = new EventoBLL();
        public CambioContraseñaForm()
        {
            InitializeComponent();
            GestorIdiomas.Instancia.Registrar(this);
            ActualizarIdioma();
        }

        public void ActualizarIdioma()
        {
            this.Text = Traducciones.Traducir("CambioContraseña");
            lblActual.Text = Traducciones.Traducir("Actual");
            lblNueva.Text = Traducciones.Traducir("Nueva");
            lblNueva2.Text = Traducciones.Traducir("Nueva2");
            btnAceptar.Text = Traducciones.Traducir("Aceptar");
            btnCancelar.Text = Traducciones.Traducir("Cancelar");
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var resultado = bll.CambiarContraseña(SesionUsuario.GetInstancia().UsuarioActual.DNI, txtActual.Text, txtNueva1.Text, txtNueva2.Text);
            if (!resultado.Exitoso) lblError.Text = Traducciones.Traducir(resultado.Mensaje);
            if (resultado.Exitoso)
            {
                lblError.Text = "";
                MessageBox.Show(Traducciones.Traducir(resultado.Mensaje));
                eventobll.RegistrarEvento("mod_seguridad", "ev_cambio_contraseña", 2);
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            GestorIdiomas.Instancia.Desregistrar(this);
            base.OnFormClosed(e);
        }
    }
}
