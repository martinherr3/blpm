///////////////////////////////////////////////////////////
//  Escala.cs
//  Implementation of the Class Escala
//  Generated by Enterprise Architect
//  Created on:      20-jun-2011 16:21:48
//  Original author: Pablo Nicoliello
///////////////////////////////////////////////////////////

using EDUAR_Entities.Shared;
using System;

namespace EDUAR_Entities
{
    [Serializable]
    public class EscalaMedicion : DTBase
    {
        public int idEscala { get; set; }
        public string nombre;
        public string descripcion;

        public EscalaMedicion()
        {
        }

        ~EscalaMedicion()
        {
        }

        public virtual void Dispose()
        {
        }

    }//end Escala
}