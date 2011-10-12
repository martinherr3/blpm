using System;
using System.Collections.Generic;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
    [Serializable]
    public class Alumno : Persona
    {
        public Alumno():base()
        {        
            listaTutores = new List<Tutor>();
        }

        private int _IdAlumno;

        public int idAlumno
        {
            get { return _IdAlumno; }
            set { _IdAlumno = value; }
        }

        private int _IdAlumnoTransaccional;
        public int idAlumnoTransaccional
        {
            get { return _IdAlumnoTransaccional; }
            set { _IdAlumnoTransaccional = value; }
        }

        private string _IdLegajo;
        public string legajo
        {
            get { return _IdLegajo; }
            set { _IdLegajo = value; }
        }
        private DateTime _fechaAlta;
        public DateTime fechaAlta
        {
            get { return _fechaAlta; }
            set { _fechaAlta = value; }
        }
        private DateTime _fechaBaja;
        public DateTime fechaBaja
        {
            get { return _fechaBaja; }
            set { _fechaBaja = value; }
        }


        

        public virtual void Dispose()
        {

        }

        private List<Tutor> _listaTutores = new List<Tutor>();

        //private List<Tutor> _listaTutores;


        public List<Tutor> listaTutores
        {
            get { return _listaTutores; }
            set { _listaTutores = value; }
        }
    }
}
