using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Servicios.Perfiles
{
    public class Perfil
    {
        public int IdPerfil { get; set; }
        public string Nombre { get; set; }
        public override string ToString()
        {
            return Nombre;
        }
        public List<Componente> Componentes { get; } = new();
        public void Agregar(Componente componente)
        {
            Componentes.Add(componente);
        }
    }
}
