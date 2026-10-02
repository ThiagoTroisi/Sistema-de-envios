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
        private PaqueteBLL paquetebll = new PaqueteBLL();
        private DestinoBLL destinobll = new DestinoBLL();

        private ModoGestionEnvio modoactual = ModoGestionEnvio.Consulta;

        private enum ModoGestionEnvio
        {
            Consulta,
            Alta,
            Modificacion,
            Baja
        }

        private EnvioBE envioSeleccionado;

        private int? dniRemitenteCargado;
        private int? dniDestinatarioCargado;

        public GestionEnviosForm()
        {
            InitializeComponent();
            GestorIdiomas.Instancia.Registrar(this);
        }

        private void GestionEnviosForm_Load(object sender, EventArgs e)
        {
            ConfigurarFiltros();
            dataGridViewEnvios.ReadOnly = true;
            dataGridViewEnvios.AllowUserToAddRows = false;
            dataGridViewEnvios.AllowUserToDeleteRows = false;
            dataGridViewEnvios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewEnvios.MultiSelect = false;
            CambiarModo(ModoGestionEnvio.Consulta);
            CargarEnvios();
            ActualizarIdioma();
        }

        private void ConfigurarFiltros()
        {
            cboEstado.Items.Clear();

            cboEstado.Items.Add("Todos");
            cboEstado.Items.Add("Registrado");
            cboEstado.Items.Add("Pagado");
            cboEstado.Items.Add("Aprobado");
            cboEstado.Items.Add("Asignado");
            cboEstado.Items.Add("En distribución");
            cboEstado.Items.Add("En sucursal");
            cboEstado.Items.Add("En devolución");
            cboEstado.Items.Add("Entregado");
            cboEstado.Items.Add("Cancelado");

            cboEstado.SelectedIndex = 0;

            dateTimePickerDesde.ShowCheckBox = true;
            dateTimePickerHasta.ShowCheckBox = true;
        }

        private void CargarEnvios()
        {
            DateTime? fechaDesde = dateTimePickerDesde.Checked
                ? dateTimePickerDesde.Value.Date
                : null;

            DateTime? fechaHasta = dateTimePickerHasta.Checked
                ? dateTimePickerHasta.Value.Date.AddDays(1)
                : null;

            DataTable tabla = enviobll.ObtenerEnvios(
                cboEstado.Text,
                txtCodigoSeguimiento.Text,
                txtDNIRFiltro.Text,
                txtDNIDFiltro.Text,
                fechaDesde,
                fechaHasta);

            dataGridViewEnvios.DataSource = tabla;

            dataGridViewEnvios.ReadOnly = true;
            dataGridViewEnvios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewEnvios.MultiSelect = false;

            if (dataGridViewEnvios.Columns["id_envio"] != null)
                dataGridViewEnvios.Columns["id_envio"].Visible = false;

            if (dataGridViewEnvios.Columns["codigo_seguimiento"] != null)
                dataGridViewEnvios.Columns["codigo_seguimiento"].HeaderText = "Código";

            if (dataGridViewEnvios.Columns["fecha_registro"] != null)
                dataGridViewEnvios.Columns["fecha_registro"].HeaderText = "Fecha";

            if (dataGridViewEnvios.Columns["remitente"] != null)
                dataGridViewEnvios.Columns["remitente"].HeaderText = "Remitente";

            if (dataGridViewEnvios.Columns["destinatario"] != null)
                dataGridViewEnvios.Columns["destinatario"].HeaderText = "Destinatario";

            if (dataGridViewEnvios.Columns["destino"] != null)
            {
                dataGridViewEnvios.Columns["destino"].HeaderText = "Destino";
                dataGridViewEnvios.Columns["destino"].Width = 200;
            }

            if (dataGridViewEnvios.Columns["estado"] != null)
                dataGridViewEnvios.Columns["estado"].HeaderText = "Estado";

            ActualizarBotonesConsulta();
        }
        private void ActualizarBotonesConsulta()
        {
            if (modoactual != ModoGestionEnvio.Consulta)
                return;

            btnRegistrarEnvio.Enabled = true;

            if (envioSeleccionado == null)
            {
                btnModificarEnvio.Enabled = true;
                btnCancelarEnvio.Enabled = true;
                return;
            }

            btnModificarEnvio.Enabled =
                envioSeleccionado.Estado == "Registrado" ||
                envioSeleccionado.Estado == "Pagado" ||
                envioSeleccionado.Estado == "Aprobado";

            btnCancelarEnvio.Enabled =
                envioSeleccionado.Estado != "En distribución" &&
                envioSeleccionado.Estado != "Entregado" &&
                envioSeleccionado.Estado != "Cancelado";
        }
        private void CambiarModo(ModoGestionEnvio modo)
        {
            modoactual = modo;

            switch (modoactual)
            {
                case ModoGestionEnvio.Consulta:
                    ConfigurarModoConsulta();
                    break;

                case ModoGestionEnvio.Alta:
                    ConfigurarModoAlta();
                    break;

                case ModoGestionEnvio.Modificacion:
                    ConfigurarModoModificacion();
                    break;

                case ModoGestionEnvio.Baja:
                    ConfigurarModoBaja();
                    break;
            }
        }

        private void ConfigurarModoConsulta()
        {
            HabilitarDatos(false);

            txtDNIR.ReadOnly = true;
            txtDNID.ReadOnly = true;

            txtNombreR.ReadOnly = true;
            txtApellidoR.ReadOnly = true;
            txtTelefonoR.ReadOnly = true;
            txtEmailR.ReadOnly = true;

            txtNombreD.ReadOnly = true;
            txtApellidoD.ReadOnly = true;
            txtTelefonoD.ReadOnly = true;
            txtEmailD.ReadOnly = true;

            btnRegistrarEnvio.Enabled = true;
            btnModificarEnvio.Enabled = true;
            btnCancelarEnvio.Enabled = true;

            btnAplicar.Enabled = false;
            btnCancelar.Enabled = false;

            dataGridViewEnvios.Enabled = true;

            ActualizarBotonesConsulta();
        }

        private void ConfigurarModoAlta()
        {
            envioSeleccionado = null;

            LimpiarFormulario();

            HabilitarDatos(true);

            txtDNIR.ReadOnly = false;
            txtDNID.ReadOnly = false;

            txtNombreR.ReadOnly = true;
            txtApellidoR.ReadOnly = true;
            txtTelefonoR.ReadOnly = true;
            txtEmailR.ReadOnly = true;

            txtNombreD.ReadOnly = true;
            txtApellidoD.ReadOnly = true;
            txtTelefonoD.ReadOnly = true;
            txtEmailD.ReadOnly = true;

            btnRegistrarEnvio.Enabled = false;
            btnModificarEnvio.Enabled = false;
            btnCancelarEnvio.Enabled = false;

            btnBuscarRemitente.Enabled = true;
            btnBuscarDestinatario.Enabled = true;

            btnAplicar.Enabled = true;
            btnCancelar.Enabled = true;

            dataGridViewEnvios.Enabled = false;
        }

        private void ConfigurarModoModificacion()
        {
            HabilitarDatos(true);

            btnBuscarRemitente.Enabled = false;
            btnBuscarDestinatario.Enabled = false;

            txtDNIR.ReadOnly = true;
            txtDNID.ReadOnly = true;

            txtNombreR.ReadOnly = true;
            txtApellidoR.ReadOnly = true;
            txtTelefonoR.ReadOnly = true;
            txtEmailR.ReadOnly = true;

            txtNombreD.ReadOnly = true;
            txtApellidoD.ReadOnly = true;
            txtTelefonoD.ReadOnly = true;
            txtEmailD.ReadOnly = true;

            btnRegistrarEnvio.Enabled = false;
            btnModificarEnvio.Enabled = false;
            btnCancelarEnvio.Enabled = false;

            btnAplicar.Enabled = true;
            btnCancelar.Enabled = true;

            dataGridViewEnvios.Enabled = true;
        }

        private void ConfigurarModoBaja()
        {
            HabilitarDatos(false);

            btnBuscarRemitente.Enabled = false;
            btnBuscarDestinatario.Enabled = false;

            txtDNIR.ReadOnly = true;
            txtDNID.ReadOnly = true;

            txtNombreR.ReadOnly = true;
            txtApellidoR.ReadOnly = true;
            txtTelefonoR.ReadOnly = true;
            txtEmailR.ReadOnly = true;

            txtNombreD.ReadOnly = true;
            txtApellidoD.ReadOnly = true;
            txtTelefonoD.ReadOnly = true;
            txtEmailD.ReadOnly = true;

            btnRegistrarEnvio.Enabled = false;
            btnModificarEnvio.Enabled = false;
            btnCancelarEnvio.Enabled = false;

            btnAplicar.Enabled = true;
            btnCancelar.Enabled = true;

            dataGridViewEnvios.Enabled = true;
        }

        private void HabilitarDatos(bool habilitar)
        {
            txtDescripcion.Enabled = habilitar;
            txtPeso.Enabled = habilitar;
            txtAlto.Enabled = habilitar;
            txtAncho.Enabled = habilitar;
            txtLargo.Enabled = habilitar;

            txtProvincia.Enabled = habilitar;
            txtCP.Enabled = habilitar;
            txtCiudad.Enabled = habilitar;
            txtDireccion.Enabled = habilitar;

            btnBuscarRemitente.Enabled = habilitar;
            btnBuscarDestinatario.Enabled = habilitar;
        }

        private void btnRegistrarEnvio_Click(object sender, EventArgs e)
        {
            CambiarModo(ModoGestionEnvio.Alta);
        }

        private void btnCancelarEnvio_Click(object sender, EventArgs e)
        {
            if (envioSeleccionado == null)
            {
                MessageBox.Show("Seleccione un envío.", "Cancelar envío", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            if (envioSeleccionado.Estado == "En distribución" ||
                envioSeleccionado.Estado == "Entregado" ||
                envioSeleccionado.Estado == "Cancelado")
            {
                MessageBox.Show("El envío no puede cancelarse en su estado actual.", "Cancelar envío", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            CambiarModo(ModoGestionEnvio.Baja);
        }

        private void btnModificarEnvio_Click(object sender, EventArgs e)
        {
            if (envioSeleccionado == null)
            {
                MessageBox.Show("Seleccione un envío.", "Modificar envío", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            if (envioSeleccionado.Estado != "Registrado" &&
                envioSeleccionado.Estado != "Pagado" &&
                envioSeleccionado.Estado != "Aprobado")
            {
                MessageBox.Show("El envío no puede modificarse en su estado actual.", "Modificar envío", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            CambiarModo(ModoGestionEnvio.Modificacion);
        }

        private void dataGridViewEnvios_SelectionChanged(object sender, EventArgs e)
        {
            if (modoactual == ModoGestionEnvio.Alta) return;

            if (dataGridViewEnvios.CurrentRow == null)
            {
                envioSeleccionado = null;
                LimpiarFormulario();

                if (modoactual == ModoGestionEnvio.Consulta) ActualizarBotonesConsulta();

                return;
            }

            if (dataGridViewEnvios.CurrentRow.Cells["id_envio"].Value == null)
            {
                envioSeleccionado = null;
                return;
            }

            int idEnvio = Convert.ToInt32(dataGridViewEnvios.CurrentRow.Cells["id_envio"].Value);

            envioSeleccionado = enviobll.ConsultaPorId(idEnvio);

            if (envioSeleccionado == null)
            {
                LimpiarFormulario();
                return;
            }

            CargarDatosEnvio(envioSeleccionado);

            if (modoactual == ModoGestionEnvio.Consulta) ActualizarBotonesConsulta();
        }

        private void CargarDatosEnvio(EnvioBE envio)
        {
            PaqueteBE paquete = paquetebll.ConsultaPorId(envio.IdPaquete);
            DestinoBE destino = destinobll.ConsultaPorId(envio.IdDestino);

            PersonaBE remitente = personabll.ConsultaPorDNI(envio.IdRemitente);
            PersonaBE destinatario = personabll.ConsultaPorDNI(envio.IdDestinatario);

            txtDescripcion.Text = paquete.Descripcion;
            txtPeso.Text = paquete.Peso.ToString();
            txtAlto.Text = paquete.Alto.ToString();
            txtAncho.Text = paquete.Ancho.ToString();
            txtLargo.Text = paquete.Largo.ToString();

            txtProvincia.Text = destino.Provincia;
            txtCP.Text = destino.CodigoPostal;
            txtCiudad.Text = destino.Ciudad;
            txtDireccion.Text = destino.Direccion;

            txtDNIR.Text = remitente.DNI.ToString();
            txtNombreR.Text = remitente.Nombre;
            txtApellidoR.Text = remitente.Apellido;
            txtTelefonoR.Text = remitente.Telefono;
            txtEmailR.Text = remitente.Email;

            txtDNID.Text = destinatario.DNI.ToString();
            txtNombreD.Text = destinatario.Nombre;
            txtApellidoD.Text = destinatario.Apellido;
            txtTelefonoD.Text = destinatario.Telefono;
            txtEmailD.Text = destinatario.Email;

            dniRemitenteCargado = remitente.DNI;
            dniDestinatarioCargado = destinatario.DNI;
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            try
            {
                switch (modoactual)
                {
                    case ModoGestionEnvio.Alta:
                        AplicarAlta();
                        break;

                    case ModoGestionEnvio.Modificacion:
                        AplicarModificacion();
                        break;

                    case ModoGestionEnvio.Baja:
                        AplicarBaja();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void AplicarAlta()
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

            PaqueteBE paquete = new PaqueteBE
            {
                Descripcion = txtDescripcion.Text,
                Peso = peso,
                Alto = alto,
                Ancho = ancho,
                Largo = largo
            };

            if (paquete.Alto > 2.50m || paquete.Ancho > 2.50m || paquete.Largo > 3.00m)
                throw new Exception("Las dimensiones del paquete superan el tamaño máximo permitido.");

            if (dniRemitente == dniDestinatario)
            {
                DialogResult resultado = MessageBox.Show("El remitente y el destinatario son la misma persona. ¿Desea continuar con el registro del envío?", "Confirmar envío", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado != DialogResult.Yes)
                    return;
            }

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
                dniDestinatario);

            enviobll.RegistrarEnvio(paquete, destino, envio);

            MessageBox.Show($"El envío se registró correctamente.\nCódigo de seguimiento: {envio.CodigoSeguimiento}", "Registro de envío", MessageBoxButtons.OK, MessageBoxIcon.Information);

            CargarEnvios();
            CambiarModo(ModoGestionEnvio.Consulta);
        }

        private void AplicarModificacion()
        {
            if (envioSeleccionado == null)
                throw new Exception("No hay un envío seleccionado.");

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

            PaqueteBE paquete = new PaqueteBE
            {
                IdPaquete = envioSeleccionado.IdPaquete,
                Descripcion = txtDescripcion.Text,
                Peso = peso,
                Alto = alto,
                Ancho = ancho,
                Largo = largo
            };

            DestinoBE destino = new DestinoBE
            {
                IdDestino = envioSeleccionado.IdDestino,
                Direccion = txtDireccion.Text,
                Ciudad = txtCiudad.Text,
                CodigoPostal = txtCP.Text,
                Provincia = txtProvincia.Text
            };

            enviobll.ModificarEnvio(envioSeleccionado, paquete, destino);

            MessageBox.Show("El envío se modificó correctamente.", "Modificar envío", MessageBoxButtons.OK, MessageBoxIcon.Information);

            CargarEnvios();
            CambiarModo(ModoGestionEnvio.Consulta);
        }

        private void AplicarBaja()
        {
            if (envioSeleccionado == null) throw new Exception("No hay un envío seleccionado.");

            DialogResult resultado = MessageBox.Show("¿Está seguro de que desea cancelar el envío seleccionado?", "Cancelar envío", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado != DialogResult.Yes) return;

            enviobll.CancelarEnvio(envioSeleccionado.IdEnvio);

            MessageBox.Show("El envío fue cancelado correctamente.", "Cancelar envío", MessageBoxButtons.OK, MessageBoxIcon.Information);

            CargarEnvios();
            CambiarModo(ModoGestionEnvio.Consulta);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            CambiarModo(ModoGestionEnvio.Consulta);
            dataGridViewEnvios_SelectionChanged(sender, e);
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
                MessageBox.Show("Ingrese un DNI válido.", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void txtDNIR_TextChanged(object sender, EventArgs e)
        {
            if (modoactual != ModoGestionEnvio.Alta)
                return;

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
            if (modoactual != ModoGestionEnvio.Alta)
                return;

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
        
        private void txtCodigoSeguimiento_TextChanged(object sender, EventArgs e)
        {
            if (modoactual == ModoGestionEnvio.Consulta)
                CargarEnvios();
        }

        private void txtDNIRFiltro_TextChanged(object sender, EventArgs e)
        {
            if (modoactual == ModoGestionEnvio.Consulta)
                CargarEnvios();
        }

        private void txtDNIDFiltro_TextChanged(object sender, EventArgs e)
        {
            if (modoactual == ModoGestionEnvio.Consulta)
                CargarEnvios();
        }

        private void cboEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modoactual == ModoGestionEnvio.Consulta)
                CargarEnvios();
        }

        private void dateTimePickerDesde_ValueChanged(object sender, EventArgs e)
        {
            if (modoactual == ModoGestionEnvio.Consulta)
                CargarEnvios();
        }

        private void dateTimePickerHasta_ValueChanged(object sender, EventArgs e)
        {
            if (modoactual == ModoGestionEnvio.Consulta)
                CargarEnvios();
        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            cboEstado.SelectedIndex = 0;

            txtCodigoSeguimiento.Clear();
            txtDNIRFiltro.Clear();
            txtDNIDFiltro.Clear();

            dateTimePickerDesde.Checked = false;
            dateTimePickerHasta.Checked = false;

            CargarEnvios();
        }
        public void ActualizarIdioma()
        {
            this.Text = Traducciones.Traducir("GestionEnvios");

            lblCodigoSeguimiento.Text = Traducciones.Traducir("Codigo");
            lblDNIRFiltro.Text = Traducciones.Traducir("DNI");
            lblDNIDFiltro.Text = Traducciones.Traducir("DNI");
            lblEstado.Text = Traducciones.Traducir("Estado");
            lblDesde.Text = Traducciones.Traducir("Desde");
            lblHasta.Text = Traducciones.Traducir("Hasta");

            lblPaquete.Text = Traducciones.Traducir("Paquete");
            lblDescripcion.Text = Traducciones.Traducir("Descripcion");
            lblPeso.Text = Traducciones.Traducir("Peso");
            lblAlto.Text = Traducciones.Traducir("Alto");
            lblAncho.Text = Traducciones.Traducir("Ancho");
            lblLargo.Text = Traducciones.Traducir("Largo");

            lblDestino.Text = Traducciones.Traducir("Destino");
            lblProvincia.Text = Traducciones.Traducir("Provincia");
            lblCP.Text = Traducciones.Traducir("CodigoPostal");
            lblCiudad.Text = Traducciones.Traducir("Ciudad");
            lblDireccion.Text = Traducciones.Traducir("Direccion");

            lblRemitente.Text = Traducciones.Traducir("Remitente");
            lblDNIR.Text = Traducciones.Traducir("DNI");
            lblNombreR.Text = Traducciones.Traducir("Nombre");
            lblApellidoR.Text = Traducciones.Traducir("Apellido");
            lblTelefonoR.Text = Traducciones.Traducir("Telefono");
            lblEmailR.Text = Traducciones.Traducir("Email");

            lblDestinatario.Text = Traducciones.Traducir("Destinatario");
            lblDNID.Text = Traducciones.Traducir("DNI");
            lblNombreD.Text = Traducciones.Traducir("Nombre");
            lblApellidoD.Text = Traducciones.Traducir("Apellido");
            lblTelefonoD.Text = Traducciones.Traducir("Telefono");
            lblEmailD.Text = Traducciones.Traducir("Email");

            btnRegistrarEnvio.Text = Traducciones.Traducir("RegistrarEnvio");
            btnModificarEnvio.Text = Traducciones.Traducir("ModificarEnvio");
            btnCancelarEnvio.Text = Traducciones.Traducir("CancelarEnvio");
            btnAplicar.Text = Traducciones.Traducir("Aplicar");
            btnCancelar.Text = Traducciones.Traducir("Cancelar");
            btnLimpiarFiltros.Text = Traducciones.Traducir("LimpiarFiltros");
            btnSalir.Text = Traducciones.Traducir("Salir");
            lblTodosLosEnvios.Text = Traducciones.Traducir("TodosLosEnvios");

            if (dataGridViewEnvios.DataSource != null)
            {
                if (dataGridViewEnvios.Columns["codigo_seguimiento"] != null)
                    dataGridViewEnvios.Columns["codigo_seguimiento"].HeaderText = Traducciones.Traducir("Codigo");

                if (dataGridViewEnvios.Columns["fecha_registro"] != null)
                    dataGridViewEnvios.Columns["fecha_registro"].HeaderText = Traducciones.Traducir("Fecha");

                if (dataGridViewEnvios.Columns["remitente"] != null)
                    dataGridViewEnvios.Columns["remitente"].HeaderText = Traducciones.Traducir("Remitente");

                if (dataGridViewEnvios.Columns["destinatario"] != null)
                    dataGridViewEnvios.Columns["destinatario"].HeaderText = Traducciones.Traducir("Destinatario");

                if (dataGridViewEnvios.Columns["destino"] != null)
                    dataGridViewEnvios.Columns["destino"].HeaderText = Traducciones.Traducir("Destino");

                if (dataGridViewEnvios.Columns["estado"] != null)
                    dataGridViewEnvios.Columns["estado"].HeaderText = Traducciones.Traducir("Estado");
            }
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