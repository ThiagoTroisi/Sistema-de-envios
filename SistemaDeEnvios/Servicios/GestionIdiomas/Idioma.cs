using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.GestionIdiomas
{
    public class Idioma
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public Idioma(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
    }
}
