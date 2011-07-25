using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_Utility.Excepciones
{
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
        public string Source { get; set; }

        /// <summary>
        /// Message interno de la Excepcion generada
        /// (Incluido en el detail del GenericException)
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// StackTrace interno de la Excepcion generada
        /// (Incluido en el detail del GenericException)
        /// </summary>
        [DataMember]
        public string StackTrace { get; set; }


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
            Message = e.Message;
            Source = e.Source;
            StackTrace = e.StackTrace;
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
            GenericException ex = new GenericException(exceptionType,e);
            FaultException<GenericException> objEx = new FaultException<GenericException>(ex, razon);
            throw objEx;
        }
    }
}
