using BLL;
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
    public partial class BackupForm : Form, IObserverIdioma
    {
        public BackupForm()
        {
            InitializeComponent();
            GestorIdiomas.Instancia.Registrar(this);
            ActualizarIdioma();
        }
        BackupBLL backupBLL = new BackupBLL();
        private void btnCrearBackup_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();

                sfd.Filter = "Backup (*.bak)|*.bak";

                string nombre = $"Backup_{DateTime.Now:yyyy-MM-dd_HH-mm}.bak";
                sfd.FileName = nombre;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    backupBLL.HacerBackup(sfd.FileName);
                    MessageBox.Show(Traducciones.Traducir("backupcorrecto"));
                }
            }
            catch (Microsoft.Data.SqlClient.SqlException) { MessageBox.Show(Traducciones.Traducir("error backup"), "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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

                Application.Restart();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            GestorIdiomas.Instancia.Desregistrar(this);
            base.OnFormClosed(e);
        }
        public void ActualizarIdioma()
        {
            this.Text = Traducciones.Traducir("gestionbackup");
            btnCrearBackup.Text = Traducciones.Traducir("crearbackup");
            btnRealizarRestore.Text = Traducciones.Traducir("realizarrestore");
            btnSalir.Text = Traducciones.Traducir("Salir");
        }
    }
}
