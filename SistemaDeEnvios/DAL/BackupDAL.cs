using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BackupDAL
    {
        public void CrearBackup(string pathCompleto)
        {
            string query = $"BACKUP DATABASE SistemaDeEnvios TO DISK = @path";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@path", pathCompleto);

                cx.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void RestaurarBackup(string path)
        {
            SqlConnection cx = new SqlConnection("Server=.;Database=master;Trusted_Connection=true;TrustServerCertificate=true;");
            string query = $@"
            ALTER DATABASE SistemaDeEnvios SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
            RESTORE DATABASE SistemaDeEnvios FROM DISK = @path WITH REPLACE;
            ALTER DATABASE SistemaDeEnvios SET MULTI_USER;";

            using (cx)
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@path", path);

                cx.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
