using Microsoft.Data.SqlClient;
using System;
using System.Security.Cryptography;
using System.Text;

namespace DAL
{
    public class DVDAL
    {
        public string CalcularHash(string texto)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] datos = Encoding.ASCII.GetBytes(texto);
                byte[] hash = sha.ComputeHash(datos);

                return Convert.ToBase64String(hash);
            }
        }

        public string ObtenerCadenaRegistro(string tabla, string columnaPK, object valorPK)
        {
            StringBuilder cadena = new StringBuilder();

            string query = $"select * from {tabla} where {columnaPK} = @id";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@id", valorPK);

                cx.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (!dr.Read()) return null;

                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string nombreColumna = dr.GetName(i);

                        if (nombreColumna.Equals("dvh", StringComparison.OrdinalIgnoreCase) || nombreColumna.Equals("dvv", StringComparison.OrdinalIgnoreCase))
                            continue;

                        object valor = dr.GetValue(i);

                        if (valor != DBNull.Value) cadena.Append(valor.ToString());
                    }
                }
            }

            return cadena.ToString();
        }

        public void ActualizarDVH(string tabla, string columnaPK, object valorPK)
        {
            string cadena = ObtenerCadenaRegistro(tabla, columnaPK, valorPK);

            if (cadena == null) throw new Exception("No se encontró el registro para calcular DVH");

            string dvh = CalcularHash(cadena);

            string query = $"update {tabla} set dvh = @dvh where {columnaPK} = @id";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@dvh", dvh);
                cmd.Parameters.AddWithValue("@id", valorPK);

                cx.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarTodosLosDVH(string tabla)
        {
            List<Dictionary<string, object>> registros = ObtenerClavesRegistros(tabla);

            foreach (Dictionary<string, object> claves in registros)
            {
                ActualizarDVH(tabla, claves);
            }

            ActualizarDVV(tabla);
        }
        private void ActualizarDVH(string tabla, Dictionary<string, object> claves)
        {
            string cadena = ObtenerCadenaRegistro(tabla, claves);

            string dvh = CalcularHash(cadena);

            string where = string.Join(" AND ", claves.Keys.Select(k => $"{k}=@{k}"));

            string query = $"UPDATE {tabla} SET dvh=@dvh WHERE {where}";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@dvh", dvh);

                foreach (var clave in claves)
                {
                    cmd.Parameters.AddWithValue($"@{clave.Key}", clave.Value);
                }

                cx.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarDVV(string tabla)
        {
            string dvv = CalcularDVV(tabla);

            if (dvv == null) throw new Exception("No se pudo calcular DVV");

            GuardarDVV(tabla, dvv);
        }

        public string CalcularDVV(string tabla)
        {
            string concatenacion = ObtenerConcatenacionDVH(tabla);

            if (string.IsNullOrWhiteSpace(concatenacion)) return null;

            return CalcularHash(concatenacion);
        }

        public string ObtenerConcatenacionDVH(string tabla)
        {
            StringBuilder cadena = new StringBuilder();

            string query = $"select dvh from {tabla}";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cx.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        if (dr["dvh"] != DBNull.Value) cadena.Append(dr["dvh"].ToString());
                    }
                }
            }

            return cadena.ToString();
        }

        public void GuardarDVV(string tabla, string dvv)
        {
            string query = "update DVV set dvv = @dvv where tabla = @tabla";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@dvv", dvv);
                cmd.Parameters.AddWithValue("@tabla", tabla);

                cx.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private List<string> ObtenerColumnasPK(string tabla)
        {
            List<string> columnas = new List<string>();

            string query = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE TABLE_NAME = @tabla ORDER BY ORDINAL_POSITION";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@tabla", tabla);

                cx.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        columnas.Add(dr.GetString(0));
                    }
                }
            }

            return columnas;
        }

        public List<Dictionary<string, object>> ObtenerClavesRegistros(string tabla)
        {
            List<Dictionary<string, object>> registros = new();

            List<string> columnasPK = ObtenerColumnasPK(tabla);

            string query = $"SELECT {string.Join(",", columnasPK)} FROM {tabla}";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cx.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dictionary<string, object> fila = new();

                        foreach (string columna in columnasPK)
                        {
                            fila[columna] = dr[columna];
                        }

                        registros.Add(fila);
                    }
                }
            }

            return registros;
        }

        public string ObtenerCadenaRegistro(string tabla, Dictionary<string, object> claves)
        {
            StringBuilder cadena = new();

            string where = string.Join(" AND ", claves.Keys.Select(k => $"{k}=@{k}"));

            string query = $"SELECT * FROM {tabla} WHERE {where}";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                foreach (var clave in claves)
                {
                    cmd.Parameters.AddWithValue($"@{clave.Key}", clave.Value);
                }

                cx.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (!dr.Read()) return null;

                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string columna = dr.GetName(i);

                        if (columna.Equals("dvh", StringComparison.OrdinalIgnoreCase)) continue;

                        if (columna.Equals("dvv", StringComparison.OrdinalIgnoreCase)) continue;

                        if (dr[i] != DBNull.Value) cadena.Append(dr[i].ToString());
                    }
                }
            }

            return cadena.ToString();
        }

        public string ObtenerDVHGuardado(string tabla, Dictionary<string, object> claves)
        {
            string where = string.Join(" AND ", claves.Keys.Select(k => $"{k}=@{k}"));

            string query = $"SELECT dvh FROM {tabla} WHERE {where}";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                foreach (var clave in claves)
                {
                    cmd.Parameters.AddWithValue($"@{clave.Key}", clave.Value);
                }

                cx.Open();

                object resultado = cmd.ExecuteScalar();

                if (resultado == null || resultado == DBNull.Value)
                    return null;

                return resultado.ToString();
            }
        }

        public string ObtenerDVVGuardado(string tabla)
        {
            string query = "SELECT dvv FROM DVV WHERE tabla = @tabla";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@tabla", tabla);

                cx.Open();

                object resultado = cmd.ExecuteScalar();

                if (resultado == null || resultado == DBNull.Value) return null;

                return resultado.ToString();
            }
        }
    }
}