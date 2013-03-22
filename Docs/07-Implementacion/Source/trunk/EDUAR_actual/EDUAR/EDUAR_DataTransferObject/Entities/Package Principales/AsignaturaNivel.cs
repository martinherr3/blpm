using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
    [Serializable]
    public class AsignaturaNivel : DTBase
    {
        public int idAsignaturaNivel { get; set; }
        public int idAsignaturaNivelTransaccional { get; set; }
        public int cargaHoraria { get; set; }
        public Asignatura asignatura { get; set; }
        public Nivel nivel { get; set; }
        public Orientacion orientacion { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsignaturaNivel"/> class.
        /// </summary>
        public AsignaturaNivel()
        {
            asignatura = new Asignatura();
            nivel = new Nivel();
            orientacion = new Orientacion();
        }
    }
}
