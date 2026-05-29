using BLL.Gestores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDeEnviosGUI.Formularios.Administrador
{
    public partial class BitacoraDeEventosForm : Form
    {
        public BitacoraDeEventosForm()
        {
            InitializeComponent();
        }
        private EventoBLL bll = new EventoBLL();
        private void BitacoraDeEventosForm_Load(object sender, EventArgs e)
        {
            RefrescarGrilla();
            CargarFiltros();
        }
        private void RefrescarGrilla()
        {
            dataGridView1.DataSource = bll.ObtenerTodosLosEventos();
            dataGridView1.Columns["nombre"].Visible = false;
            dataGridView1.Columns["apellido"].Visible = false;
            dataGridView1.Columns["Email_Usuario"].Width = 150;
            MostrarNombreYApellido();
        }
        private void MostrarNombreYApellido()
        {
            if (dataGridView1.CurrentRow != null)
            {
                txtNombre.Text = dataGridView1.CurrentRow.Cells["nombre"].Value.ToString();

                txtApellido.Text = dataGridView1.CurrentRow.Cells["apellido"].Value.ToString();
            }
        }
        private void CargarFiltros()
        {
            comboBoxEmail.Refresh();
            comboBoxEmail.DataSource = bll.ObtenerEmails();
            comboBoxEmail.DisplayMember = "email";
            comboBoxEmail.ValueMember = "email";
            comboBoxEmail.SelectedIndex = -1;
            comboBoxEmail.Text = "";

            comboBoxEvento.DataSource = bll.ObtenerEventos();
            comboBoxEvento.DisplayMember = "evento";
            comboBoxEvento.ValueMember = "evento";
            comboBoxEvento.SelectedIndex = -1;
            comboBoxEvento.Text = "";

            comboBoxModulo.DataSource = bll.ObtenerModulos();
            comboBoxModulo.DisplayMember = "modulo";
            comboBoxModulo.ValueMember = "modulo";
            comboBoxModulo.SelectedIndex = -1;
            comboBoxModulo.Text = "";

            comboBoxCriticidad.Items.Clear();
            comboBoxCriticidad.Items.AddRange(new object[] { 1, 2, 3, 4, 5 });
            comboBoxCriticidad.SelectedIndex = -1;
            comboBoxCriticidad.Text = "";

            dateTimePickerDesde.Checked = false;
            dateTimePickerHasta.Checked = false;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MostrarNombreYApellido();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            string email = comboBoxEmail.SelectedIndex != -1 ? comboBoxEmail.Text : null;

            string evento = comboBoxEvento.SelectedIndex != -1 ? comboBoxEvento.Text : null;

            string modulo = comboBoxModulo.SelectedIndex != -1 ? comboBoxModulo.Text : null;

            int? criticidad = comboBoxCriticidad.SelectedIndex != -1 ? Convert.ToInt32(comboBoxCriticidad.Text) : null;

            DateTime? desde = dateTimePickerDesde.Checked ? dateTimePickerDesde.Value : null;
            DateTime? hasta = dateTimePickerHasta.Checked ? dateTimePickerHasta.Value : null;

            dataGridView1.DataSource = bll.FiltrarEventos(email, desde, hasta, modulo, evento, criticidad);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            CargarFiltros();
            RefrescarGrilla();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Esta opción se encuentra en desarrollo", "En desarrollo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
