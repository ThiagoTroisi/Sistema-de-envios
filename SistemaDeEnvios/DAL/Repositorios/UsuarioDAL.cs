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
                            int DNI = (int)r["dni"];
                            string Nombre = r["nombre"].ToString();
                            string Apellido = r["apellido"].ToString();
                            string Email = r["email"].ToString();
                            string Contraseña = r["password"].ToString();
                            int IdRol = (int)r["id_rol"];
                            bool Bloqueado = (bool)r["bloqueado"];
                            bool Estado = (bool)r["estado"];
                            int IntentosFallidos = (int)r["intentos_fallidos"];
                            u = new Usuario(DNI, Nombre, Apellido, Email, Contraseña, IdRol, Bloqueado, Estado, IntentosFallidos);
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
                            int DNI = (int)r["dni"];
                            string Nombre = r["nombre"].ToString();
                            string Apellido = r["apellido"].ToString();
                            string Email = r["email"].ToString();
                            string Contraseña = r["password"].ToString();
                            int IdRol = (int)r["id_rol"];
                            bool Bloqueado = (bool)r["bloqueado"];
                            bool Estado = (bool)r["estado"];
                            int IntentosFallidos = (int)r["intentos_fallidos"];
                            u = new Usuario(DNI, Nombre, Apellido, Email, Contraseña, IdRol, Bloqueado, Estado, IntentosFallidos);
                        }
                    }
                }
            }
            return u;
        }

        public DataTable ObtenerUsuarios(bool todos)
        {
            string query;
            if (todos) query = "select * from Usuario";
            else query = "select * from Usuario where estado = 1";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            {
                SqlDataAdapter da = new SqlDataAdapter(query, cx);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void AltaUsuario(Usuario u)
        {
            string query = "insert into Usuario (dni, nombre, apellido, email, password, id_rol) values (@dni, @nombre, @apellido, @email, @password, @id_rol)";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, cx))
                {
                    cmd.Parameters.AddWithValue("@dni", u.DNI);
                    cmd.Parameters.AddWithValue("@nombre", u.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", u.Apellido);
                    cmd.Parameters.AddWithValue("@email", u.Email);
                    cmd.Parameters.AddWithValue("@password", u.Contraseña);
                    cmd.Parameters.AddWithValue("@id_rol", u.IdRol);

                    cx.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void ModificarUsuario(Usuario u)
        {
            string query = "update Usuario set nombre = @nombre, apellido = @apellido, email = @email, id_rol = @id_rol where dni = @dni";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, cx))
                {
                    cmd.Parameters.AddWithValue("@dni", u.DNI);
                    cmd.Parameters.AddWithValue("@nombre", u.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", u.Apellido);
                    cmd.Parameters.AddWithValue("@email", u.Email);
                    cmd.Parameters.AddWithValue("@id_rol", u.IdRol);

                    cx.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void ActivarDesactivarUsuario(int dni, bool ad)
        {
            string query;
            if (ad) query = "update Usuario set estado = 1 where dni = @dni";
            else query = "update Usuario set estado = 0 where dni = @dni";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand(query, cx);
                cmd.Parameters.AddWithValue("@dni", dni);

                cx.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void DesbloquearUsuario(int dni)
        {
            string query = "update Usuario set bloqueado = 0, intentos_fallidos = 0 where dni = @dni";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand(query, cx);
                cmd.Parameters.AddWithValue("@dni", dni);

                cx.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void BloquearUsuario(int dni)
        {
            string query = "update Usuario set bloqueado = 1 where dni = @dni";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, cx))
                {
                    cmd.Parameters.AddWithValue("@dni", dni);

                    cx.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void IncrementarIntentos(int dni)
        {
            string query = "update Usuario set intentos_fallidos = intentos_fallidos + 1 where dni = @dni";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, cx))
                {
                    cmd.Parameters.AddWithValue("@dni", dni);

                    cx.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void ReiniciarIntentos(int dni)
        {
            string query = "update Usuario set intentos_fallidos = 0 where dni = @dni";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, cx))
                {
                    cmd.Parameters.AddWithValue("@dni", dni);

                    cx.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
