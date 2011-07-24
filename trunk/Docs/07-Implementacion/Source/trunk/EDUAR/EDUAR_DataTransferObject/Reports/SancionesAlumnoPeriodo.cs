using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDUAR_Entities.Reports
{
    public class SancionesAlumnoPeriodo
    {

        public string nombreAlumno { get; set; }
        public DateTime fechaSancion { get; set; }
        public int cantidad { get; set; }
        public string tipoSancion { get; set; }
        public string motivoSancion { get; set; }

        public SancionesAlumnoPeriodo()
        {
            nombreAlumno = string.Empty;
            fechaSancion = System.DateTime.Now;
            cantidad = 0;
            tipoSancion = string.Empty;
            motivoSancion = string.Empty;
        }

    }
}
