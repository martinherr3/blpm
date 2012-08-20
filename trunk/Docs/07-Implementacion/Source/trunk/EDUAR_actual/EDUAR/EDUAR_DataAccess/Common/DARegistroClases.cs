using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System;
using System.Data;
using System.Data.SqlClient;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;
using System.Collections.Generic;

namespace EDUAR_DataAccess.Common
{
	public class DARegistroClases : DataAccesBase<RegistroClases>
	{
		#region --[Atributos]--
		private const string ClassName = "DARegistroClases";
		#endregion

		#region --[Constructor]--
		public DARegistroClases()
		{
		}

		public DARegistroClases(DATransaction objDATransaction)
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

		public override RegistroClases GetById(RegistroClases entidad)
		{
			throw new NotImplementedException();

		}

		/// <summary>
		/// Creates the specified entidad.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		public override void Create(RegistroClases entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("RegistroClases_Insert");

				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idRegistroClases", DbType.Int32, 0);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEventoAgenda", DbType.Int32, 0);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignaturaCurso", DbType.Int32, entidad.asignatura.idAsignatura);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAgendaActividades", DbType.Int32, entidad.idAgendaActividad);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.usuario.username);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoEvento", DbType.Int32, (int)enumEventoAgendaType.ClaseDiaria);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaModificacion", DbType.Date, DBNull.Value);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaEvento", DbType.Date, entidad.fechaEvento);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoRegistroClases", DbType.Int32, entidad.tipoRegistro.idTipoRegistroClases);

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

		public override void Create(RegistroClases entidad, out int identificador)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("RegistroClases_Insert");

				Transaction.DataBase.AddOutParameter(Transaction.DBcomand, "@idRegistroClases", DbType.Int32, 0);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEventoAgenda", DbType.Int32, 0);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignaturaCurso", DbType.Int32, entidad.asignatura.idAsignatura);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAgendaActividades", DbType.Int32, entidad.idAgendaActividad);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.usuario.username);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoEvento", DbType.Int32, (int)enumEventoAgendaType.ClaseDiaria);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaModificacion", DbType.Date, DBNull.Value);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaEvento", DbType.Date, entidad.fechaEvento);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaCreacion", DbType.Date, entidad.fechaAlta);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoRegistroClases", DbType.Int32, entidad.tipoRegistro.idTipoRegistroClases);

				if (Transaction.Transaction != null)
					Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
				else
					Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

				identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idRegistroClases"].Value.ToString());

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

		public override void Update(RegistroClases entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("RegistroClases_Update");

				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idRegistroClases", DbType.Int32,entidad.idRegistroClases);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEventoAgenda", DbType.Int32, entidad.idEventoAgenda);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignaturaCurso", DbType.Int32, entidad.asignatura.idAsignatura);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAgendaActividades", DbType.Int32, entidad.idAgendaActividad);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.usuario.username);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoEvento", DbType.Int32, (int)enumEventoAgendaType.ClaseDiaria);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaModificacion", DbType.Date, DateTime.Now);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaEvento", DbType.Date, entidad.fechaEvento);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaAlta", DbType.Date, entidad.fechaAlta);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoRegistroClases", DbType.Int32, entidad.tipoRegistro.idTipoRegistroClases);

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

		public override void Delete(RegistroClases entidad)
		{
			throw new NotImplementedException();

		}
		#endregion
	}
}
