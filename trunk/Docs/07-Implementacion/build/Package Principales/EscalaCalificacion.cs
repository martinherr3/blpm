///////////////////////////////////////////////////////////
//  EscalaCalificacion.cs
//  Implementation of the Class EscalaCalificacion
//  Generated by Enterprise Architect
//  Created on:      12-jun-2011 07:44:15 p.m.
//  Original author: orkus
///////////////////////////////////////////////////////////




public class EscalaCalificacion {

	private string _descripcion;
	private ValoresEscalaCalificacion _valores;

	public EscalaCalificacion(){

	}

	~EscalaCalificacion(){

	}

	public virtual void Dispose(){

	}

	public string _descripcion{
		get{
			return _descripcion;
		}
		set{
			_descripcion = value;
		}
	}

	public ValoresEscalaCalificacion _valores{
		get{
			return _valores;
		}
		set{
			_valores = value;
		}
	}

}//end EscalaCalificacion