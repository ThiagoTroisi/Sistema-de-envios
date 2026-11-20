
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

                string query = @"select id_envio, codigo_seguimiento, origen, fecha_registro, estado, id_paquete, id_destino, id_remitente from Envio where id_envio = @id_envio";

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
                                Convert.ToInt32(reader["id_remitente"])
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

                string query = @"insert into Envio (origen, fecha_registro, estado, id_paquete, id_destino, id_remitente) values (@origen, @fecha_registro, @estado, @id_paquete, @id_destino, @id_remitente); select cast(scope_identity() as int)";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@origen", envio.Origen);
                    comando.Parameters.AddWithValue("@fecha_registro", envio.FechaRegistro);
                    comando.Parameters.AddWithValue("@estado", envio.Estado);
                    comando.Parameters.AddWithValue("@id_paquete", envio.IdPaquete);
                    comando.Parameters.AddWithValue("@id_destino", envio.IdDestino);
                    comando.Parameters.AddWithValue("@id_remitente", envio.IdRemitente);

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

        public DataTable ObtenerEnvios(
            string estado,
            string codigo,
            string dniRemitente,
            string dniDestinatario,
            DateTime? fechaDesde,
            DateTime? fechaHasta)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = @"select e.id_envio, e.codigo_seguimiento, e.fecha_registro, cr.nombre + ' ' + cr.apellido as remitente, d.nombre_destinatario + ' ' + d.apellido_destinatario as destinatario, d.ciudad + ', ' + d.provincia as destino, e.estado from Envio e inner join Cliente cr on e.id_remitente = cr.dni inner join Destino d on e.id_destino = d.id_destino where 1 = 1";

                if (!string.IsNullOrWhiteSpace(estado) && estado != "Todos")
                    query += " and e.estado = @estado";

                if (!string.IsNullOrWhiteSpace(codigo))
                    query += " and e.codigo_seguimiento like @codigo";

                if (!string.IsNullOrWhiteSpace(dniRemitente))
                    query += " and cast(e.id_remitente as varchar(8)) like @dniRemitente";

                if (!string.IsNullOrWhiteSpace(dniDestinatario))
                    query += " and cast(d.dni_destinatario as varchar(8)) like @dniDestinatario";

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

        public void MarcarPendienteDistribucion(int idEnvio)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "update Envio set estado = 'Pendiente de distribución' where id_envio = @id_envio and estado = 'Registrado' and exists (select 1 from Factura f inner join Pago p on f.id_pago = p.id_pago where f.id_envio = Envio.id_envio and p.id_envio = Envio.id_envio and p.estado = 'Aprobado')";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@id_envio", idEnvio);

                    int filasAfectadas = comando.ExecuteNonQuery();

                    if (filasAfectadas != 1)
                        throw new Exception("No se pudo habilitar el envío para distribución. Verifique que tenga una factura y un pago aprobado.");
                }
            }
        }

        public DataTable ObtenerEnviosParaFacturacion()
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = @"select e.id_envio, e.codigo_seguimiento, e.origen, e.fecha_registro, e.estado, e.id_remitente, c.nombre + ' ' + c.apellido as remitente, case when exists (select 1 from Factura f where f.id_envio = e.id_envio) then cast(1 as bit) else cast(0 as bit) end as facturado from Envio e inner join Cliente c on e.id_remitente = c.dni where e.estado = 'Registrado' or exists (select 1 from Factura f where f.id_envio = e.id_envio) order by e.id_envio desc";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                {
                    DataTable tabla = new DataTable();
                    adapter.Fill(tabla);
                    return tabla;
                }
            }
        }

        public void ModificarEnvioCompleto(EnvioBE envio, PaqueteBE paquete, DestinoBE destino)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                using (SqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        string query = "EXEC sys.sp_set_session_context @key = N'ModificacionEnvio', @value = 1";

                        using (SqlCommand comando = new SqlCommand(query, conexion, transaccion))
                            comando.ExecuteNonQuery();

                        query = "DECLARE @EnviosARegistrar TABLE (id_envio INT PRIMARY KEY); INSERT INTO @EnviosARegistrar SELECT id_envio FROM Envio WHERE id_paquete = @idPaquete OR id_destino = @idDestino; IF NOT EXISTS (SELECT 1 FROM Envio WHERE id_envio = @idEnvio AND id_paquete = @idPaquete AND id_destino = @idDestino) THROW 50001, 'El envío no está asociado al paquete y destino indicados.', 1; IF (SELECT COUNT(*) FROM @EnviosARegistrar) <> (SELECT COUNT(DISTINCT EC.id_envio) FROM Envio_C EC INNER JOIN @EnviosARegistrar A ON EC.id_envio = A.id_envio WHERE EC.Act = 1) THROW 50002, 'Uno o más envíos afectados no tienen una versión histórica activa.', 1; UPDATE Paquete SET descripcion = @descripcion, peso = @peso, alto = @alto, ancho = @ancho, largo = @largo WHERE id_paquete = @idPaquete; IF @@ROWCOUNT <> 1 THROW 50003, 'No se pudo actualizar el paquete.', 1; UPDATE Destino SET direccion = @direccion, ciudad = @ciudad, codigo_postal = @codigoPostal, provincia = @provincia, dni_destinatario = @dni, nombre_destinatario = @nombre, apellido_destinatario = @apellido, telefono_destinatario = @telefono, email_destinatario = @email WHERE id_destino = @idDestino; IF @@ROWCOUNT <> 1 THROW 50004, 'No se pudo actualizar el destino.', 1; UPDATE EC SET Act = 0 FROM Envio_C EC INNER JOIN @EnviosARegistrar A ON EC.id_envio = A.id_envio WHERE EC.Act = 1; INSERT INTO Envio_C (id_envio, codigo_seguimiento, origen, fecha_registro, id_paquete, id_destino, id_remitente, intentos_fallidos, descripcion_paquete, peso, alto, ancho, largo, direccion, ciudad, codigo_postal, provincia, dni_destinatario, nombre_destinatario, apellido_destinatario, telefono_destinatario, email_destinatario, Act) SELECT E.id_envio, E.codigo_seguimiento, E.origen, E.fecha_registro, E.id_paquete, E.id_destino, E.id_remitente, E.intentos_fallidos, P.descripcion, P.peso, P.alto, P.ancho, P.largo, D.direccion, D.ciudad, D.codigo_postal, D.provincia, D.dni_destinatario, D.nombre_destinatario, D.apellido_destinatario, D.telefono_destinatario, D.email_destinatario, 1 FROM @EnviosARegistrar A INNER JOIN Envio E ON A.id_envio = E.id_envio INNER JOIN Paquete P ON E.id_paquete = P.id_paquete INNER JOIN Destino D ON E.id_destino = D.id_destino";

                        using (SqlCommand comando = new SqlCommand(query, conexion, transaccion))
                        {
                            comando.Parameters.AddWithValue("@idEnvio", envio.IdEnvio);
                            comando.Parameters.AddWithValue("@idPaquete", paquete.IdPaquete);
                            comando.Parameters.AddWithValue("@idDestino", destino.IdDestino);
                            comando.Parameters.AddWithValue("@descripcion", paquete.Descripcion);
                            comando.Parameters.AddWithValue("@peso", paquete.Peso);
                            comando.Parameters.AddWithValue("@alto", paquete.Alto);
                            comando.Parameters.AddWithValue("@ancho", paquete.Ancho);
                            comando.Parameters.AddWithValue("@largo", paquete.Largo);
                            comando.Parameters.AddWithValue("@direccion", destino.Direccion);
                            comando.Parameters.AddWithValue("@ciudad", destino.Ciudad);
                            comando.Parameters.AddWithValue("@codigoPostal", destino.CodigoPostal);
                            comando.Parameters.AddWithValue("@provincia", destino.Provincia);
                            comando.Parameters.AddWithValue("@dni", destino.DniDestinatario);
                            comando.Parameters.AddWithValue("@nombre", destino.NombreDestinatario);
                            comando.Parameters.AddWithValue("@apellido", destino.ApellidoDestinatario);
                            comando.Parameters.AddWithValue("@telefono", destino.TelefonoDestinatario);
                            comando.Parameters.AddWithValue("@email", destino.EmailDestinatario);
                            comando.ExecuteNonQuery();
                        }

                        query = "EXEC sys.sp_set_session_context @key = N'ModificacionEnvio', @value = NULL";

                        using (SqlCommand comando = new SqlCommand(query, conexion, transaccion))
                            comando.ExecuteNonQuery();

                        transaccion.Commit();
                    }
                    catch
                    {
                        try
                        {
                            transaccion.Rollback();
                        }
                        catch { }

                        try
                        {
                            using (SqlCommand comando = new SqlCommand("EXEC sys.sp_set_session_context @key = N'ModificacionEnvio', @value = NULL", conexion))
                                comando.ExecuteNonQuery();
                        }
                        catch { }

                        throw;
                    }
                }
            }
        }

        public DataTable ObtenerClientesMasFrecuentes(DateTime fechaReferencia)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = @"SELECT TOP 5
                            c.dni,
                            c.nombre,
                            c.apellido,
                            c.email,
                            COUNT(e.id_envio) AS cantidad_envios
                         FROM Cliente c
                         INNER JOIN Envio e ON e.id_remitente = c.dni
                         WHERE e.fecha_registro >= DATEADD(DAY, -90, @fechaReferencia)
                            AND e.fecha_registro < @fechaReferencia
                            AND e.estado <> 'Cancelado'
                         GROUP BY c.dni, c.nombre, c.apellido, c.email
                         ORDER BY cantidad_envios DESC, c.apellido, c.nombre";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.Add("@fechaReferencia", SqlDbType.DateTime).Value = fechaReferencia;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                    {
                        adapter.Fill(tabla);
                    }
                }
            }

            return tabla;
        }

        public DataTable ObtenerClientesConMayorDisminucion(DateTime fechaReferencia)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = @"WITH CantidadEnvios AS
                         (
                             SELECT
                                 c.dni,
                                 c.nombre,
                                 c.apellido,
                                 c.email,
                                 SUM(CASE
                                     WHEN e.fecha_registro >= DATEADD(DAY, -180, @fechaReferencia)
                                         AND e.fecha_registro < DATEADD(DAY, -90, @fechaReferencia)
                                     THEN 1 ELSE 0
                                 END) AS envios_periodo_anterior,
                                 SUM(CASE
                                     WHEN e.fecha_registro >= DATEADD(DAY, -90, @fechaReferencia)
                                         AND e.fecha_registro < @fechaReferencia
                                     THEN 1 ELSE 0
                                 END) AS envios_periodo_actual
                             FROM Cliente c
                             INNER JOIN Envio e ON e.id_remitente = c.dni
                             WHERE e.fecha_registro >= DATEADD(DAY, -180, @fechaReferencia)
                                 AND e.fecha_registro < @fechaReferencia
                                 AND e.estado <> 'Cancelado'
                             GROUP BY c.dni, c.nombre, c.apellido, c.email
                         )
                         SELECT TOP 5
                             dni,
                             nombre,
                             apellido,
                             email,
                             envios_periodo_anterior,
                             envios_periodo_actual,
                             envios_periodo_anterior - envios_periodo_actual AS envios_perdidos,
                             CAST(
                                 (envios_periodo_anterior - envios_periodo_actual) * 100.0
                                 / envios_periodo_anterior
                                 AS DECIMAL(5,2)
                             ) AS porcentaje_disminucion
                         FROM CantidadEnvios
                         WHERE envios_periodo_anterior >= 5
                             AND envios_periodo_actual < envios_periodo_anterior
                         ORDER BY porcentaje_disminucion DESC, envios_perdidos DESC";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.Add("@fechaReferencia", SqlDbType.DateTime).Value = fechaReferencia;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                    {
                        adapter.Fill(tabla);
                    }
                }
            }

            return tabla;
        }
    }
}
