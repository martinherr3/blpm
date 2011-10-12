using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities.Reports
{
    [Serializable]
    public class FilPromedioCalificacionesPeriodo : DTBase
    {
        public int idAlumno { get; set; }
        public int idAsignatura { get; set; }
        public int idCurso { get; set; }
		public int idCicloLectivo { get; set; }
        public int idPeriodo { get; set; }

        public FilPromedioCalificacionesPeriodo()
        {
        }
    }
}
