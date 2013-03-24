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
    public class DACurricula : DataAccesBase<Curricula>
    {
        #region --[Atributos]--
        private const string ClassName = "DACurricula";
        #endregion

        #region --[Constructor]--
        public DACurricula()
        {
        }

        public DACurricula(DATransaction objDATransaction)
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

        public override Curricula GetById(Curricula entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Curricula entidad)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates the specified entidad.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <param name="identificador">The identificador.</param>
        /// <exception cref="CustomizedException">
        /// </exception>
        public override void Create(Curricula entidad, out int identificador)
        {
            try
            {
                using (Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Curricula_Insert"))
                {
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@usernameAlta", DbType.String, entidad.personaAlta.username);
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignatura", DbType.Int32, entidad.asignatura.idAsignatura);
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idNivel", DbType.Int32, entidad.nivel.idNivel);
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idOrientacion", DbType.Int32, entidad.orientacion.idOrientacion);

                    Transaction.DataBase.AddOutParameter(Transaction.DBcomand, "@idCurricula", DbType.Int32, 0);

                    if (Transaction.Transaction != null)
                        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                    else
                        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

                    identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idCurricula"].Value.ToString());
                }
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Create()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Create()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Updates the specified entidad.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <exception cref="CustomizedException">
        /// </exception>
        public override void Update(Curricula entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Curricula_Update");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCurricula", DbType.Int32, entidad.idCurricula);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@usernameModificacion", DbType.String, entidad.personaModificacion.username);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignatura", DbType.Int32, entidad.asignatura.idAsignatura);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idNivel", DbType.Int32, entidad.nivel.idNivel);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idOrientacion", DbType.Int32, entidad.orientacion.idOrientacion);

                if (Transaction.Transaction != null)
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                else
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Update()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Update()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Deletes the specified entidad.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        public override void Delete(Curricula entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--

        /// <summary>
        /// Gets the curriculas.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        /// <exception cref="CustomizedException">
        /// </exception>
        public List<Curricula> GetCurriculas(Curricula entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Curricula_Select");
                if (entidad != null)
                {
                    if (entidad.asignatura.idAsignatura > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAsignatura", DbType.Int32, entidad.asignatura.idAsignatura);
                    if (entidad.nivel.idNivel > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idNivel", DbType.Int32, entidad.nivel.idNivel);
                    if (entidad.orientacion.idOrientacion > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idOrientacion", DbType.Int32, entidad.orientacion.idOrientacion);
                    if (entidad.idCurricula > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCurricula", DbType.Int32, entidad.idCurricula);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<Curricula> listaCurriculas = new List<Curricula>();
                Curricula objCurricula;
                DateTime fecha = new DateTime();
                while (reader.Read())
                {
                    objCurricula = new Curricula();
                    objCurricula.idCurricula = Convert.ToInt32(reader["idCurricula"]);
                    objCurricula.asignatura.idAsignatura = Convert.ToInt32(reader["idAsignatura"]);
                    objCurricula.asignatura.nombre = reader["Asignatura"].ToString();
                    objCurricula.nivel.idNivel = Convert.ToInt32(reader["idNivel"]);
                    objCurricula.nivel.nombre = reader["Nivel"].ToString();
                    objCurricula.orientacion.idOrientacion = Convert.ToInt32(reader["idOrientacion"]);
                    objCurricula.orientacion.nombre = reader["Orientacion"].ToString();
                    objCurricula.personaAlta.username = reader["UsuarioAlta"].ToString();
                    objCurricula.personaModificacion.username = reader["UsuarioModificacion"].ToString();

                    DateTime.TryParse(reader["fechaAlta"].ToString(), out fecha);
                    objCurricula.fechaAlta = fecha;
                    DateTime.TryParse(reader["fechaModificacion"].ToString(), out fecha);
                    objCurricula.fechaModificacion = fecha;

                    listaCurriculas.Add(objCurricula);
                }
                return listaCurriculas;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetCurriculas()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetCurriculas()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Gets the by asignatura nivel orientacion.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        /// <exception cref="CustomizedException">
        /// </exception>
        public int GetByAsignaturaNivelOrientacion(Curricula entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Curricula_Select");
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

                while (reader.Read())
                    return Convert.ToInt32(reader["idCurricula"]);

                return 0;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetByAsignaturaNivelOrientacion()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetByAsignaturaNivelOrientacion()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
        #endregion
    }
}
