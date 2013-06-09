package ar.edu.frc.utn.dlc.backend;

import ar.edu.frc.utn.dlc.Exceptions.SpiderException;
import ar.edu.frc.utn.dlc.handlers.DBHandler;
import java.io.File;
import java.util.Vector;

class Paths {

    private Vector<File> paths;
    private DBHandler dBHandler;

    /**
     * Crea una nueva instancia para agrupar los paths a ser indexados
     * @param dBHandler el handler de la base de datos para saber que archivos
     * ya estan indexados y no hace falta devolverlos
     */
    public Paths(DBHandler dBHandler) {
        this.paths = new Vector<File>();
        this.dBHandler = dBHandler;
    }

    /**
     * Un vector con los archivos a ser indexados
     * @return Un vector con los archivos a ser indexados
     * @throws SpiderException
     */
    public Vector<File> getFilesToIndex() throws SpiderException {
        Vector<File> toIndex = new Vector<File>();
        for (File p : this.paths) {
            for (File f : this.getFilesOf(p)) {
                if (!this.dBHandler.isDocumentIndexed(f.getAbsolutePath())) {
                    toIndex.add(f);
                }
            }
        }
        return toIndex;
    }

    private Vector<File> getFilesOf(File path) {
        Vector<File> files = new Vector<File>();
        for (File f : path.listFiles()) {
            if (f.isDirectory()) {
                files.addAll(this.getFilesOf(f));
            } else {
                files.add(f);
            }
        }
        return files;
    }

    /**
     * Agrega un nuevo path para extraer los archivos a indexar
     * @param toExplore el nuevo path o archivo a explorar
     */
    public void addPath(File toExplore) {
        this.paths.add(toExplore);
    }
}
