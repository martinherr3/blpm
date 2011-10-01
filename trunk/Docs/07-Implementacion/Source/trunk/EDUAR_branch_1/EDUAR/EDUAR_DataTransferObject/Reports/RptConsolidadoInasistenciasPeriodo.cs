using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities.Reports
{
    [Serializable]
    public class RptConsolidadoInasistenciasPeriodo : DTBase
    {
        public string alumno { get; set; }
        public string periodo { get; set; }
        public string inasistencias { get; set; }

        public RptConsolidadoInasistenciasPeriodo()
        {
        }
    }
}
