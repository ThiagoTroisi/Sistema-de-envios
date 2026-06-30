using Servicios.GestionIdiomas;
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
        public int IdPerfil { get; set; }
        public bool Bloqueado { get; set; }
        public bool Estado { get; set; }
        public int IntentosFallidos { get; set; }
        public int IdIdioma { get; set; }
        public Usuario(int dni, string nombre, string apellido, string email, string contraseña, int idperfil, bool bloqueado, bool estado, int intentosFallidos, int ididioma)
        {
            DNI = dni;
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            Contraseña = contraseña;
            Estado = estado;
            IdPerfil = idperfil;
            Bloqueado = bloqueado;
            IntentosFallidos = intentosFallidos;
            IdIdioma = ididioma;
        }
    }
}
