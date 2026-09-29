using BE;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class PersonaDAL
    {
        public PersonaBE ConsultaPorDNI(int dni)
        {
            PersonaBE persona = null;

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "select dni, nombre, apellido, telefono, email from Persona where dni = @dni";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@dni", dni);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            persona = new PersonaBE(
                                Convert.ToInt32(reader["dni"]),
                                reader["nombre"].ToString(),
                                reader["apellido"].ToString(),
                                reader["telefono"].ToString(),
                                reader["email"].ToString()
                            );
                        }
                    }
                }
            }

            return persona;
        }

        public bool EstaActiva(int dni)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "select estado from Persona where dni = @dni";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@dni", dni);

                    object resultado = comando.ExecuteScalar();

                    if (resultado == null) return false;

                    return Convert.ToBoolean(resultado);
                }
            }
        }

        public List<PersonaBE> ObtenerPersonas(bool todos)
        {
            List<PersonaBE> personas = new List<PersonaBE>();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = todos
                    ? "select dni, nombre, apellido, telefono, email from Persona"
                    : "select dni, nombre, apellido, telefono, email from Persona where estado = 1";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PersonaBE persona = new PersonaBE(
                                Convert.ToInt32(reader["dni"]),
                                reader["nombre"].ToString(),
                                reader["apellido"].ToString(),
                                reader["telefono"].ToString(),
                                reader["email"].ToString()
                            );

                            personas.Add(persona);
                        }
                    }
                }
            }

            return personas;
        }

        public void AltaPersona(PersonaBE persona)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "insert into Persona (dni, nombre, apellido, telefono, email, estado) values (@dni, @nombre, @apellido, @telefono, @email, 1)";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@dni", persona.DNI);
                    comando.Parameters.AddWithValue("@nombre", persona.Nombre);
                    comando.Parameters.AddWithValue("@apellido", persona.Apellido);
                    comando.Parameters.AddWithValue("@telefono", persona.Telefono);
                    comando.Parameters.AddWithValue("@email", persona.Email);

                    comando.ExecuteNonQuery();
                }
            }
        }

        public void ModificarPersona(PersonaBE persona)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "update Persona set nombre = @nombre, apellido = @apellido, telefono = @telefono, email = @email where dni = @dni";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@dni", persona.DNI);
                    comando.Parameters.AddWithValue("@nombre", persona.Nombre);
                    comando.Parameters.AddWithValue("@apellido", persona.Apellido);
                    comando.Parameters.AddWithValue("@telefono", persona.Telefono);
                    comando.Parameters.AddWithValue("@email", persona.Email);

                    comando.ExecuteNonQuery();
                }
            }
        }

        public void BajaPersona(int dni)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "update Persona set estado = 0 where dni = @dni";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@dni", dni);
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void ReactivarPersona(int dni)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "update Persona set estado = 1 where dni = @dni";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@dni", dni);
                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}