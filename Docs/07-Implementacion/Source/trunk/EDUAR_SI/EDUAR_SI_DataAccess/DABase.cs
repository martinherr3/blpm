using System.Data.SqlClient;

namespace EDUAR_SI_DataAccess
{
    /// <summary>
    /// Clase base que contiene la conexion a la base de datos.
    /// </summary>
    public class DABase
    {
        #region --[Atributos]--
        /// <summary>
        /// Conexión a la base de datos.
        /// </summary>
        protected SqlConnection sqlConnectionConfig;
        #endregion

        #region --[Constructores]--
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public DABase()
        {
        }

        /// <summary>
        /// Constructor. Inicializa el objeto SQLConnection con la cadena de conexión
        /// pasada por parámetro.
        /// </summary>
        /// <param name="connectionString">Cadena de conexión a la base de datos.</param>
        public DABase(string connectionString)
        {
            sqlConnectionConfig = new SqlConnection(connectionString);
        }
        #endregion
    }
}