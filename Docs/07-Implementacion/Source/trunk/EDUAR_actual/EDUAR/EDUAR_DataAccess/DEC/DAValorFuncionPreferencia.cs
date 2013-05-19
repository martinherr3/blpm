using System;
using System.Data;
using System.Data.SqlClient;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;
using System.Collections.Generic;

namespace EDUAR_DataAccess.Common
{
	public class DAValorFuncionPreferencia : DataAccesBase<ValorFuncionPreferencia>
	{
		#region --[Atributos]--
		private const string ClassName = "DAValorFuncionPreferencia";
		#endregion

		#region --[Constructor]--
		public DAValorFuncionPreferencia()
		{
		}

		public DAValorFuncionPreferencia(DATransaction objDATransaction)
			: base(objDATransaction)
		{

		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Gets the ValorFuncionPreferencias.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<ValorFuncionPreferencia> GetValorFuncionPreferencias(ValorFuncionPreferencia entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("ValorFuncionPreferencias_Select");
				if (entidad != null)
				{
					if (entidad.idValorFuncionPreferencia > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idValorFuncionPreferencia", DbType.Int32, entidad.idValorFuncionPreferencia);
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<ValorFuncionPreferencia> listaValorFuncionPreferencias = new List<ValorFuncionPreferencia>();
				ValorFuncionPreferencia objValorFuncionPreferencia;
				while (reader.Read())
				{
					objValorFuncionPreferencia = new ValorFuncionPreferencia();

					objValorFuncionPreferencia.idValorFuncionPreferencia = Convert.ToInt32(reader["idValorFuncionPreferencia"]);
					objValorFuncionPreferencia.nombre = reader["nombre"].ToString();

					listaValorFuncionPreferencias.Add(objValorFuncionPreferencia);
				}
				return listaValorFuncionPreferencias;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetValorFuncionPreferencias()", ClassName),
				ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetValorFuncionPreferencias()", ClassName),
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

		public override ValorFuncionPreferencia GetById(ValorFuncionPreferencia entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(ValorFuncionPreferencia entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(ValorFuncionPreferencia entidad, out int identificador)
		{
			try
			{
				using (Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("ValorFuncionPreferencias_Insert"))
				{
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idValorFuncionPreferencia", DbType.Int32, 0);

					if (Transaction.Transaction != null)
						Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
					else
						Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

					identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idValorFuncionPreferencia"].Value.ToString());
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

		public override void Update(ValorFuncionPreferencia entidad)
		{
			throw new NotImplementedException();
		}

		public override void Delete(ValorFuncionPreferencia entidad)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}