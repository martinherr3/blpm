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
    public class DAContenido : DataAccesBase<Contenido>
    {
        #region --[Atributos]--
        private const string ClassName = "DAContenido";
        #endregion

        #region --[Constructor]--
        public DAContenido()
        {
        }

        public DAContenido(DATransaction objDATransaction)
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

        public override Contenido GetById(Contenido entidad)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates the specified entidad.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <exception cref="CustomizedException">
        /// </exception>
        public override void Create(Contenido entidad)
        {
            try
            {
                int identificador = 0;
                this.Create(entidad, out identificador);
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

        public override void Create(Contenido entidad, out int identificador)
        {
            try
            {
                using (Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Contenido_Insert"))
                {
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCurricula", DbType.Int32, entidad.idCurricula);
                    Transaction.DataBase.AddOutParameter(Transaction.DBcomand, "@idContenido", DbType.Int32, 0);

                    if (Transaction.Transaction != null)
                        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                    else
                        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

                    identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idContenido"].Value.ToString());
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
        public override void Update(Contenido entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Contenido_Update");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idContenido", DbType.Int32, entidad.idContenido);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCurricula", DbType.Int32, entidad.idCurricula);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);

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
        public override void Delete(Contenido entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Contenido_Delete");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idContenido", DbType.Int32, entidad.idContenido);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@usernameBaja", DbType.String, entidad.usuarioBaja.username);

                if (Transaction.Transaction != null)
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                else
                    Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Delete()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Delete()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
        #endregion

        #region --[Métodos Públicos]--
        /// <summary>
        /// Gets the contenidos.
        /// </summary>
        /// <param name="objFiltro">The obj filtro.</param>
        /// <returns></returns>
        public List<Contenido> GetContenidos(Contenido entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Contenido_Select");
                if (entidad != null)
                {
                    if (entidad.idCurricula > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCurricula", DbType.Int32, entidad.idCurricula);
                    if (entidad.activo)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<Contenido> listaContenidos = new List<Contenido>();
                Contenido objContenido;
                while (reader.Read())
                {
                    objContenido = new Contenido();
                    objContenido.idContenido = Convert.ToInt32(reader["idContenido"]);
                    objContenido.descripcion = reader["descripcion"].ToString();
                    objContenido.activo = Convert.ToBoolean(reader["activo"]);
                    objContenido.idCurricula = Convert.ToInt32(reader["idCurricula"]);

                    listaContenidos.Add(objContenido);
                }
                return listaContenidos;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetContenidos()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetContenidos()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Gets the contenidos.
        /// </summary>
        /// <param name="objFiltro">The obj filtro.</param>
        /// <returns></returns>
        public List<Contenido> GetContenidosCalc(Contenido entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("Contenido_Select");
                if (entidad != null)
                {
                    if (entidad.idCurricula > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCurricula", DbType.Int32, entidad.idCurricula);
                    if (entidad.activo)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@activo", DbType.Boolean, entidad.activo);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<Contenido> listaContenidos = new List<Contenido>();
                Contenido objContenido;
                while (reader.Read())
                {
                    objContenido = new Contenido();
                    objContenido.idContenido = Convert.ToInt32(reader["idContenido"]);
                    objContenido.idCurricula = Convert.ToInt32(reader["idCurricula"]);

                    listaContenidos.Add(objContenido);
                }
                return listaContenidos;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetContenidos()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetContenidos()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        #endregion

    }
}
