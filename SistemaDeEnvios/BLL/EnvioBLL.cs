using BE;
using BLL.Otros;
using DAL;
using Servicios;
using System;

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
    }
}