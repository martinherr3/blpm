using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities.Reports
{
    [Serializable]
    public class FilIncidenciasAlumno : DTBase
    {
        public int idAlumno { get; set; }
        public int idCurso { get; set; }
		public int idCicloLectivo { get; set; }
        public int idPeriodo { get; set; }

        public FilIncidenciasAlumno()
        {
        }
    }
}