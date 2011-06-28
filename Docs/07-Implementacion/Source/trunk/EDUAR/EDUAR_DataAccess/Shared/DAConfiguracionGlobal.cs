using System;
using System.Data;
using System.Data.SqlClient;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EDUAR_DataAccess.Shared
{
    /// <summary>
    /// Clase que contiene la logica de acceso a datos de las configuraciones. 
    /// </summary>
    public class DAConfiguracionGlobal
    {
        #region --[Atributos]--
        private readonly DATransaction ObjDATransaction;
        private const String ClassName = "DAConfiguracionGlobal";
        #endregion

        #region --[Constructores]--
        public DAConfiguracionGlobal()
        {
            ObjDATransaction = new DATransaction();
        }

        public DAConfiguracionGlobal(DATransaction objDATransaction)
        {
            ObjDATransaction = objDATransaction;
        }
       
        #endregion

        #region --[Métodos Publicos]--
        /// <summary>
        /// Método que obtiene el valor de una configuracion en particular.
        /// </summary>  
        /// <param name="configuracion">Enumeracion con la configuracion que se quiere buscar.</param>
        /// <returns>Valor de la configuracion</returns>
        public String GetConfiguracion(enumConfiguraciones configuracion)
        {
            try
            {
                String valor = String.Empty;
                const String query = @"SELECT 
                                            IdConfiguracion
                                            ,Nombre
                                            ,Descripcion
                                            ,Valor
                                        FROM
                                            Configuraciones
                                        WHERE 
	                                        Nombre = @Nombre";


                ObjDATransaction.DBcomand = ObjDATransaction.DataBase.GetSqlStringCommand(query);

                // Añadir parámetros
                ObjDATransaction.DataBase.AddInParameter(ObjDATransaction.DBcomand, "@Nombre", DbType.String, configuracion.ToString());
                DataSet ds = ObjDATransaction.Transaction != null
                                 ? ObjDATransaction.DataBase.ExecuteDataSet(ObjDATransaction.DBcomand,ObjDATransaction.Transaction)
                                 : ObjDATransaction.DataBase.ExecuteDataSet(ObjDATransaction.DBcomand);

                if (ds.Tables[0].Rows.Count != 0)
                    valor = ds.Tables[0].Rows[0]["Valor"].ToString();
                return valor;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - GetConfiguracion()", ClassName),
                                                       ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - GetConfiguracion()", ClassName),
                                                       ex, enuExceptionType.DataAccesException);
            }
        }
        #endregion
    }
}
