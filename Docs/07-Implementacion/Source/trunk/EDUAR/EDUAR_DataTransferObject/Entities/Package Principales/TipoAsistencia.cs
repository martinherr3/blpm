using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
    [Serializable]
    public class TipoAsistencia: DTBase
    {
        private int _idTipoAsistencia;

        public int idTipoAsistencia
        {
            get { return _idTipoAsistencia; }
            set { _idTipoAsistencia = value; }
        }
        
        private string _descripcion;

        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        private decimal _valor;

        public decimal valor
        { 
            get { return _valor; }
            set { _valor = value; }    
        }

        private int _idTipoAsistenciaTransaccional;

        public int idTipoAsistenciaTransaccional
        {
            get { return _idTipoAsistenciaTransaccional; }
            set { _idTipoAsistenciaTransaccional = value; }
        }

        public TipoAsistencia()
        {

        }

        ~TipoAsistencia()
        {

        }

        public virtual void Dispose()
        {

        }

    }
}
