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
	public class DAFuncionPreferencia : DataAccesBase<FuncionPreferencia>
	{
		#region --[Atributos]--
		private const string ClassName = "DAFuncionPreferencia";
		#endregion

		#region --[Constructor]--
		public DAFuncionPreferencia()
		{
		}

		public DAFuncionPreferencia(DATransaction objDATransaction)
			: base(objDATransaction)
		{

		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Gets the FuncionPreferencias.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<FuncionPreferencia> GetFuncionPreferencias(FuncionPreferencia entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("DEC_FuncionPreferencia_Select");
				if (entidad != null)
				{
					if (entidad.idFuncionPreferencia > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idFuncionPreferencia", DbType.Int32, entidad.idFuncionPreferencia);
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

				List<FuncionPreferencia> listaFuncionPreferencias = new List<FuncionPreferencia>();
				FuncionPreferencia objFuncionPreferencia;
				while (reader.Read())
				{
					objFuncionPreferencia = new FuncionPreferencia();

					objFuncionPreferencia.idFuncionPreferencia = Convert.ToInt32(reader["idFuncionPreferencia"]);
					objFuncionPreferencia.nombre = reader["nombre"].ToString();
					objFuncionPreferencia.ayuda = reader["ayuda"].ToString();

					listaFuncionPreferencias.Add(objFuncionPreferencia);
				}
				return listaFuncionPreferencias;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetFuncionPreferencias()", ClassName),
				ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetFuncionPreferencias()", ClassName),
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

		public override FuncionPreferencia GetById(FuncionPreferencia entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(FuncionPreferencia entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(FuncionPreferencia entidad, out int identificador)
		{
			try
			{
				using (Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("FuncionPreferencias_Insert"))
				{
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idFuncionPreferencia", DbType.Int32, 0);
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

					identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idFuncionPreferencia"].Value.ToString());
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

		public override void Update(FuncionPreferencia entidad)
		{
			throw new NotImplementedException();
		}

		public override void Delete(FuncionPreferencia entidad)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}