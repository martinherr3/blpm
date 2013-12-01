using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

namespace EDUAR_DataAccess.Common
{
    public class DACurso : DataAccesBase<Curso>
    {
        #region --[Atributos]--
        private const string ClassName = "DACurso";
        #endregion

        #region --[Constructor]--
        public DACurso()
        {
        }

        public DACurso(DATransaction objDATransaction)
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

        public override Curso GetById(Curso entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Curso entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Curso entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Curso entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Curso entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        /// <summary>
        /// Gets the cursos.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<Curso> GetCursos(Curso entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Curso_Select");
                if (entidad != null)
                {

                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<Curso> listaCursos = new List<Curso>();
                Curso objCurso;
                while (reader.Read())
                {
                    objCurso = new Curso();

                    objCurso.idCurso = Convert.ToInt32(reader["idCurso"]);
                    objCurso.nombre = reader["nombre"].ToString();

                    listaCursos.Add(objCurso);
                }
                return listaCursos;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetCursos()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetCursos()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Gets the curso ciclo lectivo.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public CursoCicloLectivo GetCursoCicloLectivo(CursoCicloLectivo entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("CursosCicloLectivo_Select");
                if (entidad != null)
                {
                    if (entidad.idCursoCicloLectivo > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCursoCicloLectivo", DbType.Int32, entidad.idCursoCicloLectivo);
                }

                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                CursoCicloLectivo objCurso = null;
                while (reader.Read())
                {
                    objCurso = new CursoCicloLectivo();

                    objCurso.idCursoCicloLectivo = Convert.ToInt32(reader["idCursoCicloLectivo"]);
                    objCurso.curso.nombre = reader["nombre"].ToString();
                    objCurso.curso.nivel.nombre = reader["nivel"].ToString();
                    objCurso.curso.nivel.idNivel = Convert.ToInt32(reader["idNivel"]);
                }
                return objCurso;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetCursoCicloLectivo()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetCursoCicloLectivo()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Gets the curso ciclo lectivo.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<CursoCicloLectivo> GetCursosCicloLectivo(Nivel nivel, CicloLectivo cicloLectivo)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("CursosCicloLectivo_Select");
                if (cicloLectivo != null && cicloLectivo.idCicloLectivo > 0)
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCicloLectivo", DbType.Int32, cicloLectivo.idCicloLectivo);
                if (nivel != null && nivel.idNivel > 0)
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idNivel", DbType.Int32, nivel.idNivel);

                
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                CursoCicloLectivo objCurso = null;
                List<CursoCicloLectivo> listaCursos = new List<CursoCicloLectivo>();
                while (reader.Read())
                {
                    objCurso = new CursoCicloLectivo();

                    objCurso.idCursoCicloLectivo = Convert.ToInt32(reader["idCursoCicloLectivo"]);
                    objCurso.curso.nombre = reader["nombre"].ToString();

                    listaCursos.Add(objCurso);
                }
                return listaCursos;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetCursosCicloLectivo()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetCursosCicloLectivo()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        public List<CursoCicloLectivo> GetCursosCicloLectivo(Curso curso, CicloLectivo cicloLectivo)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("CursosCicloLectivo_Select");
                if (cicloLectivo != null && cicloLectivo.idCicloLectivo > 0)
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCicloLectivo", DbType.Int32, cicloLectivo.idCicloLectivo);
                if (curso != null)
                {
                    if (curso.nivel != null && curso.nivel.idNivel > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idNivel", DbType.Int32, curso.nivel.idNivel);
                    if (curso.nombre != null && !string.IsNullOrEmpty(curso.nombre))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@username", DbType.String, curso.nombre);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                CursoCicloLectivo objCurso = null;
                List<CursoCicloLectivo> listaCursos = new List<CursoCicloLectivo>();
                while (reader.Read())
                {
                    objCurso = new CursoCicloLectivo();

                    objCurso.idCursoCicloLectivo = Convert.ToInt32(reader["idCursoCicloLectivo"]);
                    objCurso.curso.nombre = reader["nombre"].ToString();

                    listaCursos.Add(objCurso);
                }
                return listaCursos;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetCursosCicloLectivo()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetCursosCicloLectivo()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
        #endregion

    }
}
