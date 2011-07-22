using System;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_Utility.Excepciones
{
    /// <summary>
    /// Excepción utilizada cuando se produce una excepción no controlada, al producirse esta situación se
    /// reemplaza la excepción origen por UnknowExeption, que permite definir el mensaje genérico.
    /// (Configuración en EntLib)
    /// </summary>
    
    public class CustomizedException : Exception
    {
        public enuExceptionType ExceptionType { get; set; }

        #region Constructores
        public CustomizedException(string message, Exception inner, enuExceptionType exceptionType) : base(message, inner)
        {
            ExceptionType = exceptionType;
        }
        #endregion

       
    }
}
