using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
	[Serializable]
	public class Asistencia : DTBase
	{
		public DateTime fecha { get; set; }
		public int idAsistencia { get; set; }
		public int idAsistenciaTransaccional { get; set; }
		public TipoAsistencia tipoAsistencia { get; set; }
		public AlumnoCursoCicloLectivo alumno { get; set; }

		public Asistencia()
		{
			alumno = new AlumnoCursoCicloLectivo();
			tipoAsistencia = new TipoAsistencia();
		}

		~Asistencia()
		{

		}

		public virtual void Dispose()
		{

		}
	}
}
