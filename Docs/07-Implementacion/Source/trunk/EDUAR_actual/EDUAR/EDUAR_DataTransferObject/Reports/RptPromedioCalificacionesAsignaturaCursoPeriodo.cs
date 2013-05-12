using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities.Reports
{
    [Serializable]
    public class RptPromedioCalificacionesAsignaturaCursoPeriodo : DTBase
    {
        public string periodo { get; set; }
        public string asignatura { get; set; }
        public double promedio { get; set; }

        public RptPromedioCalificacionesAsignaturaCursoPeriodo()
        {
        }
    }
}
