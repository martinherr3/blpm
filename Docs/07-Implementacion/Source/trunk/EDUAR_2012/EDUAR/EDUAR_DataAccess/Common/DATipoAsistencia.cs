using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_DataAccess.Common
{
	public class DATipoAsistencia : DataAccesBase<TipoAsistencia>
	{
		#region --[Atributos]--
		private const string ClassName = "DATipoAsistencia";
		#endregion

		#region --[Constructor]--
		public DATipoAsistencia()
		{
		}

		public DATipoAsistencia(DATransaction objDATransaction)
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

		public override TipoAsistencia GetById(TipoAsistencia entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(TipoAsistencia entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(TipoAsistencia entidad, out int identificador)
		{
			throw new NotImplementedException();
		}

		public override void Update(TipoAsistencia entidad)
		{
			throw new NotImplementedException();
		}

		public override void Delete(TipoAsistencia entidad)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Gets the tipo asistencia.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<TipoAsistencia> GetTipoAsistencia(TipoAsistencia entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TipoAsistencia_Select");
				if (entidad != null)
				{
					
				}

				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<TipoAsistencia> listaTipoAsistencias = new List<TipoAsistencia>();
				TipoAsistencia objTipoAsistencia;
				while (reader.Read())
				{
					objTipoAsistencia = new TipoAsistencia();

					objTipoAsistencia.idTipoAsistencia = (int)reader["idTipoAsistencia"];
					objTipoAsistencia.descripcion = (string)reader["descripcion"];

					listaTipoAsistencias.Add(objTipoAsistencia);
				}
				return listaTipoAsistencias;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetTipoAsistencia()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetTipoAsistencia()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
		#endregion
	}
}
