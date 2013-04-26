using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using DataAccess.Entity;
using Promethee.Utility;
using System.Data;

namespace DataAccess
{
    public class ModelosDA
    {
        private static string ClassName = "ModelosDA";

        /// <summary>
        /// Selects the specified usuario.
        /// </summary>
        /// <param name="usuario">The usuario.</param>
        /// <returns></returns>
        public static List<ModeloEntity> Select(UsuarioEntity usuario)
        {
            try
            {
                List<ModeloEntity> lista = new List<ModeloEntity>();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Modelos_Select";
                    command.Parameters.AddWithValue("@username", usuario.username);
                    command.Parameters.AddWithValue("@idModelo", DBNull.Value);
                    command.Parameters.AddWithValue("@fechaCreacion", DBNull.Value);
                    command.Parameters.AddWithValue("@nombre", DBNull.Value);

                    command.CommandTimeout = 10;

                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    ModeloEntity objEntidad = null;
                    while (reader.Read())
                    {
                        objEntidad = new ModeloEntity();
                        objEntidad.idModelo = Convert.ToInt32(reader["idModelo"]);
                        objEntidad.fechaCreacion = Convert.ToDateTime(reader["fechaCreacion"]);
                        objEntidad.username = reader["username"].ToString();
                        objEntidad.nombre = reader["nombre"].ToString();
                        objEntidad.alternativas = Convert.ToInt32(reader["alternativas"]);
                        objEntidad.criterios = Convert.ToInt32(reader["criterios"]);
                        objEntidad.filename = reader["filename"].ToString();

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
        /// Creates the specified nuevo modelo.
        /// </summary>
        /// <param name="nuevoModelo">The nuevo modelo.</param>
        public static void Save(ModeloEntity nuevoModelo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Modelos_Update";
                    if (nuevoModelo.idModelo == 0)
                        command.CommandText = "Modelos_Insert";
                    command.Parameters.AddWithValue("@idModelo", nuevoModelo.idModelo);
                    command.Parameters.AddWithValue("@username", nuevoModelo.username);
                    command.Parameters.AddWithValue("@fechaCreacion", nuevoModelo.fechaCreacion);
                    command.Parameters.AddWithValue("@nombre", nuevoModelo.nombre);

                    command.CommandTimeout = 10;

                    conn.Open();

                    int idModelo = Convert.ToInt32(command.ExecuteScalar());

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
        /// Saves the file.
        /// </summary>
        /// <param name="idModelo">The id modelo.</param>
        /// <param name="FileName">Name of the file.</param>
        /// <exception cref="CustomizedException">
        /// </exception>
        public static void SaveFile(int idModelo, string FileName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Modelos_FileName";
                    command.Parameters.AddWithValue("@idModelo", idModelo);
                    command.Parameters.AddWithValue("@filename", FileName);

                    command.CommandTimeout = 10;

                    conn.Open();

                    command.ExecuteScalar();

                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - SaveFile()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - SaveFile()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Saves the valores.
        /// </summary>
        /// <param name="listaValores">The lista valores.</param>
        /// <exception cref="CustomizedException">
        /// </exception>
        public static void SaveValores(List<RelAlternativaCriterioEntity> listaValores, int idModelo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Rel_AlternativasCriterios_Insert";
                    command.CommandTimeout = 10;
                    conn.Open();

                    foreach (RelAlternativaCriterioEntity item in listaValores)
                    {
                        command.Parameters.AddWithValue("@idModelo", idModelo);
                        command.Parameters.AddWithValue("@nombreAlternativa", item.nombreAlternativa);
                        command.Parameters.AddWithValue("@nombreCriterio", item.nombreCriterio);
                        command.Parameters.AddWithValue("@valor", item.valor);
                        command.ExecuteScalar();
                        command.Parameters.Clear();
                    }
                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - SaveValores()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - SaveValores()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Selects the valores.
        /// </summary>
        /// <param name="idModelo">The id modelo.</param>
        /// <returns></returns>
        /// <exception cref="CustomizedException">
        /// </exception>
        public static List<RelAlternativaCriterioEntity> SelectValores(int idModelo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Rel_AlternativasCriterios_Select";
                    command.CommandTimeout = 10;
                    if (idModelo > 0)
                        command.Parameters.AddWithValue("@idModelo", idModelo);

                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    RelAlternativaCriterioEntity objEntidad = null;
                    List<RelAlternativaCriterioEntity> lista = new List<RelAlternativaCriterioEntity>();
                    while (reader.Read())
                    {
                        objEntidad = new RelAlternativaCriterioEntity();
                        objEntidad.idRelAlternativaCriterio = Convert.ToInt32(reader["idRelAlternativaCriterio"]);
                        objEntidad.idAlternativa = Convert.ToInt32(reader["idAlternativa"]);
                        objEntidad.nombreAlternativa = reader["Alternativa"].ToString();
                        objEntidad.nombreCriterio = reader["Criterio"].ToString();
                        objEntidad.idCriterio = Convert.ToInt32(reader["idCriterio"]);
                        objEntidad.valor = Convert.ToDecimal(reader["valor"]);

                        lista.Add(objEntidad);
                    }
                    conn.Close();
                    return lista;
                }
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - SelectValores()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - SelectValores()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Deletes the specified mi modelo.
        /// </summary>
        /// <param name="miModelo">The mi modelo.</param>
        /// <exception cref="CustomizedException">
        /// </exception>
        public static void Delete(ModeloEntity miModelo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Modelos_Delete";
                    command.Parameters.AddWithValue("@idModelo", miModelo.idModelo);

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

        /// <summary>
        /// Actualizars the valores.
        /// </summary>
        /// <param name="alternativaUPD">The alternativa UPD.</param>
        /// <param name="listaGuardar">The lista guardar.</param>
        /// <exception cref="CustomizedException">
        /// </exception>
        public static void ActualizarValores(AlternativaEntity alternativaUPD, List<RelAlternativaCriterioEntity> listaGuardar)
        {
            SqlTransaction transaccion = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Alternativa_Update";
                    command.Parameters.AddWithValue("@idAlternativa", alternativaUPD.idAlternativa);
                    command.Parameters.AddWithValue("@nombre", alternativaUPD.nombre);
                    command.Parameters.AddWithValue("@idModelo", alternativaUPD.idModelo);

                    command.CommandTimeout = 10;

                    conn.Open();
                    transaccion = conn.BeginTransaction();
                    command.Transaction = transaccion;

                    command.ExecuteNonQuery();

                    foreach (RelAlternativaCriterioEntity item in listaGuardar)
                    {
                        command.Parameters.Clear();

                        if (item.idRelAlternativaCriterio > 0)
                        {
                            command.CommandText = "Rel_AlternativasCriterios_Update";
                            command.Parameters.AddWithValue("@idRelAlternativaCriterio", item.idRelAlternativaCriterio);
                        }
                        else
                        {
                            command.CommandText = "Rel_AlternativasCriterios_Insert";

                            command.Parameters.AddWithValue("@idModelo", alternativaUPD.idModelo);
                            command.Parameters.AddWithValue("@nombreAlternativa", item.nombreAlternativa);
                            command.Parameters.AddWithValue("@nombreCriterio", item.nombreCriterio);
                        }
                        command.Parameters.AddWithValue("@valor", item.valor);
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
                throw new CustomizedException(String.Format("Fallo en {0} - ActualizarValores()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                if (transaccion != null)
                    transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - ActualizarValores()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
    }
}
