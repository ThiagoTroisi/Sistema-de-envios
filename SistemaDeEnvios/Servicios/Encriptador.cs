using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public static class Encriptador
    {
        public static string Hash(string texto)
        {
            return BCrypt.Net.BCrypt.HashPassword(texto);
        }

        public static bool Verificar(string texto, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(texto, hash);
        }
    }
}
