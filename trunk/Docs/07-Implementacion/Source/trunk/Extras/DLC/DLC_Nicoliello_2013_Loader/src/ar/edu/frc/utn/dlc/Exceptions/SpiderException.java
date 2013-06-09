package ar.edu.frc.utn.dlc.Exceptions;

public class SpiderException extends Exception {

    public static final byte WARNING = 0;
    public static final byte ERROR = 1;
    public static final byte FATAL_ERROR = 2;
    public static final byte FILE_HANDLER_NOT_FOUND = 3;
    private byte exceptionType;

    /**
     * Constructor de instancia SpiderExcepcion
     * @param ex excepcion a ser encapsulada
     * @param exceptionType Tipos de excepcion capturadas 0 = WARNING, 1 = ERROR
     * 2 = FATAL_ERROR, 3 = FILE_HANDLER_NOT_FOUND
     */
    public SpiderException(Exception ex, byte exceptionType) {
        this(ex.getMessage(), exceptionType);
        super.setStackTrace(ex.getStackTrace());
    }

    /**
     * Constructor de instancia SpiderExcepcion
     * @param msg Mensaje encapsulado a mostrar
     * @param exceptionType Tipos de excepcion capturadas 0 = WARNING, 1 = ERROR
     * 2 = FATAL_ERROR, 3 = FILE_HANDLER_NOT_FOUND
     */
    public SpiderException(String msg, byte exceptionType) {
        super(msg);
        this.setExceptionType(exceptionType);
    }

    /**
     * Devuelve el tipo de excepcion
     * @return Retorna un byte con el tipo de excepcion
     */
    public byte getExceptionType() {
        return exceptionType;
    }

    private void setExceptionType(byte exceptionType) {
        this.exceptionType = exceptionType;
    }

    /**
     * Devuelve el nombre de la excepcion
     * @return Nombre del tipo de excepcion
     */
    public String getExceptionTypeName() {
        String toReturn;
        switch (exceptionType) {
            case WARNING:
                toReturn = "WARNING";
                break;
            case ERROR:
                toReturn = "ERROR";
                break;
            case FATAL_ERROR:
                toReturn = "FATAL ERROR";
                break;
            case FILE_HANDLER_NOT_FOUND:
                toReturn = "FILE HANDLER NOT FOUND";
                break;
            default:
                toReturn = "UNKNOWN ERROR";
        }
        return toReturn;
    }
}
