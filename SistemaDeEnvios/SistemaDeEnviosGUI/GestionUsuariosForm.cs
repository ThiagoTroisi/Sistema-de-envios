using BLL;
using BLL.Otros;
using Servicios.GestionIdiomas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDeEnviosGUI.Formularios.Administrador
{
    public enum ModoFormulario
    {
        Consulta,
        Alta,
        Modificar,
        Activar,
        Desactivar,
        Desbloquear
    }
    public partial class GestionUsuariosForm : Form, IObserverIdioma
    {
        public GestionUsuariosForm()
        {
            InitializeComponent();
            GestorIdiomas.Instancia.Registrar(this);
        }
        private UsuarioBLL bll = new UsuarioBLL();
        private ModoFormulario modoactual = ModoFormulario.Consulta;
        private int dniseleccionado;
        private void AdminGestionUsuarios_Load(object sender, EventArgs e)
        {
            CargarRoles();
            CambiarModo(ModoFormulario.Consulta);
            RefrescarGrilla();
            ActualizarIdioma();
        }

        private void RefrescarGrilla()
        {
            if (radioButtonActivos.Checked) dataGridViewUsuarios.DataSource = bll.ObtenerUsuarios(false);
            else dataGridViewUsuarios.DataSource = bll.ObtenerUsuarios(true);
            dataGridViewUsuarios.Columns["id_rol"].Visible = false;
            foreach (DataGridViewRow fila in dataGridViewUsuarios.Rows)
            {
                if (fila.IsNewRow) continue;
                fila.Cells["rol"].Value = TraducirRol(fila.Cells["rol"].Value.ToString());
            }
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
                    btnModificar.Enabled = true;
                    btnActivar.Enabled = true;
                    btnDesactivar.Enabled = true;
                    btnDesbloquear.Enabled = true;
                    radioButtonActivos.Enabled = true;
                    radioButtonTodos.Enabled = true;
                    HabilitarCampos(false);
                    break;

                case ModoFormulario.Alta:
                    lblModo.Text = Traducciones.Traducir("modo_alta");
                    btnAplicar.Enabled = true;
                    btnCancelar.Enabled = true;
                    btnAlta.Enabled = false;
                    btnModificar.Enabled = false;
                    btnActivar.Enabled = false;
                    btnDesactivar.Enabled = false;
                    btnDesbloquear.Enabled = false;
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
                    btnModificar.Enabled = false;
                    btnActivar.Enabled = false;
                    btnDesactivar.Enabled = false;
                    btnDesbloquear.Enabled = false;
                    radioButtonActivos.Enabled = false;
                    radioButtonTodos.Enabled = false;
                    HabilitarCampos(true);
                    txtDni.Enabled = false;
                    break;

                case ModoFormulario.Desbloquear:
                    lblModo.Text = Traducciones.Traducir("modo_desbloquear");
                    btnAplicar.Enabled = true;
                    btnCancelar.Enabled = true;
                    btnAlta.Enabled = false;
                    btnModificar.Enabled = false;
                    btnActivar.Enabled = false;
                    btnDesactivar.Enabled = false;
                    btnDesbloquear.Enabled = false;
                    radioButtonActivos.Enabled = false;
                    radioButtonTodos.Enabled = false;
                    HabilitarCampos(false);
                    break;
            }
        }

        private void HabilitarCampos(bool estado)
        {
            txtDni.Enabled = estado;
            txtNombre.Enabled = estado;
            txtApellido.Enabled = estado;
            txtEmail.Enabled = estado;
            comboBoxRol.Enabled = estado;
            checkBoxActivo.Enabled = estado;
            checkBoxBloqueado.Enabled = estado;
        }

        private void LimpiarTextboxes()
        {
            txtDni.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtEmail.Clear();
            comboBoxRol.SelectedIndex = -1;
            comboBoxRol.Text = "";
            checkBoxActivo.Checked = false;
            checkBoxBloqueado.Checked = false;
        }

        private void CargarRoles()
        {
            DataTable dt = bll.ObtenerRoles();

            foreach (DataRow fila in dt.Rows)
            {
                fila["descripcion"] = TraducirRol(fila["descripcion"].ToString());
            }

            comboBoxRol.DataSource = dt;
            comboBoxRol.DisplayMember = "descripcion";
            comboBoxRol.ValueMember = "id_rol";
            comboBoxRol.SelectedIndex = -1;
        }
        private string TraducirRol(string rol)
        {
            switch (rol)
            {
                case "Administrador":
                    return Traducciones.Traducir("rol_administrador");

                case "Recepcionista":
                    return Traducciones.Traducir("rol_recepcionista");

                case "Gestor":
                    return Traducciones.Traducir("rol_gestor");

                case "Repartidor":
                    return Traducciones.Traducir("rol_repartidor");

                case "Remitente / Destinatario":
                    return Traducciones.Traducir("rol_cliente");

                default:
                    return rol;
            }
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            CambiarModo(ModoFormulario.Alta);
        }
        
        private void btnModificar_Click(object sender, EventArgs e)
        {
            CambiarModo(ModoFormulario.Modificar);
        }

        private void btnActivar_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Traducciones.Traducir(bll.ActivarUsuario(dniseleccionado).Mensaje));
            RefrescarGrilla();
        }

        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Traducciones.Traducir(bll.DesactivarUsuario(dniseleccionado).Mensaje));
            RefrescarGrilla();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButtonActivos_CheckedChanged(object sender, EventArgs e)
        {
            RefrescarGrilla();
        }

        private void radioButtonTodos_CheckedChanged(object sender, EventArgs e)
        {
            RefrescarGrilla();
        }

        private void dataGridViewUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CargarDatos();
        }
        private void CargarDatos()
        {
            DataGridViewRow fila = dataGridViewUsuarios.SelectedRows[0];
            if (fila.IsNewRow) return;
            if (fila.Cells["dni"].Value == null || fila.Cells["dni"].Value == DBNull.Value) return;
            dniseleccionado = Convert.ToInt32(fila.Cells["dni"].Value);
            txtDni.Text = fila.Cells["dni"].Value.ToString();
            txtNombre.Text = fila.Cells["nombre"].Value.ToString();
            txtApellido.Text = fila.Cells["apellido"].Value.ToString();
            txtEmail.Text = fila.Cells["email"].Value.ToString();
            comboBoxRol.SelectedValue = fila.Cells["id_rol"].Value;
            checkBoxActivo.Checked = Convert.ToBoolean(fila.Cells["estado"].Value);
            checkBoxBloqueado.Checked = Convert.ToBoolean(fila.Cells["bloqueado"].Value);
        }
        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            CambiarModo(ModoFormulario.Desbloquear);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarTextboxes();
            CambiarModo(ModoFormulario.Consulta);
            RefrescarGrilla();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            switch (modoactual)
            {
                case ModoFormulario.Alta:
                    MessageBox.Show(Traducciones.Traducir(bll.AltaUsuario(Convert.ToInt32(txtDni.Text), txtNombre.Text, txtApellido.Text, txtEmail.Text, Convert.ToInt32(comboBoxRol.SelectedValue)).Mensaje));

                    break;

                case ModoFormulario.Modificar:
                    if (string.IsNullOrWhiteSpace(txtDni.Text))
                    {
                        MessageBox.Show(Traducciones.Traducir("seleccionar usuario"), "", MessageBoxButtons.OK);
                        return;
                    }
                    MessageBox.Show(Traducciones.Traducir(bll.ModificarUsuario(Convert.ToInt32(txtDni.Text), txtNombre.Text, txtApellido.Text, txtEmail.Text, Convert.ToInt32(comboBoxRol.SelectedValue)).Mensaje));
                    break;

                case ModoFormulario.Desbloquear:
                    if (string.IsNullOrWhiteSpace(txtDni.Text))
                    {
                        MessageBox.Show(Traducciones.Traducir("seleccionar usuario"), "", MessageBoxButtons.OK);
                        return;
                    }
                    MessageBox.Show(Traducciones.Traducir(bll.DesbloquearUsuario(dniseleccionado).Mensaje));
                    break;
            }
            LimpiarTextboxes();
            CambiarModo(ModoFormulario.Consulta);
            RefrescarGrilla();
        }

        public void ActualizarIdioma()
        {
            this.Text = Traducciones.Traducir("GestionUsuarios");

            lblMostrar.Text = Traducciones.Traducir("Mostrar");
            lblModoActual.Text = Traducciones.Traducir("ModoActual");

            lblDNI.Text = Traducciones.Traducir("DNI");
            lblNombre.Text = Traducciones.Traducir("Nombre");
            lblApellido.Text = Traducciones.Traducir("Apellido");
            lblEmail.Text = Traducciones.Traducir("Email");
            lblRol.Text = Traducciones.Traducir("Rol");

            radioButtonActivos.Text = Traducciones.Traducir("Activos");
            radioButtonTodos.Text = Traducciones.Traducir("Todos");

            checkBoxActivo.Text = Traducciones.Traducir("Activo");
            checkBoxBloqueado.Text = Traducciones.Traducir("Bloqueado");

            btnAlta.Text = Traducciones.Traducir("Alta");
            btnModificar.Text = Traducciones.Traducir("Modificar");
            btnActivar.Text = Traducciones.Traducir("Activar");
            btnDesactivar.Text = Traducciones.Traducir("Desactivar");
            btnDesbloquear.Text = Traducciones.Traducir("Desbloquear");
            btnAplicar.Text = Traducciones.Traducir("Aplicar");
            btnCancelar.Text = Traducciones.Traducir("Cancelar");
            btnSalir.Text = Traducciones.Traducir("Salir");

            dataGridViewUsuarios.Columns["dni"].HeaderText = Traducciones.Traducir("DNI");
            dataGridViewUsuarios.Columns["nombre"].HeaderText = Traducciones.Traducir("Nombre");
            dataGridViewUsuarios.Columns["apellido"].HeaderText = Traducciones.Traducir("Apellido");
            dataGridViewUsuarios.Columns["email"].HeaderText = Traducciones.Traducir("Email");
            dataGridViewUsuarios.Columns["estado"].HeaderText = Traducciones.Traducir("Activo");
            dataGridViewUsuarios.Columns["bloqueado"].HeaderText = Traducciones.Traducir("Bloqueado");
            dataGridViewUsuarios.Columns["rol"].HeaderText = Traducciones.Traducir("Rol");

        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            GestorIdiomas.Instancia.Desregistrar(this);
            base.OnFormClosed(e);
        }
    }
}
