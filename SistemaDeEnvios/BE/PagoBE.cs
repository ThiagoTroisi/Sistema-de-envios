using System;

namespace BE
{
    public class PagoBE
    {
        public int IdPago { get; set; }
        public int IdEnvio { get; set; }
        public int DniCliente { get; set; }
        public decimal Importe { get; set; }
        public DateTime FechaPago { get; set; }
        public string Estado { get; set; }

        public PagoBE()
        {
        }

        public PagoBE(int idPago, int idEnvio, int dniCliente, decimal importe, DateTime fechaPago, string estado)
        {
            IdPago = idPago;
            IdEnvio = idEnvio;
            DniCliente = dniCliente;
            Importe = importe;
            FechaPago = fechaPago;
            Estado = estado;
        }

        public PagoBE(int idEnvio, int dniCliente, decimal importe)
        {
            IdEnvio = idEnvio;
            DniCliente = dniCliente;
            Importe = importe;
        }
    }
}