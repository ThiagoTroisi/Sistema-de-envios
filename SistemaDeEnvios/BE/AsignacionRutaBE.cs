using System;

namespace BE
{
    public class AsignacionRutaBE
    {
        public int IdAsignacion { get; set; }
        public int IdRuta { get; set; }
        public int DniRepartidor { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Estado { get; set; }
        public string MotivoCancelacion { get; set; }
        public int? DniGestorCancelacion { get; set; }

        public AsignacionRutaBE()
        {
        }

        public AsignacionRutaBE(int idAsignacion, int idRuta, int dniRepartidor, DateTime fechaAsignacion, DateTime? fechaInicio, DateTime? fechaFin, string estado, string motivoCancelacion, int? dniGestorCancelacion)
        {
            IdAsignacion = idAsignacion;
            IdRuta = idRuta;
            DniRepartidor = dniRepartidor;
            FechaAsignacion = fechaAsignacion;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Estado = estado;
            MotivoCancelacion = motivoCancelacion;
            DniGestorCancelacion = dniGestorCancelacion;
        }

        public AsignacionRutaBE(int idRuta, int dniRepartidor)
        {
            IdRuta = idRuta;
            DniRepartidor = dniRepartidor;
        }
    }
}