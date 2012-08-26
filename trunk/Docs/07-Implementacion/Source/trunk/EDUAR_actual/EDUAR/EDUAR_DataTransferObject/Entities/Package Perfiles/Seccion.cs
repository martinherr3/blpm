using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDUAR_Entities.Shared;
using EDUAR_Entities.Security;

namespace EDUAR_Entities
{
    [Serializable]
    public class Seccion : DTBase
    {
        public int idSeccion { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public List<DTRol> listaRoles { get; set; }
        public List<Seccion> listaSecciones { get; set; }
    }
}
