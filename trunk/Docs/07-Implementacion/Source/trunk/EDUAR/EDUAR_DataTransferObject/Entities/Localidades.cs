using System;

namespace EDUAR_Entities
{
    
    public class Localidades
    {
        private decimal dIdLocalidad;
        public decimal idLocalidad
        {
            get { return dIdLocalidad; }
            set { dIdLocalidad = value; }
        }
        private decimal dIdLocalidadTransaccional;
        public decimal idLocalidadTransaccional
        {
            get { return dIdLocalidadTransaccional; }
            set { dIdLocalidadTransaccional = value; }
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
        private decimal dIdProvincia;
        public decimal idProvincia
        {
            get { return dIdProvincia; }
            set { dIdProvincia = value; }
        }
        private Boolean bActivo;
        public Boolean activo
        {
            get { return bActivo; }
            set { bActivo = value; }
        }
    }
}
