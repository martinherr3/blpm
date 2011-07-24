using System;
using System.Collections.Generic;
using EDUAR_SI_DataAccess;
using EDUAR_Entities;
using EDUAR_Utility.Enumeraciones;
using System.Data.SqlClient;
using EDUAR_Utility.Excepciones;
using System.Data;
using EDUAR_Entities.Reports;

namespace EDUAR_SI_BusinessLogic
{
    /// <summary>
    /// Clase encargada de la obtención de los datos desde la BD decisional.
    /// </summary>
    public class BLObtenerDatos : DABase
    {

        #region --[Atributos]--
        private const string ClassName = "BLObtenerDatos";
        Configuraciones objConfiguracion;

        static DataTable dataTable = new DataTable();

        #endregion

        #region --[Propiedades]--
        #endregion

        #region --[Constructores]--
        public BLObtenerDatos(String connectionString)
            : base(connectionString)
        {

        }
        #endregion

        #region --[Métodos Privados]--
        #endregion

        #region --[Métodos Públicos]--

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
        /// Obtiene una coleccion de calificaciones de un determinado alumno y en un determinado periodo para una asignatura dada
        /// Related User Stories: 26, 27
        /// </summary>
        public List<CalificacionesAlumnoPeriodo> obtenerCalificacionesAlumnoPeriodo(Alumno unAlumno, Periodo unPeriodo)
        {
            List<CalificacionesAlumnoPeriodo> listaCalificaciones = new List<CalificacionesAlumnoPeriodo>();

            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    if (sqlConnectionConfig.State == ConnectionState.Closed) sqlConnectionConfig.Open();

                    command.Connection = sqlConnectionConfig;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "CalificacionesPeriodoAlumno_Select";
                    command.CommandTimeout = 10;

                    CalificacionesAlumnoPeriodo unaCalificacion = null;

                    int idAlumno = (int)unAlumno.idAlumno;
                    int periodo = unPeriodo.fechaInicio.Year;

                    command.Parameters.AddWithValue("@idAlumno", idAlumno);
                    command.Parameters.AddWithValue("@periodo", periodo);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        unaCalificacion = new CalificacionesAlumnoPeriodo();

                        unaCalificacion.nombreAlumno = reader["nombre"].ToString();
                        unaCalificacion.curso = reader["curso"].ToString();
                        unaCalificacion.fecha = Convert.ToDateTime(reader["fecha"].ToString());
                        unaCalificacion.asignatura = reader["asignatura"].ToString();
                        unaCalificacion.calificacion = reader["calificación"].ToString();
                        unaCalificacion.instancia = reader["instancia"].ToString();

                        listaCalificaciones.Add(unaCalificacion);
                    }

                    reader.Close();
                    command.Connection.Close();
                    return listaCalificaciones;
                }
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - ObtenerCalificacionesAlumnoPeriodo()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - ObtenerCalificacionesAlumnoPeriodo()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                if (sqlConnectionConfig.State == ConnectionState.Open)
                    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Obtiene una coleccion de inasistencias de un determinado alumno en un determinado periodo
        /// Related User Stories: 54
        /// </summary>
        public List<InasistenciasAlumnoPeriodo> obtenerInasistenciasAlumnoPeriodo(Alumno unAlumno, Periodo unPeriodo)
        {
            List<InasistenciasAlumnoPeriodo> listaInasistenciasAlumno = new List<InasistenciasAlumnoPeriodo>();

            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    if (sqlConnectionConfig.State == ConnectionState.Closed) sqlConnectionConfig.Open();

                    command.Connection = sqlConnectionConfig;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "InasistenciasPeriodoAlumno_Select";
                    command.CommandTimeout = 10;

                    int idAlumno = (int)unAlumno.idAlumno;
                    int periodo = unPeriodo.fechaInicio.Year;

                    command.Parameters.AddWithValue("@idAlumno", idAlumno);
                    command.Parameters.AddWithValue("@periodo", periodo);

                    SqlDataReader reader = command.ExecuteReader();

                    InasistenciasAlumnoPeriodo unObjeto = null;

                    while (reader.Read())
                    {
                        unObjeto = new InasistenciasAlumnoPeriodo();
                        
                        unObjeto.nombreAlumno = reader["nombre"].ToString();
                        unObjeto.fechaInasistencia = Convert.ToDateTime(reader["fecha"]);
                        unObjeto.motivoInasistencia = reader["descripcion"].ToString();
                        
                        listaInasistenciasAlumno.Add(unObjeto);
                    }
                    reader.Close();
                    command.Connection.Close();
                    return listaInasistenciasAlumno;
                }
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - ObtenerInasistenciasAlumnoPeriodo()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - ObtenerInasistenciasAlumnoPeriodo()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                if (sqlConnectionConfig.State == ConnectionState.Open)
                    sqlConnectionConfig.Close();
            }
        }

        /// <summary>
        /// Obtiene una coleccion de sanciones de un determinado alumno en un determinado periodo
        /// Related User Stories: 53, 55
        /// </summary>
        public List<SancionesAlumnoPeriodo> obtenerSancionesAlumnoPeriodo(Alumno unAlumno, Periodo unPeriodo)
        {
            List<SancionesAlumnoPeriodo> listaSanciones = new List<SancionesAlumnoPeriodo>();

            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    if (sqlConnectionConfig.State == ConnectionState.Closed) sqlConnectionConfig.Open();

                    command.Connection = sqlConnectionConfig;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "SancionesPeriodoAlumno_Select";
                    command.CommandTimeout = 10;
                    
                    int idAlumno = (int)unAlumno.idAlumno;
                    int periodo = unPeriodo.fechaInicio.Year;

                    command.Parameters.AddWithValue("@idAlumno", idAlumno);
                    command.Parameters.AddWithValue("@periodo", periodo);

                    SqlDataReader reader = command.ExecuteReader();

                    SancionesAlumnoPeriodo unaSancion = null;

                    while (reader.Read())
                    {
                        unaSancion = new SancionesAlumnoPeriodo();
                        
                        unaSancion.nombreAlumno = reader["nombre"].ToString();
                        unaSancion.fechaSancion = Convert.ToDateTime(reader["fecha"]);
                        unaSancion.cantidad = Convert.ToInt32(reader["cantidad"].ToString());
                        unaSancion.tipoSancion = reader["tipo"].ToString();
                        unaSancion.motivoSancion = reader["motivo"].ToString();

                        listaSanciones.Add(unaSancion);
                    }

                    reader.Close();
                    command.Connection.Close();
                    return listaSanciones;
                }
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - ObtenerSancionesAlumnoPeriodo()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - ObtenerSancionesAlumnoPeriodo()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
            finally
            {
                if (sqlConnectionConfig.State == ConnectionState.Open)
                    sqlConnectionConfig.Close();
            }
        }


        #endregion

    }
}
