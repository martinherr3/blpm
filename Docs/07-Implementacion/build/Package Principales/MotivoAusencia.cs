///////////////////////////////////////////////////////////
//  MotivoAusencia.cs
//  Implementation of the Class MotivoAusencia
//  Generated by Enterprise Architect
//  Created on:      12-jun-2011 07:44:18 p.m.
//  Original author: orkus
///////////////////////////////////////////////////////////




public class MotivoAusencia {

	private string _descripcion;
	private int _idMotivo;

	public MotivoAusencia(){

	}

	~MotivoAusencia(){

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

	public int _idMotivo{
		get{
			return _idMotivo;
		}
		set{
			_idMotivo = value;
		}
	}

}//end MotivoAusencia