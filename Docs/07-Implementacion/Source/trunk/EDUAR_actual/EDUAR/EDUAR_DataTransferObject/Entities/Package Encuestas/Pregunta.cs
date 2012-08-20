///////////////////////////////////////////////////////////
//  Pregunta.cs
//  Implementation of the Class Pregunta
//  Generated by Enterprise Architect
//  Created on:      20-jun-2011 16:21:55
//  Original author: Pablo Nicoliello
///////////////////////////////////////////////////////////

using EDUAR_Entities.Shared;
using System;
namespace EDUAR_Entities
{
    [Serializable]
    public class Pregunta: DTBase
    {
        public int idPregunta { get; set; }
        public CategoriaPregunta categoria { get; set; }
        public EscalaMedicion escala { get; set; }
        public string textoPregunta { get; set; }
        public string objetivoPregunta { get; set; }
        public double ponderacion { get; set; }

        public Pregunta()
        {

        }

        ~Pregunta()
        {

        }

        public virtual void Dispose()
        {

        }

    }//end Pregunta
}