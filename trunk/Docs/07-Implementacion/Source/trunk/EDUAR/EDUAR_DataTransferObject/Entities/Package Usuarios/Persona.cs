using System;
using System.Collections.Generic;
using System.Text;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
    [Serializable]
    public class Persona : DTBase
    {
        private int _IdPersona;
        public int idPersona
        {
            get { return _IdPersona; }
            set { _IdPersona = value; }
        }

        private string _nombre;
        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        private string _Apellido;
        public string apellido
        {
            get { return _Apellido; }
            set { _Apellido = value; }
        }
        private Int32 _NumeroDocumento;
        public Int32 numeroDocumento
        {
            get { return _NumeroDocumento; }
            set { _NumeroDocumento = value; }
        }
        private Decimal _IdTipoDocumento;
        public Decimal idTipoDocumento
        {
            get { return _IdTipoDocumento; }
            set { _IdTipoDocumento = value; }
        }
        private string _Domicilio;
        public string domicilio
        {
            get { return _Domicilio; }
            set { _Domicilio = value; }
        }
        private string _Barrio;
        public string barrio
        {
            get { return _Barrio; }
            set { _Barrio = value; }
        }
        private Decimal _IdLocalidad;
        public Decimal idLocalidad
        {
            get { return _IdLocalidad; }
            set { _IdLocalidad = value; }
        }
        private string _Sexo;
        public string sexo
        {
            get { return _Sexo; }
            set { _Sexo = value; }
        }
        private DateTime? _FechaNacimiento;
        public DateTime? fechaNacimiento
        {
            get { return _FechaNacimiento; }
            set { _FechaNacimiento = value; }
        }
        private string _TelefonoFijo;
        public string telefonoFijo
        {
            get { return _TelefonoFijo; }
            set { _TelefonoFijo = value; }
        }
        private string _TelefonoCelular;
        public string telefonoCelular
        {
            get { return _TelefonoCelular; }
            set { _TelefonoCelular = value; }
        }
        private string _TelefonoCelularAlternativo;
        public string telefonoCelularAlternativo
        {
            get { return _TelefonoCelularAlternativo; }
            set { _TelefonoCelularAlternativo = value; }
        }
        private string _Email;
        public string email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        private Boolean? _Activo;
        public Boolean? activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }
        private Localidades _Localidad;

        public Localidades localidad
        {
            get { return _Localidad; }
            set { _Localidad = value; }
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
