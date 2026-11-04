using BE;
using BLL;
using System;
using System.Data;
using System.Windows.Forms;

namespace SistemaDeEnviosGUI
{
    public partial class RegistroEntregasForm : Form
    {
        private readonly RutaBLL rutaBLL = new RutaBLL();
        private readonly IntentoEntregaBLL intentoEntregaBLL = new IntentoEntregaBLL();

        private readonly int idRutaActual;

        public RegistroEntregasForm(int idRutaActual)
        {
            InitializeComponent();

            this.idRutaActual = idRutaActual;
        }

        private void RegistroEntregasForm_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrilla();
                ConfigurarCombos();
                CargarEnviosDeLaRuta();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar los envíos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void ConfigurarGrilla()
        {
            dataGridViewEnviosDeLaRuta.ReadOnly = true;
            dataGridViewEnviosDeLaRuta.AllowUserToAddRows = false;
            dataGridViewEnviosDeLaRuta.AllowUserToDeleteRows = false;
            dataGridViewEnviosDeLaRuta.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewEnviosDeLaRuta.MultiSelect = false;
            dataGridViewEnviosDeLaRuta.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewEnviosDeLaRuta.RowHeadersVisible = false;
        }

        private void ConfigurarCombos()
        {
            cmbResultado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbResultado.Items.Clear();
            cmbResultado.Items.Add("Entregado");
            cmbResultado.Items.Add("No entregado");

            cmbMotivo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMotivo.Items.Clear();
            cmbMotivo.Items.Add("Destinatario ausente");
            cmbMotivo.Items.Add("Dirección incorrecta o inexistente");
            cmbMotivo.Items.Add("Destinatario rechazó el envío");
            cmbMotivo.Items.Add("Problemas de acceso");
            cmbMotivo.Items.Add("Otro");

            cmbMotivo.Enabled = false;
        }

        private void CargarEnviosDeLaRuta()
        {
            DataTable envios = rutaBLL.ObtenerEnviosDeRuta(idRutaActual);

            dataGridViewEnviosDeLaRuta.DataSource = envios;

            if (dataGridViewEnviosDeLaRuta.Columns.Contains("id_ruta_envio"))
                dataGridViewEnviosDeLaRuta.Columns["id_ruta_envio"].Visible = false;

            if (dataGridViewEnviosDeLaRuta.Columns.Contains("id_envio"))
                dataGridViewEnviosDeLaRuta.Columns["id_envio"].Visible = false;

            if (dataGridViewEnviosDeLaRuta.Columns.Contains("orden_entrega"))
                dataGridViewEnviosDeLaRuta.Columns["orden_entrega"].HeaderText = "Orden";

            if (dataGridViewEnviosDeLaRuta.Columns.Contains("codigo_seguimiento"))
                dataGridViewEnviosDeLaRuta.Columns["codigo_seguimiento"].HeaderText = "Código de seguimiento";

            if (dataGridViewEnviosDeLaRuta.Columns.Contains("destinatario"))
                dataGridViewEnviosDeLaRuta.Columns["destinatario"].HeaderText = "Destinatario";

            if (dataGridViewEnviosDeLaRuta.Columns.Contains("telefono_destinatario"))
                dataGridViewEnviosDeLaRuta.Columns["telefono_destinatario"].HeaderText = "Teléfono";

            if (dataGridViewEnviosDeLaRuta.Columns.Contains("direccion"))
                dataGridViewEnviosDeLaRuta.Columns["direccion"].HeaderText = "Dirección";

            if (dataGridViewEnviosDeLaRuta.Columns.Contains("ciudad"))
                dataGridViewEnviosDeLaRuta.Columns["ciudad"].HeaderText = "Ciudad";

            if (dataGridViewEnviosDeLaRuta.Columns.Contains("provincia"))
                dataGridViewEnviosDeLaRuta.Columns["provincia"].HeaderText = "Provincia";

            if (dataGridViewEnviosDeLaRuta.Columns.Contains("codigo_postal"))
                dataGridViewEnviosDeLaRuta.Columns["codigo_postal"].HeaderText = "Código postal";

            if (dataGridViewEnviosDeLaRuta.Rows.Count > 0)
                dataGridViewEnviosDeLaRuta.Rows[0].Selected = true;

            ActualizarEnvioSeleccionado();
        }

        private void dataGridViewEnviosDeLaRuta_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarEnvioSeleccionado();
        }

        private void ActualizarEnvioSeleccionado()
        {
            if (dataGridViewEnviosDeLaRuta.CurrentRow == null)
            {
                lblEnvioSeleccionado.Text = "Ningún envío seleccionado";
                return;
            }

            string codigoSeguimiento = Convert.ToString(dataGridViewEnviosDeLaRuta.CurrentRow.Cells["codigo_seguimiento"].Value);
            lblEnvioSeleccionado.Text = $"Envío seleccionado: {codigoSeguimiento}";
        }

        private void cmbResultado_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool entregaFallida = cmbResultado.SelectedItem?.ToString() == "No entregado";

            cmbMotivo.Enabled = entregaFallida;

            if (!entregaFallida)
                cmbMotivo.SelectedIndex = -1;
        }

        private void btnRegistrarIntento_Click(object sender, EventArgs e)
        {
            if (dataGridViewEnviosDeLaRuta.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un envío.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbResultado.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar el resultado de la entrega.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string resultadoSeleccionado = cmbResultado.SelectedItem.ToString();
            string resultado = resultadoSeleccionado == "Entregado" ? "Entregado" : "Fallido";

            string motivo = null;

            if (resultado == "Fallido")
            {
                if (cmbMotivo.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe indicar el motivo por el cual no se pudo entregar el envío.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                motivo = cmbMotivo.SelectedItem.ToString();
            }

            string observaciones = string.IsNullOrWhiteSpace(txtObservaciones.Text) ? null : txtObservaciones.Text.Trim();

            try
            {
                int idRutaEnvio = Convert.ToInt32(dataGridViewEnviosDeLaRuta.CurrentRow.Cells["id_ruta_envio"].Value);

                IntentoEntregaBE intento = new IntentoEntregaBE(idRutaEnvio, resultado, motivo, observaciones);

                intentoEntregaBLL.RegistrarIntento(idRutaActual, intento);

                MessageBox.Show("El intento de entrega se registró correctamente.", "Registro realizado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarFormulario();
                CargarEnviosDeLaRuta();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al registrar el intento", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            cmbResultado.SelectedIndex = -1;
            cmbMotivo.SelectedIndex = -1;
            cmbMotivo.Enabled = false;
            txtObservaciones.Clear();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}