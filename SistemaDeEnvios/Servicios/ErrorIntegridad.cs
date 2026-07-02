using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class ErrorIntegridad
    {
        public string Tipo { get; set; }
        public string Registro { get; set; }
        public string ValorGuardado { get; set; }
        public string ValorCalculado { get; set; }
    }
}
