using BE;
using DAL;
using BLL.Otros;
using Servicios;
using System;
using System.Text.RegularExpressions;

namespace BLL
{
    public class PagoBLL
    {
        private PagoDAL dal = new PagoDAL();
        private FacturaDAL facturaDal = new FacturaDAL();
        private EnvioBLL enviobll = new EnvioBLL();
        private EventoBLL eventobll = new EventoBLL();
        private DVBLL dvBLL = new DVBLL();

        public PagoBE ConsultaPorId(int idPago)
        {
            if (idPago <= 0)
                throw new Exception("El código del pago no es válido.");

            return dal.ConsultaPorId(idPago);
        }

        public PagoBE ObtenerPagoAprobadoPorEnvio(int idEnvio)
        {
            if (idEnvio <= 0)
                throw new Exception("El código del envío no es válido.");

            return dal.ObtenerPagoAprobadoPorEnvio(idEnvio);
        }

        public void ValidarDatosTarjeta(string nombre, string numero, string mes, string anio, string codigo)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("Debe ingresar el nombre de la tarjeta.");

            string[] palabras = nombre.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (palabras.Length < 2)
                throw new Exception("Debe ingresar nombre y apellido.");

            if (!Regex.IsMatch(nombre.Trim(), @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                throw new Exception("El nombre de la tarjeta solo puede contener letras y espacios.");

            if (!Regex.IsMatch(numero ?? "", @"^\d{16}$"))
                throw new Exception("La tarjeta debe tener exactamente 16 dígitos.");

            if (!Regex.IsMatch(mes ?? "", @"^(0[1-9]|1[0-2])$"))
                throw new Exception("El mes de vencimiento no es válido.");

            if (!Regex.IsMatch(anio ?? "", @"^\d{4}$"))
                throw new Exception("El año de vencimiento no es válido.");

            if (!Regex.IsMatch(codigo ?? "", @"^\d{3}$"))
                throw new Exception("El código de seguridad debe tener exactamente 3 dígitos.");

            int mesNum = Convert.ToInt32(mes);
            int anioNum = Convert.ToInt32(anio);
            int anioActual = DateTime.Now.Year;

            if (anioNum < anioActual || anioNum > anioActual + 10)
                throw new Exception("El año de vencimiento no es válido.");

            DateTime vencimiento = new DateTime(anioNum, mesNum, DateTime.DaysInMonth(anioNum, mesNum));

            if (vencimiento < DateTime.Today)
                throw new Exception("La tarjeta está vencida.");
        }

        public int RegistrarPago(int idEnvio, int dniCliente, decimal importe)
        {
            if (idEnvio <= 0)
                throw new Exception("El código del envío no es válido.");

            if (dniCliente <= 0)
                throw new Exception("El DNI del cliente no es válido.");

            if (importe <= 0)
                throw new Exception("El importe debe ser mayor a cero.");

            EnvioBE envio = enviobll.ConsultaPorId(idEnvio);

            if (envio == null)
                throw new Exception("El envío no existe.");

            if (envio.Estado != "Registrado")
                throw new Exception("El envío no se encuentra pendiente de pago.");

            FacturaBE factura = facturaDal.ObtenerPorEnvio(idEnvio);

            if (factura == null)
                throw new Exception("Debe generar la factura antes de realizar el cobro.");

            if (factura.IdPago.HasValue)
                throw new Exception("La factura ya tiene un pago asociado.");

            if (factura.DniCliente != dniCliente)
                throw new Exception("El DNI ingresado no coincide con el cliente de la factura.");

            if (factura.Importe != importe)
                throw new Exception("El importe ingresado no coincide con el importe de la factura.");

            PagoBE pagoAprobado = dal.ObtenerPagoAprobadoPorEnvio(idEnvio);

            if (pagoAprobado != null)
                throw new Exception("El envío ya tiene un pago aprobado registrado.");

            PagoBE pago = new PagoBE(idEnvio, dniCliente, importe);
            pago.FechaPago = DateTime.Now;
            pago.Estado = "Aprobado";

            int idPago = dal.AltaPago(pago);
            dvBLL.ActualizarDVH("Pago", "id_pago", idPago);
            dvBLL.ActualizarDVV("Pago");
            facturaDal.AsociarPago(idEnvio, idPago);

            dvBLL.ActualizarDVH("Factura", "id_factura", factura.IdFactura);
            dvBLL.ActualizarDVV("Factura");

            enviobll.MarcarPendienteDistribucion(idEnvio);

            eventobll.RegistrarEvento("envios", "ev_pago_envio", 2);

            return idPago;
        }
    }
}