/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package Objetos;

/**
 *
 * @author Belén
 */
public class PersonasAuxiliar {
    private int nroPersona;
    private double tiempoLlegada;
    private String estado;
    private double finLanza;
    private double TiempoEspera;

    public int getNroPersona() {
        return nroPersona;
    }

    public void setNroPersona(int nroPersona) {
        this.nroPersona = nroPersona;
    }

    public double getTiempoLlegada() {
        return tiempoLlegada;
    }

    public void setTiempoLlegada(double tiempoLlegada) {
        this.tiempoLlegada = tiempoLlegada;
    }

    public String getEstado() {
        return estado;
    }

    public void setEstado(String estado) {
        this.estado = estado;
    }

    public double getFinLanza() {
        return finLanza;
    }

    public void setFinLanza(double finLanza) {
        this.finLanza = finLanza;
    }

    public double getTiempoEspera() {
        return TiempoEspera;
    }

    public void setTiempoEspera(double TiempoEspera) {
        this.TiempoEspera = TiempoEspera;
    }

    public PersonasAuxiliar(int nroPersona, double tiempoLlegada, String estado, double finLanza, double TiempoEspera) {
        this.nroPersona = nroPersona;
        this.tiempoLlegada = tiempoLlegada;
        this.estado = estado;
        this.finLanza = finLanza;
        this.TiempoEspera = TiempoEspera;
    }

    public PersonasAuxiliar() {
    }

    public void getNroPersona(Integer integer) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public void getTiempoLlegada(Double aDouble) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public void getEstado(String string) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public void getFinLanza(Double aDouble) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public void getTiempoEspera(Double aDouble) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }
    
    
}
