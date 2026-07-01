using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Perfiles
{
    public class Permiso : Componente
    {
        public int IdPermiso
        {
            get => Id;
            set => Id = value;
        }
        public string Nombre { get; set; }
        public override string ToString()
        {
            return Nombre;
        }
    }
}
