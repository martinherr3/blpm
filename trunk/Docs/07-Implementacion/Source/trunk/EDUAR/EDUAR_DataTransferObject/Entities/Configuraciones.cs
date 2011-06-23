using System;
using System.Collections.Generic;
using System.Text;

namespace EDUAR_Entities
{
    [Serializable]
    public class Configuraciones
    {
        private int iIdConfiguracion;
        public int idConfiguracion
        {
            get { return iIdConfiguracion; }
            set { iIdConfiguracion = value; }
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
    }
}
