using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities.Security
{
    [Serializable]
    public class DTSeguridad : DTBase
    {
        #region --[Constructores]--
        /// <summary>
        /// Constructor con DTO como parámetro.
        /// </summary>
        public DTSeguridad()
        {
            ListaUsuarios = new List<DTUsuario>();
            ListaRoles = new List<DTRol>();
            Usuario= new DTUsuario();
        }

        #endregion

        #region --[Propiedades]--
        /// <summary>
        /// Nombre de la aplicacion.
        /// </summary>
        [DataMember]
        public string Aplicacion { get; set; }

        /// <summary>
        /// Usuario.
        /// </summary>
        [DataMember]
        public DTUsuario Usuario { get; set; }

        /// <summary>
        /// Lista de usuarios.
        /// </summary>
        [DataMember]
        public List<DTUsuario> ListaUsuarios { get; set; }

        /// <summary>
        /// Lista de roles.
        /// </summary>
        [DataMember]
        public List<DTRol> ListaRoles { get; set; }

        /// <summary>
        /// .
        /// </summary>
        //[DataMember]
        public DTRol Rol { get; set; }

        /// <summary>
        /// Tamaño de la paginacion.
        /// </summary>
        [DataMember]
        public Int32 PagSize { get; set; }

        /// <summary>
        /// Paginas que se esta buscando
        /// </summary>
        [DataMember]
        public Int32 PagPaginaActual { get; set; }

        /// <summary>
        /// Paginas totales
        /// </summary>
        [DataMember]
        public Int32 PagCantidadTotalReg { get; set; }

        #endregion
    }
}
