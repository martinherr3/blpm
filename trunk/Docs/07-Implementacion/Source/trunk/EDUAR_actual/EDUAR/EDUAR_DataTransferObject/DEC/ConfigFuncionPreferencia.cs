using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
	[Serializable]
	public class ConfigFuncionPreferencia : DTBase
	{
		public int idConfigFuncionPreferencia { get; set; }
		public int idIndicador { get; set; }
		public int idFuncionPreferencia { get; set; }
		public int idValorFuncionPreferencia { get; set; }
		public float valorDefault { get; set; }
	}
}
