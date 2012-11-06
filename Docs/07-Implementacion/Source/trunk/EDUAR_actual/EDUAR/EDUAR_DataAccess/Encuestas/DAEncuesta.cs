using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System.Data.SqlClient;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;
using System.Data;
using System.Collections.Generic;
using EDUAR_Entities.Security;

namespace EDUAR_DataAccess.Encuestas
{
	public class DAEncuesta : DataAccesBase<Encuesta>
	{
		#region --[Atributos]--
		private const string ClassName = "DAEncuesta";
		#endregion

		#region --[Constructor]--
		public DAEncuesta()
		{
		}

		public DAEncuesta(DATransaction objDATransaction)
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

		public override Encuesta GetById(Encuesta entidad)
		{
			throw new NotImplementedException();
		}

		public override void Create(Encuesta entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Encuesta_Insert");

				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, 0);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.usuario.username);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaCreacion", DbType.Date, DBNull.Value);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaModificacion", DbType.Date, DBNull.Value);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@nombre", DbType.String, entidad.nombreEncuesta);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAmbito", DbType.Int32, entidad.ambito.idAmbitoEncuesta);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCursoCicloLectivo", DbType.Int32, entidad.curso.idCursoCicloLectivo);
				if (entidad.asignatura.idAsignaturaCicloLectivo > 0)
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignaturaCicloLectivo", DbType.Int32, entidad.asignatura.idAsignaturaCicloLectivo);
				if(entidad.fechaVencimiento.HasValue)
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaLimite", DbType.Date, Convert.ToDateTime(entidad.fechaVencimiento));

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

		public override void Create(Encuesta entidad, out int identificador)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Encuesta_Insert");

				Transaction.DataBase.AddOutParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, 0);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.usuario.username);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaCreacion", DbType.Date, DBNull.Value);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaModificacion", DbType.Date, DBNull.Value);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@nombre", DbType.String, entidad.nombreEncuesta);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@objetivo", DbType.String, entidad.objetivo);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAmbito", DbType.Int32, entidad.ambito.idAmbitoEncuesta);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCursoCicloLectivo", DbType.Int32, entidad.curso.idCursoCicloLectivo);
				if (entidad.asignatura.idAsignaturaCicloLectivo > 0)
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignaturaCicloLectivo", DbType.Int32, entidad.asignatura.idAsignaturaCicloLectivo);
				if (entidad.fechaVencimiento.HasValue)
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaLimite", DbType.Date, Convert.ToDateTime(entidad.fechaVencimiento));

				if (Transaction.Transaction != null)
					Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
				else
					Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

				identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idEncuesta"].Value.ToString());
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
		public override void Update(Encuesta entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Encuesta_Update");

				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, entidad.idEncuesta);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAmbito", DbType.Int32, entidad.ambito.idAmbitoEncuesta);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.usuario.username);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaCreacion", DbType.Date, entidad.fechaCreacion);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaModificacion", DbType.Date, DateTime.Now);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@nombre", DbType.String, entidad.nombreEncuesta);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@objetivo", DbType.String, entidad.objetivo);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCursoCicloLectivo", DbType.Int32, entidad.curso.idCursoCicloLectivo);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignaturaCicloLectivo", DbType.Int32, entidad.asignatura.idAsignaturaCicloLectivo);
				if (entidad.fechaVencimiento.HasValue)
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaLimite", DbType.Date, Convert.ToDateTime(entidad.fechaVencimiento));

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

		public override void Delete(Encuesta entidad)
		{
			try
			{
				if (entidad.preguntas.Count == 1)
				{
					Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Pregunta_Delete");

					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, entidad.idEncuesta);
					Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPregunta", DbType.Int32, entidad.preguntas[0].idPregunta);

					if (Transaction.Transaction != null)
						Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
					else
						Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
				}
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
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Gets the encuestas.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <param name="idAmbito">El ámbito de la encuesta.</param>
		/// <returns></returns>
		public List<Encuesta> GetEncuestas(Encuesta entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Encuesta_Select");
				if (entidad != null)
				{
					if (entidad.idEncuesta > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, entidad.idEncuesta);
					if (entidad.usuario.idPersona > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@responsable", DbType.Int32, entidad.usuario.idPersona);
					if (entidad.ambito.idAmbitoEncuesta > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAmbito", DbType.Int32, entidad.ambito.idAmbitoEncuesta);
					if (entidad.activo)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activa", DbType.Boolean, entidad.activo);
					if (entidad.curso.idCursoCicloLectivo > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCursoCicloLectivo", DbType.Int32, entidad.curso.idCursoCicloLectivo);
					if (entidad.asignatura.idAsignaturaCicloLectivo > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignaturaCicloLectivo", DbType.Int32, entidad.asignatura.idAsignaturaCicloLectivo);
				}
				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<Encuesta> listaEncuestas = new List<Encuesta>();
				Encuesta objEncuesta;
				int idAsignaturaCicloLectivo = 0;
				string fechaModificacion = string.Empty;
				DateTime fechaLanzamiento ;
				while (reader.Read())
				{
					objEncuesta = new Encuesta();

					objEncuesta.idEncuesta = Convert.ToInt32(reader["idEncuesta"]);
					objEncuesta.nombreEncuesta = reader["nombreEncuesta"].ToString();
					objEncuesta.fechaCreacion = Convert.ToDateTime(reader["fechaCreacion"].ToString());
					objEncuesta.objetivo = reader["objetivo"].ToString();

					fechaModificacion = reader["fechaModificacion"].ToString();

					if (!fechaModificacion.Equals(string.Empty)) objEncuesta.fechaModificacion = Convert.ToDateTime(reader["fechaModificacion"].ToString());

					if (DateTime.TryParse(reader["fechaLanzamiento"].ToString(), out fechaLanzamiento))
						objEncuesta.fechaLanzamiento = fechaLanzamiento;
					else
						objEncuesta.fechaLanzamiento = null;

					if (DateTime.TryParse(reader["fechaLimite"].ToString(), out fechaLanzamiento))
						objEncuesta.fechaVencimiento = fechaLanzamiento;
					else
						objEncuesta.fechaVencimiento = null;

					objEncuesta.usuario = new Persona();
					{
						objEncuesta.usuario.idPersona = Convert.ToInt32(reader["responsable"]);
						objEncuesta.usuario.username = reader["username"].ToString();
						objEncuesta.usuario.nombre = reader["nombreOrganizador"].ToString();
						objEncuesta.usuario.apellido = reader["apellidoOrganizador"].ToString();
					}

					objEncuesta.ambito = new AmbitoEncuesta();
					{
						objEncuesta.ambito.idAmbitoEncuesta = Convert.ToInt32(reader["idAmbito"]);
						objEncuesta.ambito.nombre = reader["nombreAmbito"].ToString();
						//objEncuesta.ambito.descripcion = reader["descripcionAmbito"].ToString();
					}

					objEncuesta.activo = Convert.ToBoolean(reader["activa"].ToString());
					objEncuesta.preguntas = GetPreguntasEncuesta(entidad, null);
					objEncuesta.listaRoles = GetRolesEncuesta(objEncuesta);
					objEncuesta.curso.idCursoCicloLectivo = Convert.ToInt32(reader["idCursoCicloLectivo"]);

					int.TryParse(reader["idAsignaturaCicloLectivo"].ToString(), out idAsignaturaCicloLectivo);
					objEncuesta.asignatura.idAsignaturaCicloLectivo = idAsignaturaCicloLectivo;

					listaEncuestas.Add(objEncuesta);
				}
				return listaEncuestas;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetEncuestas()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetEncuestas()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Gets the roles ambito.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<DTRol> GetRolesAmbito(AmbitoEncuesta entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("AmbitoRol_Select");

				if (entidad != null)
				{
					if (entidad.idAmbitoEncuesta > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAmbito", DbType.Int32, entidad.idAmbitoEncuesta);
				}

				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<DTRol> listaRoles = new List<DTRol>();
				DTRol objRol;

				while (reader.Read())
				{
					objRol = new DTRol();
					objRol.Nombre = reader["rolName"].ToString();
					listaRoles.Add(objRol);
				}
				return listaRoles;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetRolesAmbito()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetRolesAmbito()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Gets the preguntas de una encuesta dada.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<Pregunta> GetPreguntasEncuesta(Encuesta entidad, Pregunta pregunta)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Pregunta_Select");

				if (entidad != null)
				{
					if (entidad.idEncuesta > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, entidad.idEncuesta);
					if (pregunta != null)
					{
						if (pregunta.categoria.idCategoriaPregunta > 0) Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCategoria", DbType.Int32, pregunta.categoria.idCategoriaPregunta);
						if (pregunta.escala.idEscala > 0) Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEscala", DbType.Int32, pregunta.escala.idEscala);
					}
				}

				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<Pregunta> listaPreguntasEncuesta = new List<Pregunta>();
				Pregunta objPregunta;

				while (reader.Read())
				{
					objPregunta = new Pregunta();

					objPregunta.idPregunta = Convert.ToInt32(reader["idPregunta"]);
					objPregunta.textoPregunta = reader["textoPregunta"].ToString();
					objPregunta.objetivoPregunta = reader["objetivo"].ToString();
					objPregunta.ponderacion = Convert.ToDouble(reader["peso"]);

					objPregunta.categoria = new CategoriaPregunta();
					{
						objPregunta.categoria.idCategoriaPregunta = Convert.ToInt32(reader["idCategoria"]);
						objPregunta.categoria.nombre = reader["categoria"].ToString();
						//objPregunta.categoria.descripcion = reader["descripcionCategoria"].ToString();
					}

					objPregunta.escala = new EscalaMedicion();
					{
						objPregunta.escala.idEscala = Convert.ToInt32(reader["idEscalaPonderacion"]);
						objPregunta.escala.nombre = reader["escala"].ToString();
						objPregunta.escala.descripcion = reader["descripcionEscala"].ToString();
					}

					listaPreguntasEncuesta.Add(objPregunta);
				}
				return listaPreguntasEncuesta;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetPreguntasEncuesta()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetPreguntasEncuesta()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Gets the categorias por encuesta.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<CategoriaPregunta> GetCategoriasPorEncuesta(Encuesta entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("CategoriasEncuesta_Select");

				if (entidad != null)
				{
					if (entidad.idEncuesta > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, entidad.idEncuesta);
				}

				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<CategoriaPregunta> listaCategoriasPreguntas = new List<CategoriaPregunta>();
				CategoriaPregunta objCategoriaPregunta;

				while (reader.Read())
				{
					objCategoriaPregunta = new CategoriaPregunta();

					objCategoriaPregunta.idCategoriaPregunta = Convert.ToInt32(reader["idCategoria"]);
					objCategoriaPregunta.nombre = reader["nombre"].ToString();
					objCategoriaPregunta.descripcion = reader["descripcion"].ToString();

					objCategoriaPregunta.ambito = new AmbitoEncuesta();
					{
						objCategoriaPregunta.ambito.idAmbitoEncuesta = Convert.ToInt32(reader["idAmbito"]);
						objCategoriaPregunta.ambito.nombre = reader["nombreAmbito"].ToString();
						objCategoriaPregunta.ambito.descripcion = reader["descripcionAmbito"].ToString();
					}

					listaCategoriasPreguntas.Add(objCategoriaPregunta);
				}
				return listaCategoriasPreguntas;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetCategoriasPorEncuesta()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetCategoriasPorEncuesta()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Gets the roles encuesta.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		/// <returns></returns>
		public List<DTRol> GetRolesEncuesta(Encuesta entidad)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("RelEncuestaRol_Select");

				if (entidad != null)
				{
					if (entidad.idEncuesta > 0)
						Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, entidad.idEncuesta);
				}

				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				List<DTRol> listaRoles = new List<DTRol>();
				DTRol objRol;

				while (reader.Read())
				{
					objRol = new DTRol();
					objRol.Nombre = reader["rolName"].ToString();
					listaRoles.Add(objRol);
				}
				return listaRoles;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetRolesEncuesta()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetRolesEncuesta()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Saves the rol.
		/// </summary>
		/// <param name="roleName">Name of the role.</param>
		/// <param name="idEncuesta">The id encuesta.</param>
		public void SaveRol(string roleName, int idEncuesta)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("RelEncuestaRol_Insert");

				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, idEncuesta);
				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@rol", DbType.String, roleName);

				if (Transaction.Transaction != null)
					Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
				else
					Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - SaveRol()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - SaveRol()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Lanzars the encuesta.
		/// </summary>
		/// <param name="encuesta">The encuesta.</param>
		public void LanzarEncuesta(Encuesta encuesta)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Encuesta_Lanzar");

				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, encuesta.idEncuesta);

				if (Transaction.Transaction != null)
					Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
				else
					Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - LanzarEncuesta()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - LanzarEncuesta()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Validars the preguntas.
		/// </summary>
		/// <param name="Data">The data.</param>
		public bool ValidarPreguntas(Encuesta encuesta)
		{
			try
			{
				Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Encuesta_ValidarPreguntas");

				Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idEncuesta", DbType.Int32, encuesta.idEncuesta);

				IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

				while (reader.Read())
					return true;
				return false;
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - ValidarPreguntas()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - ValidarPreguntas()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}
		#endregion

	}
}