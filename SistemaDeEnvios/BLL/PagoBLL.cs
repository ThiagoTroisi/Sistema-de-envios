using BE;
using DAL;
using System;
using System.Text.RegularExpressions;

namespace BLL
{
    public class PagoBLL
    {
        private PagoDAL dal = new PagoDAL();
        private EnvioBLL enviobll = new EnvioBLL();
        private EventoBLL eventobll = new EventoBLL();

        public PagoBE ConsultaPorId(int idPago)
        {
            return dal.ConsultaPorId(idPago);
        }

        public PagoBE ObtenerPagoAprobadoPorEnvio(int idEnvio)
        {
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
            EnvioBE envio = enviobll.ConsultaPorId(idEnvio);

            if (envio == null)
                throw new Exception("El envío no existe.");

            if (envio.Estado != "Registrado")
                throw new Exception("El envío no se encuentra pendiente de pago.");

            PagoBE pago = new PagoBE(idEnvio, dniCliente, importe);
            pago.FechaPago = DateTime.Now;
            pago.Estado = "Aprobado";

            int idPago = dal.AltaPago(pago);

            enviobll.MarcarComoPagado(idEnvio);

            eventobll.RegistrarEvento("envios", "ev_pago_envio", 2);

            return idPago;
        }
    }
}