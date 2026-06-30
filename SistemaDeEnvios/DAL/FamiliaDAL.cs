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
    public class FamiliaDAL
    {
        public DataTable ObtenerFamilias()
        {
            DataTable dt = new DataTable();

            string query = "select id_familia, nombre from Familia where activo = 1";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }

            return dt;
        }
        public Familia ObtenerPorNombre(string nombre)
        {
            Familia f = null;
            string query = "select id_familia, nombre from Familia where nombre = @nombre and activo = 1";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cx.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        f = new Familia();
                        f.IdFamilia = (int)r["id_familia"];
                        f.Nombre = r["nombre"].ToString();
                    }
                }
            }
            return f;
        }

        public Familia ObtenerPorId(int idFamilia)
        {
            Familia f = null;
            string query = "select id_familia, nombre from Familia where id_familia = @id and activo = 1";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@id", idFamilia);
                cx.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        f = new Familia();
                        f.IdFamilia = Convert.ToInt32(r["id_familia"]);
                        f.Nombre = r["nombre"].ToString();
                    }
                }
            }
            return f;
        }

        public bool TienePadre(int idFamilia)
        {
            string query = "select 1 from Familia_Familia ff inner join Familia f on f.id_familia = ff.id_familia_padre where ff.id_familia_hija = @id and f.activo = 1";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@id", idFamilia);
                cx.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    return r.Read();
                }
            }
        }

        public List<int> ObtenerPermisosFamilia(int idFamilia)
        {
            List<int> lista = new List<int>();
            string query = "select id_permiso from Familia_Permiso where id_familia = @id";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@id", idFamilia);
                cx.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        lista.Add(Convert.ToInt32(r["id_permiso"]));
                    }
                }
            }
            return lista;
        }
        public List<int> ObtenerSubfamilias(int idFamilia)
        {
            List<int> lista = new List<int>();
            string query = "select id_familia_hija from Familia_Familia where id_familia_padre = @id";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@id", idFamilia);
                cx.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        lista.Add(Convert.ToInt32(r["id_familia_hija"]));
                    }
                }
            }
            return lista;
        }
        public void CrearFamilia(string nombre)
        {
            string query = "insert into Familia (nombre) values (@nombre)";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@nombre", nombre);

                cx.Open();

                cmd.ExecuteNonQuery();
            }
        }
        public void EliminarFamilia(int id)
        {
            string query = "update Familia set activo = 0 where id_familia = @id";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@id", id);

                cx.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarNombre(int idFamilia, string nombre)
        {
            string query = "update Familia set nombre = @nombre where id_familia = @id";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@id", idFamilia);

                cx.Open();

                cmd.ExecuteNonQuery();
            }
        }
        public void AgregarPermiso(int idFamilia, int idPermiso)
        {
            string query = @"insert into Familia_Permiso (id_familia, id_permiso) values (@familia, @permiso)";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@familia", idFamilia);
                cmd.Parameters.AddWithValue("@permiso", idPermiso);

                cx.Open();

                cmd.ExecuteNonQuery();
            }
        }
        public void EliminarPermisos(int idFamilia)
        {
            string query = "delete from Familia_Permiso where id_familia = @id";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@id", idFamilia);

                cx.Open();

                cmd.ExecuteNonQuery();
            }
        }
        public void AgregarSubfamilia(int padre, int hija)
        {
            string query = "insert into Familia_Familia (id_familia_padre, id_familia_hija) values (@padre, @hija)";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@padre", padre);
                cmd.Parameters.AddWithValue("@hija", hija);

                cx.Open();

                cmd.ExecuteNonQuery();
            }
        }
        public void EliminarSubfamilias(int idFamilia)
        {
            string query = "delete from Familia_Familia where id_familia_padre = @id";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@id", idFamilia);

                cx.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
