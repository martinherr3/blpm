using System;
using System.Data;
using System.Data.SqlClient;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities.DEC;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

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
		/// Gets the Indicadors.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		// public List<Indicador> GetIndicadors(Indicador entidad)
		// {
		// try
		// {
		// Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Indicadors_Select");
		// if (entidad != null)
		// {
		// if (entidad.idIndicador > 0)
		// Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idIndicador", DbType.Int32, entidad.idIndicador);
		// if (entidad.pagina.idPagina > 0)
		// Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPagina", DbType.Int32, entidad.pagina.idPagina);
		// if (!string.IsNullOrEmpty(entidad.pagina.titulo))
		// Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@titulo", DbType.String, entidad.pagina.titulo);
		// if (ValidarFechaSQL(entidad.fecha))
		// Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fecha", DbType.Date, entidad.fecha);
		// if (ValidarFechaSQL(entidad.hora))
		// Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@hora", DbType.Date, entidad.hora);
		// }
		// IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

		// List<Indicador> listaIndicadors = new List<Indicador>();
		// Indicador objIndicador;
		// while (reader.Read())
		// {
		// objIndicador = new Indicador();

		// objIndicador.idIndicador = Convert.ToInt32(reader["idIndicador"]);
		// objIndicador.pagina.idPagina = Convert.ToInt32(reader["idPagina"]);
		// objIndicador.pagina.titulo = reader["titulo"].ToString();
		// objIndicador.pagina.url = reader["url"].ToString();
		// objIndicador.fecha = Convert.ToDateTime(reader["fecha"].ToString());
		// objIndicador.hora = Convert.ToDateTime(reader["hora"].ToString());

		// listaIndicadors.Add(objIndicador);
		// }
		// return listaIndicadors;
		// }
		// catch (SqlException ex)
		// {
		// throw new CustomizedException(string.Format("Fallo en {0} - GetIndicadors()", ClassName),
		// ex, enuExceptionType.SqlException);
		// }
		// catch (Exception ex)
		// {
		// throw new CustomizedException(string.Format("Fallo en {0} - GetIndicadors()", ClassName),
		// ex, enuExceptionType.DataAccesException);
		// }
		// }
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
					// Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPagina", DbType.Int32, 0);
					// Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fecha", DbType.Date, entidad.fecha.Date.ToShortDateString());
					// Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@hora", DbType.Time, entidad.hora.ToShortTimeString());
					// Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.usuario);
					// Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@url", DbType.String, entidad.pagina.url);
					// Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@titulo", DbType.String, entidad.pagina.titulo);

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

		public override void Update(Indicador entidad)
		{
			throw new NotImplementedException();
		}

		public override void Delete(Indicador entidad)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}