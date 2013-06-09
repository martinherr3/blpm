using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities.Reports
{
    [Serializable]
    public class RptInasistenciasAlumnoPeriodo : DTBase
    {
        public string alumno { get; set; }
        public string curso { get; set; }
        public DateTime fecha { get; set; }
        public string motivo { get; set; }

        public RptInasistenciasAlumnoPeriodo()
        {
        }
    }
}