///////////////////////////////////////////////////////////
//  Foro.cs
//  Implementation of the Class Foro
//  Generated by Enterprise Architect
//  Created on:      20-jun-2011 16:21:50
//  Original author: Pablo Nicoliello
///////////////////////////////////////////////////////////

using System.Collections.Generic;
using System;
namespace EDUAR_Entities
{
    [Serializable]
    public class Foro
    {

        private Usuario _destino;
        private List<Usuario> _moderadores;
        private string _nombreForo;
        private List<Topico> _topicos;

        public Foro()
        {

        }

        ~Foro()
        {

        }

        public virtual void Dispose()
        {

        }

        public Usuario destino
        {
            get
            {
                return _destino;
            }
            set
            {
                _destino = value;
            }
        }

        public List<Usuario> moderadores
        {
            get
            {
                return _moderadores;
            }
            set
            {
                _moderadores = value;
            }
        }

        public string nombreForo
        {
            get
            {
                return _nombreForo;
            }
            set
            {
                _nombreForo = value;
            }
        }

        public List<Topico> topicos
        {
            get
            {
                return _topicos;
            }
            set
            {
                _topicos = value;
            }
        }

    }//end Foro
}