
namespace EDUAR_Utility.Enumeraciones
{
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

    }//end enumExceptionType

    public enum enumRoles : int
    {
        Administrador = 0
    }

    /// <summary>
    /// Enumeración que contiene las acciones genericas. 
    /// </summary>
    public enum enumAcciones
    {
        Buscar,
        Nuevo,
        Modificar,
        Eliminar,
        Seleccionar,
        Limpiar,
        Aceptar,
        Salir,
        Redirect,
        Guardar,
        Ingresar,
        Desbloquear
    }

    /// <summary>
    /// Enumeración de Configuraciones
    /// </summary>
    public enum enumConfiguraciones
    {
        BaseDeDatosOrigen,
        PasswordInicial
    }

    public enum enumEscalasCalificaciones
    {
        Numerica = 1,
        Conceptual = 2
    }

    public enum enumCargosPersonal
    {
        Director = 116,
        Vicedirector = 117,
        Psicopedagogo = 118,
        Preceptor = 119,
        Administrativo = 120,
        Docente = 121
    }
}
