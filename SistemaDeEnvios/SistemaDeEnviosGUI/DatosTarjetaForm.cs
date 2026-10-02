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
    public partial class DatosTarjetaForm : Form, IObserverIdioma
    {
        private PagoBLL pagobll = new PagoBLL();
        private FacturaBLL facturabll = new FacturaBLL();
        private EnvioBLL enviobll = new EnvioBLL();

        private int idEnvio;
        private string codigoSeguimiento;
        private decimal importe;
        private int dniCliente;

        public DatosTarjetaForm(int idEnvio, string codigoSeguimiento, decimal importe, int dniCliente)
        {
            InitializeComponent();
            GestorIdiomas.Instancia.Registrar(this);
            this.idEnvio = idEnvio;
            this.codigoSeguimiento = codigoSeguimiento;
            this.importe = importe;
            this.dniCliente = dniCliente;
        }

        private void DatosTarjetaForm_Load(object sender, EventArgs e)
        {
            txtEnvioSeleccionado.Text = codigoSeguimiento;
            txtImporte.Text = importe.ToString("C2");

            txtEnvioSeleccionado.ReadOnly = true;
            txtImporte.ReadOnly = true;

            CargarMeses();
            CargarAños();

            cboMes.SelectedIndex = -1;
            cboAño.SelectedIndex = -1;
            ActualizarIdioma();
        }

        private void CargarMeses()
        {
            cboMes.Items.Clear();

            for (int i = 1; i <= 12; i++)
            {
                cboMes.Items.Add(i.ToString("00"));
            }
        }

        private void CargarAños()
        {
            cboAño.Items.Clear();

            int añoActual = DateTime.Now.Year;

            for (int i = añoActual; i <= añoActual + 10; i++)
            {
                cboAño.Items.Add(i.ToString());
            }
        }


        private void btnPagar_Click(object sender, EventArgs e)
        {
            bool pagoRegistrado = false;

            try
            {
                if (cboMes.SelectedIndex == -1 || cboAño.SelectedIndex == -1)
                    throw new Exception("Debe seleccionar el mes y el año de vencimiento.");

                string mes = cboMes.SelectedItem.ToString();
                string año = cboAño.SelectedItem.ToString();

                pagobll.ValidarDatosTarjeta(txtNombreTarjeta.Text, txtNumeroTarjeta.Text, mes, año, txtCodigoSeguridad.Text);

                DialogResult autorizacion = MessageBox.Show("Importe a cobrar: " + importe.ToString("C2") + "\nTitular de la tarjeta: " + txtNombreTarjeta.Text.Trim() + "\n\n¿El Banco autoriza la operación?", "Autorización bancaria", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (autorizacion != DialogResult.Yes)
                {
                    MessageBox.Show("El Banco rechazó la operación. El envío continúa pendiente de pago.", "Pago rechazado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                pagobll.RegistrarPago(idEnvio, dniCliente, importe);
                pagoRegistrado = true;

                facturabll.GenerarFactura(idEnvio);
                facturabll.GenerarPdf(idEnvio, false);

                DialogResult imprimir = MessageBox.Show("La factura fue generada y guardada correctamente.\n\n¿Desea abrirla para imprimirla ahora?", "Factura generada", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                enviobll.AutorizarEnvio(idEnvio);

                string mensaje = "El pago fue registrado correctamente.\nLa factura fue generada correctamente.\nEl envío fue autorizado correctamente.";

                if (imprimir == DialogResult.No)
                    mensaje += "\n\nPuede imprimir la factura más adelante desde el formulario de facturas.";

                MessageBox.Show(mensaje, "Operación realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();

                if (imprimir == DialogResult.Yes)
                {
                    try
                    {
                        facturabll.AbrirPdf(idEnvio);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("El pago, la factura y la autorización se completaron correctamente, pero no se pudo abrir el PDF.\n\nPuede volver a intentarlo desde el formulario de facturas.\n\nDetalle: " + ex.Message, "Abrir factura", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                if (pagoRegistrado)
                {
                    MessageBox.Show("El pago ya fue registrado, pero ocurrió un error en los pasos posteriores.\n\nNo vuelva a cobrar este envío. Verifique su estado y complete la facturación o autorización desde los formularios correspondientes.\n\nDetalle: " + ex.Message, "Operación incompleta", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }

                MessageBox.Show(ex.Message, "Datos de pago", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            GestorIdiomas.Instancia.Desregistrar(this);
            base.OnFormClosed(e);
        }

        public void ActualizarIdioma()
        {
            this.Text = Traducciones.Traducir("DatosTarjeta");

            lblEnvioSeleccionado.Text = Traducciones.Traducir("EnvioSeleccionado");
            lblImporteAPagar.Text = Traducciones.Traducir("ImporteAPagar");
            lblNombreTarjeta.Text = Traducciones.Traducir("NombreTarjeta");
            lblNumeroTarjeta.Text = Traducciones.Traducir("NumeroTarjeta");
            lblCodigoSeguridad.Text = Traducciones.Traducir("CodigoSeguridad");
            lblVencimiento.Text = Traducciones.Traducir("Vencimiento");

            btnPagar.Text = Traducciones.Traducir("Pagar");
            btnCancelar.Text = Traducciones.Traducir("Cancelar");
        }
    }
}
