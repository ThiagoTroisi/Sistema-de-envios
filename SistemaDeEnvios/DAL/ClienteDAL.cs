
using BE;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class ClienteDAL
    {
        public ClienteBE ConsultaPorDNI(int dni)
        {
            ClienteBE cliente = null;

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "select dni, nombre, apellido, telefono, email from Cliente where dni = @dni";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@dni", dni);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cliente = new ClienteBE(
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

            return cliente;
        }

        public bool EstaActiva(int dni)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "select estado from Cliente where dni = @dni";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@dni", dni);

                    object resultado = comando.ExecuteScalar();

                    if (resultado == null || resultado == DBNull.Value)
                        return false;

                    return Convert.ToBoolean(resultado);
                }
            }
        }

        public List<ClienteBE> ObtenerPersonas(bool todos)
        {
            List<ClienteBE> clientes = new List<ClienteBE>();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = todos ? "select dni, nombre, apellido, telefono, email from Cliente" : "select dni, nombre, apellido, telefono, email from Cliente where estado = 1";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ClienteBE cliente = new ClienteBE(
                                Convert.ToInt32(reader["dni"]),
                                reader["nombre"].ToString(),
                                reader["apellido"].ToString(),
                                reader["telefono"].ToString(),
                                reader["email"].ToString()
                            );

                            clientes.Add(cliente);
                        }
                    }
                }
            }

            return clientes;
        }

        public void AltaPersona(ClienteBE persona)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "insert into Cliente (dni, nombre, apellido, telefono, email, estado) values (@dni, @nombre, @apellido, @telefono, @email, 1)";

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

        public void ModificarPersona(ClienteBE persona)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string query = "update Cliente set nombre = @nombre, apellido = @apellido, telefono = @telefono, email = @email where dni = @dni";

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

                string query = "update Cliente set estado = 0 where dni = @dni";

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

                string query = "update Cliente set estado = 1 where dni = @dni";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@dni", dni);
                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}