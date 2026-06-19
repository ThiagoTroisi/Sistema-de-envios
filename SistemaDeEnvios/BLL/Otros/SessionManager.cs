using BE.Entidades;
using BLL.Otros;
using DAL;
using Servicios;
using Servicios.GestionIdiomas;
using Servicios.Sesión;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Gestores
{
    public class SessionManager
    {
        private UsuarioDAL dal = new UsuarioDAL();
        public ResultadoOperacion IniciarSesion(string mail, string contra)
        {
            ResultadoOperacion rl = new ResultadoOperacion(false, "");
            if (SesionUsuario.GetInstancia().UsuarioActual != null) { rl.Mensaje = "sesion iniciada"; return rl; }
            Usuario u = dal.ConsultaPorMail(mail);
            if (u == null) { rl.Mensaje = "email no encontrado"; return rl; }
            if (!u.Estado) { rl.Mensaje = "usuario inactivo"; return rl; }
            if (u.Bloqueado) { rl.Mensaje = "usuario bloqueado"; return rl; }
            bool contraseñaok = Encriptador.VerificarContraseña(contra, u.Contraseña);
            if (!contraseñaok)
            {
                dal.IncrementarIntentos(u.DNI);

                u.IntentosFallidos++;

                if (u.IntentosFallidos >= 3)
                {
                    dal.BloquearUsuario(u.DNI);

                    rl.Mensaje = "usuario bloqueado por intentos";
                    return rl;
                }

                rl.Mensaje = "contraseña incorrecta";
                rl.Parametros = new object[] { u.IntentosFallidos };
                return rl;
            }

            dal.ReiniciarIntentos(u.DNI);

            rl.Exitoso = true;
            rl.Mensaje = "login exitoso";
            rl.Usuario = u;
            return rl;
        }
    }
}
