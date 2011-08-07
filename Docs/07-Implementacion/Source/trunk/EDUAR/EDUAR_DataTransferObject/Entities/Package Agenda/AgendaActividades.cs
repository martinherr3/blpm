using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
    [Serializable]
    public class AgendaActividades : DTBase
    {
        public int idAgendaActividad { get; set; }
        public string descripcion { get; set; }
        public bool activo { get; set; }
		public DateTime fechaCreacion { get; set; }
        public CursoCicloLectivo cursoCicloLectivo { get; set; }

        //ToDo: Agregar LISTAS con eventos 

        /// <summary>
        /// Initializes a new instance of the <see cref="AgendaActividades"/> class.
        /// </summary>
        public AgendaActividades()
        {
			cursoCicloLectivo = new CursoCicloLectivo();
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="AgendaActividades"/> is reclaimed by garbage collection.
        /// </summary>
        ~AgendaActividades()
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
