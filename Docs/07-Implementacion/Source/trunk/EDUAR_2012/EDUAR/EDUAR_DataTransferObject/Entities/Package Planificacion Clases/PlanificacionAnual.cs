using System;
///////////////////////////////////////////////////////////
//  PlanificacionAnual.cs
//  Implementation of the Class PlanificacionAnual
//  Generated by Enterprise Architect
//  Created on:      20-jun-2011 16:21:54
//  Original author: Pablo Nicoliello
///////////////////////////////////////////////////////////
using EDUAR_Entities.Shared;
using System.Collections.Generic;
namespace EDUAR_Entities
{
	[Serializable]
	public class PlanificacionAnual : DTBase
	{
		public int idPlanificacionAnual { get; set; }
		public AsignaturaCicloLectivo asignaturaCicloLectivo { get; set; }
		public Persona creador { get; set; }
		public DateTime fechaCreacion { get; set; }
		public DateTime? fechaAprobada { get; set; }
		public string observaciones { get; set; }
		public List<TemaPlanificacionAnual> listaTemasPlanificacion {get; set;}
		public bool solicitarAprobacion { get; set; }

		public PlanificacionAnual()
		{
			asignaturaCicloLectivo = new AsignaturaCicloLectivo();
			creador = new Persona();
			listaTemasPlanificacion = new List<TemaPlanificacionAnual>();
		}

		~PlanificacionAnual()
		{

		}

		public virtual void Dispose()
		{

		}
	}//end PlanificacionAnual
}