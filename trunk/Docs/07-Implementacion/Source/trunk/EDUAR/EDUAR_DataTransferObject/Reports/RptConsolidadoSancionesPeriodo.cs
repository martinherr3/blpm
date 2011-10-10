using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities.Reports
{
    [Serializable]
    public class RptConsolidadoSancionesPeriodo : DTBase
    {
        public string alumno { get; set; }
        public string periodo { get; set; }
        public string sanciones { get; set; }
        public string tipo { get; set; }
        public string motivo { get; set; }


        public RptConsolidadoSancionesPeriodo()
        {
        }
    }
}
