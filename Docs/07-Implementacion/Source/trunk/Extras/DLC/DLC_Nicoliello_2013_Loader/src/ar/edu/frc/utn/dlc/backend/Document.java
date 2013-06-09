package ar.edu.frc.utn.dlc.backend;

import java.io.File;

public class Document {

    private final String fileAbsolutePath;
    private final long tf;

    /**
     * Contructor del documento
     * @param file el archivo que esta siendo indexado
     * @param tf la frecuencia de la palabra a la cual corresponde este
     * documento
     */
    public Document(File file, long tf) {
        this.fileAbsolutePath = file.getAbsolutePath();
        this.tf = tf;
    }

    /**
     * Retorna El path absoluto del archivo indexado
     * @return el path absoluto
     */
    public String getFileAbsolutePath() {
        return fileAbsolutePath;
    }

    /**
     * la frecuencia de la palabra para la cual se creo este documento
     * @return un long qu representa la frecuencia de la palabra para la que fue
     * creado este documento
     */
    public long getTf() {
        return tf;
    }

    /**
     * Redefine el operador de comparacion para comparar solo la ruta de los
     * archivos indexados
     * @param obj el otro objeto para comparar con esto
     * @return si el objeto es igual o no a este
     */
    @Override
    public boolean equals(Object obj) {
        if (obj == null) {
            return false;
        }
        if (getClass() != obj.getClass()) {
            return false;
        }
        final Document other = (Document) obj;
        if ((this.fileAbsolutePath == null) ? (other.fileAbsolutePath != null) : !this.fileAbsolutePath.equals(other.fileAbsolutePath)) {
            return false;
        }
        return true;
    }

    /**
     * Redefine el hashcode del documento solo con el file absolute path
     * @return el hashcode
     */
    @Override
    public int hashCode() {
        return this.fileAbsolutePath.hashCode();
    }
}
