///////////////////////////////////////////////////////////
//  Encuesta.cs
//  Implementation of the Class Encuesta
//  Generated by Enterprise Architect
//  Created on:      20-jun-2011 16:21:48
//  Original author: Pablo Nicoliello
///////////////////////////////////////////////////////////

using System;
using EDUAR_Entities.Shared;
using System.Collections.Generic;

namespace EDUAR_Entities
{
    [Serializable]
    public class Encuesta: DTBase
    {
        public int idEncuesta { get; set; }
        public Persona usuario { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaModificacion { get; set; }
        public string nombreEncuesta { get; set; }
        public bool activo { get; set; }
        public List<Pregunta> preguntas { get; set; }

        public Encuesta()
        {

        }

        ~Encuesta()
        {

        }

        public virtual void Dispose()
        {

        }

    }//end Encuesta
}