using BE;
using BLL.Otros;
using DAL;
using Servicios;
using System;

namespace BLL
{
    public class DestinoBLL
    {
        private DestinoDAL dal = new DestinoDAL();
        private EventoBLL eventobll = new EventoBLL();
        private DVBLL dvBLL = new DVBLL();

        public DestinoBE ConsultaPorId(int idDestino)
        {
            if (idDestino <= 0)
                throw new Exception("El código del destino no es válido.");

            return dal.ConsultaPorId(idDestino);
        }

        public int AltaDestino(DestinoBE destino)
        {
            ValidarDestino(destino);

            int idDestino = dal.AltaDestino(destino);

            dvBLL.ActualizarDVH("Destino", "id_destino", idDestino);
            dvBLL.ActualizarDVV("Destino");

            eventobll.RegistrarEvento("destinos", "ev_alta_destino", 2);

            return idDestino;
        }

        private void ValidarDestino(DestinoBE destino)
        {
            if (destino == null)
                throw new Exception("El destino no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(destino.Direccion))
                throw new Exception("La dirección es obligatoria.");

            if (string.IsNullOrWhiteSpace(destino.Ciudad))
                throw new Exception("La ciudad es obligatoria.");

            if (string.IsNullOrWhiteSpace(destino.CodigoPostal))
                throw new Exception("El código postal es obligatorio.");

            if (string.IsNullOrWhiteSpace(destino.Provincia))
                throw new Exception("La provincia es obligatoria.");
        }

        public void ModificarDestino(DestinoBE destino)
        {
            if (destino == null)
                throw new Exception("El destino no puede ser nulo.");

            if (destino.IdDestino <= 0)
                throw new Exception("El destino seleccionado no es válido.");

            if (string.IsNullOrWhiteSpace(destino.Direccion))
                throw new Exception("La dirección de destino es obligatoria.");

            if (string.IsNullOrWhiteSpace(destino.Ciudad))
                throw new Exception("La ciudad de destino es obligatoria.");

            if (string.IsNullOrWhiteSpace(destino.CodigoPostal))
                throw new Exception("El código postal es obligatorio.");

            if (string.IsNullOrWhiteSpace(destino.Provincia))
                throw new Exception("La provincia es obligatoria.");

            dal.ModificarDestino(destino);

            dvBLL.ActualizarDVH("Destino", "id_destino", destino.IdDestino);
            dvBLL.ActualizarDVV("Destino");

            eventobll.RegistrarEvento("destinos", "ev_modificacion_destino", 2);
        }
    }
}