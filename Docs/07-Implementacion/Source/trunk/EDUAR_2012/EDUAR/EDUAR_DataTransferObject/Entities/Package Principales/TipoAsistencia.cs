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
        public int idTipoAsistencia { get; set; }
        public string descripcion { get; set; }
        public decimal valor { get; set; }
        public int idTipoAsistenciaTransaccional { get; set; }

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
