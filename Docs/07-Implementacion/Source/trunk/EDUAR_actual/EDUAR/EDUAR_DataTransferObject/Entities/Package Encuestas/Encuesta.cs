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
        public AmbitoEncuesta ambito { get; set; }
        public string nombreEncuesta { get; set; }
        public string objetivo { get; set; }
        public bool activo { get; set; }
        public List<Pregunta> preguntas { get; set; }

        public Encuesta()
        {
            usuario = new Persona();
            preguntas = new List<Pregunta>();
            ambito = new AmbitoEncuesta();
        }

        ~Encuesta()
        {

        }

        public virtual void Dispose()
        {

        }

    }//end Encuesta
}