using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities.Reports
{
    [Serializable]
    public class RptEncuestasPorEstado : DTBase
    {
        public string ambito { get; set; }
        public string status { get; set; }
        public int total { get; set; }

        public RptEncuestasPorEstado()
        {
        }

    }
}
