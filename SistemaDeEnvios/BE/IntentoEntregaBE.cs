using System;

namespace BE
{
    public class IntentoEntregaBE
    {
        public int IdIntento { get; set; }
        public int IdRutaEnvio { get; set; }
        public DateTime FechaIntento { get; set; }
        public string Resultado { get; set; }
        public string TipoInconveniente { get; set; }
        public string Observaciones { get; set; }
        public string Dvh { get; set; }

        public IntentoEntregaBE()
        {
        }

        public IntentoEntregaBE(int idIntento, int idRutaEnvio, DateTime fechaIntento, string resultado, string tipoInconveniente, string observaciones, string dvh)
        {
            IdIntento = idIntento;
            IdRutaEnvio = idRutaEnvio;
            FechaIntento = fechaIntento;
            Resultado = resultado;
            TipoInconveniente = tipoInconveniente;
            Observaciones = observaciones;
            Dvh = dvh;
        }

        public IntentoEntregaBE(int idRutaEnvio, string resultado, string tipoInconveniente, string observaciones)
        {
            IdRutaEnvio = idRutaEnvio;
            Resultado = resultado;
            TipoInconveniente = tipoInconveniente;
            Observaciones = observaciones;
        }
    }
}