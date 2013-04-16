using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Entity
{
    [Serializable]
    public class RelAlternativaCriterioEntity
    {
        public int idAlternativa { get; set; }
        public int idCriterio { get; set; }
        public string nombreAlternativa { get; set; }
        public string nombreCriterio { get; set; }
        public decimal valor { get; set; }
    }
}
