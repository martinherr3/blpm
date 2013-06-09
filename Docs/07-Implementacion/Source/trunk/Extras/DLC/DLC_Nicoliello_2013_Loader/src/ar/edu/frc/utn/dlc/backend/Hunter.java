package ar.edu.frc.utn.dlc.backend;

import ar.edu.frc.utn.dlc.Exceptions.SpiderException;
import ar.edu.frc.utn.dlc.Exceptions.BuscadorException;
import ar.edu.frc.utn.dlc.handlers.DBHandler;
import ar.edu.frc.utn.dlc.handlers.ErrorHandler;
import java.util.List;
import java.util.Vector;

class Hunter {

    private static final String PATTERN_END_WORD = "\\W";
    private DBHandler dbHandler;
    private ErrorHandler errorHandler;

    /**
     * Constructor de una instancia de la clase
     * @param connectionHandler Manejador de la base de datos
     * @param errorHandler Manejador de errores
     */
    public Hunter(DBHandler connectionHandler, ErrorHandler errorHandler) {
        this.dbHandler = connectionHandler;
        this.errorHandler = errorHandler;
    }

    /**
     * Efectua la busqueda de documentos que coincidan con el criterio de busqueda
     * @param query frase a buscar
     * @param maxMatches cantidad maxima de resultados a devolver
     * @return Retorna un conjunto de documentos
     */
    public synchronized Document[] search(String query, int maxMatches) {
        Document[] docs = null;
        try {
            String[] words = this.split(query);
            Vector<Post> posts = getPostOf(words);
            docs = getDocuments(posts, maxMatches);
        } catch (SpiderException ex) {
            //this.errorWrapper(ex);
        }
        return docs;
    }

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

    /*
     * Divide el QUERY en palabras independientes
     */
    private String[] split(String query) throws SpiderException {
        Vocabulary vocabulary = this.dbHandler.getVocabulary();
        Vector<String> splited = new Vector<String>();
        for (String w : query.split(Hunter.PATTERN_END_WORD)) {
            if (!w.isEmpty()) {
                splited.add(w);
                for (int i = 0; i < splited.size() - 1; i++) {
                    String wordI = splited.elementAt(i);
                    String wordJ = splited.elementAt(i + 1);
                    VocabularyNode nodeI = vocabulary.get(wordI);
                    VocabularyNode nodeJ = vocabulary.get(wordJ);
                    if (nodeI.getNr() > nodeJ.getNr()) {
                        splited.setElementAt(wordI, i + 1);
                        splited.setElementAt(wordJ, i);
                    }
                }//for
            }//if
        }//for
        return splited.toArray(new String[splited.size()]);
    }

    /*
     * Extrae los post de todas las palabras a buscar
     */
    private Vector<Post> getPostOf(String[] words) throws SpiderException {
        Vector<Post> posts = new Vector<Post>();
        for (String word : words) {
            Post post = this.dbHandler.getPostOf(word);
            posts.add(post);
        }
        return posts;
    }

    /*
     * Devuelve los primeros <code>maxMatches</code> documentos de todos los
     * post de una busqueda
     */
    private Document[] getDocuments(Vector<Post> posts, int maxMatches) {
        Vector<Document> candidates = new Vector<Document>();
        for (Post post : posts) {
            for (Document d : post.getDocuments()) {
                candidates.add(d);
            }
        }
        for (Document d : candidates) {
            int promote = this.getPromotes(d, candidates);
            candidates = this.removeDuplicated(d, candidates);
            candidates = this.doPromote(d, promote, candidates);
        }
        int last = maxMatches;
        if (last >= candidates.size()) {
            last = candidates.size();
        }
        List<Document> result = candidates.subList(0, last);

        return result.toArray(new Document[result.size()]);
    }

    /*
     * Devuelve un valor entero igual a la cantidad de veces que el ducumento
     * aparece en el vector de candidatos
     */
    private int getPromotes(Document d, Vector<Document> candidates) {
        boolean isFirst = true;
        int promote = 0;
        for (int i = 0; i < candidates.size(); i++) {
            Document doc = candidates.elementAt(i);
            if (doc.equals(d) && isFirst) {
                isFirst = false;
            } else if (doc.equals(d)) {
                promote++;
            }
        }
        return promote;
    }

    /*
     * Remueve todos los documentos duplicados del vector
     */
    private Vector<Document> removeDuplicated(Document d, Vector<Document> candidates) {
        Vector<Document> notDuplicated = new Vector<Document>(candidates);
        int first = notDuplicated.indexOf(d);
        while (notDuplicated.remove(d)) {
        }
        notDuplicated.add(first, d);
        return notDuplicated;
    }

    /*
     * Pociciona el documento "promote" lugares a la izquierda de su posicion
     * original
     */
    private Vector<Document> doPromote(Document d, int promote, Vector<Document> candidates) {
        int index = candidates.indexOf(d);
        Vector<Document> sorted = new Vector<Document>(candidates);
        if (index != 0) {
            int newIndex = index - promote;
            if (newIndex < 0) {
                newIndex = 0;
            }
            sorted.remove(index);
            sorted.add(newIndex, d);
        }
        return sorted;
    }
}
