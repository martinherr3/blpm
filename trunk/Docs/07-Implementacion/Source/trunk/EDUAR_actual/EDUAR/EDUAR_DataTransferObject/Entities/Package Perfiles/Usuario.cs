///////////////////////////////////////////////////////////
//  Usuario.cs
//  Implementation of the Class Usuario
//  Generated by Enterprise Architect
//  Created on:      20-jun-2011 16:21:58
//  Original author: Pablo Nicoliello
///////////////////////////////////////////////////////////

using EDUAR_Entities.Shared;
using System;
namespace EDUAR_Entities
{
    [Serializable]
    public class Usuario: DTBase
    {
        public int idUsuario { get; set; }
        public int estadoCuenta { get; set; }
        public DateTime fechaAlta { get; set; }
        public DateTime _fechaBaja { get; set; }
        public string nobre { get; set; }
        public Perfil perfil { get; set; }

        public Usuario()
        {

        }

        ~Usuario()
        {

        }

        public virtual void Dispose()
        {

        }

    }//end Usuario
}