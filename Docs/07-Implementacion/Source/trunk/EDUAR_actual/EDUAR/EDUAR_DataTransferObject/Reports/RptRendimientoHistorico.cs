using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities.Reports
{
	[Serializable]
	public class RptRendimientoHistorico : DTBase
	{
	    public string asignatura { get; set; }	
        public string ciclolectivo { get; set; }
        public string curso { get; set; }
        public double promedio { get; set; }

		public RptRendimientoHistorico()
		{
		}
	}
}