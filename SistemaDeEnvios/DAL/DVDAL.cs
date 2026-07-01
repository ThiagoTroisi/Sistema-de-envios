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

        public string CalcularDVV(string tabla)
        {
            string concatenacion = ObtenerConcatenacionDVH(tabla);

            if (string.IsNullOrWhiteSpace(concatenacion)) return null;

            return CalcularHash(concatenacion);
        }

        public void ActualizarDVV(string tabla)
        {
            string dvv = CalcularDVV(tabla);

            if (dvv == null) throw new Exception("No se pudo calcular DVV");

            GuardarDVV(tabla, dvv);
        }
    }
}