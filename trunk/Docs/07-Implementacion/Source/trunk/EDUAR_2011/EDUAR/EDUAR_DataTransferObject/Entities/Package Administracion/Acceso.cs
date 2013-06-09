using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
    [Serializable]
    public class Acceso : DTBase
    {
        public int idAcceso { get; set; }
        public Pagina pagina { get; set; }
        public DateTime fecha { get; set; }
        public DateTime hora { get; set; }
        public string usuario { get; set; }

        public Acceso()
        {
            idAcceso = 0;
            pagina = new Pagina();
        }
    }
}
