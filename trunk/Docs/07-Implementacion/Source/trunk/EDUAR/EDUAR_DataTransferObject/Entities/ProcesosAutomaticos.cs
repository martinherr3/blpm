using System;

namespace EDUAR_Entities
{
    public class ProcesosAutomaticos
    {
        public decimal idProcesoAutomatico { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool activo { get; set; }
    }
}
