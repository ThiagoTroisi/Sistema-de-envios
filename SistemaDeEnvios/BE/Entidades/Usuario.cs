using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entidades
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public int DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public bool Estado { get; set; }
        public int IdRol { get; set; }
        public Usuario(int idUsuario, int dni, string nombre, string apellido, string email, string contraseña, bool estado, int idRol)
        {
            IdUsuario = idUsuario;
            DNI = dni;
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            Contraseña = contraseña;
            Estado = estado;
            IdRol = idRol;
        }
        public Usuario(int dni, string nombre, string apellido, string email, string contraseña, bool estado, int idRol)
        {
            DNI = dni;
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            Contraseña = contraseña;
            Estado = estado;
            IdRol = idRol;
        }
    }
}
