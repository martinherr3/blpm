package ar.edu.frc.utn.dlc.backend;

import java.io.File;
import java.util.Vector;

class DocumentVector extends Vector<Document> {

    /**
     * crea una nueva instancia.
     */
    public DocumentVector() {
    }

    /**
     * Agrega un documento al vector manteniendo siempre el orden de las maximas
     * frecuencias adelante
     * @param f el archivo a agregar
     * @param tf la frecuencia del termino del post node al cual pertenece este
     * vector
     */
    public void addDocument(File f, long tf) {
        Document newDoc = new Document(f, tf);
        this.addDocument(newDoc);
    }

    /**
     * Agrega un documento al vector manteniendo siempre el orden de las maximas
     * frecuencias adelante
     * @param d el documento a agregarse
     */
    public void addDocument(Document d) {
        if (!this.contains(d)) {
            this.insertElementAt(d,0);
            this.sort();
        }
    }

    /**
     * Transforma el vector a un array para su mas comoda utilizacion
     * @return los contenidos del vector en un array
     */
    @Override
    public Document[] toArray() {
        return this.toArray(new Document[this.size()]);
    }

    private void sort() {
        for (int i = 0; i < this.size() - 1; i++) {
            Document li = this.elementAt(i);
            Document lj = this.elementAt(i + 1);
            if (li.getTf() < lj.getTf()) {
                this.setElementAt(lj, i);
                this.setElementAt(li, i + 1);
            }//if
        }//for
    }
}
