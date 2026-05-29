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
    public partial class AdminGestionUsuarios : Form
    {
        public AdminGestionUsuarios()
        {
            InitializeComponent();
        }
        private UsuarioBLL bll = new UsuarioBLL();
        private void AdminGestionUsuarios_Load(object sender, EventArgs e)
        {
            RefrescarGrilla();
        }

        private void RefrescarGrilla()
        {
            dataGridView1.DataSource = bll.ObtenerUsuarios(true);
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                ResultadoOperacion r = bll.AltaUsuario(Convert.ToInt32(txtDni.Text), txtNombre.Text, txtApellido.Text, txtEmail.Text, comboBox1.SelectedIndex + 2);
                MessageBox.Show(r.Mensaje);
                if (r.Exitoso) RefrescarGrilla();
            }
        }
        private bool ValidarDatos()
        {
            try
            {
                if (!int.TryParse(txtDni.Text, out int dni) || txtDni.Text.Length < 7 || txtDni.Text.Length > 8) throw new Exception("No se ingresó un DNI válido");
                if (string.IsNullOrWhiteSpace(txtNombre.Text)) throw new Exception("No se ingresó un nombre");
                if (string.IsNullOrWhiteSpace(txtApellido.Text)) throw new Exception("No se ingresó un apellido");
                if (string.IsNullOrWhiteSpace(txtEmail.Text)) throw new Exception("No se ingresó un email");
                if (!FormatoMail(txtEmail.Text)) throw new Exception("El formato del email no es válido");
                if (comboBox1.SelectedIndex == -1) throw new Exception("No se seleccionó ningún rol");
                return true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
        }
        private bool FormatoMail(string email)
        {
            string formato = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            bool valido = Regex.IsMatch(email, formato);
            return valido;
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {

        }

        private void btnActivar_Click(object sender, EventArgs e)
        {

        }

        private void btnDesactivar_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
