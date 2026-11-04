using BE;
using BE.Entidades;
using DAL;
using BLL.Otros;
using Servicios;
using Servicios.Sesión;
using System;
using System.Data;

namespace BLL
{
    public class IntentoEntregaBLL
    {
        private readonly IntentoEntregaDAL dal = new IntentoEntregaDAL();
        private readonly AsignacionRutaDAL asignacionRutaDAL = new AsignacionRutaDAL();
        private readonly DVBLL dvBLL = new DVBLL();
        private readonly EventoBLL eventobll = new EventoBLL();

        public int RegistrarIntento(int idRuta, IntentoEntregaBE intento)
        {
            if (intento == null)
                throw new ArgumentNullException(nameof(intento));

            if (idRuta <= 0)
                throw new Exception("El identificador de la ruta no es válido.");

            if (intento.IdRutaEnvio <= 0)
                throw new Exception("El envío seleccionado no es válido.");

            Usuario usuario = SesionUsuario.GetInstancia().UsuarioActual;

            if (usuario == null)
                throw new Exception("No hay un usuario logueado.");

            int idAsignacion = asignacionRutaDAL.ObtenerIdAsignacionActivaPorRepartidor(usuario.DNI);

            if (idAsignacion <= 0)
                throw new Exception("El repartidor no tiene una asignación activa.");

            int idRutaAsignada = asignacionRutaDAL.ObtenerIdRutaPorAsignacion(idAsignacion);

            if (idRutaAsignada != idRuta)
                throw new Exception("La ruta seleccionada no corresponde al repartidor logueado.");

            if (!dal.RutaEnCurso(idRuta))
                throw new Exception("La ruta no se encuentra en curso.");

            if (!dal.EnvioPerteneceARuta(intento.IdRutaEnvio, idRuta))
                throw new Exception("El envío seleccionado no pertenece a esta ruta.");

            if (intento.Resultado != "Entregado" && intento.Resultado != "Fallido")
                throw new Exception("El resultado del intento no es válido.");

            if (intento.Resultado == "Fallido" && string.IsNullOrWhiteSpace(intento.TipoInconveniente))
                throw new Exception("Debe indicar el motivo por el cual no se pudo entregar el envío.");

            if (intento.TipoInconveniente != null && intento.TipoInconveniente.Length > 150)
                throw new Exception("El motivo del inconveniente no puede superar los 150 caracteres.");

            if (intento.Observaciones != null && intento.Observaciones.Length > 500)
                throw new Exception("Las observaciones no pueden superar los 500 caracteres.");

            if (dal.EnvioEntregado(intento.IdRutaEnvio))
                throw new Exception("Este envío ya fue entregado y no admite nuevos intentos.");

            int idEnvio = dal.ObtenerIdEnvioPorRutaEnvio(intento.IdRutaEnvio);

            int idIntento = dal.RegistrarIntento(intento);

            dvBLL.ActualizarDVH("IntentoEntrega", "id_intento", idIntento);
            dvBLL.ActualizarDVV("IntentoEntrega");

            dvBLL.ActualizarDVH("Envio", "id_envio", idEnvio);
            dvBLL.ActualizarDVV("Envio");

            eventobll.RegistrarEvento("entregas", "ev_registro_intento_entrega", 2);

            return idIntento;
        }

        public DataTable ObtenerIntentosPorEnvio(int idEnvio)
        {
            if (idEnvio <= 0)
                throw new Exception("El identificador del envío no es válido.");

            return dal.ObtenerIntentosPorEnvio(idEnvio);
        }

        public DataTable ObtenerIntentosPorRuta(int idRuta)
        {
            if (idRuta <= 0)
                throw new Exception("El identificador de la ruta no es válido.");

            return dal.ObtenerIntentosPorRuta(idRuta);
        }

        public DataTable ObtenerResumenIntentosPorRuta(int idRuta)
        {
            if (idRuta <= 0)
                throw new Exception("El identificador de la ruta no es válido.");

            return dal.ObtenerResumenIntentosPorRuta(idRuta);
        }

        public bool TodosLosEnviosTienenIntento(int idRuta)
        {
            if (idRuta <= 0)
                throw new Exception("El identificador de la ruta no es válido.");

            return dal.TodosLosEnviosTienenIntento(idRuta);
        }

        public int ObtenerCantidadEnviosPendientesResolucion()
        {
            return dal.ObtenerCantidadEnviosPendientesResolucion();
        }

        public DataTable ObtenerEnviosPendientesResolucion()
        {
            return dal.ObtenerEnviosPendientesResolucion();
        }

        public void AutorizarNuevoIntento(int idEnvio)
        {
            if (idEnvio <= 0)
                throw new Exception("El identificador del envío no es válido.");

            dal.AutorizarNuevoIntento(idEnvio);

            dvBLL.ActualizarDVH("Envio", "id_envio", idEnvio);
            dvBLL.ActualizarDVV("Envio");

            eventobll.RegistrarEvento("entregas", "ev_autorizacion_nuevo_ciclo_intentos", 2);
        }

        public void DevolverEnvioPorIntentos(int idEnvio)
        {
            if (idEnvio <= 0)
                throw new Exception("El identificador del envío no es válido.");

            dal.DevolverEnvioPorIntentos(idEnvio);

            dvBLL.ActualizarDVH("Envio", "id_envio", idEnvio);
            dvBLL.ActualizarDVV("Envio");

            eventobll.RegistrarEvento("entregas", "ev_cancelacion_envio_por_intentos", 2);
        }
    }
}