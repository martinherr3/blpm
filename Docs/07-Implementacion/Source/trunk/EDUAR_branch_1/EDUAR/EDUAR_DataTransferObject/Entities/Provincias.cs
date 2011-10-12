using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
    public class Provincias: DTBase
    {
        public decimal idProvincia { get; set; }
        public decimal idProvinciaTransaccional { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public decimal idPais { get; set; }
        public bool activo { get; set; }
    }
}
