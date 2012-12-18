using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

namespace EDUAR_DataAccess.Common
{
	public class DATipoNovedad : DataAccesBase<TipoNovedad>
	{
		#region --[Atributos]--
		private const string ClassName = "DATipoNovedad";
		#endregion

		#region --[Constructor]--
		public DATipoNovedad()
		{
		}

		public DATipoNovedad(DATransaction objDATransaction)
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

		/// <summary>
		/// Gets the by id.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public override TipoNovedad GetById(TipoNovedad entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TipoNovedadAulica_Select");

				if (entidad.idTipoNovedad > 0)
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoNovedad", DbType.Int32, entidad.idTipoNovedad);

				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				TipoNovedad objEntidad = new TipoNovedad();

				while (reader.Read())
				{
					objEntidad.idTipoNovedad = Convert.ToInt32(reader["idTipoNovedadAulica"]);
					objEntidad.nombre = (string)(reader["descripcion"]);
				}
				return objEntidad;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetById()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetById()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		public override void Create(TipoNovedad entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(TipoNovedad entidad, out int identificador)
		{
			throw new NotImplementedException();
		}

		public override void Update(TipoNovedad entidad)
		{
			throw new NotImplementedException();
		}

		public override void Delete(TipoNovedad entidad)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Gets the estados novedad.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<TipoNovedad> GetTiposNovedad(TipoNovedad entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("TipoNovedadAulica_Select");

				if (entidad.idTipoNovedad > 0)
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoNovedad", DbType.Int32, entidad.idTipoNovedad);
				if (!string.IsNullOrEmpty(entidad.nombre))
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@nombre", DbType.String, entidad.nombre);

				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<TipoNovedad> listaEntidad = new List<TipoNovedad>();
				TipoNovedad objEntidad = null;

				while (reader.Read())
				{
					objEntidad = new TipoNovedad();
					objEntidad.idTipoNovedad = Convert.ToInt32(reader["idTipoNovedadAulica"]);
					objEntidad.nombre = (string)(reader["descripcion"]);
					listaEntidad.Add(objEntidad);
				}
				return listaEntidad;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetEstadosNovedad()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetEstadosNovedad()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
		#endregion
	}
}
