using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System.Data.SqlClient;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;
using System.Data;
using System.Collections.Generic;

namespace EDUAR_DataAccess.Common
{
	public class DADetalleRegistroClases : DataAccesBase<DetalleRegistroClases>
	{
		#region --[Atributos]--
		private const string ClassName = "DADetalleRegistroClases";
		#endregion

		#region --[Constructor]--
		public DADetalleRegistroClases()
		{
		}

		public DADetalleRegistroClases(DATransaction objDATransaction)
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

		public override DetalleRegistroClases GetById(DetalleRegistroClases entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(DetalleRegistroClases entidad)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Creates the specified entidad.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <param name="identificador">The identificador.</param>
		public override void Create(DetalleRegistroClases entidad, out int identificador)
		{
			try
			{
				using (Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("DetalleRegistroClases_Insert"))
				{
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idRegistroClases", DbType.Int32, entidad.idRegistroClases);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTemaContenido", DbType.Int32, entidad.temaContenido.idTemaContenido);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@porcentaje", DbType.Int32, entidad.porcentaje);

					Transaction.DataBase.AddOutParameter(Transaction.DBcomand, "@idDetalleRegistroClases", DbType.Int32, 0);

					if (Transaction.Transaction != null)
						Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
					else
						Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

					identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idDetalleRegistroClases"].Value.ToString());
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - Create()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - Create()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Updates the specified entidad.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		public override void Update(DetalleRegistroClases entidad)
		{
			try
			{
				using (Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("DetalleRegistroClases_Update"))
				{
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idRegistroClases", DbType.Int32, entidad.idRegistroClases);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTemaContenido", DbType.Int32, entidad.temaContenido.idTemaContenido);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@porcentaje", DbType.Int32, entidad.porcentaje);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idDetalleRegistroClases", DbType.Int32, entidad.idDetalleRegistroClases);

					if (Transaction.Transaction != null)
						Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
					else
						Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - Update()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - Update()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Deletes the specified entidad.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		public override void Delete(DetalleRegistroClases entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("DetalleRegistroClases_Delete");

				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idDetalleRegistroClases", DbType.Int32, entidad.idDetalleRegistroClases);

				if (Transaction.Transaction != null)
					Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
				else
					Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - Delete()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - Delete()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Gets the tema contenidos.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<DetalleRegistroClases> GetDetalleRegistroClases(DetalleRegistroClases entidad)
		{
			try
			{
				return GetDetalleRegistroClases(entidad, null);
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetDetalleRegistroClases()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetDetalleRegistroClases()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Gets the tema contenidos.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<DetalleRegistroClases> GetDetalleRegistroClases(DetalleRegistroClases entidad, AsignaturaCicloLectivo objAsignatura)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("DetalleRegistroClases_Select");
				if (entidad != null)
				{
					if (entidad.idRegistroClases > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idRegistroClases", DbType.Int32, entidad.idRegistroClases);
					if (entidad.temaContenido.idTemaContenido > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTemaContenido", DbType.Int32, entidad.temaContenido.idTemaContenido);
					if (entidad.porcentaje > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@porcentaje", DbType.Int32, entidad.porcentaje);
					if (entidad.idDetalleRegistroClases > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idDetalleRegistroClases", DbType.Int32, entidad.idDetalleRegistroClases);
				}

				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<DetalleRegistroClases> listaEntidad = new List<DetalleRegistroClases>();
				DetalleRegistroClases objContenido;
				while (reader.Read())
				{
					objContenido = new DetalleRegistroClases();
					objContenido.idRegistroClases = Convert.ToInt32(reader["idRegistroClases"]);
					objContenido.idDetalleRegistroClases = Convert.ToInt32(reader["idDetalleRegistroClases"]);
					objContenido.porcentaje = Convert.ToInt32(reader["porcentaje"]);
					objContenido.temaContenido.idTemaContenido = Convert.ToInt32(reader["idTemaContenido"]);

					listaEntidad.Add(objContenido);
				}
				return listaEntidad;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetDetalleRegistroClases()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetDetalleRegistroClases()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
		#endregion

	}
}
