using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
	[Serializable]
    public class Modulo: DTBase
    {
		public int idModulo { get; set; }
		public DateTime horaInicio { get; set; }
		public DateTime horaFinalizacion { get; set; }
		public int idDiaHorario { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Modulo"/> class.
		/// </summary>
		public Modulo()
		{
		}

		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the
		/// <see cref="Modulo"/> is reclaimed by garbage collection.
		/// </summary>
		~Modulo()
		{

		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources
		/// </summary>
		public virtual void Dispose()
		{

		}
    }
}
