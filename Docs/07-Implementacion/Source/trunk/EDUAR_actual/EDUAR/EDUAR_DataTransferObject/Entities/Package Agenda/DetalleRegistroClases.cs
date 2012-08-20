using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
	[Serializable]
	public class DetalleRegistroClases : DTBase
	{
		public int idDetalleRegistroClases { get; set; }
		public int porcentaje { get; set; }
		public int idRegistroClases { get; set; }
		public TemaContenido temaContenido { get; set; }

		public DetalleRegistroClases()
		{
			temaContenido = new TemaContenido();
		}

		~DetalleRegistroClases()
		{

		}

		public virtual void Dispose()
		{

		}
	}
}
