using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System.Data;
using System.Data.SqlClient;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_DataAccess.Common
{
	public class DAFeriadosYFechasEspeciales : DataAccesBase<FeriadosYFechasEspeciales>
	{
		#region --[Atributos]--
		private const string ClassName = "DAFeriadosYFechasEspeciales";
		#endregion

		#region --[Constructor]--
		public DAFeriadosYFechasEspeciales()
		{
		}

		public DAFeriadosYFechasEspeciales(DATransaction objDATransaction)
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

		public override FeriadosYFechasEspeciales GetById(FeriadosYFechasEspeciales entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(FeriadosYFechasEspeciales entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(FeriadosYFechasEspeciales entidad, out int identificador)
		{
			throw new NotImplementedException();
		}

		public override void Update(FeriadosYFechasEspeciales entidad)
		{
			throw new NotImplementedException();
		}

		public override void Delete(FeriadosYFechasEspeciales entidad)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Validars the fecha.
		/// </summary>
		/// <param name="fecha">The fecha.</param>
		/// <returns></returns>
		public bool ValidarFecha(DateTime fecha)
		{
			try
			{
				using (Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Feriados_Select"))
				{
					if (fecha != null)
					{
						if (ValidarFechaSQL(fecha))
							Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fecha", DbType.Date, fecha);
					}
					IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

					while (reader.Read())
					{
						return false;
					}
					return true;
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - ValidarFecha()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - ValidarFecha()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
		#endregion
	}
}
