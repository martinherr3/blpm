using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using DataAccess.Entity;
using Promethee.Utility;

namespace DataAccess
{
    public class CriteriosDA
    {
        private static string ClassName = "CriteriosDA";

        /// <summary>
        /// Selects the specified usuario.
        /// </summary>
        /// <param name="usuario">The usuario.</param>
        /// <returns></returns>
        public static List<CriterioEntity> Select(CriterioEntity criterio)
        {
            try
            {
                List<CriterioEntity> lista = new List<CriterioEntity>();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Criterio_Select";
                    if (criterio.idCriterio > 0)
                        command.Parameters.AddWithValue("@idCriterio", criterio.idCriterio);
                    if (criterio.idModelo > 0)
                        command.Parameters.AddWithValue("@idModelo", criterio.idModelo);
                    if (!string.IsNullOrEmpty(criterio.nombre))
                        command.Parameters.AddWithValue("@nombre", criterio.nombre);

                    command.CommandTimeout = 10;

                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    CriterioEntity objEntidad = null;
                    while (reader.Read())
                    {
                        objEntidad = new CriterioEntity();
                        objEntidad.idCriterio = Convert.ToInt32(reader["idCriterio"]);
                        objEntidad.nombre = reader["nombre"].ToString();
                        objEntidad.idModelo = Convert.ToInt32(reader["idModelo"]);
                        objEntidad.pesoDefault = Convert.ToDecimal(reader["pesoDefault"]);
                        objEntidad.maximiza = (bool)(reader["maximiza"]);
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
        /// Creates the specified nuevo Criterio.
        /// </summary>
        /// <param name="nuevoCriterio">The nuevo Criterio.</param>
        /// <exception cref="CustomizedException">
        /// </exception>
        public static void Save(CriterioEntity nuevoCriterio)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Criterio_Update";
                    if (nuevoCriterio.idCriterio == 0)
                        command.CommandText = "Criterio_Insert";
                    command.Parameters.AddWithValue("@idCriterio", nuevoCriterio.idCriterio);
                    command.Parameters.AddWithValue("@idModelo", nuevoCriterio.idModelo);
                    command.Parameters.AddWithValue("@nombre", nuevoCriterio.nombre);
                    command.Parameters.AddWithValue("@pesoDefault", nuevoCriterio.pesoDefault);
                    command.Parameters.AddWithValue("@maximiza", nuevoCriterio.maximiza);

                    command.CommandTimeout = 10;

                    conn.Open();

                    int idCriterio = Convert.ToInt32(command.ExecuteScalar());

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

        /// <summary>
        /// Saves the specified nueva entidad.
        /// </summary>
        /// <param name="nuevaEntidad">The nueva entidad.</param>
        /// <param name="listaConfig">The lista config.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public static void Save(CriterioEntity nuevoCriterio, List<ConfigFuncionPreferenciaEntity> listaConfig)
        {
            SqlTransaction transaccion = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Criterio_Update";

                    if (nuevoCriterio.idCriterio == 0)
                        command.CommandText = "Criterio_Insert";
                    command.Parameters.AddWithValue("@idCriterio", nuevoCriterio.idCriterio);
                    command.Parameters.AddWithValue("@idModelo", nuevoCriterio.idModelo);
                    command.Parameters.AddWithValue("@nombre", nuevoCriterio.nombre);
                    command.Parameters.AddWithValue("@pesoDefault", nuevoCriterio.pesoDefault);
                    command.Parameters.AddWithValue("@maximiza", nuevoCriterio.maximiza);

                    command.CommandTimeout = 10;

                    conn.Open();
                    transaccion = conn.BeginTransaction();
                    command.Transaction = transaccion;
                    int idCriterio = Convert.ToInt32(command.ExecuteScalar());
                    idCriterio = nuevoCriterio.idCriterio > 0 ? nuevoCriterio.idCriterio : idCriterio;
                    
                    //borro todo lo que exista, sirve para el update
                    command.CommandText = "ConfigFuncionPreferencia_Delete";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@idCriterio", idCriterio);
                    command.ExecuteNonQuery();

                    command.CommandText = "ConfigFuncionPreferencia_Insert";
                    foreach (ConfigFuncionPreferenciaEntity item in listaConfig)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@idCriterio", idCriterio);
                        command.Parameters.AddWithValue("@idFuncionPreferencia", item.idFuncionPreferencia);
                        command.Parameters.AddWithValue("@idValorFuncionPreferencia", item.idValorFuncionPreferencia);
                        command.Parameters.AddWithValue("@valorDefault", item.valorDefault);
                        command.ExecuteNonQuery();
                    }
                    transaccion.Commit();
                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                if (transaccion != null)
                    transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - Save()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                if (transaccion != null)
                    transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - Save()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
    }
}
