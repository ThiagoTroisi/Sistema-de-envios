using BE;
using Microsoft.Data.SqlClient;
using System;

namespace DAL
{
    public class EnvioDAL
    {
        public EnvioBE ConsultaPorId(int idEnvio)
        {
            EnvioBE envio = null;

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "select id_envio, codigo_seguimiento, origen, fecha_registro, estado, id_paquete, id_destino, id_remitente, id_destinatario from Envio where id_envio = @id_envio";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@id_envio", idEnvio);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            envio = new EnvioBE(
                                Convert.ToInt32(reader["id_envio"]),
                                reader["codigo_seguimiento"].ToString(),
                                reader["origen"].ToString(),
                                Convert.ToDateTime(reader["fecha_registro"]),
                                reader["estado"].ToString(),
                                Convert.ToInt32(reader["id_paquete"]),
                                Convert.ToInt32(reader["id_destino"]),
                                Convert.ToInt32(reader["id_remitente"]),
                                Convert.ToInt32(reader["id_destinatario"])
                            );
                        }
                    }
                }
            }

            return envio;
        }

        public int AltaEnvio(EnvioBE envio)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "insert into Envio (origen, fecha_registro, estado, id_paquete, id_destino, id_remitente, id_destinatario) values (@origen, @fecha_registro, @estado, @id_paquete, @id_destino, @id_remitente, @id_destinatario); select cast(scope_identity() as int)";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@origen", envio.Origen);
                    comando.Parameters.AddWithValue("@fecha_registro", envio.FechaRegistro);
                    comando.Parameters.AddWithValue("@estado", envio.Estado);
                    comando.Parameters.AddWithValue("@id_paquete", envio.IdPaquete);
                    comando.Parameters.AddWithValue("@id_destino", envio.IdDestino);
                    comando.Parameters.AddWithValue("@id_remitente", envio.IdRemitente);
                    comando.Parameters.AddWithValue("@id_destinatario", envio.IdDestinatario);

                    return Convert.ToInt32(comando.ExecuteScalar());
                }
            }
        }

        public void ActualizarCodigoSeguimiento(int idEnvio, string codigoSeguimiento)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "update Envio set codigo_seguimiento = @codigo_seguimiento where id_envio = @id_envio";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@id_envio", idEnvio);
                    comando.Parameters.AddWithValue("@codigo_seguimiento", codigoSeguimiento);

                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}