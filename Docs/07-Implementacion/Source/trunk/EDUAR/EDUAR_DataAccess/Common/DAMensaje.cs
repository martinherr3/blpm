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
					//if (entidad.leido)
					//Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@leido", DbType.Boolean, entidad.leido);
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
					objMensaje.idMensajeDestinatario = Convert.ToInt32(reader["idMensajeDestinatario"]);
					objMensaje.asuntoMensaje = reader["asuntoMensaje"].ToString();
					objMensaje.textoMensaje = reader["textoMensaje"].ToString();
					objMensaje.activo = Convert.ToBoolean(reader["activo"].ToString());
					objMensaje.fechaEnvio = Convert.ToDateTime(reader["fechaEnvio"].ToString());
					objMensaje.horaEnvio = Convert.ToDateTime(reader["horaEnvio"].ToString());
					objMensaje.destinatario.idPersona = Convert.ToInt32(reader["idPersonaDestinatario"]);
					objMensaje.destinatario.nombre = reader["nombreDestinatario"].ToString();
					objMensaje.destinatario.apellido = reader["apellidoDestinatario"].ToString();
					objMensaje.remitente.idPersona = Convert.ToInt32(reader["idPersonaRemitente"]);
					objMensaje.remitente.nombre = reader["nombreRemitente"].ToString();
					objMensaje.remitente.apellido = reader["apellidoRemitente"].ToString();
					objMensaje.remitente.tipoPersona.nombre = reader["tipoPersonaRemitente"].ToString();
					objMensaje.destinatario.tipoPersona.nombre = reader["tipoPersonaDestinatario"].ToString();
					objMensaje.leido = Convert.ToBoolean(reader["leido"]);
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

		/// <summary>
		/// Leers the mensaje.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		public void LeerMensaje(Mensaje entidad)
		{
			try
			{
				using (Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("MensajeDestinatario_Update"))
				{
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idMensajeDestinatario", DbType.Int32, entidad.idMensajeDestinatario);
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
				throw new CustomizedException(string.Format("Fallo en {0} - LeerMensaje()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - LeerMensaje()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Eliminars the mensaje.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		public void EliminarMensaje(Mensaje entidad)
        {
            try
            {
				if (entidad.idMensajeDestinatario > 0)
				{
					using (Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("MensajeDestinatario_Update"))
					{
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idMensajeDestinatario", DbType.Int32, entidad.idMensajeDestinatario);
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@leido", DbType.Boolean, entidad.leido);
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);

						if (Transaction.Transaction != null)
							Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
						else
							Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
					}
				}
				else

					if (entidad.idMensaje > 0)
					{
						using (Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("MensajeRemitente_Update"))
						{
							Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idMensaje", DbType.Int32, entidad.idMensaje);
							Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);

							if (Transaction.Transaction != null)
								Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
							else
								Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
						}
					}
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - EliminarMensaje()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - EliminarMensaje()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

		/// <summary>
		/// Eliminars the mensaje.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		public void EliminarListaMensajes(Mensaje entidad)
		{
			try
			{
				if (entidad.idMensajeDestinatario > 0)
				{
					using (Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("MensajesDestinatario_DesactivarLista"))
					{
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@listaMensajes", DbType.String, entidad.listaIDMensaje);
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@leido", DbType.Boolean, entidad.leido);
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);

						if (Transaction.Transaction != null)
							Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
						else
							Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
					}
				}
				else

					if (entidad.idMensaje > 0)
					{
						using (Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("MensajesRemitente_DesactivarLista"))
						{
							Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@listaMensajes", DbType.String, entidad.listaIDMensaje);
							Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);

							if (Transaction.Transaction != null)
								Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
							else
								Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
						}
					}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - EliminarListaMensajes()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - EliminarListaMensajes()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Gets the mensajes.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Mensaje> GetMensajesEnviados(Mensaje entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("MensajeEnviado_Select");
				if (entidad != null)
				{
					if (!string.IsNullOrEmpty(entidad.destinatario.username))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@usuarioRemitente", DbType.String, entidad.remitente.username);
					//if (entidad.leido)
					//Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@leido", DbType.Boolean, entidad.leido);
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
					objMensaje.idMensajeDestinatario = Convert.ToInt32(reader["idMensajeDestinatario"]);
					objMensaje.asuntoMensaje = reader["asuntoMensaje"].ToString();
					objMensaje.textoMensaje = reader["textoMensaje"].ToString();
					objMensaje.activo = Convert.ToBoolean(reader["activo"].ToString());
					objMensaje.fechaEnvio = Convert.ToDateTime(reader["fechaEnvio"].ToString());
					objMensaje.horaEnvio = Convert.ToDateTime(reader["horaEnvio"].ToString());
					objMensaje.destinatario.idPersona = Convert.ToInt32(reader["idPersonaDestinatario"]);
					objMensaje.destinatario.nombre = reader["nombreDestinatario"].ToString();
					objMensaje.destinatario.apellido = reader["apellidoDestinatario"].ToString();
					objMensaje.remitente.idPersona = Convert.ToInt32(reader["idPersonaRemitente"]);
					objMensaje.remitente.nombre = reader["nombreRemitente"].ToString();
					objMensaje.remitente.apellido = reader["apellidoRemitente"].ToString();
					objMensaje.leido = Convert.ToBoolean(reader["leido"]);
					listaMensaje.Add(objMensaje);
				}
				return listaMensaje;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetMensajesEnviados()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetMensajesEnviados()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
		#endregion

	}
}
