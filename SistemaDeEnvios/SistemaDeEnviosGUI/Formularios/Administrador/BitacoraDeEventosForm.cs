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
using Servicios.GestionIdiomas;

namespace SistemaDeEnviosGUI.Formularios.Administrador
{
    public partial class BitacoraDeEventosForm : Form, IObserverIdioma
    {
        public BitacoraDeEventosForm()
        {
            InitializeComponent();
            GestorIdiomas.Instancia.Registrar(this);
        }
        private EventoBLL bll = new EventoBLL();
        private void BitacoraDeEventosForm_Load(object sender, EventArgs e)
        {
            RefrescarGrilla();
            CargarFiltros();
            ActualizarIdioma();
        }
        private void RefrescarGrilla()
        {
            dataGridView1.DataSource = bll.ObtenerTodosLosEventos();
            dataGridView1.Columns["nombre"].Visible = false;
            dataGridView1.Columns["apellido"].Visible = false;
            dataGridView1.Columns["Email_Usuario"].Width = 150;
            dataGridView1.Columns["Evento"].Width = 150;
            TraducirDatosGrilla();
            MostrarNombreYApellido();
        }
        private void TraducirDatosGrilla()
        {
            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                if (fila.IsNewRow) continue;

                if (fila.Cells["Módulo"].Value != null)
                {
                    fila.Cells["Módulo"].Value = Traducciones.Traducir(fila.Cells["Módulo"].Value.ToString());
                }

                if (fila.Cells["Evento"].Value != null)
                {
                    fila.Cells["Evento"].Value = Traducciones.Traducir(fila.Cells["Evento"].Value.ToString());
                }
            }
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

            DataTable dtEventos = bll.ObtenerEventos();
            dtEventos.Columns.Add("texto");
            foreach (DataRow fila in dtEventos.Rows)
            {
                fila["texto"] = Traducciones.Traducir(fila["evento"].ToString());
            }
            comboBoxEvento.DataSource = dtEventos;
            comboBoxEvento.DisplayMember = "texto";
            comboBoxEvento.ValueMember = "evento";
            comboBoxEvento.SelectedIndex = -1;
            comboBoxEvento.Text = "";

            DataTable dtModulos = bll.ObtenerModulos();
            dtModulos.Columns.Add("texto");
            foreach (DataRow fila in dtModulos.Rows)
            {
                fila["texto"] = Traducciones.Traducir(fila["modulo"].ToString());
            }
            comboBoxModulo.DataSource = dtModulos;
            comboBoxModulo.DisplayMember = "texto";
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

            string evento = comboBoxEvento.SelectedIndex != -1 ? comboBoxEvento.SelectedValue.ToString() : null;

            string modulo = comboBoxModulo.SelectedIndex != -1 ? comboBoxModulo.SelectedValue.ToString() : null;

            int? criticidad = comboBoxCriticidad.SelectedIndex != -1 ? Convert.ToInt32(comboBoxCriticidad.Text) : null;

            DateTime? desde = dateTimePickerDesde.Checked ? dateTimePickerDesde.Value.Date : null;
            DateTime? hasta = dateTimePickerHasta.Checked ? dateTimePickerHasta.Value.Date.AddDays(1).AddTicks(-1) : null;

            dataGridView1.DataSource = bll.FiltrarEventos(email, desde, hasta, modulo, evento, criticidad);
            TraducirDatosGrilla();
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
                    fila.Cells[0].Value?.ToString(),
                    ((DateTime)fila.Cells[1].Value).ToString("dd/MM/yyyy"),
                    fila.Cells[2].Value?.ToString(),
                    fila.Cells[3].Value?.ToString(),
                    fila.Cells[4].Value?.ToString(),
                    fila.Cells[5].Value?.ToString()
                });
            }

            GeneradorPDF.GenerarBitacora(filas);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ActualizarIdioma()
        {
            this.Text = Traducciones.Traducir("Bitacora");
            lblNombre.Text = Traducciones.Traducir("Nombre");
            lblApellido.Text = Traducciones.Traducir("Apellido");
            lblEmail.Text = Traducciones.Traducir("Email");
            lblDesde.Text = Traducciones.Traducir("Desde");
            lblHasta.Text = Traducciones.Traducir("Hasta");
            lblModulo.Text = Traducciones.Traducir("Modulo");
            lblEvento.Text = Traducciones.Traducir("Evento");
            lblCriticidad.Text = Traducciones.Traducir("Criticidad");
            btnAplicar.Text = Traducciones.Traducir("Aplicar");
            btnLimpiar.Text = Traducciones.Traducir("Limpiar");
            btnImprimir.Text = Traducciones.Traducir("Imprimir");
            btnSalir.Text = Traducciones.Traducir("Salir");

            dataGridView1.Columns["Email_Usuario"].HeaderText = Traducciones.Traducir("Email");
            dataGridView1.Columns["Fecha"].HeaderText = Traducciones.Traducir("Fecha");
            dataGridView1.Columns["Hora"].HeaderText = Traducciones.Traducir("Hora");
            dataGridView1.Columns["Módulo"].HeaderText = Traducciones.Traducir("Modulo");
            dataGridView1.Columns["Evento"].HeaderText = Traducciones.Traducir("Evento");
            dataGridView1.Columns["Criticidad"].HeaderText = Traducciones.Traducir("Criticidad");
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            GestorIdiomas.Instancia.Desregistrar(this);
            base.OnFormClosed(e);
        }
    }
}
