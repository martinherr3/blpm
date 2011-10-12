using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities.Reports
{
	[Serializable]
	public class RptRendimientoHistorico : DTBase
	{
		public string alumno { get; set; }
		public string periodo { get; set; }
		//public DateTime fecha { get; set; }
		public string asignatura { get; set; }
		public string promedio { get; set; }
		//public string instancia { get; set; }

		public RptRendimientoHistorico()
		{
		}
	}
}