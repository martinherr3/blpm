///////////////////////////////////////////////////////////
//  Inasistencia.cs
//  Implementation of the Class Inasistencia
//  Generated by Enterprise Architect
//  Created on:      20-jun-2011 16:21:50
//  Original author: orkus
///////////////////////////////////////////////////////////



using EDUAR_Entities.Shared;
using System;
namespace EDUAR_Entities
{
    [Serializable]
    public class Inasistencia: DTBase
    {
        public int idInasistencia { get; set; }
        public DateTime fecha { get; set; }
        public MotivoAusencia motivo { get; set; }

        public Inasistencia()
        {

        }

        ~Inasistencia()
        {

        }

        public virtual void Dispose()
        {

        }

    }//end Inasistencia
}