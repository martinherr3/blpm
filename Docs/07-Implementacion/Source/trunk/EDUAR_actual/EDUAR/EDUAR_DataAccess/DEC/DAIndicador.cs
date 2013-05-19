using System;
using System.Data;
using System.Data.SqlClient;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities.DEC;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;
using System.Collections.Generic;

namespace EDUAR_DataAccess.Common
{
	public class DAIndicador : DataAccesBase<Indicador>
	{
		#region --[Atributos]--
		private const string ClassName = "DAIndicador";
		#endregion

		#region --[Constructor]--
		public DAIndicador()
		{
		}

		public DAIndicador(DATransaction objDATransaction)
			: base(objDATransaction)
		{

		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Gets the indicador.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public Indicador GetIndicador(Indicador entidad)
		{
			try
			{
				List<Indicador> lista = this.GetIndicadores(entidad);
				if (lista != null)
					if (lista.Count == 1)
						return lista[0];
				throw new CustomizedException("No se encuentra el Indicador.", null, enuExceptionType.DataAccesException);
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetIndicadors()", ClassName),
				ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetIndicadors()", ClassName),
				ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Gets the Indicadors.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Indicador> GetIndicadores(Indicador entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("DEC_Indicador_Select");
				if (entidad != null)
				{
					if (entidad.idIndicador > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idIndicador", DbType.Int32, entidad.idIndicador);
					if (!string.IsNullOrEmpty(entidad.nombre))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@nombre", DbType.String, entidad.nombre);
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<Indicador> listaIndicadors = new List<Indicador>();
				Indicador objIndicador;
				while (reader.Read())
				{
					objIndicador = new Indicador();

					objIndicador.idIndicador = Convert.ToInt32(reader["idIndicador"]);
					objIndicador.nombre = reader["nombre"].ToString();
					objIndicador.pesoDefault = Convert.ToDecimal(reader["pesoDefault"]);
					objIndicador.escala = reader["escala"].ToString();
					objIndicador.pesoMinimo = Convert.ToDecimal(reader["pesoMinimo"].ToString());
					objIndicador.pesoMaximo = Convert.ToDecimal(reader["pesoMaximo"].ToString());
					objIndicador.maximiza = Convert.ToBoolean(reader["maximiza"].ToString());

					listaIndicadors.Add(objIndicador);
				}
				return listaIndicadors;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetIndicadors()", ClassName),
				ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetIndicadors()", ClassName),
				ex, enuExceptionType.DataAccesException);
			}
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

		public override Indicador GetById(Indicador entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(Indicador entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(Indicador entidad, out int identificador)
		{
			try
			{
				using (Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Indicadors_Insert"))
				{
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idIndicador", DbType.Int32, 0);

					if (Transaction.Transaction != null)
						Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
					else
						Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

					identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idIndicador"].Value.ToString());
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
		public override void Update(Indicador entidad)
		{
			try
			{
				using (Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("DEC_Indicador_Update"))
				{
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idIndicador", DbType.Int32, entidad.idIndicador);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@nombre", DbType.String, entidad.nombre);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@escala", DbType.String, entidad.escala);

					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@pesoDefault", DbType.Decimal, entidad.pesoDefault);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@pesoMinimo", DbType.Decimal, entidad.pesoMinimo);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@pesoMaximo", DbType.Decimal, entidad.pesoMaximo);

					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@maximiza", DbType.Boolean, entidad.maximiza);

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

		public override void Delete(Indicador entidad)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}