using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
    public class AlumnoCurso : DTBase
    {
        public int idAlumnoCurso { get; set; }
        public int idAlumnoCursoTransaccional { get; set; }
        public Alumno alumno { get; set; }
		/// <summary>
		/// se desalienta la utilización de esta propiedad en favor de cursoCicloLectivo
		/// </summary>
        public Curso curso { get; set; }
		public CursoCicloLectivo cursoCicloLectivo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlumnoCurso"/> class.
        /// </summary>
        public AlumnoCurso()
        {
            idAlumnoCurso = 0;
            idAlumnoCursoTransaccional = 0;
            alumno = new Alumno();
            curso = new Curso();
			cursoCicloLectivo = new CursoCicloLectivo();
        }

        public AlumnoCurso(int idCurso)
        {
            alumno = new Alumno();
            curso = new Curso();
            curso.idCurso = idCurso;
			cursoCicloLectivo = new CursoCicloLectivo();
        }
    }
}
