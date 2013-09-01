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
			throw new NotImplementedException();
		}

		public override void Create(Reunion entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Reunion_Insert");

				// Propios del evento de agenda
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAgendaActividades", DbType.Int32, entidad.idAgendaActividad);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.usuario.username);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoEvento", DbType.Int32, (int)enumEventoAgendaType.Reunion);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaModificacion", DbType.Date, DBNull.Value);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaEvento", DbType.Date, entidad.fechaEvento);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);

				//Propios de la reunión
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idReunion", DbType.Int32, 0);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@horario", DbType.Time, entidad.horario);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEventoAgenda", DbType.Int32, 0);

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
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Reunion_Insert");

				// Propios del evento de agenda
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAgendaActividades", DbType.Int32, entidad.idAgendaActividad);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.usuario.username);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoEvento", DbType.Int32, (int)enumEventoAgendaType.Reunion);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaModificacion", DbType.Date, DBNull.Value);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaEvento", DbType.Date, entidad.fechaEvento);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);

				//Propios de la reunión
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idReunion", DbType.Int32, 0);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@horario", DbType.Time, entidad.horario);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEventoAgenda", DbType.Int32, 0);

				if (Transaction.Transaction != null)
					Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
				else
					Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

				identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idReunion"].Value.ToString());
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

		public override void Update(Reunion entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Reunion_Update");

				// Propios del evento de agenda
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAgendaActividades", DbType.Int32, entidad.idAgendaActividad);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.usuario.username);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoEvento", DbType.Int32, (int)enumEventoAgendaType.Reunion);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaModificacion", DbType.Date, DateTime.Now);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaEvento", DbType.Date, entidad.fechaEvento);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaAlta", DbType.Date, entidad.fechaAlta);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);

				//Propios de la reunión
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idReunion", DbType.Int32, entidad.idReunion);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@horario", DbType.Time, entidad.horario);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEventoAgenda", DbType.Int32, entidad.idEventoAgenda);

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
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Reunion_Select");
				if (entidad != null)
				{
					if (entidad.idReunion > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idReunion", DbType.Int32, entidad.idReunion);
					if (entidad.idEventoAgenda > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEvento", DbType.Int32, entidad.idEventoAgenda);
					if (entidad.horario != null)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@horario", DbType.Time, Convert.ToDateTime(entidad.horario).ToShortTimeString());

					if (entidad.activo)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
					if (ValidarFechaSQL(entidad.fechaEventoDesde))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaDesde", DbType.Date, entidad.fechaEventoDesde);
					if (ValidarFechaSQL(entidad.fechaEventoHasta))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaHasta", DbType.Date, entidad.fechaEventoHasta);
					if (entidad.idAgendaActividad > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAgendaActividad", DbType.Int32, entidad.idAgendaActividad);
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

					objEvento.activo = Convert.ToBoolean(reader["activo"]);
					objEvento.descripcion = reader["descripcion"].ToString();

					objEvento.fechaEvento = Convert.ToDateTime(reader["fechaEvento"]);
					objEvento.fechaAlta = Convert.ToDateTime(reader["fechaAlta"]);

					objEvento.tipoEventoAgenda = new TipoEventoAgenda { idTipoEventoAgenda = Convert.ToInt32(reader["idTipoEvento"]), descripcion = reader["tipoEvento"].ToString() };
					objEvento.usuario = new Persona() { idPersona = Convert.ToInt32(reader["idOrganizador"]), nombre = reader["nombreOrganizador"].ToString(), apellido = reader["apellidoOrganizador"].ToString() };

					objEvento.idAgendaActividad = Convert.ToInt32(reader["idAgendaActividades"]);

					//Descartamos mostrar las reuniones que han acontecido en el pasado (se desactivan)
					if (objEvento.fechaEvento < DateTime.Now && objEvento.activo == true)
					{
						objEvento.activo = false;
						Update(objEvento);
					}
					
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
