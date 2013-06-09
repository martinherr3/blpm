package ar.edu.frc.utn.dlc.backend;

import ar.edu.frc.utn.dlc.Exceptions.BuscadorException;
import ar.edu.frc.utn.dlc.Exceptions.SpiderException;
import ar.edu.frc.utn.dlc.handlers.DBHandler;
import ar.edu.frc.utn.dlc.handlers.ErrorHandler;
import ar.edu.frc.utn.dlc.handlers.FileHandler;
import java.io.File;
import java.util.HashSet;
import java.util.Vector;
import java.util.logging.Level;
import java.util.logging.Logger;

class Spider implements Runnable {

    private DBHandler dbHandler;
    private HashSet<FileHandler> fileHandlers;
    private Paths notYetExploredPaths;
    private Indexer indexer;
    private ErrorHandler errorHandler;

    /**
     * Constructor de la clase
     * @param connection El manejador de la Base de datos
     * @param errorHandler El manejador de errores
     */
    public Spider(final DBHandler connection, final ErrorHandler errorHandler) {
        try {
            this.dbHandler = connection;
            this.fileHandlers = new HashSet<FileHandler>();
            this.notYetExploredPaths = new Paths(dbHandler);
            this.errorHandler = errorHandler;
            this.indexer = new Indexer(this.dbHandler);
        } catch (SpiderException ex) {
            //errorWrapper(ex);
        }
    }

    /**
     * Sobrecarga del metodo run de la clase Thread
     */
    @Override
    public void run() {
        try {
            this.doIndex();
        } catch (BuscadorException ex) {
            Logger.getLogger(Spider.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    /**
     * Comienza la indexacion de los documentos
     */
    public void doIndex() throws BuscadorException {
        do {
            try {
                this.indexerWrapper();
            } catch (SpiderException ex) {
                //errorWrapper(ex);
            }
        } while (true);
    }

     /**
     * Agrega una ruta de archivos a explorar
     * @param toExplore ruta a explorar
     */
    public synchronized void addPath2Explore(final File toExplore) {
        this.notYetExploredPaths.addPath(toExplore);
    }

    /**
     * Agrega un manejador de archivos
     * @param fh el manejador de archivos a agregar
     */
    public synchronized void addFileHandler(final FileHandler fh) {
        this.fileHandlers.add(fh);
    }


    private void indexerWrapper() throws SpiderException, BuscadorException {
        int i = 0;
        Vector<File> toIndex = this.notYetExploredPaths.getFilesToIndex();
        while (!toIndex.isEmpty()) {
            File file = toIndex.remove(0);
            boolean indexed = false;
            for (FileHandler fh : this.fileHandlers) {
                if (fh.isMyHandler(file)) {
                    indexer.addToDb(fh, file);
                    indexed = true;
                    break;
                }//if
            }//filehandlers
            if (!indexed) {
                SpiderException sex = new SpiderException("Not FileHandler for: " + file.getAbsolutePath(), SpiderException.FILE_HANDLER_NOT_FOUND);
                //this.errorWrapper(sex);
            }
            System.gc();
        }//files
    }//indexer method

    /**
    private void errorWrapper(SpiderException ex) {
        byte exType = ex.getExceptionType();
        switch (exType) {
            case SpiderException.WARNING:
                this.errorHandler.warning(ex);
                break;
            case SpiderException.ERROR:
                this.errorHandler.error(ex);
                break;
            case SpiderException.FATAL_ERROR:
                this.errorHandler.fatalError(ex);
                break;
            case SpiderException.FILE_HANDLER_NOT_FOUND:
                this.errorHandler.fileHandlerNotFound(ex);
                break;
            default:
                this.errorWrapper(new SpiderException("Unknown Type", SpiderException.FATAL_ERROR));
        }
    }
*/

    /**
     * Cierra la conexion a la base de datos
     */
    public void closeConnection() {
        try {
            this.dbHandler.close();
        } catch (SpiderException ex) {
            //this.errorWrapper(ex);
        }
    }
}
