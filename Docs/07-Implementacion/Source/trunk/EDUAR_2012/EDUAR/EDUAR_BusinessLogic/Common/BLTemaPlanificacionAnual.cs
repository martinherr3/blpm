﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDUAR_DataAccess.Common;
using EDUAR_Entities;
using EDUAR_BusinessLogic.Shared;
using EDUAR_Entities.Shared;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;
using EDUAR_DataAccess.Shared;

namespace EDUAR_BusinessLogic.Common
{
	public class BLTemaPlanificacionAnual : BusinessLogicBase<TemaPlanificacionAnual, DATemaPlanificacionAnual>
	{
		#region --[Constante]--
		private const string ClassName = "BLTemasPlanificadosAsignatura";
		#endregion

		#region --[Constructores]--
		/// <summary>
		/// Constructor con DTO como parámetro.
		/// </summary>
		public BLTemaPlanificacionAnual(DTBase objTemasPlanificadosAsignatura)
		{
			Data = (TemaPlanificacionAnual)objTemasPlanificadosAsignatura;
		}
		/// <summary>
		/// Constructor vacio
		/// </summary>
		public BLTemaPlanificacionAnual()
		{
			Data = new TemaPlanificacionAnual();
		}
		#endregion

		#region --[Propiedades Override]--
		protected override sealed DATemaPlanificacionAnual DataAcces
		{
			get { return dataAcces; }
			set { dataAcces = value; }
		}

		public override sealed TemaPlanificacionAnual Data
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
				int idTemaPlanificacion = 0;

				if (Data.idTemaPlanificacion == 0)
					DataAcces.Create(Data, out idTemaPlanificacion);
				else
					DataAcces.Update(Data);

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
				DataAcces = new DATemaPlanificacionAnual(objDATransaction);
				int idTemaPlanificacion = 0;

				if (Data.idTemaPlanificacion == 0)
					DataAcces.Create(Data, out idTemaPlanificacion);
				else
					DataAcces.Update(Data);

				if (Data.idTemaPlanificacion > 0) DataAcces.DeleteContenidos(Data.idTemaPlanificacion);
				if (Data.listaContenidos.Count > 0)
					foreach (TemaContenido item in Data.listaContenidos)
					{
						//item.idTemaPlanificacion = idTemaPlanificacion > 0 ? idTemaPlanificacion : Data.idTemaPlanificacion;
						DataAcces.SaveContenidos(idTemaPlanificacion > 0 ? idTemaPlanificacion : Data.idTemaPlanificacion, item.idTemaContenido);
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

		/// <summary>
		/// Deletes this instance.
		/// </summary>
		public override void Delete()
		{
			try
			{
				//Abre la transaccion que se va a utilizar
				DataAcces = new DATemaPlanificacionAnual();
				DataAcces.Transaction.OpenTransaction();
				DataAcces.Delete(Data);
				DataAcces.DeleteContenidos(Data.idTemaPlanificacion);
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
				throw new CustomizedException(string.Format("Fallo en {0} - Delete()", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Deletes the specified obj DA transaction.
		/// </summary>
		/// <param name="objDATransaction">The obj DA transaction.</param>
		public override void Delete(DATransaction objDATransaction)
		{
			try
			{
				DataAcces = new DATemaPlanificacionAnual(objDATransaction);
				DataAcces.Delete(Data);
				DataAcces.DeleteContenidos(Data.idTemaPlanificacion);
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
		/// Gets the temas planificacion anual.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<TemaPlanificacionAnual> GetTemasPlanificacionAnual(PlanificacionAnual entidad)
		{
			try
			{
				return DataAcces.GetTemasPlanificacionAnual(entidad);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetTemasPlanificacionAnual", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Obteners the contenidos.
		/// </summary>
		/// <returns></returns>
		public List<TemaContenido> ObtenerContenidos()
		{
			try
			{
				return DataAcces.GetContenidos(Data);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - ObtenerContenidos", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}
		#endregion
	}
}
