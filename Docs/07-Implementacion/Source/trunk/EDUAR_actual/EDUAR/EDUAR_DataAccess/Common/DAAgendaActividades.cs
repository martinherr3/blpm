﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using EDUAR_Utility.Constantes;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

namespace EDUAR_DataAccess.Common
{
	/// <summary>
	/// 
	/// </summary>
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

		#region --[Implementación métodos heredados]--
		public override string FieldID
		{
			get { throw new NotImplementedException(); }
		}

		public override string FieldDescription
		{
			get { throw new NotImplementedException(); }
		}

		/// <summary>
		/// Gets the by id.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public override AgendaActividades GetById(AgendaActividades entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("AgendaActividades_Select");
				if (entidad.idAgendaActividad > 0)
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAgendaActividad", DbType.Int32, entidad.idAgendaActividad);
				if (entidad.cursoCicloLectivo.idCursoCicloLectivo > 0)
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCursoCicloLectivo", DbType.Int32, entidad.cursoCicloLectivo.idCursoCicloLectivo);
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				AgendaActividades objAgendaActividades = null;
				while (reader.Read())
				{
					objAgendaActividades = new AgendaActividades();

					objAgendaActividades.idAgendaActividad = Convert.ToInt32(reader["idAgendaActividad"]);
					objAgendaActividades.cursoCicloLectivo.idCicloLectivo = Convert.ToInt32(reader["idCicloLectivo"]);
					objAgendaActividades.cursoCicloLectivo.idCurso = Convert.ToInt32(reader["idCurso"]);
					objAgendaActividades.cursoCicloLectivo.idCursoCicloLectivo = Convert.ToInt32(reader["idCursoCicloLectivo"]);
					objAgendaActividades.cursoCicloLectivo.curso.nombre = reader["curso"].ToString();
					objAgendaActividades.cursoCicloLectivo.cicloLectivo.nombre = reader["cicloLectivo"].ToString();
					objAgendaActividades.cursoCicloLectivo.cicloLectivo.fechaInicio = Convert.ToDateTime(reader["fechaInicio"].ToString());
					objAgendaActividades.cursoCicloLectivo.cicloLectivo.fechaFin = Convert.ToDateTime(reader["fechaFin"].ToString());
					objAgendaActividades.descripcion = reader["descripcion"].ToString();
					objAgendaActividades.activo = Convert.ToBoolean(reader["activo"].ToString());
					objAgendaActividades.fechaCreacion = Convert.ToDateTime(reader["fechaCreacion"].ToString());
				}
				return objAgendaActividades;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetById()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetById()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
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

		#region --[Métodos Públicos]--
		/// <summary>
		/// Gets the agenda actividades.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<AgendaActividades> GetAgendaActividades(AgendaActividades entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("AgendaActividades_Select");
				if (entidad != null)
				{
					if (entidad.idAgendaActividad > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAgendaActividad", DbType.Int32, entidad.idAgendaActividad);
					if (entidad.cursoCicloLectivo.idCursoCicloLectivo > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCursoCicloLectivo", DbType.Int32, entidad.cursoCicloLectivo.idCursoCicloLectivo);
					if (entidad.cursoCicloLectivo.idCicloLectivo > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCicloLectivo", DbType.Int32, entidad.cursoCicloLectivo.idCicloLectivo);
					if (!string.IsNullOrEmpty(entidad.descripcion))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);
					if (ValidarFechaSQL(entidad.fechaCreacion))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaCreacion", DbType.Date, entidad.fechaCreacion);
					if (!string.IsNullOrEmpty(entidad.usuario))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@usuario", DbType.String, entidad.usuario);
					if (entidad.activo)
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
					objAgendaActividades.cursoCicloLectivo.cicloLectivo.fechaInicio = Convert.ToDateTime(reader["fechaInicio"].ToString());
					objAgendaActividades.cursoCicloLectivo.cicloLectivo.fechaFin = Convert.ToDateTime(reader["fechaFin"].ToString());
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
		/// Gets the evaluaciones agenda.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Evaluacion> GetEvaluacionesAgenda(Evaluacion entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Evaluacion_Select");
				if (entidad != null)
				{
					if (entidad.idAgendaActividad > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAgendaActividad", DbType.Int32, entidad.idAgendaActividad);
					if (entidad.idEventoAgenda > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEvento", DbType.Int32, entidad.idEventoAgenda);
					if (entidad.asignatura.idAsignatura > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignaturaCicloLectivo", DbType.Int32, entidad.asignatura.idAsignatura);
					if (ValidarFechaSQL(entidad.fechaEventoDesde))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaDesde", DbType.Date, entidad.fechaEventoDesde);
					if (ValidarFechaSQL(entidad.fechaEventoHasta))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaHasta", DbType.Date, entidad.fechaEventoHasta);
					if (ValidarFechaSQL(entidad.fechaEvento))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaEvento", DbType.Date, entidad.fechaEvento);
					if (entidad.activo)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<Evaluacion> listaEventos = new List<Evaluacion>();
				Evaluacion objEvento;
				while (reader.Read())
				{
					objEvento = new Evaluacion();

					objEvento.idEvaluacion = Convert.ToInt32(reader["idEvaluacion"]);
					objEvento.idEventoAgenda = Convert.ToInt32(reader["idEventoAgenda"]);
					objEvento.asignatura.nombre = reader["asignatura"].ToString();
					objEvento.asignatura.idAsignatura = Convert.ToInt32(reader["idAsignaturaCurso"]);
					objEvento.descripcion = reader["descripcion"].ToString();
					objEvento.activo = Convert.ToBoolean(reader["activo"].ToString());
					objEvento.fechaAlta = Convert.ToDateTime(reader["fechaAlta"].ToString());
					if (!string.IsNullOrEmpty(reader["fechaModificacion"].ToString()))
						objEvento.fechaModificacion = Convert.ToDateTime(reader["fechaModificacion"].ToString());
					objEvento.fechaEvento = Convert.ToDateTime(reader["fechaEvento"].ToString());
					objEvento.tipoEventoAgenda.descripcion = reader["tipoEvento"].ToString();
					objEvento.tipoEventoAgenda.idTipoEventoAgenda = Convert.ToInt32(reader["idTipoEvento"]);
					objEvento.usuario.nombre = reader["nombre"].ToString();
					objEvento.usuario.apellido = reader["apellido"].ToString();
					if (!string.IsNullOrEmpty(reader["usernameOrganizador"].ToString()))
						objEvento.usuario.username = reader["usernameOrganizador"].ToString();

					//Descartamos mostrar las evaluaciones que han acontecido en el pasado (se desactivan)
					//if (objEvento.fechaEvento < DateTime.Now && objEvento.activo == true)
					//{
					//    objEvento.activo = false;
					//    DAEvaluacion objDA = new DAEvaluacion();
					//    objDA.Update(objEvento);
					//}
					
					listaEventos.Add(objEvento);
				}
				return listaEventos;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetEvaluacionesAgenda()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetEvaluacionesAgenda()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Gets the reuniones agenda.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Reunion> GetReunionesAgenda(Reunion entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Reunion_Select");
				if (entidad != null)
				{
					if (entidad.idAgendaActividad > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAgendaActividad", DbType.Int32, entidad.idAgendaActividad);
					if (entidad.idEventoAgenda > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEvento", DbType.Int32, entidad.idEventoAgenda);
					if (ValidarFechaSQL(entidad.fechaEvento))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaEvento", DbType.Date, entidad.fechaEvento);
					if (ValidarFechaSQL(entidad.fechaEventoDesde))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaDesde", DbType.Date, entidad.fechaEventoDesde);
					if (ValidarFechaSQL(entidad.fechaEventoHasta))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaHasta", DbType.Date, entidad.fechaEventoHasta);
					//if (entidad.activo)
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<Reunion> listaEventos = new List<Reunion>();
				Reunion objEvento;
				while (reader.Read())
				{
					objEvento = new Reunion();

					objEvento.idReunion = Convert.ToInt32(reader["idReunion"]);
					objEvento.idEventoAgenda = Convert.ToInt32(reader["idEventoAgenda"]);
					objEvento.descripcion = reader["descripcion"].ToString();
					objEvento.activo = Convert.ToBoolean(reader["activo"].ToString());
					objEvento.fechaAlta = Convert.ToDateTime(reader["fechaAlta"].ToString());
					if (!string.IsNullOrEmpty(reader["fechaModificacion"].ToString()))
						objEvento.fechaModificacion = Convert.ToDateTime(reader["fechaModificacion"].ToString());
					objEvento.fechaEvento = Convert.ToDateTime(reader["fechaEvento"].ToString());
					objEvento.tipoEventoAgenda.descripcion = reader["tipoEvento"].ToString();
					objEvento.tipoEventoAgenda.idTipoEventoAgenda = Convert.ToInt32(reader["idTipoEvento"]);
					objEvento.horario = Convert.ToDateTime(reader["horario"].ToString());
					objEvento.usuario.nombre = reader["nombre"].ToString();
					objEvento.usuario.apellido = reader["apellido"].ToString();
					if (!string.IsNullOrEmpty(reader["usernameOrganizador"].ToString()))
						objEvento.usuario.username = reader["usernameOrganizador"].ToString();

					//Descartamos mostrar las reuniones que han acontecido en el pasado (se desactivan)
					//if (objEvento.fechaEvento < DateTime.Now && objEvento.activo == true)
					//{
					//    objEvento.activo = false;
					//    DAReunion objDA = new DAReunion();
					//    objDA.Update(objEvento);
					//}

					listaEventos.Add(objEvento);
				}
				return listaEventos;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetReunionesAgenda()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetReunionesAgenda()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Gets the excursiones agenda.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Excursion> GetExcursionesAgenda(Excursion entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Excursion_Select");
				if (entidad != null)
				{
					if (entidad.idExcursion > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idExcursion", DbType.Int32, entidad.idExcursion);
					if (entidad.idAgendaActividad > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAgendaActividad", DbType.Int32, entidad.idAgendaActividad);
					if (entidad.idEventoAgenda > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEvento", DbType.Int32, entidad.idEventoAgenda);
					if (ValidarFechaSQL(entidad.fechaEvento))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaEvento", DbType.Date, entidad.fechaEvento);
					if (ValidarFechaSQL(entidad.fechaEventoDesde))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaDesde", DbType.Date, entidad.fechaEventoDesde);
					if (ValidarFechaSQL(entidad.fechaEventoHasta))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaHasta", DbType.Date, entidad.fechaEventoHasta);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<Excursion> listaEventos = new List<Excursion>();
				Excursion objEvento;
				while (reader.Read())
				{
					objEvento = new Excursion();

					objEvento.idExcursion = Convert.ToInt32(reader["idExcursion"]);
					objEvento.idEventoAgenda = Convert.ToInt32(reader["idEventoAgenda"]);
					objEvento.descripcion = reader["descripcion"].ToString();
					objEvento.activo = Convert.ToBoolean(reader["activo"].ToString());
					objEvento.fechaAlta = Convert.ToDateTime(reader["fechaAlta"].ToString());
					if (!string.IsNullOrEmpty(reader["fechaModificacion"].ToString()))
						objEvento.fechaModificacion = Convert.ToDateTime(reader["fechaModificacion"].ToString());
					objEvento.fechaEvento = Convert.ToDateTime(reader["fechaEvento"].ToString());
					objEvento.tipoEventoAgenda.descripcion = reader["tipoEvento"].ToString();
					objEvento.tipoEventoAgenda.idTipoEventoAgenda = Convert.ToInt32(reader["idTipoEvento"]);
					objEvento.horaDesde = Convert.ToDateTime(reader["horaDesde"].ToString());
					objEvento.horaHasta = Convert.ToDateTime(reader["horaHasta"].ToString());
					objEvento.destino = reader["destino"].ToString();
					objEvento.usuario.nombre = reader["nombre"].ToString();
					objEvento.usuario.apellido = reader["apellido"].ToString();
					if (!string.IsNullOrEmpty(reader["usernameOrganizador"].ToString()))
						objEvento.usuario.username = reader["usernameOrganizador"].ToString();

					//Descartamos mostrar las excursiones que han acontecido en el pasado (se desactivan)
					//if (objEvento.fechaEvento < DateTime.Now && objEvento.activo == true)
					//{
					//    objEvento.activo = false;
					//    DAExcursion objDA = new DAExcursion();
					//    objDA.Update(objEvento);
					//}

					listaEventos.Add(objEvento);
				}
				return listaEventos;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetExcursionesAgenda1()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetExcursionesAgenda2()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Gets the registro clases agenda.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<RegistroClases> GetRegistroClasesAgenda(RegistroClases entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("RegistroClases_Select");
				if (entidad != null)
				{
					if (entidad.idAgendaActividad > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAgendaActividad", DbType.Int32, entidad.idAgendaActividad);
					if (entidad.idEventoAgenda > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEvento", DbType.Int32, entidad.idEventoAgenda);
					if (entidad.asignatura.idAsignatura > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignaturaCicloLectivo", DbType.Int32, entidad.asignatura.idAsignatura);
					if (ValidarFechaSQL(entidad.fechaEventoDesde))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaDesde", DbType.Date, entidad.fechaEventoDesde);
					if (ValidarFechaSQL(entidad.fechaEventoHasta))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaHasta", DbType.Date, entidad.fechaEventoHasta);
					if (ValidarFechaSQL(entidad.fechaEvento))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaEvento", DbType.Date, entidad.fechaEvento);
					if (entidad.activo)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<RegistroClases> listaEventos = new List<RegistroClases>();
				RegistroClases objEvento;
				while (reader.Read())
				{
					objEvento = new RegistroClases();

					objEvento.idRegistroClases = Convert.ToInt32(reader["idRegistroClases"]);
					objEvento.tipoRegistro.idTipoRegistroClases = Convert.ToInt32(reader["idTipoRegistroClases"]);
					objEvento.tipoRegistro.nombre = reader["tipoRegistroClases"].ToString();
					
					objEvento.idEventoAgenda = Convert.ToInt32(reader["idEventoAgenda"]);
					objEvento.asignatura.nombre = reader["asignatura"].ToString();
					objEvento.asignatura.idAsignatura = Convert.ToInt32(reader["idAsignaturaCurso"]);
					objEvento.descripcion = reader["descripcion"].ToString();
					objEvento.activo = Convert.ToBoolean(reader["activo"].ToString());
					objEvento.fechaAlta = Convert.ToDateTime(reader["fechaAlta"].ToString());
					if (!string.IsNullOrEmpty(reader["fechaModificacion"].ToString()))
						objEvento.fechaModificacion = Convert.ToDateTime(reader["fechaModificacion"].ToString());
					objEvento.fechaEvento = Convert.ToDateTime(reader["fechaEvento"].ToString());
					objEvento.tipoEventoAgenda.descripcion = reader["tipoEvento"].ToString();
					objEvento.tipoEventoAgenda.idTipoEventoAgenda = Convert.ToInt32(reader["idTipoEvento"]);
					objEvento.usuario.nombre = reader["nombre"].ToString();
					objEvento.usuario.apellido = reader["apellido"].ToString();
					if (!string.IsNullOrEmpty(reader["usernameOrganizador"].ToString()))
						objEvento.usuario.username = reader["usernameOrganizador"].ToString();
					listaEventos.Add(objEvento);
				}
				return listaEventos;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetRegistroClasesAgenda()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetRegistroClasesAgenda()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Gets the eventos agenda.
		/// </summary>
		/// <param name="idAgendaActividades">The id agenda actividades.</param>
		/// <returns></returns>
		public List<EventoAgenda> GetEventosAgenda(int idAgendaActividades)
		{
			try
			{
				return GetEventosAgenda(idAgendaActividades, new DateTime(), new DateTime());
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

		/// <summary>
		/// Gets the eventos agenda.
		/// </summary>
		/// <param name="idAgendaActividades">The id agenda actividades.</param>
		/// <returns></returns>
		public List<EventoAgenda> GetEventosAgenda(int idAgendaActividades, DateTime fechaDesde, DateTime fechaHasta)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("EventoAgenda_Select");
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAgendaActividad", DbType.Int32, idAgendaActividades);

				if (ValidarFechaSQL(fechaDesde))
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaDesde", DbType.Date, fechaDesde);
				if (ValidarFechaSQL(fechaHasta))
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaHasta", DbType.Date, fechaHasta);

				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<EventoAgenda> listaEventos = new List<EventoAgenda>();
				EventoAgenda objEvento = null;
				while (reader.Read())
				{
					objEvento = new EventoAgenda();

					objEvento.idAgendaActividad = Convert.ToInt32(reader["idAgendaActividades"]);
					objEvento.idEventoAgenda = Convert.ToInt32(reader["idEventoAgenda"]);
					objEvento.usuario.nombre = reader["nombre"].ToString();
					objEvento.usuario.apellido = reader["apellido"].ToString();                      
					objEvento.activo = Convert.ToBoolean(reader["activo"].ToString());
					objEvento.fechaAlta = Convert.ToDateTime(reader["fechaAlta"].ToString());
					objEvento.fechaEvento = Convert.ToDateTime(reader["fechaEvento"].ToString());
					objEvento.descripcion = reader["descripcion"].ToString();
					objEvento.tipoEventoAgenda.descripcion = reader["tipoEvento"].ToString();

                    //COMENTADO PARA LA PRESENTACIÓN... NO DEJABA CONSULTAR HACIA ATRAS EN LAS FECHAS
                    //if (objEvento.fechaEvento >= DateTime.Now && objEvento.activo == true)
                    //{
                    //    listaEventos.Add(objEvento);
                    //}
                    if (objEvento.activo == true)
                        listaEventos.Add(objEvento);
				}
				return listaEventos;
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

		/// <summary>
		/// Verificars the agenda evaluacion.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <param name="idAsignatura">The id asignatura.</param>
		/// <returns></returns>
		public bool VerificarAgendaEvaluacion(EventoAgenda entidad, int idAsignatura)
		{
			//TODO: Verificar que se pueda definir una evaluación de una aisgnatura diferente en un mismo día.
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Evaluacion_VerificarDisponibilidadAgenda");

				if (ValidarFechaSQL(entidad.fechaEvento))
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "fechaEvento", DbType.DateTime, entidad.fechaEvento);
				if (entidad.idAgendaActividad > 0)
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "idAgendaActividad", DbType.Int32, entidad.idAgendaActividad);
				if (idAsignatura > 0)
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "idAsignaturaCurso", DbType.Int32, idAsignatura);
				if (entidad.idEventoAgenda > 0)
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEventoAgenda", DbType.Int32, entidad.idEventoAgenda);
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				while (reader.Read())
				{
					return false;
				}
				return true;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - VerificarAgendaEvaluacion()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - VerificarAgendaEvaluacion()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Verificars the agenda excursiones.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public bool VerificarAgendaExcursiones(EventoAgenda entidad)
		{
			//TODO: Verificar que se pueda definir una evaluación de una aisgnatura diferente en un mismo día.
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Excursion_VerificarDisponibilidadAgenda");

				if (ValidarFechaSQL(entidad.fechaEvento))
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "fechaEvento", DbType.DateTime, entidad.fechaEvento);
				if (entidad.idAgendaActividad > 0)
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "idAgendaActividad", DbType.Int32, entidad.idAgendaActividad);
				if (entidad.idEventoAgenda > 0)
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "idEventoAgenda", DbType.Int32, entidad.idEventoAgenda);
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				while (reader.Read())
				{
					return false;
				}
				return true;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - VerificarAgenda()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - VerificarAgenda()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Verificars the agenda reuniones.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public bool VerificarAgendaReuniones(EventoAgenda entidad)
		{
			bool disponible = true;
			int tipoEvento = entidad.tipoEventoAgenda.idTipoEventoAgenda;

			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("ReunionValidarDisponibilidad_Select");

				if (tipoEvento == (int)enumEventoAgendaType.Reunion)
				{
					if (entidad.fechaEvento != null)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "fecha", DbType.DateTime, entidad.fechaEvento);
					if (entidad.idEventoAgenda > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "idEventoAgenda", DbType.Int32, entidad.idEventoAgenda);

					IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

					while (reader.Read())
					{
						disponible = false;
					}
				}

				return disponible;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - VerificarAgendaReuniones()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - VerificarAgendaReuniones()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Verificars the agenda registro clases.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <param name="idAsignatura">The id asignatura.</param>
		/// <returns></returns>
		public bool VerificarAgendaRegistroClases(EventoAgenda entidad, int idAsignatura)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("RegistroClases_VerificarDisponibilidadAgenda");

				if (ValidarFechaSQL(entidad.fechaEvento))
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "fechaEvento", DbType.DateTime, entidad.fechaEvento);
				if (entidad.idAgendaActividad > 0)
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "idAgendaActividad", DbType.Int32, entidad.idAgendaActividad);
				if (idAsignatura > 0)
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "idAsignaturaCurso", DbType.Int32, idAsignatura);
				if (entidad.idEventoAgenda > 0)
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEventoAgenda", DbType.Int32, entidad.idEventoAgenda);
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				while (reader.Read())
				{
					return false;
				}
				return true;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - VerificarAgendaRegistroClases()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - VerificarAgendaRegistroClases()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
		#endregion
	}
}
