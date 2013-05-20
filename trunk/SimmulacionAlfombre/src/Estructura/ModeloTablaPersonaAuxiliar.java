package Estructura;

/**
 * Javier Abell�n, 26 Oct 2003
 *
 * ModeloTabla.java
 *
 * Modelo de tabla para el ejmplo de uso del JTable
 */
import javax.swing.table.*;
import javax.swing.event.*;
import java.util.LinkedList;
import Objetos.PersonasAuxiliar;
import Objetos.AlfombraPrincipal;

/**
 * Modelo de personas. Cada fila es una persona y las columnas son los datos de
 * la persona. Implementa TableModel y dos m�todos para a�adir y eliminar
 * Personas del modelo
 */
public class ModeloTablaPersonaAuxiliar implements TableModel {

    /**
     * Returns the number of columns in the model. A
     * <code>JTable</code> uses this method to determine how many columns it
     * should create and display by default.
     *
     * @return the number of columns in the model
     * @see #getRowCount
     *
     */
//    public static int HORAINICIO = 1;
//    public static int NROPERS = 2;

    public int getColumnCount() {
        // Devuelve el n�mero de columnas del modelo, que coincide con el
        // n�mero de datos que tenemos de cada persona.
        return 5;
    }

    /**
     * Returns the number of rows in the model. A
     * <code>JTable</code> uses this method to determine how many rows it should
     * display. This method should be quick, as it is called frequently during
     * rendering.
     *
     * @return the number of rows in the model
     * @see #getColumnCount
     *
     */
    public int getRowCount() {
        // Devuelve el n�mero de personas en el modelo, es decir, el n�mero
        // de filas en la tabla.
        return datos.size();
    }

    /**
     * Returns the value for the cell at
     * <code>columnIndex</code> and
     * <code>rowIndex</code>.
     *
     * @param	rowIndex	the row whose value is to be queried
     * @param	columnIndex the column whose value is to be queried
     * @return	the value Object at the specified cell
     *
     */
    public Object getValueAt(int rowIndex, int columnIndex) {
        PersonasAuxiliar aux;

        // Se obtiene la persona de la fila indicada
        aux = (PersonasAuxiliar) (datos.get(rowIndex));

        // Se obtiene el campo apropiado seg�n el valor de columnIndex
        switch (columnIndex) {
           case 0:
                return aux.getNroPersona();
            case 1:
                return aux.getTiempoLlegada();
            case 2:
                return aux.getEstado();
            case 3:
                return aux.getFinLanza();
           case 4:
                return aux.getTiempoEspera();
           default:
                return null;
        }
    }
    /*

    
     */

    /**
     * Borra del modelo la persona en la fila indicada
     */
    public void quitarObjeto(int fila) {
        // Se borra la fila 
        datos.remove(fila);

        // Y se avisa a los suscriptores, creando un TableModelEvent...
        TableModelEvent evento = new TableModelEvent(this, fila, fila,
                TableModelEvent.ALL_COLUMNS, TableModelEvent.DELETE);

        // ... y pas�ndoselo a los suscriptores
        avisaSuscriptores(evento);
    }

    public Object get(int index) {
        return datos.get(index);
    }

    /**
     * A�ade una persona al final de la tabla
     */
    public void agregarObjeto(Object o) {
        // A�ade la persona al modelo 
        datos.add(o);

        // Avisa a los suscriptores creando un TableModelEvent...
        TableModelEvent evento;
        evento = new TableModelEvent(this, this.getRowCount() - 1,
                this.getRowCount() - 1, TableModelEvent.ALL_COLUMNS,
                TableModelEvent.INSERT);

        // ... y avisando a los suscriptores
        avisaSuscriptores(evento);
    }

    /**
     * Adds a listener to the list that is notified each time a change to the
     * data model occurs.
     *
     * @param	l	the TableModelListener
     *
     */
    public void addTableModelListener(TableModelListener l) {
        // A�ade el suscriptor a la lista de suscriptores
        listeners.add(l);
    }

    /**
     * Returns the most specific superclass for all the cell values in the
     * column. This is used by the
     * <code>JTable</code> to set up a default renderer and editor for the
     * column.
     *
     * @param columnIndex the index of the column
     * @return the common ancestor class of the object values in the model.
     *
     */
    public Class getColumnClass(int columnIndex) {
        // Devuelve la clase que hay en cada columna.
        switch (columnIndex) {
            case 0:
                return Integer.class;
            case 1:
                return Double.class;
            case 2:
                return String.class;
            case 3:
                return Double.class;
            case 4:
                return Double.class;
            default:
                return Object.class;
        }
    }

  public void setValue(Object aValue, int rowIndex, int columnIndex) {
        EventosAuxiliares aux;
        aux = (EventosAuxiliares) aValue;

        int indexRow = this.rowAutoFIFO();
        EventosAuxiliares ob = (EventosAuxiliares) this.get(indexRow);
        ob.setHoraInicioPintura(aux.getHoraInicioPintura());

        EventosAuxiliares obAnterior = (EventosAuxiliares) this.get(indexRow - 1);

        if (ob.getHoraInicioPintura() == ob.getHoraLlegada()) {
            ob.setTiempoEnCola(obAnterior.getSumatoriaTiempoEnCola());
        } else {
            if (ob.getHoraInicioPintura() < ob.getHoraLlegada()) {
                ob.setTiempoEnCola(ob.getHoraInicioPintura() - obAnterior.getHoraInicioPintura());
            } else {
                ob.setTiempoEnCola(ob.getHoraInicioPintura() - ob.getHoraLlegada());
            }
        }
        ob.setSumatoriaTiempoEnCola(obAnterior.getSumatoriaTiempoEnCola() + ob.getTiempoEnCola());
        ob.setHoraInicioPinturaRE(aux.getHoraInicioPinturaRE());
        ob.setTiempoEnColaRE(ob.getHoraInicioPinturaRE() - ob.getHoraLlegada());
        ob.setSumatoriaTiempoEnColaRE(obAnterior.getSumatoriaTiempoEnColaRE() + ob.getTiempoEnColaRE());
    }
  
  
public void copiarSumatoria() {
        int i = 0;
        EventosAuxiliares aux = new EventosAuxiliares();
        EventosAuxiliares filaDeLaTabla = new EventosAuxiliares();
        for (i = this.getRowCount() - 1; i > -1; i--) {
            aux = (EventosAuxiliares) this.get(i);
            if (aux.getSumatoriaTiempoEnCola() != 0) {
                for (int j = i + 1; j < this.getRowCount(); j++) {
                    filaDeLaTabla = (EventosAuxiliares) this.get(j);
                    filaDeLaTabla.setSumatoriaTiempoEnCola(aux.getSumatoriaTiempoEnCola());
                    filaDeLaTabla.setSumatoriaTiempoEnColaRE(aux.getSumatoriaTiempoEnColaRE());
                }
                break;
            }

        }

        for (int k = 1; k < this.getRowCount(); k++) {
            aux = (EventosAuxiliares) this.get(k);
            if (aux.getSumatoriaTiempoEnCola() == 0) {
                filaDeLaTabla = (EventosAuxiliares) this.get(k - 1);
                aux.setSumatoriaTiempoEnCola(filaDeLaTabla.getSumatoriaTiempoEnCola());
                aux.setSumatoriaTiempoEnColaRE(filaDeLaTabla.getSumatoriaTiempoEnColaRE());
            }
        }


    }
    public String getColumnName(int columnIndex) 
    {
        // Devuelve el nombre de cada columna. Este texto aparecerá en la
        // cabecera de la tabla.
        switch (columnIndex)
        {
            case 0:
                return "nroPersona";
            case 1:
                return "tiempoLlegada";
            case 2:
                return "estado";
            case 3:
                return "finLanza";
            case 4:
                return "TiempoEspera";
            default:
                return null;
        }
    }
    /**
     * Returns true if the cell at
     * <code>rowIndex</code> and
     * <code>columnIndex</code> is editable. Otherwise,
     * <code>setValueAt</code> on the cell will not change the value of that
     * cell.
     *
     * @param	rowIndex	the row whose value to be queried
     * @param	columnIndex	the column whose value to be queried
     * @return	true if the cell is editable
     * @see #setValueAt
     *
     */
    public boolean isCellEditable(int rowIndex, int columnIndex) {
        // Permite que la celda sea editable.
        return false;
    }

    /**
     * Removes a listener from the list that is notified each time a change to
     * the data model occurs.
     *
     * @param	l	the TableModelListener
     *
     */
    public void removeTableModelListener(TableModelListener l) {
        // Elimina los suscriptores.
        listeners.remove(l);
    }

    /**
     * Pasa a los suscriptores el evento.
     */
    private void avisaSuscriptores(TableModelEvent evento) {
        int i;

        // Bucle para todos los suscriptores en la lista, se llama al metodo
        // tableChanged() de los mismos, pas�ndole el evento.
        for (i = 0; i < listeners.size(); i++) {
            ((TableModelListener) listeners.get(i)).tableChanged(evento);
        }
    }

 public int rowPersonaFIFO() {
        for (int i = 1; i < this.getRowCount(); i++) {

            if (((PersonasAuxiliar) this.datos.get(i)).getEstado().equals("En Lanzamiento")
                    && ((PersonasAuxiliar) this.datos.get(i)).getFinLanza() != 0.0) {
                return i;
            }
        }
        return -1;
    }
    /**
     * Lista con los datos. Cada elemento de la lista es una instancia de
     * Persona
     */
    private LinkedList datos = new LinkedList();
    /**
     * Lista de suscriptores. El JTable ser� un suscriptor de este modelo de
     * datos
     */
    private LinkedList listeners = new LinkedList();

    public void setValueAt(Object aValue, int rowIndex, int columnIndex) {
        throw new UnsupportedOperationException("Not supported yet.");
    }
}
