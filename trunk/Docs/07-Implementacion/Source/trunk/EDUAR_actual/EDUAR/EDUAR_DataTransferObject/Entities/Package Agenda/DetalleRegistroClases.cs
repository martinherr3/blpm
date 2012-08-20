using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
	[Serializable]
	public class DetalleRegistroClases : DTBase
	{
		public int idDetalleRegistroClases { get; set; }
		public TemaContenido temaContenido { get; set; }
		public int porcentaje { get; set; }
		public int idRegistroClases { get; set; }
		public TipoRegistroClases tipoClase { get; set; }

		public DetalleRegistroClases()
		{
			temaContenido = new TemaContenido();
			tipoClase = new TipoRegistroClases();
		}

		~DetalleRegistroClases()
		{

		}

		public virtual void Dispose()
		{

		}
	}
}
