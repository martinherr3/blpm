using System;
using System.Collections.Generic;
using EDUAR_Entities.Security;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities.Reports
{
    [Serializable]
    public class FilAccesos : DTBase
    {
        public string username { get; set; }
        public int idPagina { get; set; }
        public DateTime fechaDesde { get; set; }
        public DateTime fechaHasta { get; set; }
        public List<DTRol> listaRoles { get; set; }
		public List<Pagina> listaPaginas { get; set; }

        public FilAccesos()
        {
            listaRoles = new List<DTRol>();
			listaPaginas = new List<Pagina>();
        }

        ~FilAccesos()
        { }
    }
}