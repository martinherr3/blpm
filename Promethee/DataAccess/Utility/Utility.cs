using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Promethee.Utility
{
	[Serializable]
	public class BLConstantesGenerales
	{
		#region Constantes/ReadOnly Excepciones

		/// <summary>
		/// Indica el número de error que se obtiene a la hora de tener un error de integridad de BD cuan se realiza una eliminación
		/// </summary>
		public const int IntegrityErrorNumber = 547;

		/// <summary>
		/// Indica el número de error que se obtiene a la hora de tener un error de clave única duplicada
		/// </summary>
		public const int UniqueErrorNumber = 2601;

		/// <summary>
		/// Indica el número de error que se lanza desde sql cuando hay un error de concurrencia.
		/// </summary>
		public const int ConcurrencyErrorNumber = 50000;

		/// <summary>
		/// Indica el mensaje de error que se lanza desde sql cuando hay un error de concurrencia.
		/// </summary>
		public const string ConcurrencyErrorMessage = "SGDException MessageExceptionNumber:0001";

		/// <summary>
		/// Indica el mensaje de error que se lanza desde sql cuando hay un error de concurrencia.
		/// </summary>
		public const string PermitirEliminarErrorMessage = "SGDException MessageExceptionNumber:0002";

		#endregion
	}

	/// <summary>
	/// Enumeracion que contiene todas las Excepciones que se utilizan en la aplicacion.
	/// </summary>
	public enum enuExceptionType
	{
		/// <summary>
		/// Excepcion en la capa de Negocio.
		/// </summary>
		BusinessLogicException,
		/// <summary>
		/// Excepcion en la capa de Datos.
		/// </summary>
		DataAccesException,
		/// <summary>
		/// Excepcion en la capa de Servicio.
		/// </summary>
		ServicesException,
		/// <summary>
		/// Excepcion que se lanza cuando se intenta eliminar una entidad asociada con otra. (Capa de Datos)
		/// </summary>
		IntegrityDataException,
		/// <summary>
		/// Excepcion que se lanza cuando otro usuario ya efectuó una accion sobre la entidad. (Capa de Datos)
		/// </summary>
		ConcurrencyException,
		/// <summary>
		/// Excepcion de tipo SQL. (Capa de Datos).
		/// </summary>
		SqlException,
		/// <summary>
		/// Excepcion al realizar validaciones.(Capa de Negocio y UI)
		/// </summary>
		ValidationException,
		/// <summary>
		/// Excepcion de sguridad. (Capa Negocio - Membership y Roles)
		/// </summary>
		SecurityException,
		/// <summary>
		/// Excepcion que se genera en el WorkFlow.
		/// </summary>
		WorkFlowException,
		/// <summary>
		/// Excepcion generica.
		/// </summary>
		Exception,
		/// <summary>
		/// Excepción que se genera al acceder a la base transaccional
		/// </summary>
		MySQLException
	}

	public enum enumExceptionType : int
	{
		BusinessLogicException = 0,
		DataAccesException = 1,
		ServicesException = 2,
		IntegrityDataException = 3,
		ConcurrencyException = 4,
		SqlException = 5,
		Exception = 6,
		MySQLException = 7

	}


	/// <summary>
	/// Excepción utilizada cuando se produce una excepción no controlada, al producirse esta situación se
	/// reemplaza la excepción origen por UnknowExeption, que permite definir el mensaje genérico.
	/// (Configuración en EntLib)
	/// </summary>
	[Serializable]
	public class CustomizedException : Exception
	{
		public enuExceptionType ExceptionType { get; set; }

		#region Constructores
		public CustomizedException(string message, Exception inner, enuExceptionType exceptionType)
			: base(message, inner)
		{
			ExceptionType = exceptionType;
		}
		#endregion
	}

	[Serializable]
	public class GenericException : Exception
	{
		/// <summary>
		/// Propiedad utilizada para mostrar un mensaje personalizado en el throw de la GenericException 
		/// (Incluido en el detail del GenericException)
		/// </summary>
		[DataMember]
		public string Informacion { get; set; }

		/// <summary>
		/// Source interno de la Excepcion generada
		/// (Incluido en el detail del GenericException)
		/// </summary>
		[DataMember]
		public string Origen { get; set; }

		/// <summary>
		/// Message interno de la Excepcion generada
		/// (Incluido en el detail del GenericException)
		/// </summary>
		[DataMember]
		public string Mensaje { get; set; }

		/// <summary>
		/// StackTrace interno de la Excepcion generada
		/// (Incluido en el detail del GenericException)
		/// </summary>
		[DataMember]
		public string Trace { get; set; }


		[DataMember]
		public enuExceptionType ExceptionType { get; set; }

		//[DataMember]
		//public Exception InnerExe { get; set; }

		#region Constructor Privado
		/// <summary>
		/// Constructor que carga la data de la Excepcion.
		/// </summary>
		/// <param name="exceptionType"></param>
		/// <param name="e"></param>
		private GenericException(enuExceptionType exceptionType, Exception e)
		{
			Mensaje = e.Message;
			Origen = e.Source;
			Trace = e.StackTrace;
			ExceptionType = exceptionType;

			if (e.InnerException != null)
				Informacion = e.InnerException.Message;
		}
		#endregion

		/// <summary>
		/// Metodo que lanzara la exepcion 
		/// </summary>
		/// <param name="razon"></param>
		/// <param name="exceptionType"></param>
		/// <param name="e"></param>
		public static void throwGenericException(string razon, enuExceptionType exceptionType, Exception e)
		{
			GenericException ex = new GenericException(exceptionType, e);
			FaultException<GenericException> objEx = new FaultException<GenericException>(ex, razon);
			throw objEx;
		}
	}

	/// <summary>
	/// Clase encargada de manejar los logs de la Aplicacion.
	/// </summary>
	[Serializable]
	public class PrometheeLog
	{
		///<summary>
		///Ruta donde se guardará el log. 
		///</summary>
		private string oPath;
		///<summary>
		///Permite activar/desactivar el registro de sucesos.
		///</summary>
		///<remarks></remarks>
		public Boolean grabacionActivada;

		#region --[Constructor]--
		/// <summary>
		/// Constructor con la ruta donde se guardarán los logs.
		///</summary>
		/// <param name="path">No tiene ninguna función. Se ha dejado por temas de compatibilidad.</param>
		public PrometheeLog(string path)
		{
			new PrometheeLog(path, true);
		}

		/// <summary>
		/// Constructor que permite activar/desactivar el registro de logs y la ruta donde se guardarán los logs.
		/// </summary>
		/// <param name="path">Ubicación donde se grabará el log.</param>
		/// <param name="grabaLog">Permite activar/desactivar el Log.</param>
		public PrometheeLog(string path, Boolean grabaLog)
		{
			oPath = path;
			grabacionActivada = grabaLog;
		}
		#endregion

		/// <summary>
		/// Método que guarda la cadena en un log.
		/// </summary>
		/// <param name="cadena">Cadena que vamos a guardar en el log.</param>
		/// <remarks></remarks>
		public void write(string cadena)
		{
			write(cadena, FileMode.Append);
		}

		/// <summary>
		/// Método que guarda la cadena en un log indicando el modo de acceso al fichero.
		/// </summary>
		/// <param name="cadena">Cadena que vamos a guardar en el log.</param>
		/// <param name="pFileMode">No tiene ninguna función. Se ha dejado por temas de compatibilidad.</param>
		/// <remarks>GrupoIberostar.Facilities no permite seleccionar el modo de acceso al fichero, se mantiene por temas de compatibilidad.</remarks>
		public void write(string cadena, FileMode pFileMode)
		{
			try
			{
				FileStream fs;
				StreamWriter swWriter;

				fs = new FileStream(oPath, pFileMode, FileAccess.Write);
				swWriter = new StreamWriter(fs);

				//Escritura dentro del log
				swWriter.WriteLine(cadena);
				//Cerramos los objetos
				swWriter.Close();
				fs.Close();
			}
			catch (Exception ex)
			{
				//No podemos guardar este error, ya que está fallando al guardar el error!
				Debug.WriteLine("Se ha producido el siguiente error al guardar el log " + ex.Message);
			}
		}
	}
}


