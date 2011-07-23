using System;

namespace EDUAR_Entities
{
    [Serializable]
    public class EscalaCalificacion
    {
        private decimal dIdEscalaCalificacion;
        public decimal idEscalaCalificacion
        {
            get { return dIdEscalaCalificacion; }
            set { dIdEscalaCalificacion = value; }
        }
        private decimal dIdEscalaCalificacionTransaccional;
        public decimal idEscalaCalificacionTransaccional
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
        private decimal dIdTipoEscalaCalificacion;
        public decimal idTipoEscalaCalificacion
        {
            get { return dIdTipoEscalaCalificacion; }
            set { dIdTipoEscalaCalificacion = value; }
        }
    }
}
