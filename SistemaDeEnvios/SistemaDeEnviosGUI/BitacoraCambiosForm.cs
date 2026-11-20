using Servicios;
using Servicios.BLLs;
using Servicios.GestionIdiomas;
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
    public partial class BitacoraCambiosForm : Form, IObserverIdioma
    {
        private EnvioHistorialBLL bll = new EnvioHistorialBLL();

        public BitacoraCambiosForm()
        {
            InitializeComponent();
            GestorIdiomas.Instancia.Registrar(this);
        }

        private void BitacoraCambiosForm_Load(object sender, EventArgs e)
        {
            CargarFiltros();
            RefrescarGrilla();
            ActualizarIdioma();

            dataGridViewHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewHistorial.MultiSelect = false;
            dataGridViewHistorial.ReadOnly = true;
            dataGridViewHistorial.AllowUserToAddRows = false;
            dataGridViewHistorial.AllowUserToDeleteRows = false;
        }
        private void ConfigurarColumnas()
        {
            dataGridViewHistorial.Columns["id_version"].Visible = false;
            dataGridViewHistorial.Columns["id_envio"].Visible = false;

            dataGridViewHistorial.Columns["Codigo"].Width = 115;
            dataGridViewHistorial.Columns["Fecha"].Width = 85;
            dataGridViewHistorial.Columns["Hora"].Width = 70;
            dataGridViewHistorial.Columns["Origen"].Width = 150;
            dataGridViewHistorial.Columns["Remitente"].Width = 150;
            dataGridViewHistorial.Columns["Intentos_Fallidos"].Width = 90;

            dataGridViewHistorial.Columns["DescripcionPaquete"].Width = 150;
            dataGridViewHistorial.Columns["Peso"].Width = 65;
            dataGridViewHistorial.Columns["Alto"].Width = 65;
            dataGridViewHistorial.Columns["Ancho"].Width = 65;
            dataGridViewHistorial.Columns["Largo"].Width = 65;

            dataGridViewHistorial.Columns["Direccion"].Width = 170;
            dataGridViewHistorial.Columns["Ciudad"].Width = 110;
            dataGridViewHistorial.Columns["CodigoPostal"].Width = 85;
            dataGridViewHistorial.Columns["Provincia"].Width = 110;

            dataGridViewHistorial.Columns["DniDestinatario"].Width = 90;
            dataGridViewHistorial.Columns["NombreDestinatario"].Width = 110;
            dataGridViewHistorial.Columns["ApellidoDestinatario"].Width = 110;
            dataGridViewHistorial.Columns["TelefonoDestinatario"].Width = 110;
            dataGridViewHistorial.Columns["EmailDestinatario"].Width = 170;

            dataGridViewHistorial.Columns["Act"].Width = 60;
        }
        private void RefrescarGrilla()
        {
            dataGridViewHistorial.DataSource = bll.ObtenerTodos();
            ConfigurarColumnas();
            ActualizarEstadoBotonActivar();
        }

        private void CargarFiltros()
        {
            comboBoxCodigo.DataSource = bll.ObtenerCodigosSeguimiento();
            comboBoxCodigo.DisplayMember = "codigo_seguimiento";
            comboBoxCodigo.ValueMember = "codigo_seguimiento";
            comboBoxCodigo.SelectedIndex = -1;
            comboBoxCodigo.Text = "";

            comboBoxRemitente.DataSource = bll.ObtenerRemitentes();
            comboBoxRemitente.DisplayMember = "Remitente";
            comboBoxRemitente.ValueMember = "id_remitente";
            comboBoxRemitente.SelectedIndex = -1;
            comboBoxRemitente.Text = "";

            dateTimePickerDesde.Checked = false;
            dateTimePickerHasta.Checked = false;
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            string codigo = comboBoxCodigo.SelectedIndex != -1 ? comboBoxCodigo.SelectedValue.ToString() : null;
            int? dniRemitente = comboBoxRemitente.SelectedIndex != -1 ? Convert.ToInt32(comboBoxRemitente.SelectedValue) : null;
            DateTime? desde = dateTimePickerDesde.Checked ? dateTimePickerDesde.Value.Date : null;
            DateTime? hasta = dateTimePickerHasta.Checked ? dateTimePickerHasta.Value.Date.AddDays(1) : null;

            dataGridViewHistorial.DataSource = bll.Filtrar(codigo, dniRemitente, desde, hasta);
            ConfigurarColumnas();
            ActualizarEstadoBotonActivar();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            CargarFiltros();
            RefrescarGrilla();
        }

        private void ActualizarEstadoBotonActivar()
        {
            btnActivar.Enabled = dataGridViewHistorial.CurrentRow != null &&
                dataGridViewHistorial.CurrentRow.Cells["Act"].Value != DBNull.Value &&
                !Convert.ToBoolean(dataGridViewHistorial.CurrentRow.Cells["Act"].Value);
        }

        private void dataGridViewHistorial_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ActualizarEstadoBotonActivar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void ActualizarIdioma()
        {
            this.Text = Traducciones.Traducir("Bitacora");
            lblHistorialDeEnvios.Text = Traducciones.Traducir("Historial de envíos");
            lblRemitente.Text = Traducciones.Traducir("Remitente");
            lblCod.Text = Traducciones.Traducir("CodigoSeguimiento");
            lblDesde.Text = Traducciones.Traducir("Desde");
            lblHasta.Text = Traducciones.Traducir("Hasta");

            btnAplicar.Text = Traducciones.Traducir("Aplicar");
            btnLimpiar.Text = Traducciones.Traducir("Limpiar");
            btnActivar.Text = Traducciones.Traducir("Activar");
            btnSalir.Text = Traducciones.Traducir("Salir");

            if (dataGridViewHistorial.Columns.Count > 0)
            {
                dataGridViewHistorial.Columns["Codigo"].HeaderText = Traducciones.Traducir("CodigoSeguimiento");
                dataGridViewHistorial.Columns["Fecha"].HeaderText = Traducciones.Traducir("Fecha");
                dataGridViewHistorial.Columns["Hora"].HeaderText = Traducciones.Traducir("Hora");
                dataGridViewHistorial.Columns["Origen"].HeaderText = Traducciones.Traducir("Origen");
                dataGridViewHistorial.Columns["Remitente"].HeaderText = Traducciones.Traducir("Remitente");
                dataGridViewHistorial.Columns["Intentos_Fallidos"].HeaderText = Traducciones.Traducir("IntentosFallidos");
                dataGridViewHistorial.Columns["Act"].HeaderText = Traducciones.Traducir("Activo");
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            GestorIdiomas.Instancia.Desregistrar(this);
            base.OnFormClosed(e);
        }

        private void btnActivar_Click(object sender, EventArgs e)
        {
            if (dataGridViewHistorial.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná una versión del historial.", "Bitácora de cambios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idVersion = Convert.ToInt32(dataGridViewHistorial.CurrentRow.Cells["id_version"].Value);
            string codigo = dataGridViewHistorial.CurrentRow.Cells["Codigo"].Value.ToString();

            DialogResult resultado = MessageBox.Show($"¿Estás seguro de que deseas restaurar la versión seleccionada del envío {codigo}?", "Confirmar restauración", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado != DialogResult.Yes)
                return;

            try
            {
                bll.ActivarVersion(idVersion);

                MessageBox.Show("La versión seleccionada se restauró correctamente.", "Bitácora de cambios", MessageBoxButtons.OK, MessageBoxIcon.Information);

                RefrescarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al restaurar la versión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnSerializar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Serializar((DataTable)dataGridViewHistorial.DataSource, "BitacoraCambios"))
                    MessageBox.Show("La serialización se realizó correctamente.", "Serialización", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al serializar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static readonly string carpetaSerializaciones = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SerializacionesCambios");

        public static bool Serializar(DataTable datos, string nombreFormulario)
        {
            if (datos == null)
                throw new Exception("No hay datos para serializar.");

            if (string.IsNullOrWhiteSpace(nombreFormulario))
                throw new Exception("El nombre del formulario no es válido.");

            if (datos.Columns.Count == 0)
                throw new Exception("La grilla no contiene columnas para serializar.");

            Directory.CreateDirectory(carpetaSerializaciones);

            using (SaveFileDialog dialogo = new SaveFileDialog())
            {
                dialogo.Title = "Guardar serialización";
                dialogo.InitialDirectory = carpetaSerializaciones;
                dialogo.FileName = nombreFormulario + ".xml";
                dialogo.Filter = "Archivos XML (*.xml)|*.xml";
                dialogo.DefaultExt = "xml";
                dialogo.AddExtension = true;
                dialogo.OverwritePrompt = true;

                if (dialogo.ShowDialog() != DialogResult.OK)
                    return false;

                DataTable copia = datos.Copy();
                copia.TableName = nombreFormulario;
                copia.WriteXml(dialogo.FileName, XmlWriteMode.WriteSchema);

                return true;
            }
        }

        private void btnDeserializar_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable xml = Deserializar("BitacoraCambios");

                if (xml == null)
                    return;

                dataGridViewHistorial.DataSource = xml;
                ConfigurarColumnas();
                ActualizarEstadoBotonActivar();

                MessageBox.Show("La deserialización se realizó correctamente.", "Deserialización", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al deserializar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static DataTable Deserializar(string nombreFormulario)
        {
            if (string.IsNullOrWhiteSpace(nombreFormulario))
                throw new Exception("El nombre del formulario no es válido.");

            Directory.CreateDirectory(carpetaSerializaciones);

            using (OpenFileDialog dialogo = new OpenFileDialog())
            {
                dialogo.Title = "Abrir serialización";
                dialogo.InitialDirectory = carpetaSerializaciones;
                dialogo.Filter = "Archivos XML (*.xml)|*.xml";
                dialogo.DefaultExt = "xml";
                dialogo.CheckFileExists = true;
                dialogo.Multiselect = false;

                if (dialogo.ShowDialog() != DialogResult.OK)
                    return null;

                DataTable datos = new DataTable();
                datos.ReadXml(dialogo.FileName);

                return datos;
            }
        }


        private void btnRefrescarGrilla_Click(object sender, EventArgs e)
        {
            RefrescarGrilla();
        }

        private void btnAyuda_Click(object sender, EventArgs e)
        {
            try
            {
                new AyudaForm("Ayuda - Bitácora de cambios", "BitacoraCambios.txt").ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al abrir la ayuda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
