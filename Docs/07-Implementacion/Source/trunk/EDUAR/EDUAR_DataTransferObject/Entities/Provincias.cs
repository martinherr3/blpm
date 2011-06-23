using System;
using System.Collections.Generic;
using System.Text;

namespace EDUAR_Entities
{
    [Serializable]
    public class Provincias
    {
        private Decimal dIdProvincia;
        public Decimal idProvincia
        {
            get { return dIdProvincia; }
            set { dIdProvincia = value; }
        }
        private Decimal dIdProvinciaTransaccional;
        public Decimal idProvinciaTransaccional
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
        private Decimal dIdPais;
        public Decimal idPais
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
