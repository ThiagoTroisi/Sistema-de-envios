using Microsoft.Data.SqlClient;
using Servicios.GestionIdiomas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class IdiomaDAL
    {
        public List<Idioma> ObtenerIdiomas()
        {
            List<Idioma> idiomas = new List<Idioma>();
            string query = "select id_idioma, nombre from Idioma";
            using (SqlConnection cx = Conexion.ObtenerConexion())
            {
                cx.Open();
                using (SqlCommand cmd = new SqlCommand(query, cx))
                {
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            idiomas.Add(new Idioma(Convert.ToInt32(r["id_idioma"]), r["nombre"].ToString()));
                        }
                    }
                }
            }
            return idiomas;
        }
        public Idioma ObtenerPorId(int id)
        {
            Idioma idioma = null;

            string query = "select * from Idioma where id_idioma = @id";

            using (SqlConnection cx = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cx))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cx.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        idioma = new Idioma((int)r["id_idioma"], r["nombre"].ToString());
                    }
                }
            }
            return idioma;
        }
    }
}
