///////////////////////////////////////////////////////////
//  MotivoAusencia.cs
//  Implementation of the Class MotivoAusencia
//  Generated by Enterprise Architect
//  Created on:      20-jun-2011 16:21:51
//  Original author: orkus
///////////////////////////////////////////////////////////


namespace EDUAR_Entities
{

    public class MotivoAusencia
    {

        private string _nombre;
        private int _idMotivo;
        private int _idMotivoTransaccional;

        public MotivoAusencia()
        {

        }

        ~MotivoAusencia()
        {

        }

        public virtual void Dispose()
        {

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

        public int idMotivo
        {
            get
            {
                return _idMotivo;
            }
            set
            {
                _idMotivo = value;
            }
        }
        public int idMotivoTransaccional
        {
            get
            {
                return _idMotivoTransaccional;
            }
            set
            {
                _idMotivoTransaccional = value;
            }
        }
    }//end MotivoAusencia
}