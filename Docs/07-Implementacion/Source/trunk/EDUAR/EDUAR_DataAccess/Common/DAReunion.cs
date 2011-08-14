using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System.Data;
using System.Data.SqlClient;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;
using System.Collections.Generic;

namespace EDUAR_DataAccess.Common
{
	public class DAReunion : DataAccesBase<Reunion>
	{
		#region --[Atributos]--
		private const string ClassName = "DAReunion";
		#endregion

		#region --[Constructor]--
		public DAReunion()
		{
		}

		public DAReunion(DATransaction objDATransaction)
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

		public override Reunion GetById(Reunion entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Reunion_Select");

				if (entidad.idReunion > 0)
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "idReunion", DbType.Int32, entidad.idReunion);

				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				Reunion objReunion = new Reunion();
				while (reader.Read())
				{
					objReunion.idReunion = Convert.ToInt32(reader["idReunion"]);
					objReunion.horario = Convert.ToDateTime(reader["horario"].ToString());
					objReunion.idEventoAgenda = Convert.ToInt32(reader["idEvento"]);

				}
				return objReunion;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetReunion()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetReunion()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		public override void Create(Reunion entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Reunion_Insert");

				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idReunion", DbType.Int32, 0);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@horario", DbType.DateTime, entidad.horario);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEvento", DbType.Int32, entidad.idEventoAgenda);

				if (Transaction.Transaction != null)
					Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
				else
					Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
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

		public override void Create(Reunion entidad, out int identificador)
		{
			throw new NotImplementedException();
		}

		public override void Update(Reunion entidad)
		{
			throw new NotImplementedException();
		}

		public override void Delete(Reunion entidad)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region --[Métodos Públicos]--
		public List<Reunion> GetReuniones(Reunion entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("EventoInstitucional_Select");
				if (entidad != null)
				{
					if (entidad.idReunion > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idReunion", DbType.Int32, entidad.idReunion);
					if (entidad.idEventoAgenda > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEvento", DbType.Int32, entidad.idEventoAgenda);
					if (entidad.horario != null)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@horario", DbType.Time, Convert.ToDateTime(entidad.horario).ToShortTimeString());
				}

				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<Reunion> listReuniones = new List<Reunion>();

				Reunion objEvento;

				while (reader.Read())
				{
					objEvento = new Reunion();

					objEvento.idReunion = Convert.ToInt32(reader["idReunion"]);
					if (!string.IsNullOrEmpty(reader["horario"].ToString()))
						objEvento.horario = Convert.ToDateTime(reader["horario"].ToString());
					objEvento.idEventoAgenda = Convert.ToInt32(reader["idEvento"]);

					listReuniones.Add(objEvento);
				}
				return listReuniones;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetReuniones()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetReuniones()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
		#endregion
	}
}
