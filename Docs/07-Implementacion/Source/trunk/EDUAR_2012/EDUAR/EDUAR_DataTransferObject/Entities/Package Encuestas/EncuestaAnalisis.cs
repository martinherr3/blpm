using System;
using System.Collections.Generic;
using EDUAR_Entities.Security;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
	[Serializable]
	public class EncuestaAnalisis : DTBase
	{
		public int idEncuesta { get; set; }
		public Persona usuario { get; set; }
		public DateTime fechaCreacion { get; set; }
		public DateTime fechaModificacion { get; set; }
		public DateTime? fechaLanzamiento { get; set; }
		public DateTime? fechaVencimiento { get; set; }
		public AmbitoEncuesta ambito { get; set; }
		public string nombreEncuesta { get; set; }
		public string objetivo { get; set; }
		public bool activo { get; set; }
		public List<Pregunta> preguntas { get; set; }
		public List<DTRol> listaRoles { get; set; }
		public CursoCicloLectivo curso { get; set; }
		public AsignaturaCicloLectivo asignatura { get; set; }
		public int nroRespondidas { get; set; }
		public int nroLanzadas { get; set; }
		public int nroPendientes { get; set; }
		public int nroExpiradas { get; set; }

		public EncuestaAnalisis()
		{
			usuario = new Persona();
			preguntas = new List<Pregunta>();
			ambito = new AmbitoEncuesta();
			listaRoles = new List<DTRol>();
			curso = new CursoCicloLectivo();
			asignatura = new AsignaturaCicloLectivo();
		}

		~EncuestaAnalisis()
		{

		}

		public virtual void Dispose()
		{

		}
	}
}
