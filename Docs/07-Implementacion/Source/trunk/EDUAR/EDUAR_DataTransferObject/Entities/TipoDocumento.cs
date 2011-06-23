using System;
using System.Collections.Generic;
using System.Text;

namespace EDUAR_Entities
{
    [Serializable]
    public class TipoDocumento
    {
        private Decimal dIdTipoDocumento;
        public Decimal idTipoDocumento
        {
            get { return dIdTipoDocumento; }
            set { dIdTipoDocumento = value; }
        }
        private Decimal dIdTipoDocumentoTransaccional;
        public Decimal idTipoDocumentoTransaccional
        {
            get { return dIdTipoDocumentoTransaccional; }
            set { dIdTipoDocumentoTransaccional = value; }
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
