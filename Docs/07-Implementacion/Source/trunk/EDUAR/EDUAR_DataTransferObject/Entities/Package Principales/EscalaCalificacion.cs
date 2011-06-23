using System;
using System.Collections.Generic;
using System.Text;

namespace EDUAR_Entities
{
    [Serializable]
    public class EscalaCalificacion
    {
        private Decimal dIdEscalaCalificacion;
        public Decimal idEscalaCalificacion
        {
            get { return dIdEscalaCalificacion; }
            set { dIdEscalaCalificacion = value; }
        }
        private Decimal dIdEscalaCalificacionTransaccional;
        public Decimal idEscalaCalificacionTransaccional
        {
            get { return dIdEscalaCalificacionTransaccional; }
            set { dIdEscalaCalificacionTransaccional = value; }
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
        private Decimal dIdTipoEscalaCalificacion;
        public Decimal idTipoEscalaCalificacion
        {
            get { return dIdTipoEscalaCalificacion; }
            set { dIdTipoEscalaCalificacion = value; }
        }
    }
}
