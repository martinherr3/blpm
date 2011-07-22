using System;

namespace EDUAR_Entities
{
    public class Paises
    {
        private decimal dIdPais;
        public decimal idPais
        {
            get { return dIdPais; }
            set { dIdPais = value; }
        }
        private decimal dIdPaisTransaccional;
        public decimal idPaisTransaccional
        {
            get { return dIdPaisTransaccional; }
            set { dIdPaisTransaccional = value; }
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
