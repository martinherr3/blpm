using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

namespace EDUAR_DataAccess.Common
{
    public class DAPlanificacionAnual : DataAccesBase<PlanificacionAnual>
    {
        #region --[Atributos]--
        private const string ClassName = "DAPlanificacionAnual";
        #endregion

        #region --[Constructor]--
        public DAPlanificacionAnual()
        {
        }

        public DAPlanificacionAnual(DATransaction objDATransaction)
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

        public override PlanificacionAnual GetById(PlanificacionAnual entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(PlanificacionAnual entidad)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates the specified entidad.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <param name="identificador">The identificador.</param>
        /// <exception cref="CustomizedException">
        /// </exception>
        public override void Create(PlanificacionAnual entidad, out int identificador)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("PlanificacionAnual_Insert");

                Transaction.DataBase.AddOutParameter(Transaction.DBcomand, "@idPlanificacionAnual", DbType.Int32, 2);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCurricula", DbType.Int32, entidad.curricula.idCurricula);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCicloLectivo", DbType.Int32, entidad.cicloLectivo.idCicloLectivo);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@usuarioCreador", DbType.String, entidad.creador.username);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaCreacion", DbType.Date, DateTime.Now);
                if (entidad.fechaAprobada.HasValue
                    && ValidarFechaSQL(Convert.ToDateTime(entidad.fechaAprobada)))
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaAprobada", DbType.Date, entidad.fechaAprobada);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@observaciones", DbType.String, entidad.observaciones);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@solicitarAprobacion", DbType.Boolean, entidad.solicitarAprobacion);

                if (Transaction.Transaction != null)
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                else
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

                identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idPlanificacionAnual"].Value.ToString());

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
        public override void Update(PlanificacionAnual entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("PlanificacionAnual_Update");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPlanificacionAnual", DbType.Int32, entidad.idPlanificacionAnual);
                //Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCreador", DbType.String, entidad.creador.idPersona);
                if (ValidarFechaSQL(entidad.fechaCreacion))
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaCreacion", DbType.Date, entidad.fechaCreacion);
                if (entidad.fechaAprobada.HasValue
                    && ValidarFechaSQL(Convert.ToDateTime(entidad.fechaAprobada)))
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaAprobada", DbType.Date, entidad.fechaAprobada);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@observaciones", DbType.String, entidad.observaciones);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@solicitarAprobacion", DbType.Boolean, entidad.solicitarAprobacion);

                if (Transaction.Transaction != null)
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                else
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Update()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Update()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        public override void Delete(PlanificacionAnual entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        /// <summary>
        /// Gets the planificacion.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        /// <exception cref="CustomizedException">
        /// </exception>
        public List<PlanificacionAnual> GetPlanificacion(PlanificacionAnual entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("PlanificacionAnual_Select");
                if (entidad != null)
                {
                    if (entidad.curricula.idCurricula > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCurricula", DbType.Int32, entidad.curricula.idCurricula);
                    if (entidad.idPlanificacionAnual > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPlanificacionAnual", DbType.Int32, entidad.idPlanificacionAnual);
                    if (entidad.cicloLectivo.idCicloLectivo > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCicloLectivo", DbType.Int32, entidad.cicloLectivo.idCicloLectivo);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<PlanificacionAnual> listaEntidad = new List<PlanificacionAnual>();
                PlanificacionAnual objEntidad;
                DateTime fecha = new DateTime();
                while (reader.Read())
                {
                    objEntidad = new PlanificacionAnual();
                    objEntidad.idPlanificacionAnual = Convert.ToInt32(reader["idPlanificacionAnual"]);
                    objEntidad.cicloLectivo.idCicloLectivo = Convert.ToInt32(reader["idCicloLectivo"]);
                    objEntidad.cicloLectivo.nombre = reader["cicloLectivo"].ToString();
                    objEntidad.creador = new Persona() { idPersona = Convert.ToInt32(reader["idCreador"]) };

                    if (DateTime.TryParse(reader["fechaAprobada"].ToString(), out fecha))
                        objEntidad.fechaAprobada = fecha;

                    if (DateTime.TryParse(reader["fechaCreacion"].ToString(), out fecha))
                        objEntidad.fechaCreacion = fecha;

                    objEntidad.observaciones = reader["observaciones"].ToString();
                    objEntidad.solicitarAprobacion = Convert.ToBoolean(reader["solicitarAprobacion"]);
                    objEntidad.curricula.idCurricula = Convert.ToInt32(reader["idCurricula"]);
                    objEntidad.curricula.asignatura.idAsignatura = Convert.ToInt32(reader["idAsignatura"]);
                    objEntidad.curricula.asignatura.nombre = reader["Asignatura"].ToString();
                    objEntidad.curricula.nivel.idNivel = Convert.ToInt32(reader["idNivel"]);
                    objEntidad.curricula.nivel.nombre = reader["Nivel"].ToString();
                    objEntidad.curricula.orientacion.idOrientacion = Convert.ToInt32(reader["idOrientacion"]);
                    objEntidad.curricula.orientacion.nombre = reader["Orientacion"].ToString();
                    listaEntidad.Add(objEntidad);
                }
                return listaEntidad;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetPlanificacion()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetPlanificacion()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }


        /// <summary>
        /// Deletes the cursos planificacion.
        /// </summary>
        /// <param name="idPlanificacionAnual">The id planificacion anual.</param>
        /// <exception cref="CustomizedException">
        /// </exception>
        public void DeleteCursosPlanificacion(int idPlanificacionAnual)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("RelPlanificacionCurso_Delete");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPlanificacionAnual", DbType.Int32, idPlanificacionAnual);

                if (Transaction.Transaction != null)
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                else
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - DeleteCursosPlanificacion()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - DeleteCursosPlanificacion()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Saves the cursos.
        /// </summary>
        /// <param name="idPlanificacionAnual">The id planificacion anual.</param>
        /// <param name="idCursoCicloLectivo">The id curso ciclo lectivo.</param>
        /// <exception cref="CustomizedException">
        /// </exception>
        public void SaveCursos(int idPlanificacionAnual, int idCursoCicloLectivo)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("RelPlanificacionCurso_Insert");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPlanificacionAnual", DbType.Int32, idPlanificacionAnual);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCursoCicloLectivo", DbType.Int32, idCursoCicloLectivo);

                if (Transaction.Transaction != null)
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                else
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - SaveCursos()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - SaveCursos()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Gets the cursos.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        /// <exception cref="CustomizedException">
        /// </exception>
        public List<CursoCicloLectivo> GetCursos(PlanificacionAnual entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("RelPlanificacionCurso_Select");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPlanificacionAnual", DbType.Int32, entidad.idPlanificacionAnual);

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<CursoCicloLectivo> listaEntidad = new List<CursoCicloLectivo>();
                CursoCicloLectivo objEntidad;
                while (reader.Read())
                {
                    objEntidad = new CursoCicloLectivo();
                    objEntidad.idCursoCicloLectivo = Convert.ToInt32(reader["idCursoCicloLectivo"]);
                    objEntidad.curso.nombre = reader["curso"].ToString();
                    listaEntidad.Add(objEntidad);
                }
                return listaEntidad;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetCursos()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetCursos()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
        /// <summary>
        /// Gets the planificacion.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        /// <exception cref="CustomizedException">
        /// </exception>
        public List<PlanificacionAnual> GetPlanificacion(CicloLectivo cicloLectivoActual)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("PlanificacionAnual_Select");

                if (cicloLectivoActual != null && cicloLectivoActual.idCicloLectivo > 0)
                {
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCicloLectivo", DbType.Int32, cicloLectivoActual.idCicloLectivo);
                }

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<PlanificacionAnual> listaEntidad = new List<PlanificacionAnual>();
                PlanificacionAnual objEntidad;
                while (reader.Read())
                {
                    objEntidad = new PlanificacionAnual();
                    objEntidad.idPlanificacionAnual = Convert.ToInt32(reader["idPlanificacionAnual"]);
                    objEntidad.creador = new Persona() { idPersona = Convert.ToInt32(reader["idCreador"]) };
                    if (reader["fechaAprobada"] != DBNull.Value)
                    {
                        objEntidad.fechaAprobada = Convert.ToDateTime(reader["fechaAprobada"]);
                    }
                    objEntidad.fechaCreacion = Convert.ToDateTime(reader["fechaCreacion"]);
                    if (reader["observaciones"] != DBNull.Value)
                    {
                        objEntidad.observaciones = reader["observaciones"].ToString();
                    }
                    objEntidad.cicloLectivo.idCicloLectivo = Convert.ToInt32(reader["idCicloLectivo"]);
                    objEntidad.cicloLectivo.nombre = reader["cicloLectivo"].ToString();
                    objEntidad.solicitarAprobacion = Convert.ToBoolean(reader["solicitarAprobacion"]);
                    objEntidad.curricula.idCurricula = Convert.ToInt32(reader["idCurricula"]);
                    objEntidad.curricula.asignatura.idAsignatura = Convert.ToInt32(reader["idAsignatura"]);
                    objEntidad.curricula.asignatura.nombre = reader["Asignatura"].ToString();
                    objEntidad.curricula.nivel.idNivel = Convert.ToInt32(reader["idNivel"]);
                    objEntidad.curricula.nivel.nombre = reader["Nivel"].ToString();
                    objEntidad.curricula.orientacion.idOrientacion = Convert.ToInt32(reader["idOrientacion"]);
                    objEntidad.curricula.orientacion.nombre = reader["Orientacion"].ToString();
                    objEntidad.creador.nombre = reader["nombreCreador"].ToString();
                    objEntidad.creador.apellido = reader["apellidoCreador"].ToString();
                    listaEntidad.Add(objEntidad);
                }
                return listaEntidad;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetPlanificacion()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetPlanificacion()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
        #endregion

    }
}

