using System;

namespace DataAccess.Entity
{
	[Serializable]
	public class CriterioEntity
	{
        public int idCriterio { get; set; }
        public int idModelo { get; set; }
        public string nombre { get; set; }
        public decimal pesoDefault { get; set; }
        public bool maximiza { get; set; }
	}
}
