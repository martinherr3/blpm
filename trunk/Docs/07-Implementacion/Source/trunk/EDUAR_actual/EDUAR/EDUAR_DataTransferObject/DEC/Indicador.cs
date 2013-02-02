using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities.DEC
{
	public class Indicador : DTBase
	{
		public int idIndicador { get; set; }
		public string nombre { get; set; }
		public float pesoDefault { get; set; }
		public string escala { get; set; }
		public float pesoMinimo { get; set; }
		public float pesoMaximo { get; set; }
		public bool maximiza { get; set; }
	}
}
