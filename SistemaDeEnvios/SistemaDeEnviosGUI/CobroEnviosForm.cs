using BE;
using BLL;
using Servicios.GestionIdiomas;
using System;
using System.Data;
using System.Windows.Forms;

namespace SistemaDeEnviosGUI
{
    public partial class CobroEnviosForm : Form, IObserverIdioma
    {
        private EnvioBLL enviobll = new EnvioBLL();
        private int idEnvioSeleccionado = 0;
        private decimal importeSeleccionado = 0;
        private bool actualizandoGrilla = false;

        public CobroEnviosForm()
        {
            InitializeComponent();
            GestorIdiomas.Instancia.Registrar(this);
        }

        private void CobroEnviosForm_Load(object sender, EventArgs e)
        {
            ConfigurarGrilla();
            RefrescarEnvios();
            ActualizarIdioma();
        }

        private void ConfigurarGrilla()
        {
            dataGridViewEnvios.ReadOnly = true;
            dataGridViewEnvios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewEnvios.MultiSelect = false;
            dataGridViewEnvios.AutoGenerateColumns = true;
        }

        private void RefrescarEnvios()
        {
            actualizandoGrilla = true;

            try
            {
                string dniRemitente = txtDNIRemitente.Text.Trim();
                string codigo = txtCodigoSeguimiento.Text.Trim();

                DataTable tabla = enviobll.ObtenerEnvios("Registrado", codigo, dniRemitente, "", null, null);

                dataGridViewEnvios.DataSource = tabla;

                if (dataGridViewEnvios.Columns.Contains("id_envio"))
                    dataGridViewEnvios.Columns["id_envio"].Visible = false;

                if (dataGridViewEnvios.Columns.Contains("codigo_seguimiento"))
                    dataGridViewEnvios.Columns["codigo_seguimiento"].HeaderText = "Código";

                if (dataGridViewEnvios.Columns.Contains("fecha_registro"))
                    dataGridViewEnvios.Columns["fecha_registro"].HeaderText = "Fecha";

                if (dataGridViewEnvios.Columns.Contains("remitente"))
                    dataGridViewEnvios.Columns["remitente"].HeaderText = "Remitente";

                if (dataGridViewEnvios.Columns.Contains("destinatario"))
                    dataGridViewEnvios.Columns["destinatario"].HeaderText = "Destinatario";

                if (dataGridViewEnvios.Columns.Contains("destino"))
                {
                    dataGridViewEnvios.Columns["destino"].HeaderText = "Destino";
                    dataGridViewEnvios.Columns["destino"].Width = 200;
                }

                if (dataGridViewEnvios.Columns.Contains("estado"))
                    dataGridViewEnvios.Columns["estado"].HeaderText = "Estado";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                actualizandoGrilla = false;
                LimpiarSeleccion();
            }

            CargarSeleccion();
        }

        private void CargarSeleccion()
        {
            if (dataGridViewEnvios.CurrentRow == null || dataGridViewEnvios.CurrentRow.IsNewRow)
            {
                LimpiarSeleccion();
                return;
            }

            if (dataGridViewEnvios.CurrentRow.Cells["id_envio"].Value == null || dataGridViewEnvios.CurrentRow.Cells["id_envio"].Value == DBNull.Value)
            {
                LimpiarSeleccion();
                return;
            }

            try
            {
                idEnvioSeleccionado = Convert.ToInt32(dataGridViewEnvios.CurrentRow.Cells["id_envio"].Value);

                EnvioBE envio = enviobll.ConsultaPorId(idEnvioSeleccionado);

                if (envio == null)
                {
                    LimpiarSeleccion();
                    return;
                }

                importeSeleccionado = enviobll.ObtenerImporteEnvio(idEnvioSeleccionado);

                txtEnvioSeleccionado.Text = envio.CodigoSeguimiento;
                txtImporteAAbonar.Text = importeSeleccionado.ToString("C2");
                btnCobrar.Enabled = true;
            }
            catch (Exception ex)
            {
                LimpiarSeleccion();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewEnvios_SelectionChanged(object sender, EventArgs e)
        {
            if (actualizandoGrilla) return;

            CargarSeleccion();
        }

        private void txtDNIRemitente_TextChanged(object sender, EventArgs e)
        {
            RefrescarEnvios();
        }

        private void txtCodigoSeguimiento_TextChanged(object sender, EventArgs e)
        {
            RefrescarEnvios();
        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            if (idEnvioSeleccionado <= 0)
            {
                MessageBox.Show("Debe seleccionar un envío.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                EnvioBE envio = enviobll.ConsultaPorId(idEnvioSeleccionado);
                DatosTarjetaForm formulario = new DatosTarjetaForm(idEnvioSeleccionado, txtEnvioSeleccionado.Text, importeSeleccionado, envio.IdRemitente);

                if (formulario.ShowDialog() == DialogResult.OK)
                    RefrescarEnvios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LimpiarSeleccion()
        {
            idEnvioSeleccionado = 0;
            importeSeleccionado = 0;
            txtEnvioSeleccionado.Clear();
            txtImporteAAbonar.Clear();
            btnCobrar.Enabled = false;
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            GestorIdiomas.Instancia.Desregistrar(this);
            base.OnFormClosed(e);
        }

        public void ActualizarIdioma()
        {
            this.Text = Traducciones.Traducir("GestionCobros");

            lblDNIRemitente.Text = Traducciones.Traducir("DNIRemitente");
            lblCodigoSeguimiento.Text = Traducciones.Traducir("CodigoSeguimiento");
            lblEnvioSeleccionado.Text = Traducciones.Traducir("EnvioSeleccionado");
            lblImporteAAbonar.Text = Traducciones.Traducir("ImporteAAbonar");

            btnCobrar.Text = Traducciones.Traducir("Cobrar");
            btnSalir.Text = Traducciones.Traducir("Salir");

            if (dataGridViewEnvios.DataSource != null)
            {
                if (dataGridViewEnvios.Columns.Contains("codigo_seguimiento"))
                    dataGridViewEnvios.Columns["codigo_seguimiento"].HeaderText = Traducciones.Traducir("Codigo");

                if (dataGridViewEnvios.Columns.Contains("fecha_registro"))
                    dataGridViewEnvios.Columns["fecha_registro"].HeaderText = Traducciones.Traducir("Fecha");

                if (dataGridViewEnvios.Columns.Contains("remitente"))
                    dataGridViewEnvios.Columns["remitente"].HeaderText = Traducciones.Traducir("Remitente");

                if (dataGridViewEnvios.Columns.Contains("destinatario"))
                    dataGridViewEnvios.Columns["destinatario"].HeaderText = Traducciones.Traducir("Destinatario");

                if (dataGridViewEnvios.Columns.Contains("destino"))
                    dataGridViewEnvios.Columns["destino"].HeaderText = Traducciones.Traducir("Destino");

                if (dataGridViewEnvios.Columns.Contains("estado"))
                    dataGridViewEnvios.Columns["estado"].HeaderText = Traducciones.Traducir("Estado");
            }
        }
    }
}