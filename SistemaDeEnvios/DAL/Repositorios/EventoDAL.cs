using BE.Entidades;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositorios
{
    public class EventoDAL
    {
        public void RegistrarEvento(Evento e)
        {
            string query = "insert into Evento (dni_usuario, modulo, evento, criticidad) values (@dni_usuario, @modulo, @evento, @criticidad)";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, cx))
                {
                    cmd.Parameters.AddWithValue("@dni_usuario", e.DNIUsuario);
                    cmd.Parameters.AddWithValue("@modulo", e.Modulo);
                    cmd.Parameters.AddWithValue("@evento", e.Descripcion);
                    cmd.Parameters.AddWithValue("@criticidad", e.Criticidad);

                    cx.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
