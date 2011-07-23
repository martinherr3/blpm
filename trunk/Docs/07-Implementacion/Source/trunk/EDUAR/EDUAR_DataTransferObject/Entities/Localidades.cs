using System;

namespace EDUAR_Entities
{
    [Serializable]
    public class Localidades
    {
        public decimal idLocalidad { get; set; }
        public decimal idLocalidadTransaccional { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public decimal idProvincia { get; set; }
        public bool activo { get; set; }
    }
}
