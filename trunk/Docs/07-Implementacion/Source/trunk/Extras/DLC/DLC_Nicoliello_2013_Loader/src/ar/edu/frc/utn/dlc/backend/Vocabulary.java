package ar.edu.frc.utn.dlc.backend;

import java.util.Hashtable;

public final class Vocabulary extends Hashtable<String, VocabularyNode> {

    private static Vocabulary instance;

    /**
     * Devuelve el vocabulario
     * @return objeto del tipo Vocabulario
     */
    public static synchronized Vocabulary getVocabulary() {
        if (Vocabulary.instance == null) {
            Vocabulary.instance = new Vocabulary();
        }
        return Vocabulary.instance;
    }

    private Vocabulary() {
    }

    /**
     * Agrega una palabra al vocabulario
     * @param word Palabra a agregar
     * @param maxTf Maxima ocurrencia de la palabra en un documento
     * @param nr Cantidad de documentos donde aparece la palabra
     */
    public synchronized void addWord(String word, long maxTf, long nr){
        VocabularyNode vn = new VocabularyNode(word, maxTf, nr);
        this.put(word, vn);
    }   
}