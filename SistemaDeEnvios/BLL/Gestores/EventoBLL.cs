using BE.Entidades;
using DAL.Repositorios;
using Servicios.Sesión;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Gestores
{
    public class EventoBLL
    {
        private EventoDAL dal = new EventoDAL();

        public void RegistrarEvento(string modulo, string descripcion, int criticidad)
        {
            int dni = SesionUsuario.GetInstancia().UsuarioActual.DNI;

            Evento e = new Evento(dni, modulo, descripcion, criticidad);

            dal.RegistrarEvento(e);
        }
    }
}
