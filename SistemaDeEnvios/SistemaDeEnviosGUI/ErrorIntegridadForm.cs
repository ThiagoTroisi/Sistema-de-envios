using BLL;
using DAL;
using Servicios;
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
    public partial class ErrorIntegridadForm : Form, IObserverIdioma
    {
        public ErrorIntegridadForm(List<ErrorTabla> errores)
        {
            InitializeComponent();
            GestorIdiomas.Instancia.Registrar(this);
            ActualizarIdioma();
            this.errores = errores;
        }
        List<ErrorTabla> errores;
        ErrorTabla tablaSeleccionada;
        DVBLL dvbll = new DVBLL();
        BackupBLL backupBLL = new BackupBLL();
        private void btnRecalcularDV_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var tabla in errores)
                {
                    dvbll.ActualizarTodosLosDVH(tabla.Tabla);
                    dvbll.ActualizarDVV(tabla.Tabla);
                }
                MessageBox.Show(Traducciones.Traducir("integridadrestaurada"));
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnRealizarRestore_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Backup (*.bak)|*.bak";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                backupBLL.Restaurar(ofd.FileName);
                MessageBox.Show(Traducciones.Traducir("restorecorrecto"));
                this.Close();
            }
        }

        private void ErrorIntegridadForm_Load(object sender, EventArgs e)
        {
            listBoxTablas.Items.Clear();
            foreach (var tabla in errores)
            {
                listBoxTablas.Items.Add(tabla);
            }
            listBoxTablas.DisplayMember = "";
        }

        private void listBoxTablas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxTablas.SelectedIndex == -1) return;
            tablaSeleccionada = errores[listBoxTablas.SelectedIndex];

            listBoxTablas.Text = tablaSeleccionada.Tabla;

            dgvRegistros.DataSource = null;
            dgvRegistros.DataSource = tablaSeleccionada.Errores;

            dgvRegistros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            TraducirDGV();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
        }

        public void ActualizarIdioma()
        {
            btnSalir.Text = Traducciones.Traducir("Salir");
            btnRealizarRestore.Text = Traducciones.Traducir("realizarrestore");
            btnRecalcularDV.Text = Traducciones.Traducir("recalculardv");
            this.Text = Traducciones.Traducir("gestionerror");
            lblOpcion.Text = Traducciones.Traducir("seleccioneopcion");
            lblTablasAfectadas.Text = Traducciones.Traducir("tablasafectadas");
            lblRegistrosAfectados.Text = Traducciones.Traducir("registrosafectados");
        }
        private void TraducirDGV()
        {
            dgvRegistros.Columns["Tipo"].HeaderText = Traducciones.Traducir("tipo");
            dgvRegistros.Columns["Registro"].HeaderText = Traducciones.Traducir("registro");
            dgvRegistros.Columns["ValorGuardado"].HeaderText = Traducciones.Traducir("valorguardado");
            dgvRegistros.Columns["ValorCalculado"].HeaderText = Traducciones.Traducir("valorcalculado");
        }
    }
}
