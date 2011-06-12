///////////////////////////////////////////////////////////
//  Reunion.cs
//  Implementation of the Class Reunion
//  Generated by Enterprise Architect
//  Created on:      12-jun-2011 07:44:23 p.m.
//  Original author: Bel�n
///////////////////////////////////////////////////////////




public class Reunion {

	private Curso _curso;
	private Datetime _fecha;
	private Usuario _gestorReunion;
	private Datetime _horario;
	private string _motivo;
	public Usuario m_Usuario;
	public MotivoCitacion m_MotivoCitacion;
	public Curso m_Curso;

	public Reunion(){

	}

	~Reunion(){

	}

	public virtual void Dispose(){

	}

	public Curso _curso{
		get{
			return _curso;
		}
		set{
			_curso = value;
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

	public Usuario _gestorReunion{
		get{
			return _gestorReunion;
		}
		set{
			_gestorReunion = value;
		}
	}

	public Datetime _horario{
		get{
			return _horario;
		}
		set{
			_horario = value;
		}
	}

	public string _motivo{
		get{
			return _motivo;
		}
		set{
			_motivo = value;
		}
	}

}//end Reunion