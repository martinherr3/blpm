using System;
using EDUAR_Entities.Shared;
using System.Collections.Generic;

namespace EDUAR_Entities.Reports
{
	[Serializable]
	public class FilCalificacionesAlumnoPeriodo : DTBase
	{
		public int idAlumno { get; set; }
		public int idCurso { get; set; }
		public int idNivel { get; set; }
		public int idCicloLectivo { get; set; }
		public int idAsignatura { get; set; }
		public int idInstanciaEvaluacion { get; set; }
		public int idPeriodo { get; set; }
		public DateTime fechaDesde { get; set; }
		public DateTime fechaHasta { get; set; }
		public string username { get; set; }
		public List<Asignatura> listaAsignaturas { get; set; }
		public List<CicloLectivo> listaCicloLectivo { get; set; }
		
		public FilCalificacionesAlumnoPeriodo()
		{
			listaAsignaturas = new List<Asignatura>();
			listaCicloLectivo = new List<CicloLectivo>();
		}
	}
}