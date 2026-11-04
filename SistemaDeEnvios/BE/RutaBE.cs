using System;

namespace BE
{
    public class RutaBE
    {
        public int IdRuta { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estado { get; set; }

        public RutaBE()
        {
        }

        public RutaBE(int idRuta, string descripcion, DateTime fechaCreacion, string estado)
        {
            IdRuta = idRuta;
            Descripcion = descripcion;
            FechaCreacion = fechaCreacion;
            Estado = estado;
        }

        public RutaBE(string descripcion)
        {
            Descripcion = descripcion;
        }
    }
}