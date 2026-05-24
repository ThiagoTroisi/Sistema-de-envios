using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entidades
{
    public class Bitacora
    {
        public int IdBitacora { get; set; }
        public int IdUsuario { get; set; }
        public string Accion { get; set; }
        public string Modulo { get; set; }
        public string Detalle { get; set; }
        public DateTime Fecha { get; set; }
    }
}
