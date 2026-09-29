using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DAL
{
    public class Conexion
    {
        public static SqlConnection ObtenerConexion()
        {
            string servidor = ".";

            string rutaConfig = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.txt");

            if (File.Exists(rutaConfig))
            {
                servidor = File.ReadAllText(rutaConfig).Trim();
            }

            string cadena = $"Server={servidor};Database=SistemaDeEnvios;Trusted_Connection=True;TrustServerCertificate=True;";

            return new SqlConnection(cadena);
        }
    }
}
