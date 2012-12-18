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
	public class BLRptInasistenciasAlumnoPeriodo : BusinessLogicBase<RptInasistenciasAlumnoPeriodo, DARptInasistenciasAlumnoPeriodo>
	{
		#region --[Constante]--
		private const string ClassName = "BLRptInasistenciasAlumnoPeriodo";
		#endregion

		#region --[Constructores]--
		/// <summary>
		/// Constructor con DTO como parámetro.
		/// </summary>
		public BLRptInasistenciasAlumnoPeriodo(DTBase objRptInasistenciasAlumnoPeriodo)
		{
			Data = (RptInasistenciasAlumnoPeriodo)objRptInasistenciasAlumnoPeriodo;
		}
		/// <summary>
		/// Constructor vacio
		/// </summary>
		public BLRptInasistenciasAlumnoPeriodo()
		{
			Data = new RptInasistenciasAlumnoPeriodo();
		}
		#endregion

		#region --[Propiedades Override]--
		protected override sealed DARptInasistenciasAlumnoPeriodo DataAcces
		{
			get { return dataAcces; }
			set { dataAcces = value; }
		}

		public override sealed RptInasistenciasAlumnoPeriodo Data
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
		/// Gets the RPT inasistencias alumno periodo.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<RptInasistenciasAlumnoPeriodo> GetRptInasistenciasAlumnoPeriodo(FilInasistenciasAlumnoPeriodo entidad)
		{
			try
			{
				return DataAcces.GetRptInasistenciasAlumnoPeriodo(entidad);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetRptInasistenciasAlumnoPeriodo", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Gets the RPT inasistencias alumno periodo.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<RptInasistenciasAlumnoPeriodo> GetRptInasistenciasAlumnoPeriodo(FilIncidenciasAlumno entidad)
		{
			try
			{
				FilInasistenciasAlumnoPeriodo filtro = new FilInasistenciasAlumnoPeriodo();
				filtro.idAlumno = entidad.idAlumno;
				filtro.idCicloLectivo = entidad.idCicloLectivo;
				filtro.idCurso = entidad.idCurso;
                filtro.idPeriodo = entidad.idPeriodo;

				return DataAcces.GetRptInasistenciasAlumnoPeriodo(filtro);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetRptInasistenciasAlumnoPeriodo", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}
		#endregion
	}
}
