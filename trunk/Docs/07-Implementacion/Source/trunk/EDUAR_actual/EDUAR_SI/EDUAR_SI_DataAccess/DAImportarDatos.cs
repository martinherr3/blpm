using System;
using System.Data.SqlClient;
using EDUAR_Entities;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;
using System.Data;
using System.Collections.Generic;

namespace EDUAR_SI_DataAccess
{
	public class DAImportarDatos : DABase
	{
		#region --[Atributos]--
		private const string ClassName = "DAImportarDatos";
		#endregion

		#region --[Propiedades]--
		#endregion

		#region --[Constructores]--
		/// <summary>
		/// Constructor. LLama al constructor de la clase base DABase.
		/// </summary>
		/// <param name="connectionString">Cadena de conexión a la base de datos</param>
		public DAImportarDatos(String connectionString)
			: base(connectionString)
		{

		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Guarda una colección de personas en base de datos
		/// </summary>
		/// <param name="objeto">Colección de Persona</param>
		public int GrabarPersona(Persona persona, SqlTransaction transaccion, int? idCargoPersonal, int idPersonaTransaccional)
		{
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "Personas_Insert";
					command.CommandTimeout = 10;

					command.Connection = transaccion.Connection;
					command.Transaction = transaccion;

					command.Parameters.AddWithValue("idPersona", 0).Direction = ParameterDirection.Output;
					command.Parameters.AddWithValue("nombre", persona.nombre);
					command.Parameters.AddWithValue("apellido", persona.apellido);
					command.Parameters.AddWithValue("numeroDocumento", persona.numeroDocumento);
					command.Parameters.AddWithValue("idTipoDocumento", persona.idTipoDocumento);
					command.Parameters.AddWithValue("domicilio", persona.domicilio);
					command.Parameters.AddWithValue("barrio", persona.barrio);
					command.Parameters.AddWithValue("localidad", persona.localidad.nombre);
					command.Parameters.AddWithValue("sexo", persona.sexo);
					if (ValidarFechaSQL(persona.fechaNacimiento))
						command.Parameters.AddWithValue("fechaNacimiento", persona.fechaNacimiento);
					command.Parameters.AddWithValue("telefonoFijo", persona.telefonoFijo);
					command.Parameters.AddWithValue("telefonoCelular", persona.telefonoCelular);
					command.Parameters.AddWithValue("telefonoCelularAlternativo", persona.telefonoCelularAlternativo);
					command.Parameters.AddWithValue("email", persona.email);
					command.Parameters.AddWithValue("activo", persona.activo);
					command.Parameters.AddWithValue("idTipoPersona", persona.idTipoPersona);
					command.Parameters.AddWithValue("idCargoPersonal", idCargoPersonal);
					command.Parameters.AddWithValue("idPersonaTransaccional", idPersonaTransaccional);

					command.ExecuteNonQuery();
					return Convert.ToInt32(command.Parameters["idPersona"].Value);
				}
			}
			catch (SqlException ex)
			{
				//if (transaccion != null) transaccion.Rollback();
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarPersona()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				//if (transaccion != null) transaccion.Rollback();
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarPersona()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Grabars the alumno.
		/// </summary>
		/// <param name="alumno">The alumno.</param>
		/// <param name="transaccion">The transaccion.</param>
		public void GrabarAlumno(Alumno alumno, SqlTransaction transaccion)
		{
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					command.Connection = transaccion.Connection;
					command.Transaction = transaccion;

					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "Alumnos_Insert";
					command.CommandTimeout = 10;

					command.Parameters.AddWithValue("idAlumno", alumno.idAlumno).Direction = ParameterDirection.Output;
					command.Parameters.AddWithValue("idAlumnoTransaccional", alumno.idAlumnoTransaccional);
					command.Parameters.AddWithValue("idPersona", alumno.idPersona);
					command.Parameters.AddWithValue("legajo", alumno.legajo);
					command.Parameters.AddWithValue("fechaAlta", alumno.fechaAlta);
					command.Parameters.AddWithValue("fechaBaja", alumno.fechaBaja);
					command.ExecuteNonQuery();
					command.Parameters.Clear();
				}
			}
			catch (SqlException ex)
			{
				//if (transaccion != null) transaccion.Rollback();
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarAlumno()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				//if (transaccion != null) transaccion.Rollback();
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarAlumno()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Grabars the pais.
		/// </summary>
		/// <param name="colPaises">The col paises.</param>
		public void GrabarPais(List<Paises> listaPaises, SqlTransaction transaccion)
		{
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "Paises_Insert";
					command.CommandTimeout = 10;

					command.Connection = transaccion.Connection;
					command.Transaction = transaccion;

					foreach (Paises pais in listaPaises)
					{
						command.Parameters.AddWithValue("idPais", 0).Direction = ParameterDirection.Output;
						command.Parameters.AddWithValue("idPaisTransaccional", pais.idPaisTransaccional);
						command.Parameters.AddWithValue("nombre", pais.nombre);
						command.Parameters.AddWithValue("descripcion", pais.descripcion);
						command.Parameters.AddWithValue("activo", pais.activo);
						command.ExecuteNonQuery();
						command.Parameters.Clear();
					}
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarPais()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarPais()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Grabars the provincia.
		/// </summary>
		/// <param name="colProvincias">The col provincias.</param>
		public void GrabarProvincia(List<Provincias> listaProvincias, SqlTransaction transaccion)
		{
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "Provincias_Insert";
					command.CommandTimeout = 10;

					command.Connection = transaccion.Connection;
					command.Transaction = transaccion;

					foreach (Provincias provincia in listaProvincias)
					{
						command.Parameters.AddWithValue("idProvincia", 0).Direction = ParameterDirection.Output;
						command.Parameters.AddWithValue("idProvinciaTransaccional", provincia.idProvinciaTransaccional);
						command.Parameters.AddWithValue("nombre", provincia.nombre);
						command.Parameters.AddWithValue("descripcion", provincia.descripcion);
						command.Parameters.AddWithValue("idPais", provincia.idPais);
						command.Parameters.AddWithValue("activo", provincia.activo);
						command.ExecuteNonQuery();
						command.Parameters.Clear();
					}
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarProvincia()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarProvincia()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Grabars the localidad.
		/// </summary>
		/// <param name="colLocalidades">The col localidades.</param>
		public void GrabarLocalidad(List<Localidades> ListaLocalidades, SqlTransaction transaccion)
		{
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "Localidades_Insert";
					command.CommandTimeout = 10;

					command.Connection = transaccion.Connection;
					command.Transaction = transaccion;

					foreach (Localidades localidad in ListaLocalidades)
					{
						command.Parameters.AddWithValue("idLocalidad", 0);
						command.Parameters.AddWithValue("idLocalidadTransaccional", 0);
						command.Parameters.AddWithValue("nombre", localidad.nombre);
						command.Parameters.AddWithValue("descripcion", "");
						command.Parameters.AddWithValue("idProvincia", localidad.idProvincia);
						command.Parameters.AddWithValue("activo", localidad.activo);
						command.ExecuteNonQuery();
						command.Parameters.Clear();
					}
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarLocalidad()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarLocalidad()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Grabars the tipos documentos.
		/// </summary>
		/// <param name="colTipoDocumento">The col tipos documentos.</param>
		public void GrabarTipoDocumento(List<TipoDocumento> listaTipoDocumento, SqlTransaction transaccion)
		{
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "TipoDocumento_Insert";
					command.CommandTimeout = 10;

					command.Connection = transaccion.Connection;
					command.Transaction = transaccion;

					foreach (TipoDocumento tipoDocumento in listaTipoDocumento)
					{
						command.Parameters.AddWithValue("idTipoDocumento", 0);
						command.Parameters.AddWithValue("idTipoDocumentoTransaccional", tipoDocumento.idTipoDocumentoTransaccional);
						command.Parameters.AddWithValue("nombre", tipoDocumento.nombre);
						command.Parameters.AddWithValue("descripcion", tipoDocumento.descripcion);
						command.Parameters.AddWithValue("activo", tipoDocumento.activo);
						command.ExecuteNonQuery();
						command.Parameters.Clear();
					}
				}
			}
			catch (SqlException ex)
			{
				if (transaccion != null) transaccion.Rollback();
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarTipoDocumento()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				if (transaccion != null) transaccion.Rollback();
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarTipoDocumento()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Grabars the valores escalas calificaciones.
		/// </summary>
		/// <param name="listaValoresCalificacion">The lista valores calificacion.</param>
		/// <param name="transaccion">The transaccion.</param>
		public void GrabarValoresEscalasCalificaciones(List<ValoresEscalaCalificacion> listaValoresCalificacion, SqlTransaction transaccion)
		{
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "ValoresEscalaCalificacion_Insert";
					command.CommandTimeout = 10;

					command.Connection = transaccion.Connection;
					command.Transaction = transaccion;

					foreach (ValoresEscalaCalificacion valorEscala in listaValoresCalificacion)
					{
						command.Parameters.AddWithValue("idValorEscalaCalificacion", 0);
						command.Parameters.AddWithValue("idValorEscalaCalificacionTransaccional", valorEscala.idValorEscalaCalificacionTransaccional);
						command.Parameters.AddWithValue("nombre", valorEscala.nombre);
						command.Parameters.AddWithValue("descripcion", valorEscala.descripcion);
						command.Parameters.AddWithValue("activo", valorEscala.activo);
						command.Parameters.AddWithValue("valor", valorEscala.valor);
						command.Parameters.AddWithValue("aprobado", valorEscala.aprobado);
						//command.Parameters.AddWithValue("idEscalaCalificacion", valorEscala.idEscalaCalificacion);
						command.ExecuteNonQuery();
						command.Parameters.Clear();
					}
				}
			}
			catch (SqlException ex)
			{
				if (transaccion != null) transaccion.Rollback();
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarValoresEscalasCalificaciones()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				if (transaccion != null) transaccion.Rollback();
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarValoresEscalasCalificaciones()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Grabars the personal.
		/// </summary>
		/// <param name="personal">The personal.</param>
		/// <param name="transaccion">The transaccion.</param>
		public void GrabarPersonal(Personal personal, SqlTransaction transaccion)
		{
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "Personal_Insert";
					command.CommandTimeout = 10;

					command.Connection = transaccion.Connection;
					command.Transaction = transaccion;

					command.Parameters.AddWithValue("idPersonal", personal.idPersonal).Direction = ParameterDirection.Output;
					command.Parameters.AddWithValue("idPersonalTransaccional", personal.idPersonalTransaccional);
					command.Parameters.AddWithValue("idPersona", personal.idPersona);
					command.Parameters.AddWithValue("legajo", personal.legajo);
					command.Parameters.AddWithValue("fechaAlta", DBNull.Value);
					command.Parameters.AddWithValue("fechaBaja", DBNull.Value);
					command.Parameters.AddWithValue("activo", personal.activo);
					command.Parameters.AddWithValue("idCargoPersonal", personal.cargo.idCargoTransaccional);
					command.ExecuteNonQuery();
					command.Parameters.Clear();
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarPersonal()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarPersonal()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Grabars the cargo personal.
		/// </summary>
		/// <param name="listaCargosPersonal">The lista cargos personal.</param>
		/// <param name="transaccion">The transaccion.</param>
		public void GrabarCargoPersonal(List<CargoPersonal> listaCargosPersonal, SqlTransaction transaccion)
		{
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "CargosPersonal_Insert";
					command.CommandTimeout = 10;

					command.Connection = transaccion.Connection;
					command.Transaction = transaccion;

					foreach (CargoPersonal cargoPersonal in listaCargosPersonal)
					{
						command.Parameters.AddWithValue("idCargoPersonal", 0);
						command.Parameters.AddWithValue("idCargoPersonalTransaccional", cargoPersonal.idCargoTransaccional);
						command.Parameters.AddWithValue("nombre", cargoPersonal.nombre);
						command.Parameters.AddWithValue("descripcion", cargoPersonal.descripcion);
						command.Parameters.AddWithValue("activo", cargoPersonal.activo);
						command.ExecuteNonQuery();
						command.Parameters.Clear();
					}
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarCargoPersonal()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarCargoPersonal()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
			finally
			{
				//if (sqlConnectionConfig.State == ConnectionState.Open)
				//    sqlConnectionConfig.Close();
			}
		}

		/// <summary>
		/// Grabars the asignatura.
		/// </summary>
		/// <param name="listaAsignatura">The lista asignatura.</param>
		public void GrabarAsignatura(List<Asignatura> listaAsignatura, SqlTransaction transaccion)
		{
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "Asignatura_Insert";
					command.CommandTimeout = 10;

					command.Connection = transaccion.Connection;
					command.Transaction = transaccion;

					foreach (Asignatura asignatura in listaAsignatura)
					{
						command.Parameters.AddWithValue("idAsignatura", 0);
						command.Parameters.AddWithValue("idAsignaturaTransaccional", asignatura.idAsignaturaTransaccional);
						command.Parameters.AddWithValue("nombre", asignatura.nombre);
						command.ExecuteNonQuery();
						command.Parameters.Clear();
					}
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarAsignatura()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarAsignatura()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Grabars the ciclo lectivo.
		/// </summary>
		/// <param name="listaCicloLectivo">The lista ciclo lectivo.</param>
		public void GrabarCicloLectivo(List<CicloLectivo> listaCicloLectivo, SqlTransaction transaccion)
		{
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "CicloLectivo_Insert";
					command.CommandTimeout = 10;

					command.Connection = transaccion.Connection;
					command.Transaction = transaccion;

					foreach (CicloLectivo cicloLectivo in listaCicloLectivo)
					{
						command.Parameters.AddWithValue("idCicloLectivo", 0);
						command.Parameters.AddWithValue("idCicloLectivoTransaccional", cicloLectivo.idCicloLectivoTransaccional);
						command.Parameters.AddWithValue("nombre", cicloLectivo.nombre);
						command.Parameters.AddWithValue("fechaInicio", cicloLectivo.fechaInicio);
						command.Parameters.AddWithValue("fechaFin", cicloLectivo.fechaFin);
						command.Parameters.AddWithValue("activo", cicloLectivo.activo);
						command.ExecuteNonQuery();
						command.Parameters.Clear();
					}
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarAsignatura()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarAsignatura()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Grabars the nivel.
		/// </summary>
		/// <param name="listaNiveles">The lista niveles.</param>
		public void GrabarNivel(List<Nivel> listaNiveles, SqlTransaction transaccion)
		{
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "Nivel_Insert";
					command.CommandTimeout = 10;

					command.Connection = transaccion.Connection;
					command.Transaction = transaccion;

					foreach (Nivel nivel in listaNiveles)
					{
						command.Parameters.AddWithValue("idNivel", 0);
						command.Parameters.AddWithValue("idNivelTransaccional", nivel.idNivelTransaccional);
						command.Parameters.AddWithValue("nombre", nivel.nombre);
						command.ExecuteNonQuery();
						command.Parameters.Clear();
					}
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarNivel()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarNivel()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Grabars the cursos.
		/// </summary>
		/// <param name="listaCursos">The lista cursos.</param>
		public void GrabarCursos(List<Curso> listaCursos, SqlTransaction transaccion)
		{
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "Curso_Insert";
					command.CommandTimeout = 10;

					command.Connection = transaccion.Connection;
					command.Transaction = transaccion;

					foreach (Curso curso in listaCursos)
					{
						command.Parameters.AddWithValue("idCurso", 0);
						command.Parameters.AddWithValue("idCursoTransaccional", curso.idCursoTransaccional);
						command.Parameters.AddWithValue("nombre", curso.nombre);
						command.Parameters.AddWithValue("idNivel", curso.nivel.idNivelTransaccional);
						command.ExecuteNonQuery();
						command.Parameters.Clear();
					}
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarNivel()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarNivel()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Grabars the orientacion.
		/// </summary>
		/// <param name="listaOrientacion">The lista orientacion.</param>
		public void GrabarOrientacion(List<Orientacion> listaOrientacion, SqlTransaction transaccion)
		{
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "Orientacion_Insert";
					command.CommandTimeout = 10;

					command.Connection = transaccion.Connection;
					command.Transaction = transaccion;

					foreach (Orientacion orientacion in listaOrientacion)
					{
						command.Parameters.AddWithValue("idOrientacion", 0);
						command.Parameters.AddWithValue("idOrientacionTransaccional", orientacion.idOrientacionTransaccional);
						command.Parameters.AddWithValue("nombre", orientacion.nombre);
						command.Parameters.AddWithValue("descripcion", orientacion.descripcion);
						command.ExecuteNonQuery();
						command.Parameters.Clear();
					}
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarOrientacion()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarOrientacion()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Grabars the asignatura curso.
		/// </summary>
		/// <param name="listaAsignatura">The lista asignatura.</param>
		public void GrabarAsignaturaCurso(List<AsignaturaCicloLectivo> listaAsignatura, SqlTransaction transaccion)
		{
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "AsignaturaCicloLectivo_Insert";
					command.CommandTimeout = 10;

					command.Connection = transaccion.Connection;
					command.Transaction = transaccion;

					foreach (AsignaturaCicloLectivo item in listaAsignatura)
					{
						//command.Parameters.AddWithValue("idAsignaturaCicloLectivo", 0);
						command.Parameters.AddWithValue("idAsignaturaCicloLectivoTransaccional", item.idAsignaturaCicloLectivoTransaccional);
						command.Parameters.AddWithValue("idAsignatura", item.asignatura.idAsignaturaTransaccional);
						command.Parameters.AddWithValue("idDocente", item.docente.idPersonalTransaccional);
						command.Parameters.AddWithValue("idCursoCicloLectivo", item.cursoCicloLectivo.idCursoCicloLectivoTransaccional);
						command.ExecuteNonQuery();
						command.Parameters.Clear();
					}
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarAsignaturaCurso()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarAsignaturaCurso()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Grabars the periodo.
		/// </summary>
		/// <param name="listaPeriodo">The lista periodo.</param>
		public void GrabarPeriodo(List<Periodo> listaPeriodo, SqlTransaction transaccion)
		{
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "Periodo_Insert";
					command.CommandTimeout = 10;

					command.Connection = transaccion.Connection;
					command.Transaction = transaccion;

					foreach (Periodo periodo in listaPeriodo)
					{
						command.Parameters.AddWithValue("idPeriodo", 0);
						command.Parameters.AddWithValue("idPeriodoTransaccional", periodo.idPeriodoTransaccional);
						command.Parameters.AddWithValue("nombre", periodo.nombre);
						command.Parameters.AddWithValue("idCicloLectivo", periodo.cicloLectivo.idCicloLectivoTransaccional);
						command.Parameters.AddWithValue("fechaInicio", periodo.fechaInicio);
						command.Parameters.AddWithValue("fechaFin", periodo.fechaFin);
						command.ExecuteNonQuery();
						command.Parameters.Clear();
					}
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarPeriodo()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarPeriodo()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Grabars the calificacion.
		/// </summary>
		/// <param name="listaCalificacion">The lista calificacion.</param>
		public void GrabarCalificacion(List<Calificacion> listaCalificacion, SqlTransaction transaccion)
		{
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "Calificacion_Insert";
					command.CommandTimeout = 300;

					command.Connection = transaccion.Connection;
					command.Transaction = transaccion;

					foreach (Calificacion calificacion in listaCalificacion)
					{
						command.Parameters.AddWithValue("idCalificacion", 0);
						command.Parameters.AddWithValue("idCalificacionTransaccional", calificacion.idCalificacionTransaccional);
						command.Parameters.AddWithValue("observaciones", calificacion.observacion);
						command.Parameters.AddWithValue("fecha", calificacion.fecha);
						command.Parameters.AddWithValue("idValorCalificacion", calificacion.escala.idValorEscalaCalificacionTransaccional);
						command.Parameters.AddWithValue("idAlumnoCursoCicloLectivo", calificacion.alumnoCurso.idAlumnoCursoCicloLectivoTransaccional);
						command.Parameters.AddWithValue("idAsignatura", calificacion.asignatura.idAsignaturaTransaccional);
						command.Parameters.AddWithValue("idPeriodo", calificacion.periodo.idPeriodoTransaccional);
						command.Parameters.AddWithValue("idInstanciaCalificacion", calificacion.instanciaCalificacion.idInstanciaCalificacion);
						command.ExecuteNonQuery();
						command.Parameters.Clear();
					}
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarCalificacion()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarCalificacion()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Grabars the Tutor.
		/// </summary>
		/// <param name="tutor">The tutor.</param>
		/// <param name="transaccion">The transaccion.</param>
		public void GrabarTutor(Tutor tutor, SqlTransaction transaccion)
		{
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "Tutor_Insert";
					command.CommandTimeout = 10;

					command.Connection = transaccion.Connection;
					command.Transaction = transaccion;

					command.Parameters.AddWithValue("idTutor", tutor.idTutor).Direction = ParameterDirection.Output;
					command.Parameters.AddWithValue("idTutorTransaccional", tutor.idTutorTransaccional);
					command.Parameters.AddWithValue("idTipoTutor", tutor.tipoTutor.idTipoTutorTransaccional);
					command.Parameters.AddWithValue("telefonoTrabajo", tutor.telefonoCelularAlternativo);
					command.Parameters.AddWithValue("idPersona", tutor.idPersona);
					command.ExecuteNonQuery();
					command.Parameters.Clear();
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarTutor()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarTutor()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		//TODO: Primero hacer GrabarTipoAsistencia()
		/// <summary>
		/// Grabars the tipo asistencia.
		/// </summary>
		/// <param name="listadoTipoAsistencia">The listado tipo asistencia.</param>
		public void GrabarTipoAsistencia(List<TipoAsistencia> listadoTipoAsistencia, SqlTransaction transaccion)
		{
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "TipoAsistencia_Insert";
					command.CommandTimeout = 10;

					command.Connection = transaccion.Connection;
					command.Transaction = transaccion;

					foreach (TipoAsistencia unTipoAsistencia in listadoTipoAsistencia)
					{
						command.Parameters.AddWithValue("idTipoAsistencia", 0);
						command.Parameters.AddWithValue("idTipoAsistenciaTransaccional", unTipoAsistencia.idTipoAsistenciaTransaccional);
						command.Parameters.AddWithValue("valor", unTipoAsistencia.valor);
						command.Parameters.AddWithValue("descripcion", unTipoAsistencia.descripcion);
						command.ExecuteNonQuery();
						command.Parameters.Clear();
					}
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarTipoAsistenica()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarTipoAsistencia()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Grabars the asistencia.
		/// </summary>
		/// <param name="listadoAsistencia">The listado asistencia.</param>
		public void GrabarAsistencia(List<Asistencia> listadoAsistencia, SqlTransaction transaccion)
		{
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "Asistencia_Insert";
					command.CommandTimeout = 10;

					command.Connection = transaccion.Connection;
					command.Transaction = transaccion;

					foreach (Asistencia unaAsistencia in listadoAsistencia)
					{
						command.Parameters.AddWithValue("idAsistencia", 0);
						command.Parameters.AddWithValue("idAsistenciaTransaccional", unaAsistencia.idAsistenciaTransaccional);
						command.Parameters.AddWithValue("fecha", unaAsistencia.fecha);
						command.Parameters.AddWithValue("idTipoAsistencia", unaAsistencia.tipoAsistencia.idTipoAsistenciaTransaccional);
						command.Parameters.AddWithValue("idAlumno", unaAsistencia.alumno.idAlumnoCursoCicloLectivoTransaccional);
						command.ExecuteNonQuery();
						command.Parameters.Clear();
					}
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarAsistenica()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarAsistencia()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Grabars the tipo sancion.
		/// </summary>
		/// <param name="listadoTipoSancion">The listado tipo sancion.</param>
		public void GrabarTipoSancion(List<TipoSancion> listadoTipoSancion, SqlTransaction transaccion)
		{
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "TipoSancion_Insert";
					command.CommandTimeout = 10;

					command.Connection = transaccion.Connection;
					command.Transaction = transaccion;

					foreach (TipoSancion unTipoSancion in listadoTipoSancion)
					{
						command.Parameters.AddWithValue("idTipoSancion", 0);
						command.Parameters.AddWithValue("idTipoSancionTransaccional", unTipoSancion.idTipoSancionTransaccional);
						command.Parameters.AddWithValue("nombre", unTipoSancion.nombre);
						command.Parameters.AddWithValue("descripcion", unTipoSancion.descripcion);
						command.ExecuteNonQuery();
						command.Parameters.Clear();
					}
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarTipoSancion()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarTipoSancion()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Grabars the motivo sancion.
		/// </summary>
		/// <param name="listadoMotivoSancion">The listado motivo sancion.</param>
		public void GrabarMotivoSancion(List<MotivoSancion> listadoMotivoSancion, SqlTransaction transaccion)
		{
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "MotivoSancion_Insert";
					command.CommandTimeout = 10;

					command.Connection = transaccion.Connection;
					command.Transaction = transaccion;

					foreach (MotivoSancion unMotivoSancion in listadoMotivoSancion)
					{
						command.Parameters.AddWithValue("idMotivoSancion", 0);
						command.Parameters.AddWithValue("idMotivoSancionTransaccional", unMotivoSancion.idMotivoSancionTransaccional);
						command.Parameters.AddWithValue("descripcion", unMotivoSancion.descripcion);
						command.ExecuteNonQuery();
						command.Parameters.Clear();
					}
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarMotivoSancion()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarMotivoSancion()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Grabars the sancion.
		/// </summary>
		/// <param name="listadoSancion">The listado sancion.</param>
		public void GrabarSancion(List<Sancion> listadoSancion, SqlTransaction transaccion)
		{
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "Sancion_Insert";
					command.CommandTimeout = 10;

					command.Connection = transaccion.Connection;
					command.Transaction = transaccion;

					foreach (Sancion unaSancion in listadoSancion)
					{
						command.Parameters.AddWithValue("idSancion", 0);
						command.Parameters.AddWithValue("idSancionTransaccional", unaSancion.idSancionTransaccional);
						command.Parameters.AddWithValue("cantidad", unaSancion.cantidad);
						command.Parameters.AddWithValue("fecha", unaSancion.fecha.Date);
						command.Parameters.AddWithValue("idMotivoSancion", unaSancion.motivoSancion.idMotivoSancionTransaccional);
						command.Parameters.AddWithValue("idTipoSancion", unaSancion.tipoSancion.idTipoSancionTransaccional);
						command.Parameters.AddWithValue("idAlumno", unaSancion.alumno.idAlumnoCursoCicloLectivoTransaccional);
						command.ExecuteNonQuery();
						command.Parameters.Clear();
					}
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarSancion()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarSancion()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Grabars the tipo tutor.
		/// </summary>
		/// <param name="listadoTipoTutor">The listado tipo tutor.</param>
		public void GrabarTipoTutor(List<TipoTutor> listadoTipoTutor, SqlTransaction transaccion)
		{
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "TipoTutor_Insert";
					command.CommandTimeout = 10;

					command.Connection = transaccion.Connection;
					command.Transaction = transaccion;

					foreach (TipoTutor unTipoTutor in listadoTipoTutor)
					{
						command.Parameters.AddWithValue("idTipoTutor", 0);
						command.Parameters.AddWithValue("descripcion", unTipoTutor.descripcion);
						command.Parameters.AddWithValue("idTipoTutorTransaccional", unTipoTutor.idTipoTutorTransaccional);
						command.ExecuteNonQuery();
						command.Parameters.Clear();
					}
				}
			}
			catch (SqlException ex)
			{
				if (transaccion != null) transaccion.Rollback();
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarTipoTutor()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				if (transaccion != null) transaccion.Rollback();
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarTipoTutor()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Grabars the tutores alumno.
		/// </summary>
		/// <param name="listaAlumnos">The lista alumnos.</param>
		public void GrabarTutoresAlumno(List<Alumno> listaAlumnos, SqlTransaction transaccion)
		{
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "TutorAlumno_Insert";
					command.CommandTimeout = 10;

					command.Connection = transaccion.Connection;
					command.Transaction = transaccion;

					foreach (Alumno alumno in listaAlumnos)
					{
						foreach (Tutor tutor in alumno.listaTutores)
						{
							command.Parameters.AddWithValue("idTutor", tutor.idTutorTransaccional);
							command.Parameters.AddWithValue("idAlumno", alumno.idAlumnoTransaccional);

							command.ExecuteNonQuery();
							command.Parameters.Clear();
						}
					}
				}
			}
			catch (SqlException ex)
			{
				if (transaccion != null) transaccion.Rollback();
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarTutoresAlumno()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				if (transaccion != null) transaccion.Rollback();
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarTutoresAlumno()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Grabars the tipo asistencia.
		/// </summary>
		/// <param name="listadoTipoAsistencia">The listado tipo asistencia.</param>
        public void GrabarDiasHorarios(List<DiasHorarios> listadoDiasHorario, SqlTransaction transaccion)
		{
			try
			{
				//foreach (DiasHorarios unDiasHorarios in listadoDiasHorarios)
				//    {
				//        GrabarModulos(unDiasHorarios.modulos);
				//    }
				using (SqlCommand command = new SqlCommand())
				{
					if (sqlConnectionConfig.State == ConnectionState.Closed) sqlConnectionConfig.Open();

					command.Connection = sqlConnectionConfig;
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "DiaHorario_Insert";
					command.CommandTimeout = 10;

					transaccion = sqlConnectionConfig.BeginTransaction();
					command.Transaction = transaccion;

                    foreach (DiasHorarios unDiasHorarios in listadoDiasHorario)
					{
						command.Parameters.AddWithValue("idDiaHorario", 0).Direction = ParameterDirection.Output;
						command.Parameters.AddWithValue("idDiaHorarioTransaccional", unDiasHorarios.idDiaHorarioTransaccional);
						command.Parameters.AddWithValue("idCurso", unDiasHorarios.idCursoTransaccional);
						command.Parameters.AddWithValue("idAsignatura", unDiasHorarios.idAsignaturaTransaccional);
						command.Parameters.AddWithValue("idDiaSemana", (int)unDiasHorarios.unDia);
						command.Parameters.AddWithValue("idNivel", (int)unDiasHorarios.idNivelTransaccional);
						//command.Parameters.AddWithValue("idModulo", 1);
						//GrabarModulos(unDiasHorarios.modulos);
						command.ExecuteNonQuery();
						unDiasHorarios.idDiaHorario = Convert.ToInt32(command.Parameters["idDiaHorario"].Value);
						command.Parameters.Clear();
					}
					transaccion.Commit();
				}
			}
			catch (SqlException ex)
			{
				if (transaccion != null) transaccion.Rollback();
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarDiasHorarios()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				if (transaccion != null) transaccion.Rollback();
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarDiasHorarios()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
			finally
			{
				if (sqlConnectionConfig.State == ConnectionState.Open)
					sqlConnectionConfig.Close();

				GrabarModulos(listadoDiasHorario);

			}

		}


		/// <summary>
		/// Grabars the tipo asistencia.
		/// </summary>
		/// <param name="listadoTipoAsistencia">The listado tipo asistencia.</param>
		public void GrabarModulos(List<DiasHorarios> listadoDiasHorarios)
		{
			SqlTransaction transaccion = null;
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					if (sqlConnectionConfig.State == ConnectionState.Closed) sqlConnectionConfig.Open();

					command.Connection = sqlConnectionConfig;
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "Modulo_Insert";
					command.CommandTimeout = 10;

					transaccion = sqlConnectionConfig.BeginTransaction();
					command.Transaction = transaccion;

					foreach (DiasHorarios unDiasHorarios in listadoDiasHorarios)
					{
						foreach (Modulo unModulo in unDiasHorarios.modulos)
						{
							command.Parameters.AddWithValue("idModulo", 0);
							command.Parameters.AddWithValue("horaInicio", unModulo.horaInicio);
							command.Parameters.AddWithValue("horaFinalizacion", unModulo.horaFinalizacion);
							command.Parameters.AddWithValue("idDiaHorario", unDiasHorarios.idDiaHorario);
							command.ExecuteNonQuery();
							command.Parameters.Clear();
						}
					}
					transaccion.Commit();
				}
			}
			catch (SqlException ex)
			{
				if (transaccion != null) transaccion.Rollback();
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarModulo()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				if (transaccion != null) transaccion.Rollback();
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarModulo()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
			finally
			{
				//if (sqlConnectionConfig.State == ConnectionState.Open)
				//    sqlConnectionConfig.Close();
			}

		}

		/// <summary>
		/// Grabars the alumno curso.
		/// </summary>
		/// <param name="listaAlumnoCurso">The lista alumno curso.</param>
		public void GrabarAlumnoCurso(List<AlumnoCursoCicloLectivo> listaAlumnoCurso, SqlTransaction transaccion)
		{
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "AlumnoCursoCicloLectivo_Insert";
					command.CommandTimeout = 10;

					command.Connection = transaccion.Connection;
					command.Transaction = transaccion;

					foreach (AlumnoCursoCicloLectivo alumnoCurso in listaAlumnoCurso)
					{
						command.Parameters.AddWithValue("idAlumnoCursoCicloLectivo", 0);
						command.Parameters.AddWithValue("idAlumnoCursoCicloLectivoTransaccional", alumnoCurso.idAlumnoCursoCicloLectivoTransaccional);
						command.Parameters.AddWithValue("idAlumno", alumnoCurso.alumno.idAlumnoTransaccional);
						command.Parameters.AddWithValue("idCursoCicloLectivo", alumnoCurso.cursoCicloLectivo.idCursoCicloLectivoTransaccional);
						command.ExecuteNonQuery();
						command.Parameters.Clear();
					}
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarAlumnoCurso()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarAlumnoCurso()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

		/// <summary>
		/// Grabars the curso ciclo lectivo.
		/// </summary>
		/// <param name="listaCursoCicloLectivo">The lista curso ciclo lectivo.</param>
		/// <param name="transaccion">The transaccion.</param>
		public void GrabarCursoCicloLectivo(List<CursoCicloLectivo> listaCursoCicloLectivo, SqlTransaction transaccion)
		{
			try
			{
				using (SqlCommand command = new SqlCommand())
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.CommandText = "CursoCicloLectivo_Insert";
					command.CommandTimeout = 10;

					command.Connection = transaccion.Connection;
					command.Transaction = transaccion;

					foreach (CursoCicloLectivo cursosCicloLectivo in listaCursoCicloLectivo)
					{
						command.Parameters.AddWithValue("idCursoCicloLectivo", 0);
						command.Parameters.AddWithValue("idCursoCicloLectivoTransaccional", cursosCicloLectivo.idCursoCicloLectivoTransaccional);
						command.Parameters.AddWithValue("idCurso", cursosCicloLectivo.idCurso);
						command.Parameters.AddWithValue("idCicloLectivo", cursosCicloLectivo.idCicloLectivo);
                        command.Parameters.AddWithValue("idOrientacion", cursosCicloLectivo.idOrientacionTransaccional);
                        command.Parameters.AddWithValue("idPreceptor", cursosCicloLectivo.curso.preceptor.idPersonalTransaccional);
                        
						command.ExecuteNonQuery();
						command.Parameters.Clear();
					}
				}
			}
			catch (SqlException ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarCursoCicloLectivo()", ClassName),
									ex, enuExceptionType.SqlException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(String.Format("Fallo en {0} - GrabarCursoCicloLectivo()", ClassName),
									ex, enuExceptionType.DataAccesException);
			}
		}

        /// <summary>
        /// Grabars the asignatura nivel.
        /// </summary>
        /// <param name="listaAsignaturaNivel">The lista asignatura nivel.</param>
        /// <param name="transaccion">The transaccion.</param>
        /// <exception cref="CustomizedException">
        /// </exception>
        public void GrabarAsignaturaNivel(List<AsignaturaNivel> listaAsignaturaNivel, SqlTransaction transaccion)
        {
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "AsignaturaNivel_Insert";
                    command.CommandTimeout = 10;

                    command.Connection = transaccion.Connection;
                    command.Transaction = transaccion;

                    foreach (AsignaturaNivel asignaturaNivel in listaAsignaturaNivel)
                    {
                        command.Parameters.AddWithValue("idAsignaturaNivel", 0);
                        command.Parameters.AddWithValue("idAsignaturaNivelTransaccional", asignaturaNivel.idAsignaturaNivelTransaccional);
                        command.Parameters.AddWithValue("idAsignatura", asignaturaNivel.asignatura.idAsignaturaTransaccional);
                        command.Parameters.AddWithValue("idNivel", asignaturaNivel.nivel.idNivelTransaccional);
                        command.Parameters.AddWithValue("idOrientacion", asignaturaNivel.orientacion.idOrientacionTransaccional);
                        command.Parameters.AddWithValue("cargaHoraria", asignaturaNivel.cargaHoraria);

                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarAsignaturaNivel()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarAsignaturaNivel()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Efectuars the baja usuarios.
        /// </summary>
        /// <param name="transaccion">The transaccion.</param>
        /// <exception cref="CustomizedException">
        /// </exception>
		public void EfectuarBajaUsuarios(SqlTransaction transaccion)
        {
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Baja_Usuarios";
                    command.CommandTimeout = 10;

                    command.Connection = transaccion.Connection;
                    command.Transaction = transaccion;

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - EfectuarBajaUsuarios()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - EfectuarBajaUsuarios()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
        #endregion

    }
}
