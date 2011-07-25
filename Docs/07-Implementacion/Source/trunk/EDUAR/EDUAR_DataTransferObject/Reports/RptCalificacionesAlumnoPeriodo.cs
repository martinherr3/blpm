using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities.Reports
{
    public class RptCalificacionesAlumnoPeriodo : DTBase
    {
        public string nombreAlumno { get; set; }
        public string curso { get; set; }
        public DateTime fecha { get; set; }
        public string asignatura { get; set; }
        public string calificacion { get; set; }
        public string instancia { get; set; }
        
        public int idAlumno { get; set; }
        public int idCurso { get; set; }
        public int idAsignatura { get; set; }
        public int idInstanciaEvaluacion { get; set; }
        public DateTime fechaDesde { get; set; }
        public DateTime fechaHasta { get; set; }

        public RptCalificacionesAlumnoPeriodo()
        {
        }
    }
}
