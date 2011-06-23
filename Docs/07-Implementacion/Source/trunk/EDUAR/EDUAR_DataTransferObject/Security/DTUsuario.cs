using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities.Security
{
    [Serializable]
    [DataContract]
    public class DTUsuario : DTBase
    {
        #region --[Constructores]--
        /// <summary>
        /// Constructor con parametros.
        /// </summary>
        public DTUsuario(String nombre, String password)
        {
             Nombre = nombre;
             Password = password;
             ListaRoles = new List<DTRol>();
        }
        
        /// <summary>
        /// Constructor sin parametros.
        /// </summary>
        public DTUsuario()
        {
            ListaRoles = new List<DTRol>();
        }
        #endregion

        #region --[Propiedades]--
        /// <summary>
        /// Nombre Usuario
        /// </summary>
        [DataMember]
        public String Nombre { get; set; }

        /// <summary>
        /// Nombre nuevo del usuario
        /// </summary>
        [DataMember]
        public String NombreNuevo { get; set; }

        /// <summary>
        /// Password Usuario.
        /// </summary>
        [DataMember]
        public String Password { get; set; }

        /// <summary>
        /// Nuevo Password del Usario.
        /// </summary>
        [DataMember]
        public String PasswordNuevo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public String PaswordPregunta { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public String PaswordRespuesta { get; set; }

        /// <summary>
        /// Indica si el usuario fue validado para entrar en la aplicacion.
        /// </summary>
        [DataMember]
        public Boolean UsuarioValido{ get; set; }

        /// <summary>
        /// Indica si el usuario es la primera vez que accede a la aplicacion y no cambio el password.
        /// </summary>
        [DataMember]
        public Boolean EsUsuarioInicial { get; set; }

        /// <summary>
        /// Lista con los Roles de usuario. 
        /// </summary>
        [DataMember]
        public List<DTRol>ListaRoles { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public String Email { get; set; }

        /// <summary>
        /// Indica si el usuario esta habilitado para iniciar session. 
        /// </summary>
        [DataMember]
        public Boolean Aprobado { get; set; }
        
        #endregion
    }
}
