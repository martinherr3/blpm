using System;

namespace EDUAR_Entities
{
    public class Provincias
    {
        private decimal dIdProvincia;
        public decimal idProvincia
        {
            get { return dIdProvincia; }
            set { dIdProvincia = value; }
        }
        private decimal dIdProvinciaTransaccional;
        public decimal idProvinciaTransaccional
        {
            get { return dIdProvinciaTransaccional; }
            set { dIdProvinciaTransaccional = value; }
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
        private decimal dIdPais;
        public decimal idPais
        {
            get { return dIdPais; }
            set { dIdPais = value; }
        }
        private Boolean bActivo;
        public Boolean activo
        {
            get { return bActivo; }
            set { bActivo = value; }
        }
    }
}
