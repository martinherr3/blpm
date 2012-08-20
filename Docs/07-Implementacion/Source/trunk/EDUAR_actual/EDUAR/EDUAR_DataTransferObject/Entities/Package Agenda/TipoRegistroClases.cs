using EDUAR_Entities.Shared;
using System;
namespace EDUAR_Entities
{
	[Serializable]
	public class TipoRegistroClases : DTBase
	{
		public int idTipoRegistroClases { get; set; }
		public string nombre { get; set; }

		public TipoRegistroClases()
		{
			nombre = string.Empty;
		}

		~TipoRegistroClases()
		{

		}

		public virtual void Dispose()
		{

		}
	}
}