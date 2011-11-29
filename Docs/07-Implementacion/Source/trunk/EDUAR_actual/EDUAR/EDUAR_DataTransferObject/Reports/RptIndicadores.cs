using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities.Reports
{
    [Serializable]
    public class RptIndicadores : DTBase
    {

        public string alumnoNombre { get; set; }
		public string alumnoApellido { get; set; }
        public decimal promedio { get; set; }
		public int sanciones { get; set; }
		public decimal inasistencias { get; set; }
		public int idCursoCicloLectivo { get; set; }

		public RptIndicadores()
        {
        }

    }
}
