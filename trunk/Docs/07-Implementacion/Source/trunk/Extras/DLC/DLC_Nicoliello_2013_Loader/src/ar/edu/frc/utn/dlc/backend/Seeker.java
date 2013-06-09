package ar.edu.frc.utn.dlc.backend;

import ar.edu.frc.utn.dlc.Exceptions.BuscadorException;
import ar.edu.frc.utn.dlc.handlers.DBHandler;
import ar.edu.frc.utn.dlc.handlers.ErrorHandler;
import ar.edu.frc.utn.dlc.handlers.FileHandler;
import java.io.File;

public class Seeker {

    private Buscador spiderRunnable;
    private Thread spiderThread;
    private Hunter hunter;

    /**
     * Constructor de la clase seeker
     * @param ch Implementacion de la interfaz para manejo de base de datos
     * @param eh Implementacion del manejador de errores
     */
    public Seeker(DBHandler ch, ErrorHandler eh) {
        this.spiderRunnable = new Buscador(ch, eh);
        this.spiderThread = new Thread(spiderRunnable);
        this.hunter = new Hunter(ch, eh);
    }

    /**
     * Metodo que comienza el indexado
     * @param sameThread Indica si indexa en el mismo hilo de ejecucion que el
     * de ejecucion de la aplicacion
     */
    public void startIndex(boolean sameThread) throws BuscadorException {
        if (sameThread) {
            this.spiderRunnable.doIndex();
        } else {
            spiderThread.start();
        }
    }

    /**
     * Setea el hilo de ejecucion de la clase en modo "demonio"
     * @param d true = modo demonio, false= ejecucion normal
     */
    public void setDaemon(boolean d) {
        this.spiderThread.setDaemon(d);
    }

    /**
     * Devuelve el estado del hilo
     * @return true si el hilo de ejecucion es demonio, false=ejecucion normal
     */
    public boolean isDaemon() {
        return this.spiderThread.isDaemon();
    }

    /**
     * Agrega un manejador de archivos
     * @param fh el manejador de archivos a agregar
     */
    public void addFileHandler(FileHandler fh) {
        this.spiderRunnable.addFileHandler(fh);
    }

    /**
     * Agrega una ruta de archivos a explorar
     * @param toExplore ruta a explorar
     */
    public void addPath2Explore(File toExplore) {
        this.spiderRunnable.addPath2Explore(toExplore);
    }

    /**
     * Detiene el indexado de archivos
     */
    public void stopIndex() {
        this.spiderRunnable.closeConnection();
    }

    /**
     * Efectua la busqueda de documentos que coincidan con el criterio de busqueda
     * @param query frase a buscar
     * @param maxMatches cantidad maxima de resultados a devolver
     * @return Retorna un conjunto de documentos 
     */
    public Document[] search(String query, int maxMatches) {
        return this.hunter.search(query, maxMatches);
    }
}
