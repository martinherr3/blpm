///////////////////////////////////////////////////////////
//  Sancion.cs
//  Implementation of the Class Sancion
//  Generated by Enterprise Architect
//  Created on:      12-jun-2011 07:44:23 p.m.
//  Original author: orkus
///////////////////////////////////////////////////////////




public class Sancion {

	private int _cantidad;
	private Datetime _fecha;
	private MotivoSancion _motivoSancion;
	private TipoSancion _tipoSancion;
	public MotivoSancion m_MotivoSancion;
	public TipoSancion m_TipoSancion;

	public Sancion(){

	}

	~Sancion(){

	}

	public virtual void Dispose(){

	}

	public int _cantidad{
		get{
			return _cantidad;
		}
		set{
			_cantidad = value;
		}
	}

	public Datetime _fecha{
		get{
			return _fecha;
		}
		set{
			_fecha = value;
		}
	}

	public MotivoSancion _motivoSancion{
		get{
			return _motivoSancion;
		}
		set{
			_motivoSancion = value;
		}
	}

	public TipoSancion _tipoSancion{
		get{
			return _tipoSancion;
		}
		set{
			_tipoSancion = value;
		}
	}

}//end Sancion