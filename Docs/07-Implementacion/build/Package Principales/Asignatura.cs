///////////////////////////////////////////////////////////
//  Asignatura.cs
//  Implementation of the Class Asignatura
//  Generated by Enterprise Architect
//  Created on:      12-jun-2011 07:44:11 p.m.
//  Original author: orkus
///////////////////////////////////////////////////////////




public class Asignatura {

	private Contenido _contenidos;
	private Curso _curso;
	private DiasHorarios _diasHorarios;
	private Docente _docente;
	private Especialidad _especialidad;
	private string _nombre;
	public Contenido m_Contenido;
	public Curso m_Curso;
	public DiasHorarios m_DiasHorarios;
	public Docente m_Docente;
	public Especialidad m_Especialidad;

	public Asignatura(){

	}

	~Asignatura(){

	}

	public virtual void Dispose(){

	}

	public Contenido _contenidos{
		get{
			return _contenidos;
		}
		set{
			_contenidos = value;
		}
	}

	public Curso _curso{
		get{
			return _curso;
		}
		set{
			_curso = value;
		}
	}

	public DiasHorarios _diasHorarios{
		get{
			return _diasHorarios;
		}
		set{
			_diasHorarios = value;
		}
	}

	public Docente _docente{
		get{
			return _docente;
		}
		set{
			_docente = value;
		}
	}

	public Especialidad _especialidad{
		get{
			return _especialidad;
		}
		set{
			_especialidad = value;
		}
	}

	public string _nombre{
		get{
			return _nombre;
		}
		set{
			_nombre = value;
		}
	}

}//end Asignatura