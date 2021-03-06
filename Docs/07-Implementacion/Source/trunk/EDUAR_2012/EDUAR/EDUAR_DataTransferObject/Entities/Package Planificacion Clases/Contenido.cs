///////////////////////////////////////////////////////////
//  Contenido.cs
//  Implementation of the Class Contenido
//  Generated by Enterprise Architect
//  Created on:      20-jun-2011 16:21:46
//  Original author: Pablo Nicoliello
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using EDUAR_Entities.Shared;
namespace EDUAR_Entities
{
	[Serializable]
	public class Contenido : DTBase
	{
		public int idContenido { get; set; }
		public string descripcion { get; set; }
		public AsignaturaCicloLectivo asignaturaCicloLectivo { get; set; }
		public BibliografiaRecomendada bibliografia { get; set; }
		public List<TemaContenido> listaContenidos { get; set; }

		public Contenido()
		{
			asignaturaCicloLectivo = new AsignaturaCicloLectivo();
		}

		~Contenido()
		{

		}

		public virtual void Dispose()
		{

		}
	}//end Contenido
}