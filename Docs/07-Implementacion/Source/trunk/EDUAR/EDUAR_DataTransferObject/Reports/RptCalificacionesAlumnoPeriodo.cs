using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDUAR_Entities.Reports
{
    public class RptCalificacionesAlumnoPeriodo
    {

        public string nombreAlumno { get; set; }
        public string curso { get; set; }
        public DateTime fecha { get; set; }
        public string asignatura { get; set; }
        public string calificacion { get; set; }
        public string instancia { get; set; }

        public RptCalificacionesAlumnoPeriodo()
        {
            nombreAlumno = string.Empty;
            curso = string.Empty;
            fecha = System.DateTime.Now;
            asignatura = string.Empty;
            calificacion = string.Empty;
            instancia = string.Empty;
        }

       
    }
}
