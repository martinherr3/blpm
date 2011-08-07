using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
	public class CursoCicloLectivo : DTBase
	{
		public int idCursoCicloLectivo { get; set; }
		public int idCurso { get; set; }
		public int idCicloLectivo { get; set; }
		public Curso curso { get; set; }
		public CicloLectivo cicloLectivo { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CursoCicloLectivo"/> class.
		/// </summary>
		public CursoCicloLectivo()
		{
			curso = new Curso();
			cicloLectivo = new CicloLectivo();
		}
	}
}
