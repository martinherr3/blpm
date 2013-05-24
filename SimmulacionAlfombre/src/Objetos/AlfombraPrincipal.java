/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package Objetos;

/**
 *
 * @author Belén
 */
public class AlfombraPrincipal {

    private double reloj;
    private String evento;
    private String estadoAlfombra;
    private double inicioSuspencion;
    private double finLimpieza;
    private double RND;
    private double llegaPersona;
    private int nroPersona;
    private double horaLlegoPers;
    private String  EstadoPers;
    private double finDeslizaPers;
    private double tiempoEspera;
    private int colaPesrsEspera;
    private int colaPersDeslizan;
    private int cantPersFinalizan;
    private int cantPersEsperaron;
    private int maxColaEspera;
    private double acuTiempoEspera;
    private double maxTiempoEspera;

    public double getReloj() {
        return reloj;
    }

    public AlfombraPrincipal() {
    }

    public int getCantPersEsperaron() {
        return cantPersEsperaron;
    }

    public void setCantPersEsperaron(int cantPersEsperaron) {
        this.cantPersEsperaron = cantPersEsperaron;
    }

    public void setReloj(double reloj) {
        this.reloj = reloj;
    }

    public String getEvento() {
        return evento;
    }

    public void setEvento(String evento) {
        this.evento = evento;
    }

    public String getEstadoAlfombra() {
        return estadoAlfombra;
    }

    public void setEstadoAlfombra(String estadoAlfombra) {
        this.estadoAlfombra = estadoAlfombra;
    }

    public double getInicioSuspencion() {
        return inicioSuspencion;
    }

    public void setInicioSuspencion(double inicioSuspencion) {
        this.inicioSuspencion = inicioSuspencion;
    }

    public double getFinLimpieza() {
        return finLimpieza;
    }

    public void setFinLimpieza(double finLimpieza) {
        this.finLimpieza = finLimpieza;
    }

    public double getRND() {
        return RND;
    }

    public void setRND(double RND) {
        this.RND = RND;
    }

    public double getLlegaPersona() {
        return llegaPersona;
    }

    public void setLlegaPersona(double llegaPersona) {
        this.llegaPersona = llegaPersona;
    }

    public int getNroPersona() {
        return nroPersona;
    }

    public void setNroPersona(int nroPersona) {
        this.nroPersona = nroPersona;
    }

    public double getHoraLlegoPers() {
        return horaLlegoPers;
    }

    public void setHoraLlegoPers(double horaLlegoPers) {
        this.horaLlegoPers = horaLlegoPers;
    }

    public String getEstadoPers() {
        return EstadoPers;
    }

    public void setEstadoPers(String EstadoPers) {
        this.EstadoPers = EstadoPers;
    }

    public double getFinDeslizaPers() {
        return finDeslizaPers;
    }

    public void setFinDeslizaPers(double finDeslizaPers) {
        this.finDeslizaPers = finDeslizaPers;
    }

    public double getTiempoEspera() {
        return tiempoEspera;
    }

    public void setTiempoEspera(double tiempoEspera) {
        this.tiempoEspera = tiempoEspera;
    }

    public int getColaPesrsEspera() {
        return colaPesrsEspera;
    }

    public void setColaPesrsEspera(int colaPesrsEspera) {
        this.colaPesrsEspera = colaPesrsEspera;
    }

    public int getColaPersDeslizan() {
        return colaPersDeslizan;
    }

    public void setColaPersDeslizan(int colaPersDeslizan) {
        this.colaPersDeslizan = colaPersDeslizan;
    }

    public int getCantPersFinalizan() {
        return cantPersFinalizan;
    }

    public void setCantPersFinalizan(int cantPersFinalizan) {
        this.cantPersFinalizan = cantPersFinalizan;
    }

    public int getMaxColaEspera() {
        return maxColaEspera;
    }

    public void setMaxColaEspera(int maxColaEspera) {
        this.maxColaEspera = maxColaEspera;
    }

    public double getAcuTiempoEspera() {
        return acuTiempoEspera;
    }

    public void setAcuTiempoEspera(double acuTiempoEspera) {
        this.acuTiempoEspera = acuTiempoEspera;
    }

    public double getMaxTiempoEspera() {
        return maxTiempoEspera;
    }

    public void setMaxTiempoEspera(double maxTiempoEspera) {
        this.maxTiempoEspera = maxTiempoEspera;
    }

    public AlfombraPrincipal(double reloj, String evento, String estadoAlfombra, double inicioSuspencion, double finLimpieza, double RND, double llegaPersona, int nroPersona, double horaLlegoPers, String EstadoPers, double finDeslizaPers, double tiempoEspera, int colaPesrsEspera, int colaPersDeslizan, int cantPersFinalizan, int maxColaEspera, double maxTiempoEspera) {
        this.reloj = reloj;
        this.evento = evento;
        this.estadoAlfombra = estadoAlfombra;
        this.inicioSuspencion = inicioSuspencion;
        this.finLimpieza = finLimpieza;
        this.RND = RND;
        this.llegaPersona = llegaPersona;
        this.nroPersona = nroPersona;
        this.horaLlegoPers = horaLlegoPers;
        this.EstadoPers = EstadoPers;
        this.finDeslizaPers = finDeslizaPers;
        this.tiempoEspera = tiempoEspera;
        this.colaPesrsEspera = colaPesrsEspera;
        this.colaPersDeslizan = colaPersDeslizan;
        this.cantPersFinalizan = cantPersFinalizan;
        this.maxColaEspera = maxColaEspera;
        this.maxTiempoEspera = maxTiempoEspera;
    }

    public void getReloj(Double aDouble) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public void getEvento(String string) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public void getRND(Double aDouble) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public void getLLegaProxPers(Double aDouble) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public void getEsHoraSuspender(Double aDouble) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public void getNroPersProxFinLanz(Integer integer) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public void getFinLimpieza(Integer integer) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public void getFinProxLanz(Double aDouble) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public void getCantPersEnLanz(Integer integer) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public void getEstadoAlfombra(String string) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public void getMaxCola(Integer integer) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public void getMaxTiempoDEspera(Double aDouble) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    
}