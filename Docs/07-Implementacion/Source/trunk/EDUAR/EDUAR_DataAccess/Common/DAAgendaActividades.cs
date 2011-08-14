﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Constantes;

namespace EDUAR_DataAccess.Common
{
	public class DAAgendaActividades : DataAccesBase<AgendaActividades>
	{
		#region --[Atributos]--
		private const string ClassName = "DAAgendaActividades";
		#endregion

		#region --[Constructor]--
		public DAAgendaActividades()
		{
		}

		public DAAgendaActividades(DATransaction objDATransaction)
			: base(objDATransaction)
		{

		}
		#endregion

		#region --[Métodos Públicos]--
		public List<AgendaActividades> GetAgendaActividades(AgendaActividades entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("AgendaActividades_Select");
				if (entidad != null)
				{
					if (entidad.idAgendaActividad > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAgendaActividad", DbType.Int32, entidad.idAgendaActividad);
					if (entidad.cursoCicloLectivo.idCurso > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCurso", DbType.Int32, entidad.cursoCicloLectivo.idCurso);
					if (entidad.cursoCicloLectivo.idCicloLectivo > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCicloLectivo", DbType.Int32, entidad.cursoCicloLectivo.idCicloLectivo);
					if (!string.IsNullOrEmpty(entidad.descripcion))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);
					if (ValidarFechaSQL(entidad.fechaCreacion))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaCreacion", DbType.Date, entidad.fechaCreacion);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<AgendaActividades> listaAgendaActividades = new List<AgendaActividades>();
				AgendaActividades objAgendaActividades;
				while (reader.Read())
				{
					objAgendaActividades = new AgendaActividades();

					objAgendaActividades.idAgendaActividad = Convert.ToInt32(reader["idAgendaActividad"]);
					objAgendaActividades.cursoCicloLectivo.idCicloLectivo = Convert.ToInt32(reader["idCicloLectivo"]);
					objAgendaActividades.cursoCicloLectivo.idCurso = Convert.ToInt32(reader["idCurso"]);
					objAgendaActividades.cursoCicloLectivo.idCursoCicloLectivo = Convert.ToInt32(reader["idCursoCicloLectivo"]);
					objAgendaActividades.cursoCicloLectivo.curso.nombre = reader["curso"].ToString();
					objAgendaActividades.cursoCicloLectivo.cicloLectivo.nombre = reader["cicloLectivo"].ToString();
					objAgendaActividades.descripcion = reader["descripcion"].ToString();
					objAgendaActividades.activo = Convert.ToBoolean(reader["activo"].ToString());
					objAgendaActividades.fechaCreacion = Convert.ToDateTime(reader["fechaCreacion"].ToString());
					listaAgendaActividades.Add(objAgendaActividades);
				}
				return listaAgendaActividades;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetAgendaActividadess()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetAgendaActividadess()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Gets the eventos agenda.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<EventoAgenda> GetEventosAgenda(AgendaActividades entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("EventoAgenda_Select");
				if (entidad != null)
				{
					if (entidad.idAgendaActividad > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAgendaActividad", DbType.Int32, entidad.idAgendaActividad);
					//if (entidad.cursoCicloLectivo.idCurso > 0)
					//    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCurso", DbType.Int32, entidad.cursoCicloLectivo.idCurso);
					//if (entidad.cursoCicloLectivo.idCicloLectivo > 0)
					//    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCicloLectivo", DbType.Int32, entidad.cursoCicloLectivo.idCicloLectivo);
					//if (!string.IsNullOrEmpty(entidad.descripcion))
					//    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);
					//if (ValidarFechaSQL(entidad.fechaCreacion))
					//    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaCreacion", DbType.Date, entidad.fechaCreacion);
					//Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<EventoAgenda> listaEventos = new List<EventoAgenda>();
				EventoAgenda objEvento;
				while (reader.Read())
				{
					objEvento = new EventoAgenda();
					
					objEvento.descripcion = reader["descripcion"].ToString();
					objEvento.activo = Convert.ToBoolean(reader["activo"].ToString());
					objEvento.fechaAlta = Convert.ToDateTime(reader["fechaAlta"].ToString());
					objEvento.fechaModificacion = Convert.ToDateTime(reader["fechaModificacion"].ToString());
					objEvento.fechaEvento = Convert.ToDateTime(reader["fechaEvento"].ToString());
					objEvento.tipoEventoAgenda.descripcion = reader["tipoEvento"].ToString();
					objEvento.tipoEventoAgenda.idTipoEventoAgenda = Convert.ToInt32(reader["idTipoEvento"]);

					listaEventos.Add(objEvento);
				}
				return new List<EventoAgenda>();
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetEventosAgenda()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetEventosAgenda()", ClassName),
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

		public override AgendaActividades GetById(AgendaActividades entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(AgendaActividades entidad)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Creates the specified entidad.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <param name="identificador">The identificador.</param>
		public override void Create(AgendaActividades entidad, out int identificador)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("AgendaActividades_Insert");

				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAgendaActividad", DbType.Int32, 0);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCurso", DbType.Int32, entidad.cursoCicloLectivo.idCurso);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCicloLectivo", DbType.Int32, entidad.cursoCicloLectivo.idCicloLectivo);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaCreacion", DbType.Date, entidad.fechaCreacion);

				if (Transaction.Transaction != null)
					Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
				else
					Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

				identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idAgendaActividad"].Value.ToString());

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

		/// <summary>
		/// Updates the specified entidad.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		public override void Update(AgendaActividades entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("AgendaActividades_Update");

				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAgendaActividad", DbType.Int32, entidad.idAgendaActividad);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCursoCicloLectivo", DbType.Int32, entidad.cursoCicloLectivo.idCursoCicloLectivo);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaCreacion", DbType.Date, entidad.fechaCreacion);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.String, entidad.activo);

				if (Transaction.Transaction != null)
					Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
				else
					Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
			}
			catch (SqlException ex)
			{
				if (ex.Number == BLConstantesGenerales.ConcurrencyErrorNumber)
					throw new CustomizedException(string.Format(
						   "No se puede modificar el Evento {0}, debido a que otro usuario lo ha modificado.",
						   entidad.descripcion), ex, enuExceptionType.ConcurrencyException);

				throw new CustomizedException(string.Format("Fallo en {0} - Update()", ClassName),
													  ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - Update()", ClassName),
													  ex, enuExceptionType.DataAccesException);
			}
		}

		public override void Delete(AgendaActividades entidad)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
