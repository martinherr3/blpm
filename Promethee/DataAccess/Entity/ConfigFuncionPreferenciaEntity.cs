using System;

namespace DataAccess.Entity
{
	[Serializable]
	public class ConfigFuncionPreferenciaEntity
	{
		public int idConfigFuncionPreferencia { get; set; }
		public int idCriterio { get; set; }
		public int idFuncionPreferencia { get; set; }
		public int idValorFuncionPreferencia { get; set; }
		public decimal valorDefault { get; set; }
	}
}
