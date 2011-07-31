using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
    public class Modulo: DTBase
    {
        private int _idModulo;

        public int idModulo
        {
            get { return _idModulo; }
            set { _idModulo = value; }
        }
        private DateTime _horaInicio;

        public DateTime horaInicio
        {
            get { return _horaInicio; }
            set { _horaInicio = value; }
        }
        private DateTime _horaFinalizacion;

        public DateTime horaFinalizacion
        {
            get { return _horaFinalizacion; }
            set { _horaFinalizacion = value; }
        }

        private int _idDiaHorario;

        public int idDiaHorario
        {
            get { return _idDiaHorario; }
            set { _idDiaHorario = value; }
        }
 
    }
}
