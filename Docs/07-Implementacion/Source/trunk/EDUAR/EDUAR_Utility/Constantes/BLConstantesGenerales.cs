using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDUAR_Utility.Constantes
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
}
