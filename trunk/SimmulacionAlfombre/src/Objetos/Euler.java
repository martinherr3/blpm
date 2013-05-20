/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package Objetos;

import java.io.Console;

/**
 *
 * @author Belén
 */
public class Euler {
    int i=0;
    double h=0;
    double xi=0;
    double Yi=0;
    double DY=0;
    double Yprox=0;
    
   

    public int getI() {
        return i;
    }

    public void setI(int i) {
        this.i = i;
    }

    public double getXi() {
        return xi;
    }

    public void setXi(double xi) {
        this.xi = xi+h;
    }

    public double getYi() {
        return Yi;
    }

    public void setYi(double Yi) {
        this.Yi = Yi;
    }

    public double getDY() {
        return DY;
    }

    public void setDY(double Yi) {
        this.DY = this.calcularDY(Yi);
    }

    public double getYprox() {
        return Yprox;
    }

    public double getH() {
        return h;
    }

    public void setH(double h) {
        this.h = h;
    }

    public void setYprox() {
        this.Yprox = this.calcularYprox();
    }

    public Euler() {
    }
    public Euler (double xi, double Yi, double h) {
        this.i=0;
        this.h=h;
        this.xi=xi;
        this.Yi=Yi;
        this.DY= this.calcularDY(Yi);
        this.Yprox=this.calcularYprox();
    
    }
    
    public double calcularDY( double Yi){
        double p;
        double z;
        double aux;
      p= Math.pow(Yi,2)*0.6;
      z=0.15*Yi;
      aux=p-z+8;
        return aux*(-1);}

    public void copiaObj(Euler e) {
        this.i=e.i;
        this.h=e.h;
        this.xi=e.xi;
        this.Yi=e.Yi;
        this.DY= e.DY;
        this.Yprox=e.Yprox;
    }
    
    public double calcularYprox(){
    double aux;
    aux= this.Yi+(this.h*this.DY); 
    return aux; }
    
    public void recalculoEuler(Euler e){
        this.i=e.i+1;
        this.h=e.h;
        this.xi=e.xi+e.h;
        this.Yi=e.Yprox;
        this.DY= e.calcularDY(this.Yi);
        this.Yprox=e.calcularYprox();
    }
    
//     public static void main(String[] args) {
//         Euler euler=null;
//         euler=new Euler(0,120,0.005);
//         System.out.println(euler.toString());
//         for (int i = 0; i < 10; i++) {
//             euler.recalculoEuler();
//             System.out.println(euler.toString());
//             }
//  }

    @Override
    public String toString() {
        return "Euler{" + "i=" + i + ", h=" + h + ", xi=" + xi + ", Yi=" + Yi + ", DY=" + DY + ", Yprox=" + Yprox + '}';
    }

    public void getI(Integer integer) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public void getXi(Double aDouble) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public void getYi(Double aDouble) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public void getDY(Double aDouble) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public void getYprox(Double aDouble) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }
     
}
