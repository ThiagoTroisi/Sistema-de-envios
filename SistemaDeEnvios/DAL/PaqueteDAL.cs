using BE;
using Microsoft.Data.SqlClient;
using System;

namespace DAL
{
    public class PaqueteDAL
    {
        public PaqueteBE ConsultaPorId(int idPaquete)
        {
            PaqueteBE paquete = null;

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "select id_paquete, descripcion, peso, alto, ancho, largo from Paquete where id_paquete = @id_paquete";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@id_paquete", idPaquete);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            paquete = new PaqueteBE(
                                Convert.ToInt32(reader["id_paquete"]),
                                reader["descripcion"].ToString(),
                                Convert.ToDecimal(reader["peso"]),
                                Convert.ToDecimal(reader["alto"]),
                                Convert.ToDecimal(reader["ancho"]),
                                Convert.ToDecimal(reader["largo"])
                            );
                        }
                    }
                }
            }

            return paquete;
        }

        public int AltaPaquete(PaqueteBE paquete)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "insert into Paquete (descripcion, peso, alto, ancho, largo) values (@descripcion, @peso, @alto, @ancho, @largo); select cast(scope_identity() as int)";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@descripcion", paquete.Descripcion);
                    comando.Parameters.AddWithValue("@peso", paquete.Peso);
                    comando.Parameters.AddWithValue("@alto", paquete.Alto);
                    comando.Parameters.AddWithValue("@ancho", paquete.Ancho);
                    comando.Parameters.AddWithValue("@largo", paquete.Largo);

                    return Convert.ToInt32(comando.ExecuteScalar());
                }
            }
        }
    }
}