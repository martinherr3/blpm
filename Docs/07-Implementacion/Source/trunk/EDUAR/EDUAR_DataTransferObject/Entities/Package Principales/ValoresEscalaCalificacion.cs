using System;
using System.Collections.Generic;
using System.Text;

namespace EDUAR_Entities
{
    [Serializable]
    public class ValoresEscalaCalificacion
    {
        private Decimal dIdValorEscalaCalificacion;
        public Decimal idValorEscalaCalificacion
        {
            get { return dIdValorEscalaCalificacion; }
            set { dIdValorEscalaCalificacion = value; }
        }
        private Decimal dIdValorEscalaCalificacionTransaccional;
        public Decimal idValorEscalaCalificacionTransaccional
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
        private Decimal dIdEscalaCalificacion;
        public Decimal idEscalaCalificacion
        {
            get { return dIdEscalaCalificacion; }
            set { dIdEscalaCalificacion = value; }
        }
    }
}
