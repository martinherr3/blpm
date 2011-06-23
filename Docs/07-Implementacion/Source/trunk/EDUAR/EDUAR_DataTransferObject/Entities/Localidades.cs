using System;
using System.Collections.Generic;
using System.Text;

namespace EDUAR_Entities
{
    [Serializable]
    public class Localidades
    {
        private Decimal dIdLocalidad;
        public Decimal idLocalidad
        {
            get { return dIdLocalidad; }
            set { dIdLocalidad = value; }
        }
        private Decimal dIdLocalidadTransaccional;
        public Decimal idLocalidadTransaccional
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
        private Decimal dIdProvincia;
        public Decimal idProvincia
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
