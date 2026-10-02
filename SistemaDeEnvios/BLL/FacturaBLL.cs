using BE;
using BLL.Otros;
using DAL;
using Servicios;
using System;
using System.Diagnostics;

namespace BLL
{
    public class FacturaBLL
    {
        private FacturaDAL dal = new FacturaDAL();
        private EnvioBLL enviobll = new EnvioBLL();
        private PagoBLL pagobll = new PagoBLL();
        private DestinoBLL destinobll = new DestinoBLL();
        private EventoBLL eventobll = new EventoBLL();
        private DVBLL dvBLL = new DVBLL();

        public FacturaBE ObtenerPorEnvio(int idEnvio)
        {
            if (idEnvio <= 0)
                throw new Exception("El código del envío no es válido.");

            return dal.ObtenerPorEnvio(idEnvio);
        }

        public string ObtenerNumeroFactura(int idFactura)
        {
            if (idFactura <= 0)
                throw new Exception("El número de factura no es válido.");

            return $"FAC-{idFactura:D6}";
        }

        public FacturaBE GenerarFactura(int idEnvio)
        {
            if (idEnvio <= 0)
                throw new Exception("El envío seleccionado no es válido.");

            EnvioBE envio = enviobll.ConsultaPorId(idEnvio);

            if (envio == null)
                throw new Exception("El envío no existe.");

            if (envio.Estado != "Pagado")
                throw new Exception("El envío no se encuentra pendiente de facturación.");

            PagoBE pago = pagobll.ObtenerPagoAprobadoPorEnvio(idEnvio);

            if (pago == null)
                throw new Exception("No se encontró un pago aprobado para el envío.");

            FacturaBE facturaExistente = dal.ObtenerPorPago(pago.IdPago);

            if (facturaExistente != null)
                throw new Exception("El pago seleccionado ya posee una factura.");

            FacturaBE factura = new FacturaBE(idEnvio, pago.IdPago, pago.DniCliente, pago.Importe);
            factura.Fecha = DateTime.Now;

            int idFactura = dal.AltaFactura(factura);
            factura.IdFactura = idFactura;

            enviobll.MarcarComoFacturado(idEnvio);

            dvBLL.ActualizarDVH("Factura", "id_factura", idFactura);
            dvBLL.ActualizarDVV("Factura");

            eventobll.RegistrarEvento("envios", "ev_factura_envio", 2);

            return factura;
        }

        public void GenerarPdf(int idEnvio, bool abrir = true)
        {
            FacturaBE factura = ObtenerPorEnvio(idEnvio);

            if (factura == null)
                throw new Exception("El envío no posee una factura generada.");

            EnvioBE envio = enviobll.ConsultaPorId(idEnvio);

            if (envio == null)
                throw new Exception("El envío no existe.");

            DestinoBE destino = destinobll.ConsultaPorId(envio.IdDestino);

            if (destino == null)
                throw new Exception("No se encontró el destino asociado al envío.");

            GeneradorPDF.GenerarFactura(factura, envio, destino, abrir);
        }
        public void AbrirPdf(int idEnvio)
        {
            FacturaBE factura = ObtenerPorEnvio(idEnvio);

            if (factura == null)
                throw new Exception("El envío no posee una factura generada.");

            string numeroFactura = ObtenerNumeroFactura(factura.IdFactura);
            string archivo = Path.Combine(AppContext.BaseDirectory, "Facturas", numeroFactura + ".pdf");

            if (!File.Exists(archivo))
                throw new Exception("No se encontró el archivo PDF de la factura.");

            Process.Start(new ProcessStartInfo()
            {
                FileName = archivo,
                UseShellExecute = true
            });
        }
    }
}