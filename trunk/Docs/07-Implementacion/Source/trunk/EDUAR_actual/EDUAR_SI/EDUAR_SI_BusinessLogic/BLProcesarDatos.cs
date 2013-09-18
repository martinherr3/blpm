using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDUAR_Entities;
using EDUAR_SI_DataAccess;

namespace EDUAR_SI_BusinessLogic
{
    public class BLProcesarDatos
    {
        #region --[Atributos]--
        Configuraciones objConfiguracion;
        DAObtenerDatos objDAObtenerDatos;
        DAStoreDatos objDAStoreDatos;
        #endregion

        #region --[Propiedades]--
        #endregion

        #region --[Constructores]--
        /// <summary>
        /// Constructor. LLama al constructor de la clase base BLProcesoBase.
        /// </summary>
        /// <param name="connectionString">Cadena de conexión a la base de datos.</param>
        public BLProcesarDatos(String connectionString)
        {
            objDAObtenerDatos = new DAObtenerDatos(connectionString);
        }
        #endregion

        #region --[Métodos Públicos]--
        public List<Curso> obtenerCursosBDTransaccional(Configuraciones configuracion)
        {
            try
            {
                objDAObtenerDatos = new DAObtenerDatos(configuracion.valor);
                List<Curso> listaCursos = new List<Curso>();
                listaCursos = objDAObtenerDatos.obtenerCursosBDTransaccional(configuracion);

                return listaCursos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Alumno> obtenerAlumnosCursoCicloLectivoActual(Configuraciones configuracion, int selectedCurso)
        {
            try
            {
                objDAObtenerDatos = new DAObtenerDatos(configuracion.valor);
                List<Alumno> listaAlumnos = new List<Alumno>();
                listaAlumnos = objDAObtenerDatos.obtenerAlumnosCursoCicloLectivoActual(configuracion, selectedCurso);

                return listaAlumnos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Asignatura> obtenerAsignaturasCursoCicloLectivoActual(Configuraciones configuracion, int selectedCurso)
        {
            try
            {
                objDAObtenerDatos = new DAObtenerDatos(configuracion.valor);
                List<Asignatura> listaAsignaturas = new List<Asignatura>();
                listaAsignaturas = objDAObtenerDatos.obtenerAsignaturaCursoCicloLectivoActual(configuracion, selectedCurso);

                return listaAsignaturas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MotivoSancion> obtenerMotivoSancionBDTransaccional(Configuraciones configuracion)
        {
            try
            {
                objDAObtenerDatos = new DAObtenerDatos(configuracion.valor);
                List<MotivoSancion> listaMotivosSancion = new List<MotivoSancion>();
                listaMotivosSancion = objDAObtenerDatos.obtenerMotivoSancionBDTransaccional(configuracion);

                return listaMotivosSancion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoSancion> obtenerTipoSancionBDTransaccional(Configuraciones configuracion)
        {
            try
            {
                objDAObtenerDatos = new DAObtenerDatos(configuracion.valor);
                List<TipoSancion> listaTiposSancion = new List<TipoSancion>();
                listaTiposSancion = objDAObtenerDatos.obtenerTipoSancionBDTransaccional(configuracion);

                return listaTiposSancion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void registrarSancionBDTransaccional(Configuraciones configuracion, Sancion unaSancion)
        {
            try
            {
                objDAStoreDatos = new DAStoreDatos(configuracion.valor);

                objDAStoreDatos.registrarSancionBDTransaccional(configuracion, unaSancion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void registrarAsistenciaBDTransaccional(Configuraciones configuracion, Asistencia unaAsistencia)
        {
            try
            {
                objDAStoreDatos = new DAStoreDatos(configuracion.valor);

                objDAStoreDatos.registrarAsistenciaBDTransaccional(configuracion, unaAsistencia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region --[Métodos Privados]--

        #endregion
    }
}
