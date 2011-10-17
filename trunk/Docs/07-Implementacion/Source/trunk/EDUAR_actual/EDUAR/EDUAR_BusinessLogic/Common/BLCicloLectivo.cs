using System;
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
	public class BLCicloLectivo : BusinessLogicBase<CicloLectivo, DACicloLectivo>
	{
		#region --[Constante]--
		private const string ClassName = "BLCicloLectivo";
		#endregion

		#region --[Constructores]--
		/// <summary>
		/// Constructor con DTO como parámetro.
		/// </summary>
		public BLCicloLectivo(DTBase objCicloLectivo)
		{
			Data = (CicloLectivo)objCicloLectivo;
		}
		/// <summary>
		/// Constructor vacio
		/// </summary>
		public BLCicloLectivo()
		{
			Data = new CicloLectivo();
		}
		#endregion

		#region --[Propiedades Override]--
		protected override sealed DACicloLectivo DataAcces
		{
			get { return dataAcces; }
			set { dataAcces = value; }
		}

		public override sealed CicloLectivo Data
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
				int idCicloLectivo = 0;

				if (Data.idCicloLectivo == 0)
					DataAcces.Create(Data, out idCicloLectivo);
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
				DataAcces = new DACicloLectivo(objDATransaction);
				if (Data.idCicloLectivo == 0)
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
				DataAcces = new DACicloLectivo(objDATransaction);
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
		/// Gets the ciclo lectivos.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<CicloLectivo> GetCicloLectivos(CicloLectivo entidad)
		{
			try
			{
				return DataAcces.GetCicloLectivos(entidad);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetCicloLectivos", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Gets the cursos por ciclo lectivo.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Curso> GetCursosByCicloLectivo(int idCicloLectivo)
		{
			try
			{
				return DataAcces.GetCursosByAsignatura(new Asignatura { curso = new Curso { cicloLectivo = new CicloLectivo { idCicloLectivo = idCicloLectivo } } });
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetCursosByCicloLectivo", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Gets the periodos.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Periodo> GetPeriodosByCicloLectivo(int idCicloLectivo)
		{
			try
			{
				return DataAcces.GetPeriodosByCicloLectivo(idCicloLectivo);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetPeriodosByCicloLectivo", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Gets the cursos by docente.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Curso> GetCursosByAsignatura(Asignatura entidad)
		{
			try
			{
				return DataAcces.GetCursosByAsignatura(entidad);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetCursosByAsignatura", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Gets the ciclo lectivo actual.
		/// </summary>
		/// <returns></returns>
		public CicloLectivo GetCicloLectivoActual()
		{
			try
			{
				return DataAcces.GetCicloLectivoActual();
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetCicloLectivos", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}
		#endregion
	}
}
