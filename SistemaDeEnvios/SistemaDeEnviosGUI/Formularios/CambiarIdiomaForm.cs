using BLL;
using Servicios.GestionIdiomas;
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
    public partial class CambiarIdiomaForm : Form, IObserverIdioma
    {
        public CambiarIdiomaForm()
        {
            InitializeComponent();
            GestorIdiomas.Instancia.Registrar(this);
            CargarIdiomas();
            ActualizarIdioma();
        }
        private IdiomaBLL idiomabll = new IdiomaBLL();
        private EventoBLL eventobll = new EventoBLL();
        private void CargarIdiomas()
        {
            comboBoxIdioma.DataSource = idiomabll.ObtenerIdiomas();
            comboBoxIdioma.DisplayMember = "Nombre";
            comboBoxIdioma.ValueMember = "Id";

            comboBoxIdioma.SelectedValue = GestorIdiomas.Instancia.IdiomaActual.Id;
        }
        public void ActualizarIdioma()
        {
            this.Text = Traducciones.Traducir("CambiarIdioma");
            lblIdioma.Text = Traducciones.Traducir("Idioma");
            btnAplicar.Text = Traducciones.Traducir("Aplicar");
            btnCancelar.Text = Traducciones.Traducir("Cancelar");
        }
        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (comboBoxIdioma.SelectedItem is Idioma idioma)
            {
                GestorIdiomas.Instancia.CambiarIdioma(idioma);
                MessageBox.Show(Traducciones.Traducir("idioma_cambiado"));
                eventobll.RegistrarEvento("mod_usuarios", "ev_cambio_idioma", 1);
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
