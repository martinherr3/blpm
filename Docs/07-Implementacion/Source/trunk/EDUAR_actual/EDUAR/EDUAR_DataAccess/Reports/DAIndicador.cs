using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities.Reports;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

namespace EDUAR_DataAccess.Reports
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
			throw new NotImplementedException();
		}

		/// <summary>
		/// Updates the specified entidad.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		public override void Update(Indicador entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Indicadores_Update");

				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idIndicador", DbType.Int32, entidad.idIndicador);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@invertirEscala", DbType.Boolean, entidad.invertirEscala);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@parametroCantidad", DbType.Int32, entidad.parametroCantidad);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@diasHastaPrincipal", DbType.Int32, entidad.diasHastaPrincipal);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@diasHastaIntermedio", DbType.Int32, entidad.diasHastaIntermedio);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@diasHastaSecundario", DbType.Int32, entidad.diasHastaSecundario);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@verdeNivelPrincipal", DbType.Int32, entidad.verdeNivelPrincipal);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@verdeNivelIntermedio", DbType.Int32, entidad.verdeNivelIntermedio);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@verdeNivelSecundario", DbType.Int32, entidad.verdeNivelSecundario);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@rojoNivelPrincipal", DbType.Int32, entidad.rojoNivelPrincipal);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@rojoNivelIntermedio", DbType.Int32, entidad.rojoNivelIntermedio);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@rojoNivelSecundario", DbType.Int32, entidad.rojoNivelSecundario);

				if (Transaction.Transaction != null)
					Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
				else
					Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
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

		#region --[Métodos Públicos]--
		/// <summary>
		/// Gets the indicador.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Indicador> GetIndicadores(Indicador entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Indicadores_Select");
				if (entidad != null)
				{
					//if (entidad.idPagina > 0)
					//    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPagina", DbType.Int32, entidad.idPagina);
					//if (ValidarFechaSQL(entidad.fechaDesde))
					//    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaDesde", DbType.Date, entidad.fechaDesde);
					//if (ValidarFechaSQL(entidad.fechaHasta))
					//    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaHasta", DbType.Date, entidad.fechaHasta);
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<Indicador> listaEntidad = new List<Indicador>();
				Indicador objEntidad;
				int parametroCantidad;
				while (reader.Read())
				{
					objEntidad = new Indicador();
					objEntidad.idIndicador = (int)reader["idIndicador"];
					objEntidad.nombre = reader["nombre"].ToString();
					objEntidad.invertirEscala = (bool)reader["invertirEscala"];
					int.TryParse(reader["parametroCantidad"].ToString(), out parametroCantidad);
					objEntidad.parametroCantidad = parametroCantidad;
					objEntidad.rojoNivelIntermedio = (int)reader["rojoNivelIntermedio"];
					objEntidad.rojoNivelPrincipal = (int)reader["rojoNivelPrincipal"];
					objEntidad.rojoNivelSecundario = (int)reader["rojoNivelSecundario"];
					objEntidad.verdeNivelIntermedio = (int)reader["verdeNivelIntermedio"];
					objEntidad.verdeNivelPrincipal = (int)reader["verdeNivelPrincipal"];
					objEntidad.verdeNivelSecundario = (int)reader["verdeNivelSecundario"];
                    objEntidad.diasHastaPrincipal = (int)reader["diasHastaPrincipal"];
                    objEntidad.diasHastaIntermedio = (int)reader["diasHastaIntermedio"];
                    objEntidad.diasHastaSecundario = (int)reader["diasHastaSecundario"];

					listaEntidad.Add(objEntidad);
				}
				return listaEntidad;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetIndicadores()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetIndicadores()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
		#endregion
	}
}
