using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entidades
{
    public class Evento
    {
        public int IdEvento { get; set; }
        public int DNIUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public string Modulo { get; set; }
        public string evento { get; set; }
        public int Criticidad { get; set; }
        public Evento(int idevento, int dni, DateTime fecha, string modulo, string ev, int crit)
        {
            IdEvento = idevento;
            DNIUsuario = dni;
            Fecha = fecha;
            Modulo = modulo;
            evento = ev;
            Criticidad = crit;
        }
    }
}
