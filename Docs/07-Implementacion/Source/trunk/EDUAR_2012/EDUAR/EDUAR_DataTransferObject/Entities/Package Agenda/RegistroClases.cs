using System;
using System.Collections.Generic;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_Entities
{
    [Serializable]
    public class RegistroClases : EventoAgenda
    {
		public int idRegistroClases { get; set; }
		public Asignatura asignatura { get; set; }
        public DateTime fecha { get; set; }
		public List<DetalleRegistroClases> listaDetalleRegistro { get; set; }
		public TipoRegistroClases tipoRegistro { get; set; }

        public RegistroClases()
        {
            asignatura = new Asignatura();
			listaDetalleRegistro = new List<DetalleRegistroClases>();
			tipoRegistro = new TipoRegistroClases() { idTipoRegistroClases= (int)enumTipoRegistroClases.ClaseNormal };
        }

		~RegistroClases()
        {
			asignatura = null;
			listaDetalleRegistro = null;
        }

        public virtual void Dispose()
        {

        }

	}//end RegistroClases
}