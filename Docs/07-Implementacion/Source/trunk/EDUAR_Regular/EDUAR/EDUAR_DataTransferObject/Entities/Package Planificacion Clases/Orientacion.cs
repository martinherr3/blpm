///////////////////////////////////////////////////////////
//  Orientacion.cs
//  Implementation of the Class Orientacion
//  Generated by Enterprise Architect
//  Created on:      20-jun-2011 16:21:52
//  Original author: Pablo Nicoliello
///////////////////////////////////////////////////////////


using EDUAR_Entities.Shared;
using System;
namespace EDUAR_Entities
{
    [Serializable]
    public class Orientacion: DTBase
    {

        private string _definicionCompetencias;
        private string _descripcion;
        private int _idOrientacion;
        private string _nombre;

        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public int idOrientacion
        {
            get { return _idOrientacion; }
            set { _idOrientacion = value; }
        }
        private int _idOrientacionTransaccional;

        public int idOrientacionTransaccional
        {
            get { return _idOrientacionTransaccional; }
            set { _idOrientacionTransaccional = value; }
        }
        public Orientacion()
        {

        }

        ~Orientacion()
        {

        }

        public virtual void Dispose()
        {

        }

        public string definicionCompetencias
        {
            get
            {
                return _definicionCompetencias;
            }
            set
            {
                _definicionCompetencias = value;
            }
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

    }//end Orientacion
}