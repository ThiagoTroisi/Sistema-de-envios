using BE.Entidades;
using BLL.Otros;
using DAL.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Gestores
{
    public class LoginGestor
    {
        private UsuarioDAL dal = new UsuarioDAL();
        public ResultadoOperacion IniciarSesion(string mail, string contra)
        {
            ResultadoOperacion rl = new ResultadoOperacion(false, "", null); ;
            Usuario u = dal.ConsultaPorMail(mail);
            if (u == null) { rl.Mensaje = "ERROR: Email no encontrado."; return rl; }
            if (!u.Estado) { rl.Mensaje = "ERROR: Usuario bloqueado."; return rl; }
            bool contraseñaok = BCrypt.Net.BCrypt.Verify(contra, u.Contraseña);
            if (!contraseñaok) { rl.Mensaje = "ERROR: Contraseña incorrecta."; return rl; }

            rl.Exitoso = true;
            rl.Mensaje = "Login exitoso.";
            rl.Usuario = u;
            return rl;
        }
    }
}
