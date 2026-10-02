using BLL;
using Servicios.GestionIdiomas;
using System;
using System.Data;
using System.Windows.Forms;

namespace SistemaDeEnviosGUI
{
    public partial class GestionFacturasForm : Form, IObserverIdioma
    {
        private EnvioBLL enviobll = new EnvioBLL();
        private FacturaBLL facturabll = new FacturaBLL();

        private int idEnvioSeleccionado = 0;
        private bool facturaSeleccionada = false;
        private string estadoSeleccionado = "";

        public GestionFacturasForm()
        {
            InitializeComponent();
            GestorIdiomas.Instancia.Registrar(this);
        }

        private void GestionFacturasForm_Load(object sender, EventArgs e)
        {
            rbTodos.Checked = true;
            ConfigurarGrilla();
            CargarEnvios();
            ActualizarIdioma();
        }

        private void ConfigurarGrilla()
        {
            dataGridViewEnvios.AutoGenerateColumns = true;
            dataGridViewEnvios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewEnvios.MultiSelect = false;
            dataGridViewEnvios.ReadOnly = true;
            dataGridViewEnvios.AllowUserToAddRows = false;
            dataGridViewEnvios.AllowUserToDeleteRows = false;
            dataGridViewEnvios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewEnvios.SelectionChanged += dataGridViewEnvios_SelectionChanged;
        }

        private void CargarEnvios()
        {
            try
            {
                DataTable tabla = enviobll.ObtenerEnviosParaFacturacion();

                DataView vista = tabla.DefaultView;
                string dni = txtDNIRemitente.Text.Trim();
                string codigo = txtCodigoSeguimiento.Text.Trim();

                string filtro = "";

                if (rbSinFactura.Checked)
                    filtro = "facturado = false";

                if (rbConFactura.Checked)
                    filtro = "facturado = true";

                if (dni != "")
                {
                    string filtroDni = $"CONVERT(id_remitente, 'System.String') LIKE '%{dni}%'";

                    if (filtro != "")
                        filtro += " AND ";

                    filtro += filtroDni;
                }

                if (codigo != "")
                {
                    string codigoEscapado = codigo.Replace("'", "''");
                    string filtroCodigo = $"codigo_seguimiento LIKE '%{codigoEscapado}%'";

                    if (filtro != "")
                        filtro += " AND ";

                    filtro += filtroCodigo;
                }

                vista.RowFilter = filtro;
                dataGridViewEnvios.DataSource = vista;

                FormatearGrilla();

                if (dataGridViewEnvios.Rows.Count > 0)
                {
                    dataGridViewEnvios.ClearSelection();
                    dataGridViewEnvios.Rows[0].Selected = true;
                    dataGridViewEnvios.CurrentCell = dataGridViewEnvios.Rows[0].Cells[0];
                }
                else
                {
                    LimpiarSeleccion();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Gestión de facturas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FormatearGrilla()
        {
            if (dataGridViewEnvios.Columns.Count == 0)
                return;

            if (dataGridViewEnvios.Columns["id_envio"] != null)
                dataGridViewEnvios.Columns["id_envio"].HeaderText = "Envío";

            if (dataGridViewEnvios.Columns["codigo_seguimiento"] != null)
                dataGridViewEnvios.Columns["codigo_seguimiento"].HeaderText = "Código de seguimiento";

            if (dataGridViewEnvios.Columns["origen"] != null)
                dataGridViewEnvios.Columns["origen"].HeaderText = "Origen";

            if (dataGridViewEnvios.Columns["fecha_registro"] != null)
                dataGridViewEnvios.Columns["fecha_registro"].HeaderText = "Fecha de registro";

            if (dataGridViewEnvios.Columns["estado"] != null)
                dataGridViewEnvios.Columns["estado"].HeaderText = "Estado";

            if (dataGridViewEnvios.Columns["id_remitente"] != null)
                dataGridViewEnvios.Columns["id_remitente"].HeaderText = "DNI remitente";

            if (dataGridViewEnvios.Columns["remitente"] != null)
                dataGridViewEnvios.Columns["remitente"].HeaderText = "Remitente";

            if (dataGridViewEnvios.Columns["facturado"] != null)
                dataGridViewEnvios.Columns["facturado"].HeaderText = "Facturado";
        }

        private void dataGridViewEnvios_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewEnvios.SelectedRows.Count == 0)
            {
                LimpiarSeleccion();
                return;
            }

            DataGridViewRow fila = dataGridViewEnvios.SelectedRows[0];

            idEnvioSeleccionado = Convert.ToInt32(fila.Cells["id_envio"].Value);
            facturaSeleccionada = Convert.ToBoolean(fila.Cells["facturado"].Value);
            estadoSeleccionado = fila.Cells["estado"].Value.ToString();

            txtEnvioSeleccionado.Text = fila.Cells["codigo_seguimiento"].Value.ToString();

            ActualizarBotones();
        }

        private void ActualizarBotones()
        {
            btnGenerarFactura.Enabled = !facturaSeleccionada && estadoSeleccionado == "Pagado";
            btnImprimirFactura.Enabled = facturaSeleccionada;
        }

        private void LimpiarSeleccion()
        {
            idEnvioSeleccionado = 0;
            facturaSeleccionada = false;
            estadoSeleccionado = "";

            txtEnvioSeleccionado.Clear();

            btnGenerarFactura.Enabled = false;
            btnImprimirFactura.Enabled = false;
        }

        private void txtDNIRemitente_TextChanged(object sender, EventArgs e)
        {
            CargarEnvios();
        }

        private void txtCodigoSeguimiento_TextChanged(object sender, EventArgs e)
        {
            CargarEnvios();
        }

        private void btnGenerarFactura_Click(object sender, EventArgs e)
        {
            try
            {
                if (idEnvioSeleccionado <= 0)
                    throw new Exception("Debe seleccionar un envío.");

                if (facturaSeleccionada)
                    throw new Exception("El envío seleccionado ya posee una factura.");

                if (estadoSeleccionado != "Pagado")
                    throw new Exception("Solo se pueden facturar envíos que se encuentren pagados.");

                facturabll.GenerarFactura(idEnvioSeleccionado);
                facturabll.GenerarPdf(idEnvioSeleccionado);

                MessageBox.Show("La factura fue generada correctamente.", "Factura generada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarEnvios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Generar factura", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnImprimirFactura_Click(object sender, EventArgs e)
        {
            try
            {
                if (idEnvioSeleccionado <= 0)
                    throw new Exception("Debe seleccionar un envío.");

                if (!facturaSeleccionada)
                    throw new Exception("El envío seleccionado no posee una factura.");

                facturabll.GenerarPdf(idEnvioSeleccionado);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Imprimir factura", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void rbTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTodos.Checked)
                CargarEnvios();
        }

        private void rbSinFactura_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSinFactura.Checked)
                CargarEnvios();
        }

        private void rbConFactura_CheckedChanged(object sender, EventArgs e)
        {
            if (rbConFactura.Checked)
                CargarEnvios();
        }

        public void ActualizarIdioma()
        {
            this.Text = Traducciones.Traducir("GestionFacturas");

            lblMostrar.Text = Traducciones.Traducir("Mostrar");

            lblCodigoSeguimiento.Text = Traducciones.Traducir("CodigoSeguimiento");
            lblDNIRemitente.Text = Traducciones.Traducir("DNIRemitente");
            lblEnvioSeleccionado.Text = Traducciones.Traducir("EnvioSeleccionado");

            rbTodos.Text = Traducciones.Traducir("Todos");
            rbSinFactura.Text = Traducciones.Traducir("SinFactura");
            rbConFactura.Text = Traducciones.Traducir("ConFactura");

            btnGenerarFactura.Text = Traducciones.Traducir("GenerarFactura");
            btnImprimirFactura.Text = Traducciones.Traducir("ImprimirFactura");
            btnSalir.Text = Traducciones.Traducir("Salir");

            if (dataGridViewEnvios.DataSource != null)
            {
                if (dataGridViewEnvios.Columns["id_envio"] != null)
                    dataGridViewEnvios.Columns["id_envio"].HeaderText = Traducciones.Traducir("Envio");

                if (dataGridViewEnvios.Columns["codigo_seguimiento"] != null)
                    dataGridViewEnvios.Columns["codigo_seguimiento"].HeaderText = Traducciones.Traducir("CodigoSeguimiento");

                if (dataGridViewEnvios.Columns["origen"] != null)
                    dataGridViewEnvios.Columns["origen"].HeaderText = Traducciones.Traducir("Origen");

                if (dataGridViewEnvios.Columns["fecha_registro"] != null)
                    dataGridViewEnvios.Columns["fecha_registro"].HeaderText = Traducciones.Traducir("FechaRegistro");

                if (dataGridViewEnvios.Columns["estado"] != null)
                    dataGridViewEnvios.Columns["estado"].HeaderText = Traducciones.Traducir("Estado");

                if (dataGridViewEnvios.Columns["id_remitente"] != null)
                    dataGridViewEnvios.Columns["id_remitente"].HeaderText = Traducciones.Traducir("DNIRemitente");

                if (dataGridViewEnvios.Columns["remitente"] != null)
                    dataGridViewEnvios.Columns["remitente"].HeaderText = Traducciones.Traducir("Remitente");

                if (dataGridViewEnvios.Columns["facturado"] != null)
                    dataGridViewEnvios.Columns["facturado"].HeaderText = Traducciones.Traducir("Facturado");
            }
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            GestorIdiomas.Instancia.Desregistrar(this);
            base.OnFormClosed(e);
        }
    }
}