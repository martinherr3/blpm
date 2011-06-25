using System;
using System.Collections.Generic;
using EDUAR_Entities;
using EDUAR_SI_DataAccess;
using EDUAR_Utility.Enumeraciones;
using System.Data.SqlClient;

namespace EDUAR_SI_BusinessLogic
{
    public class BLImportarDatos : BLProcesoBase
    {
        #region --[Atributos]--
        Configuraciones objConfiguracion;

        DAImportarDatos objDAImportarDatos;

        DAObtenerDatos objDAObtenerDatos;

        #endregion

        #region --[Propiedades]--
        #endregion

        #region --[Constructores]--
        /// <summary>
        /// Constructor. LLama al constructor de la clase base BLProcesoBase.
        /// </summary>
        /// <param name="connectionString">Cadena de conexión a la base de datos.</param>
        public BLImportarDatos(String connectionString)
            : base(connectionString)
        {

        }
        #endregion

        #region --[Métodos Públicos]--
        /// <summary>
        /// Procedimientoes the importar datos.
        /// </summary>
        public void ProcedimientoImportarDatos()
        {
            try
            {
                objDAImportarDatos = new DAImportarDatos(ConnectionString);
                objConfiguracion = objDAImportarDatos.ObtenerConfiguracion(enumConfiguraciones.BaseDeDatosOrigen);
                ImportarDatos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region --[Métodos Privados]--
        /// <summary>
        /// Importars the datos.
        /// </summary>
        private void ImportarDatos()
        {
            try
            {
                objDAObtenerDatos = new DAObtenerDatos(objConfiguracion.valor);

                objDAImportarDatos.GrabarPais(objDAObtenerDatos.obtenerPaisesBDTransaccional(objConfiguracion));

                objDAImportarDatos.GrabarProvincia(objDAObtenerDatos.obtenerProvinciasBDTransaccional(objConfiguracion));

                objDAImportarDatos.GrabarLocalidad(objDAObtenerDatos.obtenerLocalidadesBDTransaccional(objConfiguracion));

                objDAImportarDatos.GrabarTipoDocumento(objDAObtenerDatos.obtenerTipoDocumentoBDTransaccional(objConfiguracion));

                //User Story 140
                objDAImportarDatos.GrabarValoresEscalasCalificaciones(objDAObtenerDatos.obtenerValoresEscalaCalificacionBDTransaccional(objConfiguracion));

                //User Story 141
                GrabarAlumno();

                //User Story 142
                GrabarPersonal();

                GrabarDocente();

                //User Story 143
                objDAImportarDatos.GrabarAsignatura(objDAObtenerDatos.obtenerAsignaturaBDTransaccional(objConfiguracion));

                objDAImportarDatos.GrabarCicloLectivo(objDAObtenerDatos.obtenerCicloLectivoBDTransaccional(objConfiguracion));

                objDAImportarDatos.GrabarNivel(objDAObtenerDatos.obtenerNivelesBDTransaccional(objConfiguracion));

                objDAImportarDatos.GrabarCursos(objDAObtenerDatos.obtenerCursosBDTransaccional(objConfiguracion));

                objDAImportarDatos.GrabarOrientacion(objDAObtenerDatos.obtenerOrientacionesBDTransaccional(objConfiguracion));

                objDAImportarDatos.GrabarAsignaturaCurso(objDAObtenerDatos.obtenerAsignaturasCursoBDTransaccional(objConfiguracion));
                
                objDAImportarDatos.GrabarPeriodo(objDAObtenerDatos.obtenerPeriodosBDTransaccional(objConfiguracion));



                //List<Periodo> lista = objDAObtenerDatos.obtenerPeriodosBDTransaccional(objConfiguracion);

                objDAImportarDatos.GrabarMotivoAusencia(objDAObtenerDatos.obtenerMotivosAusenciaBDTransaccional(objConfiguracion));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Grabars the docente.
        /// </summary>
        private void GrabarDocente()
        {
            SqlTransaction transaccion = null;
            try
            {
                List<Docente> listaDocentes = objDAObtenerDatos.obtenerDocenteBDTransaccional(objConfiguracion);
               //SqlTransaction transaccion;
                Persona persona = null;
                foreach (Docente docente in listaDocentes)
                {
                    persona = new Persona()
                    {
                        idPersona = 0,
                        nombre = docente.nombre,
                        apellido = docente.apellido,
                        numeroDocumento = docente.numeroDocumento,
                        idTipoDocumento = docente.idTipoDocumento,
                        activo = docente.activo,
                        fechaNacimiento = docente.fechaNacimiento,
                        domicilio = docente.domicilio,
                        email = docente.email,
                        telefonoCelular = docente.telefonoCelular,
                        telefonoFijo = docente.telefonoFijo,
                        localidad = new Localidades() { nombre = docente.localidad.nombre },
                        sexo = docente.sexo,
                    };
                    docente.idPersona = objDAImportarDatos.GrabarPersona(persona, ref transaccion);
                    Personal personal = new Personal()
                    {
                        fechaAlta = docente.fechaAlta,
                        idPersona = docente.idPersona,
                        IdPersonalTransaccional = (int)docente.idDocenteTransaccional,
                        legajo = docente.legajo,
                        cargo = new CargoPersonal() { idCargo = docente.cargo.idCargo, idCargoTransaccional = docente.cargo.idCargoTransaccional },
                        activo = docente.activo,
                    };
                    objDAImportarDatos.GrabarPersonal(personal, ref transaccion);
                }
                transaccion.Commit();
            }
            catch (Exception ex)
            {
                if (transaccion != null)
                    transaccion.Rollback();
                throw ex;
            }
        }

        /// <summary>
        /// Grabars the personal.
        /// </summary>
        private void GrabarPersonal()
        {
            SqlTransaction transaccion = null;
            try
            {
                //Primero actualiza los cargos
                objDAImportarDatos.GrabarCargoPersonal(objDAObtenerDatos.obtenerCargosPersonalBDTransaccional(objConfiguracion));

                //Busca e inserta o actualiza el personal
                List<Personal> listaPersonal = objDAObtenerDatos.obtenerPersonalBDTransaccional(objConfiguracion);
                Persona persona = null;
                foreach (Personal personal in listaPersonal)
                {
                    persona = new Persona()
                    {
                        idPersona = 0,
                        nombre = personal.nombre,
                        apellido = personal.apellido,
                        numeroDocumento = personal.numeroDocumento,
                        idTipoDocumento = personal.idTipoDocumento,
                        activo = personal.activo,
                        fechaNacimiento = null,
                        localidad = new Localidades()
                    };
                    personal.idPersona = objDAImportarDatos.GrabarPersona(persona, ref transaccion);
                    objDAImportarDatos.GrabarPersonal(personal, ref transaccion);
                }
                transaccion.Commit();
            }
            catch (Exception ex)
            {
                if (transaccion != null)
                    transaccion.Rollback();
                throw ex;
            }
        }

        /// <summary>
        /// Grabars the alumno.
        /// </summary>
        private void GrabarAlumno()
        {
            SqlTransaction transaccion = null;
            try
            {
                List<Alumno> listaAlumnos = objDAObtenerDatos.obtenerAlumnoBDTransaccional(objConfiguracion);
                Persona persona = null;
                foreach (Alumno alumno in listaAlumnos)
                {
                    persona = new Persona()
                    {
                        idPersona = 0,
                        nombre = alumno.nombre,
                        apellido = alumno.apellido,
                        numeroDocumento = alumno.numeroDocumento,
                        idTipoDocumento = alumno.idTipoDocumento,
                        domicilio = alumno.domicilio,
                        localidad = new Localidades() { nombre = alumno.localidad.nombre },
                        sexo = alumno.sexo,
                        fechaNacimiento = alumno.fechaNacimiento,
                        telefonoFijo = alumno.telefonoFijo,
                        telefonoCelular = alumno.telefonoCelular,
                        telefonoCelularAlternativo = alumno.telefonoCelularAlternativo,
                        email = alumno.email,
                        activo = alumno.activo,
                        barrio = alumno.barrio
                    };
                    if (string.IsNullOrEmpty(alumno.barrio)) persona.barrio = string.Empty;
                    alumno.idPersona = objDAImportarDatos.GrabarPersona(persona, ref transaccion);
                    objDAImportarDatos.GrabarAlumno(alumno, ref transaccion);
                }
                transaccion.Commit();
            }
            catch (Exception ex)
            {
                if (transaccion != null)
                    transaccion.Rollback();
                throw ex;
            }
        }
        #endregion
    }
}
