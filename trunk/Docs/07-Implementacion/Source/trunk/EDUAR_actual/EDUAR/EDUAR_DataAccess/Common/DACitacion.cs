using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_DataAccess.Common
{
	public class DACitacion : DataAccesBase<Citacion>
	{
		#region --[Atributos]--
		private const string ClassName = "DACitacion";
		#endregion

		#region --[Constructor]--
		public DACitacion()
		{
		}

		public DACitacion(DATransaction objDATransaction)
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

		public override Citacion GetById(Citacion entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(Citacion entidad)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Creates the specified entidad.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <param name="identificador">The identificador.</param>
		public override void Create(Citacion entidad, out int identificador)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Citacion_Insert");

				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCitacion", DbType.Int32, 0);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idMotivoCitacion", DbType.Int32, entidad.motivoCitacion.idMotivoCitacion);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTutor", DbType.Int32, entidad.tutor.idTutor);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fecha", DbType.Date, entidad.fecha.Date.ToShortDateString());
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@hora", DbType.Time, entidad.hora.ToShortTimeString());
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.organizador.username);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@detalle", DbType.String, entidad.detalles);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCurso", DbType.Int32, entidad.cursoCicloLectivo.idCurso);

				if (Transaction.Transaction != null)
					Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
				else
					Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

				identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idCitacion"].Value.ToString());

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
		public override void Update(Citacion entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Citacion_Update");

				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCitacion", DbType.Int32, entidad.idCitacion);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idMotivoCitacion", DbType.Int32, entidad.motivoCitacion.idMotivoCitacion);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTutor", DbType.Int32, entidad.tutor.idTutor);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fecha", DbType.Date, entidad.fecha.Date.ToShortDateString());
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@hora", DbType.Time, Convert.ToDateTime(entidad.hora.Hour + ":" + entidad.hora.Minute));
				//Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.organizador.username);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idOrganizador", DbType.Int32, entidad.organizador.idPersonal);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@detalle", DbType.String, entidad.detalles);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);

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

		public override void Delete(Citacion entidad)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Gets the citaciones.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Citacion> GetCitaciones(Citacion entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Citacion_Select");
				if (entidad != null)
				{
					if (entidad.idCitacion > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCitacion", DbType.Int32, entidad.idCitacion);
					if (entidad.motivoCitacion.idMotivoCitacion > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idMotivoCitacion", DbType.Int32, entidad.motivoCitacion.idMotivoCitacion);
					if (entidad.tutor.idTutor > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTutor", DbType.Int32, entidad.tutor.idTutor);
					if (!string.IsNullOrEmpty(entidad.organizador.username))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@usuario", DbType.String, entidad.organizador.username);
					if (!string.IsNullOrEmpty(entidad.detalles))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@detalle", DbType.String, entidad.detalles);
					if (ValidarFechaSQL(entidad.fechaEventoDesde))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaDesde", DbType.Date, entidad.fechaEventoDesde);
					if (ValidarFechaSQL(entidad.fechaEventoHasta))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaHasta", DbType.Date, entidad.fechaEventoHasta);
					if (!string.IsNullOrEmpty(entidad.tutor.username))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@usuarioTutor", DbType.String, entidad.tutor.username);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
					if (entidad.vencidas)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@vencidas", DbType.Boolean, entidad.vencidas);
				}
				else
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, true);

				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<Citacion> listaCitaciones = new List<Citacion>();
				Citacion objCitacion;
				while (reader.Read())
				{
					objCitacion = new Citacion();
					objCitacion.idCitacion = Convert.ToInt32(reader["idCitacion"]);
					objCitacion.detalles = reader["detalle"].ToString();
					objCitacion.fecha = Convert.ToDateTime(reader["fecha"].ToString());
					if (!string.IsNullOrEmpty(reader["hora"].ToString()))
						objCitacion.hora = Convert.ToDateTime(reader["hora"].ToString());
					objCitacion.organizador.idPersonal = Convert.ToInt32(reader["idOrganizador"]);
					objCitacion.organizador.nombre = reader["nombreOrganizador"].ToString();
					objCitacion.organizador.apellido = reader["apellidoOrganizador"].ToString();
					if (!string.IsNullOrEmpty(reader["usernameOrganizador"].ToString()))
						objCitacion.organizador.username = reader["usernameOrganizador"].ToString();
					objCitacion.tutor.idTutor = Convert.ToInt32(reader["idTutor"]);
					objCitacion.tutor.nombre = reader["nombreTutor"].ToString();
					objCitacion.tutor.apellido = reader["apellidoTutor"].ToString();
					objCitacion.motivoCitacion.idMotivoCitacion = Convert.ToInt32(reader["idMotivoCitacion"]);
					objCitacion.motivoCitacion.nombre = reader["motivoCitacion"].ToString();
					objCitacion.activo = Convert.ToBoolean(reader["activo"]);
					if (reader["idCurso"] != DBNull.Value)
					{
						objCitacion.cursoCicloLectivo.idCurso = Convert.ToInt32(reader["idCurso"]);
					}
					else
					{
						objCitacion.cursoCicloLectivo.idCurso = 0;
					}
					listaCitaciones.Add(objCitacion);
				}
				return listaCitaciones;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetCitaciones()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetCitaciones()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Verificars the disponibilidad.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public bool VerificarDisponibilidad(Citacion entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Citacion_Select");
				if (entidad != null)
				{
					if (entidad.idCitacion > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCitacion", DbType.Int32, entidad.idCitacion);
					if (entidad.tutor.idTutor > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTutor", DbType.Int32, entidad.tutor.idTutor);
					if (!string.IsNullOrEmpty(entidad.organizador.username))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@usuario", DbType.String, entidad.organizador.username);
                    if (ValidarFechaSQL(entidad.fecha))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fecha", DbType.Date, entidad.fecha);
					if (!string.IsNullOrEmpty(entidad.hora.ToShortTimeString()))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@hora", DbType.Time, entidad.hora.ToShortTimeString());
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				while (reader.Read())
				{
					return false;
				}
				return true;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - VerificarDisponibilidad()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - VerificarDisponibilidad()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
		#endregion
	}
}
