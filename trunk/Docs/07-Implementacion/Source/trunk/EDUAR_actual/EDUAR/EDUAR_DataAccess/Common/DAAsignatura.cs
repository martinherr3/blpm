using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System;
using System.Data;
using System.Data.SqlClient;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;
using System.Collections.Generic;

namespace EDUAR_DataAccess.Common
{
    public class DAAsignatura : DataAccesBase<Asignatura>
    {
        #region --[Atributos]--
        private const string ClassName = "DAAsignatura";
        #endregion

        #region --[Constructor]--
        public DAAsignatura()
        {
        }

        public DAAsignatura(DATransaction objDATransaction)
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

        public override Asignatura GetById(Asignatura entidad)
        {
            throw new NotImplementedException();
            //{
            //    Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Personas_Select");

            //    if (entidad.idPersona > 0)
            //        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPersona", DbType.Int32, entidad.idPersona);

            //    IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

            //    Persona objPersona = new Persona();
            //    while (reader.Read())
            //    {
            //        objPersona.idPersona = Convert.ToInt32(reader["idPersona"]);
            //        objPersona.nombre = reader["nombre"].ToString();
            //        objPersona.apellido = reader["apellido"].ToString();
            //        objPersona.numeroDocumento = Convert.ToInt32(reader["numeroDocumento"]);
            //        objPersona.idTipoDocumento = Convert.ToInt32(reader["idTipoDocumento"]);
            //        objPersona.domicilio = reader["domicilio"].ToString();
            //        objPersona.barrio = reader["barrio"].ToString();
            //        if (!string.IsNullOrEmpty(reader["idLocalidad"].ToString()))
            //            objPersona.localidad = new Localidades() { idLocalidad = Convert.ToInt32(reader["idLocalidad"]) };
            //        objPersona.sexo = reader["sexo"].ToString();
            //        if (!string.IsNullOrEmpty(reader["fechaNacimiento"].ToString()))
            //            objPersona.fechaNacimiento = Convert.ToDateTime(reader["fechaNacimiento"]);
            //        objPersona.telefonoFijo = reader["telefonoFijo"].ToString();
            //        objPersona.telefonoCelular = reader["telefonoCelular"].ToString();
            //        objPersona.telefonoCelularAlternativo = reader["telefonoCelularAlternativo"].ToString();
            //        objPersona.email = reader["email"].ToString();
            //        objPersona.activo = Convert.ToBoolean(reader["activo"]);
            //        objPersona.username = reader["username"].ToString();
            //        objPersona.idTipoPersona = Convert.ToInt32(reader["idTipoPersona"]);
            //    }
            //    return objPersona;
            //}
            //catch (SqlException ex)
            //{
            //    throw new CustomizedException(string.Format("Fallo en {0} - GetPersonas()", ClassName),
            //                        ex, enuExceptionType.SqlException);
            //}
            //catch (Exception ex)
            //{
            //    throw new CustomizedException(string.Format("Fallo en {0} - GetPersonas()", ClassName),
            //                        ex, enuExceptionType.DataAccesException);
            //}
        }

        public override void Create(Asignatura entidad)
        {
            throw new NotImplementedException();

        }

        public override void Create(Asignatura entidad, out int identificador)
        {
            throw new NotImplementedException();
            //try
            //{
            //    Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Asignaturas_Insert");

            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignatura", DbType.Int32, 0);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPagina", DbType.Int32, 0);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fecha", DbType.Date, entidad.fecha.Date.ToShortDateString());
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@hora", DbType.Time, entidad.hora.ToShortTimeString());
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.usuario);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@url", DbType.String, entidad.pagina.url);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@titulo", DbType.String, entidad.pagina.titulo);

            //    if (Transaction.Transaction != null)
            //        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
            //    else
            //        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

            //    identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idAsignatura"].Value.ToString());

            //}
            //catch (SqlException ex)
            //{
            //    throw new CustomizedException(string.Format("Fallo en {0} - Create()", ClassName),
            //                        ex, enuExceptionType.SqlException);
            //}
            //catch (Exception ex)
            //{
            //    throw new CustomizedException(string.Format("Fallo en {0} - Create()", ClassName),
            //                        ex, enuExceptionType.DataAccesException);
            //}
        }

        public override void Update(Asignatura entidad)
        {
            throw new NotImplementedException();
            //try
            //{
            //    Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Personas_Update");

            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPersona", DbType.Int32, entidad.idPersona);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@nombre", DbType.String, entidad.nombre);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@apellido", DbType.String, entidad.apellido);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@numeroDocumento", DbType.Int32, entidad.numeroDocumento);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoDocumento", DbType.Int32, entidad.idTipoDocumento);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@domicilio", DbType.String, entidad.domicilio);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@barrio", DbType.String, entidad.barrio);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idLocalidad", DbType.String, entidad.localidad.idLocalidad);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@sexo", DbType.String, entidad.sexo);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@fechaNacimiento", DbType.Date, entidad.fechaNacimiento);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@telefonoFijo", DbType.String, entidad.telefonoFijo);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@telefonoCelular", DbType.String, entidad.telefonoCelular);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@telefonoCelularAlternativo", DbType.String, entidad.telefonoCelularAlternativo);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@email", DbType.String, entidad.email);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.String, entidad.activo);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, entidad.username);
            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idTipoPersona", DbType.Int32, entidad.idTipoPersona);

            //    if (Transaction.Transaction != null)
            //        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
            //    else
            //        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
            //}
            //catch (SqlException ex)
            //{
            //    if (ex.Number == BLConstantesGenerales.ConcurrencyErrorNumber)
            //        throw new CustomizedException(string.Format(
            //               "No se puede modificar la Persona {0}, debido a que otro usuario lo ha modificado.",
            //               entidad.nombre + " " + entidad.apellido), ex, enuExceptionType.ConcurrencyException);

            //    throw new CustomizedException(string.Format("Fallo en {0} - Update()", ClassName),
            //                                          ex, enuExceptionType.SqlException);
            //}
            //catch (Exception ex)
            //{
            //    throw new CustomizedException(string.Format("Fallo en {0} - Update()", ClassName),
            //                                          ex, enuExceptionType.DataAccesException);
            //}
        }

        public override void Delete(Asignatura entidad)
        {
            throw new NotImplementedException();
            //try
            //{
            //    Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Personas_Delete");

            //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idPersona", DbType.Int32, 0);


            //    if (Transaction.Transaction != null)
            //        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
            //    else
            //        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

            //}
            //catch (SqlException ex)
            //{
            //    if (ex.Number == BLConstantesGenerales.ConcurrencyErrorNumber)
            //        throw new CustomizedException(string.Format(
            //               "No se puede eliminar la Persona {0}, debido a que otro usuario lo ha modificado.",
            //               entidad.nombre + " " + entidad.apellido), ex, enuExceptionType.ConcurrencyException);
            //    if (ex.Number == BLConstantesGenerales.IntegrityErrorNumber)
            //        throw new CustomizedException(string.Format("No se puede eliminar la Persona {0}, debido a que tiene registros asociados.",
            //                           entidad.nombre + " " + entidad.apellido), ex, enuExceptionType.IntegrityDataException);


            //    throw new CustomizedException(string.Format("Fallo en {0} - Delete()", ClassName),
            //                                           ex, enuExceptionType.SqlException);
            //}
            //catch (Exception ex)
            //{
            //    throw new CustomizedException(string.Format("Fallo en {0} - Delete()", ClassName),
            //                                           ex, enuExceptionType.DataAccesException);
            //}
        }
        #endregion

        #region --[Métodos Públicos]--
        /// <summary>
        /// Gets the asignaturas.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<Asignatura> GetAsignaturas(Asignatura entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Asignatura_Select");
                if (entidad != null)
                {
                    if (entidad.idAsignatura > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignatura", DbType.Int32, entidad.idAsignatura);
                    if (!string.IsNullOrEmpty(entidad.nombre))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@titulo", DbType.String, entidad.nombre);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<Asignatura> listaAsignaturas = new List<Asignatura>();
                Asignatura objAsignatura;
                while (reader.Read())
                {
                    objAsignatura = new Asignatura();

                    objAsignatura.idAsignatura = Convert.ToInt32(reader["idAsignatura"]);
                    objAsignatura.nombre = reader["nombre"].ToString();

                    listaAsignaturas.Add(objAsignatura);
                }
                return listaAsignaturas;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAsignaturas()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAsignaturas()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Gets the asignaturas curso.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<Asignatura> GetAsignaturasCurso(Asignatura entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("AsignaturaCicloLectivo_Select");
                if (entidad != null)
                {
                    if (entidad.idAsignatura > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignatura", DbType.Int32, entidad.idAsignatura);
                    if (!string.IsNullOrEmpty(entidad.nombre))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@titulo", DbType.String, entidad.nombre);
                    if (entidad.cursoCicloLectivo.idCursoCicloLectivo > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCursoCicloLectivo", DbType.Int32, entidad.cursoCicloLectivo.idCursoCicloLectivo);
                    if (entidad.curso.cicloLectivo.idCicloLectivo > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCicloLectivo", DbType.Int32, entidad.curso.cicloLectivo.idCicloLectivo);
                    if (entidad.docente != null && !string.IsNullOrEmpty(entidad.docente.username))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@usuario", DbType.String, entidad.docente.username);

                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<Asignatura> listaAsignaturas = new List<Asignatura>();
                Asignatura objAsignatura;
                while (reader.Read())
                {
                    objAsignatura = new Asignatura();
                    //Se asigna el idAsignaturaCurso de la tabla - SOLO CUANDO MANEJO ASIGNATURA - CURSO
                    objAsignatura.idAsignatura = Convert.ToInt32(reader["idAsignaturaCicloLectivo"]);
                    objAsignatura.nombre = reader["nombreAsignatura"].ToString();
                    objAsignatura.curso.idCurso = Convert.ToInt32(reader["idCursoCicloLectivo"]);
                    objAsignatura.curso.nombre = reader["nombreCurso"].ToString();
                    objAsignatura.docente.nombre = reader["nombreDocente"].ToString();
                    objAsignatura.docente.apellido = reader["apellidoDocente"].ToString();
                    objAsignatura.docente.idPersona = Convert.ToInt32(reader["idPersona"]);
                    listaAsignaturas.Add(objAsignatura);
                }
                return listaAsignaturas;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAsignaturasCurso()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAsignaturasCurso()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Gets the asignaturas por ciclo lectivo y nivel.
        /// </summary>
        /// <param name="asignatura">The entidad.</param>
        /// <returns></returns>
        public List<Asignatura> GetAsignaturasNivelCicloLectivo(int idCicloLectivo, int idNivel)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("AsignaturasPorNivelCicloLectivo_select");

                if (idNivel > 0)
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idNivel", DbType.Int32, idNivel);
                if (idCicloLectivo > 0)
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCicloLectivo", DbType.Int32, idCicloLectivo);

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<Asignatura> listaAsignaturas = new List<Asignatura>();
                Asignatura objAsignatura;
                while (reader.Read())
                {
                    objAsignatura = new Asignatura();

                    objAsignatura.idAsignatura = Convert.ToInt32(reader["idAsignatura"]);
                    objAsignatura.nombre = reader["nombreAsignatura"].ToString();

                    listaAsignaturas.Add(objAsignatura);
                }
                return listaAsignaturas;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAsignaturasNivelCicloLectivo()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAsignaturasNivelCicloLectivo()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Gets the asignaturas por ciclo lectivos y niveles multiples.
        /// </summary>
        /// <param name="asignatura">The entidad.</param>
        /// <returns></returns>
        public List<Asignatura> GetAsignaturasNivelesCiclosLectivos(List<CicloLectivo> cicloLectivo, List<Nivel> nivel)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("GetAsignaturasPorNivelesCicloLectivos_select");

                if (cicloLectivo != null && nivel != null)
                {
                    string ciclosLectivosParam = string.Empty;
                    if (cicloLectivo.Count > 0)
                    {
                        foreach (CicloLectivo unCicloLectivo in cicloLectivo)
                            ciclosLectivosParam += string.Format("{0},", unCicloLectivo.idCicloLectivo);

                        ciclosLectivosParam = ciclosLectivosParam.Substring(0, ciclosLectivosParam.Length - 1);
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@listaCicloLectivo", DbType.String, ciclosLectivosParam);
                    }

                    string nivelesParam = string.Empty;
                    if (nivel.Count > 0)
                    {
                        foreach (Nivel unNivel in nivel)
                            nivelesParam += string.Format("{0},", unNivel.idNivel);

                        nivelesParam = nivelesParam.Substring(0, nivelesParam.Length - 1);
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@listaNivel", DbType.String, nivelesParam);
                    }
                }

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<Asignatura> listaAsignaturas = new List<Asignatura>();
                Asignatura objAsignatura;
                while (reader.Read())
                {
                    objAsignatura = new Asignatura();

                    objAsignatura.idAsignatura = Convert.ToInt32(reader["idAsignatura"]);
                    objAsignatura.nombre = reader["nombreAsignatura"].ToString();

                    listaAsignaturas.Add(objAsignatura);
                }
                return listaAsignaturas;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAsignaturasNivelesCiclosLectivos()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAsignaturasNivelesCiclosLectivos()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Gets the asignaturas nivel.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        /// <exception cref="CustomizedException">
        /// </exception>
        public List<AsignaturaNivel> GetAsignaturasNivel(AsignaturaNivel entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("AsignaturaNivel_Select");
                if (entidad != null)
                {
                    if (entidad.asignatura.idAsignatura > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignatura", DbType.Int32, entidad.asignatura.idAsignatura);
                    if (entidad.nivel.idNivel > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idNivel", DbType.Int32, entidad.nivel.idNivel);
                    if (entidad.orientacion.idOrientacion > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idOrientacion", DbType.Int32, entidad.orientacion.idOrientacion);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<AsignaturaNivel> listaAsignaturaNivel = new List<AsignaturaNivel>();
                AsignaturaNivel objAsignaturaNivel;
                while (reader.Read())
                {
                    objAsignaturaNivel = new AsignaturaNivel();
                    objAsignaturaNivel.idAsignaturaNivel = Convert.ToInt32(reader["idAsignaturaNivel"]);
                    objAsignaturaNivel.asignatura.idAsignatura = Convert.ToInt32(reader["idAsignatura"]);
                    objAsignaturaNivel.asignatura.nombre = reader["Asignatura"].ToString();
                    objAsignaturaNivel.nivel.idNivel = Convert.ToInt32(reader["idNivel"]);
                    objAsignaturaNivel.nivel.nombre = reader["Nivel"].ToString();
                    objAsignaturaNivel.orientacion.idOrientacion = Convert.ToInt32(reader["idOrientacion"]);
                    objAsignaturaNivel.orientacion.nombre = reader["Orientacion"].ToString();
                    objAsignaturaNivel.cargaHoraria = Convert.ToInt32(reader["cargaHoraria"]);

                    listaAsignaturaNivel.Add(objAsignaturaNivel);
                }
                return listaAsignaturaNivel;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAsignaturasNivel()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAsignaturasNivel()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Gets the asignaturas nivel.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        /// <exception cref="CustomizedException">
        /// </exception>
        public List<Asignatura> GetAsignaturasNivel(Nivel entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("AsignaturaNivel_Select");
                if (entidad != null)
                {
                    //if (entidad.asignatura.idAsignatura > 0)
                    //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignatura", DbType.Int32, entidad.asignatura.idAsignatura);
                    if (entidad.idNivel > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idNivel", DbType.Int32, entidad.idNivel);
                    //if (entidad.orientacion.idOrientacion > 0)
                    //    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idOrientacion", DbType.Int32, entidad.orientacion.idOrientacion);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<Asignatura> listaAsignatura = new List<Asignatura>();
                Asignatura objAsignatura;
                while (reader.Read())
                {
                    objAsignatura = new Asignatura();
                    objAsignatura.idAsignatura = Convert.ToInt32(reader["idAsignatura"]);
                    objAsignatura.nombre = reader["Asignatura"].ToString();

                    listaAsignatura.Add(objAsignatura);
                }
                return listaAsignatura;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAsignaturasNivel()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAsignaturasNivel()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Gets the asignaturas nivel.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        /// <exception cref="CustomizedException">
        /// </exception>
        public List<Asignatura> GetAsignaturasNivel(AsignaturaCicloLectivo entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("AsignaturaNivel_Select");
                if (entidad != null)
                {
                    if (entidad.cursoCicloLectivo.cicloLectivo.idCicloLectivo > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCicloLectivo", DbType.Int32, entidad.cursoCicloLectivo.cicloLectivo.idCicloLectivo);
                    if (entidad.cursoCicloLectivo.curso.nivel.idNivel > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idNivel", DbType.Int32, entidad.cursoCicloLectivo.curso.nivel.idNivel);
                    if (entidad.docente != null && !string.IsNullOrEmpty(entidad.docente.username))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@docente", DbType.String, entidad.docente.username);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<Asignatura> listaAsignatura = new List<Asignatura>();
                Asignatura objAsignatura;
                while (reader.Read())
                {
                    objAsignatura = new Asignatura();
                    objAsignatura.idAsignatura = Convert.ToInt32(reader["idAsignatura"]);
                    objAsignatura.nombre = reader["Asignatura"].ToString();

                    listaAsignatura.Add(objAsignatura);
                }
                return listaAsignatura;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAsignaturasNivel()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAsignaturasNivel()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        #endregion
    }
}
