///////////////////////////////////////////////////////////
//  Encuesta.cs
//  Implementation of the Class Encuesta
//  Generated by Enterprise Architect
//  Created on:      12-jun-2011 07:44:15 p.m.
//  Original author: Pablo Nicoliello
///////////////////////////////////////////////////////////




public class Encuesta {

	private string _ambito;
	private Usuario _creador;
	private Datetime _fechaCreacion;
	private string _nombreEncuesta;
	private Pregunta _preguntas;
	public Pregunta m_Pregunta;
	public Usuario m_Usuario;

	public Encuesta(){

	}

	~Encuesta(){

	}

	public virtual void Dispose(){

	}

	public string _ambito{
		get{
			return _ambito;
		}
		set{
			_ambito = value;
		}
	}

	public Usuario _creador{
		get{
			return _creador;
		}
		set{
			_creador = value;
		}
	}

	public Datetime _fechaCreacion{
		get{
			return _fechaCreacion;
		}
		set{
			_fechaCreacion = value;
		}
	}

	public string _nombreEncuesta{
		get{
			return _nombreEncuesta;
		}
		set{
			_nombreEncuesta = value;
		}
	}

	public Pregunta _preguntas{
		get{
			return _preguntas;
		}
		set{
			_preguntas = value;
		}
	}

}//end Encuesta