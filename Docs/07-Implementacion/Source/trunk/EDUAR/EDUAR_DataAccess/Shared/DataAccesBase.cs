using System;
using System.Runtime.Serialization;

namespace EDUAR_DataAccess.Shared
{
    /// <summary>
    /// Clase abstracta que sirve de base para los objetos comunes de DataAccesBase
    /// </summary>
    /// <typeparam name="TDataTransferObject">DTO de la entidad</typeparam>
    public abstract class DataAccesBase<TDataTransferObject>
    {
        #region --[Propiedades]--
        public DATransaction Transaction { get; set; }
        #endregion

        #region --[Constructores]--
        protected DataAccesBase()
        {
            Transaction = new DATransaction();
        }

        protected DataAccesBase(DATransaction objDATransaction)
        {
            Transaction = objDATransaction;
        }
        #endregion

        #region --[Propiedades Abstractas]--

        /// <summary>
        /// Propiedad para obtener/asignar el campo principal de la entidad
        /// </summary>
        [DataMember]
        public abstract string FieldID { get; }

        /// <summary>
        /// Propiedad para obtener/asignar el campo descripción de la entidad
        /// </summary>
        [DataMember]
        public abstract string FieldDescription { get; }

        #endregion

        #region --[Métodos Abstract]--

        /// <summary>
        /// Método abstracto que debe ser implementado para obtener una entidad
        /// </summary>
        public abstract TDataTransferObject GetById(TDataTransferObject entidad);
        
        /// <summary>
        /// Método abstracto que debe ser implementado para guardar la entidad
        /// </summary>
        public abstract void Create(TDataTransferObject entidad);

        /// <summary>
        /// Método abstracto que debe ser implementado para guardar la entidad
        /// </summary>
        public abstract void Create(TDataTransferObject entidad, out Int32 identificador);

        /// <summary>
        /// Método abstracto que debe ser implementado para guardar la entidad
        /// </summary>
        public abstract void Update(TDataTransferObject entidad);

        /// <summary>
        /// Método abstracto que debe ser implementado para eliminar la entidad
        /// </summary>
        public abstract void Delete(TDataTransferObject entidad);

        #endregion

        #region --[Métodos Públicos]--
        /// <summary>
        /// Validars the fecha SQL.
        /// </summary>
        /// <param name="fecha">The fecha.</param>
        /// <returns></returns>
        public static bool ValidarFechaSQL(DateTime fecha)
        {
            if (fecha.Year < 1753 || fecha.Year > 9999)
                return false;
            return true;
        }
        #endregion
    }
}
