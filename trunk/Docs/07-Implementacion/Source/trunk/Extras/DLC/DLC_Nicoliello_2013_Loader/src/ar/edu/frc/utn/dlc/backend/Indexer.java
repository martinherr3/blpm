package ar.edu.frc.utn.dlc.backend;

import ar.edu.frc.utn.dlc.Exceptions.BuscadorException;
import ar.edu.frc.utn.dlc.Exceptions.SpiderException;
import ar.edu.frc.utn.dlc.handlers.DBHandler;
import ar.edu.frc.utn.dlc.handlers.FileHandler;
import java.io.File;
import java.util.Vector;

class Indexer {

    private DBHandler dbHandler;
    private Vocabulary vocabulary;

    /**
     * Constructor de una nueva instancia de la clase
     * @param dBConnection Manejador para conexion  a base de datos
     * @throws SpiderException 
     */
    public Indexer(DBHandler dBConnection) throws SpiderException {
        this.dbHandler = dBConnection;
        this.vocabulary = dbHandler.getVocabulary();
    }

    /**
     * Metodo sincronizado para agregar palabras a la base de datos
     * @param handler Manejador de archivo
     * @param file Archivo a agregar a la base de datos
     * @throws SpiderException
     */
    public synchronized void addToDb(FileHandler handler, File file) throws SpiderException, BuscadorException {
        String[] words = handler.getWords(file);
        Vector<String> indexedWords = new Vector<String>();
        for (String word : words) {
            if (!indexedWords.contains(word)) {
                long tf = this.calculateTf(word, words);
                Post post = new Post(word);
                post.addDocument(file, tf);
                this.dbHandler.add2PostList(post);
                this.dbHandler.add2WordList(word);
                long maxTf = dbHandler.getMaxTfOf(word);
                long nr = dbHandler.getNrOf(word);
                this.dbHandler.add2Vocabulary(word, maxTf, nr);
                this.vocabulary.addWord(word, maxTf, nr);
                indexedWords.add(word);
            }
        }
        this.dbHandler.commit();
    }

    // Calcula la frecuencia de una palabra
    private long calculateTf(String word, String[] words) {
        long count = 0;
        for (String w : words) {
            if (w.equals(word)) {
                count++;
            }
        }
        return count;
    }
}
