using System;

namespace EDUAR_Entities
{
    public class ProcesosAutomaticos
    {
        private decimal dIdProcesoAutomatico;
        public decimal idProcesoAutomatico
        {
            get { return dIdProcesoAutomatico; }
            set { dIdProcesoAutomatico = value; }
        }
        private string szNombre;
        public string nombre
        {
            get { return szNombre; }
            set { szNombre = value; }
        }
        private string szDescripcion;
        public string descripcion
        {
            get { return szDescripcion; }
            set { szDescripcion = value; }
        }
        private Boolean bActivo;
        public Boolean activo
        {
            get { return bActivo; }
            set { bActivo = value; }
        }
    }
}
