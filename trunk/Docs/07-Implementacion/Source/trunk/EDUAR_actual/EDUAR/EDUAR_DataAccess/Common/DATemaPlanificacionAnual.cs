﻿using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System.Collections.Generic;
using System.Data;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;
using System.Data.SqlClient;

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

		public override void Create(TemaPlanificacionAnual entidad, out int identificador)
		{
			throw new NotImplementedException();
		}

		public override void Update(TemaPlanificacionAnual entidad)
		{
			throw new NotImplementedException();
		}

		public override void Delete(TemaPlanificacionAnual entidad)
		{
			throw new NotImplementedException();
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
					//if (entidad.asignaturaCicloLectivo.idAsignaturaCicloLectivo > 0)
					//    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignaturaCicloLectivo", DbType.Int32, entidad.asignaturaCicloLectivo.idAsignaturaCicloLectivo);
					if (entidad.idPlanificacionAnual > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPlanificacionAnual", DbType.Int32, entidad.idPlanificacionAnual);
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<TemaPlanificacionAnual> listaEntidad = new List<TemaPlanificacionAnual>();
				TemaPlanificacionAnual objEntidad;
				while (reader.Read())
				{
					objEntidad = new TemaPlanificacionAnual();
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
		#endregion

	}
}
