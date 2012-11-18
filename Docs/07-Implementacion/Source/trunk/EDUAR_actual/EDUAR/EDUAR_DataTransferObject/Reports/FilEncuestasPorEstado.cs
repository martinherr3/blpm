using System;
using EDUAR_Entities.Shared;
using System.Collections.Generic;

namespace EDUAR_Entities.Reports
{
	[Serializable]
	public class FilEncuestasPorEstado : DTBase
	{
		public int idCurso { get; set; }
		public int idAsignatura { get; set; }
		public List<AmbitoEncuesta> listaAmbitos { get; set; }

        public FilEncuestasPorEstado()
		{
			listaAmbitos = new List<AmbitoEncuesta>();
		}
	}
}