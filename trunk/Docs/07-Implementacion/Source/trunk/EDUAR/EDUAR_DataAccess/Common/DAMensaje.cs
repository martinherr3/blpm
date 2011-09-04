﻿using System;
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

		/// <summary>
		/// Creates the specified entidad.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <param name="identificador">The identificador.</param>
		public override void Create(Mensaje entidad, out int identificador)
		{
			try
			{
				using (Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Mensaje_Insert"))
				{
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idMensaje", DbType.Int32, 0);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaEnvio", DbType.Date, entidad.fechaEnvio.Date.ToShortDateString());
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@horaEnvio", DbType.Time, entidad.horaEnvio.Hour + ":" + entidad.horaEnvio.Minute);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.remitente.username);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@asuntoMensaje", DbType.String, entidad.asuntoMensaje);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@textoMensaje", DbType.String, entidad.textoMensaje);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);

					Transaction.DBcomand.Parameters["@idMensaje"].Direction = ParameterDirection.Output;

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
					if (!string.IsNullOrEmpty(entidad.destinatario.username))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@usuarioDestino", DbType.String, entidad.destinatario.username);
					if (entidad.leido)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@leido", DbType.Boolean, entidad.leido);
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
					objMensaje.asuntoMensaje = reader["asuntoMensaje"].ToString();
					objMensaje.textoMensaje = reader["textoMensaje"].ToString();
					objMensaje.activo = Convert.ToBoolean(reader["activo"].ToString());
					objMensaje.fechaEnvio = Convert.ToDateTime(reader["fechaEnvio"].ToString());
					objMensaje.horaEnvio = Convert.ToDateTime(reader["horaEnvio"].ToString());
					objMensaje.destinatario.nombre = reader["nombreDestinatario"].ToString();
					objMensaje.destinatario.apellido = reader["apellidoDestinatario"].ToString();
					objMensaje.remitente.nombre = reader["nombreRemitente"].ToString();
					objMensaje.remitente.apellido = reader["apellidoRemitente"].ToString();
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

		/// <summary>
		/// Saves the destinatario.
		/// </summary>
		/// <param name="Data">The data.</param>
		public void SaveDestinatario(Mensaje entidad)
		{
			try
			{
				using (Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("MensajeDestinatarios_Insert"))
				{
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idMensaje", DbType.Int32, entidad.idMensaje);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPersonaDestinatario", DbType.Int32, entidad.destinatario.idPersona);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@leido", DbType.Boolean, entidad.leido);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);

					if (Transaction.Transaction != null)
						Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
					else
						Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - SaveDestinatario()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - SaveDestinatario()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
		#endregion
	}
}
