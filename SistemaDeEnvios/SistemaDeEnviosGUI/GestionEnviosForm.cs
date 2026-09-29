using BE;
using BLL;
using Servicios.GestionIdiomas;
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

namespace SistemaDeEnviosGUI
{
    public partial class GestionEnviosForm : Form, IObserverIdioma
    {
        private EnvioBLL enviobll = new EnvioBLL();
        private PersonaBLL personabll = new PersonaBLL();

        private int? dniRemitenteCargado;
        private int? dniDestinatarioCargado;

        public GestionEnviosForm()
        {
            InitializeComponent();
            GestorIdiomas.Instancia.Registrar(this);
        }

        private void GestionEnviosForm_Load(object sender, EventArgs e)
        {

        }

        private void btnRegistrarEnvio_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                    throw new Exception("La descripción del paquete es obligatoria.");

                if (!decimal.TryParse(txtPeso.Text, out decimal peso) || peso <= 0)
                    throw new Exception("El peso debe ser un número mayor a cero.");

                if (!decimal.TryParse(txtAlto.Text, out decimal alto) || alto <= 0)
                    throw new Exception("El alto debe ser un número mayor a cero.");

                if (!decimal.TryParse(txtAncho.Text, out decimal ancho) || ancho <= 0)
                    throw new Exception("El ancho debe ser un número mayor a cero.");

                if (!decimal.TryParse(txtLargo.Text, out decimal largo) || largo <= 0)
                    throw new Exception("El largo debe ser un número mayor a cero.");

                if (string.IsNullOrWhiteSpace(txtDireccion.Text))
                    throw new Exception("La dirección de destino es obligatoria.");

                if (string.IsNullOrWhiteSpace(txtCiudad.Text))
                    throw new Exception("La ciudad de destino es obligatoria.");

                if (string.IsNullOrWhiteSpace(txtCP.Text))
                    throw new Exception("El código postal es obligatorio.");

                if (string.IsNullOrWhiteSpace(txtProvincia.Text))
                    throw new Exception("La provincia es obligatoria.");

                if (!dniRemitenteCargado.HasValue)
                    throw new Exception("Debe buscar al remitente antes de registrar el envío.");

                if (!dniDestinatarioCargado.HasValue)
                    throw new Exception("Debe buscar al destinatario antes de registrar el envío.");

                int dniRemitente = dniRemitenteCargado.Value;
                int dniDestinatario = dniDestinatarioCargado.Value;

                if (dniRemitente == dniDestinatario)
                {
                    DialogResult resultado = MessageBox.Show("El remitente y el destinatario son la misma persona. ¿Desea continuar con el registro del envío?", "Confirmar envío", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultado != DialogResult.Yes) return;
                }

                PaqueteBE paquete = new PaqueteBE
                {
                    Descripcion = txtDescripcion.Text,
                    Peso = peso,
                    Alto = alto,
                    Ancho = ancho,
                    Largo = largo
                };

                DestinoBE destino = new DestinoBE
                {
                    Direccion = txtDireccion.Text,
                    Ciudad = txtCiudad.Text,
                    CodigoPostal = txtCP.Text,
                    Provincia = txtProvincia.Text
                };

                EnvioBE envio = new EnvioBE(
                    "Sucursal Central",
                    0,
                    0,
                    dniRemitente,
                    dniDestinatario
                );

                enviobll.RegistrarEnvio(paquete, destino, envio);

                MessageBox.Show($"El envío se registró correctamente.\nCódigo de seguimiento: {envio.CodigoSeguimiento}","Registro de envío", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool BuscarPersona(int dni, bool remitente)
        {
            PersonaBE persona = personabll.ConsultaPorDNI(dni);

            if (persona == null)
            {
                DialogResult resultado = MessageBox.Show("No existe una persona registrada con ese DNI. ¿Desea registrarla?", "Persona no encontrada", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado != DialogResult.Yes) return false;

                MaestroPersonasForm form = new MaestroPersonasForm(ModoFormulario.Alta, dni);
                form.ShowDialog();

                persona = personabll.ConsultaPorDNI(dni);

                if (persona == null) return false;
            }

            if (!personabll.EstaActiva(dni))
            {
                DialogResult resultado = MessageBox.Show("La persona se encuentra dada de baja. ¿Desea reactivarla?", "Persona dada de baja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado != DialogResult.Yes) return false;

                personabll.ReactivarPersona(dni);
            }

            if (remitente)
            {
                txtNombreR.Text = persona.Nombre;
                txtApellidoR.Text = persona.Apellido;
                txtTelefonoR.Text = persona.Telefono;
                txtEmailR.Text = persona.Email;

                dniRemitenteCargado = dni;
            }
            else
            {
                txtNombreD.Text = persona.Nombre;
                txtApellidoD.Text = persona.Apellido;
                txtTelefonoD.Text = persona.Telefono;
                txtEmailD.Text = persona.Email;

                dniDestinatarioCargado = dni;
            }

            return true;
        }

        private void btnBuscarRemitente_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtDNIR.Text, out int dni) || dni <= 0)
            {
                MessageBox.Show("Ingrese un DNI válido.","Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BuscarPersona(dni, true);
        }

        private void btnBuscarDestinatario_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtDNID.Text, out int dni) || dni <= 0)
            {
                MessageBox.Show("Ingrese un DNI válido.", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BuscarPersona(dni, false);
        }

        public void ActualizarIdioma()
        {

        }

        private void txtDNIR_TextChanged(object sender, EventArgs e)
        {
            if (dniRemitenteCargado.HasValue && txtDNIR.Text != dniRemitenteCargado.Value.ToString())
            {
                LimpiarRemitente();
                dniRemitenteCargado = null;
            }
        }

        private void LimpiarRemitente()
        {
            txtNombreR.Clear();
            txtApellidoR.Clear();
            txtTelefonoR.Clear();
            txtEmailR.Clear();
        }

        private void txtDNID_TextChanged(object sender, EventArgs e)
        {
            if (dniDestinatarioCargado.HasValue && txtDNID.Text != dniDestinatarioCargado.Value.ToString())
            {
                LimpiarDestinatario();
                dniDestinatarioCargado = null;
            }
        }

        private void LimpiarDestinatario()
        {
            txtNombreD.Clear();
            txtApellidoD.Clear();
            txtTelefonoD.Clear();
            txtEmailD.Clear();
        }

        private void LimpiarFormulario()
        {
            txtDescripcion.Clear();

            txtPeso.Clear();
            txtAlto.Clear();
            txtAncho.Clear();
            txtLargo.Clear();

            txtDNIR.Clear();
            txtNombreR.Clear();
            txtApellidoR.Clear();
            txtTelefonoR.Clear();
            txtEmailR.Clear();

            txtDNID.Clear();
            txtNombreD.Clear();
            txtApellidoD.Clear();
            txtTelefonoD.Clear();
            txtEmailD.Clear();

            txtProvincia.Clear();
            txtCP.Clear();
            txtCiudad.Clear();
            txtDireccion.Clear();

            dniRemitenteCargado = null;
            dniDestinatarioCargado = null;
        }

        private void btnSalir_Click(object sender, EventArgs e)
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