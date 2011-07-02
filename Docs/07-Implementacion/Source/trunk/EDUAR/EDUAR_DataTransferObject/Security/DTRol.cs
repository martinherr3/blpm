using System;
using System.Runtime.Serialization;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities.Security
{
    [Serializable]
    [DataContract]
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
        public String Nombre { get; set; }

        /// <summary>
        /// Nombre corto.
        /// </summary>
        [DataMember]
        public String NombreCorto { get; set; }

        /// <summary>
        /// Descripcion del rol.
        /// </summary>
        [DataMember]
        public String Descripcion { get; set; }

        #endregion
    }
}
