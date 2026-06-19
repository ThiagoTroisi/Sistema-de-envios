using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.GestionIdiomas
{
    public class GestorIdiomas
    {
        private static GestorIdiomas instancia;
        private List<IObserverIdioma> observadores = new List<IObserverIdioma>();
        public Idioma IdiomaActual { get; private set; }
        private GestorIdiomas()
        {
            IdiomaActual = new Idioma(1, "Español");
        }
        public static GestorIdiomas Instancia
        {
            get
            {
                if (instancia == null)
                    instancia = new GestorIdiomas();

                return instancia;
            }
        }
        public void Registrar(IObserverIdioma o)
        {
            observadores.Add(o);
        }

        public void Desregistrar(IObserverIdioma o)
        {
            observadores.Remove(o);
        }

        private void Notificar()
        {
            foreach (IObserverIdioma o in observadores)
            {
                o.ActualizarIdioma();
            }
        }
        public void CambiarIdioma(Idioma idioma)
        {
            IdiomaActual = idioma;
            Notificar();
        }
    }
}
