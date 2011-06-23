using System;
using System.Collections.Generic;
using System.Text;

namespace EDUAR_Entities
{
    [Serializable]
    public class Docente : Personal
    {
        private Decimal _IdDocente;
        public Decimal idDocente
        {
            get { return _IdDocente; }
            set { _IdDocente = value; }
        }

        private Decimal _IdDocenteTransaccional;
        public Decimal idDocenteTransaccional
        {
            get { return _IdDocenteTransaccional; }
            set { _IdDocenteTransaccional = value; }
        }

    }
}
