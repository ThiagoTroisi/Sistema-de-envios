using BE.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Otros
{
    public class ResultadoLogin
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
        public Usuario Usuario { get; set; }
        public ResultadoLogin(bool exitoso, string mensaje, Usuario usuario)        {
            Exitoso = exitoso;
            Mensaje = mensaje;
            Usuario = usuario;
        }

    }
}
