using System;
using EDUAR_Entities.Shared;
namespace EDUAR_Entities
{
	[Serializable]
	public class CicloLectivo : DTBase
	{
		public int idCicloLectivo { get; set; }
		public int idCicloLectivoTransaccional { get; set; }
		public string nombre { get; set; }
		public DateTime fechaInicio { get; set; }
		public DateTime fechaFin { get; set; }
		public bool activo { get; set; }

		public CicloLectivo()
		{ }

		~CicloLectivo()
		{ }

		public virtual void Dispose()
		{ }
	}
}
