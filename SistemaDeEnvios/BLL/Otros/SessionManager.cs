using BE.Entidades;
using BLL.Otros;
using DAL.Repositorios;
using Servicios;
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
            ResultadoOperacion rl = new ResultadoOperacion(false, "", null); ;
            Usuario u = dal.ConsultaPorMail(mail);
            if (u == null) { rl.Mensaje = "ERROR: Email no encontrado."; return rl; }
            if (!u.Estado) { rl.Mensaje = "ERROR: Usuario inactivo."; return rl; }
            if (u.Bloqueado) { rl.Mensaje = "ERROR: Usuario bloqueado."; return rl; }
            bool contraseñaok = Encriptador.VerificarContraseña(contra, u.Contraseña);
            if (!contraseñaok)
            {
                dal.IncrementarIntentos(u.DNI);

                u.IntentosFallidos++;

                if (u.IntentosFallidos >= 3)
                {
                    dal.BloquearUsuario(u.DNI);

                    rl.Mensaje = "ERROR: Usuario bloqueado por demasiados intentos fallidos.";
                    return rl;
                }

                rl.Mensaje = $"ERROR: Contraseña incorrecta. Intento {u.IntentosFallidos} de 3";
                return rl;
            }

            dal.ReiniciarIntentos(u.DNI);

            rl.Exitoso = true;
            rl.Mensaje = "Login exitoso.";
            rl.Usuario = u;
            return rl;
        }
    }
}
