using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDUAR_Entities.Shared;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_Entities
{
	[Serializable]
	public class EventoAgenda: DTBase
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
		//Se utilizan para filtrar
		public DateTime fechaEventoDesde { get; set; }
		public DateTime fechaEventoHasta { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="EventoAgenda"/> class.
		/// </summary>
		public EventoAgenda()
		{
			idEventoAgenda = 0;
			tipoEventoAgenda = new TipoEventoAgenda();
			usuario = new Persona();
			activo = true;
		}
	}
}
