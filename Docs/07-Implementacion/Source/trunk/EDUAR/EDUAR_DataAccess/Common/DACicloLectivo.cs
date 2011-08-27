using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System.Collections.Generic;
using System.Data;
using EDUAR_Utility.Excepciones;
using System.Data.SqlClient;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_DataAccess.Common
{
	public class DACicloLectivo : DataAccesBase<CicloLectivo>
	{
		#region --[Atributos]--
		private const string ClassName = "DACicloLectivo";
		#endregion

		#region --[Constructor]--
		public DACicloLectivo()
		{
		}

		public DACicloLectivo(DATransaction objDATransaction)
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

		public override CicloLectivo GetById(CicloLectivo entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(CicloLectivo entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(CicloLectivo entidad, out int identificador)
		{
			throw new NotImplementedException();
		}

		public override void Update(CicloLectivo entidad)
		{
			throw new NotImplementedException();
		}

		public override void Delete(CicloLectivo entidad)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Gets the ciclo lectivos.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<CicloLectivo> GetCicloLectivos(CicloLectivo entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("CicloLectivo_Select");
				if (entidad != null)
				{
					if(entidad.activo)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<CicloLectivo> listaCiclosLectivos = new List<CicloLectivo>();
				CicloLectivo objCicloLectivo;
				while (reader.Read())
				{
					objCicloLectivo = new CicloLectivo();

					objCicloLectivo.idCicloLectivo = Convert.ToInt32(reader["idCicloLectivo"]);
					objCicloLectivo.nombre = reader["nombre"].ToString();
					objCicloLectivo.fechaInicio = Convert.ToDateTime(reader["fechaInicio"].ToString());
					objCicloLectivo.fechaFin = Convert.ToDateTime(reader["fechaFin"].ToString());
					objCicloLectivo.activo = Convert.ToBoolean(reader["activo"]);

					listaCiclosLectivos.Add(objCicloLectivo);
				}
				return listaCiclosLectivos;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetCicloLectivos()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetCicloLectivos()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Gets the cursos by ciclo lectivo.
		/// </summary>
		/// <param name="idCicloLectivo">The id ciclo lectivo.</param>
		/// <returns></returns>
		public List<Curso> GetCursosByCicloLectivo(int idCicloLectivo)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("CursosCicloLectivo_Select");
				if (idCicloLectivo > 0)
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCicloLectivo", DbType.Int32, idCicloLectivo);
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<Curso> listaCursos = new List<Curso>();
				Curso objCurso;
				while (reader.Read())
				{
					objCurso = new Curso();
					objCurso.idCurso = Convert.ToInt32(reader["idCurso"]);
					objCurso.nombre = reader["nombre"].ToString();

					listaCursos.Add(objCurso);
				}
				return listaCursos;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetCursosByCicloLectivo()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetCursosByCicloLectivo()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
		#endregion

	}
}
