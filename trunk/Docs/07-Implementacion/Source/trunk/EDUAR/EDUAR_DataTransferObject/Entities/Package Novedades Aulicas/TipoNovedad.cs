///////////////////////////////////////////////////////////
//  TipoNovedad.cs
//  Implementation of the Class TipoNovedad
//  Generated by Enterprise Architect
//  Created on:      20-jun-2011 16:21:56
//  Original author: orkus
///////////////////////////////////////////////////////////

using EDUAR_Entities.Shared;
using System;
namespace EDUAR_Entities
{
    [Serializable]
    public class TipoNovedad:DTBase
    {
        public int idTipoNovedad { get; set; }
        private string _descripcion;

        public TipoNovedad()
        {

        }

        ~TipoNovedad()
        {

        }

        public virtual void Dispose()
        {

        }

        public string descripcion
        {
            get
            {
                return _descripcion;
            }
            set
            {
                _descripcion = value;
            }
        }

    }//end TipoNovedad
}