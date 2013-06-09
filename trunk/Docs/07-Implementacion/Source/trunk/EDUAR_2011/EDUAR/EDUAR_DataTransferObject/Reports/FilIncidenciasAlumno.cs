using System;
using EDUAR_Entities.Shared;
using System.Collections.Generic;

namespace EDUAR_Entities.Reports
{
	[Serializable]
	public class FilIncidenciasAlumno : DTBase
	{
		public int idAlumno { get; set; }
		public int idCurso { get; set; }
		public int idCicloLectivo { get; set; }
		public int idPeriodo { get; set; }
		public List<MotivoSancion> listaMotivoSancion { get; set; }
		public List<TipoSancion> listaTipoSancion { get; set; }
		public List<TipoAsistencia> listaTiposAsistencia { get; set; }

		public FilIncidenciasAlumno()
		{
			listaMotivoSancion = new List<MotivoSancion>();
			listaTipoSancion = new List<TipoSancion>();
			listaTiposAsistencia = new List<TipoAsistencia>();
		}
	}
}