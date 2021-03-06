///////////////////////////////////////////////////////////
//  MotivoAusencia.cs
//  Implementation of the Class MotivoAusencia
//  Generated by Enterprise Architect
//  Created on:      20-jun-2011 16:21:51
//  Original author: orkus
///////////////////////////////////////////////////////////

using EDUAR_Entities.Shared;
using System;
namespace EDUAR_Entities
{
    [Serializable]
    public class MotivoAusencia: DTBase
    {

        public string nombre { get; set; }
        public int idMotivo { get; set; }
        public int idMotivoTransaccional { get; set; }

        public MotivoAusencia()
        {

        }

        ~MotivoAusencia()
        {

        }

        public virtual void Dispose()
        {

        }

    }//end MotivoAusencia
}