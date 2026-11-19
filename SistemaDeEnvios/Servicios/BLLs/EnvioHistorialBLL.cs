using BLL;
using Servicios.DALs;
using System;
using System.Data;

namespace Servicios.BLLs
{
    public class EnvioHistorialBLL
    {
        private EnvioHistorialDAL dal = new EnvioHistorialDAL();
        private EventoBLL eventoBLL = new EventoBLL();
        private DVBLL dvBLL = new DVBLL();

        public DataTable ObtenerTodos()
        {
            return dal.ObtenerTodos();
        }

        public DataTable ObtenerCodigosSeguimiento()
        {
            return dal.ObtenerCodigosSeguimiento();
        }

        public DataTable ObtenerRemitentes()
        {
            return dal.ObtenerRemitentes();
        }

        public DataTable Filtrar(string codigo, int? dniRemitente, DateTime? desde, DateTime? hasta)
        {
            if (desde.HasValue && hasta.HasValue && desde.Value.Date >= hasta.Value.Date)
                throw new Exception("El rango de fechas seleccionado no es válido.");

            return dal.Filtrar(codigo, dniRemitente, desde, hasta);
        }

        public void ActivarVersion(int idVersion)
        {
            if (idVersion <= 0)
                throw new Exception("La versión seleccionada no es válida.");

            DataTable dt = dal.ObtenerVersion(idVersion);

            if (dt.Rows.Count == 0)
                throw new Exception("La versión seleccionada no existe.");

            int idEnvio = Convert.ToInt32(dt.Rows[0]["id_envio"]);
            bool activa = Convert.ToBoolean(dt.Rows[0]["Act"]);

            if (activa)
                throw new Exception("La versión seleccionada ya está activa.");

            dal.ActivarVersion(idVersion, idEnvio);

            dvBLL.ActualizarTodosLosDVH("Envio");
            dvBLL.ActualizarTodosLosDVH("Paquete");
            dvBLL.ActualizarTodosLosDVH("Destino");
            dvBLL.ActualizarTodosLosDVH("Envio_C");

            eventoBLL.RegistrarEvento("Envio", "ev_restauracion_envio", 2);
        }
    }
}