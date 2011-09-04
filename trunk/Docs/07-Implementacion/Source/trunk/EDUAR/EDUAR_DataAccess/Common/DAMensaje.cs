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
	public class DAMensaje : DataAccesBase<Mensaje>
	{
		#region --[Atributos]--
		private const string ClassName = "DAMensaje";
		#endregion

		#region --[Constructor]--
		public DAMensaje()
		{
		}

		public DAMensaje(DATransaction objDATransaction)
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

		public override Mensaje GetById(Mensaje entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(Mensaje entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(Mensaje entidad, out int identificador)
		{
			try
			{
				using (Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Mensaje_Insert"))
				{
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idMensaje", DbType.Int32, 0);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaEnvio", DbType.Date, entidad.fechaEnvio.Date.ToShortDateString());
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@horaEnvio", DbType.Time, entidad.horaEnvio.Hour + ":" + entidad.horaEnvio.Minute);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.remitente.Nombre);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@textoMensaje", DbType.String, entidad.textoMensaje);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);

					if (Transaction.Transaction != null)
						Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
					else
						Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

					identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idMensaje"].Value.ToString());
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

		public override void Update(Mensaje entidad)
		{
			throw new NotImplementedException();
		}

		public override void Delete(Mensaje entidad)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region --[Métodos Públicos]--
		#endregion

		/// <summary>
		/// Gets the mensajes.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Mensaje> GetMensajes(Mensaje entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("MensajeRecibido_Select");
				if (entidad != null)
				{
					//if (entidad.idAgendaActividad > 0)
					//    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAgendaActividad", DbType.Int32, entidad.idAgendaActividad);
					//if (entidad.idEventoAgenda > 0)
					//    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEvento", DbType.Int32, entidad.idEventoAgenda);
					//if (entidad.asignatura.idAsignatura > 0)
					//    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignaturaCurso", DbType.Int32, entidad.asignatura.idAsignatura);
					//if (ValidarFechaSQL(entidad.fechaEventoDesde))
					//    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaDesde", DbType.Date, entidad.fechaEventoDesde);
					//if (ValidarFechaSQL(entidad.fechaEventoHasta))
					//    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaHasta", DbType.Date, entidad.fechaEventoHasta);
					//if (ValidarFechaSQL(entidad.fechaEvento))
					//    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaEvento", DbType.Date, entidad.fechaEvento);
					if (!string.IsNullOrEmpty(entidad.remitente.Nombre))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@usuarioDestino", DbType.String, entidad.remitente.Nombre);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@leido", DbType.Boolean, false);
					if (entidad.activo)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<Mensaje> listaMensaje = new List<Mensaje>();
				Mensaje objMensaje;
				while (reader.Read())
				{
					objMensaje = new Mensaje();

					objMensaje.idMensaje = Convert.ToInt32(reader["idMensaje"]);
					objMensaje.textoMensaje = reader["textoMensaje"].ToString();
					objMensaje.activo = Convert.ToBoolean(reader["activo"].ToString());
					objMensaje.fechaEnvio = Convert.ToDateTime(reader["fechaEnvio"].ToString());
					listaMensaje.Add(objMensaje);
				}
				return listaMensaje;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetMensajes()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetMensajes()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
	}
}
