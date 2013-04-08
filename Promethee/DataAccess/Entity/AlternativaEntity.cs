using System;

namespace DataAccess.Entity
{
	[Serializable]
	public class AlternativaEntity
	{
        public int idAlternativa { get; set; }
        public int idModelo { get; set; }
        public string nombre { get; set; }
	}
}
