using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Servicios.GestionIdiomas
{
    public static class Traducciones
    {
        private static Dictionary<string, Dictionary<string, string>> textos = new Dictionary<string, Dictionary<string, string>>();
        public static string Traducir(string clave)
        {
            string idioma = GestorIdiomas.Instancia.IdiomaActual.Nombre;

            if (textos.ContainsKey(clave))
            {
                if (textos[clave].ContainsKey(idioma))
                {
                    return textos[clave][idioma];
                }
            }
            return clave;
        }
        static Traducciones()
        {
            string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "traducciones.json");
            string json = File.ReadAllText(ruta);
            textos = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(json);
        }
    }
}
