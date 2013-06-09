package ar.edu.frc.utn.dlc.backend;

import java.io.File;
import java.util.Vector;

public class Post {

    private String word;
    private DocumentVector documents;

    /**
     * Crea un nuevo post para una palabra
     * @param word la palabra para la cual hacer el nuevo posteo
     */
    public Post(String word) {
        this.word = word;
        this.documents = new DocumentVector();
    }

    /**
     * Crea un nuevo post para una palabra con una lista de documentos
     * ya existente
     * @param w la palabra
     * @param docs el array de documentos ya existente
     */
    public Post(String w, Document[] docs) {
        this(w);
        for (int i = 0; i < docs.length; i++) {
            Document document = docs[i];
            this.documents.add(document);
        }
    }

    /**
     * Agrega un nuevo documento a la lista de documentos del post
     * @param file el documento
     * @param tf la frecuencia del termino en el documento
     */
    public void addDocument(File file, long tf){
        this.documents.addDocument(file, tf);
    }

    /**
     * Extrae todos los documentos del post
     * @return un array con todos los documentos del post
     */
    public Document[] getDocuments(){
        return this.documents.toArray();
    }

    /**
     * Extrae la palabra de este post
     * @return la palabra
     */
    public String getWord(){
        return this.word;
    }

    @Override
    public boolean equals(Object obj) {
        if (obj == null) {
            return false;
        }
        if (getClass() != obj.getClass()) {
            return false;
        }
        final Post other = (Post) obj;
        if ((this.word == null) ? (other.word != null) : !this.word.equals(other.word)) {
            return false;
        }
        return true;
    }

    @Override
    public int hashCode() {
        return this.word.hashCode();
    }

    protected Vector<Document> getDocuments(int n){
        Vector<Document> docs = new Vector<Document>();
        for(int i=0; i<n && i < docs.size();i++){
            Document d = this.documents.elementAt(i);
            docs.add(d);
        }
        return docs;
    }
}


