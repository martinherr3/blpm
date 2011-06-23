///////////////////////////////////////////////////////////
//  Curso.cs
//  Implementation of the Class Curso
//  Generated by Enterprise Architect
//  Created on:      20-jun-2011 16:21:47
//  Original author: orkus
///////////////////////////////////////////////////////////

using System.Collections.Generic;
namespace EDUAR_Entities
{
    public class Curso
    {
        private int _idCurso;

        public int idCurso
        {
            get { return _idCurso; }
            set { _idCurso = value; }
        }
        private int _idCursoTransaccional;

        public int idCursoTransaccional
        {
            get { return _idCursoTransaccional; }
            set { _idCursoTransaccional = value; }
        }
        private List<Alumno> _alumnos;
        private List<Asignatura> _asignaturas;
        private string _division;
        private Nivel _nivel;
        private string _nombre;
        private Preceptor _preceptor;

        public Curso()
        {

        }

        ~Curso()
        {

        }

        public virtual void Dispose()
        {

        }

        public List<Alumno> alumnos
        {
            get
            {
                return _alumnos;
            }
            set
            {
                _alumnos = value;
            }
        }

        public List<Asignatura> asignaturas
        {
            get
            {
                return _asignaturas;
            }
            set
            {
                _asignaturas = value;
            }
        }

        public string division
        {
            get
            {
                return _division;
            }
            set
            {
                _division = value;
            }
        }

        public Nivel nivel
        {
            get
            {
                return _nivel;
            }
            set
            {
                _nivel = value;
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

        public Preceptor preceptor
        {
            get
            {
                return _preceptor;
            }
            set
            {
                _preceptor = value;
            }
        }

    }//end Curso
}