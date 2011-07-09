using System;
using System.Collections.Generic;
using System.Text;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
    [Serializable]
    public class Persona : DTBase
    {
        private int _idPersona;
        public int idPersona
        {
            get { return _idPersona; }
            set { _idPersona = value; }
        }

        private string _nombre;
        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        private string _apellido;
        public string apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }
        private int _numeroDocumento;
        public int numeroDocumento
        {
            get { return _numeroDocumento; }
            set { _numeroDocumento = value; }
        }
        private int _idTipoDocumento;
        public int idTipoDocumento
        {
            get { return _idTipoDocumento; }
            set { _idTipoDocumento = value; }
        }
        private string _domicilio;
        public string domicilio
        {
            get { return _domicilio; }
            set { _domicilio = value; }
        }
        private string _barrio;
        public string barrio
        {
            get { return _barrio; }
            set { _barrio = value; }
        }
        private int _idLocalidad;
        public int idLocalidad
        {
            get { return _idLocalidad; }
            set { _idLocalidad = value; }
        }
        private string _sexo;
        public string sexo
        {
            get { return _sexo; }
            set { _sexo = value; }
        }
        private DateTime? _fechaNacimiento;
        public DateTime? fechaNacimiento
        {
            get { return _fechaNacimiento; }
            set { _fechaNacimiento = value; }
        }
        private string _telefonoFijo;
        public string telefonoFijo
        {
            get { return _telefonoFijo; }
            set { _telefonoFijo = value; }
        }
        private string _telefonoCelular;
        public string telefonoCelular
        {
            get { return _telefonoCelular; }
            set { _telefonoCelular = value; }
        }
        private string _telefonoCelularAlternativo;
        public string telefonoCelularAlternativo
        {
            get { return _telefonoCelularAlternativo; }
            set { _telefonoCelularAlternativo = value; }
        }
        private string _email;
        public string email
        {
            get { return _email; }
            set { _email = value; }
        }
        private bool? _activo;
        public bool? activo
        {
            get { return _activo; }
            set { _activo = value; }
        }
        private Localidades _localidad;

        public Localidades localidad
        {
            get { return _localidad; }
            set { _localidad = value; }
        }

        private string _username;
        public string username
        {
            get { return _username; }
            set { _username = value; }
        }

        private int _idTipoPersona;
        public int idTipoPersona
        {
            get { return _idTipoPersona; }
            set { _idTipoPersona = value; }
        }

        public Persona()
        {

        }

        ~Persona()
        {

        }
    }
}
