using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entidades
{
    public class Pago
    {
        public int IdPago { get; set; }
        public int IdEnvio { get; set; }
        public string MedioPago { get; set; }
        public string Titular { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
    }
}
