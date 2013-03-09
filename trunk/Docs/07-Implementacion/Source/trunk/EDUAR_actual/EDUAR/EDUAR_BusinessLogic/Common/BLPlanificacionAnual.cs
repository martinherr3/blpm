using System;
using System.Collections.Generic;
using EDUAR_BusinessLogic.Shared;
using EDUAR_DataAccess.Common;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using EDUAR_Entities.Shared;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

namespace EDUAR_BusinessLogic.Common
{
	public class BLPlanificacionAnual : BusinessLogicBase<PlanificacionAnual, DAPlanificacionAnual>
	{
		#region --[Constante]--
		private const string ClassName = "BLPlanificacionAnual";
		#endregion

		#region --[Constructores]--
		/// <summary>
		/// Constructor con DTO como parámetro.
		/// </summary>
		public BLPlanificacionAnual(DTBase objPlanificacionAnual)
		{
			Data = (PlanificacionAnual)objPlanificacionAnual;
		}
		/// <summary>
		/// Constructor vacio
		/// </summary>
		public BLPlanificacionAnual()
		{
			Data = new PlanificacionAnual();
		}
		#endregion

		#region --[Propiedades Override]--
		protected override sealed DAPlanificacionAnual DataAcces
		{
			get { return dataAcces; }
			set { dataAcces = value; }
		}

		public override sealed PlanificacionAnual Data
		{
			get { return data; }
			set { data = value; }
		}

		public override string FieldId
		{
			get { return DataAcces.FieldID; }
		}

		public override string FieldDescription
		{
			get { return DataAcces.FieldDescription; }
		}

		/// <summary>
		/// Gets the by id.
		/// </summary>
		public override void GetById()
		{
			try
			{
				Data = DataAcces.GetById(Data);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetById", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Método que guarda el registro actualmente cargado en memoria. No importa si se trata de una alta o modificación.
		/// </summary>
		public override void Save()
		{
			try
			{
				//Abre la transaccion que se va a utilizar
				DataAcces.Transaction.OpenTransaction();
				int idPlanificacionAnual = 0;

				if (Data.idPlanificacionAnual == 0)
					DataAcces.Create(Data, out idPlanificacionAnual);
				else
					DataAcces.Update(Data);

				if (Data.listaTemasPlanificacion.Count > 0)
				{
					BLTemaPlanificacionAnual objBLPlanificacion;
					foreach (TemaPlanificacionAnual item in Data.listaTemasPlanificacion)
					{
						item.idPlanificacionAnual = idPlanificacionAnual > 0 ? idPlanificacionAnual : Data.idPlanificacionAnual;
						objBLPlanificacion = new BLTemaPlanificacionAnual(item);
						objBLPlanificacion.Save(DataAcces.Transaction);
					}
				}
				//Se da el OK para la transaccion.
				DataAcces.Transaction.CommitTransaction();
			}
			catch (CustomizedException ex)
			{
				if (DataAcces != null && DataAcces.Transaction != null)
					DataAcces.Transaction.RollbackTransaction();
				throw ex;
			}
			catch (Exception ex)
			{
				if (DataAcces != null && DataAcces.Transaction != null)
					DataAcces.Transaction.RollbackTransaction();
				throw new CustomizedException(string.Format("Fallo en {0} - Save()", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Método que guarda el registro actualmente cargado en memoria. No importa si se trata de una alta o modificación.
		/// </summary>
		public override void Save(DATransaction objDATransaction)
		{
			try
			{
				//Si no viene el Id es porque se esta creando la entidad
				DataAcces = new DAPlanificacionAnual(objDATransaction);
				if (Data.idPlanificacionAnual == 0)
					DataAcces.Create(Data);
				else
				{
					DataAcces.Update(Data);
				}
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - Save()", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		public override void Delete()
		{
			throw new NotImplementedException();
		}

		public override void Delete(DATransaction objDATransaction)
		{
			try
			{
				DataAcces = new DAPlanificacionAnual(objDATransaction);
				DataAcces.Delete(Data);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - Delete()", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}
		#endregion

		#region --[Métodos publicos]--
		/// <summary>
		/// Gets the planificacion.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<PlanificacionAnual> GetPlanificacion(PlanificacionAnual entidad)
		{
			try
			{
				List<PlanificacionAnual> lista = DataAcces.GetPlanificacion(entidad);
				BLTemaPlanificacionAnual objBLTemas = new BLTemaPlanificacionAnual();
				foreach (PlanificacionAnual item in lista)
					item.listaTemasPlanificacion = objBLTemas.GetTemasPlanificacionAnual(item);
				return lista;
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetPlanificacion", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Gets the planificacion.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public PlanificacionAnual GetPlanificacionByAsignatura(int idAsignaturaCicloLectivo)
		{
			try
			{
				PlanificacionAnual objPlanificacion = DataAcces.GetPlanificacion(idAsignaturaCicloLectivo);
				if (objPlanificacion != null)
				{
					BLTemaPlanificacionAnual objBLTemas = new BLTemaPlanificacionAnual();
					objPlanificacion.listaTemasPlanificacion = objBLTemas.GetTemasPlanificacionAnual(objPlanificacion);
				}
				return objPlanificacion;
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetPlanificacionByAsignatura", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Gets the planificacion.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public PlanificacionAnual GetPlanificacionByAsignaturaAtrasados(int idCursoCicloLectivo, int idAsignaturaCicloLectivo)
		{
			try
			{
				PlanificacionAnual objPlanificacion = new PlanificacionAnual();
				BLTemaPlanificacionAnual objBLTemas = new BLTemaPlanificacionAnual();
				objPlanificacion.listaTemasPlanificacion = objBLTemas.GetTemasPlanificacionAnualAtrasados(idCursoCicloLectivo, idAsignaturaCicloLectivo);
				return objPlanificacion;
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetPlanificacionByAsignatura", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Gets the contenidos no asignados.
		/// </summary>
		/// <param name="idCursoCicloLectivo">The id curso ciclo lectivo.</param>
		/// <param name="idAsignaturaCicloLectivo">The id asignatura ciclo lectivo.</param>
		/// <returns></returns>
		public List<TemaPlanificacionAnual> GetContenidosNoAsignados(int idCursoCicloLectivo, int idAsignaturaCicloLectivo)
		{
			try
			{
				return (this.GetPlanificacionByAsignaturaAtrasados(idCursoCicloLectivo, idAsignaturaCicloLectivo)).listaTemasPlanificacion;
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetAsignaturas", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}
		#endregion
	}
}
