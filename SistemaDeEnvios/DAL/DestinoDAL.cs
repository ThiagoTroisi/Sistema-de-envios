using BE;
using Microsoft.Data.SqlClient;
using System;

namespace DAL
{
    public class DestinoDAL
    {
        public DestinoBE ConsultaPorId(int idDestino)
        {
            DestinoBE destino = null;

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "select id_destino, direccion, ciudad, codigo_postal, provincia from Destino where id_destino = @id_destino";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@id_destino", idDestino);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            destino = new DestinoBE(
                                Convert.ToInt32(reader["id_destino"]),
                                reader["direccion"].ToString(),
                                reader["ciudad"].ToString(),
                                reader["codigo_postal"].ToString(),
                                reader["provincia"].ToString()
                            );
                        }
                    }
                }
            }

            return destino;
        }

        public int AltaDestino(DestinoBE destino)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "insert into Destino (direccion, ciudad, codigo_postal, provincia) values (@direccion, @ciudad, @codigo_postal, @provincia); select cast(scope_identity() as int)";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@direccion", destino.Direccion);
                    comando.Parameters.AddWithValue("@ciudad", destino.Ciudad);
                    comando.Parameters.AddWithValue("@codigo_postal", destino.CodigoPostal);
                    comando.Parameters.AddWithValue("@provincia", destino.Provincia);

                    return Convert.ToInt32(comando.ExecuteScalar());
                }
            }
        }
    }
}