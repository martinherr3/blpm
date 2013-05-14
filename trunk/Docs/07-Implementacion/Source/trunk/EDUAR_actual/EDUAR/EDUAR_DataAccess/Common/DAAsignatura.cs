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
        }

        public override void Create(Asignatura entidad)
        {
            throw new NotImplementedException();

        }

        public override void Create(Asignatura entidad, out int identificador)
        {
            throw new NotImplementedException();           
        }

        public override void Update(Asignatura entidad)
        {
            throw new NotImplementedException();            
        }

        public override void Delete(Asignatura entidad)
        {
            throw new NotImplementedException();
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
                    if (entidad.idNivel > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idNivel", DbType.Int32, entidad.idNivel);                    
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
