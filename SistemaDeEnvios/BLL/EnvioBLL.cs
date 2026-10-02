using BE;
using BLL.Otros;
using DAL;
using Servicios;
using System;
using System.Data;

namespace BLL
{
    public class EnvioBLL
    {
        private EnvioDAL dal = new EnvioDAL();
        private PaqueteBLL paquetebll = new PaqueteBLL();
        private DestinoBLL destinobll = new DestinoBLL();
        private EventoBLL eventobll = new EventoBLL();
        private DVBLL dvBLL = new DVBLL();

        public EnvioBE ConsultaPorId(int idEnvio)
        {
            if (idEnvio <= 0) throw new Exception("El código del envío no es válido.");

            return dal.ConsultaPorId(idEnvio);
        }
        public int RegistrarEnvio(PaqueteBE paquete, DestinoBE destino, EnvioBE envio)
        {
            if (paquete == null) throw new Exception("Los datos del paquete son obligatorios.");

            if (destino == null) throw new Exception("Los datos del destino son obligatorios.");

            if (envio == null) throw new Exception("Los datos del envío son obligatorios.");

            int idPaquete = paquetebll.AltaPaquete(paquete);

            int idDestino = destinobll.AltaDestino(destino);

            envio.IdPaquete = idPaquete;
            envio.IdDestino = idDestino;

            return AltaEnvio(envio);
        }

        public int AltaEnvio(EnvioBE envio)
        {
            ValidarEnvio(envio);

            envio.FechaRegistro = DateTime.Now;
            envio.Estado = "Registrado";

            int idEnvio = dal.AltaEnvio(envio);

            envio.IdEnvio = idEnvio;
            envio.CodigoSeguimiento = $"ENV-{idEnvio:D6}";

            dal.ActualizarCodigoSeguimiento(idEnvio, envio.CodigoSeguimiento);

            dvBLL.ActualizarDVH("Envio", "id_envio", idEnvio);
            dvBLL.ActualizarDVV("Envio");

            eventobll.RegistrarEvento("envios", "ev_alta_envio", 2);

            return idEnvio;
        }

        private void ValidarEnvio(EnvioBE envio)
        {
            if (envio == null)
                throw new Exception("El envío no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(envio.Origen))
                throw new Exception("El origen es obligatorio.");

            if (envio.IdPaquete <= 0)
                throw new Exception("El paquete seleccionado no es válido.");

            if (envio.IdDestino <= 0)
                throw new Exception("El destino seleccionado no es válido.");

            if (envio.IdRemitente <= 0)
                throw new Exception("El remitente seleccionado no es válido.");

            if (envio.IdDestinatario <= 0)
                throw new Exception("El destinatario seleccionado no es válido.");
        }

        public DataTable ObtenerEnvios(string estado, string codigo, string dniRemitente, string dniDestinatario, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            return dal.ObtenerEnvios(estado, codigo, dniRemitente, dniDestinatario, fechaDesde, fechaHasta);
        }

        public void ModificarEnvio(EnvioBE envio, PaqueteBE paquete, DestinoBE destino)
        {
            if (envio == null)
                throw new Exception("El envío no puede ser nulo.");

            if (paquete == null)
                throw new Exception("El paquete no puede ser nulo.");

            if (destino == null)
                throw new Exception("El destino no puede ser nulo.");

            if (envio.IdEnvio <= 0)
                throw new Exception("El envío seleccionado no es válido.");

            if (envio.Estado != "Registrado" && envio.Estado != "Pagado" && envio.Estado != "Aprobado")
            {
                throw new Exception("El envío no puede modificarse en su estado actual.");
            }

            paquete.IdPaquete = envio.IdPaquete;
            destino.IdDestino = envio.IdDestino;

            paquetebll.ModificarPaquete(paquete);
            destinobll.ModificarDestino(destino);

            eventobll.RegistrarEvento("envios", "ev_modificacion_envio", 2);
        }

        public void CancelarEnvio(int idEnvio)
        {
            if (idEnvio <= 0) throw new Exception("El envío seleccionado no es válido.");

            EnvioBE envio = ConsultaPorId(idEnvio);

            if (envio == null) throw new Exception("El envío seleccionado no existe.");

            if (envio.Estado == "En distribución" || envio.Estado == "Entregado" || envio.Estado == "Cancelado")
            {
                throw new Exception("El envío no puede cancelarse en su estado actual.");
            }

            dal.CancelarEnvio(idEnvio);

            dvBLL.ActualizarDVH("Envio", "id_envio", idEnvio);
            dvBLL.ActualizarDVV("Envio");

            eventobll.RegistrarEvento("envios", "ev_baja_envio", 2);
        }
        public decimal ObtenerImporteEnvio(int idEnvio)
        {
            EnvioBE envio = ConsultaPorId(idEnvio);

            if (envio == null)
                throw new Exception("El envío no existe.");

            if (envio.Estado != "Registrado")
                throw new Exception("El envío no se encuentra pendiente de pago.");

            PaqueteBE paquete = paquetebll.ConsultaPorId(envio.IdPaquete);

            if (paquete == null)
                throw new Exception("No se encontró el paquete asociado al envío.");

            return CalcularImporte(paquete);
        }

        public decimal CalcularImporte(PaqueteBE paquete)
        {
            if (paquete == null)
                throw new Exception("El paquete no puede ser nulo.");

            decimal tarifaBase = 4000m;
            decimal tarifaPorKg = 700m;
            decimal recargoDimensiones = 0m;

            decimal volumen = paquete.Alto * paquete.Ancho * paquete.Largo;

            if (volumen > 0.05m)
                recargoDimensiones = 1000m;

            if (volumen > 0.10m)
                recargoDimensiones = 2000m;

            if (volumen > 0.20m)
                recargoDimensiones = 4000m;

            return tarifaBase + (paquete.Peso * tarifaPorKg) + recargoDimensiones;
        }

        public void MarcarComoPagado(int idEnvio)
        {
            EnvioBE envio = ConsultaPorId(idEnvio);

            if (envio == null)
                throw new Exception("El envío no existe.");

            if (envio.Estado != "Registrado")
                throw new Exception("El envío no se encuentra pendiente de pago.");

            dal.MarcarComoPagado(idEnvio);
            dvBLL.ActualizarDVH("Envio", "id_envio", idEnvio);
            dvBLL.ActualizarDVV("Envio");
        }

        public void MarcarComoFacturado(int idEnvio)
        {
            EnvioBE envio = ConsultaPorId(idEnvio);

            if (envio == null)
                throw new Exception("El envío no existe.");

            if (envio.Estado != "Pagado")
                throw new Exception("El envío no se encuentra pagado.");

            dal.MarcarComoFacturado(idEnvio);

            dvBLL.ActualizarDVH("Envio", "id_envio", idEnvio);
            dvBLL.ActualizarDVV("Envio");
        }

        public void AutorizarEnvio(int idEnvio)
        {
            EnvioBE envio = ConsultaPorId(idEnvio);

            if (envio == null)
                throw new Exception("El envío no existe.");

            if (envio.Estado != "Facturado")
                throw new Exception("El envío no se encuentra facturado.");

            dal.AutorizarEnvio(idEnvio);

            dvBLL.ActualizarDVH("Envio", "id_envio", idEnvio);
            dvBLL.ActualizarDVV("Envio");

            eventobll.RegistrarEvento("envios", "ev_autorizacion_envio", 2);
        }
        public DataTable ObtenerEnviosParaFacturacion()
        {
            return dal.ObtenerEnviosParaFacturacion();
        }
    }
}