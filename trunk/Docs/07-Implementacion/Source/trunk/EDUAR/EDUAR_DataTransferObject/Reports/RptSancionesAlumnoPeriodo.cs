using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities.Reports
{
    [Serializable]
    public class RptSancionesAlumnoPeriodo : DTBase
    {

        public string alumno { get; set; }
        public DateTime fecha { get; set; }
        public int cantidad { get; set; }
        public string tipo { get; set; }
        public string motivo { get; set; }

        public RptSancionesAlumnoPeriodo()
        {
        }

    }
}
