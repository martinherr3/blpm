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
 private double horaLlego;
 private String estadoPersna;
 private String esperoEnCola;
 private double finDeslizamiento;
 private double tiempoQueEspero;

    public PersonasAuxiliar(int nroPersona, double horaLlego, String estadoPersna, String esperoEnCola, double finDeslizamiento, double tiempoQueEspero) {
        this.nroPersona = nroPersona;
        this.horaLlego = horaLlego;
        this.estadoPersna = estadoPersna;
        this.esperoEnCola = esperoEnCola;
        this.finDeslizamiento = finDeslizamiento;
        this.tiempoQueEspero = tiempoQueEspero;
    }

    public int getNroPersona() {
        return nroPersona;
    }

    public void setNroPersona(int nroPersona) {
        this.nroPersona = nroPersona;
    }

    public double getHoraLlego() {
        return horaLlego;
    }

    public void setHoraLlego(double horaLlego) {
        this.horaLlego = horaLlego;
    }

    public String getEstadoPersna() {
        return estadoPersna;
    }

    public void setEstadoPersna(String estadoPersna) {
        this.estadoPersna = estadoPersna;
    }

    public String getEsperoEnCola() {
        return esperoEnCola;
    }

    public void setEsperoEnCola(String esperoEnCola) {
        this.esperoEnCola = esperoEnCola;
    }

    public double getFinDeslizamiento() {
        return finDeslizamiento;
    }

    public void setFinDeslizamiento(double finDeslizamiento) {
        this.finDeslizamiento = finDeslizamiento;
    }

    public double getTiempoQueEspero() {
        return tiempoQueEspero;
    }

    public void setTiempoQueEspero(double tiempoQueEspero) {
        this.tiempoQueEspero = tiempoQueEspero;
    }

    public PersonasAuxiliar() {
    }
 
    
    
}
