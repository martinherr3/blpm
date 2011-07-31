using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
    public class Paises: DTBase
    {
        public decimal idPais { get; set; }
        public decimal idPaisTransaccional { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool activo { get; set; }
    }
}
