using System;
using System.Collections.Generic;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities.Reports
{
	[Serializable]
	public class FilInasistenciasAlumnoPeriodo : DTBase
	{
		public int idAlumno { get; set; }
		public int idCurso { get; set; }
		public int idCicloLectivo { get; set; }
		public DateTime fechaDesde { get; set; }
		public DateTime fechaHasta { get; set; }
		public string username { get; set; }
		public List<MotivoSancion> listaMotivoSancion { get; set; }
		public List<TipoSancion> listaTipoSancion { get; set; }
		public List<TipoAsistencia> listaTiposAsistencia { get; set; }

		public FilInasistenciasAlumnoPeriodo()
		{
			listaMotivoSancion = new List<MotivoSancion>();
			listaTipoSancion = new List<TipoSancion>();
			listaTiposAsistencia = new List<TipoAsistencia>();
		}
	}
}