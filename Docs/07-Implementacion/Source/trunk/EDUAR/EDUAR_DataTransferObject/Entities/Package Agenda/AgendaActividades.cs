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
		public string usuario { get; set; }
		public DateTime fechaCreacion { get; set; }
        public CursoCicloLectivo cursoCicloLectivo { get; set; }
		//public List<EventoAgenda> listaEventos { get; set; }
		public List<Evaluacion> listaEvaluaciones { get; set; }
		public List<Excursion> listaExcursiones { get; set; }
		public List<Reunion> listaReuniones { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgendaActividades"/> class.
        /// </summary>
        public AgendaActividades()
        {
			idAgendaActividad = 0;
			activo = true;
			cursoCicloLectivo = new CursoCicloLectivo();
			//listaEventos = new List<EventoAgenda>();
			listaEvaluaciones = new List<Evaluacion>();
			listaExcursiones = new List<Excursion>();
			listaReuniones = new List<Reunion>();
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
