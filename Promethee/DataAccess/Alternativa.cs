using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using DataAccess.Entity;
using Promethee.Utility;

namespace DataAccess
{
    public class AlternativasDA
    {
        private static string ClassName = "AlternativasDA";

        /// <summary>
        /// Selects the specified usuario.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        /// <exception cref="CustomizedException">
        /// </exception>
        public static List<AlternativaEntity> Select(AlternativaEntity entidad)
        {
            try
            {
                List<AlternativaEntity> lista = new List<AlternativaEntity>();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Alternativa_Select";
                    if (entidad.idAlternativa > 0)
                        command.Parameters.AddWithValue("@idAlternativa", entidad.idAlternativa);
                    if (entidad.idModelo > 0)
                        command.Parameters.AddWithValue("@idModelo", entidad.idModelo);
                    if (!string.IsNullOrEmpty(entidad.nombre))
                        command.Parameters.AddWithValue("@nombre", entidad.nombre);

                    command.CommandTimeout = 10;

                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    AlternativaEntity objEntidad = null;
                    while (reader.Read())
                    {
                        objEntidad = new AlternativaEntity();
                        objEntidad.idAlternativa = Convert.ToInt32(reader["idAlternativa"]);
                        objEntidad.nombre = reader["nombre"].ToString();
                        objEntidad.idModelo = Convert.ToInt32(reader["idModelo"]);
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

        /// <summary>
        /// Creates the specified nuevo Alternativa.
        /// </summary>
        /// <param name="nuevoAlternativa">The nuevo Alternativa.</param>
        /// <exception cref="CustomizedException">
        /// </exception>
        public static void Save(AlternativaEntity nuevoAlternativa)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Alternativa_Update";
                    if (nuevoAlternativa.idAlternativa == 0)
                        command.CommandText = "Alternativa_Insert";
                    command.Parameters.AddWithValue("@idAlternativa", nuevoAlternativa.idAlternativa);
                    command.Parameters.AddWithValue("@idModelo", nuevoAlternativa.idModelo);
                    command.Parameters.AddWithValue("@nombre", nuevoAlternativa.nombre);

                    command.CommandTimeout = 10;

                    conn.Open();

                    int idAlternativa = Convert.ToInt32(command.ExecuteScalar());

                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - Save()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - Save()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
    }
}
