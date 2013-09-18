using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using EDUAR_Utility.Excepciones;
using System.Data.SqlClient;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Entities;

namespace EDUAR_SI_DataAccess
{
    public class DAStoreDatos : DABase
    {
        #region --[Atributos]--
        private const string ClassName = "DAStoreDatos";
        private MySqlConnection conMySQL;
        private const int DURACION_MODULO = 40;
        #endregion

        #region --[Propiedades]--
        #endregion

        #region --[Constructores]--
        /// <summary>
        /// Constructor. LLama al constructor de la clase base DABase.
        /// </summary>
        /// <param name="connectionString">Cadena de conexión a la base de datos</param>
        public DAStoreDatos(String connectionString)
            : base(connectionString)
        {

        }
        #endregion

        #region --[Métodos Públicos]--

        /// <summary>
        /// Registra una sanción en la BD transaccional.
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public void registrarSancionBDTransaccional(Configuraciones configuracion, Sancion unaSancion)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(configuracion.valor))
                {
                    MySqlCommand command = new MySqlCommand("sp_store_sancion;", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Add your parameters here if you need them
                    command.Parameters.Add(new MySqlParameter("var_curso", unaSancion.alumno.cursoCicloLectivo.curso.idCursoTransaccional));
                    command.Parameters.Add(new MySqlParameter("var_alumno", unaSancion.alumno.alumno.idAlumnoTransaccional));
                    command.Parameters.Add(new MySqlParameter("var_tipo_sancion", unaSancion.tipoSancion.idTipoSancionTransaccional));
                    command.Parameters.Add(new MySqlParameter("var_motivo_sancion", unaSancion.motivoSancion.idMotivoSancionTransaccional));
                    command.Parameters.Add(new MySqlParameter("var_cantidad", unaSancion.cantidad));
                    command.Parameters.Add(new MySqlParameter("var_fecha", unaSancion.fecha));

                    conn.Open();

                    command.ExecuteNonQuery();


                    //int result = (int)command.ExecuteScalar();
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - registrarSancionBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - registrarSancionBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - registrarSancionBDTransaccional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Registra una asistencia en la BD transaccional.
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public void registrarAsistenciaBDTransaccional(Configuraciones configuracion, Asistencia unaAsistencia)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(configuracion.valor))
                {
                    MySqlCommand command = new MySqlCommand("sp_store_inasistencia;", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Add your parameters here if you need them
                    command.Parameters.Add(new MySqlParameter("var_curso", unaAsistencia.alumno.cursoCicloLectivo.curso.idCursoTransaccional));
                    command.Parameters.Add(new MySqlParameter("var_alumno", unaAsistencia.alumno.alumno.idAlumnoTransaccional));
                    command.Parameters.Add(new MySqlParameter("var_tipo_asistencia", unaAsistencia.tipoAsistencia.idTipoAsistenciaTransaccional));
                    command.Parameters.Add(new MySqlParameter("var_fecha", unaAsistencia.fecha));

                    conn.Open();

                    command.ExecuteNonQuery();


                    //int result = (int)command.ExecuteScalar();
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - registrarSancionBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - registrarSancionBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - registrarSancionBDTransaccional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }





        /// <summary>
        /// Obteners the tipo asistencia BD transaccional.
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public List<TipoAsistencia> obtenerTipoAsistenciaBDTransaccional(Configuraciones configuracion)
        {
            List<TipoAsistencia> listadoTipoAsistencia = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandText = @"SELECT * 
                                            FROM tipoAsistencia";
                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    TipoAsistencia unTipoAsistencia;
                    listadoTipoAsistencia = new List<TipoAsistencia>();
                    while (reader.Read())
                    {
                        unTipoAsistencia = new TipoAsistencia();

                        unTipoAsistencia.idTipoAsistencia = 0;
                        unTipoAsistencia.idTipoAsistenciaTransaccional = (int)reader["id"];
                        unTipoAsistencia.descripcion = reader["descripcion"].ToString();
                        unTipoAsistencia.valor = (decimal)reader["valor"];

                        listadoTipoAsistencia.Add(unTipoAsistencia);
                    }
                    command.Connection.Close();
                    return listadoTipoAsistencia;
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerTipoAsistenciaBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerTipoAsistenciaBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerTipoAsistenciaBDTransaccional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Obteners the calificacion BD transaccional.
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public List<Calificacion> obtenerCalificacionBDTransaccional(Configuraciones configuracion)
        {
            List<Calificacion> listaCalificacion = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandText = @"SELECT *
                                            FROM boletincalificaciones";
                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    Calificacion calificacion;
                    listaCalificacion = new List<Calificacion>();
                    while (reader.Read())
                    {
                        DateTime fecha;
                        DateTime.TryParse(reader["fecha"].ToString(), out fecha);
                        if (fecha.Year > 1900)
                        {
                            calificacion = new Calificacion();
                            calificacion.idCalificacion = 0;
                            calificacion.idCalificacionTransaccional = (int)reader["id"];
                            calificacion.alumnoCurso.idAlumnoCursoCicloLectivoTransaccional = (int)reader["fk_alumnocursociclolectivo_id"];
                            calificacion.asignatura.idAsignaturaTransaccional = (int)reader["fk_asignatura_id"];
                            calificacion.periodo.idPeriodoTransaccional = (int)reader["fk_periodo_id"];
                            calificacion.escala.idValorEscalaCalificacionTransaccional = (int)reader["fk_escalanota_id"];
                            calificacion.observacion = reader["observacion"].ToString();
                            calificacion.fecha = fecha;
                            calificacion.instanciaCalificacion.idInstanciaCalificacion = (int)enumInstanciaCalificacion.Evaluacion;

                            listaCalificacion.Add(calificacion);
                        }
                    }
                    command.Connection.Close();
                    return listaCalificacion;
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerCalificacionBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerCalificacionBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerCalificacionBDTransaccional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Obtiene los examenes y los carga en la tabla de calificaciones
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public List<Calificacion> obtenerExamenBDTransaccional(Configuraciones configuracion)
        {
            List<Calificacion> listaCalificacion = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandText = @"SELECT id as 'idCalificacionTransaccional',
                                                fk_escalanota_id,
                                                fk_alumno_id,
                                                fk_actividad_id,
                                                fk_periodo_id,
                                                observacion,
                                                fecha
                                            FROM vw_examen";
                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    Calificacion calificacion;
                    listaCalificacion = new List<Calificacion>();
                    while (reader.Read())
                    {
                        DateTime fecha;
                        DateTime.TryParse(reader["fecha"].ToString(), out fecha);
                        if (fecha.Year > 1900)
                        {
                            calificacion = new Calificacion();
                            calificacion.idCalificacion = 0;
                            calificacion.idCalificacionTransaccional = (int)reader["idCalificacionTransaccional"];
                            calificacion.idCalificacion = 0;
                            calificacion.observacion = reader["observacion"].ToString();
                            calificacion.fecha = fecha;
                            calificacion.escala.idValorEscalaCalificacionTransaccional = (int)reader["fk_escalanota_id"];
                            calificacion.asignatura.idAsignaturaTransaccional = (int)reader["fk_actividad_id"];
                            calificacion.periodo.idPeriodoTransaccional = (int)reader["fk_periodo_id"];
                            calificacion.alumnoCurso.idAlumnoCursoCicloLectivoTransaccional = (int)reader["fk_alumno_id"];
                            calificacion.instanciaCalificacion.idInstanciaCalificacion = (int)enumInstanciaCalificacion.Examen;

                            listaCalificacion.Add(calificacion);
                        }
                    }
                    command.Connection.Close();
                    return listaCalificacion;
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerExamenBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerExamenBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerExamenBDTransaccional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Obteners the tipo sancion BD transaccional.
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public List<TipoSancion> obtenerTipoSancionBDTransaccional(Configuraciones configuracion)
        {
            List<TipoSancion> listadoTipoSancion = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandText = @"SELECT * 
                                            FROM tiposancion";
                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    TipoSancion unTipoSancion;
                    listadoTipoSancion = new List<TipoSancion>();
                    while (reader.Read())
                    {
                        unTipoSancion = new TipoSancion();

                        unTipoSancion.idTipoSancion = 0;
                        unTipoSancion.idTipoSancionTransaccional = (int)reader["id"];
                        unTipoSancion.descripcion = reader["descripcion"].ToString();
                        unTipoSancion.nombre = reader["nombre"].ToString();

                        listadoTipoSancion.Add(unTipoSancion);
                    }
                    command.Connection.Close();
                    return (listadoTipoSancion);
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerTipoSancionBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerTipoSancionBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerTipoSancionBDTransaccional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Obteners the tipo tutor BD transaccional.
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public List<TipoTutor> obtenerTipoTutorBDTransaccional(Configuraciones configuracion)
        {
            List<TipoTutor> listadoTipoTutor = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandText = @"SELECT * 
                                            FROM rol_responsable";
                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    TipoTutor unTipoTutor;
                    listadoTipoTutor = new List<TipoTutor>();
                    while (reader.Read())
                    {
                        unTipoTutor = new TipoTutor();

                        unTipoTutor.idTipoTutor = 0;
                        unTipoTutor.idTipoTutorTransaccional = (int)reader["id"];
                        unTipoTutor.descripcion = reader["nombre"].ToString();

                        listadoTipoTutor.Add(unTipoTutor);
                    }
                    command.Connection.Close();
                    return (listadoTipoTutor);
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerTipoTutorBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerTipoTutorBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obteneTipoTutorBDTransaccional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Obteners the motivo sancion BD transaccional.
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public List<MotivoSancion> obtenerMotivoSancionBDTransaccional(Configuraciones configuracion)
        {
            List<MotivoSancion> listadoMotivoSancion = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandText = @"SELECT * 
                                            FROM motivosancion";
                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    MotivoSancion unMotivoSancion;
                    listadoMotivoSancion = new List<MotivoSancion>();
                    while (reader.Read())
                    {
                        unMotivoSancion = new MotivoSancion();

                        unMotivoSancion.idMotivoSancion = 0;
                        unMotivoSancion.idMotivoSancionTransaccional = (int)reader["id"];
                        unMotivoSancion.descripcion = reader["descripcion"].ToString();

                        listadoMotivoSancion.Add(unMotivoSancion);
                    }
                    command.Connection.Close();
                    return (listadoMotivoSancion);
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerMotivoSancionBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerMotivoSancionBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obteneMotivoSancionBDTransaccional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Obteners the sancion BD transaccional.
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public List<Sancion> obtenerSancionBDTransaccional(Configuraciones configuracion)
        {
            List<Sancion> listadoSancion = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandText = @"SELECT * 
                                            FROM sancion";
                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    Sancion unaSancion;
                    listadoSancion = new List<Sancion>();
                    while (reader.Read())
                    {
                        unaSancion = new Sancion();

                        unaSancion.idSancion = 0;
                        unaSancion.idSancionTransaccional = (int)reader["id"];
                        unaSancion.cantidad = (int)reader["cantidad"];
                        unaSancion.fecha = Convert.ToDateTime(reader["fecha"]);
                        unaSancion.motivoSancion.idMotivoSancionTransaccional = (int)reader["fk_motivosancion_id"];
                        unaSancion.tipoSancion.idTipoSancionTransaccional = (int)reader["fk_tiposancion_id"];
                        unaSancion.alumno.idAlumnoCursoCicloLectivoTransaccional = (int)reader["fk_alumnocursociclolectivo_id"];

                        listadoSancion.Add(unaSancion);
                    }
                    command.Connection.Close();
                    return (listadoSancion);
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerSancionBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerSancionBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obteneSancionBDTransaccional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Obteners the alumno curso BD transaccional.
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public List<AlumnoCursoCicloLectivo> obtenerAlumnoCursoBDTransaccional(Configuraciones configuracion)
        {
            List<AlumnoCursoCicloLectivo> listaAlumnoCurso = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;
                    command.CommandText = @"SELECT *
                                            FROM rel_alumno_curso_ciclolectivo";
                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    AlumnoCursoCicloLectivo alumnoCurso;
                    listaAlumnoCurso = new List<AlumnoCursoCicloLectivo>();

                    while (reader.Read())
                    {
                        alumnoCurso = new AlumnoCursoCicloLectivo();
                        alumnoCurso.idAlumnoCursoCicloLectivo = 0;
                        alumnoCurso.idAlumnoCursoCicloLectivoTransaccional = (int)reader["id"];
                        alumnoCurso.cursoCicloLectivo.idCursoCicloLectivoTransaccional = (int)reader["fk_cursociclolectivo_id"];
                        alumnoCurso.alumno.idAlumnoTransaccional = (int)reader["fk_alumno_id"];
                        listaAlumnoCurso.Add(alumnoCurso);
                    }
                    return listaAlumnoCurso;
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerAlumnoCursoBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerAlumnoCursoBDTransaccional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Obteners the tutores alumno BD transaccional.
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public List<Alumno> obtenerTutoresAlumnoBDTransaccional(Configuraciones configuracion)
        {
            List<Alumno> listaAlumnos = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandText = @"SELECT * 
                                            FROM rel_responsable_rolresponsable";
                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    Tutor tutor;
                    Alumno alumno;

                    listaAlumnos = new List<Alumno>();
                    while (reader.Read())
                    {

                        tutor = new Tutor();
                        tutor.idTutorTransaccional = (int)reader["fk_responsable_id"];

                        alumno = new Alumno();
                        alumno.idAlumnoTransaccional = (int)reader["fk_alumno_id"];

                        alumno.listaTutores.Add(tutor);
                        listaAlumnos.Add(alumno);
                    }
                    command.Connection.Close();
                    return listaAlumnos;
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerTutoresAlumnoBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerTutoresAlumnoBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerTutoresAlumnoBDTransaccional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        public List<DiasHorarios> obtenerHorarios(Configuraciones configuracion)
        {
            List<DiasHorarios> listadoDiasHorarios = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandText = @"SELECT * 
                                            FROM rel_diashorarios";
                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    DiasHorarios unDiasHorarios;
                    listadoDiasHorarios = new List<DiasHorarios>();
                    while (reader.Read())
                    {
                        unDiasHorarios = new DiasHorarios();

                        unDiasHorarios.idDiaHorario = 0;
                        unDiasHorarios.idDiaHorarioTransaccional = (int)reader["id"];
                        unDiasHorarios.unDia = (enumDiasSemana)reader["fk_diasemana_id"];
                        unDiasHorarios.modulos = getModulos(Convert.ToDateTime(reader["fecha_inicio"]), Convert.ToDateTime(reader["fecha_fin"]));
                        unDiasHorarios.idAsignaturaTransaccional = (int)reader["fk_asignatura_id"]; // Asignatura
                        unDiasHorarios.idCursoTransaccional = (int)reader["fk_curso_id"]; // Curso (SQL Server) = Divsion (MySQL)
                        unDiasHorarios.idNivelTransaccional = (int)reader["fk_nivel_id"]; // Nivel (SQL Server) = Anio (MySQL)

                        //unDiasHorarios.motivoDiasHorario.idMotivoDiasHorarioTransaccional = (int)reader["fk_motivoDiasHorario_id"];
                        //unDiasHorarios.tipoDiasHorario.idTipoDiasHorarioTransaccional = (int)reader["fk_tipoDiasHorario_id"];

                        listadoDiasHorarios.Add(unDiasHorarios);
                    }
                    command.Connection.Close();
                    return (listadoDiasHorarios);
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerDiasHorarioBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerDiasHorarioBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerDiasHorarioBDTransaccional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        private List<Modulo> getModulos(DateTime hora_inicio, DateTime hora_fin)
        {
            bool aux = true;
            List<Modulo> ListaModulos = new List<Modulo>();
            Modulo unModulo = new Modulo();


            while (aux)
            {
                unModulo.horaInicio = hora_inicio;

                TimeSpan dif = hora_fin - hora_inicio;

                if ((dif.Minutes) <= DURACION_MODULO)
                {
                    aux = false;
                    unModulo.horaFinalizacion = hora_fin;
                }
                else
                {
                    unModulo.horaFinalizacion = hora_inicio.AddMinutes(DURACION_MODULO);
                    hora_inicio = unModulo.horaFinalizacion;
                }

                ListaModulos.Add(unModulo);
            }

            return (ListaModulos);
        }

        /// <summary>
        /// Obteners the curso ciclo lectivo BD transaccional.
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public List<CursoCicloLectivo> obtenerCursoCicloLectivoBDTransaccional(Configuraciones configuracion)
        {
            List<CursoCicloLectivo> listadoCursoCicloLectivo = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandText = @"SELECT * 
                                            FROM rel_curso_ciclolectivo";
                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    CursoCicloLectivo objCursoCicloLectivo;
                    listadoCursoCicloLectivo = new List<CursoCicloLectivo>();
                    while (reader.Read())
                    {
                        objCursoCicloLectivo = new CursoCicloLectivo();

                        objCursoCicloLectivo.idCursoCicloLectivoTransaccional = Convert.ToInt32(reader["id"]);
                        objCursoCicloLectivo.idCurso = Convert.ToInt32(reader["fk_curso"]);
                        objCursoCicloLectivo.idCicloLectivo = Convert.ToInt32(reader["fk_ciclolectivo"]);

                        listadoCursoCicloLectivo.Add(objCursoCicloLectivo);
                    }
                    command.Connection.Close();
                    return listadoCursoCicloLectivo;
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerCursoCicloLectivoBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerCursoCicloLectivoBDTransaccional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        public List<Alumno> obtenerAlumnosCursoCicloLectivoActual(Configuraciones configuracion, int selectedCurso)
        {
            List<Alumno> listaAlumnos = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.CommandText = @"sp_alumnos_curso";
                    command.Parameters.Add(new MySqlParameter("param_curso", MySqlDbType.Int32)).Value = selectedCurso;

                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    listaAlumnos = new List<Alumno>();
                    Alumno alumno = null;
                    while (reader.Read())
                    {
                        alumno = new Alumno()
                        {
                            idPersona = 0,
                            idAlumnoTransaccional = Convert.ToInt32(reader["id"]),
                            nombre = reader["nombre"].ToString(),
                            apellido = reader["apellido"].ToString(),
                            fechaNacimiento = Convert.ToDateTime(reader["fecha_nacimiento"]),
                            numeroDocumento = Convert.ToInt32(reader["nro_documento"].ToString().Replace("M", "")),
                            idTipoDocumento = Convert.ToInt32(reader["fk_tipodocumento_id"]),
                            domicilio = reader["direccion"].ToString(),
                            sexo = reader["sexo"].ToString(),
                            telefonoFijo = reader["telefono"].ToString(),
                            email = reader["email"].ToString(),
                            activo = Convert.ToBoolean(reader["activo"]),
                            localidad = new Localidades() { nombre = reader["ciudad"].ToString() }
                        };

                        listaAlumnos.Add(alumno);
                    }
                    command.Connection.Close();
                    return listaAlumnos;
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerAlumnosCursoCicloLectivoActual()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerAlumnosCursoCicloLectivoActual()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerAlumnosCursoCicloLectivoActual()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        public List<Asignatura> obtenerAsignaturaCursoCicloLectivoActual(Configuraciones configuracion, int selectedCurso)
        {
            List<Asignatura> listaAsignatura = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.CommandText = @"sp_asignatura_curso";
                    command.Parameters.Add(new MySqlParameter("param_curso", MySqlDbType.Int32)).Value = selectedCurso;

                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    listaAsignatura = new List<Asignatura>();
                    Asignatura asignatura = null;
                    while (reader.Read())
                    {
                        asignatura = new Asignatura()
                        {
                            idAsignatura = 0,
                            idAsignaturaTransaccional = Convert.ToInt32(reader["id"]),
                            nombre = reader["nombre"].ToString(),
                        };

                        listaAsignatura.Add(asignatura);
                    }
                    command.Connection.Close();
                    return listaAsignatura;
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerAsignaturaCursoCicloLectivoActual()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerAsignaturaCursoCicloLectivoActual()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerAsignaturaCursoCicloLectivoActual()", ClassName),
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
