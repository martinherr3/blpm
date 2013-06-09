using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities.Security
{
    public class DTAcceso : DTBase
    {
        private int _idAcceso;

        public int idAcceso
        {
            get { return _idAcceso; }
            set { _idAcceso = value; }
        }
        private string _pagina;

        public string pagina
        {
            get { return _pagina; }
            set { _pagina = value; }
        }
        private string _url;

        public string url
        {
            get { return _url; }
            set { _url = value; }
        }
        private DateTime _fecha;

        public DateTime fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
        private string _usuario;

        public string usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }
    }
}
