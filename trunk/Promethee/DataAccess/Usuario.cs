using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using DataAccess.Entity;
using Promethee.Utility;
using System.Data;

namespace DataAccess
{
    public class UsuariosDA
    {
        private static string ClassName = "UsuariosDA";

        /// <summary>
        /// Selects the specified usuario.
        /// </summary>
        /// <param name="usuario">The usuario.</param>
        /// <returns></returns>
        public static List<UsuarioEntity> Select()
        {
            try
            {
                List<UsuarioEntity> lista = new List<UsuarioEntity>();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Usuarios_Select";
                    //command.Parameters.AddWithValue("@username", usuario.username);
                    //command.Parameters.AddWithValue("@idUsuario", DBNull.Value);
                    //command.Parameters.AddWithValue("@fechaCreacion", DBNull.Value);
                    //command.Parameters.AddWithValue("@nombre", DBNull.Value);

                    command.CommandTimeout = 10;

                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    UsuarioEntity objEntidad = null;
                    while (reader.Read())
                    {
                        objEntidad = new UsuarioEntity();
                        objEntidad.idUsuario = Convert.ToInt32(reader["idUsuario"]);
                        objEntidad.username = reader["username"].ToString();
                        objEntidad.nombre = reader["nombre"].ToString();
                        objEntidad.apellido = reader["apellido"].ToString();

                        lista.Add(objEntidad);
                    }
                    conn.Close();
                    return lista;
                }
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - Select()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - Select()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        public static void Delete(int idUsuario)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Usuarios_Delete";
                    command.Parameters.AddWithValue("@idUsuario", idUsuario);

                    command.CommandTimeout = 10;

                    conn.Open();

                    command.ExecuteScalar();

                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - Delete()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - Delete()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
    }
}
