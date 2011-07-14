using System;
using System.Collections.Generic;
using System.Text;

namespace EDUAR_Entities
{
    [Serializable]
    public class Alumno : Persona
    {
        public Alumno()
        {
            listaTutores = new List<Tutor>();
        }

        private Decimal _IdAlumno;
        public Decimal idAlumno
        {
            get { return _IdAlumno; }
            set { _IdAlumno = value; }
        }

        private Decimal _IdAlumnoTransaccional;
        public Decimal idAlumnoTransaccional
        {
            get { return _IdAlumnoTransaccional; }
            set { _IdAlumnoTransaccional = value; }
        }
        private Decimal _idPersona;
        public Decimal idPersona
        {
            get { return _idPersona; }
            set { _idPersona = value; }
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

        private List<Tutor> _listaTutores;

        public List<Tutor> listaTutores
        {
            get { return _listaTutores; }
            set { _listaTutores = value; }
        }
    }
}
