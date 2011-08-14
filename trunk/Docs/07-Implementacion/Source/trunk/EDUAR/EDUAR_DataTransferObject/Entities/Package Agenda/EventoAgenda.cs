using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDUAR_Entities.Shared;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_Entities
{
	[Serializable]
    public class EventoAgenda : DTBase
    {
		public int idAgendaActividad { get; set; }
		public int idEventoAgenda { get; set; }
        public TipoEventoAgenda tipoEventoAgenda { get; set; }
        public Persona usuario { get; set; }
        public DateTime fechaEvento { get; set; }
		public DateTime fechaAlta { get; set; }
		public DateTime fechaModificacion { get; set; }
        public string descripcion { get; set; }
        public bool activo { get; set; }
		//public  evento<DTBase> { get; set; }
		//public AgendaActividades agendaActividades { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="EventoAgenda"/> class.
		/// </summary>
        public EventoAgenda()
        {
			idEventoAgenda = 0;
            tipoEventoAgenda = new TipoEventoAgenda();
			//agendaActividades = new AgendaActividades();
            usuario = new Persona();
			//fechaEvento = new DateTime();
			//fechaModificacion = new DateTime();
			//descripcion = string.Empty;
            activo = true;
			
			//switch(tipoEventoAgenda.idTipoEventoAgenda)
			//{
			//    case (int)enumEventoAgendaType.Evaluacion:
			//        evento = new Evaluacion();    
			//        break;
			//    case (int)enumEventoAgendaType.Reunion:
			//        evento = new Reunion();
			//        break;
			//    case (int)enumEventoAgendaType.Excursion:
			//        evento = new Excursion();    
			//        break;
			//    default:
			//        evento = null;
			//        break;
			//}

        }
    }
}
