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
        /// Obteners the configuracion.
        /// </summary>
        /// <param name="parametroConfiguracion">The parametro configuracion.</param>
        /// <returns></returns>
        public Configuraciones ObtenerConfiguracion(enumConfiguraciones configuracion)
        {
            Configuraciones objConfiguracion = null;
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    sqlConnectionConfig.Open();

                    command.Connection = sqlConnectionConfig;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Configuraciones_Select";
                    command.CommandTimeout = 10;

                    command.Parameters.AddWithValue("@nombre", configuracion.ToString());

                    SqlDataReader reader = command.ExecuteReader();
                    objConfiguracion = new Configuraciones();
                    while (reader.Read())
                    {
                        objConfiguracion.idConfiguracion = (int)reader["idConfiguracion"];
                        objConfiguracion.nombre = reader["nombre"].ToString();
                        objConfiguracion.descripcion = reader["descripcion"].ToString();
                        objConfiguracion.activo = (bool)reader["activo"];
                        objConfiguracion.valor = reader["valor"].ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - ObtenerConfiguracion()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - ObtenerConfiguracion()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                if (sqlConnectionConfig.State == ConnectionState.Open)
                    sqlConnectionConfig.Close();
            }
            return objConfiguracion;
        }

        /// <summary>
        /// Guarda una colección de personas en base de datos
        /// </summary>
        /// <param name="objeto">Colección de Persona</param>
        public int GrabarPersona(Persona persona, ref SqlTransaction transaccion)
        {
            //SqlTransaction transaccion = null;
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    if (sqlConnectionConfig.State == ConnectionState.Closed) sqlConnectionConfig.Open();

                    command.Connection = sqlConnectionConfig;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Personas_Insert";
                    command.CommandTimeout = 10;
                    if (transaccion == null)
                        transaccion = sqlConnectionConfig.BeginTransaction();
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
                    command.Parameters.AddWithValue("fechaNacimiento", persona.fechaNacimiento);
                    command.Parameters.AddWithValue("telefonoFijo", persona.telefonoFijo);
                    command.Parameters.AddWithValue("telefonoCelular", persona.telefonoCelular);
                    command.Parameters.AddWithValue("telefonoCelularAlternativo", persona.telefonoCelularAlternativo);
                    command.Parameters.AddWithValue("email", persona.email);
                    command.Parameters.AddWithValue("activo", persona.activo);
                    command.ExecuteNonQuery();
                    //transaccion.Commit();
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
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Grabars the alumno.
        /// </summary>
        /// <param name="alumno">The alumno.</param>
        /// <param name="transaccion">The transaccion.</param>
        public void GrabarAlumno(Alumno alumno, ref SqlTransaction transaccion)
        {
            //SqlTransaction transaccion = null;
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    if (sqlConnectionConfig.State == ConnectionState.Closed) sqlConnectionConfig.Open();

                    command.Connection = sqlConnectionConfig;

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Alumnos_Insert";
                    command.CommandTimeout = 10;
                    if (transaccion == null)
                        transaccion = sqlConnectionConfig.BeginTransaction();
                    command.Transaction = transaccion;

                    command.Parameters.AddWithValue("idAlumno", alumno.idAlumno).Direction = ParameterDirection.Output;
                    command.Parameters.AddWithValue("idAlumnoTransaccional", alumno.idAlumnoTransaccional);
                    command.Parameters.AddWithValue("idPersona", alumno.idPersona);
                    command.Parameters.AddWithValue("legajo", alumno.legajo);
                    command.Parameters.AddWithValue("fechaAlta", DBNull.Value);
                    command.Parameters.AddWithValue("fechaBaja", DBNull.Value);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    //transaccion.Commit();
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
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Grabars the pais.
        /// </summary>
        /// <param name="colPaises">The col paises.</param>
        public void GrabarPais(List<Paises> listaPaises)
        {
            SqlTransaction transaccion = null;
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    if (sqlConnectionConfig.State == ConnectionState.Closed) sqlConnectionConfig.Open();

                    command.Connection = sqlConnectionConfig;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Paises_Insert";
                    command.CommandTimeout = 10;

                    transaccion = sqlConnectionConfig.BeginTransaction();
                    command.Transaction = transaccion;

                    foreach (Paises pais in listaPaises)
                    {
                        command.Parameters.AddWithValue("idPais", 0).Direction = ParameterDirection.Output;
                        command.Parameters.AddWithValue("idPaisTransaccional", pais.idPaisTransaccional);
                        command.Parameters.AddWithValue("nombre", pais.nombre);
                        command.Parameters.AddWithValue("descripcion", pais.descripcion);
                        command.Parameters.AddWithValue("activo", pais.activo);
                        int cantidad = command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    transaccion.Commit();
                }
            }
            catch (SqlException ex)
            {
                if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarPais()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarPais()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Grabars the provincia.
        /// </summary>
        /// <param name="colProvincias">The col provincias.</param>
        public void GrabarProvincia(List<Provincias> listaProvincias)
        {
            SqlTransaction transaccion = null;
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    if (sqlConnectionConfig.State == ConnectionState.Closed) sqlConnectionConfig.Open();

                    command.Connection = sqlConnectionConfig;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Provincias_Insert";
                    command.CommandTimeout = 10;

                    transaccion = sqlConnectionConfig.BeginTransaction();
                    command.Transaction = transaccion;

                    foreach (Provincias provincia in listaProvincias)
                    {
                        command.Parameters.AddWithValue("idProvincia", 0).Direction = ParameterDirection.Output;
                        command.Parameters.AddWithValue("idProvinciaTransaccional", provincia.idProvinciaTransaccional);
                        command.Parameters.AddWithValue("nombre", provincia.nombre);
                        command.Parameters.AddWithValue("descripcion", provincia.descripcion);
                        command.Parameters.AddWithValue("idPais", provincia.idPais);
                        command.Parameters.AddWithValue("activo", provincia.activo);
                        int cantidad = command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    transaccion.Commit();
                }
            }
            catch (SqlException ex)
            {
                if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarProvincia()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarProvincia()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Grabars the localidad.
        /// </summary>
        /// <param name="colLocalidades">The col localidades.</param>
        public void GrabarLocalidad(List<Localidades> ListaLocalidades)
        {
            SqlTransaction transaccion = null;
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    if (sqlConnectionConfig.State == ConnectionState.Closed) sqlConnectionConfig.Open();

                    command.Connection = sqlConnectionConfig;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Localidades_Insert";
                    command.CommandTimeout = 10;

                    transaccion = sqlConnectionConfig.BeginTransaction();
                    command.Transaction = transaccion;

                    foreach (Localidades localidad in ListaLocalidades)
                    {
                        command.Parameters.AddWithValue("idLocalidad", 0);
                        command.Parameters.AddWithValue("idLocalidadTransaccional", 0);
                        command.Parameters.AddWithValue("nombre", localidad.nombre);
                        command.Parameters.AddWithValue("descripcion", "");
                        command.Parameters.AddWithValue("idProvincia", localidad.idProvincia);
                        command.Parameters.AddWithValue("activo", localidad.activo);
                        int cantidad = command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    transaccion.Commit();
                }
            }
            catch (SqlException ex)
            {
                if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarLocalidad()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarLocalidad()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Grabars the tipos documentos.
        /// </summary>
        /// <param name="colTipoDocumento">The col tipos documentos.</param>
        public void GrabarTipoDocumento(List<TipoDocumento> listaTipoDocumento)
        {
            SqlTransaction transaccion = null;
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    if (sqlConnectionConfig.State == ConnectionState.Closed) sqlConnectionConfig.Open();

                    command.Connection = sqlConnectionConfig;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "TiposDocumentos_Insert";
                    command.CommandTimeout = 10;

                    transaccion = sqlConnectionConfig.BeginTransaction();
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
                    transaccion.Commit();
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
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Grabars the valores escalas calificaciones.
        /// </summary>
        /// <param name="listaValoresCalificacion">The lista valores calificacion.</param>
        /// <param name="transaccion">The transaccion.</param>
        public void GrabarValoresEscalasCalificaciones(List<ValoresEscalaCalificacion> listaValoresCalificacion)
        {
            SqlTransaction transaccion = null;
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    if (sqlConnectionConfig.State == ConnectionState.Closed) sqlConnectionConfig.Open();

                    command.Connection = sqlConnectionConfig;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "ValoresEscalaCalificacion_Insert";
                    command.CommandTimeout = 10;

                    transaccion = sqlConnectionConfig.BeginTransaction();
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
                        command.Parameters.AddWithValue("idEscalaCalificacion", valorEscala.idEscalaCalificacion);
                        int cantidad = command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    transaccion.Commit();
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
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Grabars the personal.
        /// </summary>
        /// <param name="personal">The personal.</param>
        /// <param name="transaccion">The transaccion.</param>
        public void GrabarPersonal(Personal personal, ref SqlTransaction transaccion)
        {
            //SqlTransaction transaccion = null;
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    if (sqlConnectionConfig.State == ConnectionState.Closed) sqlConnectionConfig.Open();

                    command.Connection = sqlConnectionConfig;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Personal_Insert";
                    command.CommandTimeout = 10;
                    if (transaccion == null)
                        transaccion = sqlConnectionConfig.BeginTransaction();
                    command.Transaction = transaccion;

                    command.Parameters.AddWithValue("idPersonal", personal.IdPersonal).Direction = ParameterDirection.Output;
                    command.Parameters.AddWithValue("idPersonalTransaccional", personal.IdPersonalTransaccional);
                    command.Parameters.AddWithValue("idPersona", personal.idPersona);
                    command.Parameters.AddWithValue("legajo", personal.legajo);
                    command.Parameters.AddWithValue("fechaAlta", DBNull.Value);
                    command.Parameters.AddWithValue("fechaBaja", DBNull.Value);
                    command.Parameters.AddWithValue("activo", personal.activo);
                    command.Parameters.AddWithValue("idCargoPersonal", personal.cargo.idCargoTransaccional);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    //transaccion.Commit();
                }
            }
            catch (SqlException ex)
            {
                //if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarPersonal()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                //if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarPersonal()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }

        }

        /// <summary>
        /// Grabars the cargo personal.
        /// </summary>
        /// <param name="listaCargosPersonal">The lista cargos personal.</param>
        /// <param name="transaccion">The transaccion.</param>
        public void GrabarCargoPersonal(List<CargoPersonal> listaCargosPersonal)
        {
            SqlTransaction transaccion = null;
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    if (sqlConnectionConfig.State == ConnectionState.Closed) sqlConnectionConfig.Open();

                    command.Connection = sqlConnectionConfig;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "CargosPersonal_Insert";
                    command.CommandTimeout = 10;

                    transaccion = sqlConnectionConfig.BeginTransaction();
                    command.Transaction = transaccion;

                    foreach (CargoPersonal cargoPersonal in listaCargosPersonal)
                    {
                        command.Parameters.AddWithValue("idCargoPersonal", 0);
                        command.Parameters.AddWithValue("idCargoPersonalTransaccional", cargoPersonal.idCargoTransaccional);
                        command.Parameters.AddWithValue("nombre", cargoPersonal.nombre);
                        command.Parameters.AddWithValue("descripcion", cargoPersonal.descripcion);
                        command.Parameters.AddWithValue("activo", cargoPersonal.activo);
                        int cantidad = command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    transaccion.Commit();
                }
            }
            catch (SqlException ex)
            {
                if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarCargoPersonal()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                if (transaccion != null) transaccion.Rollback();
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
        public void GrabarAsignatura(List<Asignatura> listaAsignatura)
        {
            SqlTransaction transaccion = null;
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    if (sqlConnectionConfig.State == ConnectionState.Closed) sqlConnectionConfig.Open();

                    command.Connection = sqlConnectionConfig;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Asignatura_Insert";
                    command.CommandTimeout = 10;

                    transaccion = sqlConnectionConfig.BeginTransaction();
                    command.Transaction = transaccion;

                    foreach (Asignatura asignatura in listaAsignatura)
                    {
                        command.Parameters.AddWithValue("idAsignatura", 0);
                        command.Parameters.AddWithValue("idAsignaturaTransaccional", asignatura.idAsignaturaTransaccional);
                        command.Parameters.AddWithValue("nombre", asignatura.nombre);
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    transaccion.Commit();
                }
            }
            catch (SqlException ex)
            {
                if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarAsignatura()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarAsignatura()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Grabars the ciclo lectivo.
        /// </summary>
        /// <param name="listaCicloLectivo">The lista ciclo lectivo.</param>
        public void GrabarCicloLectivo(List<CicloLectivo> listaCicloLectivo)
        {
            SqlTransaction transaccion = null;
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    if (sqlConnectionConfig.State == ConnectionState.Closed) sqlConnectionConfig.Open();

                    command.Connection = sqlConnectionConfig;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "CicloLectivo_Insert";
                    command.CommandTimeout = 10;

                    transaccion = sqlConnectionConfig.BeginTransaction();
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
                    transaccion.Commit();
                }
            }
            catch (SqlException ex)
            {
                if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarAsignatura()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarAsignatura()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Grabars the nivel.
        /// </summary>
        /// <param name="listaNiveles">The lista niveles.</param>
        public void GrabarNivel(List<Nivel> listaNiveles)
        {
            SqlTransaction transaccion = null;
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    if (sqlConnectionConfig.State == ConnectionState.Closed) sqlConnectionConfig.Open();

                    command.Connection = sqlConnectionConfig;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Nivel_Insert";
                    command.CommandTimeout = 10;

                    transaccion = sqlConnectionConfig.BeginTransaction();
                    command.Transaction = transaccion;

                    foreach (Nivel nivel in listaNiveles)
                    {
                        command.Parameters.AddWithValue("idNivel", 0);
                        command.Parameters.AddWithValue("idNivelTransaccional", nivel.idNivelTransaccional);
                        command.Parameters.AddWithValue("nombre", nivel.nombre);
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    transaccion.Commit();
                }
            }
            catch (SqlException ex)
            {
                if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarNivel()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarNivel()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Grabars the cursos.
        /// </summary>
        /// <param name="listaCursos">The lista cursos.</param>
        public void GrabarCursos(List<Curso> listaCursos)
        {
            SqlTransaction transaccion = null;
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    if (sqlConnectionConfig.State == ConnectionState.Closed) sqlConnectionConfig.Open();

                    command.Connection = sqlConnectionConfig;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Curso_Insert";
                    command.CommandTimeout = 10;

                    transaccion = sqlConnectionConfig.BeginTransaction();
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
                    transaccion.Commit();
                }
            }
            catch (SqlException ex)
            {
                if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarNivel()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarNivel()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Grabars the orientacion.
        /// </summary>
        /// <param name="listaOrientacion">The lista orientacion.</param>
        public void GrabarOrientacion(List<Orientacion> listaOrientacion)
        {
            SqlTransaction transaccion = null;
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    if (sqlConnectionConfig.State == ConnectionState.Closed) sqlConnectionConfig.Open();

                    command.Connection = sqlConnectionConfig;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Orientacion_Insert";
                    command.CommandTimeout = 10;

                    transaccion = sqlConnectionConfig.BeginTransaction();
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
                    transaccion.Commit();
                }
            }
            catch (SqlException ex)
            {
                if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarOrientacion()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarOrientacion()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Grabars the asignatura curso.
        /// </summary>
        /// <param name="listaAsignatura">The lista asignatura.</param>
        public void GrabarAsignaturaCurso(List<Asignatura> listaAsignatura)
        {
            SqlTransaction transaccion = null;
            Asignatura actual;
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    if (sqlConnectionConfig.State == ConnectionState.Closed) sqlConnectionConfig.Open();

                    command.Connection = sqlConnectionConfig;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "AsignaturaCurso_Insert";
                    command.CommandTimeout = 10;

                    transaccion = sqlConnectionConfig.BeginTransaction();
                    command.Transaction = transaccion;
                    actual = new Asignatura();
                    foreach (Asignatura asignatura in listaAsignatura)
                    {
                        actual = asignatura;
                        command.Parameters.AddWithValue("idAsignaturaCurso", 0);
                        command.Parameters.AddWithValue("idNivel", asignatura.curso.nivel.idNivel);
                        command.Parameters.AddWithValue("idAsignatura", asignatura.idAsignatura);
                        command.Parameters.AddWithValue("idDocente", asignatura.docente.idDocente);
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    transaccion.Commit();
                }
            }
            catch (SqlException ex)
            {
                if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarAsignaturaCurso()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarAsignaturaCurso()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Grabars the motivo ausencia.
        /// </summary>
        /// <param name="listaMotivos">The lista motivos.</param>
        public void GrabarMotivoAusencia(List<MotivoAusencia> listaMotivos)
        {
            SqlTransaction transaccion = null;
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    if (sqlConnectionConfig.State == ConnectionState.Closed) sqlConnectionConfig.Open();

                    command.Connection = sqlConnectionConfig;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "MotivoAusencia_Insert";
                    command.CommandTimeout = 10;

                    transaccion = sqlConnectionConfig.BeginTransaction();
                    command.Transaction = transaccion;

                    foreach (MotivoAusencia motivo in listaMotivos)
                    {
                        command.Parameters.AddWithValue("idMotivoAusencia", 0);
                        command.Parameters.AddWithValue("idMotivoAusenciaTransaccional", motivo.idMotivoTransaccional);
                        command.Parameters.AddWithValue("nombre", motivo.nombre);
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    transaccion.Commit();
                }
            }
            catch (SqlException ex)
            {
                if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarMotivoAusencia()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarMotivoAusencia()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        public void GrabarPeriodo(List<Periodo> listaPeriodo)
        {
            SqlTransaction transaccion = null;
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    if (sqlConnectionConfig.State == ConnectionState.Closed) sqlConnectionConfig.Open();

                    command.Connection = sqlConnectionConfig;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Periodo_Insert";
                    command.CommandTimeout = 10;

                    transaccion = sqlConnectionConfig.BeginTransaction();
                    command.Transaction = transaccion;

                    foreach (Periodo periodo in listaPeriodo)
                    {
                        command.Parameters.AddWithValue("idPeriodo", 0);
                        command.Parameters.AddWithValue("idPeriodoTransaccional", periodo.idPeriodoTransaccional);
                        command.Parameters.AddWithValue("nombre", periodo.nombre);
                        command.Parameters.AddWithValue("idCicloLectivo", periodo.cicloLectivo.idCicloLectivoTransaccional);
                        command.Parameters.AddWithValue("fechaInicio",  periodo.fechaInicio);
                        command.Parameters.AddWithValue("fechaFin", periodo.fechaFin);
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    transaccion.Commit();
                }
            }
            catch (SqlException ex)
            {
                if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarPeriodo()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarPeriodo()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }


        //TODO: Primero hacer GrabarTipoAsistencia()
        public void GrabarTipoAsistencia(List<TipoAsistencia> listadoTipoAsistencia)
        {
            SqlTransaction transaccion = null;
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    if (sqlConnectionConfig.State == ConnectionState.Closed) sqlConnectionConfig.Open();

                    command.Connection = sqlConnectionConfig;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "TipoAsistencia_Insert";
                    command.CommandTimeout = 10;

                    transaccion = sqlConnectionConfig.BeginTransaction();
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
                    transaccion.Commit();
                }
            }
            catch (SqlException ex)
            {
                if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarTipoAsistenica()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarTipoAsistencia()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
 
        }

        public void GrabarAsistencia(List<Asistencia> listadoAsistencia)
        {
            SqlTransaction transaccion = null;
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    if (sqlConnectionConfig.State == ConnectionState.Closed) sqlConnectionConfig.Open();

                    command.Connection = sqlConnectionConfig;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Asistencia_Insert";
                    command.CommandTimeout = 10;

                    transaccion = sqlConnectionConfig.BeginTransaction();
                    command.Transaction = transaccion;

                    foreach (Asistencia unaAsistencia in listadoAsistencia)
                    {
                        command.Parameters.AddWithValue("idAsistencia", 0);
                        command.Parameters.AddWithValue("idAsistenciaTransaccional", unaAsistencia.idAsistenciaTransaccional);
                        command.Parameters.AddWithValue("fecha", unaAsistencia.fecha);
                        command.Parameters.AddWithValue("idTipoAsistencia", unaAsistencia.tipoAsistencia.idTipoAsistenciaTransaccional);
                        command.Parameters.AddWithValue("idAlumno", unaAsistencia.tipoAsistencia.idTipoAsistenciaTransaccional);
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    transaccion.Commit();
                }
            }
            catch (SqlException ex)
            {
                if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarAsistenica()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                if (transaccion != null) transaccion.Rollback();
                throw new CustomizedException(String.Format("Fallo en {0} - GrabarAsistencia()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

   #endregion
    }

}
