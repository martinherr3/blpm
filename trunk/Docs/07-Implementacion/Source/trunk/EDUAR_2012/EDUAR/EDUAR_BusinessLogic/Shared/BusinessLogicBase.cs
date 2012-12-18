using EDUAR_BusinessLogic.Shared;
using EDUAR_DataAccess.Shared;

namespace EDUAR_BusinessLogic.Shared
{
    /// <summary>
    /// Clase abstracta que sirve de base para los objetos comunes de BusinessLogic
    /// </summary>
    /// <typeparam name="TDataTransferObject">DTO</typeparam>
    /// <typeparam name="TDataAccessObject">DAO</typeparam>
    public abstract class BusinessLogicBase<TDataTransferObject, TDataAccessObject> where TDataAccessObject : new()
    {
        #region --[Atributos]--

        protected TDataTransferObject data;
        protected TDataAccessObject dataAcces;
     
        #endregion

        #region --[Constructores]--

        /// <summary>
        /// Constructor vacio
        /// </summary>
        protected BusinessLogicBase()
        {
            dataAcces = new TDataAccessObject();
        }

        #endregion

        #region --[Propiedades Abstractas]--
        /// <summary>
        /// Propiedad para obtener/asignar el DAO.
        /// </summary>
        protected abstract TDataAccessObject DataAcces { get; set; }

        /// <summary>
        /// Propiedad para obtener/asignar el DTO.
        /// </summary>
        public abstract TDataTransferObject Data { get; set; }

        /// <summary>
        /// Propiedad para obtener/asignar el campo principal de la entidad
        /// </summary>
        public abstract string FieldId { get; }

        /// <summary>
        /// Propiedad para obtener/asignar el campo descripción de la entidad
        /// </summary>
        public abstract string FieldDescription { get; }

        ///// <summary>
        ///// Propiedad para obtener/asignar la lista de DTO.
        ///// </summary>
        //public abstract IList<TDataTransferObject> ListaData { get; set; }

        #endregion

        #region --[Métodos Abstract]--
        /// <summary>
        /// Método abstracto que debe ser implementado para obtener una entidad
        /// </summary>
        public abstract void GetById();

        /// <summary>
        /// Método abstracto que debe ser implementado para guardar la entidad
        /// </summary>
        public abstract void Save();

        /// <summary>
        /// Método abstracto que debe ser implementado para guardar la entidad utilizando la transacción
        /// y conexión pasada
        /// </summary>
        /// <param name="objDATransaction">Objeto con la conexion a BD y la transaccion</param>
        public abstract void Save(DATransaction objDATransaction);

        /// <summary>
        /// Método abstracto que debe ser implementado para eliminar la entidad
        /// </summary>
        public abstract void Delete();

        /// <summary>
        /// Método abstracto que debe ser implementado para eliminar la entidad utilizando la transacción
        /// y conexión pasada
        /// </summary>
        /// <param name="objDATransaction">Objeto con la conexion a BD y la transaccion</param>
        public abstract void Delete(DATransaction objDATransaction);

        #endregion

    }
}
