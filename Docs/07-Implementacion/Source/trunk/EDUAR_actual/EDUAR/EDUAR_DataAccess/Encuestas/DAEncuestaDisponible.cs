using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System.Data.SqlClient;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;
using System.Data;
using System.Collections.Generic;

namespace EDUAR_DataAccess.Encuestas
{
	public class DAEncuestaDisponible : DataAccesBase<EncuestaDisponible>
	{
		#region --[Atributos]--
		private const string ClassName = "DAEncuestaDisponible";
		#endregion

		#region --[Constructor]--
		public DAEncuestaDisponible()
		{
		}

		public DAEncuestaDisponible(DATransaction objDATransaction)
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

		public override EncuestaDisponible GetById(EncuestaDisponible entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(EncuestaDisponible entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("EncuestaUsuario_Insert");

				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, 0);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.usuario.username);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaRespuesta", DbType.Date, null);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaLimite", DbType.Date, entidad.fechaLimite);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@respondida", DbType.Boolean, false);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@expirada", DbType.Boolean, false);

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

		public override void Create(EncuestaDisponible entidad, out int identificador)
		{
			throw new NotImplementedException();
		}

		public override void Update(EncuestaDisponible entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("EncuestaUsuario_Update");

				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, entidad.encuesta.idEncuesta);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.usuario.username);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaRespuesta", DbType.Date, entidad.fechaRespuesta);

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

		/// <summary>
		/// No remueve la encuesta, sino que, una vez superada la fecha límite se expira y no está más disponible
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		public override void Delete(EncuestaDisponible entidad)
		{
			if (entidad.fechaLimite < DateTime.Now)
			{
				entidad.expirada = true;
				Update(entidad);
			}
		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Gets the encuestas respondidas en general.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <param name="idAmbito">El ámbito de la encuesta.</param>
		/// <returns></returns>
		public List<EncuestaDisponible> GetEncuestasRespondidas(EncuestaDisponible entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("EncuestaUsuarioRespondidas_Select");
				if (entidad != null)
				{
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, entidad.encuesta.idEncuesta);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, null);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@expirada", DbType.Boolean, false);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@respondida", DbType.Boolean, true);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaLimite", DbType.DateTime, null);
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<EncuestaDisponible> listaEncuestasDisponibles = new List<EncuestaDisponible>();
				EncuestaDisponible objEncuesta;
				while (reader.Read())
				{
					objEncuesta = new EncuestaDisponible();
					{
						objEncuesta.encuesta.idEncuesta = Convert.ToInt32(reader["idEncuesta"]);
					}

					listaEncuestasDisponibles.Add(objEncuesta);
				}
				return listaEncuestasDisponibles;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetEncuestasRespondidas()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetEncuestasRespondidas()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Gets the encuestas respondidas en general.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <param name="idAmbito">El ámbito de la encuesta.</param>
		/// <returns></returns>
		public List<Respuesta> GetRespuestasSumarizadas(EncuestaDisponible entidad)
		{
			try
			{
				return new List<Respuesta>();
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetRespuestasSumarizadas()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetRespuestasSumarizadas()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
		
		/// <summary>
		/// Gets the encuestas disponibles.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Encuesta> GetEncuestasDisponibles(EncuestaDisponible entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("EncuestaUsuarioDisponible_Select");
				if (entidad != null)
				{
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, DBNull.Value);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.usuario.username);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaRespuesta", DbType.DateTime, DBNull.Value);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaLimite", DbType.DateTime, DBNull.Value);
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<Encuesta> listaEncuestasDisponibles = new List<Encuesta>();
				Encuesta objEncuesta;
				DateTime fechaLanzamiento;
				while (reader.Read())
				{
					objEncuesta = new Encuesta();
					{
						objEncuesta.idEncuesta = Convert.ToInt32(reader["idEncuesta"]);
						objEncuesta.nombreEncuesta = reader["nombre"].ToString();

						objEncuesta.ambito = new AmbitoEncuesta();
						{
							objEncuesta.ambito.idAmbitoEncuesta = Convert.ToInt32(reader["idAmbito"]);
							objEncuesta.ambito.nombre = reader["nombreAmbito"].ToString();
						}

						if (DateTime.TryParse(reader["fechaLimite"].ToString(), out fechaLanzamiento))
							objEncuesta.fechaVencimiento = fechaLanzamiento;
					}

					listaEncuestasDisponibles.Add(objEncuesta);
				}
				return listaEncuestasDisponibles;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetEncuestasDisponibles()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetEncuestasDisponibles()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
		#endregion
	}
}