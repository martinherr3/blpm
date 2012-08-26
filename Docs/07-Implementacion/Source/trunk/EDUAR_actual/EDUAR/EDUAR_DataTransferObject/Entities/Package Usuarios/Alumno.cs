using System;
using System.Collections.Generic;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
    [Serializable]
    public class Alumno : Persona
    {

        public int idAlumno { get; set; }
        public int idAlumnoTransaccional { get; set; }
        public string legajo { get; set; }
        public DateTime fechaAlta { get; set; }
        public DateTime fechaBaja { get; set; }
        public List<Tutor> listaTutores { get; set; }

        public Alumno():base()
        {        
            listaTutores = new List<Tutor>();
        }

        public virtual void Dispose()
        {
        }
    }
}
