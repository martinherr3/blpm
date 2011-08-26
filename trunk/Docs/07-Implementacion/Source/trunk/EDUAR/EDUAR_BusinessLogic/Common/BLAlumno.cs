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
	public class BLAlumno : BusinessLogicBase<Alumno, DAAlumno>
	{
		#region --[Constante]--
		private const string ClassName = "BLAlumno";
		#endregion

		#region --[Constructores]--
		/// <summary>
		/// Constructor con DTO como parámetro.
		/// </summary>
		public BLAlumno(DTBase objAlumno)
		{
			Data = (Alumno)objAlumno;
		}
		/// <summary>
		/// Constructor vacio
		/// </summary>
		public BLAlumno()
		{
			Data = new Alumno();
		}
		#endregion

		#region --[Propiedades Override]--
		protected override sealed DAAlumno DataAcces
		{
			get { return dataAcces; }
			set { dataAcces = value; }
		}

		public override sealed Alumno Data
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
				int idAlumno = 0;

				if (Data.idAlumno == 0)
					DataAcces.Create(Data, out idAlumno);
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
				DataAcces = new DAAlumno(objDATransaction);
				if (Data.idAlumno == 0)
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
				DataAcces = new DAAlumno(objDATransaction);
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
		//public List<Alumno> GetAlumnos(Alumno entidad)
		//{
		//    try
		//    {
		//        return DataAcces.GetAlumnos(entidad);
		//    }
		//    catch (CustomizedException ex)
		//    {
		//        throw ex;
		//    }
		//    catch (Exception ex)
		//    {
		//        throw new CustomizedException(string.Format("Fallo en {0} - GetAlumnos", ClassName), ex,
		//                                      enuExceptionType.BusinessLogicException);
		//    }
		//}
		#endregion
	}
}
