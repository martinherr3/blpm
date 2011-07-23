using System;
using System.Runtime.Serialization;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities.Security
{
    [Serializable]
    public class DTRol : DTBase
    {
        #region --[Propiedades]--

        /// <summary>
        /// Id del rol.
        /// </summary>
        [DataMember]
        public int ID { get; set; }

        /// <summary>
        /// Id del rol como cadena.
        /// </summary>
        [DataMember]
        public string RoleId { get; set; }

        /// <summary>
        /// Nombre del rol.
        /// </summary>
        [DataMember]
        public string Nombre { get; set; }

        /// <summary>
        /// Nombre corto.
        /// </summary>
        [DataMember]
        public string NombreCorto { get; set; }

        /// <summary>
        /// Descripcion del rol.
        /// </summary>
        [DataMember]
        public string Descripcion { get; set; }

        #endregion
    }
}
