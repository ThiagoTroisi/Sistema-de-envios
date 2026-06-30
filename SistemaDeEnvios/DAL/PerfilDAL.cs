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
    public class PerfilDAL
    {
        public DataTable ObtenerPerfiles()
        {
            string query = "select id_perfil, nombre from Perfil where activo = 1";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            {
                SqlDataAdapter da = new SqlDataAdapter(query, cx);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public Perfil ObtenerPorNombre(string nombre)
        {
            Perfil p = null;
            string query = "select id_perfil, nombre from Perfil where nombre = @nombre and activo = 1";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cx.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        p = new Perfil();
                        p.IdPerfil = (int)r["id_perfil"];
                        p.Nombre = r["nombre"].ToString();
                    }
                }
            }
            return p;
        }
        public Perfil ObtenerPorId(int idPerfil)
        {
            Perfil p = null;
            string query = "select id_perfil, nombre from Perfil where id_perfil = @id and activo = 1";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@id", idPerfil);
                cx.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        p = new Perfil();
                        p.IdPerfil = Convert.ToInt32(r["id_perfil"]);
                        p.Nombre = r["nombre"].ToString();
                    }
                }
            }
            return p;
        }
        public List<int> ObtenerPermisosPerfil(int idPerfil)
        {
            List<int> lista = new List<int>();
            string query = "select id_permiso from Perfil_Permiso where id_perfil = @id";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@id", idPerfil);
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
        public List<int> ObtenerFamiliasPerfil(int idPerfil)
        {
            List<int> lista = new List<int>();
            string query = "select id_familia from Perfil_Familia where id_perfil = @id";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@id", idPerfil);
                cx.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        lista.Add(Convert.ToInt32(r["id_familia"]));
                    }
                }
            }
            return lista;
        }
        public void CrearPerfil(string nombre)
        {
            string query = "insert into Perfil (nombre) values (@nombre)";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@nombre", nombre);

                cx.Open();

                cmd.ExecuteNonQuery();
            }
        }
        public void EliminarPerfil(int idPerfil)
        {
            string query = "update Perfil set activo = 0 where id_perfil = @id";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@id", idPerfil);

                cx.Open();

                cmd.ExecuteNonQuery();
            }
        }
        public void ActualizarNombre(int idPerfil, string nombre)
        {
            string query = "update Perfil set nombre = @nombre where id_perfil = @id";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@id", idPerfil);
                cx.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void AgregarPermiso(int idPerfil, int idPermiso)
        {
            string query = @"insert into Perfil_Permiso (id_perfil, id_permiso) values (@perfil, @permiso)";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@perfil", idPerfil);
                cmd.Parameters.AddWithValue("@permiso", idPermiso);
                cx.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void EliminarPermisos(int idPerfil)
        {
            string query = "delete from Perfil_Permiso where id_perfil = @id";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@id", idPerfil);
                cx.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void AgregarFamilia(int idPerfil, int idFamilia)
        {
            string query = @"insert into Perfil_Familia (id_perfil, id_familia) values (@perfil, @familia)";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@perfil", idPerfil);
                cmd.Parameters.AddWithValue("@familia", idFamilia);
                cx.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void EliminarFamilias(int idPerfil)
        {
            string query = @"delete from Perfil_Familia where id_perfil = @id";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@id", idPerfil);
                cx.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

