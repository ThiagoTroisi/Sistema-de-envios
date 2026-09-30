using BE;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

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

        public DataTable ObtenerEnvios(string estado, string codigo, string dniRemitente, string dniDestinatario, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "select e.id_envio, e.codigo_seguimiento, e.fecha_registro, pr.nombre + ' ' + pr.apellido as remitente, pd.nombre + ' ' + pd.apellido as destinatario, d.ciudad + ', ' + d.provincia as destino, e.estado from Envio e inner join Persona pr on e.id_remitente = pr.dni inner join Persona pd on e.id_destinatario = pd.dni inner join Destino d on e.id_destino = d.id_destino where 1 = 1";

                if (!string.IsNullOrWhiteSpace(estado) && estado != "Todos")
                    query += " and e.estado = @estado";

                if (!string.IsNullOrWhiteSpace(codigo))
                    query += " and e.codigo_seguimiento like @codigo";

                if (!string.IsNullOrWhiteSpace(dniRemitente))
                    query += " and cast(e.id_remitente as varchar) like @dniRemitente";

                if (!string.IsNullOrWhiteSpace(dniDestinatario))
                    query += " and cast(e.id_destinatario as varchar) like @dniDestinatario";

                if (fechaDesde.HasValue)
                    query += " and e.fecha_registro >= @fechaDesde";

                if (fechaHasta.HasValue)
                    query += " and e.fecha_registro < @fechaHasta";

                query += " order by e.fecha_registro desc";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    if (!string.IsNullOrWhiteSpace(estado) && estado != "Todos")
                        comando.Parameters.AddWithValue("@estado", estado);

                    if (!string.IsNullOrWhiteSpace(codigo))
                        comando.Parameters.AddWithValue("@codigo", "%" + codigo + "%");

                    if (!string.IsNullOrWhiteSpace(dniRemitente))
                        comando.Parameters.AddWithValue("@dniRemitente", "%" + dniRemitente + "%");

                    if (!string.IsNullOrWhiteSpace(dniDestinatario))
                        comando.Parameters.AddWithValue("@dniDestinatario", "%" + dniDestinatario + "%");

                    if (fechaDesde.HasValue)
                        comando.Parameters.AddWithValue("@fechaDesde", fechaDesde.Value.Date);

                    if (fechaHasta.HasValue)
                        comando.Parameters.AddWithValue("@fechaHasta", fechaHasta.Value.Date.AddDays(1));

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                    {
                        adapter.Fill(tabla);
                    }
                }
            }

            return tabla;
        }

        public void CancelarEnvio(int idEnvio)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "update Envio set estado = @estado where id_envio = @id_envio";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@id_envio", idEnvio);
                    comando.Parameters.AddWithValue("@estado", "Cancelado");

                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}