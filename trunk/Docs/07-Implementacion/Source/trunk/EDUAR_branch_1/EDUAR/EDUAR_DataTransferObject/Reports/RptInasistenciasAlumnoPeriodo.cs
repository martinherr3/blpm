using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities.Reports
{
    [Serializable]
    public class RptInasistenciasAlumnoPeriodo : DTBase
    {
        public string nombreAlumno { get; set; }
        public string curso { get; set; }
        public DateTime fechaInasistencia { get; set; }
        public string motivoInasistencia { get; set; }

        public RptInasistenciasAlumnoPeriodo()
        {
        }

    }
}