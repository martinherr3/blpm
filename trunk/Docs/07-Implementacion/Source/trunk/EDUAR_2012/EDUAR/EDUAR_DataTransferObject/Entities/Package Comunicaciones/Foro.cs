///////////////////////////////////////////////////////////
//  Foro.cs
//  Implementation of the Class Foro
//  Generated by Enterprise Architect
//  Created on:      20-jun-2011 16:21:50
//  Original author: Pablo Nicoliello
///////////////////////////////////////////////////////////

using System.Collections.Generic;
using System;
using EDUAR_Entities.Shared;
namespace EDUAR_Entities
{
    [Serializable]
    public class Foro: DTBase
    {
        public int idForo { get; set; }
        public Usuario destino { get; set; }
        public List<Usuario> moderadores { get; set; }
        public string nombreForo { get; set; }
        public List<Topico> topicos { get; set; }

        public Foro()
        {

        }

        ~Foro()
        {

        }

        public virtual void Dispose()
        {

        }

    }//end Foro
}