///////////////////////////////////////////////////////////
//  Plan.cs
//  Implementation of the Class Plan
//  Generated by Enterprise Architect
//  Created on:      20-jun-2011 16:21:53
//  Original author: Pablo Nicoliello
///////////////////////////////////////////////////////////


using System;
using System.Collections.Generic;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
    [Serializable]
    public class Plan:DTBase
    {
        public int idPlan { get; set; }
        public List<Asignatura> asignaturas { get; set; }
        public List<Contenido> contenidos { get; set; }
        public Usuario creador { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string nombrePlan { get; set; }
        public int periodoFinVigencia { get; set; }
        public int periodoInicioVigencia { get; set; }
        public Orientacion orientacion { get; set; }

        public Plan()
        {

        }

        ~Plan()
        {

        }

        public virtual void Dispose()
        {

        }
    }//end Plan
}