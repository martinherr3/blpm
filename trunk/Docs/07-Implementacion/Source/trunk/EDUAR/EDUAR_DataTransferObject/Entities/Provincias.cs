using System;

namespace EDUAR_Entities
{
    public class Provincias
    {
        public decimal idProvincia { get; set; }
        public decimal idProvinciaTransaccional { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public decimal idPais { get; set; }
        public bool activo { get; set; }
    }
}
