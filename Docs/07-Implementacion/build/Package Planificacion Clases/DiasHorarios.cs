///////////////////////////////////////////////////////////
//  DiasHorarios.cs
//  Implementation of the Class DiasHorarios
//  Generated by Enterprise Architect
//  Created on:      12-jun-2011 07:44:14 p.m.
//  Original author: Pablo Nicoliello
///////////////////////////////////////////////////////////




public class DiasHorarios {

	private string _dia;
	/// <summary>
	/// Hace referencia a si es la primer hora, la segunda, terera, etc.
	/// </summary>
	private int _hora;
	private double _horasCatedra;

	public DiasHorarios(){

	}

	~DiasHorarios(){

	}

	public virtual void Dispose(){

	}

	public string _dia{
		get{
			return _dia;
		}
		set{
			_dia = value;
		}
	}

	/// <summary>
	/// Hace referencia a si es la primer hora, la segunda, terera, etc.
	/// </summary>
	public int _hora{
		get{
			return _hora;
		}
		set{
			_hora = value;
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

}//end DiasHorarios