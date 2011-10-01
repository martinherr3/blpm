using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

namespace EDUAR_BusinessLogic.Shared
{
    /// <summary>
    /// Clase que contienen toda la logica de las configuraciones de la Aplicación. 
    /// </summary>
    public class BLConfiguracionGlobal
    {
        #region --[Constante]--
        private const string ClassName = "BLConfiguracionGlobal";
        #endregion

        #region --[Métodos Publicos]--
        /// <summary>
        /// Método que obtiene el valor de una configuracion en particular.
        /// </summary>  
        /// <param name="configuracion">Enumeracion con la configuracion que se quiere buscar.</param>
        /// <returns>Valor de la configuracion</returns>
        public static string ObtenerConfiguracion(enumConfiguraciones configuracion)
        {
            try
            {
                DAConfiguracionGlobal dataAcces = new DAConfiguracionGlobal();
                return dataAcces.GetConfiguracion(configuracion);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - ObtenerConfiguracion", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Método que obtiene el valor de una configuracion en particular.
        /// </summary>  
        /// <param name="objDATransaction"></param>
        /// <param name="configuracion">Enumeracion con la configuracion que se quiere buscar.</param>
        /// <returns>Valor de la configuracion</returns>
        public string ObtenerConfiguracion(DATransaction objDATransaction, enumConfiguraciones configuracion)
        {
            string valor;
            try
            {
                DAConfiguracionGlobal dataAcces = new DAConfiguracionGlobal(objDATransaction);
                valor = dataAcces.GetConfiguracion(configuracion);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - ObtenerConfiguracion", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
            return valor;
        }
        #endregion
    }
}
