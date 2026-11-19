using DAL;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace Servicios.DALs
{
    public class EnvioHistorialDAL
    {
        private const string consultaBase = "select EC.id_version, EC.id_envio, EC.codigo_seguimiento as Codigo, convert(date, EC.fecha_cambio) as Fecha, convert(time(0), EC.fecha_cambio) as Hora, EC.origen as Origen, coalesce(C.nombre + ' ' + C.apellido, 'DNI ' + cast(EC.id_remitente as varchar(20))) as Remitente, EC.intentos_fallidos as Intentos_Fallidos, EC.descripcion_paquete as DescripcionPaquete, EC.peso as Peso, EC.alto as Alto, EC.ancho as Ancho, EC.largo as Largo, EC.direccion as Direccion, EC.ciudad as Ciudad, EC.codigo_postal as CodigoPostal, EC.provincia as Provincia, EC.dni_destinatario as DniDestinatario, EC.nombre_destinatario as NombreDestinatario, EC.apellido_destinatario as ApellidoDestinatario, EC.telefono_destinatario as TelefonoDestinatario, EC.email_destinatario as EmailDestinatario, EC.Act from Envio_C EC left join Cliente C on EC.id_remitente = C.dni";

        public DataTable ObtenerTodos()
        {
            string query = consultaBase + " order by EC.fecha_cambio desc";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            {
                SqlDataAdapter da = new SqlDataAdapter(query, cx);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable ObtenerCodigosSeguimiento()
        {
            string query = "select distinct codigo_seguimiento from Envio_C where codigo_seguimiento is not null order by codigo_seguimiento";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            {
                SqlDataAdapter da = new SqlDataAdapter(query, cx);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable ObtenerRemitentes()
        {
            string query = "select distinct EC.id_remitente, coalesce(C.nombre + ' ' + C.apellido, 'DNI ' + cast(EC.id_remitente as varchar(20))) as Remitente from Envio_C EC left join Cliente C on EC.id_remitente = C.dni order by Remitente";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            {
                SqlDataAdapter da = new SqlDataAdapter(query, cx);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable Filtrar(string codigo, int? dniRemitente, DateTime? desde, DateTime? hasta)
        {
            string query = consultaBase + " where 1=1";
            SqlCommand cmd = new SqlCommand();

            if (!string.IsNullOrWhiteSpace(codigo))
            {
                query += " and EC.codigo_seguimiento = @codigo";
                cmd.Parameters.AddWithValue("@codigo", codigo);
            }

            if (dniRemitente.HasValue)
            {
                query += " and EC.id_remitente = @dniRemitente";
                cmd.Parameters.AddWithValue("@dniRemitente", dniRemitente.Value);
            }

            if (desde.HasValue)
            {
                query += " and EC.fecha_cambio >= @desde";
                cmd.Parameters.AddWithValue("@desde", desde.Value);
            }

            if (hasta.HasValue)
            {
                query += " and EC.fecha_cambio < @hasta";
                cmd.Parameters.AddWithValue("@hasta", hasta.Value);
            }

            query += " order by EC.fecha_cambio desc";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            {
                cmd.Connection = cx;
                cmd.CommandText = query;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable ObtenerVersion(int idVersion)
        {
            string query = "select id_version, id_envio, codigo_seguimiento, Act from Envio_C where id_version = @idVersion";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand(query, cx);
                cmd.Parameters.AddWithValue("@idVersion", idVersion);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }

        public bool ExisteVersionActiva(int idEnvio)
        {
            string query = "select count(*) from Envio_C where id_envio = @idEnvio and Act = 1";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand(query, cx);
                cmd.Parameters.AddWithValue("@idEnvio", idEnvio);

                cx.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        public void ActivarVersion(int idVersion, int idEnvio)
        {
            using (SqlConnection cx = Conexion.ObtenerConexion())
            {
                cx.Open();

                using (SqlTransaction transaccion = cx.BeginTransaction())
                {
                    try
                    {
                        string query = "update Envio_C set Act = 0 where id_envio = @idEnvio and Act = 1";

                        using (SqlCommand cmd = new SqlCommand(query, cx, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@idEnvio", idEnvio);
                            cmd.ExecuteNonQuery();
                        }

                        query = "update Envio_C set Act = 1 where id_version = @idVersion and id_envio = @idEnvio and Act = 0";

                        using (SqlCommand cmd = new SqlCommand(query, cx, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@idVersion", idVersion);
                            cmd.Parameters.AddWithValue("@idEnvio", idEnvio);

                            if (cmd.ExecuteNonQuery() != 1)
                                throw new Exception("No se pudo activar la versión seleccionada.");
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
    }
}