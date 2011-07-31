
using System;
using EDUAR_Entities.Shared;
namespace EDUAR_Entities
{
    [Serializable]
    public class CicloLectivo: DTBase
    {
        private int _idCicloLectivo;

        public int idCicloLectivo
        {
            get { return _idCicloLectivo; }
            set { _idCicloLectivo = value; }
        }
        private int _idCicloLectivoTransaccional;

        public int idCicloLectivoTransaccional
        {
            get { return _idCicloLectivoTransaccional; }
            set { _idCicloLectivoTransaccional = value; }
        }
        private string _nombre;

        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        private DateTime _fechaInicio;

        public DateTime fechaInicio
        {
            get { return _fechaInicio; }
            set { _fechaInicio = value; }
        }
        private DateTime _fechaFin;

        public DateTime fechaFin
        {
            get { return _fechaFin; }
            set { _fechaFin = value; }
        }
        private bool _activo;

        public bool activo
        {
            get { return _activo; }
            set { _activo = value; }
        }

    }
}
