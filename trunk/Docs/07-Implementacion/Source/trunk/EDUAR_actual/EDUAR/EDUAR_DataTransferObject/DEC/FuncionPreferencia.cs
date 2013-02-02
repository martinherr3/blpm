using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
	public class FuncionPreferencia : DTBase
	{
		public int idFuncionPreferencia { get; set; }
		public string nombre { get; set; }
		public string ayuda { get; set; }
	}
}
