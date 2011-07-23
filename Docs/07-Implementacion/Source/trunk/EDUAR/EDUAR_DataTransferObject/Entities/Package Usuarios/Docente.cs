
using System;
namespace EDUAR_Entities
{
    [Serializable]
    public class Docente : Personal
    {
        private int _IdDocente;
        public int idDocente
        {
            get { return _IdDocente; }
            set { _IdDocente = value; }
        }

        private int _IdDocenteTransaccional;
        public int idDocenteTransaccional
        {
            get { return _IdDocenteTransaccional; }
            set { _IdDocenteTransaccional = value; }
        }

    }
}
