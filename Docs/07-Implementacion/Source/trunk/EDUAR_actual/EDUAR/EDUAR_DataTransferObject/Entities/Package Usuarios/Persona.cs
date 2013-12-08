using System;
using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
	[Serializable]
	public class Persona : DTBase
	{
		public int idPersona { get; set; }
		public string nombre { get; set; }
		public string apellido { get; set; }
		public int numeroDocumento { get; set; }
		public int idTipoDocumento { get; set; }
		public string domicilio { get; set; }
		public string barrio { get; set; }
		public int idLocalidad { get; set; }
		public string sexo { get; set; }
		// Verificar que al hacer fechaNacimiento No Nullable no rompa nada
		public DateTime fechaNacimiento { get; set; }
		public string telefonoFijo { get; set; }
		public string telefonoCelular { get; set; }
		public string telefonoCelularAlternativo { get; set; }
		public string email { get; set; }
		public bool? activo { get; set; }
		public Localidades localidad { get; set; }
		public string username { get; set; }
        public TipoPersona tipoPersona { get; set; }

		public int idTipoPersona
		{
			get { return tipoPersona.idTipoPersona; }
			set
			{
				tipoPersona.idTipoPersona = value;
			}
		}

        public Usuario usuario
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public TipoDocumento tipoDocumento
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
		
		public Persona()
		{
			localidad = new Localidades();
			tipoPersona = new TipoPersona();
		}

		~Persona()
		{

		}
	}
}
