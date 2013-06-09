using System;
using EDUAR_Entities.Shared;
using System.Collections.Generic;

namespace EDUAR_Entities.Reports
{
    [Serializable]
    public class FilAnalisisAgrupados : DTBase
    {
        public List<CicloLectivo> listaCicloLectivo { get; set; }
        public List<Nivel> listaNiveles { get; set; }
        public List<Asignatura> listaAsignaturas { get; set; }
        public List<Alumno> listaAlumnos { get; set; }

        public FilAnalisisAgrupados()
        {
			listaAsignaturas = new List<Asignatura>();
            listaCicloLectivo = new List<CicloLectivo>();
            listaNiveles = new List<Nivel>();
            listaAlumnos = new List<Alumno>();
        }
    }
}