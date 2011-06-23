///////////////////////////////////////////////////////////
//  Escala.cs
//  Implementation of the Class Escala
//  Generated by Enterprise Architect
//  Created on:      20-jun-2011 16:21:48
//  Original author: Pablo Nicoliello
///////////////////////////////////////////////////////////


namespace EDUAR_Entities
{

    public class Escala
    {

        private string _formula;
        private string _nombre;

        public Escala()
        {

        }

        ~Escala()
        {

        }

        public virtual void Dispose()
        {

        }

        public string formula
        {
            get
            {
                return _formula;
            }
            set
            {
                _formula = value;
            }
        }

        public string nombre
        {
            get
            {
                return _nombre;
            }
            set
            {
                _nombre = value;
            }
        }

    }//end Escala
}