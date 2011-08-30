﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

namespace EDUAR_DataAccess.Common
{
	public class DACurso : DataAccesBase<Curso>
	{
		#region --[Atributos]--
		private const string ClassName = "DACurso";
		#endregion

		#region --[Constructor]--
		public DACurso()
		{
		}

		public DACurso(DATransaction objDATransaction)
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

		public override Curso GetById(Curso entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(Curso entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(Curso entidad, out int identificador)
		{
			throw new NotImplementedException();
		}

		public override void Update(Curso entidad)
		{
			throw new NotImplementedException();
		}

		public override void Delete(Curso entidad)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Gets the cursos.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Curso> GetCursos(Curso entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Curso_Select");
				if (entidad != null)
				{
					//if (entidad.cicloLectivo.idCicloLectivo > 0)
					//    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCicloLectivo", DbType.Int32, entidad.cicloLectivo.idCicloLectivo);

					//if (entidad.idCicloLectivo > 0)
					//    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCicloLectivo", DbType.Int32, entidad.idCicloLectivo);
					//if (entidad.pagina.idPagina > 0)
					//    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPagina", DbType.Int32, entidad.pagina.idPagina);
					//if (!string.IsNullOrEmpty(entidad.pagina.titulo))
					//    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@titulo", DbType.String, entidad.pagina.titulo);
					//if (ValidarFechaSQL(entidad.fecha))
					//    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fecha", DbType.Date, entidad.fecha);
					//if (ValidarFechaSQL(entidad.hora))
					//    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@hora", DbType.Date, entidad.hora);
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<Curso> listaCursos = new List<Curso>();
				Curso objCurso;
				while (reader.Read())
				{
					objCurso = new Curso();

					objCurso.idCurso = Convert.ToInt32(reader["idCurso"]);
					//objCurso.division = reader["division"].ToString();
					objCurso.nombre = reader["nombre"].ToString();

					listaCursos.Add(objCurso);
				}
				return listaCursos;
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
		#endregion
	}
}