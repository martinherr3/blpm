using System;
using EDUAR_SI_DataAccess;

using System.Data.Objects;

namespace  EDUAR_SI_BusinessLogic
{
    /// <summary>
    /// Clase base de las clases en la capa de negocio del proceso.
    /// </summary>
    public class BLProcesoBase
    {
        #region --[Atributos]--
        /// <summary>
        /// Contiene la cadena de conexión a MySQL
        /// </summary>
        public String ConnectionString { get; set; }
       // protected readonly ObjectSet<EDUAR_Configuraciones> objConfiguraciones;
        #endregion

        #region --[Propiedades]--

        #endregion

        #region --[Constructores]--
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public BLProcesoBase()
        {

        }

        /// <summary>
        /// Constructor. Inicializa la variable ConnectionString.
        /// </summary>
        /// <param name="connectionString">Cadena de conexión a la base de datos</param>
        public BLProcesoBase(String connectionString)
        {
            ConnectionString = connectionString;

            DABase objDABase = new DABase(ConnectionString);
            //objConfiguraciones = objDABase.ObtenerConfiguraciones();
        }
        #endregion

        #region --[Métodos Privados]--

        #endregion

        #region --[Métodos Públicos]--
        /// <summary>
        /// El sistema inserta un registro en la tabla ProcesosEjecutados con los
        /// siguientes valores:
        /// </summary>
        /// <param name="proceso">Nombre del proceso que produjo la excepción.</param>
        /// <param name="ex">Excepción producida.</param>
        /// <param name="datosFichero">Datos del fichero que se carga. Pasar null si no se
        /// carga ningún fichero.</param>
        //protected void OnErrorProcess(EDUAR_ProcesosEjecutados objProceso)
        //{
        //    DABase objDABase = new DABase();
        //    objDABase.OnErrorProcess(objProceso);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuracion"></param>
        /// <returns></returns>
        //protected String ObtenerValorconfiguracion(enumConfiguracion configuracion)
        //{
        //        foreach (DSConfiguracionGlobal.DataTableConfiguracionGlobalRow row in ConfiguracionesGlobalesData.Rows)
        //        {
        //            if (row.NombreVariable == configuracion.ToString())
        //                return !row.IsValorNull() ? row.Valor : String.Empty;
        //        }

        //    //String filtro = String.Format("NombreVariable={0}", configuracion);
        //    //return  ConfiguracionesGlobalesData.Select(filtro)[0]["Valor"].ToString();

        //    return String.Empty;
        //}

        /// <summary>
        /// Genera una traza en la tabla ProcesosAutomaticos.
        /// </summary>
        /// <param name="proceso">Nombre del proceso</param>
        /// <param name="estado">Resultado de la ejecución (0: Correcto - 1: Incorrecto).</param>
        //protected void ProcesosAutomaticosCreate(String proceso, Boolean estado)
        //{
        //    DAProcesosAutomaticos objDAProcesosAutomaticos = new DAProcesosAutomaticos(ConnectionString);
        //    DTProcesosAutomaticos resultadoProceso = new DTProcesosAutomaticos();
        //    resultadoProceso.Proceso = proceso;
        //    resultadoProceso.Estado = estado;
        //    resultadoProceso.Fecha = DateTime.Now;
        //    objDAProcesosAutomaticos.Create(resultadoProceso);
        //}
        #endregion
    }
}