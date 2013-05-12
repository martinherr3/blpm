using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities.Reports
{
    [Serializable]
    public class RptPromedioCalificacionesPeriodo : DTBase
    {
        public string alumno { get; set; }
        public string periodo { get; set; }
        public string asignatura { get; set; }
        public double promedio { get; set; }

        public RptPromedioCalificacionesPeriodo()
        {
        }
    }
}
