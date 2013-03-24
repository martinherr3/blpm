using EDUAR_Entities.Shared;
using System;
namespace EDUAR_Entities
{
    [Serializable]
    public class Curricula : DTBase
    {
        public int idCurricula { get; set; }
        public DateTime fechaAlta { get; set; }
        public DateTime fechaModificacion { get; set; }
        public Persona personaAlta { get; set; }
        public Persona personaModificacion { get; set; }
        public Asignatura asignatura { get; set; }
        public Nivel nivel { get; set; }
        public Orientacion orientacion { get; set; }

        public Curricula()
        {
            personaAlta = new Persona();
            personaModificacion = new Persona();
            asignatura = new Asignatura();
            nivel = new Nivel();
            orientacion = new Orientacion();
        }

        ~Curricula()
        {

        }

        public virtual void Dispose()
        {

        }
    }
}
