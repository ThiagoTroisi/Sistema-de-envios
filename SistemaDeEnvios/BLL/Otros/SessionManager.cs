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
        private UsuarioBLL usuariobll = new UsuarioBLL();
        private IdiomaBLL idiomabll = new IdiomaBLL();
        private EventoBLL eventobll = new EventoBLL();
        public ResultadoOperacion IniciarSesion(string mail, string contra)
        {
            ResultadoOperacion rl = new ResultadoOperacion(false, "");
            if (SesionUsuario.GetInstancia().UsuarioActual != null) { rl.Mensaje = "sesion iniciada"; return rl; }
            Usuario u = usuariobll.ConsultaPorMail(mail);
            if (u == null) { rl.Mensaje = "email no encontrado"; return rl; }
            if (!u.Estado) { rl.Mensaje = "usuario inactivo"; return rl; }
            if (u.Bloqueado) { rl.Mensaje = "usuario bloqueado"; return rl; }
            try
            {
                bool contraseñaok = Encriptador.Verificar(contra, u.Contraseña);
                if (!contraseñaok)
                {
                    usuariobll.IncrementarIntentos(u.DNI);

                    u.IntentosFallidos++;

                    if (u.IntentosFallidos >= 3)
                    {
                        usuariobll.BloquearUsuario(u.DNI);

                        rl.Mensaje = "usuario bloqueado por intentos";
                        return rl;
                    }

                    rl.Mensaje = "contraseña incorrecta";
                    rl.Parametros = new object[] { u.IntentosFallidos };
                    return rl;
                }

                rl.Exitoso = true;
                rl.Mensaje = "login exitoso";
                rl.Usuario = u;

                SesionUsuario.GetInstancia().Iniciar(rl.Usuario);
                Idioma idioma = idiomabll.ObtenerPorId(rl.Usuario.IdIdioma);
                GestorIdiomas.Instancia.CambiarIdioma(idioma);
                return rl;
            }
            catch (BCrypt.Net.SaltParseException) { rl.Mensaje = "error usuario bd"; return rl; }
        }
        public void ContinuarLogin(string mail)
        {
            Usuario u = usuariobll.ConsultaPorMail(mail);
            usuariobll.ReiniciarIntentos(u.DNI);
            eventobll.RegistrarEvento("mod_usuarios", "ev_login", 1);
        }
        public void CerrarSesion()
        {
            Usuario usuario = SesionUsuario.GetInstancia().UsuarioActual;
            usuariobll.ActualizarIdioma(usuario.DNI, GestorIdiomas.Instancia.IdiomaActual.Id);
            eventobll.RegistrarEvento("mod_usuarios", "ev_logout", 1);
            SesionUsuario.GetInstancia().Cerrar();
        }
    }
}
