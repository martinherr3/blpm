using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
	public class AlumnoCursoCicloLectivo : DTBase
	{
		public int idAlumnoCursoCicloLectivo { get; set; }
		public int idAlumnoCursoCicloLectivoTransaccional { get; set; }
		public Alumno alumno { get; set; }
		public CursoCicloLectivo cursoCicloLectivo { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="AlumnoCursoCicloLectivo"/> class.
		/// </summary>
		public AlumnoCursoCicloLectivo()
		{
			alumno = new Alumno();
			cursoCicloLectivo = new CursoCicloLectivo();
		}
	}
}

