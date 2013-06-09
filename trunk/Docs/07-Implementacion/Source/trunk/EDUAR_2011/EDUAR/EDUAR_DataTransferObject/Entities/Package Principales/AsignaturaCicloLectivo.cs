using System;
using System.Collections.Generic;
using EDUAR_Entities.Shared;
namespace EDUAR_Entities
{
	[Serializable]
	public class AsignaturaCicloLectivo : DTBase
	{
		public List<Contenido> listaContenidos { get; set; }
		public CursoCicloLectivo cursoCicloLectivo { get; set; }
		public Docente docente { get; set; }
		public Asignatura asignatura { get; set; }
		public DiasHorarios horario { get; set; }
		public int idAsignaturaCicloLectivo { get; set; }
		public int idAsignaturaCicloLectivoTransaccional { get; set; }

		public AsignaturaCicloLectivo()
		{
			listaContenidos = new List<Contenido>();
			cursoCicloLectivo = new CursoCicloLectivo();
			horario = new DiasHorarios();
			docente = new Docente();
			asignatura = new Asignatura();
		}
	}
}
