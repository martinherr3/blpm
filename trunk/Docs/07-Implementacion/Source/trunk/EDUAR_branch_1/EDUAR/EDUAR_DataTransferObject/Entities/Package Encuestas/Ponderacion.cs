///////////////////////////////////////////////////////////
//  Ponderacion.cs
//  Implementation of the Class Ponderacion
//  Generated by Enterprise Architect
//  Created on:      20-jun-2011 16:21:54
//  Original author: Pablo Nicoliello
///////////////////////////////////////////////////////////


using EDUAR_Entities.Shared;
using System;
namespace EDUAR_Entities
{
    [Serializable]
    public class Ponderacion:DTBase
    {
        public int idPonderacion { get; set; }
        private string _descripcion;
        private int _peso;

        public Ponderacion()
        {

        }

        ~Ponderacion()
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

        public int peso
        {
            get
            {
                return _peso;
            }
            set
            {
                _peso = value;
            }
        }

    }//end Ponderacion
}