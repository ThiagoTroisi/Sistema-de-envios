using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Perfiles
{
    public abstract class Componente
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public virtual void Agregar(Componente componente)
    {
        throw new NotSupportedException();
    }

    public virtual void Quitar(Componente componente)
    {
        throw new NotSupportedException();
    }

    public virtual IList<Componente> ObtenerHijos()
    {
        return new List<Componente>();
    }
}
}