using System;

namespace DataAccess.Entity
{
	[Serializable]
	public class ModeloEntity
	{
		public int idModelo { get; set; }

		public string nombre { get; set; }
		public DateTime fechaCreacion { get; set; }
		public string username { get; set; }
        public string filename { get; set; }
		public int alternativas { get; set; }
		public int criterios { get; set; }
	}
}
