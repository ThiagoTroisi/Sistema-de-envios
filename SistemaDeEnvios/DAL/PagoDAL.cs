using BE;
using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public class PagoDAL
    {
        public int AltaPago(PagoBE pago)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "insert into Pago (id_envio, dni_cliente, importe, fecha_pago, estado) values (@id_envio, @dni_cliente, @importe, @fecha_pago, @estado); select scope_identity();";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@id_envio", pago.IdEnvio);
                    comando.Parameters.AddWithValue("@dni_cliente", pago.DniCliente);
                    comando.Parameters.AddWithValue("@importe", pago.Importe);
                    comando.Parameters.AddWithValue("@fecha_pago", pago.FechaPago);
                    comando.Parameters.AddWithValue("@estado", pago.Estado);

                    return Convert.ToInt32(comando.ExecuteScalar());
                }
            }
        }

        public PagoBE ConsultaPorId(int idPago)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "select * from Pago where id_pago = @id_pago";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@id_pago", idPago);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PagoBE
                            {
                                IdPago = Convert.ToInt32(reader["id_pago"]),
                                IdEnvio = Convert.ToInt32(reader["id_envio"]),
                                DniCliente = Convert.ToInt32(reader["dni_cliente"]),
                                Importe = Convert.ToDecimal(reader["importe"]),
                                FechaPago = Convert.ToDateTime(reader["fecha_pago"]),
                                Estado = reader["estado"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }

        public PagoBE ObtenerPagoAprobadoPorEnvio(int idEnvio)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "select top 1 * from Pago where id_envio = @id_envio and estado = 'Aprobado' order by fecha_pago desc";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@id_envio", idEnvio);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PagoBE
                            {
                                IdPago = Convert.ToInt32(reader["id_pago"]),
                                IdEnvio = Convert.ToInt32(reader["id_envio"]),
                                DniCliente = Convert.ToInt32(reader["dni_cliente"]),
                                Importe = Convert.ToDecimal(reader["importe"]),
                                FechaPago = Convert.ToDateTime(reader["fecha_pago"]),
                                Estado = reader["estado"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }
    }
}