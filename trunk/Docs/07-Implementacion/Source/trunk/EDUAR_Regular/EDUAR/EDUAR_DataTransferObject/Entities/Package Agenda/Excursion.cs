///////////////////////////////////////////////////////////
//  Excursion.cs
//  Implementation of the Class Excursion
//  Generated by Enterprise Architect
//  Created on:      20-jun-2011 16:21:49
//  Original author: Pablo Nicoliello
///////////////////////////////////////////////////////////


using System;
using EDUAR_Entities.Shared;
namespace EDUAR_Entities
{
    [Serializable]
	public class Excursion : EventoAgenda
    {
        public int idExcursion { get; set; }
        public string destino { get; set; }
        public DateTime horaDesde { get; set; }
        public DateTime horaHasta { get; set; }
		//public EventoAgenda<Excursion> evento { get; set; }

        public Excursion()
        {
			//evento = new EventoAgenda<Excursion>();
        }

        ~Excursion()
        {
        }

        public virtual void Dispose()
        {

        }

    }//end Excursion
}