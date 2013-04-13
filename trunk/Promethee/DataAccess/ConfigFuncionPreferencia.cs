using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using DataAccess.Entity;
using Promethee.Utility;

namespace DataAccess
{
    public class ConfigFuncionPreferenciaDA
    {
        private static string ClassName = "ConfigFuncionPreferenciaDA";

        /// <summary>
        /// Selects the specified config funcion preferencia.
        /// </summary>
        /// <param name="ConfigFuncionPreferencia">The config funcion preferencia.</param>
        /// <returns></returns>
        /// <exception cref="CustomizedException">
        /// </exception>
        public static List<ConfigFuncionPreferenciaEntity> Select(ConfigFuncionPreferenciaEntity ConfigFuncionPreferencia, CriterioEntity criterio)
        {
            try
            {
                List<ConfigFuncionPreferenciaEntity> lista = new List<ConfigFuncionPreferenciaEntity>();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "ConfigFuncionPreferencia_Select";
                    if (ConfigFuncionPreferencia.idConfigFuncionPreferencia > 0)
                        command.Parameters.AddWithValue("@idConfigFuncionPreferencia", ConfigFuncionPreferencia.idConfigFuncionPreferencia);
                    if (criterio.idModelo > 0)
                        command.Parameters.AddWithValue("@idModelo", criterio.idModelo);

                    command.CommandTimeout = 10;

                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    ConfigFuncionPreferenciaEntity objEntidad = null;
                    while (reader.Read())
                    {
                        objEntidad = new ConfigFuncionPreferenciaEntity();
                        objEntidad.idConfigFuncionPreferencia = Convert.ToInt32(reader["idConfigFuncionPreferencia"]);
                        objEntidad.idCriterio = Convert.ToInt32(reader["idCriterio"]);
                        objEntidad.idFuncionPreferencia = Convert.ToInt32(reader["idFuncionPreferencia"]);
                        objEntidad.idValorFuncionPreferencia = Convert.ToInt32(reader["idValorFuncionPreferencia"]);
                        objEntidad.valorDefault = Convert.ToDecimal(reader["valorDefault"]);
                        
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
    }
}
