using BLL;
using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Windows.Forms;

namespace SistemaDeEnviosGUI
{
    public partial class AsignacionRutaForm : Form
    {
        private RutaBLL rutaBLL = new RutaBLL();
        private AsignacionRutaBLL asignacionBLL = new AsignacionRutaBLL();

        public AsignacionRutaForm()
        {
            InitializeComponent();

            ConfigurarGrillas();

            rbRutasPendientes.Checked = true;
            rbRepartidoresSinRuta.Checked = true;

            CargarRutas();
            CargarRepartidores();
            CargarHistorial();
        }

        private void ConfigurarGrillas()
        {
            dataGridViewRutasPendientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewRutasPendientes.MultiSelect = false;
            dataGridViewRutasPendientes.ReadOnly = true;
            dataGridViewRutasPendientes.AllowUserToAddRows = false;
            dataGridViewRutasPendientes.AllowUserToDeleteRows = false;
            dataGridViewRutasPendientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridViewRepartidores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewRepartidores.MultiSelect = false;
            dataGridViewRepartidores.ReadOnly = true;
            dataGridViewRepartidores.AllowUserToAddRows = false;
            dataGridViewRepartidores.AllowUserToDeleteRows = false;
            dataGridViewRepartidores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvHistorialAsignaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistorialAsignaciones.MultiSelect = false;
            dgvHistorialAsignaciones.ReadOnly = true;
            dgvHistorialAsignaciones.AllowUserToAddRows = false;
            dgvHistorialAsignaciones.AllowUserToDeleteRows = false;
            dgvHistorialAsignaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void CargarRutas()
        {
            try
            {
                DataTable tabla;

                if (rbRutasPendientes.Checked)
                {
                    tabla = rutaBLL.ObtenerRutasPendientesAsignacion();
                }
                else
                {
                    tabla = rutaBLL.ObtenerTodasLasRutas();
                }

                dataGridViewRutasPendientes.DataSource = tabla;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las rutas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarRepartidores()
        {
            try
            {
                DataTable tabla;

                if (rbRepartidoresSinRuta.Checked)
                {
                    tabla = asignacionBLL.ObtenerRepartidoresDisponibles();
                }
                else
                {
                    tabla = asignacionBLL.ObtenerTodosLosRepartidores();
                }

                dataGridViewRepartidores.DataSource = tabla;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los repartidores: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarHistorial()
        {
            try
            {
                dgvHistorialAsignaciones.DataSource = asignacionBLL.ObtenerAsignaciones();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el historial de asignaciones: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAsignarRuta_Click(object sender, EventArgs e)
        {
            if (dataGridViewRutasPendientes.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar una ruta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dataGridViewRepartidores.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un repartidor.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idRuta = Convert.ToInt32(dataGridViewRutasPendientes.CurrentRow.Cells["id_ruta"].Value);
                int dniRepartidor = Convert.ToInt32(dataGridViewRepartidores.CurrentRow.Cells["dni"].Value);

                DialogResult respuesta = MessageBox.Show("¿Confirma la asignación de la ruta seleccionada al repartidor seleccionado?", "Confirmar asignación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta != DialogResult.Yes)
                    return;

                int idAsignacion = asignacionBLL.AsignarRuta(idRuta, dniRepartidor);

                MessageBox.Show("La ruta fue asignada correctamente.", "Asignación realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult imprimir = MessageBox.Show("¿Desea generar e imprimir la hoja de ruta ahora?", "Hoja de ruta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (imprimir == DialogResult.Yes)
                {
                    try
                    {
                        rutaBLL.GenerarHojaRuta(idAsignacion);

                        MessageBox.Show("La hoja de ruta fue generada correctamente.", "Hoja de ruta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("La ruta fue asignada, pero no se pudo generar la hoja de ruta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("El repartidor podrá generar e imprimir la hoja de ruta desde su formulario.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                CargarRutas();
                CargarRepartidores();
                CargarHistorial();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo realizar la asignación: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnQuitarAsignacion_Click(object sender, EventArgs e)
        {
            if (dataGridViewRepartidores.CurrentRow == null || dataGridViewRepartidores.Rows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un repartidor.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (rbRepartidoresSinRuta.Checked)
                {
                    MessageBox.Show("El repartidor seleccionado no tiene ninguna ruta asignada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string disponibilidad = dataGridViewRepartidores.CurrentRow.Cells["disponibilidad"].Value.ToString();

                if (disponibilidad != "Ocupado")
                {
                    MessageBox.Show("El repartidor seleccionado no tiene ninguna ruta asignada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idRuta = Convert.ToInt32(dataGridViewRepartidores.CurrentRow.Cells["id_ruta_activa"].Value);

                string motivo = Interaction.InputBox("Ingrese el motivo por el cual desea quitar la asignación:", "Motivo de desasignación", "");

                DialogResult respuesta = MessageBox.Show("¿Confirma que desea quitar la asignación del repartidor seleccionado?", "Confirmar desasignación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta != DialogResult.Yes)
                    return;

                asignacionBLL.QuitarAsignacion(idRuta, motivo);

                MessageBox.Show("La asignación fue quitada correctamente. El repartidor quedó disponible y la ruta volvió a estar pendiente.", "Desasignación realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarRutas();
                CargarRepartidores();
                CargarHistorial();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo quitar la asignación: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void rbRutasPendientes_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRutasPendientes.Checked)
                CargarRutas();
        }

        private void rbRutasTodas_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRutasTodas.Checked)
                CargarRutas();
        }

        private void rbRepartidoresSinRuta_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRepartidoresSinRuta.Checked)
                CargarRepartidores();
        }

        private void rbRepartidoresTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRepartidoresTodos.Checked)
                CargarRepartidores();
        }
    }
}