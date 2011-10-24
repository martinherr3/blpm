using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
	[Serializable]
	public class ValoresEscalaCalificacion : DTBase
	{
		public decimal idValorEscalaCalificacion { get; set; }
		public decimal idValorEscalaCalificacionTransaccional { get; set; }
		public string nombre { get; set; }
		public string descripcion { get; set; }
		public string valor { get; set; }
		public bool activo { get; set; }
		public bool aprobado { get; set; }

		public ValoresEscalaCalificacion()
		{ }

		~ValoresEscalaCalificacion()
		{ }

		public virtual void Dispose()
		{
		}
	}
}
