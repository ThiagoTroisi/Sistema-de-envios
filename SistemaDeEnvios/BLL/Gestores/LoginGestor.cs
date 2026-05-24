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
        private UsuarioRep rep = new UsuarioRep();
        public ResultadoLogin IniciarSesion(string mail, string contra)
        {
            ResultadoLogin rl = new ResultadoLogin(false, "", null); ;
            Usuario u = rep.ConsultaPorMail(mail);
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
