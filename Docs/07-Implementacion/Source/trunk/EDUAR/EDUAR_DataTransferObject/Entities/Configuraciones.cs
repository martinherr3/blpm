using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
    public class Configuraciones: DTBase
    {
        public int idConfiguracion { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string valor { get; set; }
        public bool activo { get; set; }
    }
}
