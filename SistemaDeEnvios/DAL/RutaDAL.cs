using BE;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class RutaDAL
    {
        public int CrearRuta(RutaBE ruta, List<int> idsEnvios)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                using (SqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        string queryRuta = "insert into Ruta (descripcion, fecha_creacion, estado) values (@descripcion, GETDATE(), 'Pendiente'); select cast(scope_identity() as int);";

                        int idRuta;

                        using (SqlCommand comando = new SqlCommand(queryRuta, conexion, transaccion))
                        {
                            comando.Parameters.AddWithValue("@descripcion", string.IsNullOrWhiteSpace(ruta.Descripcion) ? (object)DBNull.Value : ruta.Descripcion);

                            idRuta = Convert.ToInt32(comando.ExecuteScalar());
                        }

                        int ordenEntrega = 1;

                        foreach (int idEnvio in idsEnvios)
                        {
                            string queryValidacion = "select estado from Envio with (UPDLOCK, HOLDLOCK) where id_envio = @id_envio";

                            using (SqlCommand comando = new SqlCommand(queryValidacion, conexion, transaccion))
                            {
                                comando.Parameters.AddWithValue("@id_envio", idEnvio);

                                object resultado = comando.ExecuteScalar();

                                if (resultado == null)
                                    throw new Exception("Uno de los envíos seleccionados no existe.");

                                string estado = resultado.ToString();

                                if (estado != "Pendiente de distribución")
                                    throw new Exception("El envío " + idEnvio + " no se encuentra pendiente de distribución.");
                            }

                            string queryRutaActiva = "select count(*) from Ruta_Envio re inner join Ruta r on re.id_ruta = r.id_ruta where re.id_envio = @id_envio and r.estado in ('Pendiente', 'Asignada', 'En curso')";

                            using (SqlCommand comando = new SqlCommand(queryRutaActiva, conexion, transaccion))
                            {
                                comando.Parameters.AddWithValue("@id_envio", idEnvio);

                                int cantidad = Convert.ToInt32(comando.ExecuteScalar());

                                if (cantidad > 0)
                                    throw new Exception("El envío " + idEnvio + " ya pertenece a otra ruta activa.");
                            }

                            string queryAsociacion = "insert into Ruta_Envio (id_ruta, id_envio, orden_entrega) values (@id_ruta, @id_envio, @orden_entrega)";

                            using (SqlCommand comando = new SqlCommand(queryAsociacion, conexion, transaccion))
                            {
                                comando.Parameters.AddWithValue("@id_ruta", idRuta);
                                comando.Parameters.AddWithValue("@id_envio", idEnvio);
                                comando.Parameters.AddWithValue("@orden_entrega", ordenEntrega);

                                comando.ExecuteNonQuery();
                            }

                            ordenEntrega++;
                        }

                        transaccion.Commit();

                        return idRuta;
                    }
                    catch
                    {
                        transaccion.Rollback();
                        throw;
                    }
                }
            }
        }

        public void AgregarEnviosARuta(int idRuta, List<int> idsEnvios)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                using (SqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        string queryRuta = "select estado from Ruta with (UPDLOCK, HOLDLOCK) where id_ruta = @id_ruta";

                        string estadoRuta;

                        using (SqlCommand comando = new SqlCommand(queryRuta, conexion, transaccion))
                        {
                            comando.Parameters.AddWithValue("@id_ruta", idRuta);

                            object resultado = comando.ExecuteScalar();

                            if (resultado == null)
                                throw new Exception("La ruta seleccionada no existe.");

                            estadoRuta = resultado.ToString();
                        }

                        if (estadoRuta != "Pendiente" && estadoRuta != "Asignada")
                            throw new Exception("No se pueden modificar rutas que ya comenzaron.");

                        string queryOrden = "select isnull(max(orden_entrega), 0) from Ruta_Envio where id_ruta = @id_ruta";

                        int ordenEntrega;

                        using (SqlCommand comando = new SqlCommand(queryOrden, conexion, transaccion))
                        {
                            comando.Parameters.AddWithValue("@id_ruta", idRuta);

                            ordenEntrega = Convert.ToInt32(comando.ExecuteScalar()) + 1;
                        }

                        foreach (int idEnvio in idsEnvios)
                        {
                            string queryValidacion = "select estado from Envio with (UPDLOCK, HOLDLOCK) where id_envio = @id_envio";

                            using (SqlCommand comando = new SqlCommand(queryValidacion, conexion, transaccion))
                            {
                                comando.Parameters.AddWithValue("@id_envio", idEnvio);

                                object resultado = comando.ExecuteScalar();

                                if (resultado == null)
                                    throw new Exception("Uno de los envíos seleccionados no existe.");

                                if (resultado.ToString() != "Pendiente de distribución")
                                    throw new Exception("El envío " + idEnvio + " no se encuentra pendiente de distribución.");
                            }

                            string queryRutaActiva = "select count(*) from Ruta_Envio re inner join Ruta r on re.id_ruta = r.id_ruta where re.id_envio = @id_envio and r.estado in ('Pendiente', 'Asignada', 'En curso')";

                            using (SqlCommand comando = new SqlCommand(queryRutaActiva, conexion, transaccion))
                            {
                                comando.Parameters.AddWithValue("@id_envio", idEnvio);

                                int cantidad = Convert.ToInt32(comando.ExecuteScalar());

                                if (cantidad > 0)
                                    throw new Exception("El envío " + idEnvio + " ya pertenece a otra ruta activa.");
                            }

                            string queryAsociacion = "insert into Ruta_Envio (id_ruta, id_envio, orden_entrega) values (@id_ruta, @id_envio, @orden_entrega)";

                            using (SqlCommand comando = new SqlCommand(queryAsociacion, conexion, transaccion))
                            {
                                comando.Parameters.AddWithValue("@id_ruta", idRuta);
                                comando.Parameters.AddWithValue("@id_envio", idEnvio);
                                comando.Parameters.AddWithValue("@orden_entrega", ordenEntrega);

                                comando.ExecuteNonQuery();
                            }

                            ordenEntrega++;
                        }

                        transaccion.Commit();
                    }
                    catch
                    {
                        transaccion.Rollback();
                        throw;
                    }
                }
            }
        }

        public void QuitarEnviosDeRuta(int idRuta, List<int> idsEnvios)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                using (SqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        string queryRuta = "select estado from Ruta with (UPDLOCK, HOLDLOCK) where id_ruta = @id_ruta";

                        string estadoRuta;

                        using (SqlCommand comando = new SqlCommand(queryRuta, conexion, transaccion))
                        {
                            comando.Parameters.AddWithValue("@id_ruta", idRuta);

                            object resultado = comando.ExecuteScalar();

                            if (resultado == null)
                                throw new Exception("La ruta seleccionada no existe.");

                            estadoRuta = resultado.ToString();
                        }

                        if (estadoRuta != "Pendiente" && estadoRuta != "Asignada")
                            throw new Exception("No se pueden modificar rutas que ya comenzaron.");

                        foreach (int idEnvio in idsEnvios)
                        {
                            string query = "delete from Ruta_Envio where id_ruta = @id_ruta and id_envio = @id_envio and not exists (select 1 from IntentoEntrega ie where ie.id_ruta_envio = Ruta_Envio.id_ruta_envio)";

                            using (SqlCommand comando = new SqlCommand(query, conexion, transaccion))
                            {
                                comando.Parameters.AddWithValue("@id_ruta", idRuta);
                                comando.Parameters.AddWithValue("@id_envio", idEnvio);

                                int filasAfectadas = comando.ExecuteNonQuery();

                                if (filasAfectadas != 1)
                                    throw new Exception("No se pudo quitar el envío " + idEnvio + ". Verifique que pertenezca a la ruta y que no tenga intentos registrados.");
                            }
                        }

                        string queryReordenar = "DECLARE @desplazamiento INT; SELECT @desplazamiento = ISNULL(MAX(orden_entrega), 0) + COUNT(*) + 1 FROM Ruta_Envio WHERE id_ruta = @id_ruta; UPDATE Ruta_Envio SET orden_entrega = orden_entrega + @desplazamiento WHERE id_ruta = @id_ruta; WITH OrdenNuevo AS (SELECT id_ruta_envio, ROW_NUMBER() OVER (ORDER BY orden_entrega, id_ruta_envio) AS nuevo_orden FROM Ruta_Envio WHERE id_ruta = @id_ruta) UPDATE re SET orden_entrega = o.nuevo_orden FROM Ruta_Envio re INNER JOIN OrdenNuevo o ON re.id_ruta_envio = o.id_ruta_envio;";

                        using (SqlCommand comando = new SqlCommand(queryReordenar, conexion, transaccion))
                        {
                            comando.Parameters.AddWithValue("@id_ruta", idRuta);
                            comando.ExecuteNonQuery();
                        }

                        string queryCantidad = "select count(*) from Ruta_Envio where id_ruta = @id_ruta";

                        using (SqlCommand comando = new SqlCommand(queryCantidad, conexion, transaccion))
                        {
                            comando.Parameters.AddWithValue("@id_ruta", idRuta);

                            int cantidadEnvios = Convert.ToInt32(comando.ExecuteScalar());

                            if (cantidadEnvios == 0)
                            {
                                string queryCancelar = "update Ruta set estado = 'Cancelada' where id_ruta = @id_ruta";

                                using (SqlCommand comandoCancelar = new SqlCommand(queryCancelar, conexion, transaccion))
                                {
                                    comandoCancelar.Parameters.AddWithValue("@id_ruta", idRuta);

                                    comandoCancelar.ExecuteNonQuery();
                                }

                                string queryAsignacion = "update AsignacionRuta set estado = 'Cancelada', fecha_fin = GETDATE() where id_ruta = @id_ruta and estado = 'Activa'";

                                using (SqlCommand comandoAsignacion = new SqlCommand(queryAsignacion, conexion, transaccion))
                                {
                                    comandoAsignacion.Parameters.AddWithValue("@id_ruta", idRuta);

                                    comandoAsignacion.ExecuteNonQuery();
                                }
                            }
                        }

                        transaccion.Commit();
                    }
                    catch
                    {
                        transaccion.Rollback();
                        throw;
                    }
                }
            }
        }

        public void CancelarRuta(int idRuta)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                using (SqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        string queryRuta = "select estado from Ruta with (UPDLOCK, HOLDLOCK) where id_ruta = @id_ruta";

                        using (SqlCommand comando = new SqlCommand(queryRuta, conexion, transaccion))
                        {
                            comando.Parameters.AddWithValue("@id_ruta", idRuta);

                            object resultado = comando.ExecuteScalar();

                            if (resultado == null)
                                throw new Exception("La ruta seleccionada no existe.");

                            string estado = resultado.ToString();

                            if (estado != "Pendiente" && estado != "Asignada")
                                throw new Exception("No se puede cancelar una ruta que ya comenzó.");
                        }

                        string queryRutaActualizar = "update Ruta set estado = 'Cancelada' where id_ruta = @id_ruta";

                        using (SqlCommand comando = new SqlCommand(queryRutaActualizar, conexion, transaccion))
                        {
                            comando.Parameters.AddWithValue("@id_ruta", idRuta);

                            comando.ExecuteNonQuery();
                        }

                        string queryAsignacion = "update AsignacionRuta set estado = 'Cancelada', fecha_fin = GETDATE() where id_ruta = @id_ruta and estado = 'Activa'";

                        using (SqlCommand comando = new SqlCommand(queryAsignacion, conexion, transaccion))
                        {
                            comando.Parameters.AddWithValue("@id_ruta", idRuta);

                            comando.ExecuteNonQuery();
                        }

                        transaccion.Commit();
                    }
                    catch
                    {
                        transaccion.Rollback();
                        throw;
                    }
                }
            }
        }

        public DataTable ObtenerEnviosPendientesDistribucion()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "select e.id_envio, e.codigo_seguimiento, d.nombre_destinatario + ' ' + d.apellido_destinatario as destinatario, d.direccion, d.ciudad, d.provincia, d.codigo_postal from Envio e inner join Destino d on e.id_destino = d.id_destino where e.estado = 'Pendiente de distribución' and not exists (select 1 from Ruta_Envio re inner join Ruta r on re.id_ruta = r.id_ruta where re.id_envio = e.id_envio and r.estado in ('Pendiente', 'Asignada', 'En curso')) order by d.provincia, d.ciudad, d.direccion";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                {
                    adapter.Fill(tabla);
                }
            }

            return tabla;
        }

        public DataTable ObtenerRutasSinIniciar()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "select r.id_ruta, r.descripcion, r.fecha_creacion, r.estado, count(re.id_envio) as cantidad_envios, ar.dni_repartidor from Ruta r left join Ruta_Envio re on r.id_ruta = re.id_ruta outer apply (select top 1 a.dni_repartidor from AsignacionRuta a where a.id_ruta = r.id_ruta and a.estado = 'Activa' order by a.fecha_asignacion desc) ar where r.estado in ('Pendiente', 'Asignada') group by r.id_ruta, r.descripcion, r.fecha_creacion, r.estado, ar.dni_repartidor order by r.fecha_creacion desc";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                {
                    adapter.Fill(tabla);
                }
            }

            return tabla;
        }

        public DataTable ObtenerRutasPendientesAsignacion()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string query = "SELECT r.id_ruta, r.descripcion, r.fecha_creacion, r.estado, COUNT(re.id_envio) AS cantidad_envios FROM Ruta r LEFT JOIN Ruta_Envio re ON r.id_ruta = re.id_ruta WHERE r.estado = 'Pendiente' GROUP BY r.id_ruta, r.descripcion, r.fecha_creacion, r.estado ORDER BY r.fecha_creacion DESC";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                {
                    adapter.Fill(tabla);
                }
            }

            return tabla;
        }

        public DataTable ObtenerTodasLasRutas()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string query = "SELECT r.id_ruta, r.descripcion, r.fecha_creacion, r.estado, COUNT(re.id_envio) AS cantidad_envios, ar.dni_repartidor, u.nombre + ' ' + u.apellido AS repartidor FROM Ruta r LEFT JOIN Ruta_Envio re ON r.id_ruta = re.id_ruta OUTER APPLY (SELECT TOP 1 a.dni_repartidor FROM AsignacionRuta a WHERE a.id_ruta = r.id_ruta AND a.estado = 'Activa' ORDER BY a.fecha_asignacion DESC) ar LEFT JOIN Usuario u ON ar.dni_repartidor = u.dni GROUP BY r.id_ruta, r.descripcion, r.fecha_creacion, r.estado, ar.dni_repartidor, u.nombre, u.apellido ORDER BY r.fecha_creacion DESC";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                {
                    adapter.Fill(tabla);
                }
            }

            return tabla;
        }

        public DataTable ObtenerEnviosDeRuta(int idRuta)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "SELECT re.id_ruta_envio, e.id_envio, re.orden_entrega, e.codigo_seguimiento, CONCAT(d.nombre_destinatario, ' ', d.apellido_destinatario) AS destinatario, d.telefono_destinatario, d.direccion, d.ciudad, d.provincia, d.codigo_postal FROM Ruta_Envio re INNER JOIN Envio e ON re.id_envio = e.id_envio INNER JOIN Destino d ON e.id_destino = d.id_destino WHERE re.id_ruta = @id_ruta ORDER BY re.orden_entrega";
                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@id_ruta", idRuta);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                    {
                        adapter.Fill(tabla);
                    }
                }
            }

            return tabla;
        }

        public string ObtenerEstadoRuta(int idRuta)
        {
            string consulta = "SELECT estado FROM Ruta WHERE id_ruta = @idRuta";

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@idRuta", idRuta);

                    object resultado = comando.ExecuteScalar();

                    if (resultado == null)
                    {
                        return null;
                    }

                    return resultado.ToString();
                }
            }
        }

        public void MoverEnvio(int idRuta, int idRutaEnvio, bool subir, string estadoEsperado)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                using (SqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        string consultaEstado = "SELECT estado FROM Ruta WITH (UPDLOCK, HOLDLOCK) WHERE id_ruta = @idRuta";

                        string estadoRuta = "";

                        using (SqlCommand comando = new SqlCommand(consultaEstado, conexion, transaccion))
                        {
                            comando.Parameters.AddWithValue("@idRuta", idRuta);

                            object resultado = comando.ExecuteScalar();

                            if (resultado == null)
                            {
                                throw new Exception("La ruta seleccionada ya no existe.");
                            }

                            estadoRuta = resultado.ToString();
                        }

                        if (estadoRuta != estadoEsperado)
                        {
                            throw new Exception("El estado de la ruta cambió durante la operación. Actualice los datos e intente nuevamente.");
                        }

                        int ordenActual = 0;
                        int ordenDestino = 0;
                        int idRutaEnvioDestino = 0;
                        int ordenTemporal = 0;

                        string consultaActual = "SELECT orden_entrega FROM Ruta_Envio WITH (UPDLOCK, HOLDLOCK) WHERE id_ruta_envio = @idRutaEnvio AND id_ruta = @idRuta";

                        using (SqlCommand comando = new SqlCommand(consultaActual, conexion, transaccion))
                        {
                            comando.Parameters.AddWithValue("@idRutaEnvio", idRutaEnvio);
                            comando.Parameters.AddWithValue("@idRuta", idRuta);

                            object resultado = comando.ExecuteScalar();

                            if (resultado == null)
                            {
                                throw new Exception("El envío seleccionado ya no pertenece a la ruta.");
                            }

                            ordenActual = Convert.ToInt32(resultado);
                        }

                        string consultaDestino = subir
                            ? "SELECT TOP 1 id_ruta_envio, orden_entrega FROM Ruta_Envio WITH (UPDLOCK, HOLDLOCK) WHERE id_ruta = @idRuta AND orden_entrega < @ordenActual ORDER BY orden_entrega DESC"
                            : "SELECT TOP 1 id_ruta_envio, orden_entrega FROM Ruta_Envio WITH (UPDLOCK, HOLDLOCK) WHERE id_ruta = @idRuta AND orden_entrega > @ordenActual ORDER BY orden_entrega ASC";

                        using (SqlCommand comando = new SqlCommand(consultaDestino, conexion, transaccion))
                        {
                            comando.Parameters.AddWithValue("@idRuta", idRuta);
                            comando.Parameters.AddWithValue("@ordenActual", ordenActual);

                            using (SqlDataReader lector = comando.ExecuteReader())
                            {
                                if (!lector.Read())
                                {
                                    throw new Exception(subir ? "El envío ya se encuentra primero en la ruta." : "El envío ya se encuentra último en la ruta.");
                                }

                                idRutaEnvioDestino = Convert.ToInt32(lector["id_ruta_envio"]);
                                ordenDestino = Convert.ToInt32(lector["orden_entrega"]);
                            }
                        }

                        string consultaTemporal = "SELECT ISNULL(MAX(orden_entrega), 0) + 1 FROM Ruta_Envio WHERE id_ruta = @idRuta";

                        using (SqlCommand comando = new SqlCommand(consultaTemporal, conexion, transaccion))
                        {
                            comando.Parameters.AddWithValue("@idRuta", idRuta);
                            ordenTemporal = Convert.ToInt32(comando.ExecuteScalar());
                        }

                        string actualizarTemporal = "UPDATE Ruta_Envio SET orden_entrega = @ordenTemporal WHERE id_ruta_envio = @idRutaEnvio";

                        using (SqlCommand comando = new SqlCommand(actualizarTemporal, conexion, transaccion))
                        {
                            comando.Parameters.AddWithValue("@ordenTemporal", ordenTemporal);
                            comando.Parameters.AddWithValue("@idRutaEnvio", idRutaEnvio);
                            comando.ExecuteNonQuery();
                        }

                        string actualizarDestino = "UPDATE Ruta_Envio SET orden_entrega = @ordenActual WHERE id_ruta_envio = @idRutaEnvioDestino";

                        using (SqlCommand comando = new SqlCommand(actualizarDestino, conexion, transaccion))
                        {
                            comando.Parameters.AddWithValue("@ordenActual", ordenActual);
                            comando.Parameters.AddWithValue("@idRutaEnvioDestino", idRutaEnvioDestino);
                            comando.ExecuteNonQuery();
                        }

                        string actualizarActual = "UPDATE Ruta_Envio SET orden_entrega = @ordenDestino WHERE id_ruta_envio = @idRutaEnvio";

                        using (SqlCommand comando = new SqlCommand(actualizarActual, conexion, transaccion))
                        {
                            comando.Parameters.AddWithValue("@ordenDestino", ordenDestino);
                            comando.Parameters.AddWithValue("@idRutaEnvio", idRutaEnvio);
                            comando.ExecuteNonQuery();
                        }

                        transaccion.Commit();
                    }
                    catch
                    {
                        transaccion.Rollback();
                        throw;
                    }
                }
            }
        }

        public DataTable ObtenerDatosHojaRuta(int idAsignacion)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "SELECT ar.id_asignacion, ar.id_ruta, ar.dni_repartidor, u.nombre, u.apellido, ar.fecha_asignacion, ar.estado AS estado_asignacion, r.descripcion, r.estado AS estado_ruta FROM AsignacionRuta ar INNER JOIN Ruta r ON ar.id_ruta = r.id_ruta INNER JOIN Usuario u ON ar.dni_repartidor = u.dni WHERE ar.id_asignacion = @id_asignacion";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@id_asignacion", idAsignacion);

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