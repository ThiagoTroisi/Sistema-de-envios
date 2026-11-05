using BE;
using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public class IntentoEntregaDAL
    {
        private readonly Conexion conexion = new Conexion();

        public int RegistrarIntento(IntentoEntregaBE intento)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                using (SqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        int idEnvio;

                        string queryEnvio = "SELECT id_envio FROM Ruta_Envio WHERE id_ruta_envio = @id_ruta_envio";

                        using (SqlCommand comando = new SqlCommand(queryEnvio, conexion, transaccion))
                        {
                            comando.Parameters.AddWithValue("@id_ruta_envio", intento.IdRutaEnvio);

                            object resultado = comando.ExecuteScalar();

                            if (resultado == null)
                                throw new Exception("No se encontró el envío correspondiente al intento.");

                            idEnvio = Convert.ToInt32(resultado);
                        }

                        string queryIntento = "INSERT INTO IntentoEntrega (id_ruta_envio, resultado, tipo_inconveniente, observaciones) VALUES (@id_ruta_envio, @resultado, @tipo_inconveniente, @observaciones); SELECT CAST(SCOPE_IDENTITY() AS INT);";

                        int idIntento;

                        using (SqlCommand comando = new SqlCommand(queryIntento, conexion, transaccion))
                        {
                            comando.Parameters.AddWithValue("@id_ruta_envio", intento.IdRutaEnvio);
                            comando.Parameters.AddWithValue("@resultado", intento.Resultado);
                            comando.Parameters.AddWithValue("@tipo_inconveniente", (object)intento.TipoInconveniente ?? DBNull.Value);
                            comando.Parameters.AddWithValue("@observaciones", (object)intento.Observaciones ?? DBNull.Value);

                            idIntento = Convert.ToInt32(comando.ExecuteScalar());
                        }

                        if (intento.Resultado == "Entregado")
                        {
                            string queryActualizar = "UPDATE Envio SET estado = 'Entregado' WHERE id_envio = @id_envio AND estado NOT IN ('Entregado', 'Cancelado', 'Pendiente de resolución')";

                            using (SqlCommand comando = new SqlCommand(queryActualizar, conexion, transaccion))
                            {
                                comando.Parameters.AddWithValue("@id_envio", idEnvio);

                                if (comando.ExecuteNonQuery() != 1)
                                    throw new Exception("No se pudo actualizar el estado del envío.");
                            }
                        }
                        else if (intento.Resultado == "Fallido")
                        {
                            string queryActualizar = "UPDATE Envio SET intentos_fallidos = intentos_fallidos + 1, estado = CASE WHEN intentos_fallidos + 1 >= 3 THEN 'Pendiente de resolución' ELSE 'Pendiente de distribución' END WHERE id_envio = @id_envio AND estado NOT IN ('Entregado', 'Cancelado', 'Pendiente de resolución')";

                            using (SqlCommand comando = new SqlCommand(queryActualizar, conexion, transaccion))
                            {
                                comando.Parameters.AddWithValue("@id_envio", idEnvio);

                                if (comando.ExecuteNonQuery() != 1)
                                    throw new Exception("No se pudo actualizar el estado del envío.");
                            }
                        }
                        else
                        {
                            throw new Exception("El resultado del intento no es válido.");
                        }

                        transaccion.Commit();

                        return idIntento;
                    }
                    catch
                    {
                        transaccion.Rollback();
                        throw;
                    }
                }
            }
        }
        public DataTable ObtenerIntentosPorEnvio(int idEnvio)
        {
            string query = "SELECT ie.id_intento, ie.fecha_intento, ie.resultado, ie.tipo_inconveniente, ie.observaciones FROM IntentoEntrega ie INNER JOIN Ruta_Envio re ON ie.id_ruta_envio = re.id_ruta_envio WHERE re.id_envio = @id_envio ORDER BY ie.fecha_intento DESC";

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@id_envio", idEnvio);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable tabla = new DataTable();
                        da.Fill(tabla);
                        return tabla;
                    }
                }
            }
        }

        public DataTable ObtenerIntentosPorRuta(int idRuta)
        {
            string query = "SELECT ie.id_intento, re.id_ruta_envio, e.id_envio, e.codigo_seguimiento, CONCAT(d.nombre_destinatario, ' ', d.apellido_destinatario) AS destinatario, ie.fecha_intento, ie.resultado, ie.tipo_inconveniente, ie.observaciones FROM IntentoEntrega ie INNER JOIN Ruta_Envio re ON ie.id_ruta_envio = re.id_ruta_envio INNER JOIN Envio e ON re.id_envio = e.id_envio INNER JOIN Destino d ON e.id_destino = d.id_destino WHERE re.id_ruta = @id_ruta ORDER BY re.orden_entrega, ie.fecha_intento DESC";

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@id_ruta", idRuta);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable tabla = new DataTable();
                        da.Fill(tabla);
                        return tabla;
                    }
                }
            }
        }

        public bool EnvioEntregado(int idRutaEnvio)
        {
            string query = "SELECT COUNT(*) FROM IntentoEntrega WHERE id_ruta_envio = @id_ruta_envio AND resultado = 'Entregado'";

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@id_ruta_envio", idRutaEnvio);

                    cn.Open();

                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        public bool TodosLosEnviosTienenIntento(int idRuta)
        {
            string query = "SELECT COUNT(*) FROM Ruta_Envio re WHERE re.id_ruta = @id_ruta AND NOT EXISTS (SELECT 1 FROM IntentoEntrega ie WHERE ie.id_ruta_envio = re.id_ruta_envio)";

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@id_ruta", idRuta);

                    cn.Open();

                    return Convert.ToInt32(cmd.ExecuteScalar()) == 0;
                }
            }
        }

        public DataTable ObtenerResumenIntentosPorRuta(int idRuta)
        {
            string query = "SELECT re.id_ruta_envio, e.id_envio, e.codigo_seguimiento, CONCAT(d.nombre_destinatario, ' ', d.apellido_destinatario) AS destinatario, (SELECT TOP 1 ie.resultado FROM IntentoEntrega ie WHERE ie.id_ruta_envio = re.id_ruta_envio ORDER BY ie.fecha_intento DESC, ie.id_intento DESC) AS ultimo_resultado, (SELECT COUNT(*) FROM IntentoEntrega ie WHERE ie.id_ruta_envio = re.id_ruta_envio) AS cantidad_intentos FROM Ruta_Envio re INNER JOIN Envio e ON re.id_envio = e.id_envio INNER JOIN Destino d ON e.id_destino = d.id_destino WHERE re.id_ruta = @id_ruta ORDER BY re.orden_entrega";

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@id_ruta", idRuta);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable tabla = new DataTable();
                        da.Fill(tabla);
                        return tabla;
                    }
                }
            }
        }

        public bool EnvioPerteneceARuta(int idRutaEnvio, int idRuta)
        {
            string query = "SELECT COUNT(*) FROM Ruta_Envio WHERE id_ruta_envio = @id_ruta_envio AND id_ruta = @id_ruta";

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@id_ruta_envio", idRutaEnvio);
                    cmd.Parameters.AddWithValue("@id_ruta", idRuta);

                    cn.Open();

                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        public bool RutaEnCurso(int idRuta)
        {
            string query = "SELECT COUNT(*) FROM Ruta WHERE id_ruta = @id_ruta AND estado = 'En curso'";

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@id_ruta", idRuta);

                    cn.Open();

                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        public int ObtenerIdEnvioPorRutaEnvio(int idRutaEnvio)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "SELECT id_envio FROM Ruta_Envio WHERE id_ruta_envio = @id_ruta_envio";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@id_ruta_envio", idRutaEnvio);

                    object resultado = comando.ExecuteScalar();

                    if (resultado == null)
                        throw new Exception("No se encontró el envío correspondiente al intento.");

                    return Convert.ToInt32(resultado);
                }
            }
        }

        public int ObtenerCantidadEnviosPendientesResolucion()
        {
            string query = "SELECT COUNT(*) FROM Envio WHERE estado = 'Pendiente de resolución'";

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public DataTable ObtenerEnviosPendientesResolucion()
        {
            string query = "SELECT e.id_envio, e.codigo_seguimiento, e.fecha_registro, e.intentos_fallidos, CONCAT(d.nombre_destinatario, ' ', d.apellido_destinatario) AS destinatario, d.telefono_destinatario, d.direccion, d.ciudad, d.provincia FROM Envio e INNER JOIN Destino d ON e.id_destino = d.id_destino WHERE e.estado = 'Pendiente de resolución' ORDER BY e.fecha_registro";

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable tabla = new DataTable();
                        da.Fill(tabla);
                        return tabla;
                    }
                }
            }
        }
        public void AutorizarNuevoIntento(int idEnvio)
        {
            string query = "UPDATE Envio SET estado = 'Pendiente de distribución' WHERE id_envio = @id_envio AND estado = 'Pendiente de resolución' AND intentos_fallidos >= 3";

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@id_envio", idEnvio);

                    cn.Open();

                    if (cmd.ExecuteNonQuery() != 1)
                        throw new Exception("No se pudo autorizar el nuevo ciclo. Verifique que el envío siga pendiente de resolución.");
                }
            }
        }
        public void DevolverEnvioPorIntentos(int idEnvio)
        {
            string query = "UPDATE Envio SET estado = 'Cancelado' WHERE id_envio = @id_envio AND estado = 'Pendiente de resolución' AND intentos_fallidos >= 3";

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@id_envio", idEnvio);

                    cn.Open();

                    if (cmd.ExecuteNonQuery() != 1)
                        throw new Exception("No se pudo cancelar el envío. Verifique que siga pendiente de resolución.");
                }
            }
        }
    }
}