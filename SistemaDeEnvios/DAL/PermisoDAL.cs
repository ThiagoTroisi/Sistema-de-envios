using Microsoft.Data.SqlClient;
using Servicios.Perfiles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PermisoDAL
    {
        public DataTable ObtenerPermisos()
        {
            DataTable dt = new DataTable();

            string query = "select id_permiso, nombre from Permiso";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }
            return dt;
        }

        public Permiso ObtenerPorId(int idPermiso)
        {
            Permiso p = null;
            string query = "select id_permiso, nombre from Permiso where id_permiso = @id";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@id", idPermiso);
                cx.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        p = new Permiso();
                        p.IdPermiso = Convert.ToInt32(r["id_permiso"]);
                        p.Nombre = r["nombre"].ToString();
                    }
                }
            }
            return p;
        }
    }
}
