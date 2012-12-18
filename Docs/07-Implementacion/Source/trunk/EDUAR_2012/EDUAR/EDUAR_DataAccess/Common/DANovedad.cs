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
	public class DANovedad : DataAccesBase<Novedad>
	{
		#region --[Atributos]--
		private const string ClassName = "DANovedad";
		#endregion

		#region --[Constructor]--
		public DANovedad()
		{
		}

		public DANovedad(DATransaction objDATransaction)
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
		public override Novedad GetById(Novedad entidad)
		{
			try
			{
				Novedad objEntidad = null;

				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("NovedadAulica_Select");
				if (entidad != null)
				{
					if (entidad.idNovedad > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idNovedadAulica", DbType.Int32, entidad.idNovedad);
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				int idNovedadPadre = 0;

				int dia, mes, anio, hora, minuto;
				while (reader.Read())
				{
					objEntidad = new Novedad();
					objEntidad.idNovedad = Convert.ToInt32(reader["idNovedadAulica"]);
					objEntidad.usuario = new Persona()
					{
						idPersona = Convert.ToInt32(reader["idPersona"]),
						nombre = reader["nombrePersona"].ToString(),
						apellido = reader["apellidoPersona"].ToString()
					};

					dia = Convert.ToDateTime(reader["fecha"]).Day;
					mes = Convert.ToDateTime(reader["fecha"]).Month;
					anio = Convert.ToDateTime(reader["fecha"]).Year;

					hora = Convert.ToDateTime(reader["hora"].ToString()).Hour;
					minuto = Convert.ToDateTime(reader["hora"].ToString()).Minute;

					objEntidad.fecha = new DateTime(anio, mes, dia, hora, minuto, 0);

					//objEntidad.fecha = Convert.ToDateTime(reader["fecha"]);
					objEntidad.observaciones = reader["observaciones"].ToString();
					objEntidad.curso.idCurso = Convert.ToInt32(reader["idCursoCicloLectivo"]);

					int.TryParse(reader["idNovedadAulicaPrincipal"].ToString(), out idNovedadPadre);
					if (idNovedadPadre > 0)
					{
						objEntidad.novedadPadre = new Novedad();
						objEntidad.novedadPadre.idNovedad = idNovedadPadre;
					}

					objEntidad.tipo = new TipoNovedad()
					{
						idTipoNovedad = Convert.ToInt32(reader["idTipoNovedad"]),
						nombre = reader["tipoNovedad"].ToString()
					};

					objEntidad.estado = new EstadoNovedad()
					{
						idEstadoNovedad = Convert.ToInt32(reader["idEstadoNovedad"]),
						nombre = reader["estadoNovedad"].ToString(),
						esFinal = Convert.ToBoolean(reader["esFinal"])
					};

					//listaEntidad.Add(objEntidad);
				}
				return objEntidad;
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

		public override void Create(Novedad entidad)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Creates the specified entidad.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <param name="identificador">The identificador.</param>
		public override void Create(Novedad entidad, out int identificador)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("NovedadAulica_Insert");

				Transaction.DataBase.AddOutParameter(Transaction.DBcomand, "@idNovedadAulica", DbType.Int32, 2);
				if (entidad.novedadPadre != null && entidad.novedadPadre.idNovedad > 0)
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idNovedadAulicaPrincipal", DbType.Int32, entidad.novedadPadre.idNovedad);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@usuarioCreador", DbType.String, entidad.usuario.username);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fecha", DbType.Date, DateTime.Today);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@hora", DbType.Time, DateTime.Now);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@observaciones", DbType.String, entidad.observaciones);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCursoCicloLectivo", DbType.Int32, entidad.curso.idCurso);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoNovedad", DbType.Int32, entidad.tipo.idTipoNovedad);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEstadoNovedad", DbType.Int32, entidad.estado.idEstadoNovedad);

				if (Transaction.Transaction != null)
					Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
				else
					Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

				identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idNovedadAulica"].Value.ToString());

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

		public override void Update(Novedad entidad)
		{
			throw new NotImplementedException();
		}

		public override void Delete(Novedad entidad)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Gets the novedad.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Novedad> GetNovedad(Novedad entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("NovedadAulica_Select");
				if (entidad != null)
				{
					if (entidad.curso.idCurso > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCursoCicloLectivo", DbType.Int32, entidad.curso.idCurso);
					if (entidad.idNovedad > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idNovedadAulica", DbType.Int32, entidad.idNovedad);
					if (entidad.tipo.idTipoNovedad > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoNovedad", DbType.Int32, entidad.tipo.idTipoNovedad);
					if (entidad.estado.idEstadoNovedad > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEstadoNovedad", DbType.Int32, entidad.estado.idEstadoNovedad);
					if (entidad.novedadPadre.idNovedad > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idNovedadAulicaPrincipal", DbType.Int32, entidad.novedadPadre.idNovedad);
					if (!string.IsNullOrEmpty(entidad.usuario.username))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@usuario", DbType.String, entidad.usuario.username);
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<Novedad> listaEntidad = new List<Novedad>();
				Novedad objEntidad;
				int idNovedadPadre = 0;

				int dia, mes, anio, hora, minuto;
				while (reader.Read())
				{
					objEntidad = new Novedad();
					objEntidad.idNovedad = Convert.ToInt32(reader["idNovedadAulica"]);
					objEntidad.usuario = new Persona()
					{
						idPersona = Convert.ToInt32(reader["idPersona"]),
						nombre = reader["nombrePersona"].ToString(),
						apellido = reader["apellidoPersona"].ToString()
					};

					dia = Convert.ToDateTime(reader["fecha"]).Day;
					mes = Convert.ToDateTime(reader["fecha"]).Month;
					anio = Convert.ToDateTime(reader["fecha"]).Year;

					hora = Convert.ToDateTime(reader["hora"].ToString()).Hour;
					minuto = Convert.ToDateTime(reader["hora"].ToString()).Minute;

					objEntidad.fecha = new DateTime(anio, mes, dia, hora, minuto, 0);

					//objEntidad.fecha = Convert.ToDateTime(reader["fecha"]);
					objEntidad.observaciones = reader["observaciones"].ToString();
					objEntidad.curso.idCurso = Convert.ToInt32(reader["idCursoCicloLectivo"]);

					int.TryParse(reader["idNovedadAulicaPrincipal"].ToString(), out idNovedadPadre);
					if (idNovedadPadre > 0)
					{
						objEntidad.novedadPadre = new Novedad();
						objEntidad.novedadPadre.idNovedad = idNovedadPadre;
					}

					objEntidad.tipo = new TipoNovedad()
					{
						idTipoNovedad = Convert.ToInt32(reader["idTipoNovedad"]),
						nombre = reader["tipoNovedad"].ToString()
					};

					objEntidad.estado = new EstadoNovedad()
					{
						idEstadoNovedad = Convert.ToInt32(reader["idEstadoNovedad"]),
						nombre = reader["estadoNovedad"].ToString(),
						esFinal = Convert.ToBoolean(reader["esFinal"])
					};

					listaEntidad.Add(objEntidad);
				}
				return listaEntidad;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetNovedad()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetNovedad()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Gets the novedades padre.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Novedad> GetNovedadesPadre(Novedad entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("NovedadAulicaPrincipales_Select");
				if (entidad != null)
				{
					if (entidad.curso.idCurso > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCursoCicloLectivo", DbType.Int32, entidad.curso.idCurso);
					if (entidad.idNovedad > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idNovedadAulica", DbType.Int32, entidad.idNovedad);
					if (entidad.tipo.idTipoNovedad > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoNovedad", DbType.Int32, entidad.tipo.idTipoNovedad);
					if (entidad.estado.idEstadoNovedad > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEstadoNovedad", DbType.Int32, entidad.estado.idEstadoNovedad);
					if (!string.IsNullOrEmpty(entidad.usuario.username))
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@usuario", DbType.String, entidad.usuario.username);
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<Novedad> listaEntidad = new List<Novedad>();
				Novedad objEntidad;
				int idNovedadPadre = 0;
				int dia, mes, anio, hora, minuto;
				while (reader.Read())
				{
					objEntidad = new Novedad();
					objEntidad.idNovedad = Convert.ToInt32(reader["idNovedadAulica"]);
					objEntidad.usuario = new Persona()
					{
						idPersona = Convert.ToInt32(reader["idPersona"]),
						nombre = reader["nombrePersona"].ToString(),
						apellido = reader["apellidoPersona"].ToString()
					};

					dia = Convert.ToDateTime(reader["fecha"]).Day;
					mes = Convert.ToDateTime(reader["fecha"]).Month;
					anio = Convert.ToDateTime(reader["fecha"]).Year;

					hora = Convert.ToDateTime(reader["hora"].ToString()).Hour;
					minuto = Convert.ToDateTime(reader["hora"].ToString()).Minute;

					objEntidad.fecha = new DateTime(anio, mes, dia, hora, minuto, 0);
					//objEntidad.fecha = Convert.ToDateTime(reader["fecha"]);
					objEntidad.observaciones = reader["observaciones"].ToString();
					objEntidad.curso.idCurso = Convert.ToInt32(reader["idCursoCicloLectivo"]);

					int.TryParse(reader["idNovedadAulicaPrincipal"].ToString(), out idNovedadPadre);
					if (idNovedadPadre > 0)
					{
						objEntidad.novedadPadre = new Novedad();
						objEntidad.novedadPadre.idNovedad = idNovedadPadre;
					}

					objEntidad.tipo = new TipoNovedad()
					{
						idTipoNovedad = Convert.ToInt32(reader["idTipoNovedad"]),
						nombre = reader["tipoNovedad"].ToString()
					};

					objEntidad.estado = new EstadoNovedad()
					{
						idEstadoNovedad = Convert.ToInt32(reader["idEstadoNovedad"]),
						nombre = reader["estadoNovedad"].ToString(),
						esFinal = Convert.ToBoolean(reader["esFinal"])
					};

					listaEntidad.Add(objEntidad);
				}
				return listaEntidad;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetNovedad()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetNovedad()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Gets the novedad indicadores.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Novedad> GetNovedadIndicadores(Novedad entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("NovedadAulicaIndicadores_Select");
				if (entidad != null)
				{
					if (entidad.curso.idCurso > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCursoCicloLectivo", DbType.Int32, entidad.curso.idCurso);
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<Novedad> listaEntidad = new List<Novedad>();
				Novedad objEntidad;
				int idNovedadPadre = 0;

				int dia, mes, anio, hora, minuto;
				while (reader.Read())
				{
					objEntidad = new Novedad();
					objEntidad.idNovedad = Convert.ToInt32(reader["idNovedadAulica"]);
					objEntidad.usuario = new Persona()
					{
						idPersona = Convert.ToInt32(reader["idPersona"]),
						nombre = reader["nombrePersona"].ToString(),
						apellido = reader["apellidoPersona"].ToString()
					};

					dia = Convert.ToDateTime(reader["fecha"]).Day;
					mes = Convert.ToDateTime(reader["fecha"]).Month;
					anio = Convert.ToDateTime(reader["fecha"]).Year;

					hora = Convert.ToDateTime(reader["hora"].ToString()).Hour;
					minuto = Convert.ToDateTime(reader["hora"].ToString()).Minute;

					objEntidad.fecha = new DateTime(anio, mes, dia, hora, minuto, 0);

					//objEntidad.fecha = Convert.ToDateTime(reader["fecha"]);
					objEntidad.observaciones = reader["observaciones"].ToString();
					objEntidad.curso.idCurso = Convert.ToInt32(reader["idCursoCicloLectivo"]);

					int.TryParse(reader["idNovedadAulicaPrincipal"].ToString(), out idNovedadPadre);
					if (idNovedadPadre > 0)
					{
						objEntidad.novedadPadre = new Novedad();
						objEntidad.novedadPadre.idNovedad = idNovedadPadre;
					}

					objEntidad.tipo = new TipoNovedad()
					{
						idTipoNovedad = Convert.ToInt32(reader["idTipoNovedad"]),
						nombre = reader["tipoNovedad"].ToString()
					};

					objEntidad.estado = new EstadoNovedad()
					{
						idEstadoNovedad = Convert.ToInt32(reader["idEstadoNovedad"]),
						nombre = reader["estadoNovedad"].ToString(),
						esFinal = Convert.ToBoolean(reader["esFinal"])
					};

					listaEntidad.Add(objEntidad);
				}
				return listaEntidad;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetNovedad()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetNovedad()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
		#endregion

	}
}
