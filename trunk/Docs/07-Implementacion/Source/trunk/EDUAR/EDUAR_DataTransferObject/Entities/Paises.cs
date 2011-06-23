using System;
using System.Collections.Generic;
using System.Text;

namespace EDUAR_Entities
{
    [Serializable]
    public class Paises
    {
        private Decimal dIdPais;
        public Decimal idPais
        {
            get { return dIdPais; }
            set { dIdPais = value; }
        }
        private Decimal dIdPaisTransaccional;
        public Decimal idPaisTransaccional
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
