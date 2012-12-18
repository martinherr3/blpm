using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
    [Serializable]
    public class TipoTutor : DTBase
    {
        public int idTipoTutor { get; set; }
        public int idTipoTutorTransaccional { get; set; }
        public string descripcion { get; set; }
    }
}
