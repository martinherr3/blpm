///////////////////////////////////////////////////////////
//  MotivoCitacion.cs
//  Implementation of the Class MotivoCitacion
//  Generated by Enterprise Architect
//  Created on:      20-jun-2011 16:21:51
//  Original author: Pablo Nicoliello
///////////////////////////////////////////////////////////


namespace EDUAR_Entities
{

    public class MotivoCitacion
    {

        private string _descripcion;
        private string _nombre;

        public MotivoCitacion()
        {

        }

        ~MotivoCitacion()
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

    }//end MotivoCitacion
}