///////////////////////////////////////////////////////////
//  TemasPlanificadosAsignatura.cs
//  Implementation of the Class TemasPlanificadosAsignatura
//  Generated by Enterprise Architect
//  Created on:      12-jun-2011 07:44:23 p.m.
//  Original author: Pablo Nicoliello
///////////////////////////////////////////////////////////




public class TemasPlanificadosAsignatura {

	private TemaContenido _contenidoEspecifico;
	private Datetime _fechaFinEstimado;
	private Datetime _fechaFinReal;
	private Datetime _fechaInicioEstimado;
	private Datetime _fechaInicioReal;
	private double _horasCatedra;
	public TemaContenido m_TemaContenido;

	public TemasPlanificadosAsignatura(){

	}

	~TemasPlanificadosAsignatura(){

	}

	public virtual void Dispose(){

	}

	public TemaContenido _contenidoEspecifico{
		get{
			return _contenidoEspecifico;
		}
		set{
			_contenidoEspecifico = value;
		}
	}

	public Datetime _fechaFinEstimado{
		get{
			return _fechaFinEstimado;
		}
		set{
			_fechaFinEstimado = value;
		}
	}

	public Datetime _fechaFinReal{
		get{
			return _fechaFinReal;
		}
		set{
			_fechaFinReal = value;
		}
	}

	public Datetime _fechaInicioEstimado{
		get{
			return _fechaInicioEstimado;
		}
		set{
			_fechaInicioEstimado = value;
		}
	}

	public Datetime _fechaInicioReal{
		get{
			return _fechaInicioReal;
		}
		set{
			_fechaInicioReal = value;
		}
	}

	public double _horasCatedra{
		get{
			return _horasCatedra;
		}
		set{
			_horasCatedra = value;
		}
	}

	public void calcularFechaFinEstimado(){

	}

	public void calcularFechaInicioEstimado(){

	}

}//end TemasPlanificadosAsignatura