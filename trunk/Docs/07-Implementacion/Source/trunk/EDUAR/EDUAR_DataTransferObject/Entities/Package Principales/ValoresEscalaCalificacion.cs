using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
    [Serializable]
    public class ValoresEscalaCalificacion: DTBase
    {
        private decimal dIdValorEscalaCalificacion;
        public decimal idValorEscalaCalificacion
        {
            get { return dIdValorEscalaCalificacion; }
            set { dIdValorEscalaCalificacion = value; }
        }
        private decimal dIdValorEscalaCalificacionTransaccional;
        public decimal idValorEscalaCalificacionTransaccional
        {
            get { return dIdValorEscalaCalificacionTransaccional; }
            set { dIdValorEscalaCalificacionTransaccional = value; }
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
        private string szValor;
        public string valor
        {
            get { return szValor; }
            set { szValor = value; }
        }
        private Boolean bActivo;
        public Boolean activo
        {
            get { return bActivo; }
            set { bActivo = value; }
        }
        private Boolean bAprobado;
        public Boolean aprobado
        {
            get { return bAprobado; }
            set { bAprobado = value; }
        }
        private decimal dIdEscalaCalificacion;
        public decimal idEscalaCalificacion
        {
            get { return dIdEscalaCalificacion; }
            set { dIdEscalaCalificacion = value; }
        }
    }
}
