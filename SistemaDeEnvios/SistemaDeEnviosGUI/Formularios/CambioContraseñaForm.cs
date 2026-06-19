using BLL;
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
    public partial class CambioContraseñaForm : Form
    {
        private UsuarioBLL bll = new UsuarioBLL();
        private EventoBLL eventobll = new EventoBLL();
        public CambioContraseñaForm()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var resultado = bll.CambiarContraseña(SesionUsuario.GetInstancia().UsuarioActual.DNI, txtActual.Text, txtNueva1.Text, txtNueva2.Text);
            if (!resultado.Exitoso) labelError.Text = $"ERROR: {resultado.Mensaje}";
            if (resultado.Exitoso)
            {
                labelError.Text = "";
                MessageBox.Show(resultado.Mensaje);
                eventobll.RegistrarEvento("Seguridad", "Cambio de contraseña", 2);
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
