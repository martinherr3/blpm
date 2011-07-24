using System;
using System.Collections.Generic;
using EDUAR_Entities.Security;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities.Reports
{
    public class RptAccesos : DTBase
    {
        public string titulo { get; set; }
        public DateTime fecha { get; set; }
        public DateTime hora { get; set; }
        public int idAcceso { get; set; }
        public string username { get; set; }
        public int idPagina { get; set; }
        public DateTime fechaDesde { get; set; }
        public DateTime fechaHasta { get; set; }
        public List<DTRol> listaRoles { get; set; }
        public string rol { get; set; }

        public RptAccesos()
        {
            listaRoles = new List<DTRol>();
        }

        ~RptAccesos()
        { }
    }
}
