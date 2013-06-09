using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities.Reports
{
	[Serializable]
	public class RptAnalisisCicloLectivoCursoAsignaturaAlumno : DTBase
	{
		public string ciclolectivo { get; set; }
		public string curso { get; set; }
		public string asignatura { get; set; }
		public string alumno { get; set; }
		public string promedio { get; set; }

		public RptAnalisisCicloLectivoCursoAsignaturaAlumno()
		{
		}
	}
}