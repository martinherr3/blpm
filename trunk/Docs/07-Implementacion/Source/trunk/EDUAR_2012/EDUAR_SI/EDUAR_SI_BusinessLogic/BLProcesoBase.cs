using System;
using EDUAR_SI_DataAccess;

using System.Data.Objects;
using EDUAR_Entities;
using EDUAR_Utility.Enumeraciones;
using System.Collections.Generic;
using EDUAR_Utility.Utilidades;

namespace EDUAR_SI_BusinessLogic
{
	/// <summary>
	/// Clase base de las clases en la capa de negocio del proceso.
	/// </summary>
	public class BLProcesoBase
	{
		#region --[Atributos]--
		/// <summary>
		/// Contiene la cadena de conexión
		/// </summary>
		public String ConnectionString { get; set; }

		protected readonly List<Configuraciones> listaConfiguraciones;
		/// <summary>
		/// Objeto Mail, utilizado para el envío de correos electrónicos.
		/// Get: si al solicitarse la propiedad es null se llama al método crearEmail().
		/// </summary>
		private EDUAREmail ObjEmail;

        private EDUARSMS ObjSMS;

		#endregion

		#region --[Propiedades]--
		/// <summary>
		/// Objeto que contiene el Email
		/// </summary>
		public EDUAREmail Email
		{
			get
			{
				if (ObjEmail == null)
					CrearEmail();

				return ObjEmail;
			}
			set { ObjEmail = value; }
		}

        /// <summary>
        /// Objeto que contiene el SMS
        /// </summary>
        public EDUARSMS SMS
        {
            get
            {
                if (ObjSMS == null)
                {
                    ObjSMS = new EDUARSMS();
                } 

                return ObjSMS;
            }
            set { ObjSMS = value; }
        }

		#endregion

		#region --[Constructores]--
		/// <summary>
		/// Constructor por defecto
		/// </summary>
		public BLProcesoBase()
		{

		}

		/// <summary>
		/// Constructor. Inicializa la variable ConnectionString.
		/// </summary>
		/// <param name="connectionString">Cadena de conexión a la base de datos</param>
		public BLProcesoBase(String connectionString)
		{
			ConnectionString = connectionString;

			DABase objDABase = new DABase(ConnectionString);

			listaConfiguraciones = objDABase.ObtenerConfiguraciones();
		}
		#endregion

		#region --[Métodos Privados]--
		/// <summary>
		/// Método encargado de crear una instancia de la clase Mail en el atributo Email.
		/// 
		/// Carga los parámetros del objeto Mail:
		/// DisplayName
		/// EmailFrom
		/// ServidorSMTP
		/// UsuarioSMTP
		/// PasswordSMTP
		/// PuertoSMTP
		/// EnableSSL
		/// A los valores los obtiene de la tabla Parametrizaciones, utilizando un objeto
		/// DAParametros y el método ParametrizacionesGET(). Si no existe el valor a cargar,
		/// no se especifica el parámetro.
		/// </summary>
		private void CrearEmail()
		{
			//Boolean? enableSSL = null;

			String emailFrom = ObtenerValorConfiguracion(enumConfiguraciones.emailFrom).valor;
			String servidorSMTP = ObtenerValorConfiguracion(enumConfiguraciones.servidorSMTP).valor;
			String displayName = ObtenerValorConfiguracion(enumConfiguraciones.displayName).valor;
			Int32? puertoSMTP = Convert.ToInt32(ObtenerValorConfiguracion(enumConfiguraciones.puertoSMTP).valor);
			Boolean? enableSSL = Convert.ToBoolean(ObtenerValorConfiguracion(enumConfiguraciones.enableSSL).valor);

			Email = new EDUAREmail(emailFrom, servidorSMTP, puertoSMTP, enableSSL, displayName);

			String usuario = ObtenerValorConfiguracion(enumConfiguraciones.SendUserName).valor;
			String password = ObtenerValorConfiguracion(enumConfiguraciones.SendUserPass).valor;

			Email.CargarCredenciales(usuario, password);
		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// El sistema inserta un registro en la tabla ProcesosEjecutados
		/// </summary>
		/// <param name="resultadoProceso">The obj proceso.</param>
		protected void OnErrorProcess(int idProcesoAutomatico, Exception ex)
		{
			DABase objDABase = new DABase(ConnectionString);
			ProcesosEjecutados objProceso = new ProcesosEjecutados();
			objProceso.idProcesoAutomatico = idProcesoAutomatico;
			objProceso.resultado = false;
			objProceso.fechaEjecucion = DateTime.Now;
			objProceso.descripcionError = ex.Message;
			objDABase.OnErrorProcess(objProceso);
		}

		/// <summary>
		/// Obteners the valor configuracion.
		/// </summary>
		/// <param name="configuracion">The configuracion.</param>
		/// <returns></returns>
		protected Configuraciones ObtenerValorConfiguracion(enumConfiguraciones configuracion)
		{
			foreach (Configuraciones row in listaConfiguraciones)
			{
				if (row.nombre.Equals(configuracion.ToString()))
					return row;
			}
			return null;
		}

		/// <summary>
		/// Genera una traza en la tabla ProcesosEjecutados.
		/// </summary>
		/// <param name="proceso">Nombre del proceso</param>
		/// <param name="estado">Resultado de la ejecución (0: Incorrecto - 1: Correcto).</param>
		protected void ProcesosEjecutadosCreate(int idProcesoAutomatico, bool estado)
		{
			DAProcesosEjecutados objDAProcesosAutomaticos = new DAProcesosEjecutados(ConnectionString);
			ProcesosEjecutados resultadoProceso = new ProcesosEjecutados();
			resultadoProceso.idProcesoAutomatico = idProcesoAutomatico;
			resultadoProceso.resultado = estado;
			resultadoProceso.fechaEjecucion = DateTime.Now;
			objDAProcesosAutomaticos.Create(resultadoProceso);
		}
		#endregion
	}
}