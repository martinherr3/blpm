package ar.edu.frc.utn.dlc.handlers;

import ar.edu.frc.utn.dlc.Exceptions.BuscadorException;

/**
 * Define una interfaz comun para el manejo de errores
 */
public interface ErrorHandler {

    /**
     * Define el evento a realizarse si  un spiderException es del tipo warning
     * @param ex la exception a ser tratada
     */
    public void warning(BuscadorException ex);

    /**
     * Define el evento a realizarse si  un spiderException es del tipo error
     * @param ex la exception a ser tratada
     */
    public void error(BuscadorException ex);

    /**
     * Define el evento a realizarse si  un spiderException es del tipo error
     * fatal
     * @param ex la exception a ser tratada
     */
    public void fatalError(BuscadorException ex);

    /**
     * Define el evento a realizarse si  un spiderException lanzado cuando no
     * se encontro un error para algun tipo de archivo.
     * @param ex la exception a ser tratada
     */
    public void fileHandlerNotFound(BuscadorException ex);
}
