using BE;
using DAL;
using BLL.Otros;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;

namespace BLL
{
    public class RutaBLL
    {
        private RutaDAL dal = new RutaDAL();
        private DVBLL dvBLL = new DVBLL();
        private EventoBLL eventobll = new EventoBLL();

        private void ValidarIdsEnvios(List<int> idsEnvios)
        {
            if (idsEnvios == null || idsEnvios.Count == 0)
                throw new Exception("Debe seleccionar al menos un envío.");

            if (idsEnvios.Count != new HashSet<int>(idsEnvios).Count)
                throw new Exception("La selección contiene envíos duplicados.");

            foreach (int idEnvio in idsEnvios)
            {
                if (idEnvio <= 0)
                    throw new Exception("La selección contiene un identificador de envío inválido.");
            }
        }

        public int CrearRuta(RutaBE ruta, List<int> idsEnvios)
        {
            if (ruta == null)
                throw new Exception("Debe proporcionar los datos de la ruta.");

            ValidarIdsEnvios(idsEnvios);

            if (!string.IsNullOrWhiteSpace(ruta.Descripcion) &&
                ruta.Descripcion.Length > 150)
            {
                throw new Exception("La descripción no puede superar los 150 caracteres.");
            }

            int idRuta = dal.CrearRuta(ruta, idsEnvios);

            dvBLL.ActualizarDVH("Ruta", "id_ruta", idRuta);
            dvBLL.ActualizarDVV("Ruta");

            dvBLL.ActualizarTodosLosDVH("Ruta_Envio");
            dvBLL.ActualizarDVV("Ruta_Envio");

            eventobll.RegistrarEvento("envios", "ev_creacion_ruta", 2);

            return idRuta;
        }

        public DataTable ObtenerEnviosPendientesDistribucion()
        {
            return dal.ObtenerEnviosPendientesDistribucion();
        }

        public DataTable ObtenerRutasSinIniciar()
        {
            return dal.ObtenerRutasSinIniciar();
        }
        public DataTable ObtenerRutasPendientesAsignacion()
        {
            return dal.ObtenerRutasPendientesAsignacion();
        }

        public DataTable ObtenerTodasLasRutas()
        {
            return dal.ObtenerTodasLasRutas();
        }

        public DataTable ObtenerEnviosDeRuta(int idRuta)
        {
            if (idRuta <= 0)
                throw new Exception("El identificador de la ruta no es válido.");

            return dal.ObtenerEnviosDeRuta(idRuta);
        }

        public void AgregarEnviosARuta(int idRuta, List<int> idsEnvios)
        {
            if (idRuta <= 0)
                throw new Exception("El identificador de la ruta no es válido.");

            ValidarIdsEnvios(idsEnvios);
            ValidarRutaModificable(idRuta);
            dal.AgregarEnviosARuta(idRuta, idsEnvios);

            dvBLL.ActualizarTodosLosDVH("Ruta_Envio");
            dvBLL.ActualizarDVV("Ruta_Envio");

            eventobll.RegistrarEvento("envios", "ev_agregar_envios_ruta", 2);
        }

        public void QuitarEnviosDeRuta(int idRuta, List<int> idsEnvios)
        {
            if (idRuta <= 0)
                throw new Exception("El identificador de la ruta no es válido.");

            ValidarIdsEnvios(idsEnvios);
            ValidarRutaModificable(idRuta);
            dal.QuitarEnviosDeRuta(idRuta, idsEnvios);

            dvBLL.ActualizarTodosLosDVH("Ruta_Envio");
            dvBLL.ActualizarDVV("Ruta_Envio");

            dvBLL.ActualizarDVH("Ruta", "id_ruta", idRuta);
            dvBLL.ActualizarDVV("Ruta");

            dvBLL.ActualizarTodosLosDVH("AsignacionRuta");
            dvBLL.ActualizarDVV("AsignacionRuta");

            eventobll.RegistrarEvento("envios", "ev_quitar_envios_ruta", 2);
        }

        public void CancelarRuta(int idRuta)
        {
            if (idRuta <= 0)
                throw new Exception("El identificador de la ruta no es válido.");

            dal.CancelarRuta(idRuta);

            dvBLL.ActualizarDVH("Ruta", "id_ruta", idRuta);
            dvBLL.ActualizarDVV("Ruta");

            dvBLL.ActualizarTodosLosDVH("AsignacionRuta");
            dvBLL.ActualizarDVV("AsignacionRuta");

            eventobll.RegistrarEvento("envios", "ev_cancelacion_ruta", 2);
        }
        public string ObtenerEstadoRuta(int idRuta)
        {
            return dal.ObtenerEstadoRuta(idRuta);
        }
        private void ValidarRutaModificable(int idRuta)
        {
            if (idRuta <= 0)
            {
                throw new Exception("El identificador de la ruta no es válido.");
            }

            string estadoRuta = ObtenerEstadoRuta(idRuta);

            if (estadoRuta == null)
            {
                throw new Exception("La ruta seleccionada no existe.");
            }

            if (estadoRuta == "Asignada")
            {
                throw new Exception("La ruta se encuentra asignada a un repartidor. Para modificarla, primero debe quitar la asignación.");
            }

            if (estadoRuta != "Pendiente")
            {
                throw new Exception("La ruta no se puede modificar porque no se encuentra pendiente.");
            }
        }

        public void SubirEnvio(int idRuta, int idRutaEnvio)
        {
            if (idRutaEnvio <= 0)
            {
                throw new Exception("El identificador del envío no es válido.");
            }

            ValidarRutaModificable(idRuta);

            dal.MoverEnvio(idRuta, idRutaEnvio, true, "Pendiente");

            dvBLL.ActualizarTodosLosDVH("Ruta_Envio");
            dvBLL.ActualizarDVV("Ruta_Envio");

            eventobll.RegistrarEvento("envios", "ev_modificacion_orden_ruta", 2);
        }

        public void BajarEnvio(int idRuta, int idRutaEnvio)
        {
            if (idRutaEnvio <= 0)
            {
                throw new Exception("El identificador del envío no es válido.");
            }

            ValidarRutaModificable(idRuta);

            dal.MoverEnvio(idRuta, idRutaEnvio, false, "Pendiente");

            dvBLL.ActualizarTodosLosDVH("Ruta_Envio");
            dvBLL.ActualizarDVV("Ruta_Envio");

            eventobll.RegistrarEvento("envios", "ev_modificacion_orden_ruta", 2);
        }

        public string GenerarHojaRuta(int idAsignacion, bool abrir = true)
        {
            if (idAsignacion <= 0)
                throw new Exception("El identificador de la asignación no es válido.");

            DataTable datosAsignacion = dal.ObtenerDatosHojaRuta(idAsignacion);

            if (datosAsignacion.Rows.Count == 0)
                throw new Exception("No se encontró la asignación solicitada.");

            DataRow asignacion = datosAsignacion.Rows[0];

            string estadoAsignacion = asignacion["estado_asignacion"].ToString();
            string estadoRuta = asignacion["estado_ruta"].ToString();

            if (estadoAsignacion != "Activa")
                throw new Exception("La asignación ya no se encuentra activa.");

            if (estadoRuta != "Asignada" && estadoRuta != "En curso")
                throw new Exception("La ruta no se encuentra habilitada para generar la hoja de ruta.");

            int idRuta = Convert.ToInt32(asignacion["id_ruta"]);

            DataTable envios = dal.ObtenerEnviosDeRuta(idRuta);

            if (envios.Rows.Count == 0)
                throw new Exception("La ruta no contiene envíos para incluir en la hoja de ruta.");

            return GeneradorPDF.GenerarHojaRuta(datosAsignacion, envios, abrir);
        }
    }
}