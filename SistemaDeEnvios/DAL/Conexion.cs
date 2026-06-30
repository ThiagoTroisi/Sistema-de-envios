using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Conexion
    {
        private static string cx = "Server=.;Database=SistemaDeEnvios1;Trusted_Connection=true;TrustServerCertificate=true;";
        public static SqlConnection ObtenerConexion() => new SqlConnection(cx);
    }
}
