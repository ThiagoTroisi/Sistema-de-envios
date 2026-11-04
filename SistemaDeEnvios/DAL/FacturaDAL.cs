using BE;
using Microsoft.Data.SqlClient;
using System;

namespace DAL
{
    public class FacturaDAL
    {
        public int AltaFactura(FacturaBE factura)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "insert into Factura (id_envio, id_pago, dni_cliente, importe, fecha) values (@id_envio, @id_pago, @dni_cliente, @importe, @fecha); select cast(scope_identity() as int);";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@id_envio", factura.IdEnvio);
                    comando.Parameters.AddWithValue("@id_pago", factura.IdPago.HasValue ? (object)factura.IdPago.Value : DBNull.Value);
                    comando.Parameters.AddWithValue("@dni_cliente", factura.DniCliente);
                    comando.Parameters.AddWithValue("@importe", factura.Importe);
                    comando.Parameters.AddWithValue("@fecha", factura.Fecha);

                    return Convert.ToInt32(comando.ExecuteScalar());
                }
            }
        }

        public FacturaBE ObtenerPorEnvio(int idEnvio)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "select * from Factura where id_envio = @id_envio";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@id_envio", idEnvio);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new FacturaBE
                            {
                                IdFactura = Convert.ToInt32(reader["id_factura"]),
                                IdEnvio = Convert.ToInt32(reader["id_envio"]),
                                IdPago = reader["id_pago"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["id_pago"]),
                                DniCliente = Convert.ToInt32(reader["dni_cliente"]),
                                Importe = Convert.ToDecimal(reader["importe"]),
                                Fecha = Convert.ToDateTime(reader["fecha"]),
                                Dvh = reader["dvh"] == DBNull.Value ? null : reader["dvh"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }

        public FacturaBE ObtenerPorPago(int idPago)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "select * from Factura where id_pago = @id_pago";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@id_pago", idPago);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new FacturaBE
                            {
                                IdFactura = Convert.ToInt32(reader["id_factura"]),
                                IdEnvio = Convert.ToInt32(reader["id_envio"]),
                                IdPago = reader["id_pago"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["id_pago"]),
                                DniCliente = Convert.ToInt32(reader["dni_cliente"]),
                                Importe = Convert.ToDecimal(reader["importe"]),
                                Fecha = Convert.ToDateTime(reader["fecha"]),
                                Dvh = reader["dvh"] == DBNull.Value ? null : reader["dvh"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }

        public void AsociarPago(int idEnvio, int idPago)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "update Factura set id_pago = @id_pago where id_envio = @id_envio and id_pago is null";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@id_envio", idEnvio);
                    comando.Parameters.AddWithValue("@id_pago", idPago);

                    int filasAfectadas = comando.ExecuteNonQuery();

                    if (filasAfectadas != 1)
                        throw new Exception("No se pudo asociar el pago a la factura del envío.");
                }
            }
        }
    }
}