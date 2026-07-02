using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Perfiles
{
    public class Familia : Componente
    {
        public int IdFamilia
        {
            get => Id;
            set => Id = value;
        }
        public override string ToString()
        {
            return Nombre;
        }
        private List<Componente> hijos = new();

        public override void Agregar(Componente componente)
        {
            hijos.Add(componente);
        }

        public override void Quitar(Componente componente)
        {
            hijos.Remove(componente);
        }

        public override IList<Componente> ObtenerHijos()
        {
            return hijos;
        }
    }
}
