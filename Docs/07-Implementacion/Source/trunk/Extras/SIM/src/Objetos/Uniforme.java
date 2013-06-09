/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package Objetos;

/**
 *
 * @author Eri
 */
public class Uniforme {

    private static Double rdo;
    private static Double rnd;
    public static Double getRdo(Double desde, Double hasta) {
        rnd =(Double)Math.random();
        rdo =(Double) (desde +((hasta-desde)*rnd ));
        return rdo;
    }

    public static Double getRnd() {
        return rnd;
    }






}
