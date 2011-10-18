using System;
using EDUAR_Entities.Shared;
using System.Collections.Generic;

namespace EDUAR_Entities.Reports
{
	[Serializable]
	public class FilSancionesAlumnoPeriodo : DTBase
	{
		public int idAlumno { get; set; }
		public int idCurso { get; set; }
		public int idCicloLectivo { get; set; }
		public DateTime fechaDesde { get; set; }
		public DateTime fechaHasta { get; set; }
		public string username { get; set; }
		public List<TipoSancion> listaTipoSancion { get; set; }
		public List<MotivoSancion> listaMotivoSancion { get; set; }
		
		public FilSancionesAlumnoPeriodo()
		{
			listaTipoSancion = new List<TipoSancion>();
			listaMotivoSancion = new List<MotivoSancion>();
		}
	}
}