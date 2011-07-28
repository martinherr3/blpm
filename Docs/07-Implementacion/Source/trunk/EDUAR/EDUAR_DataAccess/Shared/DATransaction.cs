using System;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EDUAR_DataAccess.Shared
{
    public class DATransaction
    {
        #region --[Propiedades]--

        public Database DataBase { get; set; }

        public DbTransaction Transaction { get; set; }

        public DbCommand DBcomand { get; set; }

        public DbConnection Conection { get; set; }

        #endregion

        #region --[Constructor]--
        /// <summary>
        /// Constructor vacio
        /// </summary>
        public DATransaction()
        {
            DataBase = DAHelper.GetDataBaseFromWebConfig();
            Conection = DataBase.CreateConnection();
            DBcomand = Conection.CreateCommand();
        }

        /// <summary>
        /// Constructor vacio
        /// </summary>
        public DATransaction(string cadenaConexion)
        {
            DataBase = DAHelper.GetDataBaseFromWebConfig(cadenaConexion);
            Conection = DataBase.CreateConnection();
            DBcomand = Conection.CreateCommand();
        }
        #endregion

        #region --[Begin-Comint-RoolBack]--
        /// <summary>
        /// Metodo que abre la transaccion y no esta dentro de una ContinueTransaction.
        /// El metodo que invoca este metodo solo realiza una sola operacion en la Base de datos.
        /// 
        /// </summary>
        public void OpenTransaction()
        {
            try
            {
                Conection.Open();
                Transaction = Conection.BeginTransaction();
                DBcomand = Conection.CreateCommand();
                DBcomand.Transaction = Transaction;
            }
            catch (Exception)
            {
                if (Transaction != null)
                    Transaction.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Método que confirma todos los cambios realizados desde la llamada al método OpenTransaction() o  OpenTransactionContinue()
        /// Realiza un Commit en la Base de datos y cierra la conexion.
        /// </summary>
        public void CommitTransaction()
        {
            try
            {
                Transaction.Commit();
            }
            finally
            {
                Conection.Close();
            }
        }

        /// <summary>
        /// Método que realiza un rollback en la base de datos.
        /// </summary>
        public void RollbackTransaction()
        {
            try
            {
                if (Transaction != null && Transaction.Connection.State == System.Data.ConnectionState.Open)
                    Transaction.Rollback();
            }
            finally
            {
                Conection.Close();
            }

        }
        #endregion
    }
}
