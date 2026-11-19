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
    public partial class AyudaForm : Form
    {
        public AyudaForm(string titulo, string nombreArchivo)
        {
            InitializeComponent();

            Text = titulo;
            StartPosition = FormStartPosition.CenterParent;
            Size = new System.Drawing.Size(650, 500);
            MinimizeBox = false;
            MaximizeBox = true;

            string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Ayudas", nombreArchivo);

            if (!File.Exists(ruta))
                throw new FileNotFoundException("No se encontró el archivo de ayuda.", ruta);

            RichTextBox txtAyuda = new RichTextBox();
            txtAyuda.ReadOnly = true;
            txtAyuda.WordWrap = true;
            txtAyuda.ScrollBars = RichTextBoxScrollBars.Vertical;
            txtAyuda.Dock = DockStyle.Fill;
            txtAyuda.BorderStyle = BorderStyle.None;
            txtAyuda.Font = new System.Drawing.Font("Segoe UI", 10);
            txtAyuda.BackColor = System.Drawing.SystemColors.Window;
            txtAyuda.DetectUrls = false;

            txtAyuda.Text = File.ReadAllText(ruta).Replace("\r\n", "\n").Replace("\n", Environment.NewLine);

            Controls.Add(txtAyuda);
        }
    }
}
