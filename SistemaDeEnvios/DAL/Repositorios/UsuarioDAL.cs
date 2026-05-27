using BE.Entidades;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DAL.Repositorios
{
    public class UsuarioDAL
    {
        public Usuario ConsultaPorMail(string mail)
        {
            Usuario u = null;
            string query = "select * from Usuario where email = @email";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            {
                cx.Open();
                using (SqlCommand cmd = new SqlCommand(query, cx))
                {
                    cmd.Parameters.AddWithValue("@email", mail);
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            int IdUsuario = (int)r["id_usuario"];
                            int DNI = (int)r["dni"];
                            string Nombre = r["nombre"].ToString();
                            string Apellido = r["apellido"].ToString();
                            string Email = r["email"].ToString();
                            string Contraseña = r["password"].ToString();
                            bool Estado = (bool)r["estado"];
                            int IdRol = (int)r["id_rol"];
                            u = new Usuario(IdUsuario, DNI, Nombre, Apellido, Email, Contraseña, Estado, IdRol);
                        }
                    }
                }
            }
            return u;
        }
        public Usuario ConsultaPorDNI(int dni)
        {
            Usuario u = null;
            string query = "select * from Usuario where dni = @dni";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            {
                cx.Open();
                using (SqlCommand cmd = new SqlCommand(query, cx))
                {
                    cmd.Parameters.AddWithValue("@dni", dni);
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            int IdUsuario = (int)r["id_usuario"];
                            int DNI = (int)r["dni"];
                            string Nombre = r["nombre"].ToString();
                            string Apellido = r["apellido"].ToString();
                            string Email = r["email"].ToString();
                            string Contraseña = r["password"].ToString();
                            bool Estado = (bool)r["estado"];
                            int IdRol = (int)r["id_rol"];
                            u = new Usuario(IdUsuario, DNI, Nombre, Apellido, Email, Contraseña, Estado, IdRol);
                        }
                    }
                }
            }
            return u;
        }

        public DataTable ObtenerUsuarios()
        {
            DataTable dt = new DataTable();

            string query = "select u.id_usuario, u.dni, u.nombre, u.apellido, u.email, r.descripcion as rol, u.estado from Usuario u inner join Rol r on u.id_rol = r.id_rol";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            {
                using (SqlDataAdapter da = new SqlDataAdapter(query, cx))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void AltaUsuario(Usuario u)
        {
            string query = "insert into Usuario (dni, nombre, apellido, email, password, estado, id_rol) values (@dni, @nombre, @apellido, @email, @password, @estado, @id_rol)";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, cx))
                {
                    cmd.Parameters.AddWithValue("@dni", u.DNI);
                    cmd.Parameters.AddWithValue("@nombre", u.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", u.Apellido);
                    cmd.Parameters.AddWithValue("@email", u.Email);
                    cmd.Parameters.AddWithValue("@password", u.Contraseña);
                    cmd.Parameters.AddWithValue("@estado", u.Estado);
                    cmd.Parameters.AddWithValue("@id_rol", u.IdRol);

                    cx.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
