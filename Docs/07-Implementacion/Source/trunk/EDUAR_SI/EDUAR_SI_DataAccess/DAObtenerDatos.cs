using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EDUAR_Entities;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;
using MySql.Data.MySqlClient;

namespace EDUAR_SI_DataAccess
{
    public class DAObtenerDatos : DABase
    {
        #region --[Atributos]--
        private const string ClassName = "DAObtenerDatos";
        private MySqlConnection conMySQL;
        #endregion

        #region --[Propiedades]--
        #endregion

        #region --[Constructores]--
        /// <summary>
        /// Constructor. LLama al constructor de la clase base DABase.
        /// </summary>
        /// <param name="connectionString">Cadena de conexión a la base de datos</param>
        public DAObtenerDatos(String connectionString)
            : base(connectionString)
        {

        }
        #endregion

        #region --[Métodos Públicos]--
        /// <summary>
        /// Obteners the alumnos BD transaccional.
        /// </summary>
        /// <param name="configuracion">Una configuración con la cadena de bd transaccional.</param>
        /// <returns></returns>
        public List<Alumno> obtenerAlumnoBDTransaccional(Configuraciones configuracion)
        {
            List<Alumno> listaAlumno = null;
            
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandText = @"SELECT * 
                                            FROM vw_alumno";
                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    listaAlumno = new List<Alumno>();
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
                            localidad = new Localidades() { nombre = reader["ciudad"].ToString()}
                        };

                        listaAlumno.Add(alumno);
                    }
                    command.Connection.Close();
                    return listaAlumno;
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerAlumnoBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerAlumnoBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerAlumnoBDTransaccional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Obteners the localidades BD transaccional.
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public List<Localidades> obtenerLocalidadesBDTransaccional(Configuraciones configuracion)
        {
            List<Localidades> listaLocalidades = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandText = @"SELECT DISTINCT ciudad, fk_provincia_id
                                                    FROM alumno
                                                    WHERE fk_provincia_id >0";
                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    Localidades localidad;
                    listaLocalidades = new List<Localidades>();
                    while (reader.Read())
                    {
                        localidad = new Localidades()
                        {
                            idLocalidad = 0,
                            idLocalidadTransaccional = 0,
                            nombre = reader["ciudad"].ToString(),
                            idProvincia = Convert.ToInt32(reader["fk_provincia_id"]),
                            activo = true
                        };
                        listaLocalidades.Add(localidad);
                    }
                    command.Connection.Close();
                    return listaLocalidades;
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerLocalidadesBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerLocalidadesBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerLocalidadesBDTransaccional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }

        }

        /// <summary>
        /// Obteners the provincias BD transaccional.
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public List<Provincias> obtenerProvinciasBDTransaccional(Configuraciones configuracion)
        {
            List<Provincias> listaProvincias = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandText = @"SELECT *
                                                    FROM provincia";
                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    Provincias provincia;
                    listaProvincias = new List<Provincias>();
                    while (reader.Read())
                    {
                        provincia = new Provincias()
                        {
                            idProvincia = 0,
                            idProvinciaTransaccional = Convert.ToInt32(reader["id"].ToString()),
                            nombre = reader["nombre_corto"].ToString(),
                            descripcion = reader["nombre_largo"].ToString(),
                            idPais = Convert.ToInt32(reader["fk_pais_id"].ToString()),
                            activo = true
                        };
                        listaProvincias.Add(provincia);
                    }
                    command.Connection.Close();
                    return listaProvincias;
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerProvinciasBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerProvinciasBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerProvinciasBDTransaccional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Obteners the paises BD transaccional.
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public List<Paises> obtenerPaisesBDTransaccional(Configuraciones configuracion)
        {
            List<Paises> listaPaises = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandText = @"SELECT id,nombre_corto,nombre_largo 
                                                    FROM pais";
                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    Paises pais;
                    listaPaises = new List<Paises>();
                    while (reader.Read())
                    {
                        pais = new Paises()
                        {
                            idPais = 0,
                            idPaisTransaccional = (int)reader["id"],
                            nombre = reader["nombre_corto"].ToString(),
                            descripcion = reader["nombre_largo"].ToString(),
                            activo = true
                        };
                        listaPaises.Add(pais);
                    }
                    command.Connection.Close();
                    return listaPaises;
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerPaisesBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerPaisesBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerPaisesBDTransaccional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Obteners the tipos documentos BD transaccional.
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public List<TipoDocumento> obtenerTipoDocumentoBDTransaccional(Configuraciones configuracion)
        {
            List<TipoDocumento> listaTipoDocumento = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandText = @"SELECT * 
                                                    FROM tipodocumento";
                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    TipoDocumento tipoDocumento;
                    listaTipoDocumento = new List<TipoDocumento>();
                    while (reader.Read())
                    {
                        tipoDocumento = new TipoDocumento()
                        {
                            idTipoDocumento = 0,
                            idTipoDocumentoTransaccional = Convert.ToInt32(reader["id"]),
                            nombre = reader["nombre"].ToString(),
                            descripcion = reader["descripcion"].ToString(),
                            activo = true
                        };
                        listaTipoDocumento.Add(tipoDocumento);
                    }
                    command.Connection.Close();
                    return listaTipoDocumento;
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerTipoDocumentoBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerTipoDocumentoBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerTipoDocumentoBDTransaccional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Obteners the valores escala calificacion.
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public List<ValoresEscalaCalificacion> obtenerValoresEscalaCalificacionBDTransaccional(Configuraciones configuracion)
        {
            List<ValoresEscalaCalificacion> listEscalaCalificacion = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandText = @"SELECT  id as 'idEscalaNotaTransaccional',
                                                    nombre,
                                                    descripcion,
                                                    nombre as 'valor',
                                                    aprobado
                                            FROM vw_ValoresEscalaCalificacion";
                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    ValoresEscalaCalificacion valorEscala;
                    listEscalaCalificacion = new List<ValoresEscalaCalificacion>();
                    while (reader.Read())
                    {
                        valorEscala = new ValoresEscalaCalificacion()
                        {
                            idValorEscalaCalificacion = 0,
                            idValorEscalaCalificacionTransaccional = Convert.ToInt32(reader["idEscalaNotaTransaccional"]),
                            nombre = reader["nombre"].ToString(),
                            descripcion = reader["descripcion"].ToString(),
                            valor = reader["valor"].ToString(),
                            activo = true,
                            aprobado = Convert.ToBoolean(reader["aprobado"])
                        };
                        if (valorEscala.descripcion.Contains("conceptual"))
                        { valorEscala.idEscalaCalificacion = (int)enumEscalasCalificaciones.Conceptual; }
                        else { valorEscala.idEscalaCalificacion = (int)enumEscalasCalificaciones.Numerica; }
                        listEscalaCalificacion.Add(valorEscala);
                    }
                    command.Connection.Close();
                    return listEscalaCalificacion;
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerValoresEscalaCalificacion()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerValoresEscalaCalificacion()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerValoresEscalaCalificacion()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Obteners the cargos personal BD transaccional.
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public List<CargoPersonal> obtenerCargosPersonalBDTransaccional(Configuraciones configuracion)
        {
            List<CargoPersonal> listaCargosPersonal = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandText = @"SELECT  *
                                            FROM vw_cargos";
                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    CargoPersonal cargo;
                    listaCargosPersonal = new List<CargoPersonal>();
                    while (reader.Read())
                    {
                        cargo = new CargoPersonal()
                        {
                            idCargo = 0,
                            idCargoTransaccional = (int)reader["id"],
                            nombre = reader["nombre"].ToString(),
                            descripcion = reader["descripcion"].ToString(),
                            activo = (bool)reader["activo"]
                        };
                        listaCargosPersonal.Add(cargo);
                    }
                    command.Connection.Close();
                    return listaCargosPersonal;
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerCargosPersonalBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerCargosPersonalBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerCargosPersonalBDTransaccional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Obteners the personal.
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public List<Personal> obtenerPersonalBDTransaccional(Configuraciones configuracion)
        {
            List<Personal> listPersonal = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandText = @"SELECT  *
                                            FROM vw_personal";
                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    Personal personal;
                    listPersonal = new List<Personal>();
                    while (reader.Read())
                    {
                        personal = new Personal()
                        {
                            IdPersonal = 0,
                            IdPersonalTransaccional = (int)reader["id"],
                            nombre = reader["nombre"].ToString(),
                            apellido = reader["apellido"].ToString(),
                            numeroDocumento = (int)reader["nro_documento"],
                            idTipoDocumento = (int)reader["fk_id_tipo_documento"],
                            fechaAlta = (DateTime)reader["fechaIngreso"],
                            activo = (bool)reader["activo"]
                        };
                        personal.cargo = new CargoPersonal();
                        personal.cargo.idCargoTransaccional = (int)reader["fk_id_cargo"];
                        listPersonal.Add(personal);
                    }
                    command.Connection.Close();
                    return listPersonal;
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerPersonal()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerPersonal()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerPersonal()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Obteners the docentes BD transaccional.
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public List<Docente> obtenerDocenteBDTransaccional(Configuraciones configuracion)
        {
            List<Docente> listaDocente = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandText = @"SELECT  *
                                            FROM vw_docentes";
                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    Docente docente;
                    listaDocente = new List<Docente>();
                    while (reader.Read())
                    {
                        docente = new Docente()
                        {
                            idDocente = 0,
                            idDocenteTransaccional = (int)reader["id"],
                            nombre = reader["nombre"].ToString(),
                            apellido = reader["apellido"].ToString(),
                            numeroDocumento = Convert.ToInt32(reader["nro_documento"]),
                            idTipoDocumento = (int)reader["fk_tipodocumento_id"],
                            activo = Convert.ToBoolean(reader["activo"]),
                            sexo = reader["sexo"].ToString(),
                            fechaNacimiento = (DateTime)reader["fecha_nacimiento"],
                            domicilio = reader["direccion"].ToString(),
                            localidad = new Localidades() { nombre = reader["ciudad"].ToString() },
                            email = reader["email"].ToString(),
                            telefonoFijo = reader["telefono"].ToString(),
                            telefonoCelular = reader["telefono_movil"].ToString(),
                            cargo = new CargoPersonal(),
                        };
                        docente.cargo.idCargo = (int)enumCargosPersonal.Docente;
                        docente.cargo.idCargoTransaccional = 6;
                        listaDocente.Add(docente);
                    }
                    command.Connection.Close();
                    return listaDocente;
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerDocenteBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerDocenteBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerDocenteBDTransaccional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Obteners the asignatura BD transaccional.
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public List<Asignatura> obtenerAsignaturaBDTransaccional(Configuraciones configuracion)
        {
            List<Asignatura> listaAsignaturas = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandText = @"SELECT  *
                                            FROM vw_materias";
                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    Asignatura asignatura;
                    listaAsignaturas = new List<Asignatura>();
                    while (reader.Read())
                    {
                        asignatura = new Asignatura()
                        {
                            idAsignaturaTransaccional = (int)reader["id"],
                            nombre = reader["nombre"].ToString(),
                        };
                        listaAsignaturas.Add(asignatura);
                    }
                    command.Connection.Close();
                    return listaAsignaturas;
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerAsignaturaBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerAsignaturaBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerAsignaturaBDTransaccional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Obteners the ciclo lectivo BD transaccional.
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public List<CicloLectivo> obtenerCicloLectivoBDTransaccional(Configuraciones configuracion)
        {
            List<CicloLectivo> listaCicloLectivo = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandText = @"SELECT  *
                                            FROM vw_ciclolectivo";
                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    CicloLectivo cicloLectivo;
                    listaCicloLectivo = new List<CicloLectivo>();
                    while (reader.Read())
                    {
                        cicloLectivo = new CicloLectivo()
                        {
                            idCicloLectivoTransaccional = (int)reader["id"],
                            nombre = reader["descripcion"].ToString(),
                            fechaInicio = (DateTime)reader["fecha_inicio"],
                            fechaFin = (DateTime)reader["fecha_fin"],
                            activo = Convert.ToBoolean(reader["actual"])
                        };
                        listaCicloLectivo.Add(cicloLectivo);
                    }
                    command.Connection.Close();
                    return listaCicloLectivo;
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerCicloLectivoBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerCicloLectivoBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerCicloLectivoBDTransaccional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Obteners the niveles BD transaccional.
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public List<Nivel> obtenerNivelesBDTransaccional(Configuraciones configuracion)
        {
            List<Nivel> listaNiveles = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandText = @"SELECT  *
                                            FROM vw_nivel";
                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    Nivel nivel;
                    listaNiveles = new List<Nivel>();
                    while (reader.Read())
                    {
                        nivel = new Nivel()
                        {
                            idNivelTransaccional = (int)reader["id"],
                            nombre = reader["descripcion"].ToString()
                        };
                        listaNiveles.Add(nivel);
                    }
                    command.Connection.Close();
                    return listaNiveles;
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerNivelesBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerNivelesBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerNivelesBDTransaccional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Obteners the cursos BD transaccional.
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public List<Curso> obtenerCursosBDTransaccional(Configuraciones configuracion)
        {
            List<Curso> listaCursos = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandText = @"SELECT  *
                                            FROM vw_curso";
                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    Curso curso;
                    listaCursos = new List<Curso>();
                    while (reader.Read())
                    {
                        curso = new Curso()
                        {
                            idCursoTransaccional = (int)reader["id"],
                            nombre = reader["descripcion"].ToString(),
                            nivel = new Nivel() { idNivelTransaccional = (int)reader["fk_anio_id"] }
                        };
                        listaCursos.Add(curso);
                    }
                    command.Connection.Close();
                    return listaCursos;
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerCursosBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerCursosBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerCursosBDTransaccional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Obteners the orientaciones BD transaccional.
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public List<Orientacion> obtenerOrientacionesBDTransaccional(Configuraciones configuracion)
        {
            List<Orientacion> listaOrientaciones = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandText = @"SELECT  *
                                            FROM vw_orientaciones";
                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    Orientacion orientacion;
                    listaOrientaciones = new List<Orientacion>();
                    while (reader.Read())
                    {
                        orientacion = new Orientacion()
                        {
                            idOrientacionTransaccional = (int)reader["id"],
                            nombre = reader["nombre"].ToString(),
                            descripcion = reader["descripcion"].ToString()
                        };
                        listaOrientaciones.Add(orientacion);
                    }
                    command.Connection.Close();
                    return listaOrientaciones;
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerOrientacionesBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerOrientacionesBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerOrientacionesBDTransaccional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Obteners the asignaturas curso BD transaccional.
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public List<Asignatura> obtenerAsignaturasCursoBDTransaccional(Configuraciones configuracion)
        {
            List<Asignatura> listaAsignaturas = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandText = @"SELECT 
                                                fk_anio_id as 'idNivel',
                                                fk_actividad_id as 'idAsignatura',
                                                fk_docente_id as 'idDocente',
                                                fk_orientacion_id as 'idOrientacion'
                                             FROM vw_asignatura_curso";
                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    Asignatura asignatura;
                    listaAsignaturas = new List<Asignatura>();
                    while (reader.Read())
                    {
                        asignatura = new Asignatura()
                        {
                            idAsignatura = (int)reader["idAsignatura"],
                            docente = new Docente() { idDocente = (int)reader["idDocente"] },
                            curso = new Curso() { nivel = new Nivel() { idNivel = (int)reader["idNivel"] } }
                        };
                        listaAsignaturas.Add(asignatura);
                    }
                    command.Connection.Close();
                    return listaAsignaturas;
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerAsignaturasCursoBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerAsignaturasCursoBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerAsignaturasCursoBDTransaccional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Obteners the tutores BD transaccional.
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public List<Tutor> obtenerTutoresBDTransaccional(Configuraciones configuracion)
        {
            List<Tutor> listaTutores = null;
            MySqlDataReader reader = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandText = @"SELECT * FROM vw_tutores";
                    conMySQL.Open();

                    reader = command.ExecuteReader();
                    Tutor tutor;
                    listaTutores = new List<Tutor>();
                    while (reader.Read())
                    {
                        tutor = new Tutor();
                        tutor.idPersona = 0;
                        tutor.idTutorTransaccional = Convert.ToInt32(reader["id"]);
                        tutor.nombre = reader["nombre"].ToString();
                        tutor.apellido = reader["apellido"].ToString();
                        tutor.numeroDocumento = Convert.ToInt32(reader["nro_documento"].ToString().Replace("M", "").Replace(".",""));
                        tutor.idTipoDocumento = Convert.ToInt32(reader["fk_tipodocumento_id"]);
                        tutor.domicilio = reader["direccion"].ToString();
                        tutor.sexo = reader["sexo"].ToString();
                        tutor.telefonoFijo = reader["telefono"].ToString();
                        tutor.telefonoCelular = reader["telefono_movil"].ToString();
                        tutor.email = reader["email"].ToString();
                        tutor.activo = true;

                        // Aca tengo que obtener la instancia de Tipo Tutor que se corresponda a ese ID
                        tutor.tipoTutor = new TipoTutor();
                        tutor.tipoTutor.idTipoTutorTransaccional = Convert.ToInt32(reader["fk_rolresponsable_id"]);                        

                        tutor.localidad = new Localidades() { nombre = reader["ciudad"].ToString() };

                        listaTutores.Add(tutor);
                    }
                    command.Connection.Close();
                    return listaTutores;
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerTutoresBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerTutoresBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerTutoresBDTransaccional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Obteners the periodos BD transaccional.
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public List<Periodo> obtenerPeriodosBDTransaccional(Configuraciones configuracion)
        {
            List<Periodo> listaPeriodo = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandText = @"SELECT * 
                                            FROM vw_periodo";
                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    Periodo periodo;
                    listaPeriodo = new List<Periodo>();
                    while (reader.Read())
                    {
                        periodo = new Periodo()
                        {
                            idPeriodo = 0,

                            idPeriodoTransaccional = (int)reader["id"],
                            nombre = reader["descripcion"].ToString(),
                            fechaInicio = Convert.ToDateTime(reader["fecha_inicio"]),
                            fechaFin = Convert.ToDateTime(reader["fecha_fin"]),
                            cicloLectivo = new CicloLectivo() { idCicloLectivoTransaccional = (int)reader["fk_ciclolectivo_id"] }
                        };
                        listaPeriodo.Add(periodo);
                    }
                    command.Connection.Close();
                    return listaPeriodo;
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerPeriodosBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerPeriodosBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerPeriodosBDTransaccional()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                //if (sqlConnectionConfig.State == ConnectionState.Open)
                //    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Obteners the asistencia BD transaccional.
        /// </summary>
        /// <param name="configuracion">The configuracion.</param>
        /// <returns></returns>
        public List<Asistencia> obtenerAsistenciaBDTransaccional(Configuraciones configuracion)
        {                      //obtenerAsistenciaBDTransaccional
            List<Asistencia> listadoAsistencia = null;
            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    conMySQL = new MySqlConnection(configuracion.valor);
                    command.Connection = conMySQL;

                    command.CommandText = @"SELECT * 
                                            FROM Asistencia";
                    conMySQL.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    Asistencia unaAsistencia;
                    listadoAsistencia = new List<Asistencia>();
                    while (reader.Read())
                    {
                        unaAsistencia = new Asistencia();

                        unaAsistencia.idAsistencia = 0;
                        unaAsistencia.idAsistenciaTransaccional = (int)reader["id"];
                        unaAsistencia.fecha = Convert.ToDateTime(reader["fecha"]);
                        unaAsistencia.tipoAsistencia = new TipoAsistencia();
                        unaAsistencia.tipoAsistencia.idTipoAsistenciaTransaccional = (int)reader["fk_tipoasistencia_id"];
                        unaAsistencia.unAlumno = new Alumno();
                        unaAsistencia.unAlumno.idAlumno = (int)reader["fk_alumno_id"];

                        listadoAsistencia.Add(unaAsistencia);
                    }
                    command.Connection.Close();
                    return listadoAsistencia;
                }
            }
            catch (MySqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerAsistenciaBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerAsistenciaBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerAsistenciaBDTransaccional()", ClassName),
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
                                            FROM tipoasistencia";
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
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerAsistenciaBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerAsistenciaBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerAsistenciaBDTransaccional()", ClassName),
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
                                            FROM vw_tipoSancion";
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
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerTipoAsistenciaBDTransaccional()", ClassName),
                                        ex, enuExceptionType.MySQLException);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obtenerTipoSancionBDTransaccional()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - obteneTipoSancionBDTransaccional()", ClassName),
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
                                            FROM vw_tipoTutor";
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
                                            FROM vw_motivosancion";
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
                                            FROM vw_sancion";
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
                                            FROM vw_tutores_alumnos";
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

        #endregion
    }
}
