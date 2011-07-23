///////////////////////////////////////////////////////////
//  Novedad.cs
//  Implementation of the Class Novedad
//  Generated by Enterprise Architect
//  Created on:      20-jun-2011 16:21:51
//  Original author: orkus
///////////////////////////////////////////////////////////


using System.Collections.Generic;
using System;
namespace EDUAR_Entities
{
    [Serializable]
    public class Novedad
    {

        private Curso _curso;
        private DateTime _fecha;
        private Usuario _informante;
        private List<Usuario> _involucrados;
        private Usuario _m_Usuario;
        private string _novedad;
        private TipoNovedad _tipoNovedad;

        public Novedad()
        {

        }

        ~Novedad()
        {

        }

        public virtual void Dispose()
        {

        }

        public Curso curso
        {
            get
            {
                return _curso;
            }
            set
            {
                _curso = value;
            }
        }

        public DateTime fecha
        {
            get
            {
                return _fecha;
            }
            set
            {
                _fecha = value;
            }
        }

        public Usuario informante
        {
            get
            {
                return _informante;
            }
            set
            {
                _informante = value;
            }
        }

        public List<Usuario> involucrados
        {
            get
            {
                return _involucrados;
            }
            set
            {
                _involucrados = value;
            }
        }

        public Usuario usuario
        {
            get
            {
                return _m_Usuario;
            }
            set
            {
                _m_Usuario = value;
            }
        }

        public string novedad
        {
            get
            {
                return _novedad;
            }
            set
            {
                _novedad = value;
            }
        }

        public TipoNovedad tipoNovedad
        {
            get
            {
                return _tipoNovedad;
            }
            set
            {
                _tipoNovedad = value;
            }
        }

    }//end Novedad
}