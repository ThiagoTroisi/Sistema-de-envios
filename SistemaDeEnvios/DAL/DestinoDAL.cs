
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

                string query = "select id_destino, direccion, ciudad, codigo_postal, provincia, dni_destinatario, nombre_destinatario, apellido_destinatario, telefono_destinatario, email_destinatario from Destino where id_destino = @id_destino";

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
                                reader["provincia"].ToString(),
                                Convert.ToInt32(reader["dni_destinatario"]),
                                reader["nombre_destinatario"].ToString(),
                                reader["apellido_destinatario"].ToString(),
                                reader["telefono_destinatario"].ToString(),
                                reader["email_destinatario"].ToString()
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

                string query = "insert into Destino (direccion, ciudad, codigo_postal, provincia, dni_destinatario, nombre_destinatario, apellido_destinatario, telefono_destinatario, email_destinatario) values (@direccion, @ciudad, @codigo_postal, @provincia, @dni_destinatario, @nombre_destinatario, @apellido_destinatario, @telefono_destinatario, @email_destinatario); select cast(scope_identity() as int)";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@direccion", destino.Direccion);
                    comando.Parameters.AddWithValue("@ciudad", destino.Ciudad);
                    comando.Parameters.AddWithValue("@codigo_postal", destino.CodigoPostal);
                    comando.Parameters.AddWithValue("@provincia", destino.Provincia);
                    comando.Parameters.AddWithValue("@dni_destinatario", destino.DniDestinatario);
                    comando.Parameters.AddWithValue("@nombre_destinatario", destino.NombreDestinatario);
                    comando.Parameters.AddWithValue("@apellido_destinatario", destino.ApellidoDestinatario);
                    comando.Parameters.AddWithValue("@telefono_destinatario", destino.TelefonoDestinatario);
                    comando.Parameters.AddWithValue("@email_destinatario", destino.EmailDestinatario);

                    return Convert.ToInt32(comando.ExecuteScalar());
                }
            }
        }

        public void ModificarDestino(DestinoBE destino)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "update Destino set direccion = @direccion, ciudad = @ciudad, codigo_postal = @codigo_postal, provincia = @provincia, dni_destinatario = @dni_destinatario, nombre_destinatario = @nombre_destinatario, apellido_destinatario = @apellido_destinatario, telefono_destinatario = @telefono_destinatario, email_destinatario = @email_destinatario where id_destino = @id_destino";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@id_destino", destino.IdDestino);
                    comando.Parameters.AddWithValue("@direccion", destino.Direccion);
                    comando.Parameters.AddWithValue("@ciudad", destino.Ciudad);
                    comando.Parameters.AddWithValue("@codigo_postal", destino.CodigoPostal);
                    comando.Parameters.AddWithValue("@provincia", destino.Provincia);
                    comando.Parameters.AddWithValue("@dni_destinatario", destino.DniDestinatario);
                    comando.Parameters.AddWithValue("@nombre_destinatario", destino.NombreDestinatario);
                    comando.Parameters.AddWithValue("@apellido_destinatario", destino.ApellidoDestinatario);
                    comando.Parameters.AddWithValue("@telefono_destinatario", destino.TelefonoDestinatario);
                    comando.Parameters.AddWithValue("@email_destinatario", destino.EmailDestinatario);

                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}
