using System;
using EDUAR_Entities.Shared;
using System.Collections.Generic;

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
		//public List<TipoAsistencia> listaTiposAsistencia { get; set; }

		public RptConsolidadoSancionesPeriodo()
		{
			//listaTiposAsistencia = new List<TipoAsistencia>();
		}
	}
}
