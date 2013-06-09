
using System;
using EDUAR_Entities.Shared;
namespace EDUAR_Entities
{
    [Serializable]
    public class Docente : Personal
    {
        private int _idDocente;
        public int idDocente
        {
            get { return _idDocente; }
            set { _idDocente = value; }
        }

        private int _IdDocenteTransaccional;
        public int idDocenteTransaccional
        {
            get { return _IdDocenteTransaccional; }
            set { _IdDocenteTransaccional = value; }
        }

    }
}
