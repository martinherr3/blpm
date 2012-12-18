using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System.Data;
using System.Data.SqlClient;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;
using System.Collections.Generic;

namespace EDUAR_DataAccess.Common
{
	public class DAEstadoNovedad : DataAccesBase<EstadoNovedad>
	{
		#region --[Atributos]--
		private const string ClassName = "DAEstadoNovedad";
		#endregion

		#region --[Constructor]--
		public DAEstadoNovedad()
		{
		}

		public DAEstadoNovedad(DATransaction objDATransaction)
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
		public override EstadoNovedad GetById(EstadoNovedad entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("EstadoNovedadAulica_Select");

				if (entidad.idEstadoNovedad > 0)
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEstadoNovedad", DbType.Int32, entidad.idEstadoNovedad);

				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				EstadoNovedad objEntidad = new EstadoNovedad();

				while (reader.Read())
				{
					objEntidad.idEstadoNovedad = Convert.ToInt32(reader["idEstadoNovedad"]);
					objEntidad.nombre = (string)(reader["nombre"]);
					objEntidad.esFinal = (bool)(reader["esFinal"]);
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

		public override void Create(EstadoNovedad entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(EstadoNovedad entidad, out int identificador)
		{
			throw new NotImplementedException();
		}

		public override void Update(EstadoNovedad entidad)
		{
			throw new NotImplementedException();
		}

		public override void Delete(EstadoNovedad entidad)
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
		public List<EstadoNovedad> GetEstadosNovedad(EstadoNovedad entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("EstadoNovedadAulica_Select");

				if (entidad.idEstadoNovedad > 0)
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEstadoNovedad", DbType.Int32, entidad.idEstadoNovedad);
				if (!string.IsNullOrEmpty(entidad.nombre))
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@nombre", DbType.String, entidad.nombre);

				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<EstadoNovedad> listaEntidad = new List<EstadoNovedad>();
				EstadoNovedad objEntidad = null;

				while (reader.Read())
				{
					objEntidad = new EstadoNovedad();
					objEntidad.idEstadoNovedad = Convert.ToInt32(reader["idEstadoNovedadAulica"]);
					objEntidad.nombre = (string)(reader["nombre"]);
					objEntidad.esFinal = (bool)(reader["esFinal"]);
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
