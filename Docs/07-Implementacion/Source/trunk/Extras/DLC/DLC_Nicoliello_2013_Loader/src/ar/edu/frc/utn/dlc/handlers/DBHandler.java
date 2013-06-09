package ar.edu.frc.utn.dlc.handlers;

import ar.edu.frc.utn.dlc.Exceptions.SpiderException;
import ar.edu.frc.utn.dlc.backend.Post;
import ar.edu.frc.utn.dlc.backend.Vocabulary;

public interface DBHandler {

    /**Agrega una palabra al vocabulario
     * @param maxTf maxima frecuencia de una palabra en algun documento indexado
     * @param nr cantidad de documentos donde aparece esta palabra
     * @throws SpiderException
     */
    public void add2Vocabulary(String word, long maxTf, long nr) throws SpiderException;

    /**
     * Agrega una palabra a la lista de posteo
     * @param postNode Objeto que encapsula a una palabra con su max tf y su nr
     * @throws SpiderException
     */
    public void add2PostList(Post postNode) throws SpiderException;

    /**
     * Agrega una palabra a la lista de palabras
     * @param word la palabra a agregar
     * @throws SpiderException
     */
    public void add2WordList(String word) throws SpiderException;

    /**
     * Verifica a la existencia de una palabra en la lista de posteo
     * @param word la palabra a buscar
     * @return
     * @throws SpiderException
     */
    public boolean existsPostOf(String word) throws SpiderException;

    /**
     * Extrae todo el vocabulario de la BD
     * @return todo el vocabulario
     * @throws SpiderException
     */
    public Vocabulary getVocabulary() throws SpiderException;

    /**
     * Extrae un post de una palabra
     * @param word la palabra a buscar
     * @return Post la lista de posteo
     * @throws SpiderException
     */
    public Post getPostOf(String word) throws SpiderException;

    /**
     * Confirma los cambios en la DB
     * @throws SpiderException
     */
    public void commit() throws SpiderException;

    /**
     * Cierra la conexion con la base de datos
     * @throws SpiderException
     */
    public void close() throws SpiderException;

    /**
     * Retorna la cantidad todal de documentos indexados
     * @return long que representa la cantidad de documentos indexados
     * @throws SpiderException
     */
    public long getDocumentNumber() throws SpiderException;

    /**
     * Evalua si un documento fue o no indexado
     * @param absolutePath la ruta absoluta al documento
     * @return si el documento esta o no indexado
     * @throws SpiderException
     */
   public boolean isDocumentIndexed(String absolutePath) throws SpiderException;

   /**
    * Extrae la maxima frecuendia de una palabra
    * @param word la palabra a buscar
    * @return long que representa el maximo tf
    * @throws SpiderException
    */
   public long getMaxTfOf(String word) throws SpiderException;

   /**
    * La cantidad de documentos que tiene una palabra
    * @param word la palabr a buscar
    * @return  long que representa la cantidad de documentos donde aparece word
    * @throws SpiderException
    */
   public long getNrOf(String word) throws SpiderException;
}
