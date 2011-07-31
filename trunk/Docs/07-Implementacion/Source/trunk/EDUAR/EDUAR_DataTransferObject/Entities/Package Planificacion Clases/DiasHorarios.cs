///////////////////////////////////////////////////////////
//  DiaHorario.cs
//  Implementation of the Class DiaHorario
//  Generated by Enterprise Architect
//  Created on:      20-jun-2011 16:21:47
//  Original author: Pablo Nicoliello
///////////////////////////////////////////////////////////
using EDUAR_Entities.Shared;
using System.Collections.Generic;
using EDUAR_Utility.Enumeraciones;


namespace EDUAR_Entities
{
    public class DiasHorarios: DTBase
    {
		private int _idDiaHorario;
	  
	    public int idDiaHorario
        {
            get
            {
                return _idDiaHorario;
            }
            set
            {
                _idDiaHorario = value;
            }
        }

        private int _idNivel;

        public int idNivel
        {
            get { return _idNivel; }
            set { _idNivel = value; }
        }


        private int _idAsignatura;

        public int idAsignatura
        {
            get { return _idAsignatura; }
            set { _idAsignatura = value; }
        }

        private int _idCurso;

        public int idCurso
        {
            get { return _idCurso; }
            set { _idCurso = value; }
        }


        private int _idDiaHorarioTransaccional;

        public int idDiaHorarioTransaccional
        {
            get { return _idDiaHorarioTransaccional; }
            set { _idDiaHorarioTransaccional = value; }
        }

	    private enumDiasSemana _unDia;

        public enumDiasSemana unDia
        {
            get { return _unDia; }
            set { _unDia = value; }
        }

        private List<Modulo> _modulos;

		public List<Modulo> modulos
        {
            get { return _modulos; }
            set { _modulos = value; }
        }

        public DiasHorarios()
        {

        }

        ~DiasHorarios()
        {

        }

        public virtual void Dispose()
        {

        }



      

    }//end DiaHorario
}