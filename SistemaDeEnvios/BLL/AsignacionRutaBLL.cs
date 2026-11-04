using BE.Entidades;
using BLL.Otros;
using DAL;
using Servicios;
using Servicios.Sesión;
using System;
using System.Data;

namespace BLL
{
    public class AsignacionRutaBLL
    {
        private AsignacionRutaDAL dal = new AsignacionRutaDAL();
        private DVBLL dvBLL = new DVBLL();
        private EventoBLL eventobll = new EventoBLL();
        private IntentoEntregaBLL intentoEntregaBLL = new IntentoEntregaBLL();

        public DataTable ObtenerRepartidoresDisponibles()
        {
            return dal.ObtenerRepartidoresDisponibles();
        }

        public int AsignarRuta(int idRuta, int dniRepartidor)
        {
            if (idRuta <= 0)
                throw new Exception("Debe seleccionar una ruta válida.");

            if (dniRepartidor <= 0)
                throw new Exception("Debe seleccionar un repartidor válido.");

            string estadoRuta = new RutaBLL().ObtenerEstadoRuta(idRuta);

            if (estadoRuta == null)
                throw new Exception("La ruta seleccionada no existe.");

            if (estadoRuta != "Pendiente")
                throw new Exception("Solo se pueden asignar rutas que se encuentran pendientes.");

            DataTable repartidoresDisponibles = dal.ObtenerRepartidoresDisponibles();

            bool repartidorDisponible = false;

            foreach (DataRow fila in repartidoresDisponibles.Rows)
            {
                if (Convert.ToInt32(fila["dni"]) == dniRepartidor)
                {
                    repartidorDisponible = true;
                    break;
                }
            }

            if (!repartidorDisponible)
                throw new Exception("El repartidor seleccionado no está habilitado o ya tiene una ruta activa.");

            int idAsignacion = dal.AsignarRuta(idRuta, dniRepartidor);

            dvBLL.ActualizarTodosLosDVH("AsignacionRuta");
            dvBLL.ActualizarDVV("AsignacionRuta");
            dvBLL.ActualizarDVH("Ruta", "id_ruta", idRuta);
            dvBLL.ActualizarDVV("Ruta");

            eventobll.RegistrarEvento("envios", "ev_asignacion_ruta", 2);

            return idAsignacion;
        }
        public void QuitarAsignacion(int idRuta, string motivo)
        {
            if (idRuta <= 0)
                throw new Exception("El identificador de ruta es inválido.");

            if (string.IsNullOrWhiteSpace(motivo))
                throw new Exception("Debe ingresar el motivo de la desasignación.");

            if (motivo.Length > 250)
                throw new Exception("El motivo no puede superar los 250 caracteres.");

            int idAsignacion = dal.ObtenerIdAsignacionActiva(idRuta);

            dal.QuitarAsignacion(idRuta, motivo);

            GeneradorPDF.RenombrarHojaRutaComoCancelada(idRuta, idAsignacion);

            dvBLL.ActualizarTodosLosDVH("AsignacionRuta");
            dvBLL.ActualizarDVV("AsignacionRuta");
            dvBLL.ActualizarDVH("Ruta", "id_ruta", idRuta);
            dvBLL.ActualizarDVV("Ruta");

            eventobll.RegistrarEvento("envios", "ev_desasignacion_ruta", 2);
        }

        public DataTable ObtenerAsignaciones()
        {
            return dal.ObtenerAsignaciones();
        }
        public DataTable ObtenerTodosLosRepartidores()
        {
            return dal.ObtenerTodosLosRepartidores();
        }

        public void IniciarRuta(int idAsignacion)
        {
            ValidarAsignacionDelUsuario(idAsignacion);

            int idRuta = dal.ObtenerIdRutaPorAsignacion(idAsignacion);

            dal.IniciarRuta(idAsignacion);

            dvBLL.ActualizarDVH("AsignacionRuta", "id_asignacion", idAsignacion);
            dvBLL.ActualizarDVV("AsignacionRuta");

            dvBLL.ActualizarDVH("Ruta", "id_ruta", idRuta);
            dvBLL.ActualizarDVV("Ruta");

            eventobll.RegistrarEvento("entregas", "ev_inicio_ruta", 2);
        }

        public void FinalizarRuta(int idAsignacion)
        {
            ValidarAsignacionDelUsuario(idAsignacion);

            int idRuta = dal.ObtenerIdRutaPorAsignacion(idAsignacion);

            if (!intentoEntregaBLL.TodosLosEnviosTienenIntento(idRuta))
                throw new Exception("No se puede finalizar la ruta porque hay envíos sin registrar un resultado.");

            dal.FinalizarRuta(idAsignacion);

            dvBLL.ActualizarDVH("AsignacionRuta", "id_asignacion", idAsignacion);
            dvBLL.ActualizarDVV("AsignacionRuta");

            dvBLL.ActualizarDVH("Ruta", "id_ruta", idRuta);
            dvBLL.ActualizarDVV("Ruta");

            eventobll.RegistrarEvento("entregas", "ev_fin_ruta", 2);
        }

        public int ObtenerMiAsignacionActiva()
        {
            Usuario usuario = SesionUsuario.GetInstancia().UsuarioActual;

            if (usuario == null)
                throw new Exception("No hay un usuario logueado.");

            return dal.ObtenerIdAsignacionActivaPorRepartidor(usuario.DNI);
        }

        private void ValidarAsignacionDelUsuario(int idAsignacion)
        {
            Usuario usuario = SesionUsuario.GetInstancia().UsuarioActual;

            if (usuario == null)
                throw new Exception("No hay un usuario logueado.");

            if (idAsignacion <= 0)
                throw new Exception("La asignación seleccionada no es válida.");

            if (!dal.AsignacionPerteneceARepartidor(idAsignacion, usuario.DNI))
                throw new Exception("La asignación no pertenece al repartidor logueado o ya no se encuentra activa.");
        }

        public DataTable ObtenerDatosAsignacion(int idAsignacion)
        {
            if (idAsignacion <= 0)
                throw new Exception("El identificador de asignación no es válido.");

            return dal.ObtenerDatosAsignacion(idAsignacion);
        }
    }
}