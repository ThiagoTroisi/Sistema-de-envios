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
    public partial class MaestroPersonasForm : Form, IObserverIdioma
    {
        private PersonaBLL personabll = new PersonaBLL();
        private ModoFormulario modoactual = ModoFormulario.Consulta;
        private int dniseleccionado;
        public MaestroPersonasForm()
        {
            InitializeComponent();
            GestorIdiomas.Instancia.Registrar(this);
        }
        private int? dniInicial;
        private bool cerrarDespuesDeAlta;
        public MaestroPersonasForm(ModoFormulario modo, int dni)
        {
            InitializeComponent();
            GestorIdiomas.Instancia.Registrar(this);

            modoactual = modo;
            dniInicial = dni;
            cerrarDespuesDeAlta = true;
        }
        private void MaestroPersonasForm_Load(object sender, EventArgs e)
        {
            radioButtonActivos.Checked = true;

            if (dniInicial.HasValue)
            {
                CambiarModo(modoactual);
                txtDni.Text = dniInicial.Value.ToString();
            }
            else
            {
                CambiarModo(ModoFormulario.Consulta);
                RefrescarGrilla();
            }

            ActualizarIdioma();
        }
        private void RefrescarGrilla()
        {
            dataGridViewPersonas.DataSource = personabll.ObtenerPersonas(radioButtonTodos.Checked);
            CargarDatos();
        }

        private void CambiarModo(ModoFormulario modo)
        {
            modoactual = modo;

            switch (modo)
            {
                case ModoFormulario.Consulta:
                    lblModo.Text = Traducciones.Traducir("modo_consulta");
                    btnAplicar.Enabled = false;
                    btnCancelar.Enabled = false;
                    btnAlta.Enabled = true;
                    btnBaja.Enabled = true;
                    btnModificar.Enabled = true;
                    btnReactivar.Enabled = true;
                    radioButtonActivos.Enabled = true;
                    radioButtonTodos.Enabled = true;
                    HabilitarCampos(false);
                    break;

                case ModoFormulario.Alta:
                    lblModo.Text = Traducciones.Traducir("modo_alta");
                    btnAplicar.Enabled = true;
                    btnCancelar.Enabled = true;
                    btnAlta.Enabled = false;
                    btnBaja.Enabled = false;
                    btnModificar.Enabled = false;
                    btnReactivar.Enabled = false;
                    radioButtonActivos.Enabled = false;
                    radioButtonTodos.Enabled = false;
                    LimpiarTextboxes();
                    HabilitarCampos(true);
                    break;

                case ModoFormulario.Modificar:
                    lblModo.Text = Traducciones.Traducir("modo_modificar");
                    btnAplicar.Enabled = true;
                    btnCancelar.Enabled = true;
                    btnAlta.Enabled = false;
                    btnBaja.Enabled = false;
                    btnModificar.Enabled = false;
                    btnReactivar.Enabled = false;
                    radioButtonActivos.Enabled = false;
                    radioButtonTodos.Enabled = false;
                    HabilitarCampos(true);
                    txtDni.Enabled = false;
                    break;
            }
        }

        private void HabilitarCampos(bool estado)
        {
            txtDni.Enabled = estado;
            txtNombre.Enabled = estado;
            txtApellido.Enabled = estado;
            txtTelefono.Enabled = estado;
            txtEmail.Enabled = estado;
        }

        private void LimpiarTextboxes()
        {
            txtDni.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
        }

        private void CargarDatos()
        {
            if (dataGridViewPersonas.SelectedRows.Count == 0) return;

            DataGridViewRow fila = dataGridViewPersonas.SelectedRows[0];

            if (fila.IsNewRow) return;

            if (fila.Cells["dni"].Value == null || fila.Cells["dni"].Value == DBNull.Value) return;

            dniseleccionado = Convert.ToInt32(fila.Cells["dni"].Value);
            txtDni.Text = fila.Cells["dni"].Value.ToString();
            txtNombre.Text = fila.Cells["nombre"].Value.ToString();
            txtApellido.Text = fila.Cells["apellido"].Value.ToString();
            txtTelefono.Text = fila.Cells["telefono"].Value.ToString();
            txtEmail.Text = fila.Cells["email"].Value.ToString();
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            CambiarModo(ModoFormulario.Alta);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDni.Text))
            {
                MessageBox.Show(Traducciones.Traducir("seleccionar_persona"), "", MessageBoxButtons.OK);
                return;
            }

            CambiarModo(ModoFormulario.Modificar);
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDni.Text))
            {
                MessageBox.Show(Traducciones.Traducir("seleccionar_persona"), "", MessageBoxButtons.OK);
                return;
            }

            try
            {
                personabll.BajaPersona(Convert.ToInt32(txtDni.Text));
                MessageBox.Show(Traducciones.Traducir("baja_persona_exitosa"), "", MessageBoxButtons.OK);
                LimpiarTextboxes();
                RefrescarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK);
            }
        }

        private void btnReactivar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDni.Text))
            {
                MessageBox.Show(Traducciones.Traducir("seleccionar_persona"), "", MessageBoxButtons.OK);
                return;
            }

            try
            {
                personabll.ReactivarPersona(Convert.ToInt32(txtDni.Text));
                MessageBox.Show(Traducciones.Traducir("reactivacion_persona_exitosa"), "", MessageBoxButtons.OK);
                LimpiarTextboxes();
                RefrescarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK);
            }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            try
            {
                switch (modoactual)
                {
                    case ModoFormulario.Alta:
                        PersonaBE nueva = new PersonaBE(Convert.ToInt32(txtDni.Text), txtNombre.Text, txtApellido.Text, txtTelefono.Text, txtEmail.Text);
                        personabll.AltaPersona(nueva);
                        MessageBox.Show(Traducciones.Traducir("alta_persona_exitosa"), "", MessageBoxButtons.OK);
                        if (cerrarDespuesDeAlta)
                        {
                            this.Close();
                            return;
                        }
                        break;

                    case ModoFormulario.Modificar:
                        if (string.IsNullOrWhiteSpace(txtDni.Text))
                        {
                            MessageBox.Show(Traducciones.Traducir("seleccionar_persona"), "", MessageBoxButtons.OK);
                            return;
                        }

                        PersonaBE modificada = new PersonaBE(Convert.ToInt32(txtDni.Text), txtNombre.Text, txtApellido.Text, txtTelefono.Text, txtEmail.Text);
                        personabll.ModificarPersona(modificada);
                        MessageBox.Show(Traducciones.Traducir("modificacion_persona_exitosa"), "", MessageBoxButtons.OK);
                        break;
                }

                LimpiarTextboxes();
                CambiarModo(ModoFormulario.Consulta);
                RefrescarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarTextboxes();
            CambiarModo(ModoFormulario.Consulta);
            RefrescarGrilla();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButtonActivos_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonActivos.Checked) RefrescarGrilla();
        }

        private void radioButtonTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonTodos.Checked) RefrescarGrilla();
        }

        private void dataGridViewPersonas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CargarDatos();
        }

        public void ActualizarIdioma()
        {
            this.Text = Traducciones.Traducir("GestionPersonas");

            lblModoActual.Text = Traducciones.Traducir("ModoActual");

            lblDNI.Text = Traducciones.Traducir("DNI");
            lblNombre.Text = Traducciones.Traducir("Nombre");
            lblApellido.Text = Traducciones.Traducir("Apellido");
            lblTelefono.Text = Traducciones.Traducir("Telefono");
            lblEmail.Text = Traducciones.Traducir("Email");

            radioButtonActivos.Text = Traducciones.Traducir("Activos");
            radioButtonTodos.Text = Traducciones.Traducir("Todos");

            btnAlta.Text = Traducciones.Traducir("Alta");
            btnBaja.Text = Traducciones.Traducir("Baja");
            btnModificar.Text = Traducciones.Traducir("Modificar");
            btnReactivar.Text = Traducciones.Traducir("Reactivar");
            btnAplicar.Text = Traducciones.Traducir("Aplicar");
            btnCancelar.Text = Traducciones.Traducir("Cancelar");
            btnSalir.Text = Traducciones.Traducir("Salir");

            if (dataGridViewPersonas.DataSource != null)
            {
                dataGridViewPersonas.Columns["dni"].HeaderText = Traducciones.Traducir("DNI");
                dataGridViewPersonas.Columns["nombre"].HeaderText = Traducciones.Traducir("Nombre");
                dataGridViewPersonas.Columns["apellido"].HeaderText = Traducciones.Traducir("Apellido");
                dataGridViewPersonas.Columns["telefono"].HeaderText = Traducciones.Traducir("Telefono");
                dataGridViewPersonas.Columns["email"].HeaderText = Traducciones.Traducir("Email");
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            GestorIdiomas.Instancia.Desregistrar(this);
            base.OnFormClosed(e);
        }

    }
}
