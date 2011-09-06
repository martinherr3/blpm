﻿using System;
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

        public FilInasistenciasAlumnoPeriodo()
        {
        }
    }
}