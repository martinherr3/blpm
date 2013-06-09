package ar.edu.frc.utn.dlc.handlers;

import java.io.File;

import ar.edu.frc.utn.dlc.Exceptions.BuscadorException;

/**
 * Interfaz comun para el manejo de archivos
 */
public interface FileHandler {

    /**
     * Extrae todas las palabras de un archivo
     * @param f el archivo a extraer las palabras
     * @return un array con todas las palabras del archivo
     * @throws BuscadorException
     */
    public String[] getWords(File f) throws BuscadorException;

    /**
     * Evalua si este handler es valido para este archivo
     * @param f el archivo a evaluar
     * @return si el handler es o no valido para este archivo
     * @throws BuscadorException
     */
    public boolean isMyHandler(File f) throws BuscadorException;

    /**
     * Abre el archivo con alguna aplicacion por defecto
     * @param f el archivo a abrir
     * @throws BuscadorException
     */
    public void apOpenFile(File f) throws BuscadorException;
}
