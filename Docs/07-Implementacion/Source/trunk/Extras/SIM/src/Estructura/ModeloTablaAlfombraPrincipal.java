/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package Estructura;

import Objetos.Euler;
import Objetos.AlfombraPrincipal;
import java.util.LinkedList;
import javax.swing.event.TableModelEvent;
import javax.swing.event.TableModelListener;
import javax.swing.table.TableModel;

/**
 *
 * @author Belén
 */
public class ModeloTablaAlfombraPrincipal implements TableModel{

    /**
     * Returns the number of columns in the model. A
     * <code>JTable</code> uses this method to determine how many columns it
     * should create and display by default.
     *
     * @return the number of columns in the model
     * @see #getRowCount
     *
     */
    public int getColumnCount() {
        // Devuelve el número de columnas del modelo, que coincide con el
        // número de datos que tenemos de cada persona.
        return 19;
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
        // Devuelve el número de personas en el modelo, es decir, el número
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
        AlfombraPrincipal aux;

        // Se obtiene la persona de la fila indicada
        aux = (AlfombraPrincipal) (datos.get(rowIndex));

        // Se obtiene el campo apropiado según el valor de columnIndex
        switch (columnIndex) {
            case 0:
                return aux.getReloj();
            case 1:
                return aux.getEvento();
            case 2:
                return aux.getEstadoAlfombra();
            case 3:
                return aux.getInicioSuspencion();
            
            case 4:
                return aux.getFinLimpieza();
            case 5:
                return aux.getRND();
            case 6:
                return aux.getLlegaPersona();
            case 7:
                return aux.getNroPersona();
            case 8:
                return aux.getHoraLlegoPers();
            case 9:
                return aux.getEstadoPers();
            case 10:
                return aux.getFinDeslizaPers();
            case 11:
                return aux.getTiempoEspera();
            case 12:
                return aux.getColaPesrsEspera();
            case 13:
                return aux.getColaPersDeslizan();
            case 14:
                return aux.getCantPersFinalizan();
            case 15:
                return aux.getCantPersEsperaron();
            case 16:
                return aux.getMaxColaEspera();
            case 17:
                return aux.getAcuTiempoEspera();
            case 18:
                return aux.getMaxTiempoEspera();
                    
                
            default:
                return null;
        }
    }

    /**
     * Borra del modelo la persona en la fila indicada
     */
    public void borraFila(int fila) {
        // Se borra la fila 
        datos.remove(fila);

        // Y se avisa a los suscriptores, creando un TableModelEvent...
        TableModelEvent evento = new TableModelEvent((TableModel) this, fila, fila,
                TableModelEvent.ALL_COLUMNS, TableModelEvent.DELETE);

        // ... y pasándoselo a los suscriptores
        avisaSuscriptores(evento);
    }

    /**
     * Añade una persona al final de la tabla
     */
    public void anhadeFila(AlfombraPrincipal nuevaFila) {
        // Añade la persona al modelo 
        datos.add(nuevaFila);

        // Avisa a los suscriptores creando un TableModelEvent...
        TableModelEvent evento;
        evento = new TableModelEvent((TableModel) this, this.getRowCount() - 1,
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
        // Añade el suscriptor a la lista de suscriptores
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
                return Double.class;
            case 1:
                return String.class;
            case 2:
                return String.class;
            case 3:
                 return Double.class;
            case 4:
                return Double.class;
            case 5:
                return Double.class;
            case 6:
                return Double.class;
            case 7:
                return Integer.class;
            case 8:
                return Double.class;
            case 9:
                return String.class;
            case 10:
                return Double.class;
            case 11:
                 return Double.class;
            case 12:
                return Integer.class;
            case 13:
                return Integer.class;
            case 14:
                return Integer.class;
            case 15:
                return Integer.class;
            case 16:
                return Integer.class;
            case 17:
                return Double.class;
            case 18:
                 return Double.class;    
                
            default:
                // Devuelve una clase Object por defecto.
                return Object.class;
        }
    }

    /**
     * Returns the name of the column at
     * <code>columnIndex</code>. This is used to initialize the table's column
     * header name. Note: this name does not need to be unique; two columns in a
     * table can have the same name.
     *
     * @param	columnIndex	the index of the column
     * @return the name of the column
     *
     */
    public String getColumnName(int columnIndex) {
        // Devuelve el nombre de cada columna. Este texto aparecerá en la
        // cabecera de la tabla.
        switch (columnIndex) {
            case 0:
                return "Reloj";
            case 1:
                return "Evento";
            case 2:
                return "Estado";
            case 3:
                return "SuspencionInicia";
            case 4:
                return "LimpiezaFin";
            case 5:
                return "rndLlegaPers";
            case 6:
                return "LlegaProxPers";
            case 7:
                return "NroPersona";
            case 8:
                return "HoraLlegó";
            case 9:
                return "EstadoPers";
            case 10:
                return "DeslizamientoFin";
            case 11:
                return "TotalEspera";
            case 12:
                return "EsperaColaPers";
             case 13:
                return "DeslizColaPers";
            case 14:
                return "TotalDeslizamientos";
            case 15:
                return "AcuPersEsper";
            case 16:
                return "MaxColaEspera";
            case 17:
                return "AcuTpoEspera";
            case 18:
                return "MaxTpoEspera";
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
        return true;
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
     * Sets the value in the cell at
     * <code>columnIndex</code> and
     * <code>rowIndex</code> to
     * <code>aValue</code>.
     *
     * @param	aValue	the new value
     * @param	rowIndex	the row whose value is to be changed
     * @param	columnIndex the column whose value is to be changed
     * @see #getValueAt
     * @see #isCellEditable
     *
     */
        public void setValueAt(Object aValue, int rowIndex, int columnIndex) {
        // Obtiene la persona de la fila indicada
        AlfombraPrincipal aux;
        aux = (AlfombraPrincipal) (datos.get(rowIndex));

        // Cambia el campo de Persona que indica columnIndex, poniendole el 
        // aValue que se nos pasa.

//        int indexRow = this.rowAutoFIFO();
//        EventosAuxiliares ob = (EventosAuxiliares) this.get(indexRow);
//        ob.setHoraInicioPintura(aux.getHoraInicioPintura());
//
//        EventosAuxiliares obAnterior = (EventosAuxiliares) this.get(indexRow - 1);
//
//        if (ob.getHoraInicioPintura() == ob.getHoraLlegada()) {
//            ob.setTiempoEnCola(obAnterior.getSumatoriaTiempoEnCola());
//        } else {
//            if (ob.getHoraInicioPintura() < ob.getHoraLlegada()) {
//                ob.setTiempoEnCola(ob.getHoraInicioPintura() - obAnterior.getHoraInicioPintura());
//            } else {
//                ob.setTiempoEnCola(ob.getHoraInicioPintura() - ob.getHoraLlegada());
//            }
//        }
//        ob.setSumatoriaTiempoEnCola(obAnterior.getSumatoriaTiempoEnCola() + ob.getTiempoEnCola());
//        ob.setHoraInicioPinturaRE(aux.getHoraInicioPinturaRE());
//        ob.setTiempoEnColaRE(ob.getHoraInicioPinturaRE() - ob.getHoraLlegada());
//        ob.setSumatoriaTiempoEnColaRE(obAnterior.getSumatoriaTiempoEnColaRE() + ob.getTiempoEnColaRE());
//        

        switch (columnIndex) {
            case 0:
                aux.getReloj((Double) aValue);
                break;
            case 1:
                aux.getEvento((String) aValue);
                break;
            case 2:
                aux.getRND((Double) aValue);
                break;
            case 3:
                aux.getLLegaProxPers((Double) aValue);
                break;
            case 4:
                aux.getFinLimpieza((Integer) aValue);
                break;
            case 5:
                aux.getEsHoraSuspender((Double) aValue);
                break;
            case 6:
                aux.getNroPersProxFinLanz((Integer) aValue);
                break;
            case 7:
                aux.getFinProxLanz((Double) aValue);
                break;
            case 8:
                aux.getCantPersEnLanz((Integer) aValue);
                break;
            case 9:
                aux.getEstadoAlfombra((String) aValue);
                break;
            case 10:
                aux.getMaxCola((Integer) aValue);
                break;
            case 11:
                aux.getMaxTiempoDEspera((Double) aValue);
                break;
            default:
                break;
        }

        // Avisa a los suscriptores del cambio, creando un TableModelEvent ...
        TableModelEvent evento = new TableModelEvent((TableModel) this, rowIndex, rowIndex,
                columnIndex);

        // ... y pasándoselo a los suscriptores.
        avisaSuscriptores(evento);
    }

    /**
     * Pasa a los suscriptores el evento.
     */
    private void avisaSuscriptores(TableModelEvent evento) {
        int i;
        // Bucle para todos los suscriptores en la lista, se llama al metodo
        // tableChanged() de los mismos, pasándole el evento.
        for (i = 0; i < listeners.size(); i++) {
            ((TableModelListener) listeners.get(i)).tableChanged(evento);
        }
    }
    /**
     * Lista con los datos. Cada elemento de la lista es una instancia de
     * Persona
     */
    private LinkedList datos = new LinkedList();
    /**
     * Lista de suscriptores. El JTable será un suscriptor de este modelo de
     * datos
     */
    private LinkedList listeners = new LinkedList();
//    public int rowAutoFIFO() {
//        for (int i = 1; i < this.getRowCount(); i++) {
//
//
//            if (((Euler) this.datos.get(i)).getHoraInicioPintura() == 0) {
//                return i;
//            }
//
//        }
//        return -1;
//    }
}
