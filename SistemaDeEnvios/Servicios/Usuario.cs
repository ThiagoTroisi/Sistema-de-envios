using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entidades
{
    public class Usuario
    {
        public int DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public int IdRol { get; set; }
        public bool Bloqueado { get; set; }
        public bool Estado { get; set; }
        public int IntentosFallidos { get; set; }
        public Usuario(int dni, string nombre, string apellido, string email, string contraseña, int idRol, bool bloqueado, bool estado, int intentosFallidos)
        {
            DNI = dni;
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            Contraseña = contraseña;
            Estado = estado;
            IdRol = idRol;
            Bloqueado = bloqueado;
            IntentosFallidos = intentosFallidos;
        }
    }
}
