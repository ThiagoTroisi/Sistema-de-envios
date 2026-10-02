using System;

namespace BE
{
    public class FacturaBE
    {
        public int IdFactura { get; set; }
        public int IdEnvio { get; set; }
        public int IdPago { get; set; }
        public int DniCliente { get; set; }
        public decimal Importe { get; set; }
        public DateTime Fecha { get; set; }
        public string Dvh { get; set; }

        public FacturaBE() { }

        public FacturaBE(int idFactura, int idEnvio, int idPago, int dniCliente, decimal importe, DateTime fecha)
        {
            IdFactura = idFactura;
            IdEnvio = idEnvio;
            IdPago = idPago;
            DniCliente = dniCliente;
            Importe = importe;
            Fecha = fecha;
        }

        public FacturaBE(int idEnvio, int idPago, int dniCliente, decimal importe)
        {
            IdEnvio = idEnvio;
            IdPago = idPago;
            DniCliente = dniCliente;
            Importe = importe;
        }
    }
}