using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
	[Serializable]
	public class ValorFuncionPreferencia : DTBase
	{
		public int idValorFuncionPreferencia { get; set; }
		public string nombre { get; set; }
	}
}
