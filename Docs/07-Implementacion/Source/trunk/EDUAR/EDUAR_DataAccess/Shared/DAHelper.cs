using System;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EDUAR_DataAccess.Shared
{
    /// <summary>
    /// Clase que ofrece funcionalidad común al proyecto NotificacionesTutores.DataEntities
    /// </summary>
    public class DAHelper
    {
        #region Métodos Públicos

        /// <summary>
        /// Método estático que obtiene un objeto DataBase en función del connectionstring con name "SGDActividades"
        /// </summary>
        /// <returns>Objeto de tipo DataBase</returns>
        public static Database GetDataBaseFromWebConfig()
        {
            return DatabaseFactory.CreateDatabase("NOTUConnectionString.DataEntities");
        }

        /// <summary>
        /// Método estático que obtiene un objeto DataBase en función del connectionstring con name "SGDActividades"
        /// </summary>
        /// <returns>Objeto de tipo DataBase</returns>
        public static Database GetDataBaseFromWebConfig(String cadenaConexion)
        {
            return DatabaseFactory.CreateDatabase(cadenaConexion);
        }


        /// <summary>
        /// Método estático que obtiene un objeto DataTable paginado segun los  parametros
        /// </summary>
        /// <returns>Objeto de tipo DataBase</returns>
        public static DataTable ObtenerTablaPaginada(DataTable tablaSinPaginar, Int32 paginaActual, Int32 paginacionTamaño, String ordenamiento)
        {
            DataTable dtFiltrada = tablaSinPaginar.Clone();

            //if(tablaSinPaginar.Rows.Count ==0)
            //    return dtFiltrada;

            DataView vista = tablaSinPaginar.DefaultView;
            vista.Sort =ordenamiento ;

            Int32 rowDesde = (paginaActual*paginacionTamaño) - paginacionTamaño;
            Int32 rowHasta = (paginaActual*paginacionTamaño);

            for (Int32 i = rowDesde; i < rowHasta; i++)
            {
                if(tablaSinPaginar.Rows.Count <=i)
                    break;

                dtFiltrada.ImportRow(vista.Table.Rows[i]);
            }

            return dtFiltrada;
        }

        

        #endregion
    }
}
