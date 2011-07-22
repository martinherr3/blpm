using System;

namespace EDUAR_Entities
{
    public class ProcesosEjecutados
    {
        public decimal idProcesoEjecutado { get; set; }
        public DateTime fechaEjecucion { get; set; }
        public bool resultado { get; set; }
        public string descripcionError { get; set; }
        public decimal idProcesoAutomatico { get; set; }
    }
}
