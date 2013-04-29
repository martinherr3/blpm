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
        public decimal relevancia { get; set; }
        public string cadenaSeleccion { get; set; }

        public List<ValoresSeleccionados> valoresSeleccionados { get; set; }
        
		public decimal porcentaje { get; set; }
		public int respuestasEsperadas { get; set; }
		public int respuestasObtenidas { get; set; }

		public RespuestaPreguntaAnalisis()
		{
            valoresSeleccionados = new List<ValoresSeleccionados>();
		}

		~RespuestaPreguntaAnalisis()
		{

		}

		public virtual void Dispose()
		{

		}
	}
}
