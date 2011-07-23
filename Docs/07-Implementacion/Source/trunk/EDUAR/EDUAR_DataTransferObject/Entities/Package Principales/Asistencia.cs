using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDUAR_Entities
{
    [Serializable]
    public class Asistencia
    {
        private int _idAsistencia;

        public int idAsistencia
        {
            get { return _idAsistencia; }
            set { _idAsistencia = value; }
        }


        private DateTime _fecha;
        public DateTime fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
        private TipoAsistencia _tipoAsistencia;

        public TipoAsistencia tipoAsistencia
        {
            get { return _tipoAsistencia; }
            set { _tipoAsistencia = value; }
        }

        private int _idAsistenciaTransaccional;

        public int idAsistenciaTransaccional
        {
            get { return _idAsistenciaTransaccional; }
            set { _idAsistenciaTransaccional = value; }
        }

        private Alumno _unAlumno;

        public Alumno unAlumno
        {
            get { return _unAlumno; }
            set { _unAlumno = value; }
        }



        
        public Asistencia()
        {

        }

        ~Asistencia()
        {

        }

        public virtual void Dispose()
        {

        }


      
    }
}
