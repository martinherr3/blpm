using System;
using System.Collections.Generic;
using EDUAR_BusinessLogic.Shared;
using EDUAR_DataAccess.Reports;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities.Reports;
using EDUAR_Entities.Shared;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

namespace EDUAR_BusinessLogic.Reports
{
	public class BLRptSancionesAlumnoPeriodo : BusinessLogicBase<RptSancionesAlumnoPeriodo, DARptSancionesAlumnoPeriodo>
	{
		#region --[Constante]--
		private const string ClassName = "BLRptSancionesAlumnoPeriodo";
		#endregion

		#region --[Constructores]--
		/// <summary>
		/// Constructor con DTO como parámetro.
		/// </summary>
		public BLRptSancionesAlumnoPeriodo(DTBase objRptSancionesAlumnoPeriodo)
		{
			Data = (RptSancionesAlumnoPeriodo)objRptSancionesAlumnoPeriodo;
		}
		/// <summary>
		/// Constructor vacio
		/// </summary>
		public BLRptSancionesAlumnoPeriodo()
		{
			Data = new RptSancionesAlumnoPeriodo();
		}
		#endregion

		#region --[Propiedades Override]--
		protected override sealed DARptSancionesAlumnoPeriodo DataAcces
		{
			get { return dataAcces; }
			set { dataAcces = value; }
		}

		public override sealed RptSancionesAlumnoPeriodo Data
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
			throw new NotImplementedException();
		}

		/// <summary>
		/// Método que guarda el registro actualmente cargado en memoria. No importa si se trata de una alta o modificación.
		/// </summary>
		public override void Save()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Método que guarda el registro actualmente cargado en memoria. No importa si se trata de una alta o modificación.
		/// </summary>
		public override void Save(DATransaction objDATransaction)
		{
			throw new NotImplementedException();
		}

		public override void Delete()
		{
			throw new NotImplementedException();
		}

		public override void Delete(DATransaction objDATransaction)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region --[Métodos publicos]--
		/// <summary>
		/// Gets the RPT sanciones alumno periodo.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<RptSancionesAlumnoPeriodo> GetRptSancionesAlumnoPeriodo(FilSancionesAlumnoPeriodo entidad)
		{
			try
			{
				return DataAcces.GetRptSancionesAlumnoPeriodo(entidad);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetRptSancionesAlumnoPeriodo", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Gets the RPT sanciones alumno periodo.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<RptSancionesAlumnoPeriodo> GetRptSancionesAlumnoPeriodo(FilIncidenciasAlumno entidad)
		{
			try
			{
				FilSancionesAlumnoPeriodo filtro = new FilSancionesAlumnoPeriodo();
				filtro.idAlumno = entidad.idAlumno;
				filtro.idCicloLectivo = entidad.idCicloLectivo;
				filtro.idCurso = entidad.idCurso;

				return DataAcces.GetRptSancionesAlumnoPeriodo(filtro);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetRptSancionesAlumnoPeriodo", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}
		#endregion
	}
}
