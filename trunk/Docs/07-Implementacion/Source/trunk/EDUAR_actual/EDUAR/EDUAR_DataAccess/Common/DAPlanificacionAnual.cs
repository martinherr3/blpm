﻿using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCreador", DbType.String, entidad.creador.idPersona);
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
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<PlanificacionAnual> listaEntidad = new List<PlanificacionAnual>();
                PlanificacionAnual objEntidad;
                while (reader.Read())
                {
                    objEntidad = new PlanificacionAnual();
                    objEntidad.idPlanificacionAnual = Convert.ToInt32(reader["idPlanificacionAnual"]);
                    objEntidad.creador = new Persona() { idPersona = Convert.ToInt32(reader["idCreador"]) };
                    objEntidad.fechaAprobada = Convert.ToDateTime(reader["fechaAprobada"]);
                    objEntidad.fechaCreacion = Convert.ToDateTime(reader["fechaCreacion"]);
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
        #endregion
    }
}
