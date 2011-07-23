using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDUAR_Entities.Shared;
using EDUAR_Entities.Security;

namespace EDUAR_Entities
{
    [Serializable]
    public class Seccion : DTBase
    {
        private string _title;

        public string title
        {
            get { return _title; }
            set { _title = value; }
        }
        private string _url;

        public string url
        {
            get { return _url; }
            set { _url = value; }
        }
        private List<DTRol> _listaRoles;

        public List<DTRol> listaRoles
        {
            get { return _listaRoles; }
            set { _listaRoles = value; }
        }

        private List<Seccion> _listaSecciones;

        public List<Seccion> listaSecciones
        {
            get { return _listaSecciones; }
            set { _listaSecciones = value; }
        }
    }
}
