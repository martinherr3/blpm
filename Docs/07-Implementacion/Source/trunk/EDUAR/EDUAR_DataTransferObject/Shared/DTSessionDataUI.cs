using System;
using System.Runtime.Serialization;
using EDUAR_Entities.Security;

namespace EDUAR_Entities.Shared
{
    /// <summary>
    /// Este objeto se utilizará para mantener datos en session.
    /// </summary>
    public class DTSessionDataUI : DTBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DTSessionDataUI"/> class.
        /// </summary>
        public DTSessionDataUI()
        {
            ObjDTUsuario = new DTUsuario();
        }

        /// <summary>
        /// Contiene los datos del usuario logueado
        /// </summary>
        [DataMember]
        public DTUsuario ObjDTUsuario { get; set; }

        [DataMember]
        public Uri urlDefault { get; set; }
    }
}
