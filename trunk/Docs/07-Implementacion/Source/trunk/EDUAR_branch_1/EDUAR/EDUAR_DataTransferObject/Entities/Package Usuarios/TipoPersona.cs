using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
	[Serializable]
	public class TipoPersona : DTBase
	{
		public int idTipoPersona { get; set; }
		public string nombre { get; set; }
	}
}
