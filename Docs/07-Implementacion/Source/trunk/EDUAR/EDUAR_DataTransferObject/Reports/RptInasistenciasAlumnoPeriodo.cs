using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities.Reports
{
    public class RptInasistenciasAlumnoPeriodo : DTBase
    {
        public string nombreAlumno { get; set; }
        public DateTime fechaInasistencia { get; set; }
        public string motivoInasistencia { get; set; }

        public RptInasistenciasAlumnoPeriodo()
        {
            nombreAlumno = string.Empty;
            fechaInasistencia = System.DateTime.Now;
            motivoInasistencia = string.Empty;
        }

    }
}