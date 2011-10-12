using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
    [Serializable]
    public class Pagina : DTBase
    {
        public int idPagina { get; set; }
        public string titulo { get; set; }
        public string url { get; set; }
    }
}
