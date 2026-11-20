using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDeEnviosGUI
{
    public partial class ReporteInteligenteForm : Form
    {
        private readonly EnvioBLL envioBLL = new EnvioBLL();
        private EventoBLL eventobll = new EventoBLL();

        private Label lblNombreCliente;
        private Label lblEmailCliente;
        private Label lblEstadistica1;
        private Label lblEstadistica2;
        private Label lblEstadistica3;

        private DataTable clientesActuales;

        public ReporteInteligenteForm()
        {
            InitializeComponent();

            ConfigurarFormulario();
            ConfigurarGrilla();
            ConfigurarPanel();

            rbClientesMasFieles.CheckedChanged += rbReporte_CheckedChanged;
            rbClientesConMayorDisminucionDeEnvios.CheckedChanged += rbReporte_CheckedChanged;
            dataGridViewClientes.SelectionChanged += dataGridViewClientes_SelectionChanged;

            btnGenerarPDF.Click += btnGenerarPDF_Click;
            btnPrepararCorreo.Click += btnPrepararCorreo_Click;
            btnSalir.Click += btnSalir_Click;

            rbClientesMasFieles.Checked = true;
        }

        private void ConfigurarFormulario()
        {
            Text = "Reporte inteligente";
            StartPosition = FormStartPosition.CenterParent;
        }

        private void ConfigurarGrilla()
        {
            dataGridViewClientes.AutoGenerateColumns = true;
            dataGridViewClientes.ReadOnly = true;
            dataGridViewClientes.AllowUserToAddRows = false;
            dataGridViewClientes.AllowUserToDeleteRows = false;
            dataGridViewClientes.MultiSelect = false;
            dataGridViewClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ConfigurarPanel()
        {
            panel1.Controls.Clear();

            lblNombreCliente = new Label();
            lblNombreCliente.AutoSize = true;
            lblNombreCliente.Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold);
            lblNombreCliente.Location = new System.Drawing.Point(10, 10);
            lblNombreCliente.Text = "Seleccione un cliente";

            lblEmailCliente = new Label();
            lblEmailCliente.AutoSize = true;
            lblEmailCliente.Location = new System.Drawing.Point(10, 40);

            lblEstadistica1 = new Label();
            lblEstadistica1.AutoSize = true;
            lblEstadistica1.Location = new System.Drawing.Point(10, 70);

            lblEstadistica2 = new Label();
            lblEstadistica2.AutoSize = true;
            lblEstadistica2.Location = new System.Drawing.Point(10, 95);

            lblEstadistica3 = new Label();
            lblEstadistica3.AutoSize = true;
            lblEstadistica3.Location = new System.Drawing.Point(10, 120);

            panel1.Controls.Add(lblNombreCliente);
            panel1.Controls.Add(lblEmailCliente);
            panel1.Controls.Add(lblEstadistica1);
            panel1.Controls.Add(lblEstadistica2);
            panel1.Controls.Add(lblEstadistica3);

            textBoxMensajeMail.Multiline = true;
            textBoxMensajeMail.ScrollBars = ScrollBars.Vertical;
            textBoxMensajeMail.WordWrap = true;
        }

        private void rbReporte_CheckedChanged(object sender, EventArgs e)
        {
            if (!((RadioButton)sender).Checked)
                return;

            CargarReporte();
        }

        private void CargarReporte()
        {
            try
            {
                dataGridViewClientes.SelectionChanged -= dataGridViewClientes_SelectionChanged;

                if (rbClientesMasFieles.Checked)
                    clientesActuales = envioBLL.ObtenerClientesMasFrecuentes();
                else
                    clientesActuales = envioBLL.ObtenerClientesConMayorDisminucion();

                dataGridViewClientes.DataSource = null;
                dataGridViewClientes.DataSource = clientesActuales;

                LimpiarDetalle();

                if (dataGridViewClientes.Rows.Count > 0)
                {
                    dataGridViewClientes.CurrentCell = dataGridViewClientes.Rows[0].Cells[0];
                    dataGridViewClientes.Rows[0].Selected = true;
                }
                else
                {
                    MessageBox.Show("No se encontraron clientes para el reporte seleccionado.", "Reporte inteligente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte: " + ex.Message, "Reporte inteligente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dataGridViewClientes.SelectionChanged += dataGridViewClientes_SelectionChanged;
            }

            if (dataGridViewClientes.Rows.Count > 0)
                dataGridViewClientes_SelectionChanged(null, EventArgs.Empty);
        }

        private void dataGridViewClientes_SelectionChanged(object sender, EventArgs e)
        {
            if (rbClientesMasFieles.Checked && !dataGridViewClientes.Columns.Contains("cantidad_envios"))
                return;

            if (rbClientesConMayorDisminucionDeEnvios.Checked && !dataGridViewClientes.Columns.Contains("envios_periodo_anterior"))
                return;
            if (dataGridViewClientes.CurrentRow == null || dataGridViewClientes.CurrentRow.IsNewRow)
            {
                LimpiarDetalle();
                return;
            }

            DataGridViewRow fila = dataGridViewClientes.CurrentRow;

            string nombre = Convert.ToString(fila.Cells["nombre"].Value);
            string apellido = Convert.ToString(fila.Cells["apellido"].Value);
            string email = Convert.ToString(fila.Cells["email"].Value);

            lblNombreCliente.Text = nombre + " " + apellido;
            lblEmailCliente.Text = "Correo: " + email;

            if (rbClientesMasFieles.Checked)
            {
                int cantidad = Convert.ToInt32(fila.Cells["cantidad_envios"].Value);

                lblEstadistica1.Text = "Cantidad de envíos: " + cantidad;
                lblEstadistica2.Text = "Período analizado: últimos 90 días";
                lblEstadistica3.Text = "Tipo de reporte: clientes más frecuentes";
            }
            else
            {
                int anterior = Convert.ToInt32(fila.Cells["envios_periodo_anterior"].Value);
                int actual = Convert.ToInt32(fila.Cells["envios_periodo_actual"].Value);
                int perdidos = Convert.ToInt32(fila.Cells["envios_perdidos"].Value);
                decimal porcentaje = Convert.ToDecimal(fila.Cells["porcentaje_disminucion"].Value);

                lblEstadistica1.Text = "Envíos en período anterior: " + anterior;
                lblEstadistica2.Text = "Envíos en período actual: " + actual;
                lblEstadistica3.Text = "Disminución: " + porcentaje.ToString("0.00") + "% (" + perdidos + " envíos)";

            }

            GenerarMensaje(nombre, email);
        }

        private void GenerarMensaje(string nombre, string email)
        {
            if (rbClientesMasFieles.Checked)
            {
                textBoxMensajeMail.Text = "Hola " + nombre + ", esperamos que te encuentres bien!\n\nQueremos agradecerte por confiar en nuestro servicio y elegirnos para realizar tus envíos. Como reconocimiento a tu preferencia, queremos ofrecerte un descuento especial del 50% de descuento para tu próximo envío. Para aplicar el descuento debés presentar este correo al recepcionista al momento de solicitar tu envío.\n\n¡Gracias por elegirnos!\n\n- El equipo de Al Toque Logistics";
            }
            else
            {
                textBoxMensajeMail.Text = "Hola " + nombre + ", esperamos que te encuentres bien!\n\nHace un tiempo que no utilizás nuestros servicios con la misma frecuencia y nos gustaría volver a acompañarte. Por eso, queremos ofrecerte un descuento especial del 50% de descuento para tu próximo envío. Para aplicar el descuento debés presentar este correo al recepcionista al momento de solicitar tu envío.\n\n¡Esperamos volver a contar con vos!\n\n- El equipo de Al Toque Logistics";
            }
        }

        private void LimpiarDetalle()
        {
            if (lblNombreCliente == null)
                return;

            lblNombreCliente.Text = "Seleccione un cliente";
            lblEmailCliente.Text = "";
            lblEstadistica1.Text = "";
            lblEstadistica2.Text = "";
            lblEstadistica3.Text = "";
            textBoxMensajeMail.Clear();
        }

        private void btnPrepararCorreo_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewClientes.CurrentRow == null)
                    throw new Exception("Debe seleccionar un cliente.");

                string email = Convert.ToString(dataGridViewClientes.CurrentRow.Cells["email"].Value);

                if (string.IsNullOrWhiteSpace(email))
                    throw new Exception("El cliente seleccionado no tiene un correo electrónico válido.");

                if (string.IsNullOrWhiteSpace(textBoxMensajeMail.Text))
                    throw new Exception("El mensaje del correo está vacío.");

                string asunto = rbClientesMasFieles.Checked ? "Beneficio especial por fidelidad" : "Tenemos una propuesta especial para vos";
                string url = "mailto:" + email + "?subject=" + Uri.EscapeDataString(asunto) + "&body=" + Uri.EscapeDataString(textBoxMensajeMail.Text);

                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Preparar correo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (clientesActuales == null || clientesActuales.Rows.Count == 0)
                    throw new Exception("No hay clientes para generar el reporte.");

                string archivo = GeneradorPDF.GenerarReporteInteligente(clientesActuales, rbClientesMasFieles.Checked);

                

                MessageBox.Show("El reporte PDF se generó correctamente.", "Reporte inteligente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el PDF: " + ex.Message, "Reporte inteligente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAyuda_Click(object sender, EventArgs e)
        {
            try
            {
                new AyudaForm("Ayuda - Reporte inteligente", "ReporteInteligente.txt").ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al abrir la ayuda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
