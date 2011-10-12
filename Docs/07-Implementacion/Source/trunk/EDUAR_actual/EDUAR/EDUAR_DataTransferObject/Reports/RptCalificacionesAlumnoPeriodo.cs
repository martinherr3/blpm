using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities.Reports
{
    [Serializable]
    public class RptCalificacionesAlumnoPeriodo : DTBase
    {
        public string alumno { get; set; }
        public string curso { get; set; }
        public DateTime fecha { get; set; }
        public string asignatura { get; set; }
        public string calificacion { get; set; }
		//public string instancia { get; set; }
        
        public RptCalificacionesAlumnoPeriodo()
        {
        }
    }
}
