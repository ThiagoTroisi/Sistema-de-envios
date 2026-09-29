using System;

namespace BE
{
    public class EnvioBE
    {
        public int IdEnvio { get; set; }
        public string CodigoSeguimiento { get; set; }
        public string Origen { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Estado { get; set; }
        public int IdPaquete { get; set; }
        public int IdDestino { get; set; }
        public int IdRemitente { get; set; }
        public int IdDestinatario { get; set; }

        public EnvioBE(int idEnvio, string codigoSeguimiento, string origen, DateTime fechaRegistro, string estado, int idPaquete, int idDestino, int idRemitente, int idDestinatario)
        {
            IdEnvio = idEnvio;
            CodigoSeguimiento = codigoSeguimiento;
            Origen = origen;
            FechaRegistro = fechaRegistro;
            Estado = estado;
            IdPaquete = idPaquete;
            IdDestino = idDestino;
            IdRemitente = idRemitente;
            IdDestinatario = idDestinatario;
        }

        public EnvioBE(string origen, int idPaquete, int idDestino, int idRemitente, int idDestinatario)
        {
            Origen = origen;
            IdPaquete = idPaquete;
            IdDestino = idDestino;
            IdRemitente = idRemitente;
            IdDestinatario = idDestinatario;
        }

        public EnvioBE() { }
    }
}