using System;

namespace EDUAR_Entities
{
    public class ProcesosEjecutados
    {
        private decimal dIdProcesoEjecutado;
        public decimal idProcesoEjecutado
        {
            get { return dIdProcesoEjecutado; }
            set { dIdProcesoEjecutado = value; }
        }
        private DateTime dtFechaEjecucion;
        public DateTime fechaEjecucion
        {
            get { return dtFechaEjecucion; }
            set { dtFechaEjecucion = value; }
        }
        private Boolean bResultado;
        public Boolean resultado
        {
            get { return bResultado; }
            set { bResultado = value; }
        }
        private string szDescripcionError;
        public string descripcionError
        {
            get { return szDescripcionError; }
            set { szDescripcionError = value; }
        }
        private decimal dIdProcesoAutomatico;
        public decimal idProcesoAutomatico
        {
            get { return dIdProcesoAutomatico; }
            set { dIdProcesoAutomatico = value; }
        }
    }
}
