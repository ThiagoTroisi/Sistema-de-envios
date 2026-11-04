using BLL;
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
    public partial class ResolucionEnviosForm : Form
    {
        private IntentoEntregaBLL intentoEntregaBLL = new IntentoEntregaBLL();

        public ResolucionEnviosForm()
        {
            InitializeComponent();
            ConfigurarGrillas();
            CargarEnviosPendientes();
        }

        private void ConfigurarGrillas()
        {
            dataGridViewEnviosPendientes.ReadOnly = true;
            dataGridViewEnviosPendientes.AllowUserToAddRows = false;
            dataGridViewEnviosPendientes.AllowUserToDeleteRows = false;
            dataGridViewEnviosPendientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewEnviosPendientes.MultiSelect = false;
            dataGridViewEnviosPendientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewEnviosPendientes.RowHeadersVisible = false;

            dataGridViewHistorialIntentos.ReadOnly = true;
            dataGridViewHistorialIntentos.AllowUserToAddRows = false;
            dataGridViewHistorialIntentos.AllowUserToDeleteRows = false;
            dataGridViewHistorialIntentos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewHistorialIntentos.MultiSelect = false;
            dataGridViewHistorialIntentos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewHistorialIntentos.RowHeadersVisible = false;

            btnAutorizarNuevoIntento.Enabled = false;
            btnCancelarEnvio.Enabled = false;
        }

        private void CargarEnviosPendientes()
        {
            try
            {
                dataGridViewEnviosPendientes.DataSource = intentoEntregaBLL.ObtenerEnviosPendientesResolucion();

                if (dataGridViewEnviosPendientes.Columns.Contains("id_envio"))
                    dataGridViewEnviosPendientes.Columns["id_envio"].Visible = false;

                if (dataGridViewEnviosPendientes.Columns.Contains("codigo_seguimiento"))
                    dataGridViewEnviosPendientes.Columns["codigo_seguimiento"].HeaderText = "Código de seguimiento";

                if (dataGridViewEnviosPendientes.Columns.Contains("fecha_registro"))
                    dataGridViewEnviosPendientes.Columns["fecha_registro"].HeaderText = "Fecha de registro";

                if (dataGridViewEnviosPendientes.Columns.Contains("intentos_fallidos"))
                    dataGridViewEnviosPendientes.Columns["intentos_fallidos"].HeaderText = "Intentos fallidos";

                if (dataGridViewEnviosPendientes.Columns.Contains("destinatario"))
                    dataGridViewEnviosPendientes.Columns["destinatario"].HeaderText = "Destinatario";

                if (dataGridViewEnviosPendientes.Columns.Contains("telefono_destinatario"))
                    dataGridViewEnviosPendientes.Columns["telefono_destinatario"].HeaderText = "Teléfono";

                if (dataGridViewEnviosPendientes.Columns.Contains("direccion"))
                    dataGridViewEnviosPendientes.Columns["direccion"].HeaderText = "Dirección";

                if (dataGridViewEnviosPendientes.Columns.Contains("ciudad"))
                    dataGridViewEnviosPendientes.Columns["ciudad"].HeaderText = "Ciudad";

                if (dataGridViewEnviosPendientes.Columns.Contains("provincia"))
                    dataGridViewEnviosPendientes.Columns["provincia"].HeaderText = "Provincia";

                if (dataGridViewEnviosPendientes.Rows.Count == 0)
                {
                    LimpiarHistorial();
                    btnAutorizarNuevoIntento.Enabled = false;
                    btnCancelarEnvio.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar los envíos pendientes de resolución.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewEnviosPendientes_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewEnviosPendientes.SelectedRows.Count == 0)
            {
                LimpiarHistorial();
                btnAutorizarNuevoIntento.Enabled = false;
                btnCancelarEnvio.Enabled = false;
                return;
            }

            btnAutorizarNuevoIntento.Enabled = true;
            btnCancelarEnvio.Enabled = true;

            int idEnvio = Convert.ToInt32(dataGridViewEnviosPendientes.SelectedRows[0].Cells["id_envio"].Value);

            CargarHistorialIntentos(idEnvio);
        }

        private void CargarHistorialIntentos(int idEnvio)
        {
            try
            {
                dataGridViewHistorialIntentos.DataSource = intentoEntregaBLL.ObtenerIntentosPorEnvio(idEnvio);

                if (dataGridViewHistorialIntentos.Columns.Contains("id_intento"))
                    dataGridViewHistorialIntentos.Columns["id_intento"].Visible = false;

                if (dataGridViewHistorialIntentos.Columns.Contains("fecha_intento"))
                    dataGridViewHistorialIntentos.Columns["fecha_intento"].HeaderText = "Fecha y hora";

                if (dataGridViewHistorialIntentos.Columns.Contains("resultado"))
                    dataGridViewHistorialIntentos.Columns["resultado"].HeaderText = "Resultado";

                if (dataGridViewHistorialIntentos.Columns.Contains("tipo_inconveniente"))
                    dataGridViewHistorialIntentos.Columns["tipo_inconveniente"].HeaderText = "Inconveniente";

                if (dataGridViewHistorialIntentos.Columns.Contains("observaciones"))
                    dataGridViewHistorialIntentos.Columns["observaciones"].HeaderText = "Observaciones";
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar el historial de intentos.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarHistorial()
        {
            dataGridViewHistorialIntentos.DataSource = null;
        }

        private int ObtenerIdEnvioSeleccionado()
        {
            if (dataGridViewEnviosPendientes.SelectedRows.Count == 0)
                throw new Exception("Debe seleccionar un envío.");

            return Convert.ToInt32(dataGridViewEnviosPendientes.SelectedRows[0].Cells["id_envio"].Value);
        }

        private void btnAutorizarNuevoIntento_Click(object sender, EventArgs e)
        {
            try
            {
                int idEnvio = ObtenerIdEnvioSeleccionado();

                DialogResult resultado = MessageBox.Show("¿Está seguro de que desea autorizar un nuevo intento de entrega para este envío?", "Confirmar autorización", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado != DialogResult.Yes)
                    return;

                intentoEntregaBLL.AutorizarNuevoIntento(idEnvio);

                MessageBox.Show("Se autorizó un nuevo intento de entrega correctamente.", "Operación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarEnviosPendientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo autorizar el nuevo intento.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelarEnvio_Click(object sender, EventArgs e)
        {
            try
            {
                int idEnvio = ObtenerIdEnvioSeleccionado();

                DialogResult resultado = MessageBox.Show("¿Está seguro de que desea cancelar este envío? Esta operación no se puede deshacer.", "Confirmar cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (resultado != DialogResult.Yes)
                    return;

                intentoEntregaBLL.DevolverEnvioPorIntentos(idEnvio);

                MessageBox.Show("El envío fue cancelado correctamente.", "Operación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarEnviosPendientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cancelar el envío.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
