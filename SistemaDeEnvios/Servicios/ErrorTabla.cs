using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class ErrorTabla
    {
        public string Tabla { get; set; }
        public List<ErrorIntegridad> Errores { get; set; } = new();
        public int CantidadErrores
        {
            get => Errores.Count;
        }
        public override string ToString()
        {
            return $"{Tabla} ({CantidadErrores})";
        }
    }
}
