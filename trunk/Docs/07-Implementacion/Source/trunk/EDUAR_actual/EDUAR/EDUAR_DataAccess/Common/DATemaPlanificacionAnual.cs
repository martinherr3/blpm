using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System.Collections.Generic;
using System.Data;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;
using System.Data.SqlClient;
using EDUAR_Utility.Constantes;

namespace EDUAR_DataAccess.Common
{
    public class DATemaPlanificacionAnual : DataAccesBase<TemaPlanificacionAnual>
    {
        #region --[Atributos]--
        private const string ClassName = "DATemasPlanificadosAsignatura";
        #endregion

        #region --[Constructor]--
        public DATemaPlanificacionAnual()
        {
        }

        public DATemaPlanificacionAnual(DATransaction objDATransaction)
            : base(objDATransaction)
        {

        }
        #endregion

        #region --[Implementación métodos heredados]--
        public override string FieldID
        {
            get { throw new NotImplementedException(); }
        }

        public override string FieldDescription
        {
            get { throw new NotImplementedException(); }
        }

        public override TemaPlanificacionAnual GetById(TemaPlanificacionAnual entidad)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates the specified entidad.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        public override void Create(TemaPlanificacionAnual entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TemaPlanificacionAnual_Insert");

                Transaction.DataBase.AddOutParameter(Transaction.DBcomand, "@idTemaPlanificacion", DbType.Int32, 2);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPlanificacionAnual", DbType.Int32, entidad.idPlanificacionAnual);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@contenidosConceptuales", DbType.String, entidad.contenidosConceptuales);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@contenidosProcedimentales", DbType.String, entidad.contenidosProcedimentales);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@contenidosActitudinales", DbType.String, entidad.contenidosActitudinales);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@estrategiasAprendizaje", DbType.String, entidad.estrategiasAprendizaje);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@criteriosEvaluacion", DbType.String, entidad.criteriosEvaluacion);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@instrumentosEvaluacion", DbType.String, entidad.instrumentosEvaluacion);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaInicioEstimada", DbType.Date, entidad.fechaFinEstimada);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaFinEstimada", DbType.Date, entidad.fechaFinEstimada);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@observaciones", DbType.String, entidad.observaciones);

                if (Transaction.Transaction != null)
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                else
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Create()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Create()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Creates the specified entidad.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <param name="identificador">The identificador.</param>
        public override void Create(TemaPlanificacionAnual entidad, out int identificador)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TemaPlanificacionAnual_Insert");

                Transaction.DataBase.AddOutParameter(Transaction.DBcomand, "@idTemaPlanificacion", DbType.Int32, 2);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPlanificacionAnual", DbType.Int32, entidad.idPlanificacionAnual);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@contenidosConceptuales", DbType.String, entidad.contenidosConceptuales);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@contenidosProcedimentales", DbType.String, entidad.contenidosProcedimentales);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@contenidosActitudinales", DbType.String, entidad.contenidosActitudinales);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@estrategiasAprendizaje", DbType.String, entidad.estrategiasAprendizaje);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@criteriosEvaluacion", DbType.String, entidad.criteriosEvaluacion);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@instrumentosEvaluacion", DbType.String, entidad.instrumentosEvaluacion);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaInicioEstimada", DbType.Date, entidad.fechaInicioEstimada);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaFinEstimada", DbType.Date, entidad.fechaFinEstimada);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@observaciones", DbType.String, entidad.observaciones);

                if (Transaction.Transaction != null)
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                else
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

                identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idTemaPlanificacion"].Value.ToString());

            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Create()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Create()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Updates the specified entidad.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        public override void Update(TemaPlanificacionAnual entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TemaPlanificacionAnual_Update");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTemaPlanificacion", DbType.Int32, entidad.idTemaPlanificacion);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPlanificacionAnual", DbType.Int32, entidad.idPlanificacionAnual);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@contenidosConceptuales", DbType.String, entidad.contenidosConceptuales);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@contenidosProcedimentales", DbType.String, entidad.contenidosProcedimentales);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@contenidosActitudinales", DbType.String, entidad.contenidosActitudinales);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@estrategiasAprendizaje", DbType.String, entidad.estrategiasAprendizaje);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@criteriosEvaluacion", DbType.String, entidad.criteriosEvaluacion);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@instrumentosEvaluacion", DbType.String, entidad.instrumentosEvaluacion);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaInicioEstimada", DbType.Date, entidad.fechaInicioEstimada);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaFinEstimada", DbType.Date, entidad.fechaFinEstimada);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@observaciones", DbType.String, entidad.observaciones);

                if (Transaction.Transaction != null)
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                else
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
            }
            catch (SqlException ex)
            {
                if (ex.Number == BLConstantesGenerales.ConcurrencyErrorNumber)
                    throw new CustomizedException("No se puede modificar la planificación {0}, debido a que otro usuario lo ha modificado."
                        , ex, enuExceptionType.ConcurrencyException);

                throw new CustomizedException(string.Format("Fallo en {0} - Update()", ClassName),
                                                      ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Update()", ClassName),
                                                      ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Deletes the specified entidad.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        public override void Delete(TemaPlanificacionAnual entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TemaPlanificacionAnual_Delete");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTemaPlanificacion", DbType.Int32, entidad.idTemaPlanificacion);

                if (Transaction.Transaction != null)
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                else
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Delete()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Delete()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
        #endregion

        #region --[Métodos Públicos]--
        /// <summary>
        /// Gets the temas planificacion anual.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<TemaPlanificacionAnual> GetTemasPlanificacionAnual(PlanificacionAnual entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TemaPlanificacionAnual_Select");
                if (entidad != null)
                {
                    if (entidad.idPlanificacionAnual > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPlanificacionAnual", DbType.Int32, entidad.idPlanificacionAnual);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<TemaPlanificacionAnual> listaEntidad = new List<TemaPlanificacionAnual>();
                TemaPlanificacionAnual objEntidad;
                while (reader.Read())
                {
                    objEntidad = new TemaPlanificacionAnual();
                    DateTime objFecha;
                    if (DateTime.TryParse(reader["fechaAprobada"].ToString(), out objFecha))
                        objEntidad.fechaAprobada = objFecha;
                    else
                        objEntidad.fechaAprobada = null;

                    objEntidad.fechaInicioEstimada = Convert.ToDateTime(reader["fechaInicioEstimada"]);
                    objEntidad.fechaFinEstimada = Convert.ToDateTime(reader["fechaFinEstimada"]);
                    objEntidad.idPlanificacionAnual = Convert.ToInt32(reader["idPlanificacionAnual"]);
                    objEntidad.contenidosActitudinales = reader["contenidosActitudinales"].ToString();
                    objEntidad.contenidosConceptuales = reader["contenidosConceptuales"].ToString();
                    objEntidad.contenidosProcedimentales = reader["contenidosProcedimentales"].ToString();
                    objEntidad.criteriosEvaluacion = reader["criteriosEvaluacion"].ToString();
                    objEntidad.estrategiasAprendizaje = reader["estrategiasAprendizaje"].ToString();
                    objEntidad.instrumentosEvaluacion = reader["instrumentosEvaluacion"].ToString();
                    objEntidad.idTemaPlanificacion = Convert.ToInt32(reader["idTemaPlanificacion"]);
                    objEntidad.observaciones = reader["observaciones"].ToString();
                    listaEntidad.Add(objEntidad);
                }
                return listaEntidad;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetTemasPlanificacionAnual()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetTemasPlanificacionAnual()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Gets the temas planificacion anual which are behind the schedule.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<TemaPlanificacionAnual> GetTemasPlanificacionAnualAtrasados(/*PlanificacionAnual entidad*/ int idCursoCicloLectivo, int idAsignaturaCicloLectivo)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TemaPlanificacionAnualAtrasados_Select");

                if (idCursoCicloLectivo > 0)
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCursoCicloLectivo", DbType.Int32, idCursoCicloLectivo);
                if (idAsignaturaCicloLectivo > 0)
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignaturaCicloLectivo", DbType.Int32, idAsignaturaCicloLectivo);


                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<TemaPlanificacionAnual> listaEntidad = new List<TemaPlanificacionAnual>();
                TemaPlanificacionAnual objEntidad;
                while (reader.Read())
                {
                    objEntidad = new TemaPlanificacionAnual();
                    DateTime objFecha;
                    if (DateTime.TryParse(reader["fechaAprobada"].ToString(), out objFecha))
                        objEntidad.fechaAprobada = objFecha;
                    else
                        objEntidad.fechaAprobada = null;

                    objEntidad.fechaInicioEstimada = Convert.ToDateTime(reader["fechaInicioEstimada"]);
                    objEntidad.fechaFinEstimada = Convert.ToDateTime(reader["fechaFinEstimada"]);
                    objEntidad.idPlanificacionAnual = Convert.ToInt32(reader["idPlanificacionAnual"]);
                    objEntidad.contenidosActitudinales = reader["contenidosActitudinales"].ToString();
                    objEntidad.contenidosConceptuales = reader["contenidosConceptuales"].ToString();
                    objEntidad.contenidosProcedimentales = reader["contenidosProcedimentales"].ToString();
                    objEntidad.criteriosEvaluacion = reader["criteriosEvaluacion"].ToString();
                    objEntidad.estrategiasAprendizaje = reader["estrategiasAprendizaje"].ToString();
                    objEntidad.instrumentosEvaluacion = reader["instrumentosEvaluacion"].ToString();
                    objEntidad.idTemaPlanificacion = Convert.ToInt32(reader["idTemaPlanificacion"]);
                    objEntidad.observaciones = reader["observaciones"].ToString();
                    //objEntidad.asignatura.nombre = reader["Asignatura"].ToString();
                    listaEntidad.Add(objEntidad);
                }
                return listaEntidad;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetTemasPlanificacionAnual()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetTemasPlanificacionAnual()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }


        /// <summary>
        /// Deletes the contenidos.
        /// </summary>
        /// <param name="idTemaPlanificacion">The id tema planificacion.</param>
        public void DeleteContenidos(int idTemaPlanificacion)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TemaPlanificacionTemaContenido_Delete");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTemaPlanificacion", DbType.Int32, idTemaPlanificacion);

                if (Transaction.Transaction != null)
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                else
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - DeleteContenidos()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - DeleteContenidos()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Saves the contenidos.
        /// </summary>
        /// <param name="p">The idTemaPlanificacion.</param>
        /// <param name="p_2">The idTemaContenido.</param>
        public void SaveContenidos(int idTemaPlanificacion, int idTemaContenido)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TemaPlanificacionTemaContenido_Insert");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTemaPlanificacion", DbType.Int32, idTemaPlanificacion);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTemaContenido", DbType.Int32, idTemaContenido);

                if (Transaction.Transaction != null)
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                else
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - SaveContenidos()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - SaveContenidos()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Gets the contenidos.
        /// </summary>
        /// <param name="Data">The data.</param>
        /// <returns></returns>
        public List<TemaContenido> GetContenidos(TemaPlanificacionAnual entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TemaPlanificacionTemaContenido_Select");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTemaPlanificacion", DbType.Int32, entidad.idTemaPlanificacion);
                //Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTemaContenido", DbType.Int32, entidad.idTemaContenido);

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<TemaContenido> listaEntidad = new List<TemaContenido>();
                TemaContenido objEntidad;
                while (reader.Read())
                {
                    objEntidad = new TemaContenido();
                    objEntidad.idTemaContenido = Convert.ToInt32(reader["idTemaContenido"]);
                    objEntidad.obligatorio = Convert.ToBoolean(reader["obligatorio"]);
                    //objEntidad.activo = Convert.ToBoolean(reader["activo"]);
                    listaEntidad.Add(objEntidad);
                }
                return listaEntidad;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetContenidos()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetContenidos()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Gets the contenidos desactivados.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        /// <exception cref="CustomizedException">
        /// </exception>
        public List<TemaContenido> GetContenidosDesactivados(TemaPlanificacionAnual entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TemaPlanificacionTemaContenidoDesactivado_Select");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTemaPlanificacion", DbType.Int32, entidad.idTemaPlanificacion);

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<TemaContenido> listaEntidad = new List<TemaContenido>();
                TemaContenido objEntidad;
                while (reader.Read())
                {
                    objEntidad = new TemaContenido();
                    objEntidad.idTemaContenido = Convert.ToInt32(reader["idTemaContenido"]);
                    objEntidad.obligatorio = Convert.ToBoolean(reader["obligatorio"]);
                    objEntidad.activo = Convert.ToBoolean(reader["activo"]);
                    listaEntidad.Add(objEntidad);
                }
                return listaEntidad;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetContenidosDesactivados()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetContenidosDesactivados()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
        #endregion
    }
}
