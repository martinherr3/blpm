///////////////////////////////////////////////////////////
//  Pregunta.cs
//  Implementation of the Class Pregunta
//  Generated by Enterprise Architect
//  Created on:      12-jun-2011 07:44:22 p.m.
//  Original author: Pablo Nicoliello
///////////////////////////////////////////////////////////




public class Pregunta {

	private Escala _escala;
	private Ponderacion _ponderacion;
	private string _textoPregunta;
	public Escala m_Escala;

	public Pregunta(){

	}

	~Pregunta(){

	}

	public virtual void Dispose(){

	}

	public Escala _escala{
		get{
			return _escala;
		}
		set{
			_escala = value;
		}
	}

	public Ponderacion _ponderacion{
		get{
			return _ponderacion;
		}
		set{
			_ponderacion = value;
		}
	}

	public string _textoPregunta{
		get{
			return _textoPregunta;
		}
		set{
			_textoPregunta = value;
		}
	}

}//end Pregunta