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
        public Pago(int idPago, int idEnvio, string MedioPago, string Titular, decimal Monto, DateTime fecha)
        {
            this.IdPago = idPago;
            this.IdEnvio = idEnvio;
            this.MedioPago = MedioPago;
            this.Titular = Titular;
            this.Monto = Monto;
            this.FechaPago = fecha;
        }
    }
}
