using BE;
using BLL.Otros;
using DAL;
using Servicios;
using System;

namespace BLL
{
    public class PaqueteBLL
    {
        private PaqueteDAL dal = new PaqueteDAL();
        private EventoBLL eventobll = new EventoBLL();
        private DVBLL dvBLL = new DVBLL();

        public PaqueteBE ConsultaPorId(int idPaquete)
        {
            if (idPaquete <= 0) throw new Exception("El código del paquete no es válido.");

            return dal.ConsultaPorId(idPaquete);
        }

        public int AltaPaquete(PaqueteBE paquete)
        {
            ValidarPaquete(paquete);

            int idPaquete = dal.AltaPaquete(paquete);

            dvBLL.ActualizarDVH("Paquete", "id_paquete", idPaquete);
            dvBLL.ActualizarDVV("Paquete");

            eventobll.RegistrarEvento("maestro_paquetes", "ev_alta_paquete", 2);

            return idPaquete;
        }

        private void ValidarPaquete(PaqueteBE paquete)
        {
            if (paquete == null) throw new Exception("El paquete no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(paquete.Descripcion))
                throw new Exception("La descripción es obligatoria.");

            if (paquete.Peso <= 0)
                throw new Exception("El peso debe ser mayor a cero.");

            if (paquete.Alto <= 0)
                throw new Exception("El alto debe ser mayor a cero.");

            if (paquete.Ancho <= 0)
                throw new Exception("El ancho debe ser mayor a cero.");

            if (paquete.Largo <= 0)
                throw new Exception("El largo debe ser mayor a cero.");
        }

        public void ModificarPaquete(PaqueteBE paquete)
        {
            if (paquete == null)
                throw new Exception("El paquete no puede ser nulo.");

            if (paquete.IdPaquete <= 0)
                throw new Exception("El paquete seleccionado no es válido.");

            if (string.IsNullOrWhiteSpace(paquete.Descripcion))
                throw new Exception("La descripción del paquete es obligatoria.");

            if (paquete.Peso <= 0)
                throw new Exception("El peso debe ser mayor a cero.");

            if (paquete.Alto <= 0)
                throw new Exception("El alto debe ser mayor a cero.");

            if (paquete.Ancho <= 0)
                throw new Exception("El ancho debe ser mayor a cero.");

            if (paquete.Largo <= 0)
                throw new Exception("El largo debe ser mayor a cero.");

            dal.ModificarPaquete(paquete);

            dvBLL.ActualizarDVH("Paquete", "id_paquete", paquete.IdPaquete);
            dvBLL.ActualizarDVV("Paquete");

            eventobll.RegistrarEvento("paquetes",  "ev_modificacion_paquete", 2);
        }
    }
}