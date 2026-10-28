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

            if (destino.DniDestinatario < 1000000 || destino.DniDestinatario > 99999999)
                throw new Exception("El DNI del destinatario debe contener entre 7 y 8 dígitos.");

            if (string.IsNullOrWhiteSpace(destino.NombreDestinatario))
                throw new Exception("El nombre del destinatario es obligatorio.");

            if (string.IsNullOrWhiteSpace(destino.ApellidoDestinatario))
                throw new Exception("El apellido del destinatario es obligatorio.");

            if (string.IsNullOrWhiteSpace(destino.TelefonoDestinatario))
                throw new Exception("El teléfono del destinatario es obligatorio.");

            if (!FormatoTelefono(destino.TelefonoDestinatario))
                throw new Exception("El formato del teléfono del destinatario no es válido.");

            if (string.IsNullOrWhiteSpace(destino.EmailDestinatario))
                throw new Exception("El email del destinatario es obligatorio.");

            if (!FormatoMail(destino.EmailDestinatario))
                throw new Exception("El formato del email del destinatario no es válido.");
        }
        private bool FormatoMail(string email)
        {
            string formato = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, formato);
        }

        private bool FormatoTelefono(string telefono)
        {
            string formato = @"^(11|15)\d{8}$|^(2224|2225)\d{6}$";
            return System.Text.RegularExpressions.Regex.IsMatch(telefono, formato);
        }
        public void ModificarDestino(DestinoBE destino)
        {
            if (destino == null)
                throw new Exception("El destino no puede ser nulo.");

            if (destino.IdDestino <= 0)
                throw new Exception("El destino seleccionado no es válido.");

            ValidarDestino(destino);

            dal.ModificarDestino(destino);

            dvBLL.ActualizarDVH("Destino", "id_destino", destino.IdDestino);
            dvBLL.ActualizarDVV("Destino");

            eventobll.RegistrarEvento("destinos", "ev_modificacion_destino", 2);
        }
    }
}