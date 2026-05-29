using BLL.Gestores;
using BLL.Otros;
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
    public partial class GestionUsuariosForm : Form
    {
        public GestionUsuariosForm()
        {
            InitializeComponent();
        }
        private UsuarioBLL bll = new UsuarioBLL();
        private ModoFormulario modoactual = ModoFormulario.Consulta;
        private int dniseleccionado;
        private void AdminGestionUsuarios_Load(object sender, EventArgs e)
        {
            CargarRoles();
            CambiarModo(ModoFormulario.Consulta);
            RefrescarGrilla();
        }

        private void RefrescarGrilla()
        {
            if (radioButtonActivos.Checked) dataGridViewUsuarios.DataSource = bll.ObtenerUsuarios(false);
            else dataGridViewUsuarios.DataSource = bll.ObtenerUsuarios(true);
            dataGridViewUsuarios.Columns["id_rol"].Visible = false;
            CargarDatos();
        }

        private void CambiarModo(ModoFormulario modo)
        {
            modoactual = modo;
            switch (modo)
            {
                case ModoFormulario.Consulta:
                    labelModo.Text = "Consulta";
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
                    labelModo.Text = "Añadir";
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
                    labelModo.Text = "Modificar";
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
                    labelModo.Text = "Desbloquear";
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
            comboBoxRol.DataSource = bll.ObtenerRoles();
            comboBoxRol.DisplayMember = "descripcion";
            comboBoxRol.ValueMember = "id_rol";
            comboBoxRol.SelectedIndex = -1;
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
            var res = bll.ActivarUsuario(dniseleccionado);
            MessageBox.Show(res.Mensaje);
            RefrescarGrilla();
        }

        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            var res = bll.DesactivarUsuario(dniseleccionado);
            MessageBox.Show(res.Mensaje);
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
                    MessageBox.Show(bll.AltaUsuario(Convert.ToInt32(txtDni.Text), txtNombre.Text, txtApellido.Text, txtEmail.Text, Convert.ToInt32(comboBoxRol.SelectedValue)).Mensaje);

                    break;

                case ModoFormulario.Modificar:
                    if (string.IsNullOrWhiteSpace(txtDni.Text))
                    {
                        MessageBox.Show("Seleccione un usuario en la grilla", "Seleccione un usuario", MessageBoxButtons.OK);
                        return;
                    }
                    MessageBox.Show(bll.ModificarUsuario(Convert.ToInt32(txtDni.Text), txtNombre.Text, txtApellido.Text, txtEmail.Text, Convert.ToInt32(comboBoxRol.SelectedValue)).Mensaje);
                    break;

                case ModoFormulario.Desbloquear:
                    if (string.IsNullOrWhiteSpace(txtDni.Text))
                    {
                        MessageBox.Show("Seleccione un usuario en la grilla", "Seleccione un usuario", MessageBoxButtons.OK);
                        return;
                    }
                    MessageBox.Show(bll.DesbloquearUsuario(dniseleccionado).Mensaje);
                    break;
            }
            LimpiarTextboxes();
            CambiarModo(ModoFormulario.Consulta);
            RefrescarGrilla();
        }
    }
}
