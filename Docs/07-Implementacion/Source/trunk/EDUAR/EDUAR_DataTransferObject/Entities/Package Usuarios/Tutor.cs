///////////////////////////////////////////////////////////
//  Tutor.cs
//  Implementation of the Class Tutor
//  Generated by Enterprise Architect
//  Created on:      20-jun-2011 16:21:57
//  Original author: Pablo Nicoliello
///////////////////////////////////////////////////////////



namespace EDUAR_Entities
{
    public class Tutor : Persona
    {

        private Alumno _alumnosASuCargo;
        private string _telefonoFijoTrabajo;

        public Tutor()
        {

        }

        ~Tutor()
        {

        }

        //public override void Dispose()
        //{

        //}

        public Alumno alumnosASuCargo
        {
            get
            {
                return _alumnosASuCargo;
            }
            set
            {
                _alumnosASuCargo = value;
            }
        }

        public string telefonoFijoTrabajo
        {
            get
            {
                return _telefonoFijoTrabajo;
            }
            set
            {
                _telefonoFijoTrabajo = value;
            }
        }

    }//end Tutor
}