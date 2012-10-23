using System;
using EDUAR_Entities.Shared;
using System.Collections.Generic;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_Entities
{
    [Serializable]
    public class EncuestaDisponible : DTBase
    {
        //OBSERVACIÓN: El usuario solo se considera para asegurarse que se responda una sola vez, no va a ser divulgado       
        public Encuesta encuesta { get; set; }
        public Persona usuario { get; set; }

        public bool respondida { get; set; }
        public DateTime fechaRespuesta { get; set; }
        public DateTime fechaLimite { get; set; }
        public bool expirada { get; set; }
		public List<Respuesta> listaRespuestas { get; set; }

        public EncuestaDisponible()
        {
            encuesta = new Encuesta();
            usuario = new Persona();
			listaRespuestas = new List<Respuesta>();
        }
    }
}
