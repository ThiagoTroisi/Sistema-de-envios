using BE.Entidades;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EventoDAL
    {
        public int RegistrarEvento(Evento e)
        {
            string query = "insert into Evento (dni_usuario, modulo, evento, criticidad) values (@dni_usuario, @modulo, @evento, @criticidad); SELECT SCOPE_IDENTITY();";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, cx))
                {
                    cmd.Parameters.AddWithValue("@dni_usuario", e.DNIUsuario);
                    cmd.Parameters.AddWithValue("@modulo", e.Modulo);
                    cmd.Parameters.AddWithValue("@evento", e.Descripcion);
                    cmd.Parameters.AddWithValue("@criticidad", e.Criticidad);

                    cx.Open();
                    object result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
        }
        public DataTable ObtenerTodosLosEventos()
        {
            string query = "select U.email as Email_Usuario, convert(date, E.fecha) as Fecha, convert(time(0), E.fecha) as Hora, E.modulo as Módulo, E.evento as Evento, E.criticidad as Criticidad, U.nombre, U.apellido from Evento E inner join Usuario U on E.dni_usuario = U.dni where E.fecha >= dateadd(day, -3, getdate()) order by E.fecha desc";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            {
                SqlDataAdapter da = new SqlDataAdapter(query, cx);

                DataTable dt = new DataTable();

                da.Fill(dt);

                return dt;
            }
        }
        public DataTable FiltrarEventos(string email, DateTime? desde, DateTime? hasta, string modulo, string evento, int? criticidad)
        {
            string query = "select U.email as Email_Usuario, convert(date, E.fecha) as Fecha, convert(time(0), E.fecha) as Hora, E.modulo as Módulo, E.evento as Evento, E.criticidad as Criticidad, U.nombre, U.apellido from Evento E inner join Usuario U on E.dni_usuario = U.dni where 1=1";
            SqlCommand cmd = new SqlCommand();

            if (!string.IsNullOrWhiteSpace(email))
            {
                query += " and U.email like @email";

                cmd.Parameters.AddWithValue("@email", "%" + email + "%");
            }
            if (desde.HasValue)
            {
                query += " and E.fecha >= @desde";

                cmd.Parameters.AddWithValue("@desde", desde.Value);
            }
            if (hasta.HasValue)
            {
                query += " and E.fecha <= @hasta";

                cmd.Parameters.AddWithValue("@hasta", hasta.Value);
            }
            if (!string.IsNullOrWhiteSpace(modulo))
            {
                query += " and E.modulo = @modulo";

                cmd.Parameters.AddWithValue("@modulo", modulo);
            }
            if (!string.IsNullOrWhiteSpace(evento))
            {
                query += " and E.evento = @evento";

                cmd.Parameters.AddWithValue("@evento", evento);
            }
            if (criticidad.HasValue)
            {
                query += " and E.criticidad = @criticidad";

                cmd.Parameters.AddWithValue("@criticidad", criticidad.Value);
            }
            query += " order by E.fecha desc";
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
        public DataTable ObtenerColumna(string columna)
        {
            string query;
            if (columna != "email") query = $"select distinct {columna} from Evento";
            else query = "select distinct U.email from Evento E inner join Usuario U on E.dni_usuario = U.dni order by U.email";

                using (SqlConnection cx = Conexion.ObtenerConexion())
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, cx);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
        }
    }
}
