using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System.Data;
using System.Data.SqlClient;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_DataAccess.Encuestas
{
    public class DACategoriaPregunta : DataAccesBase<CategoriaPregunta>
    {
        #region --[Atributos]--
        private const string ClassName = "DACategoriaPregunta";
        #endregion

        #region --[Constructor]--
        public DACategoriaPregunta()
        {
        }

        public DACategoriaPregunta(DATransaction objDATransaction)
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

        public override CategoriaPregunta GetById(CategoriaPregunta entidad)
        {
            try
            {
                if (entidad != null)
                {
                    //El identificador es mandatorio
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCategoria", DbType.Int32, entidad.idCategoriaPregunta);

                    if (!string.IsNullOrEmpty(entidad.nombre))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@nombre", DbType.String, entidad.nombre);
                    if (!string.IsNullOrEmpty(entidad.descripcion))
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                CategoriaPregunta objCategoriaPregunta = null;
                if (reader.Read())
                {
                    objCategoriaPregunta = new CategoriaPregunta();
                    objCategoriaPregunta.idCategoriaPregunta = Convert.ToInt32(reader["idCategoria"]);
                    objCategoriaPregunta.nombre = reader["nombre"].ToString();
                    objCategoriaPregunta.descripcion = reader["descripcion"].ToString();
                }
                return objCategoriaPregunta;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Select()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Select()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        public override void Create(CategoriaPregunta entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(CategoriaPregunta entidad, out int identificador)
        {
            try
            {
                using (Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("CategoriaPregunta_Insert"))
                {
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@nombre", DbType.String, entidad.nombre);
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAmbito", DbType.Int32, entidad.ambito.idAmbitoEncuesta);
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCategoria", DbType.Int32, 0);

                    if (Transaction.Transaction != null)
                        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand, Transaction.Transaction);
                    else
                        Transaction.DataBase.ExecuteNonQuery(Transaction.DBcomand);

                    identificador = Int32.Parse(Transaction.DBcomand.Parameters["@idCategoria"].Value.ToString());
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

        public override void Update(CategoriaPregunta entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("CategoriaPregunta_Update");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@nombre", DbType.String, entidad.nombre);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@descripcion", DbType.String, entidad.descripcion);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAmbito", DbType.Int32, entidad.ambito.idAmbitoEncuesta);
                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCategoriaPregunta", DbType.Int32, entidad.idCategoriaPregunta);

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

        public override void Delete(CategoriaPregunta entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("CategoriaPregunta_Delete");

                Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCategoriaPregunta", DbType.Int32, entidad.idCategoriaPregunta);

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
        /// Gets the encuestas.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<CategoriaPregunta> GetCategoriasPregunta(CategoriaPregunta entidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("CategoriaPregunta_Select");
                if (entidad != null)
                {
                    if (entidad.idCategoriaPregunta > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCategoria", DbType.Int32, entidad.idCategoriaPregunta);
                    if (entidad.ambito.idAmbitoEncuesta > 0)
                        Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idAmbito", DbType.Int32, entidad.ambito.idAmbitoEncuesta);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                List<CategoriaPregunta> listaCategoriasPregunta = new List<CategoriaPregunta>();
                CategoriaPregunta objCategoriaPregunta;

                while (reader.Read())
                {
                    objCategoriaPregunta = new CategoriaPregunta();

                    objCategoriaPregunta.idCategoriaPregunta = Convert.ToInt32(reader["idCategoria"]);
                    objCategoriaPregunta.nombre = reader["nombreCategoria"].ToString();
                    objCategoriaPregunta.descripcion = reader["descripcionCategoria"].ToString();

                    //int valor = Convert.ToInt32(reader["cantidadEncuestas"]);

                    //if(valor > 0) objCategoriaPregunta.asignada = true;
                    //else objCategoriaPregunta.asignada = false;

                    objCategoriaPregunta.ambito = new AmbitoEncuesta();
                    {
                        objCategoriaPregunta.ambito.idAmbitoEncuesta = Convert.ToInt32(reader["idAmbito"]);
                        objCategoriaPregunta.ambito.nombre = reader["nombreAmbito"].ToString();
                        objCategoriaPregunta.ambito.descripcion = reader["descripcionAmbito"].ToString();
                    }

                    objCategoriaPregunta.disponible = EsCategoriaDisponible(objCategoriaPregunta.idCategoriaPregunta);

                    listaCategoriasPregunta.Add(objCategoriaPregunta);
                }
                return listaCategoriasPregunta;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetCategoriasPregunta()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetCategoriasPregunta()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Verifica que la categoría en cuestión no está siendo utilizada en ninguna encuesta.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public bool EsCategoriaDisponible(int idEntidad)
        {
            try
            {
                Transaction.DBcomand = Transaction.DataBase.GetStoredProcCommand("CategoriaPregunta_Select");
                if (idEntidad != 0)
                {
                    Transaction.DataBase.AddInParameter(Transaction.DBcomand, "@idCategoria", DbType.Int32, idEntidad);
                }
                IDataReader reader = Transaction.DataBase.ExecuteReader(Transaction.DBcomand);

                bool estaDisponible = false;

                while (reader.Read())
                {
                    if (Convert.ToInt32(reader["cantidadEncuestas"]) == 0) estaDisponible = true;
                }
                return estaDisponible;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - EsCategoriaUtilizada()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - EsCategoriaUtilizada()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }
        #endregion
    }
}
