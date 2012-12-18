using System;
using EDUAR_Entities.Shared;
using System.Collections.Generic;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_Entities
{
    [Serializable]
    public class Respuesta : DTBase
    {
        //OBSERVACIÓN: El usuario solo se considera para asegurarse que se responda una sola vez, no va a ser divulgado
        
        public EncuestaDisponible encuestaDisponible { get; set; }

        public Pregunta pregunta { get; set; }
        public int idRespuesta { get; set; }
        public int respuestaSeleccion { get; set; }
        public string respuestaTextual { get; set; }
        public DateTime fechaRespuesta { get; set; }

        public Respuesta()
        {
            encuestaDisponible = new EncuestaDisponible();
			pregunta = new Pregunta();
        }

        /// <summary>
        /// Resultado numerico de la respuesta en base a la respuesta y su peso.
        /// Es parametrizable para cambiar el tipo de fórmula a utilizar.
        /// </summary>
        /// <returns></returns>
        public double valoracion(int parametro)
        {
            double resultado = 0;

            switch (parametro)
            {
                case (int)enumFormulaCalculoRespuestas.Lineal:
                    resultado = pregunta.ponderacion * respuestaSeleccion;
                    break;
                case (int)enumFormulaCalculoRespuestas.Logaritmica:

                    break;
                case (int)enumFormulaCalculoRespuestas.Trigonometrica:

                    break;
                case (int)enumFormulaCalculoRespuestas.Textual:
                    if (!respuestaTextual.Equals("")) resultado = pregunta.ponderacion;
                    break;
                default:
                    break;
            }
                        
            return resultado;
        }
    }
}
