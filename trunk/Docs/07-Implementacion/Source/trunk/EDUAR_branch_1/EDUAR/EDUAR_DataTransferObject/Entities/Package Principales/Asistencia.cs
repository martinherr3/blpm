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
		public Alumno unAlumno { get; set; }

		public Asistencia()
		{
			unAlumno = new Alumno();
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
