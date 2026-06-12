using BE.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Sesión
{
    public class SesionUsuario
    {
        private static SesionUsuario instancia;
        private SesionUsuario() { }
        public static SesionUsuario GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new SesionUsuario();
            }
            return instancia;
        }
        public Usuario UsuarioActual { get; private set; }
        public void Iniciar(Usuario u)
        {
            UsuarioActual = u;
        }
        public void Cerrar()
        {
            UsuarioActual = null;
        }
    }
}
