using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
	[Serializable]
	public class RespuestaPreguntaAnalisis :DTBase
	{
		public int idPregunta { get; set; }
		public string textoPregunta { get; set; }
		public int idEscalaPonderacion { get; set; }
		public int cant1 { get; set; }
		public int cant2 { get; set; }
		public int cant3 { get; set; }
		public int cant4 { get; set; }
		public int cant5 { get; set; }

		public RespuestaPreguntaAnalisis()
		{
			
		}

		~RespuestaPreguntaAnalisis()
		{

		}

		public virtual void Dispose()
		{

		}
	}
}
