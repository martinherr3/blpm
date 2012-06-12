using System;
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

		public override void Create(PlanificacionAnual entidad, out int identificador)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("PlanificacionAnual_Insert");

				//Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPlanificacionAnual", DbType.Int32, 0);
				Transaction.DataBase.AddOutParameter(Transaction.DBcomand, "@idPlanificacionAnual", DbType.Int32, 2);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignaturaCicloLectivo", DbType.Int32, entidad.asignaturaCicloLectivo.idAsignaturaCicloLectivo);
				//Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPlan", DbType.Int32, entidad);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@usuarioCreador", DbType.String, entidad.creador.username);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaCreacion", DbType.Date, DateTime.Now);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaAprobada", DbType.Date, entidad.fechaAprobada);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@observaciones", DbType.String, entidad.observaciones);


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
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignaturaCicloLectivo", DbType.Int32, entidad.asignaturaCicloLectivo.idAsignaturaCicloLectivo);
				//Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPlan", DbType.Int32, entidad);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCreador", DbType.String, entidad.creador.idPersona);
				if (ValidarFechaSQL(entidad.fechaCreacion))
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaCreacion", DbType.Date, entidad.fechaCreacion);
				if (entidad.fechaAprobada != null && ValidarFechaSQL(Convert.ToDateTime(entidad.fechaAprobada)))
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaAprobada", DbType.Date, entidad.fechaAprobada);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@observaciones", DbType.String, entidad.observaciones);

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
		public List<PlanificacionAnual> GetPlanificacion(PlanificacionAnual entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("PlanificacionAnual_Select");
				if (entidad != null)
				{
					if (entidad.asignaturaCicloLectivo.idAsignaturaCicloLectivo > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignaturaCicloLectivo", DbType.Int32, entidad.asignaturaCicloLectivo.idAsignaturaCicloLectivo);
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
					objEntidad.asignaturaCicloLectivo = new AsignaturaCicloLectivo() { idAsignaturaCicloLectivo = Convert.ToInt32(reader["idAsignaturaCicloLectivo"]) };
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
		/// Gets the planificacion.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public PlanificacionAnual GetPlanificacion(int idAsignaturaCicloLectivo)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("PlanificacionAnual_Select");
				if (idAsignaturaCicloLectivo > 0)
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignaturaCicloLectivo", DbType.Int32, idAsignaturaCicloLectivo);

				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				PlanificacionAnual objEntidad;
				while (reader.Read())
				{
					objEntidad = new PlanificacionAnual();
					objEntidad.idPlanificacionAnual = Convert.ToInt32(reader["idPlanificacionAnual"]);
					objEntidad.creador = new Persona() { idPersona = Convert.ToInt32(reader["idCreador"]) };
					DateTime fecha;
					if (DateTime.TryParse(reader["fechaAprobada"].ToString(), out fecha))
						objEntidad.fechaAprobada = fecha;
					objEntidad.fechaCreacion = Convert.ToDateTime(reader["fechaCreacion"]);
					objEntidad.observaciones = reader["observaciones"].ToString();
					objEntidad.asignaturaCicloLectivo = new AsignaturaCicloLectivo()
						{
							idAsignaturaCicloLectivo = Convert.ToInt32(reader["idAsignaturaCicloLectivo"]),
							asignatura = new Asignatura() { nombre = reader["Asignatura"].ToString() }
						};
					return objEntidad;
				}
				return null;
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
