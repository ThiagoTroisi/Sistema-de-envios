using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Servicios;
using BLL;

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

            DateTime? desde = dateTimePickerDesde.Checked ? dateTimePickerDesde.Value.Date : null;
            DateTime? hasta = dateTimePickerHasta.Checked ? dateTimePickerHasta.Value.Date.AddDays(1).AddTicks(-1) : null;

            dataGridView1.DataSource = bll.FiltrarEventos(email, desde, hasta, modulo, evento, criticidad);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            CargarFiltros();
            RefrescarGrilla();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            List<string[]> filas = new List<string[]>();

            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                if (fila.IsNewRow) continue;

                filas.Add(new string[]
                {
                    fila.Cells["Email_Usuario"].Value?.ToString(),
                    fila.Cells["Fecha"].Value?.ToString(),
                    fila.Cells["Hora"].Value?.ToString(),
                    fila.Cells["Módulo"].Value?.ToString(),
                    fila.Cells["Evento"].Value?.ToString(),
                    fila.Cells["Criticidad"].Value?.ToString()
                });
            }

            GeneradorPDF.GenerarBitacora(filas);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
