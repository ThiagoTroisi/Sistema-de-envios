using BE.Entidades;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositorios
{
    public class UsuarioRep
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
                            u = new Usuario
                            {
                                IdUsuario = (int)r["id_usuario"],
                                DNI = (int)r["dni"],
                                Nombre = r["nombre"].ToString(),
                                Apellido = r["apellido"].ToString(),
                                Email = r["email"].ToString(),
                                Contraseña = r["password"].ToString(),
                                Estado = (bool)r["estado"],
                                IdRol = (int)r["id_rol"]
                            };
                        }
                    }
                }
            }
            return u;
        }
    }
}
