using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace DAL
{
    public class AsignacionRutaDAL
    {
        public DataTable ObtenerRepartidoresDisponibles()
        {
            string query = "SELECT u.dni, u.nombre, u.apellido FROM Usuario u INNER JOIN Perfil p ON u.id_perfil = p.id_perfil WHERE p.nombre = 'Repartidor' AND p.activo = 1 AND u.estado = 1 AND u.bloqueado = 0 AND NOT EXISTS (SELECT 1 FROM AsignacionRuta ar INNER JOIN Ruta r ON ar.id_ruta = r.id_ruta WHERE ar.dni_repartidor = u.dni AND ar.estado = 'Activa' AND r.estado IN ('Asignada', 'En curso')) ORDER BY u.apellido, u.nombre";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            {
                SqlDataAdapter da = new SqlDataAdapter(query, cx);
                DataTable dt = new DataTable();

                da.Fill(dt);

                return dt;
            }
        }

        public int AsignarRuta(int idRuta, int dniRepartidor)
        {
            using (SqlConnection cx = Conexion.ObtenerConexion())
            {
                cx.Open();

                using (SqlTransaction transaccion = cx.BeginTransaction())
                {
                    try
                    {
                        string queryRuta = "SELECT estado FROM Ruta WITH (UPDLOCK, HOLDLOCK) WHERE id_ruta = @idRuta";

                        string estadoRuta;

                        using (SqlCommand cmd = new SqlCommand(queryRuta, cx, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@idRuta", idRuta);

                            object resultado = cmd.ExecuteScalar();

                            if (resultado == null)
                                throw new Exception("La ruta seleccionada no existe.");

                            estadoRuta = resultado.ToString();
                        }

                        if (estadoRuta != "Pendiente")
                            throw new Exception("La ruta no se encuentra disponible para asignación. Si ya tiene un repartidor asignado, primero debe desasignarlo.");

                        string queryRepartidor = "SELECT COUNT(*) FROM Usuario u WITH (UPDLOCK, HOLDLOCK) INNER JOIN Perfil p ON u.id_perfil = p.id_perfil WHERE u.dni = @dni AND p.nombre = 'Repartidor' AND p.activo = 1 AND u.estado = 1 AND u.bloqueado = 0";

                        using (SqlCommand cmd = new SqlCommand(queryRepartidor, cx, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@dni", dniRepartidor);

                            int cantidad = (int)cmd.ExecuteScalar();

                            if (cantidad == 0)
                                throw new Exception("El usuario seleccionado no es un repartidor habilitado.");
                        }

                        string queryDisponibilidad = "SELECT COUNT(*) FROM AsignacionRuta ar WITH (UPDLOCK, HOLDLOCK) INNER JOIN Ruta r ON ar.id_ruta = r.id_ruta WHERE ar.dni_repartidor = @dni AND ar.estado = 'Activa' AND r.estado IN ('Asignada', 'En curso')";

                        using (SqlCommand cmd = new SqlCommand(queryDisponibilidad, cx, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@dni", dniRepartidor);

                            int cantidad = (int)cmd.ExecuteScalar();

                            if (cantidad > 0)
                                throw new Exception("El repartidor seleccionado ya tiene otra ruta activa.");
                        }

                        string queryInsertar = "INSERT INTO AsignacionRuta (id_ruta, dni_repartidor) VALUES (@idRuta, @dniRepartidor); SELECT CAST(SCOPE_IDENTITY() AS INT);";

                        int idAsignacion;

                        using (SqlCommand cmd = new SqlCommand(queryInsertar, cx, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@idRuta", idRuta);
                            cmd.Parameters.AddWithValue("@dniRepartidor", dniRepartidor);

                            idAsignacion = (int)cmd.ExecuteScalar();
                        }

                        string queryActualizarRuta = "UPDATE Ruta SET estado = 'Asignada' WHERE id_ruta = @idRuta";

                        using (SqlCommand cmd = new SqlCommand(queryActualizarRuta, cx, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@idRuta", idRuta);

                            cmd.ExecuteNonQuery();
                        }

                        transaccion.Commit();

                        return idAsignacion;
                    }
                    catch
                    {
                        transaccion.Rollback();
                        throw;
                    }
                }
            }
        }

        public void QuitarAsignacion(int idRuta, string motivo)
        {
            using (SqlConnection cx = Conexion.ObtenerConexion())
            {
                cx.Open();

                using (SqlTransaction transaccion = cx.BeginTransaction())
                {
                    try
                    {
                        string queryRuta = "SELECT estado FROM Ruta WITH (UPDLOCK, HOLDLOCK) WHERE id_ruta = @idRuta";

                        string estadoRuta;

                        using (SqlCommand cmd = new SqlCommand(queryRuta, cx, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@idRuta", idRuta);

                            object resultado = cmd.ExecuteScalar();

                            if (resultado == null)
                                throw new Exception("La ruta seleccionada no existe.");

                            estadoRuta = resultado.ToString();
                        }

                        if (estadoRuta != "Asignada")
                            throw new Exception("Solo se puede quitar la asignación de una ruta que todavía no comenzó.");

                        string queryAsignacion = "UPDATE AsignacionRuta SET estado = 'Cancelada', fecha_fin = GETDATE(), motivo_cancelacion = @motivo WHERE id_ruta = @idRuta AND estado = 'Activa'";

                        using (SqlCommand cmd = new SqlCommand(queryAsignacion, cx, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@idRuta", idRuta);
                            cmd.Parameters.AddWithValue("@motivo", motivo);

                            int filasAfectadas = cmd.ExecuteNonQuery();

                            if (filasAfectadas == 0)
                                throw new Exception("La ruta no tiene una asignación activa.");
                        }

                        string queryActualizarRuta = "UPDATE Ruta SET estado = 'Pendiente' WHERE id_ruta = @idRuta";

                        using (SqlCommand cmd = new SqlCommand(queryActualizarRuta, cx, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@idRuta", idRuta);

                            cmd.ExecuteNonQuery();
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

        public DataTable ObtenerAsignaciones()
        {
            string query = "SELECT ar.id_asignacion, ar.id_ruta, r.descripcion, ar.dni_repartidor, u.nombre + ' ' + u.apellido AS repartidor, ar.fecha_asignacion, ar.fecha_inicio, ar.fecha_fin, ar.estado, ar.motivo_cancelacion FROM AsignacionRuta ar INNER JOIN Ruta r ON ar.id_ruta = r.id_ruta INNER JOIN Usuario u ON ar.dni_repartidor = u.dni ORDER BY ar.fecha_asignacion DESC";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            {
                SqlDataAdapter da = new SqlDataAdapter(query, cx);
                DataTable dt = new DataTable();

                da.Fill(dt);

                return dt;
            }
        }

        public DataTable ObtenerTodosLosRepartidores()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string query = "SELECT u.dni, u.nombre, u.apellido, CASE WHEN EXISTS (SELECT 1 FROM AsignacionRuta ar INNER JOIN Ruta r ON ar.id_ruta = r.id_ruta WHERE ar.dni_repartidor = u.dni AND ar.estado = 'Activa' AND r.estado IN ('Asignada', 'En curso')) THEN 'Ocupado' ELSE 'Disponible' END AS disponibilidad, (SELECT TOP 1 r.id_ruta FROM AsignacionRuta ar INNER JOIN Ruta r ON ar.id_ruta = r.id_ruta WHERE ar.dni_repartidor = u.dni AND ar.estado = 'Activa' AND r.estado IN ('Asignada', 'En curso') ORDER BY ar.fecha_asignacion DESC) AS id_ruta_activa FROM Usuario u INNER JOIN Perfil p ON u.id_perfil = p.id_perfil WHERE p.nombre = 'Repartidor' AND p.activo = 1 AND u.estado = 1 AND u.bloqueado = 0 ORDER BY u.apellido, u.nombre";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                {
                    adapter.Fill(tabla);
                }
            }

            return tabla;
        }

        public int ObtenerIdAsignacionActiva(int idRuta)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "SELECT id_asignacion FROM AsignacionRuta WHERE id_ruta = @id_ruta AND estado = 'Activa'";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@id_ruta", idRuta);

                    object resultado = comando.ExecuteScalar();

                    if (resultado == null)
                        throw new Exception("No se encontró una asignación activa para la ruta seleccionada.");

                    return Convert.ToInt32(resultado);
                }
            }
        }


        public void IniciarRuta(int idAsignacion)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                cn.Open();

                using (SqlTransaction transaccion = cn.BeginTransaction())
                {
                    try
                    {
                        string queryAsignacion = "UPDATE AsignacionRuta SET fecha_inicio = GETDATE() WHERE id_asignacion = @id_asignacion AND estado = 'Activa' AND fecha_inicio IS NULL";

                        using (SqlCommand cmd = new SqlCommand(queryAsignacion, cn, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@id_asignacion", idAsignacion);

                            if (cmd.ExecuteNonQuery() != 1)
                                throw new Exception("La asignación no está activa o ya fue iniciada.");
                        }

                        string queryRuta = "UPDATE Ruta SET estado = 'En curso' WHERE id_ruta = (SELECT id_ruta FROM AsignacionRuta WHERE id_asignacion = @id_asignacion) AND estado = 'Asignada'";

                        using (SqlCommand cmd = new SqlCommand(queryRuta, cn, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@id_asignacion", idAsignacion);

                            if (cmd.ExecuteNonQuery() != 1)
                                throw new Exception("La ruta no se encuentra en estado Asignada.");
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

        public void FinalizarRuta(int idAsignacion)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                cn.Open();

                using (SqlTransaction transaccion = cn.BeginTransaction())
                {
                    try
                    {
                        string queryAsignacion = "UPDATE AsignacionRuta SET estado = 'Finalizada', fecha_fin = GETDATE() WHERE id_asignacion = @id_asignacion AND estado = 'Activa' AND fecha_inicio IS NOT NULL";

                        using (SqlCommand cmd = new SqlCommand(queryAsignacion, cn, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@id_asignacion", idAsignacion);

                            if (cmd.ExecuteNonQuery() != 1)
                                throw new Exception("La asignación no está activa o la ruta no fue iniciada.");
                        }

                        string queryRuta = "UPDATE Ruta SET estado = 'Finalizada' WHERE id_ruta = (SELECT id_ruta FROM AsignacionRuta WHERE id_asignacion = @id_asignacion) AND estado = 'En curso'";

                        using (SqlCommand cmd = new SqlCommand(queryRuta, cn, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@id_asignacion", idAsignacion);

                            if (cmd.ExecuteNonQuery() != 1)
                                throw new Exception("La ruta no se encuentra en curso.");
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

        public int ObtenerIdRutaPorAsignacion(int idAsignacion)
        {
            string query = "SELECT id_ruta FROM AsignacionRuta WHERE id_asignacion = @id_asignacion";

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@id_asignacion", idAsignacion);

                    cn.Open();

                    object resultado = cmd.ExecuteScalar();

                    if (resultado == null || resultado == DBNull.Value)
                        throw new Exception("No se encontró la ruta correspondiente a la asignación.");

                    return Convert.ToInt32(resultado);
                }
            }
        }

        public int ObtenerIdAsignacionActivaPorRepartidor(int dniRepartidor)
        {
            string query = "SELECT id_asignacion FROM AsignacionRuta WHERE dni_repartidor = @dni_repartidor AND estado = 'Activa'";

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@dni_repartidor", dniRepartidor);

                    cn.Open();

                    object resultado = cmd.ExecuteScalar();

                    if (resultado == null || resultado == DBNull.Value)
                        return 0;

                    return Convert.ToInt32(resultado);
                }
            }
        }

        public bool AsignacionPerteneceARepartidor(int idAsignacion, int dniRepartidor)
        {
            string query = "SELECT COUNT(*) FROM AsignacionRuta WHERE id_asignacion = @id_asignacion AND dni_repartidor = @dni_repartidor AND estado = 'Activa'";

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@id_asignacion", idAsignacion);
                    cmd.Parameters.AddWithValue("@dni_repartidor", dniRepartidor);

                    cn.Open();

                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        public DataTable ObtenerDatosAsignacion(int idAsignacion)
        {
            string query = "SELECT ar.id_asignacion, ar.id_ruta, ar.dni_repartidor, u.nombre AS nombre_repartidor, u.apellido AS apellido_repartidor, ar.fecha_asignacion, ar.fecha_inicio, ar.fecha_fin, ar.estado AS estado_asignacion, r.descripcion, r.estado AS estado_ruta FROM AsignacionRuta ar INNER JOIN Ruta r ON ar.id_ruta = r.id_ruta INNER JOIN Usuario u ON ar.dni_repartidor = u.dni WHERE ar.id_asignacion = @id_asignacion";

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@id_asignacion", idAsignacion);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable tabla = new DataTable();
                        da.Fill(tabla);
                        return tabla;
                    }
                }
            }
        }

        public DataTable ObtenerHistorialRutas(int dniRepartidor)
        {
            string query = "SELECT ar.id_asignacion, ar.id_ruta, r.descripcion, ar.fecha_asignacion, ar.fecha_inicio, ar.fecha_fin FROM AsignacionRuta ar INNER JOIN Ruta r ON ar.id_ruta = r.id_ruta WHERE ar.dni_repartidor = @dni_repartidor AND ar.estado = 'Finalizada' ORDER BY ar.fecha_fin DESC";

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@dni_repartidor", dniRepartidor);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable tabla = new DataTable();
                        da.Fill(tabla);
                        return tabla;
                    }
                }
            }
        }


    }
}