using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SistemaDeEnviosGUI
{
    public partial class CreacionRutasForm : Form
    {
        private RutaBLL rutaBLL = new RutaBLL();
        private IntentoEntregaBLL intentoEntregaBLL = new IntentoEntregaBLL();
        public CreacionRutasForm()
        {
            InitializeComponent();
            ConfigurarGrillas();
            CargarEnviosPendientes();
            CargarRutas();
            ActualizarIndicadorEnviosResolucion();
        }

        private void ConfigurarGrillas()
        {
            dataGridViewEnviosPendientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewEnviosPendientes.MultiSelect = true;
            dataGridViewEnviosPendientes.ReadOnly = true;
            dataGridViewEnviosPendientes.AllowUserToAddRows = false;
            dataGridViewEnviosPendientes.AllowUserToDeleteRows = false;
            dataGridViewEnviosPendientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridViewRutas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewRutas.MultiSelect = false;
            dataGridViewRutas.ReadOnly = true;
            dataGridViewRutas.AllowUserToAddRows = false;
            dataGridViewRutas.AllowUserToDeleteRows = false;
            dataGridViewRutas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridViewEnviosDeRuta.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewEnviosDeRuta.MultiSelect = true;
            dataGridViewEnviosDeRuta.ReadOnly = true;
            dataGridViewEnviosDeRuta.AllowUserToAddRows = false;
            dataGridViewEnviosDeRuta.AllowUserToDeleteRows = false;
            dataGridViewEnviosDeRuta.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void CargarEnviosPendientes()
        {
            try
            {
                DataTable tablaEnvios = rutaBLL.ObtenerEnviosPendientesDistribucion();
                dataGridViewEnviosPendientes.DataSource = tablaEnvios;

                if (dataGridViewEnviosPendientes.Columns.Contains("id_envio"))
                {
                    dataGridViewEnviosPendientes.Columns["id_envio"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar los envíos pendientes.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarRutas()
        {
            try
            {
                int? idRutaSeleccionada = null;

                if (dataGridViewRutas.CurrentRow != null)
                {
                    idRutaSeleccionada = Convert.ToInt32(dataGridViewRutas.CurrentRow.Cells["id_ruta"].Value);
                }

                DataTable tablaRutas = rutaBLL.ObtenerRutasSinIniciar();

                dataGridViewRutas.DataSource = tablaRutas;

                if (dataGridViewRutas.Columns.Contains("id_ruta"))
                {
                    dataGridViewRutas.Columns["id_ruta"].Visible = false;
                }

                if (dataGridViewRutas.Columns.Contains("dni_repartidor"))
                {
                    dataGridViewRutas.Columns["dni_repartidor"].Visible = false;
                }

                if (idRutaSeleccionada.HasValue)
                {
                    foreach (DataGridViewRow fila in dataGridViewRutas.Rows)
                    {
                        int idRuta = Convert.ToInt32(fila.Cells["id_ruta"].Value);

                        if (idRuta == idRutaSeleccionada.Value)
                        {
                            fila.Selected = true;
                            dataGridViewRutas.CurrentCell = fila.Cells["estado"];
                            break;
                        }
                    }
                }

                CargarEnviosDeRutaSeleccionada();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar las rutas.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarEnviosDeRutaSeleccionada()
        {
            if (dataGridViewRutas.CurrentRow == null)
            {
                dataGridViewEnviosDeRuta.DataSource = null;
                return;
            }

            try
            {
                int idRuta = Convert.ToInt32(dataGridViewRutas.CurrentRow.Cells["id_ruta"].Value);
                DataTable tablaEnvios = rutaBLL.ObtenerEnviosDeRuta(idRuta);
                dataGridViewEnviosDeRuta.DataSource = tablaEnvios;

                if (dataGridViewEnviosDeRuta.Columns.Contains("id_envio"))
                {
                    dataGridViewEnviosDeRuta.Columns["id_envio"].Visible = false;
                }

                if (dataGridViewEnviosDeRuta.Columns.Contains("id_ruta_envio"))
                {
                    dataGridViewEnviosDeRuta.Columns["id_ruta_envio"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar los envíos de la ruta seleccionada.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<int> ObtenerIdsEnviosSeleccionados()
        {
            List<int> idsEnvios = new List<int>();

            foreach (DataGridViewRow fila in dataGridViewEnviosPendientes.SelectedRows)
            {
                int idEnvio = Convert.ToInt32(fila.Cells["id_envio"].Value);
                idsEnvios.Add(idEnvio);
            }

            return idsEnvios;
        }

        private List<int> ObtenerIdsEnviosDeRutaSeleccionados()
        {
            List<int> idsEnvios = new List<int>();

            foreach (DataGridViewRow fila in dataGridViewEnviosDeRuta.SelectedRows)
            {
                int idEnvio = Convert.ToInt32(fila.Cells["id_envio"].Value);
                idsEnvios.Add(idEnvio);
            }

            return idsEnvios;
        }

        private int ObtenerIdRutaSeleccionada()
        {
            if (dataGridViewRutas.CurrentRow == null)
            {
                throw new Exception("Debe seleccionar una ruta.");
            }

            int idRuta = Convert.ToInt32(dataGridViewRutas.CurrentRow.Cells["id_ruta"].Value);
            return idRuta;
        }

        private void btnCrearRuta_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> idsEnvios = ObtenerIdsEnviosSeleccionados();

                if (idsEnvios.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar al menos un envío para crear la ruta.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                RutaBE ruta = new RutaBE();
                ruta.Descripcion = txtDescripcionRuta.Text.Trim();

                if (string.IsNullOrWhiteSpace(ruta.Descripcion))
                {
                    ruta.Descripcion = null;
                }

                int idRuta = rutaBLL.CrearRuta(ruta, idsEnvios);

                MessageBox.Show("La ruta se creó correctamente.\n\nNúmero de ruta: " + idRuta, "Ruta creada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtDescripcionRuta.Clear();
                CargarEnviosPendientes();
                CargarRutas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo crear la ruta.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregarEnvios_Click(object sender, EventArgs e)
        {
            try
            {
                int idRuta = ObtenerIdRutaSeleccionada();
                List<int> idsEnvios = ObtenerIdsEnviosSeleccionados();

                if (idsEnvios.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar al menos un envío para agregar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                rutaBLL.AgregarEnviosARuta(idRuta, idsEnvios);

                MessageBox.Show("Los envíos se agregaron correctamente a la ruta.", "Envíos agregados", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarEnviosPendientes();
                CargarEnviosDeRutaSeleccionada();
                CargarRutas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron agregar los envíos.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQuitarEnvios_Click(object sender, EventArgs e)
        {
            try
            {
                int idRuta = ObtenerIdRutaSeleccionada();
                List<int> idsEnvios = ObtenerIdsEnviosDeRutaSeleccionados();

                if (idsEnvios.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar al menos un envío de la ruta.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult respuesta = MessageBox.Show("¿Está seguro de que desea quitar los envíos seleccionados de la ruta?", "Confirmar operación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta != DialogResult.Yes)
                {
                    return;
                }

                rutaBLL.QuitarEnviosDeRuta(idRuta, idsEnvios);

                MessageBox.Show("Los envíos seleccionados se quitaron de la ruta.", "Envíos quitados", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarEnviosPendientes();
                CargarRutas();
                CargarEnviosDeRutaSeleccionada();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron quitar los envíos.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelarRuta_Click(object sender, EventArgs e)
        {
            try
            {
                int idRuta = ObtenerIdRutaSeleccionada();

                DialogResult respuesta = MessageBox.Show("¿Está seguro de que desea cancelar la ruta seleccionada?", "Confirmar cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta != DialogResult.Yes)
                {
                    return;
                }

                rutaBLL.CancelarRuta(idRuta);

                MessageBox.Show("La ruta se canceló correctamente.", "Ruta cancelada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarEnviosPendientes();
                CargarRutas();
                CargarEnviosDeRutaSeleccionada();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cancelar la ruta.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSubirOrden_Click(object sender, EventArgs e)
        {
            try
            {
                int idRuta = ObtenerIdRutaSeleccionada();

                if (dataGridViewEnviosDeRuta.CurrentRow == null)
                {
                    MessageBox.Show("Debe seleccionar un envío de la ruta.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idRutaEnvio = Convert.ToInt32(dataGridViewEnviosDeRuta.CurrentRow.Cells["id_ruta_envio"].Value);

                rutaBLL.SubirEnvio(idRuta, idRutaEnvio);

                CargarEnviosDeRutaSeleccionada();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo modificar el orden", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBajarOrden_Click(object sender, EventArgs e)
        {
            try
            {
                int idRuta = ObtenerIdRutaSeleccionada();

                if (dataGridViewEnviosDeRuta.CurrentRow == null)
                {
                    MessageBox.Show("Debe seleccionar un envío de la ruta.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idRutaEnvio = Convert.ToInt32(dataGridViewEnviosDeRuta.CurrentRow.Cells["id_ruta_envio"].Value);

                rutaBLL.BajarEnvio(idRuta, idRutaEnvio);

                CargarEnviosDeRutaSeleccionada();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo modificar el orden", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridViewEnviosPendientes_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void dataGridViewRutas_SelectionChanged_1(object sender, EventArgs e)
        {
            CargarEnviosDeRutaSeleccionada();
        }
        private void ActualizarIndicadorEnviosResolucion()
        {
            try
            {
                int cantidad = intentoEntregaBLL.ObtenerCantidadEnviosPendientesResolucion();

                lblValorEnviosResolucion.Text = cantidad.ToString();
                btnResolverEnvios.Enabled = cantidad > 0;
                if (cantidad > 0)
                {
                    lblEnviosResolucion.BackColor = Color.Red;
                    lblValorEnviosResolucion.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblValorEnviosResolucion.Text = "?";
                btnResolverEnvios.Enabled = false;

                MessageBox.Show("No se pudo consultar la cantidad de envíos pendientes de resolución.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnResolverEnvios_Click(object sender, EventArgs e)
        {
            using (ResolucionEnviosForm form = new ResolucionEnviosForm())
            {
                form.ShowDialog();
            }

            ActualizarIndicadorEnviosResolucion();
            CargarEnviosPendientes();
        }
    }
}
