using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities.Reports
{
    [Serializable]
    public class RptAccesos : DTBase
    {
        public string pagina { get; set; }
        public DateTime fecha { get; set; }
        public string rol { get; set; }
        public int accesos { get; set; }

        public RptAccesos()
        {
        }

        ~RptAccesos()
        { }
    }
}
