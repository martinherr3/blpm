using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
	public class ProcesosAutomaticos : DTBase
	{
		public decimal idProcesoAutomatico { get; set; }
		public string nombre { get; set; }
		public string descripcion { get; set; }
		public bool activo { get; set; }
		public string email { get; set; }
	}
}
