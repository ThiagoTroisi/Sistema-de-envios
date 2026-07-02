using BE.Entidades;
using DAL;
using Servicios.Sesión;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class EventoBLL
    {
        EventoDAL dal = new EventoDAL();
        DVBLL dvbll = new DVBLL();

        public void RegistrarEvento(string modulo, string descripcion, int criticidad)
        {
            int dni = SesionUsuario.GetInstancia().UsuarioActual.DNI;

            Evento e = new Evento(dni, modulo, descripcion, criticidad);

            int idEvento = dal.RegistrarEvento(e);
            dvbll.ActualizarDVH("Evento", "id_evento", idEvento);
            dvbll.ActualizarDVV("Evento");
        }
        public DataTable ObtenerTodosLosEventos()
        {
            return dal.ObtenerTodosLosEventos();
        }
        public DataTable ObtenerEmails()
        {
            return dal.ObtenerColumna("email");
        }

        public DataTable ObtenerEventos()
        {
            return dal.ObtenerColumna("evento");
        }

        public DataTable ObtenerModulos()
        {
            return dal.ObtenerColumna("modulo");
        }
        public DataTable FiltrarEventos(string email, DateTime? desde, DateTime? hasta, string modulo, string evento, int? criticidad)
        {
            return dal.FiltrarEventos(email, desde, hasta, modulo, evento, criticidad);
        }
    }
}
