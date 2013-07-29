using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities.Reports
{
	[Serializable]
	public class InformeIndicador : DTBase
	{
		public int idIndicador { get; set; }
		public string nombre { get; set; }
		public string nombreSP { get; set; }
		public bool invertirEscala { get; set; }
		public int parametroCantidad { get; set; }
		public int diasHastaPrincipal { get; set; }
		public int diasHastaIntermedio { get; set; }
		public int diasHastaSecundario { get; set; }
		public int verdeNivelPrincipal { get; set; }
		public int verdeNivelIntermedio { get; set; }
		public int verdeNivelSecundario { get; set; }
		public int rojoNivelPrincipal { get; set; }
		public int rojoNivelIntermedio { get; set; }
		public int rojoNivelSecundario { get; set; }

		public InformeIndicador()
		{
		}

		~InformeIndicador()
		{ }

		public virtual void Dispose()
		{

		}
	}
}
