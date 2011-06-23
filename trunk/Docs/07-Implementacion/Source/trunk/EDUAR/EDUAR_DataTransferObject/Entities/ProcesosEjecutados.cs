using System;
using System.Collections.Generic;
using System.Text;

namespace EDUAR_Entities
{
    [Serializable]
    public class ProcesosEjecutados
    {
        private Decimal dIdProcesoEjecutado;
        public Decimal idProcesoEjecutado
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
        private Decimal dIdProcesoAutomatico;
        public Decimal idProcesoAutomatico
        {
            get { return dIdProcesoAutomatico; }
            set { dIdProcesoAutomatico = value; }
        }
    }
}
