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
	public class DAConfigFuncionPreferencia : DataAccesBase<ConfigFuncionPreferencia>
	{
		#region --[Atributos]--
		private const string ClassName = "DAConfigFuncionPreferencia";
		#endregion

		#region --[Constructor]--
		public DAConfigFuncionPreferencia()
		{
		}

		public DAConfigFuncionPreferencia(DATransaction objDATransaction)
			: base(objDATransaction)
		{

		}
		#endregion

		#region --[Métodos Públicos]--
		///<summary>
		///Gets the ConfigFuncionPreferencias.
		///</summary>
		///<param name="entidad">The entidad.</param>
		///<returns></returns>
		public List<ConfigFuncionPreferencia> GetConfigFuncionPreferencias(ConfigFuncionPreferencia entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("DEC_ConfigFuncionPreferencia_Select");
				if (entidad != null)
				{
					if (entidad.idFuncionPreferencia > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idFuncionPreferencia", DbType.Int32, entidad.idFuncionPreferencia);
					if (entidad.idIndicador > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idIndicador", DbType.Int32, entidad.idIndicador);
					//if (!string.IsNullOrEmpty(entidad.pagina.titulo))
					//    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@titulo", DbType.String, entidad.pagina.titulo);
					//if (ValidarFechaSQL(entidad.fecha))
					//    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fecha", DbType.Date, entidad.fecha);
					//if (ValidarFechaSQL(entidad.hora))
					//    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@hora", DbType.Date, entidad.hora);
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<ConfigFuncionPreferencia> listaConfigFuncionPreferencias = new List<ConfigFuncionPreferencia>();
				ConfigFuncionPreferencia objConfigFuncionPreferencia;
				while (reader.Read())
				{
					objConfigFuncionPreferencia = new ConfigFuncionPreferencia();

					objConfigFuncionPreferencia.idConfigFuncionPreferencia = Convert.ToInt32(reader["idConfigFuncionPreferencia"]);
					objConfigFuncionPreferencia.idIndicador = Convert.ToInt32(reader["idIndicador"]);
					objConfigFuncionPreferencia.idFuncionPreferencia = Convert.ToInt32(reader["idFuncionPreferencia"]);
					objConfigFuncionPreferencia.idValorFuncionPreferencia = Convert.ToInt32(reader["idValorFuncionPreferencia"]);
					objConfigFuncionPreferencia.valorDefault = Convert.ToInt32(reader["valorDefault"]);

					listaConfigFuncionPreferencias.Add(objConfigFuncionPreferencia);
				}
				return listaConfigFuncionPreferencias;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetConfigFuncionPreferencias()", ClassName),
				ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetConfigFuncionPreferencias()", ClassName),
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

		public override ConfigFuncionPreferencia GetById(ConfigFuncionPreferencia entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(ConfigFuncionPreferencia entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(ConfigFuncionPreferencia entidad, out int identificador)
		{
			try
			{
				using (Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("ConfigFuncionPreferencias_Insert"))
				{
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idConfigFuncionPreferencia", DbType.Int32, 0);
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

					identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idConfigFuncionPreferencia"].Value.ToString());
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

		public override void Update(ConfigFuncionPreferencia entidad)
		{
			throw new NotImplementedException();
		}

		public override void Delete(ConfigFuncionPreferencia entidad)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
