using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDUAR_Entities
{
    public class TipoEventoAgenda
    {
        public int idTipoEventoAgenda { get; set; }
        public string descripcion { get; set; }

        public TipoEventoAgenda()
        {
            descripcion = string.Empty;
        }
    }
}
