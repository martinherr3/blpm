using System.Collections.Generic;
using System;
using EDUAR_Entities.Shared;
namespace EDUAR_Entities
{
	[Serializable]
	public class EstadoNovedad : DTBase
	{
		public int idEstadoNovedad { get; set; }
		public string nombre { get; set; }
		public bool esFinal { get; set; }

		public EstadoNovedad()
		{

		}

		~EstadoNovedad()
		{

		}

		public virtual void Dispose()
		{

		}
	}
}
