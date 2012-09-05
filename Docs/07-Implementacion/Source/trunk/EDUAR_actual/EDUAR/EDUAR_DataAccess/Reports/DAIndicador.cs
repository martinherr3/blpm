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

		public override void Update(Indicador entidad)
		{
			throw new NotImplementedException();
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
