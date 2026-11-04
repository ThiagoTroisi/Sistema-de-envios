using BE.Entidades;
using BLL;
using Servicios.Sesión;
using System;
using System.Data;
using System.Windows.Forms;

namespace SistemaDeEnviosGUI
{
    public partial class RepartidorForm : Form
    {
        private readonly AsignacionRutaBLL asignacionRutaBLL = new AsignacionRutaBLL();
        private readonly RutaBLL rutaBLL = new RutaBLL();
        private readonly IntentoEntregaBLL intentoEntregaBLL = new IntentoEntregaBLL();

        private int idAsignacionActual;
        private int idRutaActual;

        public RepartidorForm()
        {
            InitializeComponent();
        }

        private void RepartidorForm_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrillas();
                CargarDatosRepartidor();
                CargarRutaActual();
                CargarResumenDiario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGrillas()
        {
            dataGridViewEnviosDeLaRuta.ReadOnly = true;
            dataGridViewEnviosDeLaRuta.AllowUserToAddRows = false;
            dataGridViewEnviosDeLaRuta.AllowUserToDeleteRows = false;
            dataGridViewEnviosDeLaRuta.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewEnviosDeLaRuta.MultiSelect = false;
            dataGridViewEnviosDeLaRuta.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridViewHistorial.ReadOnly = true;
            dataGridViewHistorial.AllowUserToAddRows = false;
            dataGridViewHistorial.AllowUserToDeleteRows = false;
            dataGridViewHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewHistorial.MultiSelect = false;
            dataGridViewHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void CargarDatosRepartidor()
        {
            Usuario usuario = SesionUsuario.GetInstancia().UsuarioActual;

            if (usuario == null)
                throw new Exception("No hay un usuario logueado.");

            lblBienvenida.Text = $"Bienvenido, {usuario.Nombre} {usuario.Apellido}";
        }

        private void CargarRutaActual()
        {
            idAsignacionActual = asignacionRutaBLL.ObtenerMiAsignacionActiva();

            if (idAsignacionActual <= 0)
            {
                MostrarSinRutaAsignada();
                return;
            }

            DataTable datosAsignacion = asignacionRutaBLL.ObtenerDatosAsignacion(idAsignacionActual);

            if (datosAsignacion.Rows.Count == 0)
                throw new Exception("No se encontraron los datos de la asignación activa.");

            DataRow fila = datosAsignacion.Rows[0];

            idRutaActual = Convert.ToInt32(fila["id_ruta"]);

            lblValorNumeroRuta.Text = idRutaActual.ToString();
            lblValorEstadoRuta.Text = fila["estado_ruta"].ToString();
            lblValorFechaAsignacion.Text = Convert.ToDateTime(fila["fecha_asignacion"]).ToString("dd/MM/yyyy HH:mm");

            CargarEnviosDeLaRuta();
            ActualizarEstadoBotonesRuta();
        }

        private void MostrarSinRutaAsignada()
        {
            idAsignacionActual = 0;
            idRutaActual = 0;

            lblValorNumeroRuta.Text = "-";
            lblValorEstadoRuta.Text = "Sin ruta asignada";
            lblValorFechaAsignacion.Text = "-";
            lblCantidadProgreso.Text = "0 de 0 envíos";
            pbProgreso.Value = 0;

            dataGridViewEnviosDeLaRuta.DataSource = null;

            btnIniciarRuta.Enabled = false;
            btnImprimirHojaRuta.Enabled = false;
            btnFinalizarRuta.Enabled = false;
            btnRegistrarEntregas.Enabled = false;
        }

        private void CargarEnviosDeLaRuta()
        {
            if (idRutaActual <= 0)
            {
                dataGridViewEnviosDeLaRuta.DataSource = null;
                return;
            }

            DataTable envios = rutaBLL.ObtenerEnviosDeRuta(idRutaActual);
            DataTable resumenIntentos = intentoEntregaBLL.ObtenerResumenIntentosPorRuta(idRutaActual);

            foreach (DataColumn columna in resumenIntentos.Columns)
            {
                if (columna.ColumnName != "id_ruta_envio")
                    continue;

                break;
            }

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

            ActualizarProgreso();
        }

        private void ActualizarProgreso()
        {
            int totalEnvios = dataGridViewEnviosDeLaRuta.Rows.Count;
            int enviosConIntento = 0;

            if (totalEnvios > 0)
            {
                DataTable resumen = intentoEntregaBLL.ObtenerResumenIntentosPorRuta(idRutaActual);

                foreach (DataRow fila in resumen.Rows)
                {
                    if (fila["cantidad_intentos"] != DBNull.Value && Convert.ToInt32(fila["cantidad_intentos"]) > 0)
                        enviosConIntento++;
                }
            }

            lblCantidadProgreso.Text = $"{enviosConIntento} de {totalEnvios} envíos";

            pbProgreso.Minimum = 0;
            pbProgreso.Maximum = Math.Max(totalEnvios, 1);
            pbProgreso.Value = Math.Min(enviosConIntento, pbProgreso.Maximum);
        }

        private void ActualizarEstadoBotonesRuta()
        {
            string estadoRuta = lblValorEstadoRuta.Text;

            btnIniciarRuta.Enabled = estadoRuta == "Asignada";
            btnImprimirHojaRuta.Enabled = estadoRuta == "Asignada" || estadoRuta == "En curso";
            btnFinalizarRuta.Enabled = estadoRuta == "En curso";
            btnRegistrarEntregas.Enabled = estadoRuta == "En curso";
        }

        private void CargarResumenDiario()
        {
            // Lo completamos en la siguiente etapa.
        }

        private void btnIniciarRuta_Click(object sender, EventArgs e)
        {
            if (idAsignacionActual <= 0)
            {
                MessageBox.Show("No hay una ruta asignada para iniciar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult respuesta = MessageBox.Show("¿Desea iniciar la ruta asignada?", "Confirmar inicio", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                asignacionRutaBLL.IniciarRuta(idAsignacionActual);

                MessageBox.Show("La ruta se inició correctamente.", "Ruta iniciada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarRutaActual();
                CargarResumenDiario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al iniciar la ruta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimirHojaRuta_Click(object sender, EventArgs e)
        {
            if (idAsignacionActual <= 0)
            {
                MessageBox.Show("No hay una ruta asignada para imprimir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                rutaBLL.GenerarHojaRuta(idAsignacionActual, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al generar la hoja de ruta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegistrarEntregas_Click(object sender, EventArgs e)
        {
            if (idRutaActual <= 0 || idAsignacionActual <= 0)
            {
                MessageBox.Show("No hay una ruta asignada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lblValorEstadoRuta.Text != "En curso")
            {
                MessageBox.Show("Primero debe iniciar la ruta para registrar entregas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (RegistroEntregasForm formulario = new RegistroEntregasForm(idRutaActual))
            {
                formulario.ShowDialog();
            }

            CargarEnviosDeLaRuta();
        }

        private void btnFinalizarRuta_Click(object sender, EventArgs e)
        {
            if (idAsignacionActual <= 0)
            {
                MessageBox.Show("No hay una ruta asignada para finalizar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lblValorEstadoRuta.Text != "En curso")
            {
                MessageBox.Show("La ruta no se encuentra en curso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult respuesta = MessageBox.Show("¿Está seguro de que desea finalizar la ruta? Esta acción no se puede deshacer.", "Confirmar finalización", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                asignacionRutaBLL.FinalizarRuta(idAsignacionActual);

                MessageBox.Show("La ruta se finalizó correctamente.", "Ruta finalizada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarRutaActual();
                CargarResumenDiario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al finalizar la ruta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}