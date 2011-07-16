using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
    public class TipoTutor : DTBase
    {
        private int _idTipoTutor;

        public int idTipoTutor
        {
            get { return _idTipoTutor; }
            set { _idTipoTutor = value; }
        }

        private int _idTipoTutorTransaccional;

        public int idTipoTutorTransaccional
        {
            get { return _idTipoTutorTransaccional; }
            set { _idTipoTutorTransaccional = value; }
        }

        private string _descripcion;

        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

    }
}
